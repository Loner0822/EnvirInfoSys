using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class PasswordForm : XtraForm
	{
		public string unitid = "";

		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private AccessHelper ahp = null;

		private string pw_mode = "";

		private CheckedListBoxItem[] items = new CheckedListBoxItem[5]
		{
			new CheckedListBoxItem("服务器IP设置权限", isChecked: false),
			new CheckedListBoxItem("边界线属性设置权限", isChecked: false),
			new CheckedListBoxItem("图符管理设置权限", isChecked: false),
			new CheckedListBoxItem("地图设置权限", isChecked: false),
			new CheckedListBoxItem("查看日志权限", isChecked: false)
            //new CheckedListBoxItem("人员管辖对应权限", isChecked: false)
        };

		private IContainer components = null;

		private GroupControl groupControl1;

		private RadioGroup radioGroup1;

		private GroupControl groupControl2;

		private CheckedListBoxControl checkedListBoxControl1;

		private Splitter splitter1;

		private GroupControl groupControl3;

		private SimpleButton simpleButton1;

		private TextEdit textEdit3;

		private TextEdit textEdit2;

		private TextEdit textEdit1;

		private LabelControl labelControl3;

		private LabelControl labelControl2;

		private LabelControl labelControl1;

		public PasswordForm()
		{
			InitializeComponent();
		}

		private void PasswordForm_Shown(object sender, EventArgs e)
		{
			ahp = new AccessHelper(WorkPath + "data\\PASSWORD_H0001Z000E00.mdb");
			pw_mode = radioGroup1.Properties.Items[0].Description;
			checkedListBoxControl1.Items.AddRange(items);
			radioGroup1.SelectedIndex = 1;
			radioGroup1.SelectedIndex = 0;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string text = textEdit1.Text;
			string text2 = textEdit2.Text;
			string text3 = textEdit3.Text;
			string sql = "select PWMD5 from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWNAME = '" + pw_mode + "' and PWMD5 = '" + GetMd5_16byte(text) + "' and UNITID = '" + unitid + "'";
			DataTable dataTable = ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				if (text2 == text)
				{
					textEdit1.Text = "";
					textEdit2.Text = "";
					textEdit3.Text = "";
					XtraMessageBox.Show("新密码与旧密码相同!");
					return;
				}
				if (text2 != text3)
				{
					textEdit2.Text = "";
					textEdit3.Text = "";
					XtraMessageBox.Show("新密码与密码确认不匹配!");
					return;
				}
				sql = "update PASSWORD_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', PWMD5 = '" + GetMd5_16byte(text2) + "' where ISDELETE = 0 and PWNAME = '" + pw_mode + "' and UNITID = '" + unitid + "'";
				ahp.ExecuteSql(sql, (OleDbParameter[])null);
				XtraMessageBox.Show("修改成功!");
			}
			else
			{
				XtraMessageBox.Show("密码不正确,请重新输入!");
			}
			textEdit1.Text = "";
			textEdit2.Text = "";
			textEdit3.Text = "";
		}

		public static string GetMd5_16byte(string ConvertString)
		{
			string empty = string.Empty;
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			empty = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(ConvertString)), 4, 8);
			empty = empty.Replace("-", "");
			return empty.ToLower();
		}

		private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
		{
			RadioGroupItem radioGroupItem = radioGroup1.Properties.Items[radioGroup1.SelectedIndex];
			if (radioGroupItem.Description == "管理员密码")
			{
				groupControl2.Visible = true;
			}
			else
			{
				groupControl2.Visible = false;
			}
			pw_mode = radioGroupItem.Description;
			string sql = "select AUTHORITY from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWNAME = '" + pw_mode + "' and UNITID = '" + unitid + "'";
			if (ahp == null)
			{
				ahp = new AccessHelper(WorkPath + "data\\PASSWORD_H0001Z000E00.mdb");
			}
			DataTable dataTable = ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count <= 0)
			{
				return;
			}
			string text = dataTable.Rows[0]["AUTHORITY"].ToString();
			string[] array = text.Split(';');
			for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < array.Length; j++)
				{
					if (checkedListBoxControl1.Items[i].ToString() == array[j])
					{
						checkedListBoxControl1.SetItemChecked(i, value: true);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					checkedListBoxControl1.SetItemChecked(i, value: false);
				}
			}
		}

		private void checkedListBoxControl1_Leave(object sender, EventArgs e)
		{
			string text = "";
			for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
			{
				if (checkedListBoxControl1.GetItemChecked(i))
				{
					text = text + checkedListBoxControl1.Items[i].ToString() + ";";
				}
			}
			string sql = "update PASSWORD_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', AUTHORITY = '" + text + "' where ISDELETE = 0 and PWNAME = '" + pw_mode + "' and UNITID = '" + unitid + "'";
			ahp.ExecuteSql(sql, (OleDbParameter[])null);
		}

		private void PasswordForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			groupControl1.Focus();
			string sql = "select AUTHORITY from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWNAME = '管理员密码' and UNITID = '" + unitid + "'";
			if (ahp == null)
			{
				ahp = new AccessHelper(WorkPath + "data\\PASSWORD_H0001Z000E00.mdb");
			}
			DataTable dataTable = ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				string text = dataTable.Rows[0]["AUTHORITY"].ToString();
				FileReader.Authority = text.Split(';');
			}
			ahp.CloseConn();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordForm));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.radioGroup1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 491);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "模式选择";
            // 
            // radioGroup1
            // 
            this.radioGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioGroup1.Location = new System.Drawing.Point(2, 31);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "管理员密码"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "编辑模式"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "查看模式")});
            this.radioGroup1.Size = new System.Drawing.Size(196, 458);
            this.radioGroup1.TabIndex = 0;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.checkedListBoxControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl2.Location = new System.Drawing.Point(200, 278);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(416, 213);
            this.groupControl2.TabIndex = 1;
            this.groupControl2.Text = "设置权限";
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxControl1.Location = new System.Drawing.Point(2, 31);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(412, 180);
            this.checkedListBoxControl1.TabIndex = 0;
            this.checkedListBoxControl1.Leave += new System.EventHandler(this.checkedListBoxControl1_Leave);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(200, 268);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(416, 10);
            this.splitter1.TabIndex = 8;
            this.splitter1.TabStop = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.simpleButton1);
            this.groupControl3.Controls.Add(this.textEdit3);
            this.groupControl3.Controls.Add(this.textEdit2);
            this.groupControl3.Controls.Add(this.textEdit1);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(200, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(416, 268);
            this.groupControl3.TabIndex = 9;
            this.groupControl3.Text = "修改密码";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Location = new System.Drawing.Point(170, 217);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(112, 34);
            this.simpleButton1.TabIndex = 6;
            this.simpleButton1.Text = "确认";
            this.simpleButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textEdit3
            // 
            this.textEdit3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit3.Location = new System.Drawing.Point(195, 157);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.PasswordChar = '*';
            this.textEdit3.Size = new System.Drawing.Size(150, 30);
            this.textEdit3.TabIndex = 5;
            // 
            // textEdit2
            // 
            this.textEdit2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit2.Location = new System.Drawing.Point(195, 107);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.PasswordChar = '*';
            this.textEdit2.Size = new System.Drawing.Size(150, 30);
            this.textEdit2.TabIndex = 4;
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit1.Location = new System.Drawing.Point(195, 57);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.PasswordChar = '*';
            this.textEdit1.Size = new System.Drawing.Size(150, 30);
            this.textEdit1.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl3.Location = new System.Drawing.Point(81, 161);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 22);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "密码确认";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl2.Location = new System.Drawing.Point(81, 111);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 22);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "新密码";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl1.Location = new System.Drawing.Point(81, 61);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 22);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "旧密码";
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 491);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PasswordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密码管理";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PasswordForm_FormClosing);
            this.Shown += new System.EventHandler(this.PasswordForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

		}
	}
}

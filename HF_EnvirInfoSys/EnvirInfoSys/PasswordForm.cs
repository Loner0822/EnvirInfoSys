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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.PasswordForm));
			groupControl1 = new DevExpress.XtraEditors.GroupControl();
			radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
			groupControl2 = new DevExpress.XtraEditors.GroupControl();
			checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
			splitter1 = new System.Windows.Forms.Splitter();
			groupControl3 = new DevExpress.XtraEditors.GroupControl();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			textEdit3 = new DevExpress.XtraEditors.TextEdit();
			textEdit2 = new DevExpress.XtraEditors.TextEdit();
			textEdit1 = new DevExpress.XtraEditors.TextEdit();
			labelControl3 = new DevExpress.XtraEditors.LabelControl();
			labelControl2 = new DevExpress.XtraEditors.LabelControl();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
			groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)radioGroup1.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
			groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)checkedListBoxControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControl3).BeginInit();
			groupControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)textEdit3.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textEdit2.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
			SuspendLayout();
			groupControl1.Controls.Add(radioGroup1);
			groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
			groupControl1.Location = new System.Drawing.Point(0, 0);
			groupControl1.Name = "groupControl1";
			groupControl1.Size = new System.Drawing.Size(200, 491);
			groupControl1.TabIndex = 0;
			groupControl1.Text = "模式选择";
			radioGroup1.Dock = System.Windows.Forms.DockStyle.Fill;
			radioGroup1.Location = new System.Drawing.Point(2, 31);
			radioGroup1.Name = "radioGroup1";
			radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[3]
			{
				new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "管理员密码"),
				new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "编辑模式"),
				new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "查看模式")
			});
			radioGroup1.Size = new System.Drawing.Size(196, 458);
			radioGroup1.TabIndex = 0;
			radioGroup1.SelectedIndexChanged += new System.EventHandler(radioGroup1_SelectedIndexChanged);
			groupControl2.Controls.Add(checkedListBoxControl1);
			groupControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
			groupControl2.Location = new System.Drawing.Point(200, 278);
			groupControl2.Name = "groupControl2";
			groupControl2.Size = new System.Drawing.Size(416, 213);
			groupControl2.TabIndex = 1;
			groupControl2.Text = "设置权限";
			checkedListBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			checkedListBoxControl1.Location = new System.Drawing.Point(2, 31);
			checkedListBoxControl1.Name = "checkedListBoxControl1";
			checkedListBoxControl1.Size = new System.Drawing.Size(412, 180);
			checkedListBoxControl1.TabIndex = 0;
			checkedListBoxControl1.Leave += new System.EventHandler(checkedListBoxControl1_Leave);
			splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			splitter1.Location = new System.Drawing.Point(200, 268);
			splitter1.Name = "splitter1";
			splitter1.Size = new System.Drawing.Size(416, 10);
			splitter1.TabIndex = 8;
			splitter1.TabStop = false;
			groupControl3.Controls.Add(simpleButton1);
			groupControl3.Controls.Add(textEdit3);
			groupControl3.Controls.Add(textEdit2);
			groupControl3.Controls.Add(textEdit1);
			groupControl3.Controls.Add(labelControl3);
			groupControl3.Controls.Add(labelControl2);
			groupControl3.Controls.Add(labelControl1);
			groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControl3.Location = new System.Drawing.Point(200, 0);
			groupControl3.Name = "groupControl3";
			groupControl3.Size = new System.Drawing.Size(416, 268);
			groupControl3.TabIndex = 9;
			groupControl3.Text = "修改密码";
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton1.Location = new System.Drawing.Point(170, 217);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(112, 34);
			simpleButton1.TabIndex = 6;
			simpleButton1.Text = "确认";
			simpleButton1.Click += new System.EventHandler(button1_Click);
			textEdit3.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit3.Location = new System.Drawing.Point(195, 157);
			textEdit3.Name = "textEdit3";
			textEdit3.Properties.PasswordChar = '*';
			textEdit3.Size = new System.Drawing.Size(150, 30);
			textEdit3.TabIndex = 5;
			textEdit2.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit2.Location = new System.Drawing.Point(195, 107);
			textEdit2.Name = "textEdit2";
			textEdit2.Properties.PasswordChar = '*';
			textEdit2.Size = new System.Drawing.Size(150, 30);
			textEdit2.TabIndex = 4;
			textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit1.Location = new System.Drawing.Point(195, 57);
			textEdit1.Name = "textEdit1";
			textEdit1.Properties.PasswordChar = '*';
			textEdit1.Size = new System.Drawing.Size(150, 30);
			textEdit1.TabIndex = 3;
			labelControl3.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl3.Location = new System.Drawing.Point(81, 161);
			labelControl3.Name = "labelControl3";
			labelControl3.Size = new System.Drawing.Size(72, 22);
			labelControl3.TabIndex = 2;
			labelControl3.Text = "密码确认";
			labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl2.Location = new System.Drawing.Point(81, 111);
			labelControl2.Name = "labelControl2";
			labelControl2.Size = new System.Drawing.Size(54, 22);
			labelControl2.TabIndex = 1;
			labelControl2.Text = "新密码";
			labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl1.Location = new System.Drawing.Point(81, 61);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(54, 22);
			labelControl1.TabIndex = 0;
			labelControl1.Text = "旧密码";
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(616, 491);
			base.Controls.Add(groupControl3);
			base.Controls.Add(splitter1);
			base.Controls.Add(groupControl2);
			base.Controls.Add(groupControl1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "PasswordForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "密码管理";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(PasswordForm_FormClosing);
			base.Shown += new System.EventHandler(PasswordForm_Shown);
			((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
			groupControl1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)radioGroup1.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
			groupControl2.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)checkedListBoxControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControl3).EndInit();
			groupControl3.ResumeLayout(performLayout: false);
			groupControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)textEdit3.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textEdit2.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
			ResumeLayout(performLayout: false);
		}
	}
}

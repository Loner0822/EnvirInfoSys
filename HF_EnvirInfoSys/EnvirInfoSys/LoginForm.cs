using DevExpress.XtraEditors;
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
	public class LoginForm : XtraForm
	{
		public int Mode;

		public string unitid = "";

		private string Workpath = AppDomain.CurrentDomain.BaseDirectory;

		private IContainer components = null;

		private SimpleButton simpleButton1;

		private SimpleButton simpleButton2;

		private TextEdit textEdit1;

		private LabelControl labelControl1;

		private LabelControl labelControl2;

		public LoginForm()
		{
			InitializeComponent();
		}

		private void LoginForm_Shown(object sender, EventArgs e)
		{
			textEdit1.Focus();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string a = "";
			string md5_16byte = GetMd5_16byte(textEdit1.Text);
			AccessHelper accessHelper = new AccessHelper(Workpath + "data\\PASSWORD_H0001Z000E00.mdb");
			string sql = "select PWNAME from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWMD5 = '" + md5_16byte + "' and UNITID = '" + unitid + "'";
			DataTable dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
			accessHelper.CloseConn();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				a = dataTable.Rows[i]["PWNAME"].ToString();
				if (a != "管理员密码")
				{
					break;
				}
			}
			if (a == "编辑模式")
			{
				base.DialogResult = DialogResult.OK;
				Mode = 1;
				Close();
			}
			else if (a == "查看模式")
			{
				base.DialogResult = DialogResult.OK;
				Mode = 2;
				Close();
			}
			else
			{
				XtraMessageBox.Show("密码错误!");
				textEdit1.Focus();
				textEdit1.SelectAll();
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			Close();
		}

		public static string GetMd5_16byte(string ConvertString)
		{
			string empty = string.Empty;
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			empty = BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.Default.GetBytes(ConvertString)), 4, 8);
			empty = empty.Replace("-", "");
			return empty.ToLower();
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.LoginForm));
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			textEdit1 = new DevExpress.XtraEditors.TextEdit();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			labelControl2 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
			SuspendLayout();
			simpleButton1.Location = new System.Drawing.Point(96, 142);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(112, 34);
			simpleButton1.TabIndex = 8;
			simpleButton1.Text = "确定";
			simpleButton1.Click += new System.EventHandler(button1_Click);
			simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			simpleButton2.Location = new System.Drawing.Point(236, 142);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(112, 34);
			simpleButton2.TabIndex = 9;
			simpleButton2.Text = "取消";
			simpleButton2.Click += new System.EventHandler(button2_Click);
			textEdit1.Location = new System.Drawing.Point(156, 42);
			textEdit1.Name = "textEdit1";
			textEdit1.Properties.PasswordChar = '*';
			textEdit1.Size = new System.Drawing.Size(192, 30);
			textEdit1.TabIndex = 10;
			labelControl1.Location = new System.Drawing.Point(96, 46);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(54, 22);
			labelControl1.TabIndex = 11;
			labelControl1.Text = "密码：";
			labelControl2.Location = new System.Drawing.Point(75, 95);
			labelControl2.Name = "labelControl2";
			labelControl2.Size = new System.Drawing.Size(298, 22);
			labelControl2.TabIndex = 12;
			labelControl2.Text = "初始密码: 1 - 编辑模式; 2 - 查看模式";
			base.AcceptButton = simpleButton1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = simpleButton2;
			base.ClientSize = new System.Drawing.Size(438, 209);
			base.ControlBox = false;
			base.Controls.Add(labelControl2);
			base.Controls.Add(labelControl1);
			base.Controls.Add(textEdit1);
			base.Controls.Add(simpleButton2);
			base.Controls.Add(simpleButton1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "LoginForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "请登录";
			base.Shown += new System.EventHandler(LoginForm_Shown);
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

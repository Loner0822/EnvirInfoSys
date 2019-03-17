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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(96, 142);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(112, 34);
            this.simpleButton1.TabIndex = 8;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(236, 142);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(112, 34);
            this.simpleButton2.TabIndex = 9;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(156, 42);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.PasswordChar = '*';
            this.textEdit1.Size = new System.Drawing.Size(192, 30);
            this.textEdit1.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(96, 46);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 22);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "密码：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(75, 95);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(298, 22);
            this.labelControl2.TabIndex = 12;
            this.labelControl2.Text = "初始密码: 1 - 编辑模式; 2 - 查看模式";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.simpleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(438, 209);
            this.ControlBox = false;
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "请登录";
            this.Shown += new System.EventHandler(this.LoginForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}

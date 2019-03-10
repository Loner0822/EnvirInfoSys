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
	public class CheckPwForm : XtraForm
	{
		public string unitid = "";

		private string Workpath = AppDomain.CurrentDomain.BaseDirectory;

		private IContainer components = null;

		private LabelControl labelControl1;

		private TextEdit textEdit1;

		private SimpleButton simpleButton2;

		private SimpleButton simpleButton1;

		private LabelControl labelControl2;

		public CheckPwForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string md5_16byte = GetMd5_16byte(textEdit1.Text);
			AccessHelper accessHelper = new AccessHelper(Workpath + "data\\PASSWORD_H0001Z000E00.mdb");
			string sql = "select PGUID from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWNAME = '管理员密码' and PWMD5 = '" + md5_16byte + "' and UNITID = '" + unitid + "'";
			DataTable dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
			accessHelper.CloseConn();
			if (dataTable.Rows.Count > 0)
			{
				base.DialogResult = DialogResult.OK;
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

		private void CheckPwForm_Shown(object sender, EventArgs e)
		{
			textEdit1.Focus();
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.CheckPwForm));
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			textEdit1 = new DevExpress.XtraEditors.TextEdit();
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			labelControl2 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
			SuspendLayout();
			labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl1.Location = new System.Drawing.Point(60, 46);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(54, 22);
			labelControl1.TabIndex = 15;
			labelControl1.Text = "密码：";
			textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit1.Location = new System.Drawing.Point(120, 42);
			textEdit1.Name = "textEdit1";
			textEdit1.Properties.PasswordChar = '*';
			textEdit1.Size = new System.Drawing.Size(192, 30);
			textEdit1.TabIndex = 14;
			simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			simpleButton2.Location = new System.Drawing.Point(200, 134);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(112, 34);
			simpleButton2.TabIndex = 13;
			simpleButton2.Text = "取消";
			simpleButton2.Click += new System.EventHandler(button2_Click);
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton1.Location = new System.Drawing.Point(60, 134);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(112, 34);
			simpleButton1.TabIndex = 12;
			simpleButton1.Text = "确定";
			simpleButton1.Click += new System.EventHandler(button1_Click);
			labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl2.Location = new System.Drawing.Point(107, 93);
			labelControl2.Name = "labelControl2";
			labelControl2.Size = new System.Drawing.Size(154, 22);
			labelControl2.TabIndex = 16;
			labelControl2.Text = "初始管理员密码：0";
			base.AcceptButton = simpleButton1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = simpleButton2;
			base.ClientSize = new System.Drawing.Size(373, 198);
			base.ControlBox = false;
			base.Controls.Add(labelControl2);
			base.Controls.Add(labelControl1);
			base.Controls.Add(textEdit1);
			base.Controls.Add(simpleButton2);
			base.Controls.Add(simpleButton1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "CheckPwForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "请输入管理员密码";
			base.Shown += new System.EventHandler(CheckPwForm_Shown);
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

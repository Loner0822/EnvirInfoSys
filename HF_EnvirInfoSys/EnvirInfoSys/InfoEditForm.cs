using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class InfoEditForm : XtraForm
	{
		public string EditText = "";

		private IContainer components = null;

		private SimpleButton simpleButton2;

		private SimpleButton simpleButton1;

		private LabelControl labelControl1;

		private TextEdit textEdit1;

		public InfoEditForm()
		{
			InitializeComponent();
		}

		private void InfoEditForm_Shown(object sender, EventArgs e)
		{
			textEdit1.Text = EditText;
			textEdit1.Focus();
			textEdit1.SelectAll();
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			EditText = textEdit1.Text;
			if (EditText == "")
			{
				XtraMessageBox.Show("名称不可为空!");
				textEdit1.Focus();
			}
			else
			{
				base.DialogResult = DialogResult.OK;
				Close();
			}
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.InfoEditForm));
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			textEdit1 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
			SuspendLayout();
			simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			simpleButton2.Location = new System.Drawing.Point(231, 121);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(112, 34);
			simpleButton2.TabIndex = 7;
			simpleButton2.Text = "取消";
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton1.Location = new System.Drawing.Point(92, 121);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(112, 34);
			simpleButton1.TabIndex = 6;
			simpleButton1.Text = "确认";
			simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
			labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			labelControl1.Location = new System.Drawing.Point(76, 53);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(90, 22);
			labelControl1.TabIndex = 5;
			labelControl1.Text = "菜单名称：";
			textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit1.Location = new System.Drawing.Point(187, 49);
			textEdit1.Name = "textEdit1";
			textEdit1.Size = new System.Drawing.Size(182, 30);
			textEdit1.TabIndex = 4;
			base.AcceptButton = simpleButton1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = simpleButton2;
			base.ClientSize = new System.Drawing.Size(446, 205);
			base.ControlBox = false;
			base.Controls.Add(simpleButton2);
			base.Controls.Add(simpleButton1);
			base.Controls.Add(labelControl1);
			base.Controls.Add(textEdit1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "InfoEditForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "菜单命名";
			base.Shown += new System.EventHandler(InfoEditForm_Shown);
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

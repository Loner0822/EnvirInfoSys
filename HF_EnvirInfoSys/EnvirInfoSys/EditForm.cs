using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class EditForm : XtraForm
	{
		public string EditText = "";

		private IContainer components = null;

		private TextEdit textEdit1;

		private LabelControl labelControl1;

		private SimpleButton simpleButton1;

		private SimpleButton simpleButton2;

		public EditForm()
		{
			InitializeComponent();
		}

		private void EditForm_Shown(object sender, EventArgs e)
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
			textEdit1 = new DevExpress.XtraEditors.TextEdit();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
			SuspendLayout();
			textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			textEdit1.Location = new System.Drawing.Point(204, 38);
			textEdit1.Name = "textEdit1";
			textEdit1.Size = new System.Drawing.Size(150, 30);
			textEdit1.TabIndex = 0;
			labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			labelControl1.Location = new System.Drawing.Point(62, 42);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(126, 22);
			labelControl1.TabIndex = 1;
			labelControl1.Text = "管辖类型名称：";
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton1.Location = new System.Drawing.Point(77, 110);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(112, 34);
			simpleButton1.TabIndex = 2;
			simpleButton1.Text = "确认";
			simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
			simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			simpleButton2.Location = new System.Drawing.Point(216, 110);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(112, 34);
			simpleButton2.TabIndex = 3;
			simpleButton2.Text = "取消";
			base.AcceptButton = simpleButton1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = simpleButton2;
			base.ClientSize = new System.Drawing.Size(416, 182);
			base.ControlBox = false;
			base.Controls.Add(simpleButton2);
			base.Controls.Add(simpleButton1);
			base.Controls.Add(labelControl1);
			base.Controls.Add(textEdit1);
			base.Name = "EditForm";
			Text = "请输入管辖类型名称";
			base.Shown += new System.EventHandler(EditForm_Shown);
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

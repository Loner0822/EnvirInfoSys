using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class HelpForm : XtraForm
	{
		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private IContainer components = null;

		private WebBrowser webBrowser1;

		public HelpForm()
		{
			InitializeComponent();
		}

		private void HelpForm_Shown(object sender, EventArgs e)
		{
			webBrowser1.Url = new Uri(WorkPath + "help.mht");
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.HelpForm));
			webBrowser1 = new System.Windows.Forms.WebBrowser();
			SuspendLayout();
			webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			webBrowser1.Location = new System.Drawing.Point(0, 0);
			webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.Size = new System.Drawing.Size(1007, 529);
			webBrowser1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1007, 529);
			base.Controls.Add(webBrowser1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "HelpForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "帮助文档";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Shown += new System.EventHandler(HelpForm_Shown);
			ResumeLayout(performLayout: false);
		}
	}
}

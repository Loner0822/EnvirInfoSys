using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class InfoPicForm : XtraForm
	{
		public string picpath = "";

		private IContainer components = null;

		private PictureEdit pictureEdit1;

		public InfoPicForm()
		{
			InitializeComponent();
		}

		private void InfoPicForm_Shown(object sender, EventArgs e)
		{
			FileStream stream = new FileStream(picpath, FileMode.Open, FileAccess.Read);
			pictureEdit1.Image = Image.FromStream(stream);
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.InfoPicForm));
			pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
			SuspendLayout();
			pictureEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
			pictureEdit1.Location = new System.Drawing.Point(0, 0);
			pictureEdit1.Name = "pictureEdit1";
			pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
			pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
			pictureEdit1.Size = new System.Drawing.Size(1224, 623);
			pictureEdit1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1224, 623);
			base.Controls.Add(pictureEdit1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MinimizeBox = false;
			base.Name = "InfoPicForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Shown += new System.EventHandler(InfoPicForm_Shown);
			((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
			ResumeLayout(performLayout: false);
		}
	}
}

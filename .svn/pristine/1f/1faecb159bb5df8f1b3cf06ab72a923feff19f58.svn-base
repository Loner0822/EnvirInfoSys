using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class ucPictureBox : XtraUserControl
	{
		public delegate void ClickHandle(object sender, EventArgs e, string iconguid);

		public delegate void DoubleClickHandle(object sender, EventArgs e, string iconguid);

		private string _iconname = string.Empty;

		private string _iconpguid = string.Empty;

		private string _iconpath = string.Empty;

		private bool _iconcheck = false;

		private IContainer components = null;

		private Panel panel1;

		private PictureBox pictureBox1;

		private ToolTip toolTip1;

		private PanelControl panelControl1;

		private LabelControl label1;

		public string IconName
		{
			get
			{
				return _iconname;
			}
			set
			{
				_iconname = value;
				toolTip1.SetToolTip(label1, _iconname);
				label1.Text = _iconname;
			}
		}

		public string IconPguid
		{
			get
			{
				return _iconpguid;
			}
			set
			{
				_iconpguid = value;
			}
		}

		public string IconPath
		{
			get
			{
				return _iconpath;
			}
			set
			{
				_iconpath = value;
				pictureBox1.Load(_iconpath);
			}
		}

		public bool IconCheck
		{
			get
			{
				return _iconcheck;
			}
			set
			{
				_iconcheck = value;
				if (_iconcheck)
				{
					pictureBox1.BorderStyle = BorderStyle.Fixed3D;
				}
				else
				{
					pictureBox1.BorderStyle = BorderStyle.None;
				}
			}
		}

		public event ClickHandle Single_Click;

		public event DoubleClickHandle Double_Click;

		public ucPictureBox()
		{
			InitializeComponent();
		}

		private void PB_Click(object sender, EventArgs e)
		{
			MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
			if (this.Single_Click != null && mouseEventArgs.Button == MouseButtons.Left)
			{
				this.Single_Click(this, new EventArgs(), _iconpguid);
			}
		}

		private void PB_DoubleClick(object sender, EventArgs e)
		{
			MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
			if (this.Double_Click != null && mouseEventArgs.Button == MouseButtons.Left)
			{
				this.Double_Click(this, new EventArgs(), _iconpguid);
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
			components = new System.ComponentModel.Container();
			panel1 = new System.Windows.Forms.Panel();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			panelControl1 = new DevExpress.XtraEditors.PanelControl();
			label1 = new DevExpress.XtraEditors.LabelControl();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			SuspendLayout();
			panel1.Controls.Add(pictureBox1);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(99, 55);
			panel1.TabIndex = 1;
			pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
			pictureBox1.Location = new System.Drawing.Point(22, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(55, 55);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			pictureBox1.Click += new System.EventHandler(PB_Click);
			pictureBox1.DoubleClick += new System.EventHandler(PB_DoubleClick);
			panelControl1.Appearance.BackColor = System.Drawing.Color.Transparent;
			panelControl1.Appearance.Options.UseBackColor = true;
			panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			panelControl1.Controls.Add(label1);
			panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			panelControl1.Location = new System.Drawing.Point(0, 55);
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new System.Drawing.Size(99, 32);
			panelControl1.TabIndex = 2;
			label1.Appearance.BackColor = System.Drawing.Color.Transparent;
			label1.Appearance.Options.UseBackColor = true;
			label1.Appearance.Options.UseTextOptions = true;
			label1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			label1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			label1.Dock = System.Windows.Forms.DockStyle.Fill;
			label1.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
			label1.Location = new System.Drawing.Point(0, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(99, 32);
			label1.TabIndex = 0;
			label1.Text = "label";
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(panelControl1);
			base.Controls.Add(panel1);
			base.Name = "ucPictureBox";
			base.Size = new System.Drawing.Size(99, 87);
			panel1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(performLayout: false);
			ResumeLayout(performLayout: false);
		}
	}
}

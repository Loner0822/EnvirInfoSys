using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PublishSys
{
	public class ucPictureBox : UserControl
	{
		public delegate void ClickHandle(object sender, EventArgs e, string iconguid);

		public delegate void DoubleClickHandle(object sender, EventArgs e, string iconguid);

		private string _iconname = string.Empty;

		private string _iconpguid = string.Empty;

		private string _iconpath = string.Empty;

		private bool _iconcheck = false;

		private IContainer components = null;

		private ToolTip toolTip1;

		private Panel panel1;

		private PictureBox pictureBox1;

		private Label label1;

		private BackgroundWorker backgroundWorker1;

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
			toolTip1 = new System.Windows.Forms.ToolTip(components);
			panel1 = new System.Windows.Forms.Panel();
			pictureBox1 = new System.Windows.Forms.PictureBox();
			label1 = new System.Windows.Forms.Label();
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
			SuspendLayout();
			panel1.Controls.Add(pictureBox1);
			panel1.Dock = System.Windows.Forms.DockStyle.Top;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(99, 55);
			panel1.TabIndex = 0;
			panel1.Click += new System.EventHandler(PB_Click);
			panel1.DoubleClick += new System.EventHandler(PB_DoubleClick);
			pictureBox1.Location = new System.Drawing.Point(22, 0);
			pictureBox1.Name = "pictureBox1";
			pictureBox1.Size = new System.Drawing.Size(55, 55);
			pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			pictureBox1.TabIndex = 1;
			pictureBox1.TabStop = false;
			pictureBox1.Click += new System.EventHandler(PB_Click);
			pictureBox1.DoubleClick += new System.EventHandler(PB_DoubleClick);
			label1.Dock = System.Windows.Forms.DockStyle.Fill;
			label1.Location = new System.Drawing.Point(0, 55);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(99, 22);
			label1.TabIndex = 1;
			label1.Text = "label1";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			label1.Click += new System.EventHandler(PB_Click);
			label1.DoubleClick += new System.EventHandler(PB_DoubleClick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			BackColor = System.Drawing.SystemColors.Control;
			base.Controls.Add(label1);
			base.Controls.Add(panel1);
			base.Name = "ucPictureBox";
			base.Size = new System.Drawing.Size(99, 77);
			base.Click += new System.EventHandler(PB_Click);
			base.DoubleClick += new System.EventHandler(PB_DoubleClick);
			panel1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
			ResumeLayout(performLayout: false);
		}
	}
}

using DevExpress.XtraWaitForm;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class WaitForm1 : WaitForm
	{
		public enum WaitFormCommand
		{

		}

		private IContainer components = null;

		private ProgressPanel progressPanel1;

		private TableLayoutPanel tableLayoutPanel1;

		public WaitForm1()
		{
			InitializeComponent();
			progressPanel1.AutoHeight = true;
		}

		public override void SetCaption(string caption)
		{
			base.SetCaption(caption);
			progressPanel1.Caption = caption;
		}

		public override void SetDescription(string description)
		{
			base.SetDescription(description);
			progressPanel1.Description = description;
		}

		public override void ProcessCommand(Enum cmd, object arg)
		{
			base.ProcessCommand(cmd, arg);
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
			progressPanel1 = new DevExpress.XtraWaitForm.ProgressPanel();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			tableLayoutPanel1.SuspendLayout();
			SuspendLayout();
			progressPanel1.Appearance.BackColor = System.Drawing.Color.Transparent;
			progressPanel1.Appearance.Options.UseBackColor = true;
			progressPanel1.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f);
			progressPanel1.AppearanceCaption.Options.UseFont = true;
			progressPanel1.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
			progressPanel1.AppearanceDescription.Options.UseFont = true;
			progressPanel1.BarAnimationElementThickness = 2;
			progressPanel1.Caption = "请稍后";
			progressPanel1.Description = "加载中...";
			progressPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			progressPanel1.FrameInterval = 3000;
			progressPanel1.ImageHorzOffset = 20;
			progressPanel1.Location = new System.Drawing.Point(0, 23);
			progressPanel1.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
			progressPanel1.Name = "progressPanel1";
			progressPanel1.Size = new System.Drawing.Size(369, 55);
			progressPanel1.TabIndex = 0;
			progressPanel1.Text = "progressPanel1";
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
			tableLayoutPanel1.ColumnCount = 1;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel1.Controls.Add(progressPanel1, 0, 0);
			tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 19, 0, 19);
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
			tableLayoutPanel1.Size = new System.Drawing.Size(369, 101);
			tableLayoutPanel1.TabIndex = 1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(9f, 18f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(369, 101);
			base.Controls.Add(tableLayoutPanel1);
			DoubleBuffered = true;
			base.Margin = new System.Windows.Forms.Padding(4);
			base.Name = "WaitForm1";
			base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			Text = "Form1";
			tableLayoutPanel1.ResumeLayout(performLayout: false);
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

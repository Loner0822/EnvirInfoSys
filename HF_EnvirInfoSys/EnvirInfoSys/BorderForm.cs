using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class BorderForm : XtraForm
	{
		public ToPointData borData = new ToPointData();

		public bool IsPoint = false;

		public bool IsLine = false;

		private IContainer components = null;

		private LabelControl labelControl7;

		private LabelControl labelControl6;

		private LabelControl labelControl5;

		private LabelControl labelControl4;

		private LabelControl labelControl3;

		private LabelControl labelControl2;

		private LabelControl labelControl1;

		private SimpleButton simpleButton3;

		private SimpleButton simpleButton2;

		private SimpleButton simpleButton1;

		private TextEdit textEdit4;

		private TextEdit textEdit3;

		private TextEdit textEdit2;

		private TextEdit textEdit1;

		private RadioButton radioButton2;

		private RadioButton radioButton1;

		private TrackBar trackBar1;

		private ColorDialog colorDialog1;

		public BorderForm()
		{
			InitializeComponent();
		}

		private void BorderForm_Shown(object sender, EventArgs e)
		{
			if (!IsPoint)
			{
				textEdit2.Visible = false;
				textEdit3.Visible = false;
				labelControl5.Visible = false;
				labelControl6.Visible = false;
			}
			else if (!IsLine)
			{
				textEdit2.Text = borData.lng.ToString();
				textEdit3.Text = borData.lat.ToString();
			}
			else
			{
				textEdit2.Text = borData.lng.ToString();
				textEdit3.Text = borData.lat.ToString();
				textEdit2.Enabled = false;
				textEdit3.Enabled = false;
			}
			if (borData.line_data.Type == "实线")
			{
				radioButton1.Checked = true;
			}
			else
			{
				radioButton2.Checked = true;
			}
			textEdit1.Text = borData.line_data.Width.ToString();
			textEdit4.BackColor = ColorTranslator.FromHtml(borData.line_data.Color);
			labelControl7.Text = borData.line_data.Opacity.ToString("f");
			trackBar1.Value = (int)(borData.line_data.Opacity * 20.0);
		}

		private void radioButton1_CheckedChanged(object sender, EventArgs e)
		{
			if (radioButton1.Checked)
			{
				borData.line_data.Type = "实线";
			}
			else
			{
				borData.line_data.Type = "虚线";
			}
		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{
			if (!radioButton2.Checked)
			{
				borData.line_data.Type = "实线";
			}
			else
			{
				borData.line_data.Type = "虚线";
			}
		}

		private void textBox2_Leave(object sender, EventArgs e)
		{
			if (textEdit1.Text != "")
			{
				borData.line_data.Width = int.Parse(textEdit1.Text);
			}
			else
			{
				borData.line_data.Width = 0;
			}
		}

		private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar != '\b' && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				textEdit4.BackColor = colorDialog1.Color;
				borData.line_data.Color = ColorTranslator.ToHtml(colorDialog1.Color);
			}
		}

		private void trackBar1_ValueChanged(object sender, EventArgs e)
		{
			labelControl7.Text = ((double)trackBar1.Value / 20.0).ToString("f");
			borData.line_data.Opacity = (double)trackBar1.Value / 20.0;
		}

		private void textBox3_Leave(object sender, EventArgs e)
		{
			if (textEdit2.Text != "")
			{
				borData.lng = double.Parse(textEdit2.Text);
			}
			else
			{
				borData.lng = 0.0;
			}
		}

		private void textBox4_Leave(object sender, EventArgs e)
		{
			if (textEdit3.Text != "")
			{
				borData.lat = double.Parse(textEdit3.Text);
			}
			else
			{
				borData.lat = 0.0;
			}
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			Close();
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.BorderForm));
			labelControl7 = new DevExpress.XtraEditors.LabelControl();
			labelControl6 = new DevExpress.XtraEditors.LabelControl();
			labelControl5 = new DevExpress.XtraEditors.LabelControl();
			labelControl4 = new DevExpress.XtraEditors.LabelControl();
			labelControl3 = new DevExpress.XtraEditors.LabelControl();
			labelControl2 = new DevExpress.XtraEditors.LabelControl();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			textEdit4 = new DevExpress.XtraEditors.TextEdit();
			textEdit3 = new DevExpress.XtraEditors.TextEdit();
			textEdit2 = new DevExpress.XtraEditors.TextEdit();
			textEdit1 = new DevExpress.XtraEditors.TextEdit();
			radioButton2 = new System.Windows.Forms.RadioButton();
			radioButton1 = new System.Windows.Forms.RadioButton();
			trackBar1 = new System.Windows.Forms.TrackBar();
			colorDialog1 = new System.Windows.Forms.ColorDialog();
			((System.ComponentModel.ISupportInitialize)textEdit4.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textEdit3.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textEdit2.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
			SuspendLayout();
			labelControl7.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl7.Location = new System.Drawing.Point(307, 211);
			labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			labelControl7.Name = "labelControl7";
			labelControl7.Size = new System.Drawing.Size(10, 22);
			labelControl7.TabIndex = 49;
			labelControl7.Text = "0";
			labelControl6.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl6.Location = new System.Drawing.Point(50, 324);
			labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			labelControl6.Name = "labelControl6";
			labelControl6.Size = new System.Drawing.Size(72, 22);
			labelControl6.TabIndex = 48;
			labelControl6.Text = "指向纬度";
			labelControl5.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl5.Location = new System.Drawing.Point(50, 270);
			labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			labelControl5.Name = "labelControl5";
			labelControl5.Size = new System.Drawing.Size(72, 22);
			labelControl5.TabIndex = 47;
			labelControl5.Text = "指向经度";
			labelControl4.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl4.Location = new System.Drawing.Point(50, 211);
			labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			labelControl4.Name = "labelControl4";
			labelControl4.Size = new System.Drawing.Size(90, 22);
			labelControl4.TabIndex = 46;
			labelControl4.Text = "线条透明度";
			labelControl3.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl3.Location = new System.Drawing.Point(50, 154);
			labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			labelControl3.Name = "labelControl3";
			labelControl3.Size = new System.Drawing.Size(72, 22);
			labelControl3.TabIndex = 45;
			labelControl3.Text = "线条颜色";
			labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl2.Location = new System.Drawing.Point(50, 104);
			labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			labelControl2.Name = "labelControl2";
			labelControl2.Size = new System.Drawing.Size(72, 22);
			labelControl2.TabIndex = 44;
			labelControl2.Text = "线条宽度";
			labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl1.Location = new System.Drawing.Point(50, 46);
			labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(72, 22);
			labelControl1.TabIndex = 43;
			labelControl1.Text = "线条类型";
			simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			simpleButton3.Location = new System.Drawing.Point(219, 378);
			simpleButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			simpleButton3.Name = "simpleButton3";
			simpleButton3.Size = new System.Drawing.Size(111, 42);
			simpleButton3.TabIndex = 42;
			simpleButton3.Text = "取消";
			simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton2.Location = new System.Drawing.Point(73, 378);
			simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(111, 42);
			simpleButton2.TabIndex = 41;
			simpleButton2.Text = "确认";
			simpleButton2.Click += new System.EventHandler(simpleButton2_Click);
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton1.Location = new System.Drawing.Point(281, 145);
			simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(96, 42);
			simpleButton1.TabIndex = 40;
			simpleButton1.Text = "颜色选择";
			simpleButton1.Click += new System.EventHandler(button1_Click);
			textEdit4.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit4.Enabled = false;
			textEdit4.Location = new System.Drawing.Point(163, 149);
			textEdit4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textEdit4.Name = "textEdit4";
			textEdit4.Properties.Appearance.BackColor = System.Drawing.SystemColors.ActiveBorder;
			textEdit4.Properties.Appearance.Options.UseBackColor = true;
			textEdit4.Size = new System.Drawing.Size(111, 30);
			textEdit4.TabIndex = 39;
			textEdit3.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit3.Location = new System.Drawing.Point(163, 319);
			textEdit3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textEdit3.Name = "textEdit3";
			textEdit3.Size = new System.Drawing.Size(167, 30);
			textEdit3.TabIndex = 38;
			textEdit3.Leave += new System.EventHandler(textBox4_Leave);
			textEdit2.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit2.Location = new System.Drawing.Point(163, 265);
			textEdit2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textEdit2.Name = "textEdit2";
			textEdit2.Size = new System.Drawing.Size(167, 30);
			textEdit2.TabIndex = 37;
			textEdit2.Leave += new System.EventHandler(textBox3_Leave);
			textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit1.Location = new System.Drawing.Point(163, 99);
			textEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			textEdit1.Name = "textEdit1";
			textEdit1.Size = new System.Drawing.Size(167, 30);
			textEdit1.TabIndex = 36;
			textEdit1.EditValueChanged += new System.EventHandler(textBox2_Leave);
			textEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox2_KeyPress);
			radioButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
			radioButton2.AutoSize = true;
			radioButton2.Location = new System.Drawing.Point(247, 48);
			radioButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			radioButton2.Name = "radioButton2";
			radioButton2.Size = new System.Drawing.Size(71, 26);
			radioButton2.TabIndex = 35;
			radioButton2.TabStop = true;
			radioButton2.Text = "虚线";
			radioButton2.UseVisualStyleBackColor = true;
			radioButton2.CheckedChanged += new System.EventHandler(radioButton2_CheckedChanged);
			radioButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			radioButton1.AutoSize = true;
			radioButton1.Location = new System.Drawing.Point(163, 48);
			radioButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			radioButton1.Name = "radioButton1";
			radioButton1.Size = new System.Drawing.Size(71, 26);
			radioButton1.TabIndex = 34;
			radioButton1.TabStop = true;
			radioButton1.Text = "实线";
			radioButton1.UseVisualStyleBackColor = true;
			radioButton1.CheckedChanged += new System.EventHandler(radioButton1_CheckedChanged);
			trackBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
			trackBar1.Location = new System.Drawing.Point(162, 207);
			trackBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			trackBar1.Maximum = 20;
			trackBar1.Name = "trackBar1";
			trackBar1.Size = new System.Drawing.Size(138, 69);
			trackBar1.TabIndex = 33;
			trackBar1.ValueChanged += new System.EventHandler(trackBar1_ValueChanged);
			base.AcceptButton = simpleButton2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = simpleButton3;
			base.ClientSize = new System.Drawing.Size(401, 464);
			base.Controls.Add(labelControl7);
			base.Controls.Add(labelControl6);
			base.Controls.Add(labelControl5);
			base.Controls.Add(labelControl4);
			base.Controls.Add(labelControl3);
			base.Controls.Add(labelControl2);
			base.Controls.Add(labelControl1);
			base.Controls.Add(simpleButton3);
			base.Controls.Add(simpleButton2);
			base.Controls.Add(simpleButton1);
			base.Controls.Add(textEdit4);
			base.Controls.Add(textEdit3);
			base.Controls.Add(textEdit2);
			base.Controls.Add(textEdit1);
			base.Controls.Add(radioButton2);
			base.Controls.Add(radioButton1);
			base.Controls.Add(trackBar1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "BorderForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "线条设置";
			base.Shown += new System.EventHandler(BorderForm_Shown);
			((System.ComponentModel.ISupportInitialize)textEdit4.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textEdit3.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textEdit2.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textEdit1.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

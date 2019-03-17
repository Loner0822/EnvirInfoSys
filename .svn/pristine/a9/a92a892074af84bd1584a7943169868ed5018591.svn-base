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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BorderForm));
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit4 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl7
            // 
            this.labelControl7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl7.Location = new System.Drawing.Point(307, 211);
            this.labelControl7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(10, 22);
            this.labelControl7.TabIndex = 49;
            this.labelControl7.Text = "0";
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl6.Location = new System.Drawing.Point(50, 324);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 22);
            this.labelControl6.TabIndex = 48;
            this.labelControl6.Text = "指向纬度";
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl5.Location = new System.Drawing.Point(50, 270);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 22);
            this.labelControl5.TabIndex = 47;
            this.labelControl5.Text = "指向经度";
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl4.Location = new System.Drawing.Point(50, 211);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(90, 22);
            this.labelControl4.TabIndex = 46;
            this.labelControl4.Text = "线条透明度";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl3.Location = new System.Drawing.Point(50, 154);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 22);
            this.labelControl3.TabIndex = 45;
            this.labelControl3.Text = "线条颜色";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl2.Location = new System.Drawing.Point(50, 104);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 22);
            this.labelControl2.TabIndex = 44;
            this.labelControl2.Text = "线条宽度";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl1.Location = new System.Drawing.Point(50, 46);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 22);
            this.labelControl1.TabIndex = 43;
            this.labelControl1.Text = "线条类型";
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton3.Location = new System.Drawing.Point(219, 378);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(111, 42);
            this.simpleButton3.TabIndex = 42;
            this.simpleButton3.Text = "取消";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton2.Location = new System.Drawing.Point(73, 378);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(111, 42);
            this.simpleButton2.TabIndex = 41;
            this.simpleButton2.Text = "确认";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Location = new System.Drawing.Point(281, 145);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(96, 42);
            this.simpleButton1.TabIndex = 40;
            this.simpleButton1.Text = "颜色选择";
            this.simpleButton1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textEdit4
            // 
            this.textEdit4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit4.Enabled = false;
            this.textEdit4.Location = new System.Drawing.Point(163, 149);
            this.textEdit4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEdit4.Name = "textEdit4";
            this.textEdit4.Properties.Appearance.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.textEdit4.Properties.Appearance.Options.UseBackColor = true;
            this.textEdit4.Size = new System.Drawing.Size(111, 30);
            this.textEdit4.TabIndex = 39;
            // 
            // textEdit3
            // 
            this.textEdit3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit3.Location = new System.Drawing.Point(163, 319);
            this.textEdit3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Size = new System.Drawing.Size(167, 30);
            this.textEdit3.TabIndex = 38;
            this.textEdit3.Leave += new System.EventHandler(this.textBox4_Leave);
            // 
            // textEdit2
            // 
            this.textEdit2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit2.Location = new System.Drawing.Point(163, 265);
            this.textEdit2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(167, 30);
            this.textEdit2.TabIndex = 37;
            this.textEdit2.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit1.Location = new System.Drawing.Point(163, 99);
            this.textEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(167, 30);
            this.textEdit1.TabIndex = 36;
            this.textEdit1.EditValueChanged += new System.EventHandler(this.textBox2_Leave);
            this.textEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox2_KeyPress);
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(247, 48);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(71, 26);
            this.radioButton2.TabIndex = 35;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "虚线";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(163, 48);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(71, 26);
            this.radioButton1.TabIndex = 34;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "实线";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.trackBar1.Location = new System.Drawing.Point(162, 207);
            this.trackBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(138, 69);
            this.trackBar1.TabIndex = 33;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // BorderForm
            // 
            this.AcceptButton = this.simpleButton2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButton3;
            this.ClientSize = new System.Drawing.Size(401, 464);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.textEdit4);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.trackBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BorderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "线条设置";
            this.Shown += new System.EventHandler(this.BorderForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit4.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}

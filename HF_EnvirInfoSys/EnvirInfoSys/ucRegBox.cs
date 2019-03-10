using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class ucRegBox : XtraUserControl
	{
		public delegate void SelectedIndexChange(object sender, EventArgs e, string pguid, int unitlevel);

		private Dictionary<string, string> show_nodes;

		private int level = -1;

		public string unitguid = "";

		public string unitname = "";

		private IContainer components = null;

		private LabelControl labelControl1;

		private ComboBoxEdit comboBoxEdit1;

		public event SelectedIndexChange SelectedChange;

		public ucRegBox()
		{
			InitializeComponent();
		}

		private void ucRegBox_Load(object sender, EventArgs e)
		{
		}

		public void ucRegBox_Text(string text)
		{
			labelControl1.Text = text;
		}

		public void ucRegBox_Refresh(Dictionary<string, string> nodes, int unitlevel)
		{
			comboBoxEdit1.Enabled = true;
			show_nodes = nodes;
			level = unitlevel;
			comboBoxEdit1.Properties.Items.Clear();
			comboBoxEdit1.Properties.Items.Add("请选择");
			foreach (KeyValuePair<string, string> node in nodes)
			{
				comboBoxEdit1.Properties.Items.Add(node.Value);
			}
			comboBoxEdit1.SelectedIndex = 0;
		}

		public void ucRegBox_SelectAndLock(string pguid, bool unLock)
		{
			int num = 0;
			while (true)
			{
				if (num < comboBoxEdit1.Properties.Items.Count)
				{
					if (comboBoxEdit1.Properties.Items[num].ToString() == show_nodes[pguid])
					{
						break;
					}
					num++;
					continue;
				}
				return;
			}
			comboBoxEdit1.SelectedIndex = num;
			unitguid = pguid;
			unitname = show_nodes[pguid];
			comboBoxEdit1.Enabled = unLock;
		}

		private void Selected_Change(object sender, EventArgs e)
		{
			if (this.SelectedChange != null && level != -1)
			{
				string text = comboBoxEdit1.SelectedItem.ToString();
				string pguid = "";
				if (text == "请选择")
				{
					unitname = "";
					unitguid = "";
				}
				else
				{
					foreach (KeyValuePair<string, string> show_node in show_nodes)
					{
						if (show_node.Value == text)
						{
							pguid = (unitguid = show_node.Key);
							unitname = text;
							break;
						}
					}
				}
				this.SelectedChange(this, new EventArgs(), pguid, level);
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
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
			SuspendLayout();
			labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl1.Appearance.Options.UseTextOptions = true;
			labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			labelControl1.Location = new System.Drawing.Point(3, 17);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(63, 22);
			labelControl1.TabIndex = 0;
			labelControl1.Text = "tmp";
			comboBoxEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			comboBoxEdit1.Location = new System.Drawing.Point(73, 13);
			comboBoxEdit1.Name = "comboBoxEdit1";
			comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxEdit1.Size = new System.Drawing.Size(115, 30);
			comboBoxEdit1.TabIndex = 1;
			comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(Selected_Change);
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(comboBoxEdit1);
			base.Controls.Add(labelControl1);
			base.Name = "ucRegBox";
			base.Size = new System.Drawing.Size(200, 55);
			base.Load += new System.EventHandler(ucRegBox_Load);
			((System.ComponentModel.ISupportInitialize)comboBoxEdit1.Properties).EndInit();
			ResumeLayout(performLayout: false);
		}
	}
}

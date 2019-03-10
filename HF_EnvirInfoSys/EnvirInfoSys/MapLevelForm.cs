using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class MapLevelForm : XtraForm
	{
		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private string[] folds = null;

		public string unitid = "";

		public string nodeid = "";

		public string MapPath = "";

		public string mapstring = "";

		private IContainer components = null;

		private PanelControl panelControl1;

		private SimpleButton simpleButton1;

		private CheckedListBoxControl checkedListBoxControl1;

		public MapLevelForm()
		{
			InitializeComponent();
		}

		private void MapLevelForm_Load(object sender, EventArgs e)
		{
			checkedListBoxControl1.Items.Clear();
			string path = MapPath + "\\roadmap";
			folds = Directory.GetDirectories(path);
			for (int i = 0; i < folds.Length; i++)
			{
				int num = folds[i].LastIndexOf("\\");
				folds[i] = folds[i].Substring(num + 1);
				checkedListBoxControl1.Items.Add(folds[i]);
			}
			string sql = "select MAPLEVEL from ENVIRMAPDY_H0001Z000E00 where ISDELETE = 0 and PGUID = '" + nodeid + "' and UNITID = '" + unitid + "'";
			DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			string[] array;
			if (dataTable.Rows.Count > 0)
			{
				string text = dataTable.Rows[0]["MAPLEVEL"].ToString();
				array = text.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					for (int j = 0; j < checkedListBoxControl1.Items.Count; j++)
					{
						if (checkedListBoxControl1.Items[j].Value.ToString() == array[i])
						{
							checkedListBoxControl1.SetItemChecked(j, value: true);
						}
					}
				}
				return;
			}
			array = mapstring.Split(',');
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < checkedListBoxControl1.Items.Count; j++)
				{
					if (checkedListBoxControl1.Items[j].Value.ToString() == array[i])
					{
						checkedListBoxControl1.SetItemChecked(j, value: true);
					}
				}
			}
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			string text = "";
			List<string> list = new List<string>();
			for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
			{
				if (checkedListBoxControl1.GetItemChecked(i))
				{
					string item = checkedListBoxControl1.GetItemValue(i).ToString();
					list.Add(item);
				}
			}
			text = string.Join(",", list.ToArray());
			if (list.Count <= 0)
			{
				if (XtraMessageBox.Show("未选中任何级别!是否要重新对应?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					return;
				}
				Close();
			}
			string sql = "select PGUID from ENVIRMAPDY_H0001Z000E00 where ISDELETE = 0 and PGUID = '" + nodeid + "' and UNITID = '" + unitid + "'";
			DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				sql = "update ENVIRMAPDY_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', MAPLEVEL = '" + text + "' where ISDELETE = 0 and PGUID = '" + nodeid + "'";
				FileReader.often_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
			else
			{
				sql = "insert into ENVIRMAPDY_H0001Z000E00 (PGUID, S_UDTIME, UNITID, MAPLEVEL) values('" + nodeid + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + unitid + "', '" + text + "')";
				FileReader.often_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.MapLevelForm));
			panelControl1 = new DevExpress.XtraEditors.PanelControl();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
			((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)checkedListBoxControl1).BeginInit();
			SuspendLayout();
			panelControl1.Controls.Add(simpleButton1);
			panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelControl1.Location = new System.Drawing.Point(0, 199);
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new System.Drawing.Size(219, 45);
			panelControl1.TabIndex = 1;
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton1.Location = new System.Drawing.Point(54, 5);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(112, 34);
			simpleButton1.TabIndex = 0;
			simpleButton1.Text = "确认";
			simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
			checkedListBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			checkedListBoxControl1.Location = new System.Drawing.Point(0, 0);
			checkedListBoxControl1.Name = "checkedListBoxControl1";
			checkedListBoxControl1.Size = new System.Drawing.Size(219, 199);
			checkedListBoxControl1.TabIndex = 2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(219, 244);
			base.Controls.Add(checkedListBoxControl1);
			base.Controls.Add(panelControl1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MapLevelForm";
			Text = "地图级别选择";
			base.Load += new System.EventHandler(MapLevelForm_Load);
			((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)checkedListBoxControl1).EndInit();
			ResumeLayout(performLayout: false);
		}
	}
}

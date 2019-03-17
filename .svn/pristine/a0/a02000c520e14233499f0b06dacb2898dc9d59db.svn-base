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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapLevelForm));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.checkedListBoxControl1 = new DevExpress.XtraEditors.CheckedListBoxControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 199);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(219, 45);
            this.panelControl1.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Location = new System.Drawing.Point(54, 5);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(112, 34);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "确认";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // checkedListBoxControl1
            // 
            this.checkedListBoxControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBoxControl1.Location = new System.Drawing.Point(0, 0);
            this.checkedListBoxControl1.Name = "checkedListBoxControl1";
            this.checkedListBoxControl1.Size = new System.Drawing.Size(219, 199);
            this.checkedListBoxControl1.TabIndex = 2;
            // 
            // MapLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 244);
            this.Controls.Add(this.checkedListBoxControl1);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapLevelForm";
            this.Text = "地图级别选择";
            this.Load += new System.EventHandler(this.MapLevelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkedListBoxControl1)).EndInit();
            this.ResumeLayout(false);

		}
	}
}

using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class LogForm : XtraForm
	{
		public string unitid = "";

		private DataTable Log_dt;
        private GridControl gridControl1;
        private GridView gridView1;
        private IContainer components = null;

		public LogForm()
		{
			InitializeComponent();
		}

		private void LogForm_Shown(object sender, EventArgs e)
		{
			Log_dt = new DataTable();
			Log_dt.Columns.Add("序号", typeof(int));
			Log_dt.Columns.Add("操作用户", typeof(string));
			Log_dt.Columns.Add("操作时间", typeof(string));
			Log_dt.Columns.Add("操作类型", typeof(string));
			Log_dt.Columns.Add("操作内容", typeof(string));
			string sql = "select * from LOG_H0001Z000E00 where ISDELETE = 0 and UNITID = '" + unitid + "'order by RUNTIME desc";
			DataTable dataTable = FileReader.log_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				Log_dt.Rows.Add(i + 1, dataTable.Rows[i]["OSNAME"].ToString(), dataTable.Rows[i]["RUNTIME"].ToString(), dataTable.Rows[i]["EVENT"].ToString(), dataTable.Rows[i]["REMARK"].ToString());
			}
			gridControl1.DataSource = Log_dt;
			GridView gridView = gridControl1.MainView as GridView;
			gridView.OptionsBehavior.Editable = false;
			gridView1.Columns[0].Width = 40;
			gridView1.Columns[1].Width = 150;
			gridView1.Columns[2].Width = 200;
			gridView1.Columns[3].Width = 150;
            for (int i = 0; i < 4; ++i)
                gridView1.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(2);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(917, 474);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridView1.DetailHeight = 223;
            this.gridView1.FixedLineWidth = 1;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 474);
            this.Controls.Add(this.gridControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "操作日志";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.LogForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

		}
	}
}

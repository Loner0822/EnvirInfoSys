using DevExpress.Utils;
using DevExpress.Utils.Behaviors;
using DevExpress.Utils.DragDrop;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Extensions;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class Classify_1Form : XtraForm
	{
		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private AccessHelper ahp1 = null;

		private AccessHelper ahp2 = null;

		private AccessHelper ahp3 = null;

		private AccessHelper ahp4 = null;

		public string unitid = "";

		public string gxguid = "-1";

		public int maxlevel = 0;

		public bool NeedShowMap = false;

		private List<string> Prop_GUID;

		private Dictionary<string, string> Show_Name;

		private Dictionary<string, string> Show_FDName;

		private Dictionary<string, string> inherit_GUID;

		private Dictionary<string, string> Show_Value;

		private DataTable GX_dt;

		private IContainer components = null;

		private GroupControl groupControl1;

		private Splitter splitter1;

		private GroupControl groupControl2;

		private XtraTabControl xtraTabControl1;

		private Splitter splitter2;

		private GroupControl groupControl3;

		private GroupControl groupControl5;

		private XtraTabControl xtraTabControl2;

		private GroupControl groupControl4;

		private XtraTabPage xtraTabPage1;

		private XtraTabPage xtraTabPage2;

		private XtraTabPage xtraTabPage3;

		private FlowLayoutPanel flowLayoutPanel1;

		private PopupMenu popupMenu1;

		private PopupMenu popupMenu2;

		private BarManager barManager1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarButtonItem barButtonItem1;

		private BarButtonItem barButtonItem2;

		private BehaviorManager behaviorManager1;

		private PopupMenu popupMenu3;

		private BarButtonItem barButtonItem3;

		private BarButtonItem barButtonItem4;

		private GridControl gridControl1;

		private GridView gridView1;

		private DragDropEvents dragDropEvents1;

		private BarButtonItem barButtonItem5;

		private GridControl gridControl2;

		private GridView gridView2;

		private GridControl gridControl3;

		private GridView gridView3;

		private GridControl gridControl4;

		private GridView gridView4;

		private BarDockControl barDockControl3;

		private BarManager barManager2;

		private Bar bar2;

		private BarDockControl barDockControl1;

		private BarDockControl barDockControl2;

		private BarDockControl barDockControl4;

		private BarButtonItem barButtonItem6;

		private BarButtonItem barButtonItem7;

		private BarButtonItem barButtonItem8;

		public Classify_1Form()
		{
			InitializeComponent();
		}

		public void SetUpGrid(GridControl grid, DataTable table)
		{
			GridView gridView = grid.MainView as GridView;
			grid.DataSource = table;
			gridView.OptionsBehavior.Editable = false;
		}

		public DataTable FillTable()
		{
			GX_dt = new DataTable();
			GX_dt.Columns.Add("guid", typeof(string));
			GX_dt.Columns.Add("序号", typeof(int));
			GX_dt.Columns.Add("显示名称", typeof(string));
			string sql = "select PGUID, FLNAME from ENVIRGXFL_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + gxguid + "' and UNITID = '" + unitid + "' order by SHOWINDEX";
			DataTable dataTable = ahp1.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GX_dt.Rows.Add(dataTable.Rows[i]["PGUID"].ToString(), i + 1, dataTable.Rows[i]["FLNAME"].ToString());
			}
			gridControl1.DataSource = GX_dt;
			gridView1.Columns[0].Visible = false;
			gridView1.Columns[1].Width = 50;
			return GX_dt;
		}

		public void HandleBehaviorDragDropEvents()
		{
			DragDropBehavior behavior = behaviorManager1.GetBehavior<DragDropBehavior>(gridView1);
			behavior.DragDrop += Behavior_DragDrop;
			behavior.DragOver += Behavior_DragOver;
			behavior.EndDragDrop += Behavior_EndDragDrop;
		}

		private void Behavior_DragDrop(object sender, DragDropEventArgs e)
		{
			GridView gridView = e.Target as GridView;
			GridView gridView2 = e.Source as GridView;
			if (e.Action == DragDropActions.None || gridView != gridView2)
			{
				return;
			}
			DataTable dataTable = gridView2.GridControl.DataSource as DataTable;
			Point pt = gridView.GridControl.PointToClient(Cursor.Position);
			GridHitInfo gridHitInfo = gridView.CalcHitInfo(pt);
			int[] data = e.GetData<int[]>();
			int rowHandle = gridHitInfo.RowHandle;
			int dataSourceRowIndex = gridView.GetDataSourceRowIndex(rowHandle);
			List<DataRow> list = new List<DataRow>();
			int[] array = data;
			foreach (int rowHandle2 in array)
			{
				int dataSourceRowIndex2 = gridView2.GetDataSourceRowIndex(rowHandle2);
				DataRow item = dataTable.Rows[dataSourceRowIndex2];
				list.Add(item);
			}
			int num;
			switch (e.InsertType)
			{
			case InsertType.Before:
				num = ((dataSourceRowIndex > data[data.Length - 1]) ? (dataSourceRowIndex - 1) : dataSourceRowIndex);
				for (int j = list.Count - 1; j >= 0; j--)
				{
					DataRow item = list[j];
					DataRow dataRow = dataTable.NewRow();
					dataRow.ItemArray = item.ItemArray;
					dataTable.Rows.Remove(item);
					dataTable.Rows.InsertAt(dataRow, num);
				}
				break;
			case InsertType.After:
				num = ((dataSourceRowIndex < data[0]) ? (dataSourceRowIndex + 1) : dataSourceRowIndex);
				for (int j = 0; j < list.Count; j++)
				{
					DataRow item = list[j];
					DataRow dataRow = dataTable.NewRow();
					dataRow.ItemArray = item.ItemArray;
					dataTable.Rows.Remove(item);
					dataTable.Rows.InsertAt(dataRow, num);
				}
				break;
			default:
				num = -1;
				break;
			}
			int num2 = gridView.FocusedRowHandle = gridView.GetRowHandle(num);
			gridView.SelectRow(gridView.FocusedRowHandle);
		}

		private void Behavior_DragOver(object sender, DragOverEventArgs e)
		{
			DragOverGridEventArgs dragOverGridEventArgs = DragOverGridEventArgs.GetDragOverGridEventArgs(e);
			e.InsertType = dragOverGridEventArgs.InsertType;
			e.InsertIndicatorLocation = dragOverGridEventArgs.InsertIndicatorLocation;
			e.Action = dragOverGridEventArgs.Action;
			Cursor.Current = dragOverGridEventArgs.Cursor;
			dragOverGridEventArgs.Handled = true;
		}

		private void Behavior_EndDragDrop(object sender, EndDragDropEventArgs e)
		{
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				DataRow dataRow = gridView1.GetDataRow(i);
				dataRow["序号"] = i + 1;
				string sql = "update ENVIRGXFL_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', SHOWINDEX = " + (i + 1).ToString() + " where ISDELETE = 0 and UPGUID = '" + gxguid + "' and PGUID = '" + dataRow["guid"].ToString() + "' and UNITID = '" + unitid + "'";
				ahp1.ExecuteSql(sql, (OleDbParameter[])null);
			}
		}

		private void Classify_1Form_Load(object sender, EventArgs e)
		{
			ahp1 = new AccessHelper(WorkPath + "data\\ENVIR_H0001Z000E00.mdb");
			ahp2 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000K00.mdb");
			ahp3 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000K01.mdb");
			ahp4 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000E00.mdb");
			SetUpGrid(gridControl1, FillTable());
			HandleBehaviorDragDropEvents();
			xtraTabControl2.TabPages[0].PageVisible = false;
			xtraTabControl1.Controls.Clear();
			Build_Icon_Library("H0001Z000K00");
			Build_Icon_Library("H0001Z000E00");
		}

		private void Build_Icon_Library(string database)
		{
			AccessHelper accessHelper = null;
			accessHelper = ((!(database == "H0001Z000K00")) ? ahp4 : ahp2);
			string sql = "select UPGUID, PROPVALUE from ZSK_PROP_" + database + " where ISDELETE = 0 and PROPNAME = '图符库' order by PROPVALUE, SHOWINDEX";
			DataTable dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text = dataTable.Rows[i]["PROPVALUE"].ToString();
				string pguid = dataTable.Rows[i]["UPGUID"].ToString();
				int num = text.IndexOf("图符库");
				if (num < 0)
				{
					continue;
				}
				text = text.Substring(0, num);
				if (text == "备用")
				{
					continue;
				}
				bool flag = false;
				for (int j = 0; j < xtraTabControl1.TabPages.Count; j++)
				{
					if (xtraTabControl1.TabPages[j].Name == text)
					{
						flag = true;
						FlowLayoutPanel flp = (FlowLayoutPanel)xtraTabControl1.TabPages[j].Controls[0];
						Add_Icon(flp, pguid, database);
					}
				}
				if (!flag)
				{
					xtraTabControl1.TabPages.Add(text);
					num = xtraTabControl1.TabPages.Count - 1;
					FlowLayoutPanel flp = new FlowLayoutPanel();
					flp.Dock = DockStyle.Fill;
					flp.FlowDirection = FlowDirection.LeftToRight;
					flp.WrapContents = true;
					flp.AutoScroll = true;
					flp.MouseDown += flowLayoutPanel_MouseDown;
					Add_Icon(flp, pguid, database);
					xtraTabControl1.TabPages[num].Name = text;
					xtraTabControl1.TabPages[num].BackColor = SystemColors.Control;
					xtraTabControl1.TabPages[num].Controls.Add(flp);
				}
			}
		}

		private void Add_Icon(FlowLayoutPanel flp, string pguid, string database)
		{
			AccessHelper accessHelper = null;
			accessHelper = ((!(database == "H0001Z000K00")) ? ahp4 : ahp2);
			string str = WorkPath + "ICONDER\\b_PNGICON\\";
			ucPictureBox ucPictureBox = new ucPictureBox();
			string text = "select JDNAME from ZSK_OBJECT_" + database + " where ISDELETE = 0 and PGUID = '" + pguid + "'";
			if (database == "H0001Z000E00")
			{
				text = text + " and UNITID = '" + unitid + "'";
			}
			DataTable dataTable = accessHelper.ExecuteDataTable(text, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				ucPictureBox.Parent = flp;
				ucPictureBox.Name = pguid;
				ucPictureBox.IconName = dataTable.Rows[0]["JDNAME"].ToString();
				ucPictureBox.IconPguid = pguid;
				ucPictureBox.IconPath = str + pguid + ".png";
				ucPictureBox.Single_Click += Icon_SingleClick;
				ucPictureBox.Double_Click += Icon_DoubleClick;
				ucPictureBox.IconCheck = false;
			}
		}

		private void Get_Icon_From_Access(string flguid, string database)
		{
			AccessHelper accessHelper = (!(database == "H0001Z000K00")) ? ahp4 : ahp2;
			string str = WorkPath + "ICONDER\\b_PNGICON\\";
			string str2 = "select PGUID, JDNAME from ZSK_OBJECT_" + database + " where ISDELETE = 0";
			str2 = ((!(database == "H0001Z000K00")) ? (str2 + " and UNITID = '" + unitid + "' order by LEVELNUM, SHOWINDEX") : (str2 + " order by LEVELNUM, SHOWINDEX"));
			DataTable dataTable = accessHelper.ExecuteDataTable(str2, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text = dataTable.Rows[i]["PGUID"].ToString();
				string iconName = dataTable.Rows[i]["JDNAME"].ToString();
				if (File.Exists(str + text + ".png"))
				{
					str2 = "select PGUID from ENVIRGXDY_H0001Z000E00 where ISDELETE = 0 and ICONGUID = '" + text + "' and FLGUID = '" + flguid + "' and UNITID = '" + unitid + "'";
					DataTable dataTable2 = ahp1.ExecuteDataTable(str2, (OleDbParameter[])null);
					if (dataTable2.Rows.Count > 0)
					{
						ucPictureBox ucPictureBox = new ucPictureBox();
						ucPictureBox.Parent = flowLayoutPanel1;
						ucPictureBox.Name = text;
						ucPictureBox.IconName = iconName;
						ucPictureBox.IconPguid = text;
						ucPictureBox.IconPath = str + text + ".png";
						ucPictureBox.Single_Click += Icon_SingleClick;
						ucPictureBox.Double_Click += Icon_DoubleClick;
						ucPictureBox.IconCheck = false;
					}
				}
			}
		}

		private void xtraTabControl1_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
		{
			XtraTabPage selectedTabPage = xtraTabControl1.SelectedTabPage;
			if (selectedTabPage != null && selectedTabPage.Controls.Count > 0)
			{
				FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)selectedTabPage.Controls[0];
				foreach (ucPictureBox control in flowLayoutPanel.Controls)
				{
					control.IconCheck = false;
				}
			}
		}

		private void Icon_SingleClick(object sender, EventArgs e, string iconguid)
		{
			ucPictureBox ucPictureBox = (ucPictureBox)sender;
			if (!ucPictureBox.IconCheck)
			{
				if (ucPictureBox.Parent == flowLayoutPanel1)
				{
					foreach (ucPictureBox control in flowLayoutPanel1.Controls)
					{
						control.IconCheck = false;
					}
					ucPictureBox.IconCheck = true;
					Show_Icon_Property(iconguid);
				}
				else
				{
					FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)ucPictureBox.Parent;
					foreach (ucPictureBox control2 in flowLayoutPanel.Controls)
					{
						control2.IconCheck = false;
					}
					ucPictureBox.IconCheck = true;
				}
			}
		}

		private void Icon_DoubleClick(object sender, EventArgs e, string iconguid)
		{
			if (gridView1.DataRowCount == 0)
			{
				XtraMessageBox.Show("请先添加管辖分类!");
				return;
			}
			string text = gridView1.GetFocusedDataRow()["guid"].ToString();
			ucPictureBox ucPictureBox = (ucPictureBox)sender;
			if (ucPictureBox.Parent == flowLayoutPanel1)
			{
				Control value = ucPictureBox;
				flowLayoutPanel1.Controls.Remove(value);
				string sql = "update ENVIRGXDY_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + iconguid + "' and FLGUID = '" + text + "' and UNITID = '" + unitid + "'";
				ahp1.ExecuteSql(sql, (OleDbParameter[])null);
			}
			else
			{
				foreach (ucPictureBox control in flowLayoutPanel1.Controls)
				{
					if (control.IconPguid == ucPictureBox.IconPguid)
					{
						XtraMessageBox.Show("已添加该图符!");
						return;
					}
				}
				ucPictureBox ucPictureBox3 = new ucPictureBox();
				ucPictureBox3.IconName = ucPictureBox.IconName;
				ucPictureBox3.IconPguid = ucPictureBox.IconPguid;
				ucPictureBox3.IconPath = ucPictureBox.IconPath;
				ucPictureBox3.IconCheck = false;
				ucPictureBox3.Single_Click += Icon_SingleClick;
				ucPictureBox3.Double_Click += Icon_DoubleClick;
				flowLayoutPanel1.Controls.Add(ucPictureBox3);
				string sql = "select PGUID from ENVIRGXDY_H0001Z000E00 where ICONGUID = '" + iconguid + "' and FLGUID = '" + text + "' and UNITID = '" + unitid + "'";
				DataTable dataTable = ahp1.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					sql = "update ENVIRGXDY_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + iconguid + "' and FLGUID = '" + text + "' and UNITID = '" + unitid + "'";
					ahp1.ExecuteSql(sql, (OleDbParameter[])null);
				}
				else
				{
					sql = "insert into ENVIRGXDY_H0001Z000E00 (PGUID, S_UDTIME, ICONGUID, FLGUID, UNITID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + iconguid + "', '" + text + "', '" + unitid + "')";
					ahp1.ExecuteSql(sql, (OleDbParameter[])null);
				}
			}
			if (flowLayoutPanel1.Controls.Count > 0)
			{
				ucPictureBox ucPictureBox4 = (ucPictureBox)flowLayoutPanel1.Controls[0];
				string iconPguid = ucPictureBox4.IconPguid;
				Icon_SingleClick(flowLayoutPanel1.Controls[0], new EventArgs(), iconPguid);
			}
		}

		private void Show_Icon_Property(string iconguid)
		{
			string text = "";
			text = "{26E232C8-595F-44E5-8E0F-8E0FC1BD7D24}";
			Get_Property_Data(gridControl2, gridView2, iconguid, text);
			text = "{B55806E6-9D63-4666-B6EB-AAB80814648E}";
			Get_Property_Data(gridControl3, gridView3, iconguid, text);
			text = "{D7DE9C5E-253C-491C-A380-06E41C68D2C8}";
			Get_Property_Data(gridControl4, gridView4, iconguid, text);
		}

		private void Get_Property_Data(GridControl gc, GridView gv, string iconguid, string typeguid)
		{
			Prop_GUID = new List<string>();
			Show_Name = new Dictionary<string, string>();
			Show_FDName = new Dictionary<string, string>();
			inherit_GUID = new Dictionary<string, string>();
			Show_Value = new Dictionary<string, string>();
			gc.DataSource = null;
			gv.Columns.Clear();
			DataTable dataTable = new DataTable();
			string sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_H0001Z000K00 where ISDELETE = 0 and UPGUID = '" + iconguid + "' and PROTYPEGUID = '" + typeguid + "' order by SHOWINDEX";
			DataTable proptable = ahp2.ExecuteDataTable(sql, (OleDbParameter[])null);
			Add_Prop(proptable);
			sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_H0001Z000K01 where ISDELETE = 0 and UPGUID = '" + iconguid + "' and PROTYPEGUID = '" + typeguid + "' order by SHOWINDEX";
			proptable = ahp3.ExecuteDataTable(sql, (OleDbParameter[])null);
			Add_Prop(proptable);
			sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + iconguid + "' and PROTYPEGUID = '" + typeguid + "' order by SHOWINDEX";
			proptable = ahp4.ExecuteDataTable(sql, (OleDbParameter[])null);
			Add_Prop(proptable);
			List<string> list = new List<string>();
			bool flag = false;
			for (int i = 0; i < Prop_GUID.Count; i++)
			{
				string key = Prop_GUID[i];
				flag = true;
				dataTable.Columns.Add(Show_Name[key]);
				list.Add(Show_Value[key]);
			}
			DataRow dataRow = dataTable.NewRow();
			if (flag)
			{
				for (int i = 0; i < list.Count; i++)
				{
					dataRow[i] = list[i];
				}
			}
			dataTable.Rows.Add(dataRow);
			gc.DataSource = dataTable;
		}

		private void Add_Prop(DataTable proptable)
		{
			for (int i = 0; i < proptable.Rows.Count; i++)
			{
				Prop_GUID.Add(proptable.Rows[i]["PGUID"].ToString());
				Show_Name[proptable.Rows[i]["PGUID"].ToString()] = proptable.Rows[i]["PROPNAME"].ToString();
				Show_FDName[proptable.Rows[i]["PGUID"].ToString()] = proptable.Rows[i]["FDNAME"].ToString();
				inherit_GUID[proptable.Rows[i]["PGUID"].ToString()] = proptable.Rows[i]["SOURCEGUID"].ToString();
				Show_Value[proptable.Rows[i]["PGUID"].ToString()] = proptable.Rows[i]["PROPVALUE"].ToString();
			}
		}

		private void gridView1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			flowLayoutPanel1.Controls.Clear();
			if (gridView1.GetFocusedDataRow() != null)
			{
				string flguid = gridView1.GetFocusedDataRow()["guid"].ToString();
				Show_Icon_List(flguid);
			}
		}

		private void Show_Icon_List(string flguid)
		{
			Get_Icon_From_Access(flguid, "H0001Z000K00");
			Get_Icon_From_Access(flguid, "H0001Z000E00");
			if (flowLayoutPanel1.Controls.Count > 0)
			{
				ucPictureBox ucPictureBox = (ucPictureBox)flowLayoutPanel1.Controls[0];
				Icon_SingleClick(flowLayoutPanel1.Controls[0], new EventArgs(), ucPictureBox.IconPguid);
			}
		}

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			string text = gridView1.GetFocusedDataRow()["guid"].ToString();
			foreach (ucPictureBox control in flowLayoutPanel1.Controls)
			{
				string sql = "update ENVIRGXDY_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and FLGUID = '" + text + "' and UNITID = '" + unitid + "'";
				ahp1.ExecuteSql(sql, (OleDbParameter[])null);
			}
			flowLayoutPanel1.Controls.Clear();
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			string text = gridView1.GetFocusedDataRow()["guid"].ToString();
			int selectedTabPageIndex = xtraTabControl1.SelectedTabPageIndex;
			FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)xtraTabControl1.TabPages[selectedTabPageIndex].Controls[0];
			foreach (ucPictureBox control in flowLayoutPanel.Controls)
			{
				bool flag = false;
				foreach (ucPictureBox control2 in flowLayoutPanel1.Controls)
				{
					if (control.IconPguid == control2.IconPguid)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					ucPictureBox ucPictureBox3 = new ucPictureBox();
					ucPictureBox3.IconName = control.IconName;
					ucPictureBox3.IconPguid = control.IconPguid;
					ucPictureBox3.IconPath = control.IconPath;
					ucPictureBox3.IconCheck = false;
					ucPictureBox3.Single_Click += Icon_SingleClick;
					ucPictureBox3.Double_Click += Icon_DoubleClick;
					flowLayoutPanel1.Controls.Add(ucPictureBox3);
					string sql = "select PGUID from ENVIRGXDY_H0001Z000E00 where ICONGUID = '" + control.IconPguid + "' and FLGUID = '" + text + "' and UNITID = '" + unitid + "'";
					DataTable dataTable = ahp1.ExecuteDataTable(sql, (OleDbParameter[])null);
					if (dataTable.Rows.Count > 0)
					{
						sql = "update ENVIRGXDY_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and FLGUID = '" + text + "' and UNITID = '" + unitid + "'";
						ahp1.ExecuteSql(sql, (OleDbParameter[])null);
					}
					else
					{
						sql = "insert into ENVIRGXDY_H0001Z000E00 (PGUID, S_UDTIME, ICONGUID, FLGUID, UNITID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + control.IconPguid + "', '" + text + "', '" + unitid + "')";
						ahp1.ExecuteSql(sql, (OleDbParameter[])null);
					}
				}
			}
			if (flowLayoutPanel1.Controls.Count > 0)
			{
				ucPictureBox ucPictureBox4 = (ucPictureBox)flowLayoutPanel1.Controls[0];
				string iconPguid = ucPictureBox4.IconPguid;
				Icon_SingleClick(flowLayoutPanel1.Controls[0], new EventArgs(), iconPguid);
			}
		}

		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
		{
			int num = gridView1.RowCount + 1;
			DataRow dataRow = GX_dt.NewRow();
			string text2 = (string)(dataRow["guid"] = Guid.NewGuid().ToString("B"));
			EditForm editForm = new EditForm();
			if (editForm.ShowDialog() == DialogResult.OK)
			{
				dataRow["显示名称"] = editForm.EditText;
				dataRow["序号"] = num;
				string sql = "insert into ENVIRGXFL_H0001Z000E00 (PGUID, S_UDTIME, FLNAME, UPGUID, SHOWINDEX, UNITID) values ('" + text2 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + editForm.EditText + "', '" + gxguid + "', '" + num.ToString() + "', '" + unitid + "')";
				ahp1.ExecuteSql(sql, (OleDbParameter[])null);
				GX_dt.Rows.Add(dataRow);
				gridView1.FocusedRowHandle = num - 1;
			}
		}

		private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
		{
			int focusedRowHandle = gridView1.FocusedRowHandle;
			string text = gridView1.GetFocusedDataRow()["guid"].ToString();
			string sql = "update ENVIRGXFL_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ISDELETE = 0 and PGUID = '" + text + "' and UNITID = '" + unitid + "'";
			ahp1.ExecuteSql(sql, (OleDbParameter[])null);
			sql = "update ENVIRGXDY_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ISDELETE = 0 and FLGUID = '" + text + "' and UNITID = '" + unitid + "'";
			ahp1.ExecuteSql(sql, (OleDbParameter[])null);
			gridView1.DeleteSelectedRows();
			for (int i = focusedRowHandle; i < gridView1.RowCount; i++)
			{
				gridView1.GetDataRow(i)["序号"] = i + 1;
			}
		}

		private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
		{
			EditForm editForm = new EditForm();
			editForm.EditText = gridView1.GetFocusedDataRow()["显示名称"].ToString();
			if (editForm.ShowDialog() == DialogResult.OK)
			{
				gridView1.GetFocusedDataRow()["显示名称"] = editForm.EditText;
				string sql = "update ENVIRGXFL_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', FLNAME = '" + editForm.EditText + "' where ISDELETE = 0 and PGUID = '" + gridView1.GetFocusedDataRow()["guid"].ToString() + "' and UNITID = '" + unitid + "'";
				ahp1.ExecuteSql(sql, (OleDbParameter[])null);
			}
		}

		private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				popupMenu1.ShowPopup(barManager1, Control.MousePosition);
			}
		}

		private void flowLayoutPanel_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				popupMenu2.ShowPopup(barManager1, Control.MousePosition);
			}
		}

		private void gridView1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				popupMenu3.ShowPopup(barManager1, Control.MousePosition);
			}
		}

		private void Classify_1Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			ahp1.CloseConn();
			ahp2.CloseConn();
			ahp3.CloseConn();
			ahp4.CloseConn();
		}

		private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
		{
			Classify_2Form classify_2Form = new Classify_2Form();
			classify_2Form.unitid = unitid;
			classify_2Form.maxlevel = maxlevel;
			classify_2Form.ShowDialog();
			string remark = "修改图符对应设置";
			ComputerInfo.WriteLog("图符对应设置", remark);
		}

		private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
		{
			NeedShowMap = true;
			Process process = Process.Start(WorkPath + "tfkzdy.exe");
			process.WaitForExit();
			ahp2.CloseConn();
			ahp4.CloseConn();
			ahp2 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000K00.mdb");
			ahp4 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000E00.mdb");
			xtraTabControl1.Controls.Clear();
			xtraTabControl1.TabPages.Clear();
			Build_Icon_Library("H0001Z000K00");
			Build_Icon_Library("H0001Z000E00");
			flowLayoutPanel1.Controls.Clear();
			if (gridView1.GetFocusedDataRow() != null)
			{
				string flguid = gridView1.GetFocusedDataRow()["guid"].ToString();
				Show_Icon_List(flguid);
				string path = WorkPath + "ICONDER\\b_PNGICON_tmp\\";
				string path2 = WorkPath + "ICONDER\\s_PNGICON_tmp\\";
				if (Directory.Exists(path) || Directory.Exists(path2))
				{
					XtraMessageBox.Show("检测到存在更换图符操作，即将重启程序");
					process = Process.Start(WorkPath + "ReStart.exe", "EnvirInfoSys.exe");
				}
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Classify_1Form));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl2 = new DevExpress.XtraGrid.GridControl();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl3 = new DevExpress.XtraGrid.GridControl();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.gridControl4 = new DevExpress.XtraGrid.GridControl();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.behaviorManager1 = new DevExpress.Utils.Behaviors.BehaviorManager(this.components);
            this.dragDropEvents1 = new DevExpress.Utils.DragDrop.DragDropEvents(this.components);
            this.popupMenu3 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem8 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).BeginInit();
            this.xtraTabControl2.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            this.xtraTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 35);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(261, 698);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "图符显示管理";
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(2, 31);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.MenuManager = this.barManager1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(257, 665);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gridView1.Appearance.FocusedRow.BackColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gridView1.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.behaviorManager1.SetBehaviors(this.gridView1, new DevExpress.Utils.Behaviors.Behavior[] {
            ((DevExpress.Utils.Behaviors.Behavior)(DevExpress.Utils.DragDrop.DragDropBehavior.Create(typeof(DevExpress.XtraGrid.Extensions.ColumnViewDragDropSource), true, true, true, this.dragDropEvents1)))});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowColumnMoving = false;
            this.gridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsFilter.AllowMultiSelectInCheckedFilterPopup = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView1_FocusedRowChanged);
            this.gridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridView1_MouseDown);
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3,
            this.barButtonItem4,
            this.barButtonItem5});
            this.barManager1.MaxItemId = 5;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 35);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1309, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 733);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1309, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 35);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 698);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1309, 35);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 698);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "清空";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "全选";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "添加";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "删除";
            this.barButtonItem4.Id = 3;
            this.barButtonItem4.Name = "barButtonItem4";
            this.barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem4_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Caption = "编辑";
            this.barButtonItem5.Id = 4;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(261, 35);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 698);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.xtraTabControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl2.Location = new System.Drawing.Point(885, 35);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(424, 698);
            this.groupControl2.TabIndex = 2;
            this.groupControl2.Text = "图符库";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 31);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new System.Drawing.Size(420, 665);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(875, 35);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(10, 698);
            this.splitter2.TabIndex = 3;
            this.splitter2.TabStop = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.groupControl3.Appearance.Options.UseBackColor = true;
            this.groupControl3.Controls.Add(this.groupControl5);
            this.groupControl3.Controls.Add(this.groupControl4);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(271, 35);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(604, 698);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "图符列表";
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.xtraTabControl2);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl5.Location = new System.Drawing.Point(148, 31);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(454, 665);
            this.groupControl5.TabIndex = 1;
            this.groupControl5.Text = "图符属性";
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.Location = new System.Drawing.Point(2, 31);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl2.Size = new System.Drawing.Size(450, 632);
            this.xtraTabControl2.TabIndex = 0;
            this.xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.gridControl2);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(442, 590);
            this.xtraTabPage1.Text = "固定属性";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.MenuManager = this.barManager1;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(442, 590);
            this.gridControl2.TabIndex = 0;
            this.gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView2});
            // 
            // gridView2
            // 
            this.gridView2.GridControl = this.gridControl2;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsBehavior.Editable = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.gridControl3);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(442, 590);
            this.xtraTabPage2.Text = "基本属性";
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.Location = new System.Drawing.Point(0, 0);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.MenuManager = this.barManager1;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(442, 590);
            this.gridControl3.TabIndex = 1;
            this.gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView3});
            // 
            // gridView3
            // 
            this.gridView3.GridControl = this.gridControl3;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.Editable = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.gridControl4);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(442, 590);
            this.xtraTabPage3.Text = "扩展属性";
            // 
            // gridControl4
            // 
            this.gridControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl4.Location = new System.Drawing.Point(0, 0);
            this.gridControl4.MainView = this.gridView4;
            this.gridControl4.MenuManager = this.barManager1;
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.Size = new System.Drawing.Size(442, 590);
            this.gridControl4.TabIndex = 1;
            this.gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView4});
            // 
            // gridView4
            // 
            this.gridView4.GridControl = this.gridControl4;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsBehavior.Editable = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.flowLayoutPanel1);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl4.Location = new System.Drawing.Point(2, 31);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(146, 665);
            this.groupControl4.TabIndex = 0;
            this.groupControl4.Text = "选中图符";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(2, 31);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(142, 632);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseDown);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem1)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // popupMenu2
            // 
            this.popupMenu2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem2)});
            this.popupMenu2.Manager = this.barManager1;
            this.popupMenu2.Name = "popupMenu2";
            // 
            // popupMenu3
            // 
            this.popupMenu3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5)});
            this.popupMenu3.Manager = this.barManager1;
            this.popupMenu3.Name = "popupMenu3";
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem6,
            this.barButtonItem7,
            this.barButtonItem8});
            this.barManager2.MainMenu = this.bar2;
            this.barManager2.MaxItemId = 3;
            // 
            // bar2
            // 
            this.bar2.BarName = "Main menu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem8)});
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.MultiLine = true;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "Main menu";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Border = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.barButtonItem6.Caption = "管辖分类";
            this.barButtonItem6.Id = 0;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "图符对应";
            this.barButtonItem7.Id = 1;
            this.barButtonItem7.Name = "barButtonItem7";
            this.barButtonItem7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem7_ItemClick);
            // 
            // barButtonItem8
            // 
            this.barButtonItem8.Caption = "图符扩展";
            this.barButtonItem8.Id = 2;
            this.barButtonItem8.Name = "barButtonItem8";
            this.barButtonItem8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem8_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barManager2;
            this.barDockControl1.Size = new System.Drawing.Size(1309, 35);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 733);
            this.barDockControl2.Manager = this.barManager2;
            this.barDockControl2.Size = new System.Drawing.Size(1309, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 35);
            this.barDockControl3.Manager = this.barManager2;
            this.barDockControl3.Size = new System.Drawing.Size(0, 698);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1309, 35);
            this.barDockControl4.Manager = this.barManager2;
            this.barDockControl4.Size = new System.Drawing.Size(0, 698);
            // 
            // Classify_1Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 733);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Classify_1Form";
            this.Text = "管辖分类设置";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Classify_1Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl2)).EndInit();
            this.xtraTabControl2.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            this.xtraTabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.behaviorManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}

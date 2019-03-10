using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
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
	public class Classify_2Form : XtraForm
	{
		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private AccessHelper ahp1 = null;

		private AccessHelper ahp2 = null;

		private AccessHelper ahp3 = null;

		private AccessHelper ahp4 = null;

		private AccessHelper ahp5 = null;

		public string unitid = "";

		public string gxguid = "-1";

		public int maxlevel = 0;

		private List<string> Prop_GUID;

		private Dictionary<string, string> Show_Name;

		private Dictionary<string, string> Show_FDName;

		private Dictionary<string, string> inherit_GUID;

		private Dictionary<string, string> Show_Value;

		private Dictionary<string, string> GL_NAME;

		private Dictionary<string, string> GL_JDCODE;

		private Dictionary<string, string> GL_UPGUID;

		private Dictionary<string, string> GL_MAP;

		private List<GL_Point> GL_List;

		private IContainer components = null;

		private GroupControl groupControl1;

		private Splitter splitter1;

		private Splitter splitter2;

		private GroupControl groupControl2;

		private XtraTabControl xtraTabControl1;

		private TreeList treeList1;

		private GroupControl groupControl3;

		private GroupControl groupControl5;

		private XtraTabControl xtraTabControl2;

		private XtraTabPage xtraTabPage1;

		private GridControl gridControl2;

		private GridView gridView2;

		private XtraTabPage xtraTabPage2;

		private GridControl gridControl3;

		private GridView gridView3;

		private XtraTabPage xtraTabPage3;

		private GridControl gridControl4;

		private GridView gridView4;

		private GroupControl groupControl4;

		private FlowLayoutPanel flowLayoutPanel1;

		private BarManager barManager1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private PopupMenu popupMenu1;

		private PopupMenu popupMenu2;

		private BarButtonItem barButtonItem1;

		private BarButtonItem barButtonItem2;

		public Classify_2Form()
		{
			InitializeComponent();
		}

		private void Classify_1Form_Load(object sender, EventArgs e)
		{
			ahp1 = new AccessHelper(WorkPath + "data\\ENVIR_H0001Z000E00.mdb");
			ahp2 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000K00.mdb");
			ahp3 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000K01.mdb");
			ahp4 = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000E00.mdb");
			ahp5 = new AccessHelper(WorkPath + "data\\ENVIRDYDATA_H0001Z000E00.mdb");
			xtraTabControl2.TabPages[0].PageVisible = false;
			xtraTabControl1.Controls.Clear();
			Build_Icon_Library("H0001Z000K00");
			Build_Icon_Library("H0001Z000E00");
			Load_Unit_Level();
		}

		private void Load_Unit_Level()
		{
			GL_NAME = new Dictionary<string, string>();
			GL_JDCODE = new Dictionary<string, string>();
			GL_UPGUID = new Dictionary<string, string>();
			GL_MAP = new Dictionary<string, string>();
			GL_List = new List<GL_Point>();
			string sql = "select PGUID, JDNAME, JDCODE, UPGUID from ZSK_OBJECT_H0001Z000K01 where ISDELETE = 0 and LEVELNUM >= " + maxlevel.ToString();
			DataTable dataTable = ahp3.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GL_NAME[dataTable.Rows[i]["PGUID"].ToString()] = dataTable.Rows[i]["JDNAME"].ToString();
				GL_JDCODE[dataTable.Rows[i]["PGUID"].ToString()] = dataTable.Rows[i]["JDCODE"].ToString();
				GL_UPGUID[dataTable.Rows[i]["PGUID"].ToString()] = dataTable.Rows[i]["UPGUID"].ToString();
				GL_Point gL_Point = new GL_Point();
				gL_Point.Name = dataTable.Rows[i]["JDNAME"].ToString();
				gL_Point.pguid = dataTable.Rows[i]["PGUID"].ToString();
				gL_Point.upguid = dataTable.Rows[i]["UPGUID"].ToString();
				GL_List.Add(gL_Point);
			}
			treeList1.Nodes.Clear();
			treeList1.Appearance.FocusedCell.BackColor = Color.SteelBlue;
			treeList1.KeyFieldName = "pguid";
			treeList1.ParentFieldName = "upguid";
			treeList1.DataSource = GL_List;
			treeList1.HorzScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;
			treeList1.ExpandAll();
			if (treeList1.Nodes.Count > 0)
			{
				treeList1.FocusedNode = treeList1.Nodes[0];
			}
		}

		private void Add_Unit_Node(TreeNode pa)
		{
			foreach (KeyValuePair<string, string> item in GL_UPGUID)
			{
				if (item.Value == pa.Tag.ToString())
				{
					TreeNode treeNode = new TreeNode();
					treeNode.Text = GL_NAME[item.Key];
					treeNode.Tag = item.Key;
					pa.Nodes.Add(treeNode);
					Add_Unit_Node(treeNode);
				}
			}
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

		private void Get_Icon_From_Access(string levelguid, string database)
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
					str2 = "select PGUID from ICONDUIYING_H0001Z000E00 where ISDELETE = 0 and ICONGUID = '" + text + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
					DataTable dataTable2 = ahp5.ExecuteDataTable(str2, (OleDbParameter[])null);
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
			if (selectedTabPage.Controls.Count > 0)
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
			string text = treeList1.FocusedNode["pguid"].ToString();
			ucPictureBox ucPictureBox = (ucPictureBox)sender;
			if (ucPictureBox.Parent == flowLayoutPanel1)
			{
				Control value = ucPictureBox;
				flowLayoutPanel1.Controls.Remove(value);
				string sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + iconguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
				ahp5.ExecuteSql(sql, (OleDbParameter[])null);
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
				string sql = "select PGUID from ICONDUIYING_H0001Z000E00 where ICONGUID = '" + iconguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
				DataTable dataTable = ahp5.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + iconguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
					ahp5.ExecuteSql(sql, (OleDbParameter[])null);
				}
				else
				{
					sql = "insert into ICONDUIYING_H0001Z000E00 (PGUID, S_UDTIME, ICONGUID, LEVELGUID, UNITEID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + iconguid + "', '" + text + "', '" + unitid + "')";
					ahp5.ExecuteSql(sql, (OleDbParameter[])null);
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

		private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			string flguid = treeList1.FocusedNode["pguid"].ToString();
			flowLayoutPanel1.Controls.Clear();
			Show_Icon_List(flguid);
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
			string text = treeList1.FocusedNode["pguid"].ToString();
			foreach (ucPictureBox control in flowLayoutPanel1.Controls)
			{
				string sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
				ahp5.ExecuteSql(sql, (OleDbParameter[])null);
			}
			flowLayoutPanel1.Controls.Clear();
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			string text = treeList1.FocusedNode["pguid"].ToString();
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
					string sql = "select PGUID from ICONDUIYING_H0001Z000E00 where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
					DataTable dataTable = ahp5.ExecuteDataTable(sql, (OleDbParameter[])null);
					if (dataTable.Rows.Count > 0)
					{
						sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
						ahp5.ExecuteSql(sql, (OleDbParameter[])null);
					}
					else
					{
						sql = "insert into ICONDUIYING_H0001Z000E00 (PGUID, S_UDTIME, ICONGUID, LEVELGUID, UNITEID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + control.IconPguid + "', '" + text + "', '" + unitid + "')";
						ahp5.ExecuteSql(sql, (OleDbParameter[])null);
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

		private void Classify_1Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			ahp1.CloseConn();
			ahp2.CloseConn();
			ahp3.CloseConn();
			ahp4.CloseConn();
			ahp5.CloseConn();
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.Classify_2Form));
			groupControl1 = new DevExpress.XtraEditors.GroupControl();
			treeList1 = new DevExpress.XtraTreeList.TreeList();
			splitter1 = new System.Windows.Forms.Splitter();
			splitter2 = new System.Windows.Forms.Splitter();
			groupControl2 = new DevExpress.XtraEditors.GroupControl();
			xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
			groupControl3 = new DevExpress.XtraEditors.GroupControl();
			groupControl5 = new DevExpress.XtraEditors.GroupControl();
			xtraTabControl2 = new DevExpress.XtraTab.XtraTabControl();
			xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
			gridControl2 = new DevExpress.XtraGrid.GridControl();
			gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
			xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
			gridControl3 = new DevExpress.XtraGrid.GridControl();
			gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
			xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
			gridControl4 = new DevExpress.XtraGrid.GridControl();
			gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
			groupControl4 = new DevExpress.XtraEditors.GroupControl();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			barManager1 = new DevExpress.XtraBars.BarManager(components);
			barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
			popupMenu2 = new DevExpress.XtraBars.PopupMenu(components);
			((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
			groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)treeList1).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
			groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)xtraTabControl1).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControl3).BeginInit();
			groupControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupControl5).BeginInit();
			groupControl5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)xtraTabControl2).BeginInit();
			xtraTabControl2.SuspendLayout();
			xtraTabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControl2).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
			xtraTabPage2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControl3).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView3).BeginInit();
			xtraTabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)gridControl4).BeginInit();
			((System.ComponentModel.ISupportInitialize)gridView4).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControl4).BeginInit();
			groupControl4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupMenu1).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupMenu2).BeginInit();
			SuspendLayout();
			groupControl1.Controls.Add(treeList1);
			groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
			groupControl1.Location = new System.Drawing.Point(0, 0);
			groupControl1.Name = "groupControl1";
			groupControl1.Size = new System.Drawing.Size(200, 738);
			groupControl1.TabIndex = 0;
			groupControl1.Text = "管辖范围";
			treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
			treeList1.Location = new System.Drawing.Point(2, 31);
			treeList1.Name = "treeList1";
			treeList1.OptionsBehavior.Editable = false;
			treeList1.Size = new System.Drawing.Size(196, 705);
			treeList1.TabIndex = 0;
			treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
			treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(treeList1_FocusedNodeChanged);
			splitter1.Location = new System.Drawing.Point(200, 0);
			splitter1.Name = "splitter1";
			splitter1.Size = new System.Drawing.Size(10, 738);
			splitter1.TabIndex = 2;
			splitter1.TabStop = false;
			splitter2.Dock = System.Windows.Forms.DockStyle.Right;
			splitter2.Location = new System.Drawing.Point(880, 0);
			splitter2.Name = "splitter2";
			splitter2.Size = new System.Drawing.Size(10, 738);
			splitter2.TabIndex = 6;
			splitter2.TabStop = false;
			groupControl2.Controls.Add(xtraTabControl1);
			groupControl2.Dock = System.Windows.Forms.DockStyle.Right;
			groupControl2.Location = new System.Drawing.Point(890, 0);
			groupControl2.Name = "groupControl2";
			groupControl2.Size = new System.Drawing.Size(424, 738);
			groupControl2.TabIndex = 5;
			groupControl2.Text = "图符库";
			xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			xtraTabControl1.Location = new System.Drawing.Point(2, 31);
			xtraTabControl1.Name = "xtraTabControl1";
			xtraTabControl1.Size = new System.Drawing.Size(420, 705);
			xtraTabControl1.TabIndex = 0;
			xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl1_SelectedPageChanged);
			groupControl3.Appearance.BackColor = System.Drawing.SystemColors.Control;
			groupControl3.Appearance.Options.UseBackColor = true;
			groupControl3.Controls.Add(groupControl5);
			groupControl3.Controls.Add(groupControl4);
			groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControl3.Location = new System.Drawing.Point(210, 0);
			groupControl3.Name = "groupControl3";
			groupControl3.Size = new System.Drawing.Size(670, 738);
			groupControl3.TabIndex = 7;
			groupControl3.Text = "图符列表";
			groupControl5.Controls.Add(xtraTabControl2);
			groupControl5.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControl5.Location = new System.Drawing.Point(148, 31);
			groupControl5.Name = "groupControl5";
			groupControl5.Size = new System.Drawing.Size(520, 705);
			groupControl5.TabIndex = 1;
			groupControl5.Text = "图符属性";
			xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			xtraTabControl2.Location = new System.Drawing.Point(2, 31);
			xtraTabControl2.Name = "xtraTabControl2";
			xtraTabControl2.SelectedTabPage = xtraTabPage1;
			xtraTabControl2.Size = new System.Drawing.Size(516, 672);
			xtraTabControl2.TabIndex = 0;
			xtraTabControl2.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[3]
			{
				xtraTabPage1,
				xtraTabPage2,
				xtraTabPage3
			});
			xtraTabPage1.Controls.Add(gridControl2);
			xtraTabPage1.Name = "xtraTabPage1";
			xtraTabPage1.Size = new System.Drawing.Size(508, 630);
			xtraTabPage1.Text = "固定属性";
			gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControl2.Location = new System.Drawing.Point(0, 0);
			gridControl2.MainView = gridView2;
			gridControl2.Name = "gridControl2";
			gridControl2.Size = new System.Drawing.Size(508, 630);
			gridControl2.TabIndex = 0;
			gridControl2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1]
			{
				gridView2
			});
			gridView2.GridControl = gridControl2;
			gridView2.Name = "gridView2";
			gridView2.OptionsBehavior.Editable = false;
			gridView2.OptionsView.ShowGroupPanel = false;
			xtraTabPage2.Controls.Add(gridControl3);
			xtraTabPage2.Name = "xtraTabPage2";
			xtraTabPage2.Size = new System.Drawing.Size(508, 630);
			xtraTabPage2.Text = "基本属性";
			gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControl3.Location = new System.Drawing.Point(0, 0);
			gridControl3.MainView = gridView3;
			gridControl3.Name = "gridControl3";
			gridControl3.Size = new System.Drawing.Size(508, 630);
			gridControl3.TabIndex = 1;
			gridControl3.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1]
			{
				gridView3
			});
			gridView3.GridControl = gridControl3;
			gridView3.Name = "gridView3";
			gridView3.OptionsBehavior.Editable = false;
			gridView3.OptionsView.ShowGroupPanel = false;
			xtraTabPage3.Controls.Add(gridControl4);
			xtraTabPage3.Name = "xtraTabPage3";
			xtraTabPage3.Size = new System.Drawing.Size(508, 630);
			xtraTabPage3.Text = "扩展属性";
			gridControl4.Dock = System.Windows.Forms.DockStyle.Fill;
			gridControl4.Location = new System.Drawing.Point(0, 0);
			gridControl4.MainView = gridView4;
			gridControl4.Name = "gridControl4";
			gridControl4.Size = new System.Drawing.Size(508, 630);
			gridControl4.TabIndex = 1;
			gridControl4.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[1]
			{
				gridView4
			});
			gridView4.GridControl = gridControl4;
			gridView4.Name = "gridView4";
			gridView4.OptionsBehavior.Editable = false;
			gridView4.OptionsView.ShowGroupPanel = false;
			groupControl4.Controls.Add(flowLayoutPanel1);
			groupControl4.Dock = System.Windows.Forms.DockStyle.Left;
			groupControl4.Location = new System.Drawing.Point(2, 31);
			groupControl4.Name = "groupControl4";
			groupControl4.Size = new System.Drawing.Size(146, 705);
			groupControl4.TabIndex = 0;
			groupControl4.Text = "选中图符";
			flowLayoutPanel1.AutoScroll = true;
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			flowLayoutPanel1.Location = new System.Drawing.Point(2, 31);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(142, 672);
			flowLayoutPanel1.TabIndex = 4;
			flowLayoutPanel1.WrapContents = false;
			barManager1.DockControls.Add(barDockControlTop);
			barManager1.DockControls.Add(barDockControlBottom);
			barManager1.DockControls.Add(barDockControlLeft);
			barManager1.DockControls.Add(barDockControlRight);
			barManager1.Form = this;
			barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[2]
			{
				barButtonItem1,
				barButtonItem2
			});
			barManager1.MaxItemId = 2;
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			barDockControlTop.Location = new System.Drawing.Point(0, 0);
			barDockControlTop.Manager = barManager1;
			barDockControlTop.Size = new System.Drawing.Size(1314, 0);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			barDockControlBottom.Location = new System.Drawing.Point(0, 738);
			barDockControlBottom.Manager = barManager1;
			barDockControlBottom.Size = new System.Drawing.Size(1314, 0);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			barDockControlLeft.Manager = barManager1;
			barDockControlLeft.Size = new System.Drawing.Size(0, 738);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			barDockControlRight.Location = new System.Drawing.Point(1314, 0);
			barDockControlRight.Manager = barManager1;
			barDockControlRight.Size = new System.Drawing.Size(0, 738);
			barButtonItem1.Caption = "清空";
			barButtonItem1.Id = 0;
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
			barButtonItem2.Caption = "全选";
			barButtonItem2.Id = 1;
			barButtonItem2.Name = "barButtonItem2";
			barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem2_ItemClick);
			popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem1)
			});
			popupMenu1.Manager = barManager1;
			popupMenu1.Name = "popupMenu1";
			popupMenu2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem2)
			});
			popupMenu2.Manager = barManager1;
			popupMenu2.Name = "popupMenu2";
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1314, 738);
			base.Controls.Add(groupControl3);
			base.Controls.Add(splitter2);
			base.Controls.Add(groupControl2);
			base.Controls.Add(splitter1);
			base.Controls.Add(groupControl1);
			base.Controls.Add(barDockControlLeft);
			base.Controls.Add(barDockControlRight);
			base.Controls.Add(barDockControlBottom);
			base.Controls.Add(barDockControlTop);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Classify_2Form";
			Text = "图符对应管理";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new System.EventHandler(Classify_1Form_Load);
			((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
			groupControl1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)treeList1).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
			groupControl2.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)xtraTabControl1).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControl3).EndInit();
			groupControl3.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)groupControl5).EndInit();
			groupControl5.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)xtraTabControl2).EndInit();
			xtraTabControl2.ResumeLayout(performLayout: false);
			xtraTabPage1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)gridControl2).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
			xtraTabPage2.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)gridControl3).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView3).EndInit();
			xtraTabPage3.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)gridControl4).EndInit();
			((System.ComponentModel.ISupportInitialize)gridView4).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControl4).EndInit();
			groupControl4.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)popupMenu1).EndInit();
			((System.ComponentModel.ISupportInitialize)popupMenu2).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

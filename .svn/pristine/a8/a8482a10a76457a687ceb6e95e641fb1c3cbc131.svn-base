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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Classify_2Form));
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
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
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.popupMenu2 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.treeList1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(200, 738);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "管辖范围";
            // 
            // treeList1
            // 
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(2, 31);
            this.treeList1.Name = "treeList1";
            this.treeList1.OptionsBehavior.Editable = false;
            this.treeList1.Size = new System.Drawing.Size(196, 705);
            this.treeList1.TabIndex = 0;
            this.treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
            this.treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(this.treeList1_FocusedNodeChanged);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(200, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 738);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(880, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(10, 738);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.xtraTabControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupControl2.Location = new System.Drawing.Point(890, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(424, 738);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "图符库";
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(2, 31);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.Size = new System.Drawing.Size(420, 705);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.groupControl3.Appearance.Options.UseBackColor = true;
            this.groupControl3.Controls.Add(this.groupControl5);
            this.groupControl3.Controls.Add(this.groupControl4);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl3.Location = new System.Drawing.Point(210, 0);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(670, 738);
            this.groupControl3.TabIndex = 7;
            this.groupControl3.Text = "图符列表";
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.xtraTabControl2);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl5.Location = new System.Drawing.Point(148, 31);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(520, 705);
            this.groupControl5.TabIndex = 1;
            this.groupControl5.Text = "图符属性";
            // 
            // xtraTabControl2
            // 
            this.xtraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl2.Location = new System.Drawing.Point(2, 31);
            this.xtraTabControl2.Name = "xtraTabControl2";
            this.xtraTabControl2.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl2.Size = new System.Drawing.Size(516, 672);
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
            this.xtraTabPage1.Size = new System.Drawing.Size(508, 630);
            this.xtraTabPage1.Text = "固定属性";
            // 
            // gridControl2
            // 
            this.gridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl2.Location = new System.Drawing.Point(0, 0);
            this.gridControl2.MainView = this.gridView2;
            this.gridControl2.Name = "gridControl2";
            this.gridControl2.Size = new System.Drawing.Size(508, 630);
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
            this.xtraTabPage2.Size = new System.Drawing.Size(508, 630);
            this.xtraTabPage2.Text = "基本属性";
            // 
            // gridControl3
            // 
            this.gridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl3.Location = new System.Drawing.Point(0, 0);
            this.gridControl3.MainView = this.gridView3;
            this.gridControl3.Name = "gridControl3";
            this.gridControl3.Size = new System.Drawing.Size(508, 630);
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
            this.xtraTabPage3.Size = new System.Drawing.Size(508, 630);
            this.xtraTabPage3.Text = "扩展属性";
            // 
            // gridControl4
            // 
            this.gridControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl4.Location = new System.Drawing.Point(0, 0);
            this.gridControl4.MainView = this.gridView4;
            this.gridControl4.Name = "gridControl4";
            this.gridControl4.Size = new System.Drawing.Size(508, 630);
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
            this.groupControl4.Size = new System.Drawing.Size(146, 705);
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(142, 672);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
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
            this.barButtonItem2});
            this.barManager1.MaxItemId = 2;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1314, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 738);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1314, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 738);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1314, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 738);
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
            // Classify_2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1314, 738);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Classify_2Form";
            this.Text = "图符对应管理";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Classify_1Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}

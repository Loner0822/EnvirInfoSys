using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using MapHelper;
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
	public class MapSetForm : XtraForm
	{
		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private string AccessPath = AppDomain.CurrentDomain.BaseDirectory + "data\\ENVIR_H0001Z000E00.mdb";

		private string IniFilePath = AppDomain.CurrentDomain.BaseDirectory + "parameter.ini";

		private string[] folds = null;

		public string unitid = "";

		public string MapPath = "";

		private string[] GL_PGUID;

		private List<GL_Node> GL_List;

		private Dictionary<string, string> GL_NAME;

		private Dictionary<string, string> GL_JDCODE;

		private Dictionary<string, string> GL_UPGUID;

		private Dictionary<string, string> GL_MAP;

		private Dictionary<string, string> GL_NAME_PGUID;

		private Dictionary<string, Dictionary<string, Polygon>> GL_POLY;

		private Dictionary<string, object> borderDic = null;

		private Dictionary<string, List<double[]>> borList = new Dictionary<string, List<double[]>>();

		private LineData borData = null;

		private bool Before_ShowMap = false;

		private int cur_level = 0;

		private int now_level = 0;

		private string levelguid = "";

		private string map_type = "";

		private int MapX = 0;

		private int MapY = 0;

		private string border_guid = "";

		private IContainer components = null;

		private BarManager barManager1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private GroupControl groupControl2;

		private PanelControl panelControl1;

		private FlowLayoutPanel flowLayoutPanel1;

		private PanelControl panelControl2;

		private Label label1;

		private Splitter splitter1;

		private GroupControl groupControl1;

		private TreeList treeList1;

		private BarButtonItem barButtonItem1;

		private PopupMenu popupMenu1;

		private BarButtonItem barButtonItem2;

		private XtraOpenFileDialog xtraOpenFileDialog1;

		private MapHelper.MapHelper mapHelper1;

		private BarButtonItem barButtonItem3;

		private BarButtonItem barButtonItem4;

		private BarButtonItem barButtonItem5;

		private PopupMenu popupMenu2;

		public MapSetForm()
		{
			InitializeComponent();
		}

		private void MapSetForm_Load(object sender, EventArgs e)
		{
			borData = new LineData();
			borData.Get_NewLine();
			folds = Get_Map_List();
			if (folds != null)
			{
				Load_Unit_Level();
				FileReader.inip = new IniOperator(IniFilePath);
				string s = FileReader.inip.ReadString("mapproperties", "centerlat", "");
				string s2 = FileReader.inip.ReadString("mapproperties", "centerlng", "");
				mapHelper1.centerlat = double.Parse(s);
				mapHelper1.centerlng = double.Parse(s2);
				string sql = "select LAT, LNG from ORGCENTERDATA where ISDELETE = 0 and UNITEID = '" + unitid + "'";
				DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					mapHelper1.centerlat = double.Parse(dataTable.Rows[0]["LAT"].ToString());
					mapHelper1.centerlng = double.Parse(dataTable.Rows[0]["LNG"].ToString());
				}
				mapHelper1.webpath = WorkPath + "googlemap";
				mapHelper1.roadmappath = MapPath + "\\roadmap";
				mapHelper1.satellitemappath = MapPath + "\\satellite_en";
				mapHelper1.iconspath = WorkPath + "PNGICONFOLDER";
				mapHelper1.maparr = folds;
				Load_Border(unitid);
				PictureBox pictureBox = new PictureBox();
				ToolTip toolTip = new ToolTip();
				toolTip.SetToolTip(pictureBox, "指针");
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox.BorderStyle = BorderStyle.Fixed3D;
				pictureBox.Width = 32;
				pictureBox.Height = 32;
				pictureBox.Click += Vector_Click;
				pictureBox.Name = "指针";
				FileStream fileStream = new FileStream(WorkPath + "icon\\指针.png", FileMode.Open, FileAccess.Read);
				pictureBox.Image = Image.FromStream(fileStream);
				flowLayoutPanel1.Controls.Add(pictureBox);
				fileStream.Close();
				fileStream.Dispose();
				pictureBox = new PictureBox();
				toolTip = new ToolTip();
				toolTip.SetToolTip(pictureBox, "画线");
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox.Width = 32;
				pictureBox.Height = 32;
				pictureBox.Click += Line_Click;
				pictureBox.Name = "画线";
				fileStream = new FileStream(WorkPath + "icon\\画线.png", FileMode.Open, FileAccess.Read);
				pictureBox.Image = Image.FromStream(fileStream);
				flowLayoutPanel1.Controls.Add(pictureBox);
				fileStream.Close();
				fileStream.Dispose();
				pictureBox = new PictureBox();
				toolTip = new ToolTip();
				toolTip.SetToolTip(pictureBox, "画多边形");
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox.Width = 32;
				pictureBox.Height = 32;
				pictureBox.Click += Polygon_Click;
				pictureBox.Name = "画多边形";
				fileStream = new FileStream(WorkPath + "icon\\多边形.png", FileMode.Open, FileAccess.Read);
				pictureBox.Image = Image.FromStream(fileStream);
				flowLayoutPanel1.Controls.Add(pictureBox);
				fileStream.Close();
				fileStream.Dispose();
				pictureBox = new PictureBox();
				toolTip = new ToolTip();
				toolTip.SetToolTip(pictureBox, "中心点");
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				pictureBox.Width = 32;
				pictureBox.Height = 32;
				pictureBox.MouseDown += Center_MouseDown;
				pictureBox.Click += Center_Click;
				pictureBox.Name = "中心点";
				fileStream = new FileStream(WorkPath + "icon\\中心点.png", FileMode.Open, FileAccess.Read);
				pictureBox.Image = Image.FromStream(fileStream);
				flowLayoutPanel1.Controls.Add(pictureBox);
				fileStream.Close();
				fileStream.Dispose();
				mapHelper1.ShowMap(cur_level, cur_level.ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
			}
		}

		private void Polygon_Click(object sender, EventArgs e)
		{
		}

		private void Line_Click(object sender, EventArgs e)
		{
		}

		private void Vector_Click(object sender, EventArgs e)
		{
		}

		private void Center_MouseDown(object sender, MouseEventArgs e)
		{
			PictureBox pictureBox = (PictureBox)sender;
			pictureBox.BorderStyle = BorderStyle.Fixed3D;
		}

		private void Center_Click(object sender, EventArgs e)
		{
			double[] mapCenter = mapHelper1.GetMapCenter();
			TreeListNode focusedNode = treeList1.FocusedNode;
			string text = focusedNode.GetValue("pguid").ToString();
			string sql = "select PGUID from ORGCENTERDATA where ISDELETE = 0 and PGUID = '" + text + "'";
			DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				sql = "update ORGCENTERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LAT = '" + mapCenter[0].ToString() + "', LNG = '" + mapCenter[1].ToString() + "' where ISDELETE = 0 and PGUID = '" + text + "'";
				FileReader.line_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
			else
			{
				sql = "insert into ORGCENTERDATA (PGUID, S_UDTIME, UNITEID, LAT, LNG) values('" + text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + text + "', '" + mapCenter[0].ToString() + "', '" + mapCenter[1].ToString() + "')";
				FileReader.line_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
			PictureBox pictureBox = (PictureBox)sender;
			pictureBox.BorderStyle = BorderStyle.None;
			XtraMessageBox.Show("中心点设置成功!");
		}

		private string[] Get_Map_List()
		{
			string[] array = null;
			if (!Directory.Exists(MapPath + "//roadmap"))
			{
				XtraMessageBox.Show("未导入地图文件!请在数据管理菜单导入地图");
				return null;
			}
			array = Directory.GetDirectories(MapPath + "//roadmap");
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i].LastIndexOf("\\");
				array[i] = array[i].Substring(num + 1);
			}
			return array;
		}

		private void Load_Unit_Level()
		{
			GL_NAME = new Dictionary<string, string>();
			GL_JDCODE = new Dictionary<string, string>();
			GL_UPGUID = new Dictionary<string, string>();
			GL_MAP = new Dictionary<string, string>();
			GL_NAME_PGUID = new Dictionary<string, string>();
			GL_POLY = new Dictionary<string, Dictionary<string, Polygon>>();
			FileReader.once_ahp = new AccessHelper(WorkPath + "data\\ZSK_H0001Z000K01.mdb");
			string sql = "select PGUID, JDNAME, JDCODE, UPGUID from ZSK_OBJECT_H0001Z000K01 where ISDELETE = 0 order by LEVELNUM, SHOWINDEX";
			DataTable dataTable = FileReader.once_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			GL_PGUID = new string[dataTable.Rows.Count];
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text = dataTable.Rows[i]["PGUID"].ToString();
				GL_PGUID[i] = text;
				GL_NAME[text] = dataTable.Rows[i]["JDNAME"].ToString();
				GL_JDCODE[text] = dataTable.Rows[i]["JDCODE"].ToString();
				GL_NAME_PGUID[dataTable.Rows[i]["JDNAME"].ToString()] = text;
			}
			FileReader.once_ahp.CloseConn();
			FileReader.once_ahp = new AccessHelper(WorkPath + "data\\ENVIRDYDATA_H0001Z000E00.mdb");
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text = dataTable.Rows[i]["PGUID"].ToString();
				sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
				DataTable dataTable2 = FileReader.once_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable2.Rows.Count > 0)
				{
					GL_MAP.Add(text, dataTable2.Rows[0]["MAPLEVEL"].ToString());
				}
				else
				{
					GL_MAP.Add(text, string.Empty);
				}
			}
			FileReader.once_ahp.CloseConn();
			GL_List = new List<GL_Node>();
			treeList1.Nodes.Clear();
			treeList1.Appearance.FocusedCell.BackColor = Color.SteelBlue;
			treeList1.KeyFieldName = "pguid";
			treeList1.ParentFieldName = "upguid";
			FileReader.once_ahp = new AccessHelper(WorkPath + "data\\PersonMange.mdb");
			sql = "select PGUID, UPPGUID, ORGNAME, ULEVEL from RG_单位注册 where ISDELETE = 0 and PGUID = '" + unitid + "'";
			dataTable = FileReader.once_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GL_Node gL_Node = new GL_Node();
				gL_Node.pguid = dataTable.Rows[i]["PGUID"].ToString();
				gL_Node.upguid = dataTable.Rows[i]["UPPGUID"].ToString();
				GL_UPGUID[gL_Node.pguid] = dataTable.Rows[i]["UPPGUID"].ToString();
				gL_Node.Name = dataTable.Rows[i]["ORGNAME"].ToString();
				gL_Node.level = dataTable.Rows[i]["ULEVEL"].ToString();
				GL_List.Add(gL_Node);
				Add_Unit_Node(gL_Node);
			}
			treeList1.DataSource = GL_List;
			treeList1.HorzScrollVisibility = ScrollVisibility.Auto;
			treeList1.Columns[1].Visible = false;
			treeList1.Columns[2].Visible = false;
			treeList1.Columns[3].Visible = false;
			treeList1.Columns[4].Visible = false;
			treeList1.Columns[5].Visible = false;
			treeList1.ExpandAll();
			FileReader.once_ahp.CloseConn();
		}

		private void Add_Unit_Node(GL_Node pa)
		{
			string sql = "select PGUID, UPPGUID, ORGNAME, ULEVEL from RG_单位注册 where ISDELETE = 0 and UPPGUID = '" + pa.pguid + "'";
			DataTable dataTable = FileReader.once_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				GL_Node gL_Node = new GL_Node();
				gL_Node.pguid = dataTable.Rows[i]["PGUID"].ToString();
				gL_Node.upguid = dataTable.Rows[i]["UPPGUID"].ToString();
				GL_UPGUID[gL_Node.pguid] = dataTable.Rows[i]["UPPGUID"].ToString();
				gL_Node.Name = dataTable.Rows[i]["ORGNAME"].ToString();
				gL_Node.level = dataTable.Rows[i]["ULEVEL"].ToString();
				GL_List.Add(gL_Node);
				Add_Unit_Node(gL_Node);
			}
		}

		private void Load_Border(string u_guid)
		{
			borList = new Dictionary<string, List<double[]>>();
			borderDic = new Dictionary<string, object>();
			LineData lineData = new LineData();
			lineData.Load_Line("边界线");
			if (lineData.Type != null)
			{
				borData = lineData;
			}
			borderDic.Add("type", borData.Type);
			borderDic.Add("width", borData.Width);
			borderDic.Add("color", borData.Color);
			borderDic.Add("opacity", borData.Opacity);
			string sql = "select LAT, LNG, BORDERGUID from BORDERDATA where ISDELETE = 0 and UNITID = '" + u_guid + "' order by SHOWINDEX";
			DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string key = dataTable.Rows[i]["BORDERGUID"].ToString();
				if (borList.ContainsKey(key))
				{
					borList[key].Add(new double[2]
					{
						double.Parse(dataTable.Rows[i]["LAT"].ToString()),
						double.Parse(dataTable.Rows[i]["LNG"].ToString())
					});
					continue;
				}
				borList[key] = new List<double[]>();
				borList[key].Add(new double[2]
				{
					double.Parse(dataTable.Rows[i]["LAT"].ToString()),
					double.Parse(dataTable.Rows[i]["LNG"].ToString())
				});
			}
			if (GL_UPGUID.ContainsKey(u_guid))
			{
			}
		}

		private Dictionary<string, List<double[]>> Get_Border_Line(string pguid)
		{
			Dictionary<string, List<double[]>> dictionary = new Dictionary<string, List<double[]>>();
			string sql = "select LAT, LNG, BORDERGUID from BORDERDATA where ISDELETE = 0 and UNITID = '" + pguid + "' order by SHOWINDEX";
			DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string key = dataTable.Rows[i]["BORDERGUID"].ToString();
				if (dictionary.ContainsKey(key))
				{
					dictionary[key].Add(new double[2]
					{
						double.Parse(dataTable.Rows[i]["LAT"].ToString()),
						double.Parse(dataTable.Rows[i]["LNG"].ToString())
					});
					continue;
				}
				dictionary[key] = new List<double[]>();
				dictionary[key].Add(new double[2]
				{
					double.Parse(dataTable.Rows[i]["LAT"].ToString()),
					double.Parse(dataTable.Rows[i]["LNG"].ToString())
				});
			}
			if (!GL_POLY.ContainsKey(pguid))
			{
				return dictionary;
			}
			foreach (KeyValuePair<string, List<double[]>> item in dictionary)
			{
				GL_POLY[pguid][item.Key] = new Polygon(item.Value);
			}
			return dictionary;
		}

		private void DrawBorder()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string text = "fb66d40b-50fa-4d88-8156-c590328004cb";
			string text2 = "e62844cb-2839-4b49-853a-250e11ec1901";
			dictionary["color"] = borData.Color;
			dictionary["weight"] = 0;
			dictionary["fillColor"] = "#C0C0C0";
			dictionary["fillOpacity"] = 0.5;
			if (borList.Count > 1)
			{
				dictionary["fillColor"] = "#CCFF33";
				foreach (KeyValuePair<string, List<double[]>> bor in borList)
				{
					dictionary["points"] = bor.Value;
				}
			}
			else
			{
				foreach (KeyValuePair<string, List<double[]>> bor2 in borList)
				{
					dictionary["points"] = bor2.Value;
				}
			}
			TreeListNode focusedNode = treeList1.FocusedNode;
			string text3 = focusedNode.GetValue("pguid").ToString();
			Dictionary<string, List<double[]>> dictionary2 = Get_Border_Line(text3);
			foreach (KeyValuePair<string, List<double[]>> item in dictionary2)
			{
				Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
				dictionary3["type"] = borData.Type;
				dictionary3["width"] = borData.Width;
				dictionary3["color"] = borData.Color;
				dictionary3["opacity"] = borData.Opacity;
				dictionary3["path"] = item.Value;
				mapHelper1.DrawBorder(text3, dictionary3);
			}
		}

		private void EraseBorder()
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			mapHelper1.deleteMarker(focusedNode["pguid"].ToString());
			foreach (TreeListNode node in focusedNode.Nodes)
			{
				mapHelper1.deleteMarker(node["pguid"].ToString());
			}
		}

		private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			borList = new Dictionary<string, List<double[]>>();
			if (e.Node != null)
			{
				levelguid = GL_NAME_PGUID[e.Node.GetValue("level").ToString()];
				string[] array = GL_MAP[levelguid].Split(',');
				string sql = "select MAPLEVEL from ENVIRMAPDY_H0001Z000E00 where ISDELETE = 0 and UNITID = '" + unitid + "' and PGUID = '" + focusedNode["pguid"].ToString() + "'";
				DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					array = dataTable.Rows[0]["MAPLEVEL"].ToString().Split(',');
				}
				if (array[0] != string.Empty)
				{
					cur_level = int.Parse(array[0]);
				}
				else
				{
					cur_level = int.Parse(folds[0]);
				}
				label1.Text = "当前级别：" + GL_NAME[levelguid];
				Load_Border(e.Node.GetValue("pguid").ToString());
				if (!GL_POLY.ContainsKey(e.Node.GetValue("pguid").ToString()))
				{
					GL_POLY[e.Node.GetValue("pguid").ToString()] = new Dictionary<string, Polygon>();
				}
				foreach (KeyValuePair<string, List<double[]>> bor in borList)
				{
					GL_POLY[e.Node.GetValue("pguid").ToString()][bor.Key] = new Polygon(bor.Value);
				}
				bool flag = false;
				sql = "select MARKELAT, MARKELNG from ENVIRICONDATA_H0001Z000E00 where ISDELETE = 0 and MAKRENAME like '%" + e.Node.GetValue("Name").ToString() + "%' and UNITEID = '" + unitid + "'";
				dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					mapHelper1.centerlat = double.Parse(dataTable.Rows[0]["MARKELAT"].ToString());
					mapHelper1.centerlng = double.Parse(dataTable.Rows[0]["MARKELNG"].ToString());
					flag = true;
				}
				sql = "select LAT, LNG from ORGCENTERDATA where ISDELETE = 0 and UNITEID = '" + e.Node.GetValue("pguid").ToString() + "'";
				dataTable = FileReader.line_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					mapHelper1.centerlat = double.Parse(dataTable.Rows[0]["LAT"].ToString());
					mapHelper1.centerlng = double.Parse(dataTable.Rows[0]["LNG"].ToString());
					flag = true;
				}
				if (!flag && Before_ShowMap)
				{
					double[] mapCenter = mapHelper1.GetMapCenter();
					mapHelper1.centerlat = mapCenter[0];
					mapHelper1.centerlng = mapCenter[1];
				}
				if (!Before_ShowMap)
				{
					mapHelper1.ShowMap(cur_level, cur_level.ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
				}
				else
				{
					mapHelper1.setMapLevel(cur_level, cur_level.ToString());
					mapHelper1.SetMapCenter(mapHelper1.centerlat, mapHelper1.centerlng);
					EraseBorder();
					DrawBorder();
				}
				Before_ShowMap = true;
			}
		}

		private void treeList1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				TreeListNode nodeAt = treeList1.GetNodeAt(e.X, e.Y);
				if (nodeAt != null)
				{
					popupMenu1.ShowPopup(barManager1, Control.MousePosition);
					MapX = Control.MousePosition.X;
					MapY = Control.MousePosition.Y;
					treeList1.FocusedNode = nodeAt;
				}
			}
		}

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			MapLevelForm mapLevelForm = new MapLevelForm();
			TreeListNode focusedNode = treeList1.FocusedNode;
			levelguid = GL_NAME_PGUID[focusedNode["level"].ToString()];
			mapLevelForm.StartPosition = FormStartPosition.Manual;
			mapLevelForm.Left = MapX;
			mapLevelForm.Top = MapY;
			if (mapLevelForm.Top + mapLevelForm.Height > base.Height)
			{
				mapLevelForm.Top -= mapLevelForm.Height;
			}
			mapLevelForm.nodeid = focusedNode["pguid"].ToString();
			mapLevelForm.unitid = unitid;
			mapLevelForm.mapstring = GL_MAP[levelguid];
			mapLevelForm.MapPath = MapPath;
			mapLevelForm.ShowDialog();
			if (focusedNode.ParentNode != null)
			{
				treeList1.FocusedNode = focusedNode.ParentNode;
			}
			else if (focusedNode.Nodes[0] != null)
			{
				treeList1.FocusedNode = focusedNode.Nodes[0];
			}
			treeList1.FocusedNode = focusedNode;
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
		}

		private void mapHelper1_MapTypeChanged(string mapType)
		{
			map_type = mapType;
		}

		private void mapHelper1_LevelChanged(int lastLevel, int currLevel, string showLevel)
		{
			now_level = currLevel;
			DrawBorder();
		}

		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string fileName = xtraOpenFileDialog1.FileName;
				string[] array = File.ReadAllLines(fileName);
				string text = Guid.NewGuid().ToString("B");
				borList[text] = new List<double[]>();
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					string[] array3 = text2.Split(' ', ',', ':', '\t', '\r', '\n', ';');
					borList[text].Add(new double[2]
					{
						double.Parse(array3[1]),
						double.Parse(array3[0])
					});
				}
				borderDic["path"] = borList;
				for (int j = 0; j < borList[text].Count; j++)
				{
					string sql = "insert into BORDERDATA (PGUID, S_UDTIME, UNITID, LAT, LNG, SHOWINDEX, BORDERGUID) values('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + focusedNode["pguid"].ToString() + "', '" + borList[text][j][0] + "', '" + borList[text][j][1] + "', " + j.ToString() + ", '" + text + "')";
					FileReader.line_ahp.ExecuteSql(sql, (OleDbParameter[])null);
				}
				string remark = "添加" + focusedNode["Name"].ToString() + "的边界线";
				ComputerInfo.WriteLog("导入边界线", remark);
				mapHelper1.ShowMap(cur_level, cur_level.ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
			}
		}

		private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (XtraMessageBox.Show("是否要删除该条边界线？", "提示", MessageBoxButtons.OKCancel) != DialogResult.Cancel)
			{
				TreeListNode focusedNode = treeList1.FocusedNode;
				string sql = "update BORDERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ISDELETE = 1 where ISDELETE = 0 and UNITID = '" + focusedNode["pguid"].ToString() + "' and BORDERGUID = '" + border_guid + "'";
				FileReader.line_ahp.ExecuteSql(sql, (OleDbParameter[])null);
				string remark = "删除" + focusedNode["Name"].ToString() + "的边界线";
				ComputerInfo.WriteLog("导入边界线", remark);
				mapHelper1.ShowMap(cur_level, cur_level.ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
				borList.Remove(border_guid);
			}
		}

		private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string sql = "update BORDERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ISDELETE = 1 where ISDELETE = 0 and UNITID = '" + focusedNode["pguid"].ToString() + "' and BORDERGUID = '" + border_guid + "'";
				FileReader.line_ahp.ExecuteSql(sql, (OleDbParameter[])null);
				string fileName = xtraOpenFileDialog1.FileName;
				string[] array = File.ReadAllLines(fileName);
				string text = Guid.NewGuid().ToString("B");
				borList[text] = new List<double[]>();
				string[] array2 = array;
				foreach (string text2 in array2)
				{
					string[] array3 = text2.Split(' ', ',', ':', '\t', '\r', '\n', ';');
					borList[text].Add(new double[2]
					{
						double.Parse(array3[1]),
						double.Parse(array3[0])
					});
				}
				borderDic["path"] = borList;
				for (int j = 0; j < borList[text].Count; j++)
				{
					sql = "insert into BORDERDATA (PGUID, S_UDTIME, UNITID, LAT, LNG, SHOWINDEX, BORDERGUID) values('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + focusedNode["pguid"].ToString() + "', '" + borList[text][j][0] + "', '" + borList[text][j][1] + "', '" + j.ToString() + "', '" + text + "')";
					FileReader.line_ahp.ExecuteSql(sql, (OleDbParameter[])null);
				}
				string remark = "更新" + focusedNode["Name"].ToString() + "的边界线";
				ComputerInfo.WriteLog("导入边界线", remark);
				mapHelper1.ShowMap(cur_level, cur_level.ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
			}
		}

		private void mapHelper1_MapRightClick(bool canedit, double lat, double lng, int x, int y)
		{
			if (Check_InBorder(new dPoint(lat, lng)))
			{
				barButtonItem4.Visibility = BarItemVisibility.Always;
				barButtonItem5.Visibility = BarItemVisibility.Always;
			}
			else
			{
				barButtonItem4.Visibility = BarItemVisibility.Never;
				barButtonItem5.Visibility = BarItemVisibility.Never;
			}
			popupMenu2.ShowPopup(barManager1, Control.MousePosition);
		}

		private bool Check_InBorder(dPoint p)
		{
			Polygon polygon = null;
			foreach (KeyValuePair<string, List<double[]>> bor in borList)
			{
				polygon = new Polygon(bor.Value);
				if (polygon.PointInPolygon(p))
				{
					border_guid = bor.Key;
					return true;
				}
			}
			border_guid = "";
			return false;
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.MapSetForm));
			barManager1 = new DevExpress.XtraBars.BarManager(components);
			barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
			treeList1 = new DevExpress.XtraTreeList.TreeList();
			groupControl1 = new DevExpress.XtraEditors.GroupControl();
			splitter1 = new System.Windows.Forms.Splitter();
			panelControl1 = new DevExpress.XtraEditors.PanelControl();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			panelControl2 = new DevExpress.XtraEditors.PanelControl();
			label1 = new System.Windows.Forms.Label();
			groupControl2 = new DevExpress.XtraEditors.GroupControl();
			mapHelper1 = new MapHelper.MapHelper();
			popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
			xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(components);
			popupMenu2 = new DevExpress.XtraBars.PopupMenu(components);
			((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)treeList1).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
			groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)panelControl2).BeginInit();
			panelControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
			groupControl2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)popupMenu1).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupMenu2).BeginInit();
			SuspendLayout();
			barManager1.DockControls.Add(barDockControlTop);
			barManager1.DockControls.Add(barDockControlBottom);
			barManager1.DockControls.Add(barDockControlLeft);
			barManager1.DockControls.Add(barDockControlRight);
			barManager1.Form = this;
			barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[5]
			{
				barButtonItem1,
				barButtonItem2,
				barButtonItem3,
				barButtonItem4,
				barButtonItem5
			});
			barManager1.MaxItemId = 5;
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			barDockControlTop.Location = new System.Drawing.Point(0, 0);
			barDockControlTop.Manager = barManager1;
			barDockControlTop.Size = new System.Drawing.Size(1297, 0);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			barDockControlBottom.Location = new System.Drawing.Point(0, 696);
			barDockControlBottom.Manager = barManager1;
			barDockControlBottom.Size = new System.Drawing.Size(1297, 0);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			barDockControlLeft.Manager = barManager1;
			barDockControlLeft.Size = new System.Drawing.Size(0, 696);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			barDockControlRight.Location = new System.Drawing.Point(1297, 0);
			barDockControlRight.Manager = barManager1;
			barDockControlRight.Size = new System.Drawing.Size(0, 696);
			barButtonItem1.Caption = "设置地图对应级别";
			barButtonItem1.Id = 0;
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
			barButtonItem2.Caption = "导入单位边界线";
			barButtonItem2.Id = 1;
			barButtonItem2.Name = "barButtonItem2";
			barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem2_ItemClick);
			barButtonItem3.Caption = "添加单位边界线";
			barButtonItem3.Id = 2;
			barButtonItem3.Name = "barButtonItem3";
			barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick);
			barButtonItem4.Caption = "删除单位边界线";
			barButtonItem4.Id = 3;
			barButtonItem4.Name = "barButtonItem4";
			barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem4_ItemClick);
			barButtonItem5.Caption = "更新单位边界线";
			barButtonItem5.Id = 4;
			barButtonItem5.Name = "barButtonItem5";
			barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem5_ItemClick);
			treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
			treeList1.Location = new System.Drawing.Point(2, 31);
			treeList1.Name = "treeList1";
			treeList1.OptionsBehavior.Editable = false;
			treeList1.Size = new System.Drawing.Size(196, 663);
			treeList1.TabIndex = 0;
			treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
			treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(treeList1_FocusedNodeChanged);
			treeList1.MouseDown += new System.Windows.Forms.MouseEventHandler(treeList1_MouseDown);
			groupControl1.Controls.Add(treeList1);
			groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
			groupControl1.Location = new System.Drawing.Point(0, 0);
			groupControl1.Name = "groupControl1";
			groupControl1.Size = new System.Drawing.Size(200, 696);
			groupControl1.TabIndex = 0;
			groupControl1.Text = "管辖范围";
			splitter1.Location = new System.Drawing.Point(200, 0);
			splitter1.Name = "splitter1";
			splitter1.Size = new System.Drawing.Size(10, 696);
			splitter1.TabIndex = 2;
			splitter1.TabStop = false;
			panelControl1.Controls.Add(flowLayoutPanel1);
			panelControl1.Controls.Add(panelControl2);
			panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
			panelControl1.Location = new System.Drawing.Point(2, 31);
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new System.Drawing.Size(1083, 63);
			panelControl1.TabIndex = 1;
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			flowLayoutPanel1.Location = new System.Drawing.Point(2, 2);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(885, 59);
			flowLayoutPanel1.TabIndex = 3;
			panelControl2.Controls.Add(label1);
			panelControl2.Dock = System.Windows.Forms.DockStyle.Right;
			panelControl2.Location = new System.Drawing.Point(887, 2);
			panelControl2.Name = "panelControl2";
			panelControl2.Size = new System.Drawing.Size(194, 59);
			panelControl2.TabIndex = 2;
			label1.Dock = System.Windows.Forms.DockStyle.Fill;
			label1.Location = new System.Drawing.Point(2, 2);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(190, 55);
			label1.TabIndex = 2;
			label1.Text = "当前级别：";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			groupControl2.Controls.Add(mapHelper1);
			groupControl2.Controls.Add(panelControl1);
			groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControl2.Location = new System.Drawing.Point(210, 0);
			groupControl2.Name = "groupControl2";
			groupControl2.Size = new System.Drawing.Size(1087, 696);
			groupControl2.TabIndex = 3;
			groupControl2.Text = "地图显示";
			mapHelper1.BackColor = System.Drawing.Color.Black;
			mapHelper1.centerlat = 0.0;
			mapHelper1.centerlng = 0.0;
			mapHelper1.Dock = System.Windows.Forms.DockStyle.Fill;
			mapHelper1.iconspath = null;
			mapHelper1.Location = new System.Drawing.Point(2, 94);
			mapHelper1.maparr = null;
			mapHelper1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
			mapHelper1.Name = "mapHelper1";
			mapHelper1.roadmappath = null;
			mapHelper1.satellitemappath = null;
			mapHelper1.Size = new System.Drawing.Size(1083, 600);
			mapHelper1.TabIndex = 3;
			mapHelper1.webpath = null;
			mapHelper1.MapRightClick += new MapHelper.MapHelper.DlMapRightClick(mapHelper1_MapRightClick);
			mapHelper1.LevelChanged += new MapHelper.MapHelper.DlLevelChanged(mapHelper1_LevelChanged);
			mapHelper1.MapTypeChanged += new MapHelper.MapHelper.DlMapTypeChanged(mapHelper1_MapTypeChanged);
			popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[1]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem1)
			});
			popupMenu1.Manager = barManager1;
			popupMenu1.Name = "popupMenu1";
			xtraOpenFileDialog1.FileName = null;
			xtraOpenFileDialog1.Filter = "文本文件|*.txt";
			popupMenu2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[3]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem3),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem4),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem5)
			});
			popupMenu2.Manager = barManager1;
			popupMenu2.Name = "popupMenu2";
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(1297, 696);
			base.Controls.Add(groupControl2);
			base.Controls.Add(splitter1);
			base.Controls.Add(groupControl1);
			base.Controls.Add(barDockControlLeft);
			base.Controls.Add(barDockControlRight);
			base.Controls.Add(barDockControlBottom);
			base.Controls.Add(barDockControlTop);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "MapSetForm";
			Text = "地图设置";
			base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			base.Load += new System.EventHandler(MapSetForm_Load);
			((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)treeList1).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
			groupControl1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)panelControl2).EndInit();
			panelControl2.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
			groupControl2.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)popupMenu1).EndInit();
			((System.ComponentModel.ISupportInitialize)popupMenu2).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraBars;

namespace PublishSys
{
    public partial class MapSetForm : XtraForm
    {
        private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        private string AccessPath = AppDomain.CurrentDomain.BaseDirectory + "Publish\\data\\ENVIR_H0001Z000E00.mdb";
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
        private string border_guid = "";

        public MapSetForm()
        {
            InitializeComponent();
        }

        private void MapSetForm_Load(object sender, EventArgs e)
        {
            FileReader.often_ahp = new AccessHelper(AccessPath);
            FileReader.line_ahp = new AccessHelper(WorkPath + "Publish\\data\\经纬度注册.mdb");
            FileReader.inip = new IniOperator(WorkPath + "RegInfo.ini");
            //unitid = FileReader.inip.ReadString("Public", "UnitID", "-1");
            //MapPath = FileReader.inip.ReadString("Individuation", "mappath", "");
            //MapPath = MapPath.Replace("\0", "");
            //MapPath = "http://192.168.0.109:190/downfile/googlemaps";
            borData = new LineData();
            borData.Get_NewLine();
            map_type = "g_map";
            folds = Get_Map_List();
            if (folds != null)
            {
                Load_Unit_Level();

                string sql = "select LAT, LNG from ORGCENTERDATA where ISDELETE = 0 and UNITEID = '" + unitid + "'";
                DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql);
                if (dataTable.Rows.Count > 0)
                {
                    mapHelper1.centerlat = double.Parse(dataTable.Rows[0]["LAT"].ToString());
                    mapHelper1.centerlng = double.Parse(dataTable.Rows[0]["LNG"].ToString());
                }
                mapHelper1.webpath = WorkPath + "Publish\\googlemap";
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
                FileStream fileStream = new FileStream(WorkPath + "Publish\\icon\\指针.png", FileMode.Open, FileAccess.Read);
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
                fileStream = new FileStream(WorkPath + "Publish\\icon\\画线.png", FileMode.Open, FileAccess.Read);
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
                fileStream = new FileStream(WorkPath + "Publish\\icon\\多边形.png", FileMode.Open, FileAccess.Read);
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
                fileStream = new FileStream(WorkPath + "Publish\\icon\\中心点.png", FileMode.Open, FileAccess.Read);
                pictureBox.Image = Image.FromStream(fileStream);
                flowLayoutPanel1.Controls.Add(pictureBox);
                fileStream.Close();
                fileStream.Dispose();
                mapHelper1.ShowMap(cur_level, cur_level.ToString(), false, map_type, null, null, null, 1.0, 400);
            }
        }

        private string[] Get_Map_List()
        {
            string[] array = new string[] { "11", "12", "13", "14", "15", "16", "17", "18", "19" };
            /*if (!Directory.Exists(MapPath + "//roadmap"))
            {
                XtraMessageBox.Show("未导入地图文件!请在数据管理菜单导入地图");
                return null;
            }
            array = Directory.GetDirectories(MapPath + "//roadmap");
            for (int i = 0; i < array.Length; i++)
            {
                int num = array[i].LastIndexOf("\\");
                array[i] = array[i].Substring(num + 1);
            }*/
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
            FileReader.once_ahp = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000K01.mdb");
            string sql = "select PGUID, JDNAME, JDCODE, UPGUID from ZSK_OBJECT_H0001Z000K01 where ISDELETE = 0 order by LEVELNUM, SHOWINDEX";
            DataTable dataTable = FileReader.once_ahp.ExecuteDataTable(sql);
            GL_PGUID = new string[dataTable.Rows.Count];
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string pguid = dataTable.Rows[i]["PGUID"].ToString();
                GL_PGUID[i] = pguid;
                GL_NAME[pguid] = dataTable.Rows[i]["JDNAME"].ToString();
                GL_JDCODE[pguid] = dataTable.Rows[i]["JDCODE"].ToString();
                GL_NAME_PGUID[dataTable.Rows[i]["JDNAME"].ToString()] = pguid;
            }
            FileReader.once_ahp.CloseConn();
            FileReader.once_ahp = new AccessHelper(WorkPath + "Publish\\data\\ENVIRDYDATA_H0001Z000E00.mdb");
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string pguid = dataTable.Rows[i]["PGUID"].ToString();
                sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and LEVELGUID = '" + pguid + "' and UNITEID = '" + unitid + "'";
                DataTable dataTable2 = FileReader.once_ahp.ExecuteDataTable(sql);
                if (dataTable2.Rows.Count > 0)
                {
                    GL_MAP.Add(pguid, dataTable2.Rows[0]["MAPLEVEL"].ToString());
                }
                else
                {
                    GL_MAP.Add(pguid, string.Empty);
                }
            }
            FileReader.once_ahp.CloseConn();
            GL_List = new List<GL_Node>();
            treeList1.Nodes.Clear();
            treeList1.Appearance.FocusedCell.BackColor = Color.SteelBlue;
            treeList1.KeyFieldName = "pguid";
            treeList1.ParentFieldName = "upguid";
            FileReader.once_ahp = new AccessHelper(WorkPath + "Publish\\data\\PersonMange.mdb");
            sql = "select PGUID, UPPGUID, ORGNAME, ULEVEL from RG_单位注册 where ISDELETE = 0 and PGUID = '" + unitid + "'";
            dataTable = FileReader.once_ahp.ExecuteDataTable(sql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                GL_Node gL_Node = new GL_Node
                {
                    pguid = dataTable.Rows[i]["PGUID"].ToString(),
                    upguid = dataTable.Rows[i]["UPPGUID"].ToString()
                };
                GL_UPGUID[gL_Node.pguid] = dataTable.Rows[i]["UPPGUID"].ToString();
                gL_Node.Name = dataTable.Rows[i]["ORGNAME"].ToString();
                gL_Node.level = dataTable.Rows[i]["ULEVEL"].ToString();
                GL_List.Add(gL_Node);
                Add_Unit_Node(gL_Node);
            }
            treeList1.DataSource = GL_List;
            treeList1.HorzScrollVisibility = DevExpress.XtraTreeList.ScrollVisibility.Auto;
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
            DataTable dataTable = FileReader.once_ahp.ExecuteDataTable(sql);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                GL_Node gL_Node = new GL_Node
                {
                    pguid = dataTable.Rows[i]["PGUID"].ToString(),
                    upguid = dataTable.Rows[i]["UPPGUID"].ToString()
                };
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
            LineData lineData = new LineData();
            lineData.Load_Line("边界线");
            if (lineData.Type != null)
            {
                borData = lineData;
            }
            borderDic = new Dictionary<string, object>
            {
                { "type", borData.Type },
                { "width", borData.Width },
                { "color", borData.Color },
                { "opacity", borData.Opacity }
            };
            string sql = "select LAT, LNG, BORDERGUID from BORDERDATA where ISDELETE = 0 and UNITID = '" + u_guid + "' order by SHOWINDEX";
            DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql);
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
                borList[key] = new List<double[]>
                {
                    new double[2]
                {
                double.Parse(dataTable.Rows[i]["LAT"].ToString()),
                double.Parse(dataTable.Rows[i]["LNG"].ToString())
                }
                };
            }
            /*if (GL_UPGUID.ContainsKey(u_guid))
            {
                if (dataTable.Rows.Count <= 0)
                {
                    Load_Border(GL_UPGUID[u_guid]);
                }
                else
                {
                    borderDic.Add("path", borList);
                }
            }*/
        }

        private void Vector_Click(object sender, EventArgs e)
        {
            
        }

        private void Line_Click(object sender, EventArgs e)
        {

        }

        private void Polygon_Click(object sender, EventArgs e)
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
            DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                sql = "update ORGCENTERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LAT = '" + mapCenter[0].ToString() + "', LNG = '" + mapCenter[1].ToString() + "' where ISDELETE = 0 and PGUID = '" + text + "'";
                FileReader.line_ahp.ExecuteSql(sql);
            }
            else
            {
                sql = "insert into ORGCENTERDATA (PGUID, S_UDTIME, UNITEID, LAT, LNG) values('" + text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + unitid + "', '" + mapCenter[0].ToString() + "', '" + mapCenter[1].ToString() + "')";
                FileReader.line_ahp.ExecuteSql(sql);
            }
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.BorderStyle = BorderStyle.None;
            XtraMessageBox.Show("中心点设置成功!");
            FileReader.line_ahp.CloseConn();
            FileReader.line_ahp = new AccessHelper(WorkPath + "Publish\\data\\经纬度注册.mdb");
        }

        private void TreeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;
            borList = new Dictionary<string, List<double[]>>();
            if (e.Node != null)
            {
                levelguid = GL_NAME_PGUID[e.Node.GetValue("level").ToString()];
                string[] array = GL_MAP[levelguid].Split(',');
                string sql = "select MAPLEVEL from ENVIRMAPDY_H0001Z000E00 where ISDELETE = 0 and UNITID = '" + unitid + "' and PGUID = '" + focusedNode["pguid"].ToString() + "'";
                DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql);
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
                labelControl1.Text = "当前级别：" + GL_NAME[levelguid];
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
                dataTable = FileReader.often_ahp.ExecuteDataTable(sql);
                if (dataTable.Rows.Count > 0)
                {
                    mapHelper1.centerlat = double.Parse(dataTable.Rows[0]["MARKELAT"].ToString());
                    mapHelper1.centerlng = double.Parse(dataTable.Rows[0]["MARKELNG"].ToString());
                    flag = true;
                }
                sql = "select LAT, LNG from ORGCENTERDATA where ISDELETE = 0 and PGUID = '" + e.Node.GetValue("pguid").ToString() + "'";
                dataTable = FileReader.line_ahp.ExecuteDataTable(sql);
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
                    mapHelper1.ShowMap(cur_level, cur_level.ToString(), false, map_type, null, null, null, 1.0, 400);
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

        private void DrawBorder()
        {
            //string text = "fb66d40b-50fa-4d88-8156-c590328004cb";
            //string text2 = "e62844cb-2839-4b49-853a-250e11ec1901";
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {                
                ["color"] = borData.Color,
                ["weight"] = 0,
                ["fillColor"] = "#C0C0C0",
                ["fillOpacity"] = 0.5
            };
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
                Dictionary<string, object> dictionary3 = new Dictionary<string, object>
                {
                    ["type"] = borData.Type,
                    ["width"] = borData.Width,
                    ["color"] = borData.Color,
                    ["opacity"] = borData.Opacity,
                    ["path"] = item.Value
                };
                mapHelper1.DrawBorder(text3, dictionary3);
            }
        }

        private Dictionary<string, List<double[]>> Get_Border_Line(string pguid)
        {
            Dictionary<string, List<double[]>> dictionary = new Dictionary<string, List<double[]>>();
            string sql = "select LAT, LNG, BORDERGUID from BORDERDATA where ISDELETE = 0 and UNITID = '" + pguid + "' order by SHOWINDEX";
            DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql);
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
                dictionary[key] = new List<double[]>
                {
                    new double[2]
                {
                double.Parse(dataTable.Rows[i]["LAT"].ToString()),
                double.Parse(dataTable.Rows[i]["LNG"].ToString())
                }
                };
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

        private void EraseBorder()
        {
            TreeListNode focusedNode = treeList1.FocusedNode;
            mapHelper1.deleteMarker(focusedNode["pguid"].ToString());
            foreach (TreeListNode node in focusedNode.Nodes)
            {
                mapHelper1.deleteMarker(node["pguid"].ToString());
            }
        }

        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    FileReader.line_ahp.ExecuteSql(sql);
                }
                mapHelper1.ShowMap(cur_level, cur_level.ToString(), false, map_type, null, null, null, 1.0, 400);
            }
        }

        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;
            string sql = "update BORDERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ISDELETE = 1 where ISDELETE = 0 and UNITID = '" + focusedNode["pguid"].ToString() + "' and BORDERGUID = '" + border_guid + "'";
            FileReader.line_ahp.ExecuteSql(sql);
            borList.Remove(border_guid);
            mapHelper1.ShowMap(cur_level, cur_level.ToString(), false, map_type, null, null, null, 1.0, 400);
        }

        private void BarButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode focusedNode = treeList1.FocusedNode;
            borList = new Dictionary<string, List<double[]>>();
            if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sql = "update BORDERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ISDELETE = 1 where ISDELETE = 0 and UNITID = '" + focusedNode["pguid"].ToString() + "' and BORDERGUID = '" + border_guid + "'";
                FileReader.line_ahp.ExecuteSql(sql);
                borList.Remove(border_guid);
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
                    FileReader.line_ahp.ExecuteSql(sql);
                }
                mapHelper1.ShowMap(cur_level, cur_level.ToString(), false, map_type, null, null, null, 1.0, 400);
            }
        }

        private void MapHelper1_LevelChanged(int lastLevel, int currLevel, string showLevel)
        {
            now_level = currLevel;
            DrawBorder();
        }

        private void MapHelper1_MapTypeChanged(string mapType)
        {
            map_type = mapType;
        }

        private void MapHelper1_MapRightClick(bool canedit, double lat, double lng, int x, int y)
        {
            if (Check_InBorder(new DPoint(lat, lng)))
            {
                barButtonItem2.Visibility = BarItemVisibility.Always;
                barButtonItem3.Visibility = BarItemVisibility.Always;
            }
            else
            {
                barButtonItem2.Visibility = BarItemVisibility.Never;
                barButtonItem3.Visibility = BarItemVisibility.Never;
            }
            popupMenu1.ShowPopup(barManager1, Control.MousePosition);
        }

        private bool Check_InBorder(DPoint p)
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
    }

    public class FileReader
    {
        public static AccessHelper once_ahp = null;
        public static AccessHelper often_ahp = null;
        public static AccessHelper line_ahp = null;
        public static AccessHelper log_ahp = null;
        public static IniOperator inip = null;
        public static string[] Authority = null;
    }

    public class GL_Node
    {
        public string pguid
        {
            get;
            set;
        }

        public string upguid
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string level
        {
            get;
            set;
        }

        public string maps
        {
            get;
            set;
        }

        public string lat
        {
            get;
            set;
        }

        public string lng
        {
            get;
            set;
        }

        public bool reg
        {
            get;
            set;
        }
    }


}
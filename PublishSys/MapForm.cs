using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PublishSys
{
    public partial class MapForm : XtraForm
    {
        public string unitid = "";
        public int maxlevel = 0;

        private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;
        private IniOperator inip = null;
        private AccessHelper ahp1 = null;
        private AccessHelper ahp2 = null;
        private AccessHelper ahp3 = null;
        private AccessHelper ahp4 = null;
        private AccessHelper ahp5 = null;
        private AccessHelper ahp6 = null;

        /// <summary>
        /// 组织结构
        /// </summary>
        private List<LevelUnit> LevelList = null;

        /// <summary>
        /// 地图数据
        /// </summary>
        private string[] folds = null;
        private string map_type = "g_map";
        private bool Before_ShowMap = false;
        Dictionary<string, object> borderDic = null;
        private Dictionary<string, List<double[]>> borList = new Dictionary<string, List<double[]>>();

        /// <summary>
        /// 图符数据
        /// </summary>
        private List<string> Prop_GUID = null;                  // 属性GUID
        private Dictionary<string, string> Show_Name = null;    // 属性名称
        private Dictionary<string, string> Show_FDName = null;  // 属性表名
        private Dictionary<string, string> inherit_GUID = null; // 继承属性GUID
        private Dictionary<string, string> Show_Value = null;   // 属性值
        private Dictionary<string, string> GUID_ICON = new Dictionary<string, string>();        

        public MapForm()
        {
            InitializeComponent();
        }

        private void MapForm_Load(object sender, EventArgs e)
        {
            ahp1 = new AccessHelper(WorkPath + "Publish\\data\\ENVIR_H0001Z000E00.mdb");
            ahp2 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000K00.mdb");
            ahp3 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000K01.mdb");
            ahp4 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000E00.mdb");
            ahp5 = new AccessHelper(WorkPath + "Publish\\data\\经纬度注册.mdb");
            ahp6 = new AccessHelper(WorkPath + "Publish\\data\\ENVIRDYDATA_H0001Z000E00.mdb");
            Get_Map_Show();
            Get_Icon_Lib();
            Get_Level_List();
        }

        private void MapForm_Shown(object sender, EventArgs e)
        {

        }

        private void Get_Map_Show()
        {
            inip = new IniOperator(WorkPath + "Publish\\parameter.ini");
            textEdit1.Text = inip.ReadString("mapproperties", "centerlng", "0");
            textEdit2.Text = inip.ReadString("mapproperties", "centerlat", "0");
            if (textEdit1.Text == "0" || textEdit2.Text == "0")
            {
                XtraMessageBox.Show("获取不到当前经纬度");
            }
            checkedListBoxControl1.Items.Clear();
            Show_Map_List("http://192.168.0.109:190/downfile/googlemaps");

            // 导入边界线
            borList = new Dictionary<string, List<double[]>>();
            borderDic = new Dictionary<string, object>
            {
                { "type", "实线" },
                { "width", 1 },
                { "color", "#000000" },
                { "opacity", 1 }
            };
            string sql = "select LAT, LNG, BORDERGUID from BORDERDATA where ISDELETE = 0 and UNITID = '" + unitid + "' order by SHOWINDEX";
            DataTable dt = ahp5.ExecuteDataTable(sql, null);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                string pguid = dt.Rows[i]["BORDERGUID"].ToString();
                if (borList.ContainsKey(pguid))
                    borList[pguid].Add(new double[] { double.Parse(dt.Rows[i]["LAT"].ToString()), double.Parse(dt.Rows[i]["LNG"].ToString()) });
                else
                {
                    borList[pguid] = new List<double[]>
                    {
                        new double[] { double.Parse(dt.Rows[i]["LAT"].ToString()), double.Parse(dt.Rows[i]["LNG"].ToString()) }
                    };
                }
            }
            if (dt.Rows.Count > 0)
                borderDic.Add("path", borList);
            else
                borderDic = null;
        }

        private void Show_Map_List(string url)
        {
            folds = new string[] { "11", "12", "13", "14", "15", "16", "17", "18", "19" };
            for (int i = 0; i < folds.Length; i++)
                checkedListBoxControl1.Items.Add(folds[i]);
            mapHelper1.centerlat = double.Parse(textEdit2.Text);
            mapHelper1.centerlng = double.Parse(textEdit1.Text);
            mapHelper1.webpath = WorkPath + "Publish\\googlemap";
            mapHelper1.roadmappath = url + "\\roadmap";
            mapHelper1.satellitemappath = url + "\\satellite_en";
            mapHelper1.iconspath = WorkPath + "Publish\\PNGICONFOLDER";
            mapHelper1.maparr = folds;
            if (checkedListBoxControl1.Items.Count > 0)
                checkedListBoxControl1.SelectedIndex = 0;
        }

        private void Get_Icon_Lib()
        {
            xtraTabControl2.Controls.Clear();
            xtraTabControl2.TabPages.Clear();
            ahp2 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000K00.mdb");
            string sql = "select UPGUID, PROPVALUE from ZSK_PROP_H0001Z000K00 where ISDELETE = 0 and PROPNAME = '图符库' order by PROPVALUE, SHOWINDEX";
            DataTable dt = ahp2.ExecuteDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i]["PROPVALUE"].ToString();
                string pguid = dt.Rows[i]["UPGUID"].ToString();
                int num = name.IndexOf("图符库");
                if (num < 0)
                {
                    continue;
                }
                name = name.Substring(0, num);
                if (name == "备用")
                {
                    continue;
                }
                bool flag = false;
                for (int j = 0; j < xtraTabControl2.TabPages.Count; j++)
                {
                    if (xtraTabControl2.TabPages[j].Name == name)
                    {
                        flag = true;
                        FlowLayoutPanel flp = (FlowLayoutPanel)xtraTabControl2.TabPages[j].Controls[0];
                        Add_Icon(flp, pguid);
                    }
                }
                if (!flag)
                {
                    xtraTabControl2.TabPages.Add(name);
                    num = xtraTabControl2.TabPages.Count - 1;
                    FlowLayoutPanel flp = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        FlowDirection = FlowDirection.LeftToRight,
                        WrapContents = true,
                        AutoScroll = true
                    };
                    //flp.Controls.Clear();
                    flp.MouseDown += IconLib_MouseDown;
                    Add_Icon(flp, pguid);
                    xtraTabControl2.TabPages[num].Name = name;
                    xtraTabControl2.TabPages[num].BackColor = SystemColors.Control;
                    xtraTabControl2.TabPages[num].Controls.Add(flp);
                }
            }
        }

        private void Add_Icon(FlowLayoutPanel flp, string pguid)
        {
            string str = WorkPath + "Publish\\ICONDER\\b_PNGICON\\";
            ucPictureBox ucPB = new ucPictureBox();
            string sql = "select JDNAME from ZSK_OBJECT_H0001Z000K00 where ISDELETE = 0 and PGUID = '" + pguid + "'";
            DataTable dt = ahp2.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                ucPB.Parent = flp;
                ucPB.Name = pguid;
                ucPB.IconName = dt.Rows[0]["JDNAME"].ToString();
                ucPB.IconPguid = pguid;
                ucPB.IconPath = str + pguid + ".png";
                ucPB.Single_Click += Icon_SingleClick;
                ucPB.Double_Click += Icon_DoubleClick;
                ucPB.IconCheck = false;
            }
        }

        private void Icon_SingleClick(object sender, EventArgs e, string iconguid)
        {
            ucPictureBox ucPB = (ucPictureBox)sender;
            if (!ucPB.IconCheck)
            {
                if (ucPB.Parent == flowLayoutPanel1)
                {
                    foreach (ucPictureBox control in flowLayoutPanel1.Controls)
                    {
                        control.IconCheck = false;
                    }
                    ucPB.IconCheck = true;
                    Show_Icon_Property(iconguid);
                }
                else
                {
                    FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)ucPB.Parent;
                    foreach (ucPictureBox control2 in flowLayoutPanel.Controls)
                    {
                        control2.IconCheck = false;
                    }
                    ucPB.IconCheck = true;
                }
            }
        }

        private void Icon_DoubleClick(object sender, EventArgs e, string iconguid)
        {
            TreeListNode pNode = treeList1.FocusedNode;
            if (pNode == null)
                return;
            string levelguid = pNode["Pguid"].ToString();
            ucPictureBox tmp = (ucPictureBox)sender;
            if (tmp.Parent == flowLayoutPanel1)
            {
                Control value = tmp;
                flowLayoutPanel1.Controls.Remove(value);
                string sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    + "' where ICONGUID = '" + iconguid + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                ahp6.ExecuteSql(sql);
            }
            else
            {
                foreach (ucPictureBox control in flowLayoutPanel1.Controls)
                {
                    if (control.IconPguid == tmp.IconPguid)
                    {
                        MessageBox.Show("已添加该图符!");
                        return;
                    }
                }
                ucPictureBox ucPictureBox3 = new ucPictureBox
                {
                    IconName = tmp.IconName,
                    IconPguid = tmp.IconPguid,
                    IconPath = tmp.IconPath,
                    IconCheck = false
                };
                ucPictureBox3.Single_Click += Icon_SingleClick;
                ucPictureBox3.Double_Click += Icon_DoubleClick;
                flowLayoutPanel1.Controls.Add(ucPictureBox3);
                string sql = "select PGUID from ICONDUIYING_H0001Z000E00 where ISDELETE = 0 and ICONGUID = '" + iconguid + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                DataTable dataTable = ahp6.ExecuteDataTable(sql);
                if (dataTable.Rows.Count > 0)
                {
                    sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + iconguid + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                    ahp6.ExecuteSql(sql);
                }
                else
                {
                    sql = "insert into ICONDUIYING_H0001Z000E00 (PGUID, S_UDTIME, LEVELGUID, ICONGUID, UNITEID) values ('" + levelguid + "_" + iconguid + "_" + unitid + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + levelguid + "', '" + iconguid + "', '" + unitid + "')";
                    ahp6.ExecuteSql(sql);
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
            string typeguid = "";
            typeguid = "{26E232C8-595F-44E5-8E0F-8E0FC1BD7D24}";
            Get_Property_Data(gridControl2, iconguid, typeguid);    // 固定属性
            typeguid = "{B55806E6-9D63-4666-B6EB-AAB80814648E}";
            Get_Property_Data(gridControl1, iconguid, typeguid);    // 基础属性
        }

        private void Get_Property_Data(GridControl gridControl, string iconguid, string typeguid)
        {
            gridControl.DataSource = null;
            Prop_GUID = new List<string>();
            Show_Name = new Dictionary<string, string>();
            Show_FDName = new Dictionary<string, string>();
            inherit_GUID = new Dictionary<string, string>();
            Show_Value = new Dictionary<string, string>();

            string sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_H0001Z000K00 where ISDELETE = 0 and UPGUID = '"
                + iconguid + "' and PROTYPEGUID = '" + typeguid + "' order by SHOWINDEX";
            DataTable proptable = ahp2.ExecuteDataTable(sql);
            Add_Prop(proptable);

            sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_H0001Z000K01 where ISDELETE = 0 and UPGUID = '"
                + iconguid + "' and PROTYPEGUID = '" + typeguid + "' order by SHOWINDEX";
            proptable = ahp3.ExecuteDataTable(sql);
            Add_Prop(proptable);


            DataTable propValue = new DataTable();
            for (int i = 0; i < Prop_GUID.Count; ++i)
            {
                string pguid = Prop_GUID[i];
                propValue.Columns.Add(Show_Name[pguid]);
            }
            propValue.Rows.Add();
            for (int i = 0; i < Prop_GUID.Count; ++i)
            {
                string pguid = Prop_GUID[i];
                propValue.Rows[0][Show_Name[pguid]] = Show_Value[pguid];
            }
            gridControl.DataSource = propValue;
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

        private void FlowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);
        }

        private void IconLib_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu2.ShowPopup(MousePosition);
        }

        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode pNode = treeList1.FocusedNode;
            if (pNode != null)
            {
                string levelguid = pNode["Pguid"].ToString();
                foreach (ucPictureBox control in flowLayoutPanel1.Controls)
                {
                    string sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                    ahp6.ExecuteSql(sql);
                }
                flowLayoutPanel1.Controls.Clear();
            }
        }

        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode pNode = treeList1.FocusedNode;
            if (pNode != null)
            {
                string levelguid = pNode["Pguid"].ToString();
                int selIndex = xtraTabControl2.SelectedTabPageIndex;
                FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)xtraTabControl2.TabPages[selIndex].Controls[0];
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
                        ucPictureBox ucPictureBox3 = new ucPictureBox
                        {
                            IconName = control.IconName,
                            IconPguid = control.IconPguid,
                            IconPath = control.IconPath,
                            IconCheck = false
                        };
                        ucPictureBox3.Single_Click += Icon_SingleClick;
                        ucPictureBox3.Double_Click += Icon_DoubleClick;
                        flowLayoutPanel1.Controls.Add(ucPictureBox3);
                        string sql = "select PGUID from ICONDUIYING_H0001Z000E00 where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                        DataTable dataTable = ahp6.ExecuteDataTable(sql);
                        if (dataTable.Rows.Count > 0)
                        {
                            sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                            ahp6.ExecuteSql(sql);
                        }
                        else
                        {
                            sql = "insert into ICONDUIYING_H0001Z000E00 (PGUID, S_UDTIME, LEVELGUID, ICONGUID, UNITEID) values ('" +levelguid + "_" + control.IconPguid + "_" + unitid + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + levelguid + "', '" + control.IconPguid + "', '" + unitid + "')";
                            ahp6.ExecuteSql(sql);
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
        }

        private void Get_Level_List()
        {
            treeList1.Nodes.Clear();
            treeList1.Appearance.FocusedCell.BackColor = Color.SteelBlue;
            treeList1.KeyFieldName = "Pguid";
            treeList1.ParentFieldName = "Upguid";
            LevelList = new List<LevelUnit>();
            string sql = "select PGUID, JDNAME, JDCODE, UPGUID from ZSK_OBJECT_H0001Z000K01 where ISDELETE = 0 and LEVELNUM >= " + maxlevel.ToString();
            DataTable dt = ahp3.ExecuteDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string pguid = dt.Rows[i]["PGUID"].ToString();
                string upguid = dt.Rows[i]["UPGUID"].ToString();
                string name = dt.Rows[i]["JDNAME"].ToString();
                string code = dt.Rows[i]["JDCODE"].ToString();
                LevelUnit levelunit = new LevelUnit(pguid, upguid, name, code);
                LevelList.Add(levelunit);
            }
            treeList1.DataSource = LevelList;
            treeList1.HorzScrollVisibility = ScrollVisibility.Auto;
            treeList1.Columns[1].Visible = false;
            treeList1.ExpandAll();
            if (treeList1.Nodes.Count > 0)
                treeList1.FocusedNode = treeList1.Nodes[0];
            else
            {
                XtraMessageBox.Show("未导入组织结构数据，即将关闭窗口");
                Close();
                return;
            }
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            inip = new IniOperator(WorkPath + "Publish\\RegInfo.ini");
            inip.WriteString("Public", "UnitID", unitid);
            //Process process = Process.Start(WorkPath + "Publish\\MapSet.exe");
            MapSetForm mapset = new MapSetForm
            {
                unitid = unitid,
                MapPath = "http://192.168.0.109:190/downfile/googlemaps"
            };
            mapset.ShowDialog();
            string sql = "select LAT, LNG from ORGCENTERDATA where ISDELETE = 0 and PGUID = '" + unitid + "'";
            DataTable dataTable = FileReader.line_ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                double m_lat = double.Parse(dataTable.Rows[0]["LAT"].ToString());
                double m_lng = double.Parse(dataTable.Rows[0]["LNG"].ToString());
                //mapHelper1.SetMapCenter(m_lat, m_lng);
                inip = new IniOperator(WorkPath + "Publish\\parameter.ini");
                inip.WriteString("mapproperties", "centerlng", m_lng.ToString());
                inip.WriteString("mapproperties", "centerlat", m_lat.ToString());
            }
            int selectedIndex = checkedListBoxControl1.SelectedIndex;
            mapHelper1.InitMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString(), false, map_type, null, null, null, 1.0, 400);
            mapHelper1.ShowMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString());
            // 导入边界线
            borList = new Dictionary<string, List<double[]>>();
            borderDic = new Dictionary<string, object>
            {
                { "type", "实线" },
                { "width", 1 },
                { "color", "#000000" },
                { "opacity", 1 }
            };
            sql = "select LAT, LNG, BORDERGUID from BORDERDATA where ISDELETE = 0 and UNITID = '" + unitid + "' order by SHOWINDEX";
            DataTable dt = ahp5.ExecuteDataTable(sql, null);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                string pguid = dt.Rows[i]["BORDERGUID"].ToString();
                if (borList.ContainsKey(pguid))
                    borList[pguid].Add(new double[] { double.Parse(dt.Rows[i]["LAT"].ToString()), double.Parse(dt.Rows[i]["LNG"].ToString()) });
                else
                {
                    borList[pguid] = new List<double[]>
                    {
                        new double[] { double.Parse(dt.Rows[i]["LAT"].ToString()), double.Parse(dt.Rows[i]["LNG"].ToString()) }
                    };
                }
            }
            if (dt.Rows.Count > 0)
                borderDic.Add("path", borList);
            else
                borderDic = null;
            if (borderDic != null)
            {
                Dictionary<string, List<double[]>> dictionary = (Dictionary<string, List<double[]>>)borderDic["path"];
                foreach (KeyValuePair<string, List<double[]>> item in dictionary)
                {
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>
                    {
                        ["type"] = borderDic["type"],
                        ["width"] = borderDic["width"],
                        ["color"] = borderDic["color"],
                        ["opacity"] = borderDic["opacity"],
                        ["path"] = item.Value
                    };
                    mapHelper1.DrawBorder(unitid, dictionary2);
                }
            }
        }

        private void TreeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            TreeListNode pNode = treeList1.FocusedNode;
            if (pNode == null)
                return;
            string levelguid = pNode["Pguid"].ToString();
            flowLayoutPanel1.Controls.Clear();
            Show_Icon_List(levelguid);
            for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
            {
                if (checkedListBoxControl1.GetItemChecked(i))
                {
                    checkedListBoxControl1.SetItemChecked(i, value: false);
                }
            }
            string sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
            DataTable dataTable = ahp6.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                string text2 = dataTable.Rows[0]["MAPLEVEL"].ToString();
                string[] array = text2.Split(',');
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < checkedListBoxControl1.Items.Count; j++)
                    {
                        if (checkedListBoxControl1.Items[j].ToString() == array[i])
                        {
                            checkedListBoxControl1.SetItemChecked(j, value: true);
                            break;
                        }
                    }
                }
            }
            int selectedIndex = checkedListBoxControl1.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (!Before_ShowMap)
                {
                    mapHelper1.InitMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString(), false, map_type, null, null, null, 1.0, 400);
                    mapHelper1.ShowMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString());
                }
                else
                {
                    mapHelper1.InitMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString(), false, map_type, null, null, null, 1.0, 400);
                    mapHelper1.ShowMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString());
                    //mapHelper1.setMapLevel(11, "11");
                }
                Before_ShowMap = true;
                if (borderDic != null)
                {
                    Dictionary<string, List<double[]>> dictionary = (Dictionary<string, List<double[]>>)borderDic["path"];
                    foreach (KeyValuePair<string, List<double[]>> item in dictionary)
                    {
                        Dictionary<string, object> dictionary2 = new Dictionary<string, object>
                        {
                            ["type"] = borderDic["type"],
                            ["width"] = borderDic["width"],
                            ["color"] = borderDic["color"],
                            ["opacity"] = borderDic["opacity"],
                            ["path"] = item.Value
                        };
                        mapHelper1.DrawBorder(unitid, dictionary2);
                    }
                }
            }
        }

        private void Show_Icon_List(string levelguid)
        {
            string str = WorkPath + "Publish\\ICONDER\\b_PNGICON\\";
            string sql = "select PGUID, JDNAME from ZSK_OBJECT_H0001Z000K00 where ISDELETE = 0 order by LEVELNUM, SHOWINDEX";
            DataTable dataTable = ahp2.ExecuteDataTable(sql);
            string a = "";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string text = dataTable.Rows[i]["PGUID"].ToString();
                string iconName = dataTable.Rows[i]["JDNAME"].ToString();
                if (!File.Exists(str + text + ".png"))
                {
                    continue;
                }
                sql = "select PGUID from ICONDUIYING_H0001Z000E00 where ISDELETE = 0 and ICONGUID = '" + text + "' and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                DataTable dataTable2 = ahp6.ExecuteDataTable(sql);
                if (dataTable2.Rows.Count > 0)
                {
                    if (a == "")
                    {
                        a = text;
                    }
                    ucPictureBox ucPB = new ucPictureBox
                    {
                        Parent = flowLayoutPanel1,
                        Name = text,
                        IconName = iconName,
                        IconPguid = text,
                        IconPath = str + text + ".png"
                    };
                    ucPB.Single_Click += Icon_SingleClick;
                    ucPB.Double_Click += Icon_DoubleClick;
                    ucPB.IconCheck = false;
                }
            }
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                ucPictureBox ucPictureBox = (ucPictureBox)flowLayoutPanel1.Controls[0];
                Icon_SingleClick(flowLayoutPanel1.Controls[0], new EventArgs(), ucPictureBox.IconPguid);
            }
        }

        private void XtraTabControl2_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            XtraTabPage tabPage = xtraTabControl2.SelectedTabPage;
            if (tabPage.Controls.Count <= 0)
                return;
            FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)tabPage.Controls[0];
            foreach (ucPictureBox control in flowLayoutPanel.Controls)
            {
                control.IconCheck = false;
            }
        }

        private void CheckedListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = checkedListBoxControl1.SelectedIndex;
            if (!Before_ShowMap)
            {
                mapHelper1.InitMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString(), false, map_type, null, null, null, 1.0, 400);
                mapHelper1.ShowMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString());
            }
            else
            {
                //mapHelper1.setMapLevel(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString());
                mapHelper1.InitMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString(), false, map_type, null, null, null, 1.0, 400);
                mapHelper1.ShowMap(int.Parse(checkedListBoxControl1.Items[selectedIndex].ToString()), checkedListBoxControl1.Items[selectedIndex].ToString());
            }
            if (borderDic != null)
            {
                Dictionary<string, List<double[]>> dictionary = (Dictionary<string, List<double[]>>)borderDic["path"];
                foreach (KeyValuePair<string, List<double[]>> item in dictionary)
                {
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>
                    {
                        ["type"] = borderDic["type"],
                        ["width"] = borderDic["width"],
                        ["color"] = borderDic["color"],
                        ["opacity"] = borderDic["opacity"],
                        ["path"] = item.Value
                    };
                    mapHelper1.DrawBorder(unitid, dictionary2);
                }
            }
            Before_ShowMap = true;
        }

        private void CheckedListBoxControl1_Leave(object sender, EventArgs e)
        {
            List<string> mpLst = new List<string>();
            for (int i = 0; i < checkedListBoxControl1.Items.Count; i++)
            {
                if (checkedListBoxControl1.GetItemChecked(i))
                {
                    mpLst.Add(checkedListBoxControl1.Items[i].ToString());
                }
            }
            TreeListNode pNode = treeList1.FocusedNode;
            if (pNode != null)
            {
                string levelguid = pNode["Pguid"].ToString();
                string sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                DataTable dataTable = ahp6.ExecuteDataTable(sql);
                string mplevel = string.Join(",", mpLst);
                //GL_MAP[text] = text2;
                if (dataTable.Rows.Count > 0)
                {
                    sql = "update MAPDUIYING_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', MAPLEVEL = '" + mplevel + "' where ISDELETE = 0 and LEVELGUID = '" + levelguid + "' and UNITEID = '" + unitid + "'";
                    ahp6.ExecuteSql(sql);
                }
                else
                {
                    sql = "insert into MAPDUIYING_H0001Z000E00 (PGUID, S_UDTIME, LEVELGUID, MAPLEVEL, UNITEID) values ('" + levelguid + "_" + unitid + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + levelguid + "', '" + mplevel + "', '" + unitid + "')";
                    ahp6.ExecuteSql(sql);
                }
            }
        }

        private void MapHelper1_LevelChanged(int lastLevel, int currLevel, string showLevel)
        {
            if (borderDic != null)
            {
                Dictionary<string, List<double[]>> dictionary = (Dictionary<string, List<double[]>>)borderDic["path"];
                foreach (KeyValuePair<string, List<double[]>> item in dictionary)
                {
                    Dictionary<string, object> dictionary2 = new Dictionary<string, object>
                    {
                        ["type"] = borderDic["type"],
                        ["width"] = borderDic["width"],
                        ["color"] = borderDic["color"],
                        ["opacity"] = borderDic["opacity"],
                        ["path"] = item.Value
                    };
                    mapHelper1.DrawBorder(unitid, dictionary2);
                }
            }
        }

        private void MapHelper1_MapTypeChanged(string mapType)
        {
            map_type = mapType;
        }

        private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            groupControl1.Focus();
            inip = new IniOperator(WorkPath + "Publish\\RegInfo.ini");
            inip.WriteString("Public", "UnitID", unitid);
            ahp1.CloseConn();
            ahp2.CloseConn();
            ahp3.CloseConn();
            ahp4.CloseConn();
            ahp5.CloseConn();
            ahp6.CloseConn();
        }

        private void MapForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ahp1.CloseConn();
            ahp2.CloseConn();
            ahp3.CloseConn();
            ahp4.CloseConn();
            ahp5.CloseConn();
            ahp6.CloseConn();
        }
    }
}
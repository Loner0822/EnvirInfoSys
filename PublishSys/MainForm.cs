﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using DevExpress.XtraTreeList;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTreeList.Nodes;
using System.Net.Sockets;
using System.Linq;
using System.IO;

namespace PublishSys
{
    public partial class MainForm : DevExpress.XtraEditors.XtraForm
    {

        private AccessHelper ahp = null;
        private IniOperator inip = null;
        private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

        private string Last_Level = null;
        private int Curr_Level = 0;
        List<AdminUnit> AdminList = null;

        private bool list_show = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Get_Tree_View();
            Get_Publish_Record();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            textEdit3.Text = "1.00.00";
            treeList1.Focus();
            //textEdit3.Focus();
            //textEdit3.SelectAll();
        }

        private void Get_Tree_View()
        {
            inip = new IniOperator(WorkPath + "PackUp.ini");
            Last_Level = inip.ReadString("unitlevel", "lastlevel", "县");
            Last_Level = Last_Level.Replace("\0", "");
            treeList1.Nodes.Clear();
            treeList1.Appearance.FocusedCell.BackColor = Color.SteelBlue;
            treeList1.KeyFieldName = "Id";
            treeList1.ParentFieldName = "Pid";
            ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\ZZJG_H0001Z000Z00.mdb");
            string sql = "select PGUID, UPGUID, JDNAME, ZSKOBJ_PGUID from ZUZHI_JIEGOU_H0001Z000Z00 where ISDELETE = 0";
            AdminList = new List<AdminUnit>();
            DataTable dt = ahp.ExecuteDataTable(sql);
            ahp.CloseConn();
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                string pguid = dt.Rows[i]["PGUID"].ToString();
                string upguid = dt.Rows[i]["UPGUID"].ToString();
                string name = dt.Rows[i]["JDNAME"].ToString();
                string level = dt.Rows[i]["ZSKOBJ_PGUID"].ToString();
                AdminUnit adminUnit = new AdminUnit(pguid, upguid, level, name);
                AdminList.Add(adminUnit);
            }
            treeList1.DataSource = AdminList;
            treeList1.HorzScrollVisibility = ScrollVisibility.Auto;
            treeList1.Columns[0].Visible = false;
            treeList1.ExpandAll();
            list_show = true;
            treeList1.FocusedNode = treeList1.Nodes[0];
            
        }

        private void Get_Publish_Record()
        {
            DataTable pr_dt = new DataTable();
            pr_dt.Columns.Add("序号", typeof(int));
            pr_dt.Columns.Add("发布单位", typeof(string));
            pr_dt.Columns.Add("发布时间", typeof(string));
            pr_dt.Columns.Add("发布系统", typeof(string));
            pr_dt.Columns.Add("系统版本", typeof(string));
            ahp = new AccessHelper(WorkPath + "data\\PublishData.mdb");
            string sql = "select * from PUBLISH_H0001Z000E00 where ISDELETE = 0 order by S_UDTIME";
            DataTable dt = ahp.ExecuteDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
                pr_dt.Rows.Add(i + 1, dt.Rows[i]["UNITNAME"].ToString(), dt.Rows[i]["S_UDTIME"].ToString(), dt.Rows[i]["UNITNAME"].ToString() + "环境信息化系统", dt.Rows[i]["VERSION"].ToString());
            gridControl1.DataSource = pr_dt;
            GridView gridView = gridControl1.MainView as GridView;
            gridView.OptionsBehavior.Editable = false;
            for (int i = 0; i < 5; ++i)
                gridView1.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
            gridView1.Columns[0].Width = 60;
            gridView1.Columns[1].Width = 150;
            gridView1.Columns[2].Width = 150;
            gridView1.Columns[3].Width = 200;
            ahp.CloseConn();
        }

        private void BarButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process process = Process.Start(WorkPath + "区域环境信息化系统\\IconDataDown.exe", "-1 1 2");
            process.WaitForExit();
        }

        private void BarButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process process = Process.Start(WorkPath + "区域环境信息化系统\\OrgDataDown.exe");
            process.WaitForExit();
            Get_Tree_View();
        }

        private void BarButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process process = Process.Start(WorkPath + "区域环境信息化系统\\SetIP.exe");
            process.WaitForExit();
        }

        private void BarButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ahp.CloseConn();
            Close();
        }

        private void BarButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ahp.CloseConn();
            Process process = Process.Start(WorkPath + "DataBF.exe");
            process.WaitForExit();
        }

        private void BarButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process process = Process.Start(WorkPath + "DataHF.exe");
            process.WaitForExit();
        }

        private void BarButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Process process = Process.Start(WorkPath + "DataUP.exe", "PublishSys.exe 0 2");
            process.WaitForExit();
        }

        private void BarButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ahp.CloseConn();
            Process process = Process.Start(WorkPath + "DataUP.exe", "PublishSys.exe 1 1");
            process.WaitForExit();
        }

        private void TreeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if (list_show == false)
                return;
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = true;
            simpleButton3.Enabled = true;
            textEdit1.Text = "";
            textEdit2.Text = "";
            TreeListNode pNode = treeList1.FocusedNode;
            if (pNode == null)
                return;
            ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\ZSK_H0001Z000K01.mdb");
            string sql = "select LEVELNUM from ZSK_OBJECT_H0001Z000K01 where ISDELETE = 0 and JDNAME = '" + pNode["Level"].ToString() + "'";
            DataTable dt = ahp.ExecuteDataTable(sql);
            ahp.CloseConn();
            if (dt.Rows.Count > 0)
                Curr_Level = int.Parse(dt.Rows[0]["LEVELNUM"].ToString());
            ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\经纬度注册.mdb");
            sql = "select LAT, LNG from ORGCENTERDATA where ISDELETE = 0 and PGUID = '" + pNode["Id"].ToString() + "'";
            dt = ahp.ExecuteDataTable(sql);
            ahp.CloseConn();
            if (dt.Rows.Count > 0 && dt.Rows[0]["LNG"].ToString() != string.Empty && dt.Rows[0]["LAT"].ToString() != string.Empty)
            {
                textEdit1.Text = dt.Rows[0]["LNG"].ToString();
                textEdit2.Text = dt.Rows[0]["LAT"].ToString();
                inip = new IniOperator(WorkPath + "区域环境信息化系统\\parameter.ini");
                inip.WriteString("mapproperties", "centerlng", textEdit1.Text);
                inip.WriteString("mapproperties", "centerlat", textEdit2.Text);
                return;
            }
            
            if (textEdit1.Text == "" || textEdit2.Text == "")
            {
                if (XtraMessageBox.Show("是否从网上获取 " + pNode["Name"].ToString() + " 的经纬度?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    string[] array = mapHelper1.AddressToLocation(pNode["Name"].ToString());
                    textEdit1.Text = array[1];
                    textEdit2.Text = array[0];
                    simpleButton1.Focus();

                    ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\经纬度注册.mdb");
                    sql = "select LAT, LNG from ORGCENTERDATA where ISDELETE = 0 and UNITEID = '" + pNode["Id"].ToString() + "'";
                    dt = ahp.ExecuteDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "update ORGCENTERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LAT = '" + textEdit2.Text + "', LNG = '" + textEdit1.Text + "' where ISDELETE = 0 and PGUID = '" + pNode["Id"].ToString() + "'";
                        ahp.ExecuteSql(sql);
                    }
                    else
                    {
                        sql = "insert into ORGCENTERDATA (PGUID, S_UDTIME, UNITEID, LAT, LNG) values ('" + pNode["Id"].ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + pNode["Id"].ToString() + "', '" + textEdit2.Text + "', '" + textEdit1.Text + "')";
                        ahp.ExecuteSql(sql);
                    }
                    ahp.CloseConn();
                }
            }

            inip = new IniOperator(WorkPath + "区域环境信息化系统\\parameter.ini");
            inip.WriteString("mapproperties", "centerlng", textEdit1.Text);
            inip.WriteString("mapproperties", "centerlat", textEdit2.Text);
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            TreeListNode pNode = treeList1.FocusedNode;
            if (textEdit1.Text == "")
            {
                XtraMessageBox.Show("请填写当前单位经度!");
                textEdit1.Focus();
                return;
            }
            if (textEdit2.Text == "")
            {
                XtraMessageBox.Show("请填写当前单位纬度!");
                textEdit2.Focus();
                return;
            }

            IniOperator iniOperator = new IniOperator(WorkPath + "区域环境信息化系统\\SyncInfo.ini");
            string ip = iniOperator.ReadString("MapLogin", "ip", "");
            string port = iniOperator.ReadString("MapLogin", "port", "");
            if (port == "")
            {
                XtraMessageBox.Show("请先设置服务器IP!");
                return;
            }
            if (TestServerConnection(ip, int.Parse(port), 500))
            {
                MapForm mapForm = new MapForm
                {
                    unitid = pNode["Id"].ToString(),
                    unitlevel = pNode["Level"].ToString(),
                    unitname = pNode["Name"].ToString(),
                    maxlevel = Curr_Level,
                    Text = "地图对应"
                };
                mapForm.ShowDialog();

                inip = new IniOperator(WorkPath + "区域环境信息化系统\\parameter.ini");
                textEdit1.Text = inip.ReadString("mapproperties", "centerlng", "");
                textEdit2.Text = inip.ReadString("mapproperties", "centerlat", "");
            }
            else
                XtraMessageBox.Show("请连接地图下载服务器!");
        }

        private bool TestServerConnection(string host, int port, int millisecondsTimeout)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    IAsyncResult asyncResult = tcpClient.BeginConnect(host, port, null, null);
                    asyncResult.AsyncWaitHandle.WaitOne(millisecondsTimeout);
                    return tcpClient.Connected;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    tcpClient.Close();
                }
            }
        }

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            if (textEdit1.Text == "")
            {
                XtraMessageBox.Show("请填写当前单位经度!");
                textEdit1.Focus();
                return;
            }
            if (textEdit2.Text == "")
            {
                XtraMessageBox.Show("请填写当前单位纬度!");
                textEdit2.Focus();
                return;
            }

            TreeListNode pNode = treeList1.FocusedNode;
            inip = new IniOperator(WorkPath + "区域环境信息化系统\\RegInfo.ini");

            inip.WriteString("Public", "UnitName", pNode["Name"].ToString());
            inip.WriteString("Public", "UnitLevel", pNode["Level"].ToString());
            inip.WriteString("Public", "UnitID", pNode["Id"].ToString());
            inip.WriteString("Public", "AppName", "环境信息化系统");
            inip.WriteString("版本号", "VerNum", textEdit3.Text);
            inip = new IniOperator(WorkPath + "PackUp.ini");
            inip.WriteString("packup", "my_app_name", "环境信息化系统");
            inip.WriteString("packup", "my_app_version", textEdit3.Text);
            inip.WriteString("packup", "my_app_publisher", pNode["Name"].ToString());
            inip.WriteString("packup", "my_app_exe_name", "EnvirInfoSys.exe");
            inip.WriteString("packup", "my_app_id", "{" + Guid.NewGuid().ToString("B"));
            inip.WriteString("packup", "source_exe_path", WorkPath + "区域环境信息化系统\\EnvirInfoSys.exe");
            inip.WriteString("packup", "source_path", WorkPath + "区域环境信息化系统");
            inip.WriteString("packup", "registry_subkey", "环境信息化系统");

            ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\PASSWORD_H0001Z000E00.mdb");
            string sql = "select PGUID from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWNAME = '管理员密码' and UNITID = '" + pNode["Id"].ToString() + "'";
            DataTable dataTable = ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                sql = "update PASSWORD_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', PWMD5 = '95d565ef66e7dff9' where PWNAME = '管理员密码' and UNITID = '" + pNode["Id"].ToString() + "'";
                ahp.ExecuteSql(sql);
            }
            else
            {
                sql = "insert into PASSWORD_H0001Z000E00 (PGUID, S_UDTIME, PWNAME, PWMD5, UNITID) values ('管理员密码_" + pNode["Id"].ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '管理员密码', '95d565ef66e7dff9', '" + pNode["Id"].ToString() + "')";
                ahp.ExecuteSql(sql);
            }
            sql = "select PGUID from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWNAME = '编辑模式' and UNITID = '" + pNode["Id"].ToString() + "'";
            dataTable = ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                sql = "update PASSWORD_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', PWMD5 = 'a0b923820dcc509a' where PWNAME = '编辑模式' and UNITID = '" + pNode["Id"].ToString() + "'";
                ahp.ExecuteSql(sql);
            }
            else
            {
                sql = "insert into PASSWORD_H0001Z000E00 (PGUID, S_UDTIME, PWNAME, PWMD5, UNITID) values ('编辑模式_" + pNode["Id"].ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '编辑模式', 'a0b923820dcc509a', '" + pNode["Id"].ToString() + "')";
                ahp.ExecuteSql(sql);
            }
            sql = "select PGUID from PASSWORD_H0001Z000E00 where ISDELETE = 0 and PWNAME = '查看模式' and UNITID = '" + pNode["Id"].ToString() + "'";
            dataTable = ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                sql = "update PASSWORD_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', PWMD5 = '9d4c2f636f067f89' where PWNAME = '查看模式' and UNITID = '" + pNode["Id"].ToString() + "'";
                ahp.ExecuteSql(sql);
            }
            else
            {
                sql = "insert into PASSWORD_H0001Z000E00 (PGUID, S_UDTIME, PWNAME, PWMD5, UNITID) values ('查看模式_" + pNode["Id"].ToString() + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '查看模式', '9d4c2f636f067f89', '" + pNode["Id"].ToString() + "')";
                ahp.ExecuteSql(sql);
            }
            ahp.CloseConn();
            ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\ENVIR_H0001Z000E00.mdb");
            sql = "select PGUID from ENVIRGXFL_H0001Z000E00 where ISDELETE = 0 and UPGUID = '-1' and UNITID = '" + pNode["Id"].ToString() + "'";
            dataTable = ahp.ExecuteDataTable(sql);
            if (dataTable.Rows.Count > 0)
            {
                sql = "update ENVIRGXFL_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', FLNAME = '管辖', SHOWINDEX = 0 where ISDELETE = 0 and UPGUID = '-1' and UNITID = '" + pNode["Id"].ToString() + "'";
                ahp.ExecuteSql(sql);
            }
            else
            {
                sql = "insert into ENVIRGXFL_H0001Z000E00 (PGUID, S_UDTIME, FLNAME, UNITID) values ('" + pNode["Id"].ToString() + "_管辖', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '管辖', '" + pNode["Id"].ToString() + "')";
                ahp.ExecuteSql(sql);
            }
            ahp.CloseConn();
            ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\ZSK_AppInfo.mdb");
            sql = "update APPINFO set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', UNITID = '" + pNode["Id"].ToString() + "' where ISDELETE = 0 and PGUID = '{8C3B99C5-26D3-48B2-A676-250189FCEA2F}'";
            ahp.ExecuteSql(sql);
            ahp.CloseConn();
            Process process = Process.Start(WorkPath + "PackUp.exe");
            process.WaitForExit();
            if (process.ExitCode == -1)
            {
                XtraMessageBox.Show("发布失败!");
                return;
            }

            PublishSelect publishSelect = new PublishSelect();
            List<string> ProgList = new List<string>();
            inip = new IniOperator(WorkPath + "Publish_Info.ini");
            int cnt = inip.ReadInteger("Publish", "Count", 0);
            for (int i = 0; i < cnt; ++i)
            {
                inip = new IniOperator(WorkPath + "Publish_Info.ini");
                string progname = inip.ReadString("Publish", i.ToString(), "");
                progname = progname.Replace("\0", "");
                inip = new IniOperator(WorkPath + progname + "\\RegInfo.ini");
                ProgList.Add(progname);
                //Process proc = Process.Start(WorkPath + "Inno Setup\\Compil32.exe", "/cc \"" + WorkPath + progname + ".iss\"");
                //proc.WaitForExit();
            }

            publishSelect.proglist = ProgList;
            if (publishSelect.ShowDialog() == DialogResult.OK)
            {
                if (!Directory.Exists(WorkPath + "区域环境信息化发布管理"))
                    return;
                string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string[] Files = Directory.GetFiles(WorkPath + "区域环境信息化发布管理", "*exe");
                foreach (string file in Files)
                {
                    string FileName = Path.GetFileName(file);
                    File.Copy(file, DesktopPath + "//" + FileName, true);
                }

                string text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string text2 = Guid.NewGuid().ToString("B");
                ahp = new AccessHelper(WorkPath + "data\\PublishData.mdb");
                sql = "insert into PUBLISH_H0001Z000E00 (PGUID, S_UDTIME, UNITID, UNITNAME, VERSION, SYSTEMNAME) values ('" + text2 + "', '" + text + "', '" + pNode["Id"].ToString() + "', '" + pNode["Name"].ToString() + "', '" + textEdit3.Text + "', '" + pNode["Name"].ToString() + "环境信息化系统')";
                ahp.ExecuteSql(sql);
                ahp.CloseConn();
                Get_Publish_Record();
                Process p = Process.Start(WorkPath + "区域环境信息化系统\\DownOrgMapByBorder.exe", "2 2 2 2");
                p.WaitForExit();
                Directory.Delete(WorkPath + "区域环境信息化发布管理", true);
                XtraMessageBox.Show("发布成功!");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            TreeListNode pNode = treeList1.FocusedNode;
            string pguid = pNode["Id"].ToString();
            string lvlist = Get_Level_List(pguid);
            if(lvlist == "")
                return;
            if (XtraMessageBox.Show("是否要下载单位地图到打包目录?", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Process p = Process.Start(WorkPath + "区域环境信息化系统\\DownOrgMapByBorder.exe", pguid + " " + lvlist + " 1");
                p.WaitForExit();
                if (p.ExitCode != 0)
                {
                    MessageBox.Show("下载地图失败!请检查服务器后重新下载");
                    return;
                }
            }
            else
            {
                if (xtraFolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    Process p = Process.Start(WorkPath + "区域环境信息化系统\\DownOrgMapByBorder.exe", pguid + " " + lvlist + " 1 " + xtraFolderBrowserDialog1.SelectedPath);
                    p.WaitForExit();
                    if (p.ExitCode != 0)
                    {
                        MessageBox.Show("下载地图失败!请检查服务器后重新下载");
                        return;
                    }
                }
            }
        }

        private string Get_Level_List(string pguid)
        {
            string lvlist = "";
            ahp = new AccessHelper(WorkPath + "区域环境信息化系统\\data\\ENVIRDYDATA_H0001Z000E00.mdb");
            string sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and UNITEID = '" + pguid + "'";
            DataTable dt = ahp.ExecuteDataTable(sql);
            ahp.CloseConn();
            for (int i = 0; i < dt.Rows.Count; ++i)
                lvlist += dt.Rows[i]["MAPLEVEL"].ToString() + ",";
            List<string> tmp = new List<string>(lvlist.Split(','));
            List<int> tmp_num = new List<int>();
            for (int i = 0; i < tmp.Count; ++i)
                if (tmp[i] != "")
                    tmp_num.Add(int.Parse(tmp[i]));
            tmp_num.Sort();
            tmp_num = tmp_num.Distinct().ToList();

            lvlist = "";
            for (int i = 0; i < tmp_num.Count; ++i)
            {
                if (tmp_num[i] != 0)
                    lvlist += tmp_num[i].ToString() + ",";
            }
            if (lvlist == "")
            {
                XtraMessageBox.Show("当前单位尚未对应地图级别!");
                return "";
            }
            return lvlist.Substring(0, lvlist.Length - 1);
        }
    }
}
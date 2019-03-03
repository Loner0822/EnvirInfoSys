using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;

namespace PublishSys
{
    public partial class MapForm : DevExpress.XtraEditors.XtraForm
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
            Get_Level_List();
        }

        private void MapForm_Shown(object sender, EventArgs e)
        {

        }

        private void Get_Level_List()
        {
            treeList1.Nodes.Clear();
            treeList1.Appearance.FocusedCell.BackColor = Color.SteelBlue;
            treeList1.KeyFieldName = "Id";
            treeList1.ParentFieldName = "Pid";
            LevelList = new List<LevelUnit>();
            string sql = "select PGUID, JDNAME, JDCODE, UPGUID, LEVELNUM from ZSK_OBJECT_H0001Z000K01 where ISDELETE = 0 and LEVELNUM >= " + maxlevel.ToString();
            DataTable dt = ahp3.ExecuteDataTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                
            }
            /*
            string sql = "select PGUID, JDNAME, JDCODE, UPGUID, LEVELNUM from ZSK_OBJECT_H0001Z000K01 where ISDELETE = 0 and LEVELNUM >= " + maxlevel.ToString();
            DataTable dataTable = ahp3.ExecuteDataTable(sql, (OleDbParameter[])null);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                GL_NAME[dataTable.Rows[i]["PGUID"].ToString()] = dataTable.Rows[i]["JDNAME"].ToString();
                GL_JDCODE[dataTable.Rows[i]["PGUID"].ToString()] = dataTable.Rows[i]["JDCODE"].ToString();
                GL_UPGUID[dataTable.Rows[i]["PGUID"].ToString()] = dataTable.Rows[i]["UPGUID"].ToString();
            }
            treeView1.Nodes.Clear();
            treeView1.HideSelection = false;
            treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeView1.DrawNode += treeView1_DrawNode;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i]["LEVELNUM"].ToString() == maxlevel.ToString())
                {
                    TreeNode treeNode = new TreeNode();
                    treeNode.Text = GL_NAME[dataTable.Rows[i]["PGUID"].ToString()];
                    treeNode.Tag = dataTable.Rows[i]["PGUID"].ToString();
                    treeView1.Nodes.Add(treeNode);
                    Add_Unit_Node(treeNode);
                }
            }
            treeView1.ExpandAll();
            */
        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            inip = new IniOperator(WorkPath + "Publish\\RegInfo.ini");
            inip.WriteString("Public", "UnitID", unitid);
            Process process = Process.Start(WorkPath + "Publish\\MapSet.exe");
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {

        }
    }
}
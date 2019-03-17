using MapHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PublishSys
{
	public class MapForm : Form
	{
		private IniOperator inip = null;

		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private AccessHelper ahp1 = null;

		private AccessHelper ahp2 = null;

		private AccessHelper ahp3 = null;

		private AccessHelper ahp4 = null;

		private AccessHelper ahp5 = null;

		private AccessHelper ahp6 = null;

		public string unitid = "";

		private string[] folds = null;

		private string map_type = "g_map";

		private bool Before_ShowMap = false;

		private List<string> Prop_GUID;

		private Dictionary<string, string> Show_Name;

		private Dictionary<string, string> Show_FDName;

		private Dictionary<string, string> inherit_GUID;

		private Dictionary<string, string> Show_Value;

		private Dictionary<string, string> GUID_ICON = new Dictionary<string, string>();

		private Dictionary<string, object> borderDic = null;

		private Dictionary<string, List<double[]>> borList = new Dictionary<string, List<double[]>>();

		public int maxlevel = 0;

		private Dictionary<string, string> GL_NAME;

		private Dictionary<string, string> GL_JDCODE;

		private Dictionary<string, string> GL_UPGUID;

		private Dictionary<string, string> GL_MAP;

		private string slat;

		private string slng;

		private IContainer components = null;

		private Panel panel1;

		private GroupBox groupBox1;

		private Splitter splitter1;

		private Panel panel2;

		private GroupBox groupBox2;

		private TreeView treeView1;

		private FolderBrowserDialog folderBrowserDialog1;

		private ContextMenuStrip contextMenuStrip1;

		private ToolStripMenuItem toolStripMenuItem2;

		private Timer timer1;

		private OpenFileDialog openFileDialog1;

		private TabControl tabControl1;

		private TabPage tabPage1;

		private GroupBox groupBox5;

		private Panel panel5;

		private MapHelper.MapHelper mapHelper1;

		private Splitter splitter3;

		private GroupBox groupBox6;

		private CheckedListBox checkedListBox1;

		private Panel panel8;

		private Button button3;

		private Label label4;

		private Label label3;

		private Label label2;

		private Label label1;

		private TextBox textBox3;

		private TextBox textBox2;

		private Button button1;

		private TextBox textBox1;

		private TabPage tabPage2;

		private GroupBox groupBox3;

		private GroupBox groupBox4;

		private TabControl tabControl2;

		private Splitter splitter2;

		private GroupBox groupBox7;

		private TabControl tabControl3;

		private TabPage tabPage3;

		private DataGridView dataGridView2;

		private TabPage tabPage4;

		private DataGridView dataGridView3;

		private FlowLayoutPanel flowLayoutPanel1;

		private ContextMenuStrip contextMenuStrip2;

		private ToolStripMenuItem 全选ToolStripMenuItem;

		private ContextMenuStrip contextMenuStrip3;

		private ToolStripMenuItem 清空ToolStripMenuItem;

		public MapForm()
		{
			InitializeComponent();
		}

		private void MapForm_Load(object sender, EventArgs e)
		{
			tabControl2.Controls.Clear();
			ahp2 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000K00.mdb");
			string sql = "select UPGUID, PROPVALUE from ZSK_PROP_H0001Z000K00 where ISDELETE = 0 and PROPNAME = '图符库' order by PROPVALUE, SHOWINDEX";
			DataTable dataTable = ahp2.ExecuteDataTable(sql, (OleDbParameter[])null);
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
				for (int j = 0; j < tabControl2.TabPages.Count; j++)
				{
					if (tabControl2.TabPages[j].Name == text)
					{
						flag = true;
						FlowLayoutPanel flp = (FlowLayoutPanel)tabControl2.TabPages[j].Controls[0];
						Add_Icon(flp, pguid);
					}
				}
				if (!flag)
				{
					tabControl2.TabPages.Add(text);
					num = tabControl2.TabPages.Count - 1;
					FlowLayoutPanel flp = new FlowLayoutPanel();
					flp.Dock = DockStyle.Fill;
					flp.FlowDirection = FlowDirection.LeftToRight;
					flp.WrapContents = true;
					flp.AutoScroll = true;
					flp.MouseDown += dataGridView_MouseDown;
					Add_Icon(flp, pguid);
					tabControl2.TabPages[num].Name = text;
					tabControl2.TabPages[num].BackColor = SystemColors.Control;
					tabControl2.TabPages[num].Controls.Add(flp);
				}
			}
			ahp2.CloseConn();
		}

		private void MapForm_Shown(object sender, EventArgs e)
		{
			ahp1 = new AccessHelper(WorkPath + "Publish\\data\\ENVIR_H0001Z000E00.mdb");
			ahp2 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000K00.mdb");
			ahp3 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000K01.mdb");
			ahp4 = new AccessHelper(WorkPath + "Publish\\data\\ZSK_H0001Z000E00.mdb");
			ahp5 = new AccessHelper(WorkPath + "Publish\\data\\经纬度注册.mdb");
			ahp6 = new AccessHelper(WorkPath + "Publish\\data\\ENVIRDYDATA_H0001Z000E00.mdb");
			checkedListBox1.Items.Clear();
			Load_Unit_Level();
			if (treeView1.Nodes.Count <= 0)
			{
				MessageBox.Show("未导入组织结构数据，即将关闭窗口");
				Close();
				return;
			}
			button3.Enabled = false;
			inip = new IniOperator(WorkPath + "Publish\\parameter.ini");
			textBox1.Text = inip.ReadString("mapproperties", unitid, "");
			textBox2.Text = inip.ReadString("mapproperties", "centerlng", "0");
			textBox3.Text = inip.ReadString("mapproperties", "centerlat", "0");
			if (textBox2.Text == "0" || textBox3.Text == "0")
			{
				MessageBox.Show("获取不到当前经纬度");
			}
			checkedListBox1.Items.Clear();
			if (textBox1.Text != string.Empty)
			{
				Show_Map_List(textBox1.Text);
			}
			borList = new Dictionary<string, List<double[]>>();
			borderDic = new Dictionary<string, object>();
			borderDic.Add("type", "实线");
			borderDic.Add("width", 1);
			borderDic.Add("color", "#000000");
			borderDic.Add("opacity", 1);
			string sql = "select LAT, LNG, BORDERGUID from BORDERDATA where ISDELETE = 0 and UNITID = '" + unitid + "' order by SHOWINDEX";
			DataTable dataTable = ahp5.ExecuteDataTable(sql, (OleDbParameter[])null);
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
			if (dataTable.Rows.Count > 0)
			{
				borderDic.Add("path", borList);
			}
			else
			{
				borderDic = null;
			}
			if (treeView1.Nodes.Count > 0)
			{
				treeView1.SelectedNode = null;
				treeView1.SelectedNode = treeView1.Nodes[0];
			}
			tabControl3.TabPages[0].Parent = null;
			timer1.Enabled = true;
		}

		private void Add_Icon(FlowLayoutPanel flp, string pguid)
		{
			string str = WorkPath + "Publish\\ICONDER\\b_PNGICON\\";
			ucPictureBox ucPictureBox = new ucPictureBox();
			string sql = "select JDNAME from ZSK_OBJECT_H0001Z000K00 where ISDELETE = 0 and PGUID = '" + pguid + "'";
			DataTable dataTable = ahp2.ExecuteDataTable(sql, (OleDbParameter[])null);
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

		private void Show_Icon_List(string levelguid)
		{
			string str = WorkPath + "Publish\\ICONDER\\b_PNGICON\\";
			string sql = "select PGUID, JDNAME from ZSK_OBJECT_H0001Z000K00 where ISDELETE = 0 order by LEVELNUM, SHOWINDEX";
			DataTable dataTable = ahp2.ExecuteDataTable(sql, (OleDbParameter[])null);
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
				DataTable dataTable2 = ahp6.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable2.Rows.Count > 0)
				{
					if (a == "")
					{
						a = text;
					}
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
			TreeNode selectedNode = treeView1.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			string text = selectedNode.Tag.ToString();
			ucPictureBox ucPictureBox = (ucPictureBox)sender;
			if (ucPictureBox.Parent == flowLayoutPanel1)
			{
				Control value = ucPictureBox;
				flowLayoutPanel1.Controls.Remove(value);
				string sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + iconguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
				ahp6.ExecuteSql(sql, (OleDbParameter[])null);
			}
			else
			{
				foreach (ucPictureBox control in flowLayoutPanel1.Controls)
				{
					if (control.IconPguid == ucPictureBox.IconPguid)
					{
						MessageBox.Show("已添加该图符!");
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
				string sql = "select PGUID from ICONDUIYING_H0001Z000E00 where ISDELETE = 0 and ICONGUID = '" + iconguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
				DataTable dataTable = ahp6.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + iconguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
					ahp6.ExecuteSql(sql, (OleDbParameter[])null);
				}
				else
				{
					sql = "insert into ICONDUIYING_H0001Z000E00 (PGUID, S_UDTIME, LEVELGUID, ICONGUID, UNITEID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + text + "', '" + iconguid + "', '" + unitid + "')";
					ahp6.ExecuteSql(sql, (OleDbParameter[])null);
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
			Get_Property_Data(dataGridView2, iconguid, text);
			text = "{B55806E6-9D63-4666-B6EB-AAB80814648E}";
			Get_Property_Data(dataGridView3, iconguid, text);
		}

		private void Get_Property_Data(DataGridView dgv, string iconguid, string typeguid)
		{
			Prop_GUID = new List<string>();
			Show_Name = new Dictionary<string, string>();
			Show_FDName = new Dictionary<string, string>();
			inherit_GUID = new Dictionary<string, string>();
			Show_Value = new Dictionary<string, string>();
			dgv.Columns.Clear();
			string sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_H0001Z000K00 where ISDELETE = 0 and UPGUID = '" + iconguid + "' and PROTYPEGUID = '" + typeguid + "' order by SHOWINDEX";
			DataTable proptable = ahp2.ExecuteDataTable(sql, (OleDbParameter[])null);
			Add_Prop(proptable);
			sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_H0001Z000K01 where ISDELETE = 0 and UPGUID = '" + iconguid + "' and PROTYPEGUID = '" + typeguid + "' order by SHOWINDEX";
			proptable = ahp3.ExecuteDataTable(sql, (OleDbParameter[])null);
			Add_Prop(proptable);
			List<string> list = new List<string>();
			bool flag = false;
			for (int i = 0; i < Prop_GUID.Count; i++)
			{
				string text = Prop_GUID[i];
				flag = true;
				DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
				dataGridViewTextBoxColumn.Name = text;
				dataGridViewTextBoxColumn.HeaderText = Show_Name[text];
				dgv.Columns.Add(dataGridViewTextBoxColumn);
				list.Add(Show_Value[text]);
			}
			if (flag)
			{
				dgv.Rows.Add(list.ToArray());
			}
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

		private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStrip3.Show(Control.MousePosition.X, Control.MousePosition.Y);
			}
		}

		private void dataGridView_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuStrip2.Show(Control.MousePosition.X, Control.MousePosition.Y);
			}
		}

		private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = treeView1.SelectedNode;
			if (selectedNode != null)
			{
				string text = selectedNode.Tag.ToString();
				int selectedIndex = tabControl2.SelectedIndex;
				FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)tabControl2.TabPages[selectedIndex].Controls[0];
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
						DataTable dataTable = ahp6.ExecuteDataTable(sql, (OleDbParameter[])null);
						if (dataTable.Rows.Count > 0)
						{
							sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 0, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
							ahp6.ExecuteSql(sql, (OleDbParameter[])null);
						}
						else
						{
							sql = "insert into ICONDUIYING_H0001Z000E00 (PGUID, S_UDTIME, LEVELGUID, ICONGUID, UNITEID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + text + "', '" + control.IconPguid + "', '" + unitid + "')";
							ahp6.ExecuteSql(sql, (OleDbParameter[])null);
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

		private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TreeNode selectedNode = treeView1.SelectedNode;
			if (selectedNode != null)
			{
				string text = selectedNode.Tag.ToString();
				foreach (ucPictureBox control in flowLayoutPanel1.Controls)
				{
					string sql = "update ICONDUIYING_H0001Z000E00 set ISDELETE = 1, S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where ICONGUID = '" + control.IconPguid + "' and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
					ahp6.ExecuteSql(sql, (OleDbParameter[])null);
				}
				flowLayoutPanel1.Controls.Clear();
			}
		}

		private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
		{
			TabPage selectedTab = tabControl2.SelectedTab;
			FlowLayoutPanel flowLayoutPanel = (FlowLayoutPanel)selectedTab.Controls[0];
			foreach (ucPictureBox control in flowLayoutPanel.Controls)
			{
				control.IconCheck = false;
			}
		}

		private void Delete_Path()
		{
			string text = WorkPath + "Publish\\googlemap\\map";
			string text2 = WorkPath + "Publish\\googlemap\\satellite";
			string text3 = "";
			if (Directory.Exists(text))
			{
				text3 = text3 + text + ",";
			}
			if (Directory.Exists(text2))
			{
				text3 += text2;
			}
			Process process = Process.Start(WorkPath + "DeleteDir.exe", text3);
			process.WaitForExit();
		}

		private void Load_Unit_Level()
		{
			GL_NAME = new Dictionary<string, string>();
			GL_JDCODE = new Dictionary<string, string>();
			GL_UPGUID = new Dictionary<string, string>();
			GL_MAP = new Dictionary<string, string>();
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
		}

		private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
		{
			e.DrawDefault = true;
		}

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeNode selectedNode = treeView1.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			string text = selectedNode.Tag.ToString();
			flowLayoutPanel1.Controls.Clear();
			Show_Icon_List(text);
			for (int i = 0; i < checkedListBox1.Items.Count; i++)
			{
				if (checkedListBox1.GetItemChecked(i))
				{
					checkedListBox1.SetItemChecked(i, value: false);
				}
			}
			string sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
			DataTable dataTable = ahp6.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				string text2 = dataTable.Rows[0]["MAPLEVEL"].ToString();
				string[] array = text2.Split(',');
				for (int i = 0; i < array.Length; i++)
				{
					for (int j = 0; j < checkedListBox1.Items.Count; j++)
					{
						if (checkedListBox1.Items[j].ToString() == array[i])
						{
							checkedListBox1.SetItemChecked(j, value: true);
							break;
						}
					}
				}
			}
			int selectedIndex = checkedListBox1.SelectedIndex;
			if (selectedIndex >= 0)
			{
				if (!Before_ShowMap)
				{
					mapHelper1.ShowMap(int.Parse(checkedListBox1.Items[selectedIndex].ToString()), checkedListBox1.Items[selectedIndex].ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
				}
				else
				{
					mapHelper1.setMapLevel(int.Parse(checkedListBox1.Items[selectedIndex].ToString()), checkedListBox1.Items[selectedIndex].ToString().ToString());
				}
				Before_ShowMap = true;
				if (borderDic != null)
				{
					Dictionary<string, List<double[]>> dictionary = (Dictionary<string, List<double[]>>)borderDic["path"];
					foreach (KeyValuePair<string, List<double[]>> item in dictionary)
					{
						Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
						dictionary2["type"] = borderDic["type"];
						dictionary2["width"] = borderDic["width"];
						dictionary2["color"] = borderDic["color"];
						dictionary2["opacity"] = borderDic["opacity"];
						dictionary2["path"] = item.Value;
						mapHelper1.DrawBorder(unitid, dictionary2);
					}
				}
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

		private void Show_Map_List(string tmp)
		{
			int num = tmp.LastIndexOf("roadmap");
			if (num < 0)
			{
				num = tmp.LastIndexOf("satellite");
			}
			if (num < 0)
			{
				num = tmp.LastIndexOf("satellite_en");
			}
			if (num > 0)
			{
				tmp = tmp.Substring(0, num);
			}
			textBox1.Text = tmp;
			checkedListBox1.Items.Clear();
			string path = textBox1.Text + "\\satellite_en";
			if (!Directory.Exists(path))
			{
				MessageBox.Show("请下载或导入混合图(无偏移)");
				textBox1.Text = "";
				return;
			}
			path = textBox1.Text + "\\roadmap";
			if (!Directory.Exists(path))
			{
				MessageBox.Show("请下载或导入街道图");
				textBox1.Text = "";
				return;
			}
			inip.WriteString("mapproperties", unitid, textBox1.Text);
			inip = new IniOperator(WorkPath + "Publish\\parameter.ini");
			inip.WriteString("Individuation", "mappath", textBox1.Text);
			folds = Directory.GetDirectories(path);
			for (int i = 0; i < folds.Length; i++)
			{
				num = folds[i].LastIndexOf("\\");
				folds[i] = folds[i].Substring(num + 1);
			}
			Array.Sort(folds, new CompStr());
			for (int i = 0; i < folds.Length; i++)
			{
				checkedListBox1.Items.Add(folds[i]);
			}
			mapHelper1.centerlat = double.Parse(textBox3.Text);
			mapHelper1.centerlng = double.Parse(textBox2.Text);
			mapHelper1.webpath = WorkPath + "Publish\\googlemap";
			mapHelper1.roadmappath = tmp + "\\roadmap";
			mapHelper1.satellitemappath = tmp + "\\satellite_en";
			mapHelper1.iconspath = WorkPath + "Publish\\PNGICONFOLDER";
			mapHelper1.maparr = folds;
			if (checkedListBox1.Items.Count > 0)
			{
				checkedListBox1.SelectedIndex = 0;
			}
		}

		private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			button3.Enabled = true;
			int selectedIndex = checkedListBox1.SelectedIndex;
			if (!Before_ShowMap)
			{
				mapHelper1.ShowMap(int.Parse(checkedListBox1.Items[selectedIndex].ToString()), checkedListBox1.Items[selectedIndex].ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
			}
			else
			{
				mapHelper1.setMapLevel(int.Parse(checkedListBox1.Items[selectedIndex].ToString()), checkedListBox1.Items[selectedIndex].ToString());
			}
			if (borderDic != null)
			{
				Dictionary<string, List<double[]>> dictionary = (Dictionary<string, List<double[]>>)borderDic["path"];
				foreach (KeyValuePair<string, List<double[]>> item in dictionary)
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					dictionary2["type"] = borderDic["type"];
					dictionary2["width"] = borderDic["width"];
					dictionary2["color"] = borderDic["color"];
					dictionary2["opacity"] = borderDic["opacity"];
					dictionary2["path"] = item.Value;
					mapHelper1.DrawBorder(unitid, dictionary2);
				}
			}
			Before_ShowMap = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (textBox2.Text.Trim().Equals(""))
			{
				MessageBox.Show("请输入经度");
				textBox2.Focus();
				return;
			}
			if (textBox3.Text.Trim().Equals(""))
			{
				MessageBox.Show("请输入纬度");
				textBox3.Focus();
				return;
			}
			inip = new IniOperator(WorkPath + "Publish\\parameter.ini");
			folderBrowserDialog1.SelectedPath = inip.ReadString("mapproperties", unitid, "");
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				string selectedPath = folderBrowserDialog1.SelectedPath;
				Show_Map_List(selectedPath);
			}
		}

		private bool Check_Map_LngLat()
		{
			string text = textBox1.Text + "\\satellite_en";
			folds = Directory.GetDirectories(text);
			for (int i = 0; i < folds.Length; i++)
			{
				int num = folds[i].LastIndexOf("\\");
				folds[i] = folds[i].Substring(num + 1);
			}
			Array.Sort(folds, new CompStr());
			double num2 = double.Parse(textBox2.Text);
			double num3 = double.Parse(textBox3.Text);
			num2 = Math.Pow(2.0, int.Parse(folds[folds.Length - 1]) - 1) * (1.0 + num2 / 180.0);
			num3 = Math.Pow(2.0, int.Parse(folds[folds.Length - 1]) - 1) * (1.0 - Math.Log(Math.Tan(Math.PI * num3 / 180.0) + 1.0 / Math.Cos(Math.PI * num3 / 180.0)) / Math.PI);
			string str = text + "\\" + folds[folds.Length - 1];
			int num4 = (int)Math.Floor(num2);
			int num5 = (int)Math.Ceiling(num3);
			str = str + "\\" + num4.ToString();
			if (Directory.Exists(str))
			{
				str = str + "\\" + num5.ToString() + ".jpg";
				if (File.Exists(str))
				{
					return true;
				}
			}
			return false;
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			textBox2.Text = slng;
			textBox3.Text = slat;
			inip = new IniOperator(WorkPath + "Publish\\parameter.ini");
			inip.WriteString("mapproperties", "centerlat", slat);
			inip.WriteString("mapproperties", "centerlng", slng);
			string sql = "update ORGCENTERDATA set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LNG = '" + slng + "', LAT = '" + slat + "' where ISDELETE = 0 and UNITEID = '" + unitid + "'";
			ahp5.ExecuteSql(sql, (OleDbParameter[])null);
			mapHelper1.centerlat = double.Parse(textBox3.Text);
			mapHelper1.centerlng = double.Parse(textBox2.Text);
			int selectedIndex = checkedListBox1.SelectedIndex;
			if (!Before_ShowMap)
			{
				mapHelper1.ShowMap(int.Parse(checkedListBox1.Items[selectedIndex].ToString()), checkedListBox1.Items[selectedIndex].ToString(), canEdit: false, map_type, null, null, null, 1.0, 400);
			}
			else
			{
				mapHelper1.setMapLevel(int.Parse(checkedListBox1.Items[selectedIndex].ToString()), checkedListBox1.Items[selectedIndex].ToString());
			}
			Before_ShowMap = true;
		}

		private void mapHelper1_MapRightClick(bool canedit, double lat, double lng, int x, int y)
		{
			contextMenuStrip1.Show(Control.MousePosition.X, Control.MousePosition.Y);
			slat = string.Concat(lat);
			slng = string.Concat(lng);
		}

		private void checkedListBox1_Leave(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < checkedListBox1.Items.Count; i++)
			{
				if (checkedListBox1.GetItemChecked(i))
				{
					list.Add(checkedListBox1.Items[i].ToString());
				}
			}
			TreeNode selectedNode = treeView1.SelectedNode;
			if (selectedNode != null)
			{
				string text = selectedNode.Tag.ToString();
				string sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
				DataTable dataTable = ahp6.ExecuteDataTable(sql, (OleDbParameter[])null);
				string text2 = string.Join(",", list);
				GL_MAP[text] = text2;
				if (dataTable.Rows.Count > 0)
				{
					sql = "update MAPDUIYING_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', MAPLEVEL = '" + text2 + "' where ISDELETE = 0 and LEVELGUID = '" + text + "' and UNITEID = '" + unitid + "'";
					ahp6.ExecuteSql(sql, (OleDbParameter[])null);
				}
				else
				{
					sql = "insert into MAPDUIYING_H0001Z000E00 (PGUID, S_UDTIME, LEVELGUID, MAPLEVEL, UNITEID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + text + "', '" + text2 + "', '" + unitid + "')";
					ahp6.ExecuteSql(sql, (OleDbParameter[])null);
				}
			}
		}

		private void MapForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			groupBox1.Focus();
			TreeNode selectedNode = treeView1.SelectedNode;
			if (selectedNode == null)
			{
				return;
			}
			string text = selectedNode.Tag.ToString();
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string path = WorkPath + "Publish\\googlemap\\map";
			if (Directory.Exists(path))
			{
				list = Directory.GetDirectories(path).ToList();
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = Path.GetFileNameWithoutExtension(list[i]);
			}
			string sql = "select MAPLEVEL from MAPDUIYING_H0001Z000E00 where ISDELETE = 0 and UNITEID = '" + unitid + "'";
			DataTable dataTable = ahp6.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string text2 = dataTable.Rows[i]["MAPLEVEL"].ToString();
				if (text2 != string.Empty)
				{
					list2.AddRange(text2.Split(','));
				}
			}
			list2 = list2.Distinct().ToList();
			for (int i = list2.Count - 1; i >= 0; i--)
			{
				if (list2[i] == string.Empty)
				{
					list2.Remove(list2[i]);
				}
			}
			List<string> list3 = list.Except(list2).ToList();
			List<string> list4 = list2.Except(list).ToList();
			if (list3.Count + list4.Count != 0)
			{
				inip = new IniOperator(WorkPath + "Publish\\RegInfo.ini");
				inip.WriteString("Public", "UnitID", unitid);
				ahp1.CloseConn();
				ahp2.CloseConn();
				ahp3.CloseConn();
				ahp4.CloseConn();
				ahp5.CloseConn();
				ahp6.CloseConn();
			}
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

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Enabled = false;
			inip = new IniOperator(WorkPath + "Publish\\RegInfo.ini");
			string a = inip.ReadString("Public", "UnitID", "");
			if (a != unitid)
			{
				Delete_Path();
			}
		}

		private void mapHelper1_MapTypeChanged(string mapType)
		{
			map_type = mapType;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			inip = new IniOperator(WorkPath + "Publish\\RegInfo.ini");
			inip.WriteString("Public", "UnitID", unitid);
			inip.WriteString("Individuation", "mappath", textBox1.Text);
			Process process = Process.Start(WorkPath + "Publish\\MapSet.exe");
		}

		private void mapHelper1_LevelChanged(int lastLevel, int currLevel, string showLevel)
		{
			if (borderDic != null)
			{
				Dictionary<string, List<double[]>> dictionary = (Dictionary<string, List<double[]>>)borderDic["path"];
				foreach (KeyValuePair<string, List<double[]>> item in dictionary)
				{
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					dictionary2["type"] = borderDic["type"];
					dictionary2["width"] = borderDic["width"];
					dictionary2["color"] = borderDic["color"];
					dictionary2["opacity"] = borderDic["opacity"];
					dictionary2["path"] = item.Value;
					mapHelper1.DrawBorder(unitid, dictionary2);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.mapHelper1 = new MapHelper.MapHelper();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tabControl3 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.tabControl3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 758);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(222, 758);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "组织结构";
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 24);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(216, 731);
            this.treeView1.TabIndex = 0;
            this.treeView1.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.treeView1_DrawNode);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(222, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 758);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(232, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1273, 758);
            this.panel2.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1273, 758);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1267, 731);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.splitter3);
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.panel8);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1259, 699);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "地图对应";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.panel5);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(234, 66);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1022, 630);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "地图显示";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.mapHelper1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 24);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1016, 603);
            this.panel5.TabIndex = 6;
            // 
            // mapHelper1
            // 
            this.mapHelper1.BackColor = System.Drawing.Color.Black;
            this.mapHelper1.centerlat = 0D;
            this.mapHelper1.centerlng = 0D;
            this.mapHelper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapHelper1.iconspath = null;
            this.mapHelper1.Location = new System.Drawing.Point(0, 0);
            this.mapHelper1.maparr = null;
            this.mapHelper1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mapHelper1.Name = "mapHelper1";
            this.mapHelper1.roadmappath = null;
            this.mapHelper1.satellitemappath = null;
            this.mapHelper1.Size = new System.Drawing.Size(1016, 603);
            this.mapHelper1.TabIndex = 0;
            this.mapHelper1.webpath = null;
            this.mapHelper1.MapRightClick += new MapHelper.MapHelper.DlMapRightClick(this.mapHelper1_MapRightClick);
            this.mapHelper1.LevelChanged += new MapHelper.MapHelper.DlLevelChanged(this.mapHelper1_LevelChanged);
            this.mapHelper1.MapTypeChanged += new MapHelper.MapHelper.DlMapTypeChanged(this.mapHelper1_MapTypeChanged);
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(224, 66);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(10, 630);
            this.splitter3.TabIndex = 6;
            this.splitter3.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.checkedListBox1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Location = new System.Drawing.Point(3, 66);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(221, 630);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "已下载地图列表";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "13",
            "14",
            "15"});
            this.checkedListBox1.Location = new System.Drawing.Point(3, 24);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(215, 603);
            this.checkedListBox1.TabIndex = 2;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            this.checkedListBox1.Leave += new System.EventHandler(this.checkedListBox1_Leave);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.SystemColors.Control;
            this.panel8.Controls.Add(this.button3);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.label2);
            this.panel8.Controls.Add(this.label1);
            this.panel8.Controls.Add(this.textBox3);
            this.panel8.Controls.Add(this.textBox2);
            this.panel8.Controls.Add(this.button1);
            this.panel8.Controls.Add(this.textBox1);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1253, 63);
            this.panel8.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(1128, 15);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(120, 30);
            this.button3.TabIndex = 8;
            this.button3.Text = "导入边界线";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(498, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "地图文件夹";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "纬度：";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(134, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "经度：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "地图中心点";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(376, 18);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 28);
            this.textBox3.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(202, 18);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 28);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(1002, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "浏览";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(602, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(394, 28);
            this.textBox1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.splitter2);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1259, 699);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图符对应";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.tabControl3);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(161, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(367, 693);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "图符属性";
            // 
            // tabControl3
            // 
            this.tabControl3.Controls.Add(this.tabPage3);
            this.tabControl3.Controls.Add(this.tabPage4);
            this.tabControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl3.Location = new System.Drawing.Point(3, 24);
            this.tabControl3.Name = "tabControl3";
            this.tabControl3.SelectedIndex = 0;
            this.tabControl3.Size = new System.Drawing.Size(361, 666);
            this.tabControl3.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(353, 634);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "固定属性";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView2.Location = new System.Drawing.Point(3, 3);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 30;
            this.dataGridView2.Size = new System.Drawing.Size(347, 628);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dataGridView3);
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(353, 634);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "基础属性";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 3);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowTemplate.Height = 30;
            this.dataGridView3.Size = new System.Drawing.Size(347, 628);
            this.dataGridView3.TabIndex = 1;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(528, 3);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(10, 693);
            this.splitter2.TabIndex = 10;
            this.splitter2.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tabControl2);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(538, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(718, 693);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "图符库";
            // 
            // tabControl2
            // 
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 24);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(712, 666);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flowLayoutPanel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(158, 693);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "已对应图符";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(152, 666);
            this.flowLayoutPanel1.TabIndex = 4;
            this.flowLayoutPanel1.WrapContents = false;
            this.flowLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.flowLayoutPanel1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(333, 32);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(332, 28);
            this.toolStripMenuItem2.Text = "更新当前点经纬度到地图中心点";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "(*.txt)|*.txt";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(117, 32);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(116, 28);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // contextMenuStrip3
            // 
            this.contextMenuStrip3.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清空ToolStripMenuItem});
            this.contextMenuStrip3.Name = "contextMenuStrip3";
            this.contextMenuStrip3.Size = new System.Drawing.Size(117, 32);
            // 
            // 清空ToolStripMenuItem
            // 
            this.清空ToolStripMenuItem.Name = "清空ToolStripMenuItem";
            this.清空ToolStripMenuItem.Size = new System.Drawing.Size(116, 28);
            this.清空ToolStripMenuItem.Text = "清空";
            this.清空ToolStripMenuItem.Click += new System.EventHandler(this.清空ToolStripMenuItem_Click);
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1505, 758);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MapForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapForm_FormClosed);
            this.Load += new System.EventHandler(this.MapForm_Load);
            this.Shown += new System.EventHandler(this.MapForm_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.tabControl3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip3.ResumeLayout(false);
            this.ResumeLayout(false);

		}
	}
}

using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
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
	public class InfoSetForm : XtraForm
	{
		private AccessHelper ahp = null;

		public string unitid = "";

		public string markerguid = "";

		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private bool NeedChange = true;

		private List<Menu_Node> Menu_List;

		private IContainer components = null;

		private GroupControl groupControl1;

		private TreeList treeList1;

		private SplitterControl splitterControl1;

		private XtraOpenFileDialog xtraOpenFileDialog1;

		private GroupControl groupControl3;

		private SimpleButton simpleButton1;

		private TextEdit textEdit2;

		private LabelControl labelControl2;

		private LabelControl labelControl1;

		private ComboBoxEdit comboBoxEdit1;

		private SimpleButton simpleButton2;

		private PopupMenu popupMenu1;

		private BarManager barManager1;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private BarButtonItem barButtonItem1;

		private BarButtonItem barButtonItem2;

		private BarButtonItem barButtonItem3;

		private BarButtonItem barButtonItem4;

		public InfoSetForm()
		{
			InitializeComponent();
		}

		private void InfoSetForm_Shown(object sender, EventArgs e)
		{
			comboBoxEdit1.Enabled = false;
			simpleButton1.Enabled = false;
			simpleButton2.Enabled = false;
			textEdit2.Text = "";
			textEdit2.Enabled = false;
			ahp = new AccessHelper(WorkPath + "data\\ENVIR_H0001Z000E00.mdb");
			Read_Tree();
		}

		private void Read_Tree()
		{
			Menu_List = new List<Menu_Node>();
			string sql = "select PGUID, UPGUID, FUNCNAME, FUNCTION, ADDRESS from ENVIRLIST_H0001Z000E00 where ISDELETE = 0 and UNITID = '" + unitid + "' and MARKERID = '" + markerguid + "' order by SHOWINDEX";
			DataTable dataTable = ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				Menu_Node menu_Node = new Menu_Node();
				string pguid = dataTable.Rows[i]["PGUID"].ToString();
				string upguid = dataTable.Rows[i]["UPGUID"].ToString();
				menu_Node.pguid = pguid;
				menu_Node.upguid = upguid;
				menu_Node.name = dataTable.Rows[i]["FUNCNAME"].ToString();
				menu_Node.func = dataTable.Rows[i]["FUNCTION"].ToString();
				menu_Node.addr = dataTable.Rows[i]["ADDRESS"].ToString();
				Menu_List.Add(menu_Node);
			}
			treeList1.Nodes.Clear();
			treeList1.OptionsBehavior.ShowEditorOnMouseUp = true;
			treeList1.Appearance.FocusedCell.BackColor = Color.SteelBlue;
			treeList1.KeyFieldName = "pguid";
			treeList1.ParentFieldName = "upguid";
			treeList1.DataSource = Menu_List;
			treeList1.Columns[1].Visible = false;
			treeList1.Columns[2].Visible = false;
			treeList1.ExpandAll();
		}

		private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
		{
			if (e.Node == null)
			{
				return;
			}
			if (e.Node.ParentNode != null)
			{
				if (comboBoxEdit1.Properties.Items.Count >= 5)
				{
					comboBoxEdit1.Properties.Items.RemoveAt(4);
				}
			}
			else if (comboBoxEdit1.Properties.Items.Count < 5)
			{
				comboBoxEdit1.Properties.Items.Add("列表");
			}
			if (e.Node.Nodes.Count > 0)
			{
				comboBoxEdit1.Text = "列表";
				comboBoxEdit1.Enabled = false;
				simpleButton1.Enabled = false;
				simpleButton2.Enabled = false;
				textEdit2.Text = "";
				textEdit2.Enabled = false;
				return;
			}
			comboBoxEdit1.Enabled = true;
			simpleButton1.Enabled = true;
			simpleButton2.Enabled = false;
			textEdit2.Enabled = true;
			switch (e.Node.GetValue("func").ToString())
			{
			case "word":
				comboBoxEdit1.SelectedIndex = 0;
				break;
			case "pdf":
				comboBoxEdit1.SelectedIndex = 1;
				break;
			case "web":
				simpleButton1.Enabled = false;
				comboBoxEdit1.SelectedIndex = 2;
				break;
			case "exe":
				comboBoxEdit1.SelectedIndex = 3;
				break;
			case "list":
				simpleButton1.Enabled = false;
				comboBoxEdit1.SelectedIndex = 4;
				break;
			}
			if (NeedChange)
			{
				textEdit2.Text = e.Node.GetValue("addr").ToString();
			}
		}

		private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
		{
			textEdit2.Text = "";
			if (comboBoxEdit1.SelectedIndex == 2 || comboBoxEdit1.SelectedIndex == 4)
			{
				simpleButton1.Enabled = false;
			}
			else
			{
				simpleButton1.Enabled = true;
			}
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			switch (comboBoxEdit1.SelectedIndex)
			{
			case 0:
				xtraOpenFileDialog1.Filter = "Word文档|*.doc;*.docx";
				break;
			case 1:
				xtraOpenFileDialog1.Filter = "PDF文档|*.pdf";
				break;
			case 3:
				xtraOpenFileDialog1.Filter = "应用程序|*.exe";
				break;
			}
			if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textEdit2.Text = xtraOpenFileDialog1.FileName;
			}
		}

		private void simpleButton2_Click(object sender, EventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			if (focusedNode != null)
			{
				NeedChange = false;
				focusedNode.SetValue("func", textEdit2.Text);
				switch (comboBoxEdit1.SelectedIndex)
				{
				case 0:
					focusedNode.SetValue("func", "word");
					break;
				case 1:
					focusedNode.SetValue("func", "pdf");
					break;
				case 2:
					focusedNode.SetValue("func", "web");
					break;
				case 3:
					focusedNode.SetValue("func", "exe");
					break;
				case 4:
					focusedNode.SetValue("func", "list");
					break;
				}
				switch (comboBoxEdit1.SelectedIndex)
				{
				case 0:
				case 1:
				{
					string text = textEdit2.Text;
					string text2 = markerguid + focusedNode["pguid"].ToString();
					string extension = Path.GetExtension(text);
					string destFileName = WorkPath + "file\\" + text2 + extension;
					File.Copy(text, destFileName, overwrite: true);
					string sql = "update ENVIRLIST_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', FUNCTION = '" + focusedNode.GetValue("func").ToString() + "', ADDRESS = '" + text2 + extension + "' where ISDELETE = 0 and PGUID = '" + focusedNode.GetValue("pguid") + "' and UNITID = '" + unitid + "'";
					ahp.ExecuteSql(sql, (OleDbParameter[])null);
					break;
				}
				case 2:
				case 3:
				case 4:
				{
					string sql = "update ENVIRLIST_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', FUNCTION = '" + focusedNode.GetValue("func").ToString() + "', ADDRESS = '" + textEdit2.Text + "' where ISDELETE = 0 and PGUID = '" + focusedNode.GetValue("pguid") + "' and UNITID = '" + unitid + "'";
					ahp.ExecuteSql(sql, (OleDbParameter[])null);
					break;
				}
				}
				NeedChange = true;
				XtraMessageBox.Show("保存成功!");
			}
		}

		private void treeList1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
			{
				return;
			}
			TreeListNode nodeAt = treeList1.GetNodeAt(e.X, e.Y);
			if (nodeAt != null)
			{
				if (nodeAt.ParentNode != null)
				{
					barButtonItem2.Visibility = BarItemVisibility.Never;
				}
				else
				{
					barButtonItem2.Visibility = BarItemVisibility.Always;
				}
				barButtonItem3.Visibility = BarItemVisibility.Always;
				barButtonItem4.Visibility = BarItemVisibility.Always;
				popupMenu1.ShowPopup(barManager1, Control.MousePosition);
				treeList1.FocusedNode = nodeAt;
			}
			if (treeList1.Nodes.Count == 0)
			{
				barButtonItem2.Visibility = BarItemVisibility.Never;
				barButtonItem3.Visibility = BarItemVisibility.Never;
				barButtonItem4.Visibility = BarItemVisibility.Never;
				popupMenu1.ShowPopup(barManager1, Control.MousePosition);
			}
		}

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			Menu_Node menu_Node = new Menu_Node();
			menu_Node.pguid = Guid.NewGuid().ToString("B");
			if (focusedNode == null || focusedNode.ParentNode == null)
			{
				menu_Node.upguid = "";
			}
			else
			{
				menu_Node.upguid = focusedNode.ParentNode.GetValue("pguid").ToString();
			}
			InfoEditForm infoEditForm = new InfoEditForm();
			infoEditForm.Owner = this;
			if (infoEditForm.ShowDialog() == DialogResult.OK)
			{
				menu_Node.name = infoEditForm.EditText;
				menu_Node.func = "list";
				menu_Node.addr = "";
				string sql = "insert into ENVIRLIST_H0001Z000E00 (PGUID, S_UDTIME, UPGUID, FUNCNAME, FUNCTION, UNITID, MARKERID) values('" + menu_Node.pguid + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + menu_Node.upguid + "', '" + menu_Node.name + "', '" + menu_Node.func + "', '" + unitid + "', '" + markerguid + "')";
				ahp.ExecuteSql(sql, (OleDbParameter[])null);
				Read_Tree();
			}
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			Menu_Node menu_Node = new Menu_Node();
			menu_Node.pguid = Guid.NewGuid().ToString("B");
			menu_Node.upguid = focusedNode.GetValue("pguid").ToString();
			InfoEditForm infoEditForm = new InfoEditForm();
			if (infoEditForm.ShowDialog() == DialogResult.OK)
			{
				menu_Node.name = infoEditForm.EditText;
				menu_Node.func = "list";
				menu_Node.addr = "";
				string sql = "insert into ENVIRLIST_H0001Z000E00 (PGUID, S_UDTIME, UPGUID, FUNCNAME, FUNCTION, UNITID, MARKERID) values('" + menu_Node.pguid + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + menu_Node.upguid + "', '" + menu_Node.name + "', '" + menu_Node.func + "', '" + unitid + "', '" + markerguid + "')";
				ahp.ExecuteSql(sql, (OleDbParameter[])null);
				Read_Tree();
			}
		}

		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			InfoEditForm infoEditForm = new InfoEditForm();
			infoEditForm.EditText = focusedNode.GetValue("name").ToString();
			if (infoEditForm.ShowDialog() == DialogResult.OK)
			{
				focusedNode.SetValue("name", infoEditForm.EditText);
				string sql = "update ENVIRLIST_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', FUNCNAME = '" + infoEditForm.EditText + "' where ISDELETE = 0 and PGUID = '" + focusedNode.GetValue("pguid").ToString() + "' and UNITID = '" + unitid + "'";
				ahp.ExecuteSql(sql, (OleDbParameter[])null);
				treeList1.RefreshDataSource();
			}
		}

		private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
		{
			TreeListNode focusedNode = treeList1.FocusedNode;
			string text = focusedNode.GetValue("pguid").ToString();
			treeList1.DeleteNode(focusedNode);
			string sql = "update ENVIRLIST_H0001Z000E00 set ISDELETE = 1 where PGUID = '" + text + "' or UPGUID = '" + text + "' and UNITID = '" + unitid + "'";
			ahp.ExecuteSql(sql, (OleDbParameter[])null);
			treeList1.RefreshDataSource();
		}

		private void InfoSetForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			ahp.CloseConn();
		}

		private void treeList1_DragDrop(object sender, DragEventArgs e)
		{
			TreeList treeList = sender as TreeList;
			Point pt = treeList.PointToClient(new Point(e.X, e.Y));
			TreeListNode node = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
			TreeListNode node2 = treeList.CalcHitInfo(pt).Node;
			treeList.SetNodeIndex(node, treeList.GetNodeIndex(node2));
			e.Effect = DragDropEffects.None;
		}

		private void treeList1_AfterDragNode(object sender, AfterDragNodeEventArgs e)
		{
			int num = 0;
			foreach (TreeListNode node in treeList1.Nodes)
			{
				if (node.ParentNode == null)
				{
					string text = node.GetValue("pguid").ToString();
					string sql = "update ENVIRLIST_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', SHOWINDEX = " + num.ToString() + " where ISDELETE = 0 and PGUID = '" + text + "' and UNITID = '" + unitid + "'";
					ahp.ExecuteSql(sql, (OleDbParameter[])null);
					num++;
					UpDateNode(node);
				}
			}
		}

		private void UpDateNode(TreeListNode pNode)
		{
			int num = 0;
			foreach (TreeListNode node in pNode.Nodes)
			{
				string text = node.GetValue("pguid").ToString();
				string sql = "update ENVIRLIST_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', SHOWINDEX = " + num.ToString() + " where ISDELETE = 0 and PGUID = '" + text + "' and UNITID = '" + unitid + "'";
				ahp.ExecuteSql(sql, (OleDbParameter[])null);
				num++;
				UpDateNode(node);
			}
		}

		private void textEdit2_EditValueChanged(object sender, EventArgs e)
		{
			if (textEdit2.Text != "")
			{
				simpleButton2.Enabled = true;
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
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.InfoSetForm));
			groupControl1 = new DevExpress.XtraEditors.GroupControl();
			treeList1 = new DevExpress.XtraTreeList.TreeList();
			splitterControl1 = new DevExpress.XtraEditors.SplitterControl();
			xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(components);
			groupControl3 = new DevExpress.XtraEditors.GroupControl();
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			textEdit2 = new DevExpress.XtraEditors.TextEdit();
			labelControl2 = new DevExpress.XtraEditors.LabelControl();
			labelControl1 = new DevExpress.XtraEditors.LabelControl();
			popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
			barManager1 = new DevExpress.XtraBars.BarManager(components);
			barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
			groupControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)treeList1).BeginInit();
			((System.ComponentModel.ISupportInitialize)groupControl3).BeginInit();
			groupControl3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEdit1.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)textEdit2.Properties).BeginInit();
			((System.ComponentModel.ISupportInitialize)popupMenu1).BeginInit();
			((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
			SuspendLayout();
			groupControl1.Controls.Add(treeList1);
			groupControl1.Dock = System.Windows.Forms.DockStyle.Left;
			groupControl1.Location = new System.Drawing.Point(0, 0);
			groupControl1.Name = "groupControl1";
			groupControl1.Size = new System.Drawing.Size(219, 492);
			groupControl1.TabIndex = 0;
			groupControl1.Text = "菜单列表";
			treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
			treeList1.Location = new System.Drawing.Point(2, 31);
			treeList1.Name = "treeList1";
			treeList1.OptionsBehavior.Editable = false;
			treeList1.OptionsDragAndDrop.DragNodesMode = DevExpress.XtraTreeList.DragNodesMode.Single;
			treeList1.Size = new System.Drawing.Size(215, 459);
			treeList1.TabIndex = 0;
			treeList1.ViewStyle = DevExpress.XtraTreeList.TreeListViewStyle.TreeView;
			treeList1.AfterDragNode += new DevExpress.XtraTreeList.AfterDragNodeEventHandler(treeList1_AfterDragNode);
			treeList1.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler(treeList1_FocusedNodeChanged);
			treeList1.DragDrop += new System.Windows.Forms.DragEventHandler(treeList1_DragDrop);
			treeList1.MouseDown += new System.Windows.Forms.MouseEventHandler(treeList1_MouseDown);
			splitterControl1.Location = new System.Drawing.Point(219, 0);
			splitterControl1.Name = "splitterControl1";
			splitterControl1.Size = new System.Drawing.Size(7, 492);
			splitterControl1.TabIndex = 1;
			splitterControl1.TabStop = false;
			xtraOpenFileDialog1.FileName = null;
			groupControl3.Controls.Add(simpleButton2);
			groupControl3.Controls.Add(comboBoxEdit1);
			groupControl3.Controls.Add(simpleButton1);
			groupControl3.Controls.Add(textEdit2);
			groupControl3.Controls.Add(labelControl2);
			groupControl3.Controls.Add(labelControl1);
			groupControl3.Dock = System.Windows.Forms.DockStyle.Fill;
			groupControl3.Location = new System.Drawing.Point(226, 0);
			groupControl3.Name = "groupControl3";
			groupControl3.Size = new System.Drawing.Size(529, 492);
			groupControl3.TabIndex = 9;
			groupControl3.Text = "菜单设置";
			simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton2.Location = new System.Drawing.Point(213, 329);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(112, 34);
			simpleButton2.TabIndex = 6;
			simpleButton2.Text = "保存";
			simpleButton2.Click += new System.EventHandler(simpleButton2_Click);
			comboBoxEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
			comboBoxEdit1.Location = new System.Drawing.Point(195, 155);
			comboBoxEdit1.Name = "comboBoxEdit1";
			comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[1]
			{
				new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)
			});
			comboBoxEdit1.Properties.Items.AddRange(new object[5]
			{
				"文档",
				"PDF",
				"网页",
				"应用程序",
				"列表"
			});
			comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			comboBoxEdit1.Size = new System.Drawing.Size(155, 30);
			comboBoxEdit1.TabIndex = 5;
			comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(comboBoxEdit1_SelectedIndexChanged);
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
			simpleButton1.Location = new System.Drawing.Point(366, 240);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(117, 34);
			simpleButton1.TabIndex = 4;
			simpleButton1.Text = "浏览";
			simpleButton1.Click += new System.EventHandler(simpleButton1_Click);
			textEdit2.Anchor = System.Windows.Forms.AnchorStyles.None;
			textEdit2.Location = new System.Drawing.Point(195, 243);
			textEdit2.Name = "textEdit2";
			textEdit2.Size = new System.Drawing.Size(155, 30);
			textEdit2.TabIndex = 3;
			textEdit2.EditValueChanged += new System.EventHandler(textEdit2_EditValueChanged);
			labelControl2.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl2.Location = new System.Drawing.Point(54, 247);
			labelControl2.Name = "labelControl2";
			labelControl2.Size = new System.Drawing.Size(115, 22);
			labelControl2.TabIndex = 1;
			labelControl2.Text = "文件/网页地址";
			labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
			labelControl1.Location = new System.Drawing.Point(73, 163);
			labelControl1.Name = "labelControl1";
			labelControl1.Size = new System.Drawing.Size(72, 22);
			labelControl1.TabIndex = 0;
			labelControl1.Text = "菜单类型";
			popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[4]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem1),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem2),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem3),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem4)
			});
			popupMenu1.Manager = barManager1;
			popupMenu1.Name = "popupMenu1";
			barButtonItem1.Caption = "添加菜单";
			barButtonItem1.Id = 4;
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
			barButtonItem2.Caption = "添加子菜单";
			barButtonItem2.Id = 5;
			barButtonItem2.Name = "barButtonItem2";
			barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem2_ItemClick);
			barButtonItem3.Caption = "编辑";
			barButtonItem3.Id = 6;
			barButtonItem3.Name = "barButtonItem3";
			barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem3_ItemClick);
			barButtonItem4.Caption = "删除";
			barButtonItem4.Id = 7;
			barButtonItem4.Name = "barButtonItem4";
			barButtonItem4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem4_ItemClick);
			barManager1.DockControls.Add(barDockControlTop);
			barManager1.DockControls.Add(barDockControlBottom);
			barManager1.DockControls.Add(barDockControlLeft);
			barManager1.DockControls.Add(barDockControlRight);
			barManager1.Form = this;
			barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[4]
			{
				barButtonItem1,
				barButtonItem2,
				barButtonItem3,
				barButtonItem4
			});
			barManager1.MaxItemId = 8;
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			barDockControlTop.Location = new System.Drawing.Point(0, 0);
			barDockControlTop.Manager = barManager1;
			barDockControlTop.Size = new System.Drawing.Size(755, 0);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			barDockControlBottom.Location = new System.Drawing.Point(0, 492);
			barDockControlBottom.Manager = barManager1;
			barDockControlBottom.Size = new System.Drawing.Size(755, 0);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			barDockControlLeft.Location = new System.Drawing.Point(0, 0);
			barDockControlLeft.Manager = barManager1;
			barDockControlLeft.Size = new System.Drawing.Size(0, 492);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			barDockControlRight.Location = new System.Drawing.Point(755, 0);
			barDockControlRight.Manager = barManager1;
			barDockControlRight.Size = new System.Drawing.Size(0, 492);
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(755, 492);
			base.Controls.Add(groupControl3);
			base.Controls.Add(splitterControl1);
			base.Controls.Add(groupControl1);
			base.Controls.Add(barDockControlLeft);
			base.Controls.Add(barDockControlRight);
			base.Controls.Add(barDockControlBottom);
			base.Controls.Add(barDockControlTop);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "InfoSetForm";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "菜单设置";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(InfoSetForm_FormClosed);
			base.Shown += new System.EventHandler(InfoSetForm_Shown);
			((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
			groupControl1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)treeList1).EndInit();
			((System.ComponentModel.ISupportInitialize)groupControl3).EndInit();
			groupControl3.ResumeLayout(performLayout: false);
			groupControl3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)comboBoxEdit1.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)textEdit2.Properties).EndInit();
			((System.ComponentModel.ISupportInitialize)popupMenu1).EndInit();
			((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

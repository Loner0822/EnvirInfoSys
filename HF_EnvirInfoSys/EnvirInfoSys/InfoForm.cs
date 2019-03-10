using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Spire.DocViewer.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ucPropertyGrid;

namespace EnvirInfoSys
{
	public class InfoForm : XtraForm
	{
		public delegate void StopShine(string pguid);

		private InputLanguageCollection langs = InputLanguage.InstalledInputLanguages;

		private AccessHelper ahp1 = null;

		private AccessHelper ahp2 = null;

		private AccessHelper ahp3 = null;

		private AccessHelper ahp4 = null;

		public bool Update_Data = false;

		public bool CanEdit = true;

		public string unitid = "";

		public string Node_GUID = "";

		public string Icon_GUID = "";

		public string Node_Name = "";

		public string JdCode = "";

		public Dictionary<string, string> FDName_Value;

		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private string AccessPath1 = AppDomain.CurrentDomain.BaseDirectory + "data\\ENVIR_H0001Z000E00.mdb";

		private string AccessPath2 = AppDomain.CurrentDomain.BaseDirectory + "data\\ZSK_H0001Z000K00.mdb";

		private string AccessPath3 = AppDomain.CurrentDomain.BaseDirectory + "data\\ZSK_H0001Z000K01.mdb";

		private string AccessPath4 = AppDomain.CurrentDomain.BaseDirectory + "data\\ZSK_H0001Z000E00.mdb";

		private string IniFilePath = AppDomain.CurrentDomain.BaseDirectory + "parameter.ini";

		private Dictionary<string, string> Show_Name;

		private Dictionary<string, string> Show_FDName;

		private Dictionary<string, string> inherit_GUID;

		private Dictionary<string, string> Show_Value;

		private Dictionary<string, string> Show_DB;

		private List<string> Menu_GUID;

		private Dictionary<string, string> Menu_Upguid;

		private Dictionary<string, string> Menu_Name;

		private Dictionary<string, string> Menu_Func;

		private Dictionary<string, string> Menu_Addr;

		private Dictionary<string, List<string>> Menu_List;

		private DocDocumentViewer documentviewer = new DocDocumentViewer();

		private IContainer components = null;

		private BarManager barManager1;

		private Bar bar2;

		private Bar bar3;

		private BarDockControl barDockControlTop;

		private BarDockControl barDockControlBottom;

		private BarDockControl barDockControlLeft;

		private BarDockControl barDockControlRight;

		private Bar bar1;

		private PanelControl panelControl1;

		private WebBrowser webBrowser1;

		private PropertyGrid propertyGrid1;

		private ListView listView1;

		private ImageList imageList1;

		private BackgroundWorker backgroundWorker1;

		private PopupMenu popupMenu1;

		private BarButtonItem barButtonItem1;

		private BarButtonItem barButtonItem2;

		private XtraOpenFileDialog xtraOpenFileDialog1;

		public event StopShine stopshine;

		public InfoForm()
		{
			InitializeComponent();
		}

		private void InfoForm_LostFocus(object sender, EventArgs e)
		{
			Close();
		}

		private Dictionary<string, string> Get_Prop_Type()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("{26E232C8-595F-44E5-8E0F-8E0FC1BD7D24}", "固定属性");
			dictionary.Add("{B55806E6-9D63-4666-B6EB-AAB80814648E}", "基础属性");
			dictionary.Add("{D7DE9C5E-253C-491C-A380-06E41C68D2C8}", "扩展属性");
			return dictionary;
		}

		private List<string> Get_Prop_List(Dictionary<string, string> prop_type)
		{
			List<string> list = new List<string>();
			List<string> list2 = new List<string>(prop_type.Keys);
			for (int i = 1; i < list2.Count; i++)
			{
				string text = "";
				AccessHelper accessHelper = null;
				if (i == 1)
				{
					text = "H0001Z000K01";
					string sql = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_" + text + " where ISDELETE = 0 and UPGUID = '" + Icon_GUID + "' and PROTYPEGUID = '" + list2[i] + "' order by SHOWINDEX";
					DataTable dataTable = ahp3.ExecuteDataTable(sql, (OleDbParameter[])null);
					for (int j = 0; j < dataTable.Rows.Count; j++)
					{
						list.Add(dataTable.Rows[j]["PGUID"].ToString());
						Show_Name[dataTable.Rows[j]["PGUID"].ToString()] = dataTable.Rows[j]["PROPNAME"].ToString();
						Show_FDName[dataTable.Rows[j]["PGUID"].ToString()] = dataTable.Rows[j]["FDNAME"].ToString();
						inherit_GUID[dataTable.Rows[j]["PGUID"].ToString()] = dataTable.Rows[j]["SOURCEGUID"].ToString();
						Show_Value[dataTable.Rows[j]["PGUID"].ToString()] = dataTable.Rows[j]["PROPVALUE"].ToString();
						Show_DB[dataTable.Rows[j]["PGUID"].ToString()] = text;
					}
					text = "H0001Z000K00";
					accessHelper = ahp2;
				}
				if (i == 2)
				{
					text = "H0001Z000E00";
					accessHelper = ahp4;
				}
				string str = "select PGUID, PROPNAME, FDNAME, SOURCEGUID, PROPVALUE from ZSK_PROP_" + text + " where ISDELETE = 0 and UPGUID = '" + Icon_GUID + "' and PROTYPEGUID = '" + list2[i] + "'";
				if (i == 2)
				{
					str = str + " and UNITID = '" + unitid + "'";
				}
				str += " order by SHOWINDEX";
				DataTable dataTable2 = accessHelper.ExecuteDataTable(str, (OleDbParameter[])null);
				for (int j = 0; j < dataTable2.Rows.Count; j++)
				{
					list.Add(dataTable2.Rows[j]["PGUID"].ToString());
					Show_Name[dataTable2.Rows[j]["PGUID"].ToString()] = dataTable2.Rows[j]["PROPNAME"].ToString();
					Show_FDName[dataTable2.Rows[j]["PGUID"].ToString()] = dataTable2.Rows[j]["FDNAME"].ToString();
					inherit_GUID[dataTable2.Rows[j]["PGUID"].ToString()] = dataTable2.Rows[j]["SOURCEGUID"].ToString();
					Show_Value[dataTable2.Rows[j]["PGUID"].ToString()] = dataTable2.Rows[j]["PROPVALUE"].ToString();
					Show_DB[dataTable2.Rows[j]["PGUID"].ToString()] = text;
				}
			}
			return list;
		}

		private string Get_Data_Type(string propguid)
		{
			string text = Show_DB[propguid];
			AccessHelper accessHelper = null;
			accessHelper = ((text == "H0001Z000K00") ? ahp2 : ((!(text == "H0001Z000K01")) ? ahp4 : ahp3));
			string text2 = Icon_GUID + "_" + propguid;
			DataTable dataTable;
			string sql;
			if (inherit_GUID[propguid] != "")
			{
				propguid = inherit_GUID[propguid];
				sql = "select UPGUID from ZSK_PROP_" + text + " where ISDELETE = 0 and PGUID = '" + propguid + "'";
				dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count != 0)
				{
					text2 = dataTable.Rows[0]["UPGUID"].ToString() + "_" + propguid;
				}
			}
			sql = "select DATATYPE from  ZSK_DATATYPE_" + text + " where ISDELETE = 0 and UPGUID = '" + text2 + "'";
			dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count != 0)
			{
				if (dataTable.Rows[0]["DATATYPE"].ToString() != "可选项")
				{
					return dataTable.Rows[0]["DATATYPE"].ToString();
				}
				sql = "select PROPVALUE from ZSK_LIMIT_" + text + " where ISDELETE = 0 and UPGUID = '" + text2 + "'";
				DataTable dataTable2 = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable2.Rows.Count != 0)
				{
					if (dataTable2.Rows[0]["PROPVALUE"].ToString() == "否")
					{
						return "可选项";
					}
					return "多选";
				}
			}
			return "文本";
		}

		private string Get_fw(string propguid)
		{
			string text = Show_DB[propguid];
			string text2 = Icon_GUID + "_" + propguid;
			string text3 = "";
			AccessHelper accessHelper = (text == "H0001Z000K00") ? ahp2 : ((!(text == "H0001Z000K01")) ? ahp4 : ahp3);
			string sql;
			DataTable dataTable;
			if (inherit_GUID[propguid] != "")
			{
				propguid = inherit_GUID[propguid];
				sql = "select UPGUID from ZSK_PROP_" + text + " where ISDELETE = 0 and PGUID = '" + propguid + "'";
				dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count != 0)
				{
					text2 = dataTable.Rows[0]["UPGUID"].ToString() + "_" + propguid;
				}
			}
			sql = "select COMBOSTR from  ZSK_COMBOSTRLIST_" + text + " where ISDELETE = 0 and UPGUID = '" + text2 + "' order by SHOWINDEX";
			dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				text3 = ((i != 0) ? (text3 + "," + dataTable.Rows[i]["COMBOSTR"].ToString()) : (text3 + dataTable.Rows[i]["COMBOSTR"].ToString()));
			}
			return text3;
		}

		private void BaseInfo()
		{
			PropertyManageCls propertyManageCls = new PropertyManageCls();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Show_Name = new Dictionary<string, string>();
			Show_FDName = new Dictionary<string, string>();
			Show_Value = new Dictionary<string, string>();
			inherit_GUID = new Dictionary<string, string>();
			Show_DB = new Dictionary<string, string>();
			dictionary = Get_Prop_Type();
			List<string> list = Get_Prop_List(dictionary);
			propertyGrid1.Enabled = true;
			string sql = "select MAKRENAME, REGINFO from ENVIRICONDATA_H0001Z000E00 where ISDELETE = 0 and PGUID = '" + Node_GUID + "' and UNITEID = '" + unitid + "'";
			DataTable dataTable = ahp1.ExecuteDataTable(sql, (OleDbParameter[])null);
			Property property = new Property("名称", dataTable.Rows[0]["MAKRENAME"]);
			property.Category = "\t注册信息";
			property.ReadOnly = true;
			propertyManageCls.Add(property);
			property = new Property("注册地址", dataTable.Rows[0]["REGINFO"]);
			property.Category = "\t注册信息";
			property.ReadOnly = true;
			propertyManageCls.Add(property);
			List<string> list2 = new List<string>(dictionary.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				if (Show_Name[list[i]] == "名称")
				{
					continue;
				}
				Property property2 = new Property(Show_Name[list[i]], "", sReadonly: false, sVisible: true);
				property2.DisplayName = Show_Name[list[i]];
				int index = (Show_DB[list[i]] == "H0001Z000K00" || Show_DB[list[i]] == "H0001Z000K01") ? 1 : 2;
				property2.Category = dictionary[list2[index]];
				property2.ReadOnly = false;
				property2.FdName = Show_FDName[list[i]];
				if (!Update_Data)
				{
					property2.Value = Show_Value[list[i]];
				}
				else
				{
					sql = "select " + Show_FDName[list[i]] + " from " + JdCode + " where ISDELETE = 0 and PGUID = '" + Node_GUID + "'";
					dataTable = ahp1.ExecuteDataTable(sql, (OleDbParameter[])null);
					if (dataTable.Rows.Count > 0)
					{
						property2.Value = dataTable.Rows[0][Show_FDName[list[i]]];
					}
					else
					{
						property2.Value = "";
					}
				}
				string text = Get_Data_Type(list[i]);
				switch (text)
				{
				case "数字":
				{
					Dictionary<string, string> dictionary2 = Get_dw(list[i]);
					dictionary2["defvalue"] = property2.Value.ToString();
					if (property2.Value.ToString() != "")
					{
						Property property3 = property2;
						property3.Value += dictionary2["danwei"];
					}
					property2.isNum = true;
					property2.Editor = new PropertyGridNumber(dictionary2);
					break;
				}
				case "可选项":
				{
					string text2 = Get_fw(list[i]);
					property2.Converter = new DropDownListConverter(text2.Split(','));
					break;
				}
				case "时间":
					property2.Editor = new PropertyGridDateTimePickerItem();
					break;
				case "日期":
					property2.Editor = new PropertyGridDateItem();
					break;
				case "多选":
				{
					string allOptions = Get_fw(list[i]);
					property2.Editor = new PropertyGridMultiSelect(allOptions);
					break;
				}
				}
				property2.ReadOnly = true;
				propertyManageCls.Add(property2);
			}
			propertyGrid1.SelectedObject = propertyManageCls;
		}

		private Dictionary<string, string> Get_dw(string propguid)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary.Add("danwei", "");
			dictionary.Add("afterdecpoint", "");
			dictionary.Add("upper", "");
			dictionary.Add("limit", "");
			dictionary.Add("defvalue", "");
			Dictionary<string, string> dictionary2 = dictionary;
			string text = Show_DB[propguid];
			AccessHelper accessHelper = null;
			accessHelper = ((text == "H0001Z000K00") ? ahp2 : ((!(text == "H0001Z000K01")) ? ahp4 : ahp3));
			string text2 = Icon_GUID + "_" + propguid;
			string sql;
			DataTable dataTable;
			if (inherit_GUID[propguid] != "")
			{
				propguid = inherit_GUID[propguid];
				sql = "select UPGUID from ZSK_PROP_" + text + " where ISDELETE = 0 and PGUID = '" + propguid + "'";
				dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
				if (dataTable.Rows.Count > 0)
				{
					text2 = dataTable.Rows[0]["UPGUID"].ToString() + "_" + propguid;
				}
			}
			sql = "select PROPNAME, PROPVALUE from ZSK_LIMIT_" + text + " where ISDELETE = 0 and UPGUID = '" + text2 + "'";
			dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (dataTable.Rows[i]["PROPNAME"].ToString() == "单位")
				{
					dictionary2["danwei"] = dataTable.Rows[i]["PROPVALUE"].ToString();
				}
				if (dataTable.Rows[i]["PROPNAME"].ToString() == "小数位数")
				{
					dictionary2["afterdecpoint"] = dataTable.Rows[i]["PROPVALUE"].ToString();
				}
				if (dataTable.Rows[i]["PROPNAME"].ToString() == "上限")
				{
					dictionary2["upper"] = dataTable.Rows[i]["PROPVALUE"].ToString();
				}
				if (dataTable.Rows[i]["PROPNAME"].ToString() == "下限")
				{
					dictionary2["limit"] = dataTable.Rows[i]["PROPVALUE"].ToString();
				}
			}
			return dictionary2;
		}

		private void LoadPicture()
		{
			imageList1.Images.Clear();
			listView1.Items.Clear();
			imageList1.ImageSize = new Size(120, 90);
			DirectoryInfo directoryInfo = new DirectoryInfo(WorkPath + "picture");
			FileInfo[] files = directoryInfo.GetFiles();
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				FileStream fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
				Image image = Image.FromStream(fileStream);
				imageList1.Images.Add(fileInfo.Name, image);
				fileStream.Close();
			}
			listView1.View = View.LargeIcon;
			listView1.LargeImageList = imageList1;
			for (int j = 0; j < imageList1.Images.Count; j++)
			{
				listView1.Items.Add(imageList1.Images.Keys[j]);
				listView1.Items[j].ImageIndex = j;
			}
		}

		private void listView1_ItemActivate(object sender, EventArgs e)
		{
			ListView listView = (ListView)sender;
			InfoPicForm infoPicForm = new InfoPicForm();
			infoPicForm.Owner = this;
			ListViewItem focusedItem = listView.FocusedItem;
			int length = focusedItem.Text.IndexOf(".");
			infoPicForm.Text = focusedItem.Text.Substring(0, length);
			infoPicForm.picpath = WorkPath + "picture\\" + focusedItem.Text;
			infoPicForm.ShowDialog();
		}

		private void GetMenuList()
		{
			Menu_GUID = new List<string>();
			Menu_Upguid = new Dictionary<string, string>();
			Menu_Name = new Dictionary<string, string>();
			Menu_Func = new Dictionary<string, string>();
			Menu_Addr = new Dictionary<string, string>();
			Menu_List = new Dictionary<string, List<string>>();
			ahp1.CloseConn();
			ahp1 = new AccessHelper(AccessPath1);
			string text = "2d4c409b-8355-4ec4-b51c-9dd639742de3";
			string value = "";
			Menu_GUID.Add(text);
			Menu_Upguid[text] = value;
			Menu_Name[text] = "基本信息";
			Menu_Func[text] = "info";
			Menu_Addr[text] = "";
			BarButtonItem barButtonItem = new BarButtonItem();
			barButtonItem.Caption = "关闭";
			barButtonItem.ItemClick += barButtonItem4_ItemClick;
			bar2.AddItem(barButtonItem);
			for (int i = 0; i < Menu_GUID.Count; i++)
			{
				string text2 = Menu_GUID[i];
				if (Menu_Upguid[text2] == "")
				{
					BarButtonItem barButtonItem2 = new BarButtonItem();
					barButtonItem2.Caption = Menu_Name[text2];
					barButtonItem2.Tag = text2;
					barButtonItem2.ItemClick += MenuStripItem_Click;
					bar2.InsertItem(bar2.ItemLinks[0], barButtonItem2);
				}
			}
			if (bar2.ItemLinks[0].Item.Tag != null)
			{
				BarButtonItem item = (BarButtonItem)bar2.ItemLinks[0].Item;
				MenuStripItem_Click(barManager1, new ItemClickEventArgs(item, bar2.ItemLinks[0]));
			}
		}

		private void Show_Info(string pguid, bool second_bar)
		{
			string text = Menu_Func[pguid];
			bool flag = File.Exists(WorkPath + "file\\" + Menu_Addr[pguid]) || File.Exists(Menu_Addr[pguid]);
			switch (text)
			{
			case "list":
			{
				bar1.Visible = true;
				panelControl1.Visible = false;
				bar1.BeginUpdate();
				bar1.ClearLinks();
				bar1.Offset = 0;
				bar1.ApplyDockRowCol();
				string sql = "select PGUID, FUNCNAME from ENVIRLIST_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + pguid + "' and UNITID = '" + unitid + "' and MARKERID in('" + Node_GUID + "', 'all') order by SHOWINDEX";
				DataTable dataTable = ahp1.ExecuteDataTable(sql, (OleDbParameter[])null);
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					BarButtonItem barButtonItem = new BarButtonItem();
					barButtonItem.Caption = dataTable.Rows[i]["FUNCNAME"].ToString();
					barButtonItem.Tag = dataTable.Rows[i]["PGUID"].ToString();
					barButtonItem.ItemClick += ToolStripItem_Click;
					bar1.AddItem(barButtonItem);
				}
				bar1.EndUpdate();
				if (bar1.ItemLinks.Count > 0)
				{
					ToolStripItem_Click(barManager1, new ItemClickEventArgs(bar1.ItemLinks[0].Item, bar1.ItemLinks[0]));
				}
				break;
			}
			case "pdf":
				bar1.Visible = second_bar;
				panelControl1.Visible = true;
				propertyGrid1.Visible = false;
				listView1.Visible = false;
				webBrowser1.Visible = true;
				documentviewer.Visible = false;
				webBrowser1.Dock = DockStyle.Fill;
				if (flag)
				{
					webBrowser1.Navigate(WorkPath + "file\\" + Menu_Addr[pguid]);
				}
				else
				{
					XtraMessageBox.Show("文件不存在!");
				}
				break;
			case "web":
				bar1.Visible = second_bar;
				panelControl1.Visible = true;
				propertyGrid1.Visible = false;
				listView1.Visible = false;
				webBrowser1.Visible = true;
				documentviewer.Visible = false;
				webBrowser1.Dock = DockStyle.Fill;
				webBrowser1.Navigate(Menu_Addr[pguid]);
				break;
			case "word":
				bar1.Visible = second_bar;
				panelControl1.Visible = true;
				propertyGrid1.Visible = false;
				listView1.Visible = false;
				webBrowser1.Visible = false;
				documentviewer.Visible = true;
				documentviewer.Dock = DockStyle.Fill;
				if (flag)
				{
					documentviewer.LoadFromFile(WorkPath + "file\\" + Menu_Addr[pguid]);
				}
				else
				{
					XtraMessageBox.Show("文件不存在!");
				}
				break;
			case "exe":
				bar1.Visible = second_bar;
				panelControl1.Visible = false;
				if (flag)
				{
					Process process = Process.Start(Menu_Addr[pguid]);
					process.WaitForExit();
				}
				else
				{
					XtraMessageBox.Show("文件不存在!");
				}
				break;
			case "info":
				bar1.Visible = second_bar;
				panelControl1.Visible = true;
				propertyGrid1.Visible = true;
				listView1.Visible = false;
				webBrowser1.Visible = false;
				documentviewer.Visible = false;
				propertyGrid1.Dock = DockStyle.Fill;
				break;
			case "pic":
				bar1.Visible = second_bar;
				panelControl1.Visible = true;
				propertyGrid1.Visible = false;
				listView1.Visible = true;
				webBrowser1.Visible = false;
				documentviewer.Visible = false;
				listView1.Dock = DockStyle.Fill;
				break;
			}
		}

		private void MenuStripItem_Click(object sender, ItemClickEventArgs e)
		{
			foreach (BarItemLink itemLink in bar2.ItemLinks)
			{
				BarButtonItem barButtonItem = (BarButtonItem)itemLink.Item;
				if (barButtonItem.Border == BorderStyles.Style3D)
				{
					barButtonItem.Border = BorderStyles.NoBorder;
				}
			}
			e.Item.Border = BorderStyles.Style3D;
			string pguid = e.Item.Tag.ToString();
			Show_Info(pguid, second_bar: false);
		}

		private void ToolStripItem_Click(object sender, ItemClickEventArgs e)
		{
			foreach (BarItemLink itemLink in bar1.ItemLinks)
			{
				BarButtonItem barButtonItem = (BarButtonItem)itemLink.Item;
				if (barButtonItem.Border == BorderStyles.Style3D)
				{
					barButtonItem.Border = BorderStyles.NoBorder;
				}
			}
			e.Item.Border = BorderStyles.Style3D;
			string pguid = e.Item.Tag.ToString();
			Show_Info(pguid, second_bar: true);
		}

		private void InfoForm_Shown(object sender, EventArgs e)
		{
			string filename = AppDomain.CurrentDomain.BaseDirectory + "ICONDER\\b_PNGICON\\" + Icon_GUID + ".png";
			Image original = Image.FromFile(filename);
			Bitmap bitmap = new Bitmap(original);
			IntPtr hicon = bitmap.GetHicon();
			Icon icon2 = base.Icon = Icon.FromHandle(hicon);
		}

		private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
		{
			for (int i = 0; i < FileReader.Authority.Length; i++)
			{
				if (FileReader.Authority[i] == "一件一档菜单设置权限")
				{
					CheckPwForm checkPwForm = new CheckPwForm();
					checkPwForm.unitid = unitid;
					if (checkPwForm.ShowDialog() != DialogResult.OK)
					{
						XtraMessageBox.Show("未能获取管理员权限");
						return;
					}
					break;
				}
			}
			InfoSetForm infoSetForm = new InfoSetForm();
			infoSetForm.Owner = this;
			infoSetForm.unitid = unitid;
			infoSetForm.markerguid = Node_GUID;
			infoSetForm.ShowDialog();
			bar2.ItemLinks.Clear();
			GetMenuList();
		}

		private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
		{
			Close();
		}

		private void DataForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.stopshine(Node_GUID);
			ahp1.CloseConn();
			ahp2.CloseConn();
			ahp3.CloseConn();
			ahp4.CloseConn();
		}

		private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			((WebBrowser)sender).Document.Window.Error += Window_Error;
			foreach (HtmlElement link in webBrowser1.Document.Links)
			{
				link.SetAttribute("target", "_self");
			}
			foreach (HtmlElement form in webBrowser1.Document.Forms)
			{
				form.SetAttribute("target", "_self");
			}
		}

		private void Window_Error(object sender, HtmlElementErrorEventArgs e)
		{
			e.Handled = true;
		}

		private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
		}

		private void listView1_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				popupMenu1.ShowPopup(barManager1, Control.MousePosition);
			}
		}

		private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (xtraOpenFileDialog1.ShowDialog() == DialogResult.OK)
			{
				string fileName = xtraOpenFileDialog1.FileName;
				int startIndex = fileName.LastIndexOf('\\');
				string str = fileName.Substring(startIndex);
				File.Copy(fileName, WorkPath + "picture" + str, overwrite: true);
				LoadPicture();
			}
		}

		private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
		{
			ListView.SelectedListViewItemCollection selectedItems = listView1.SelectedItems;
			foreach (ListViewItem item in selectedItems)
			{
				File.Delete(WorkPath + "picture\\" + item.Text);
			}
			LoadPicture();
		}

		private void InfoForm_Load(object sender, EventArgs e)
		{
			ahp1 = new AccessHelper(AccessPath1);
			ahp2 = new AccessHelper(AccessPath2);
			ahp3 = new AccessHelper(AccessPath3);
			ahp4 = new AccessHelper(AccessPath4);
			BaseInfo();
			LoadPicture();
			GetMenuList();
			documentviewer.Parent = panelControl1;
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.InfoForm));
			barManager1 = new DevExpress.XtraBars.BarManager(components);
			bar2 = new DevExpress.XtraBars.Bar();
			bar3 = new DevExpress.XtraBars.Bar();
			bar1 = new DevExpress.XtraBars.Bar();
			barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
			barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
			panelControl1 = new DevExpress.XtraEditors.PanelControl();
			listView1 = new System.Windows.Forms.ListView();
			webBrowser1 = new System.Windows.Forms.WebBrowser();
			propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			imageList1 = new System.Windows.Forms.ImageList(components);
			backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			popupMenu1 = new DevExpress.XtraBars.PopupMenu(components);
			xtraOpenFileDialog1 = new DevExpress.XtraEditors.XtraOpenFileDialog(components);
			((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)popupMenu1).BeginInit();
			SuspendLayout();
			barManager1.AllowShowToolbarsPopup = false;
			barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[3]
			{
				bar2,
				bar3,
				bar1
			});
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
			barManager1.MainMenu = bar2;
			barManager1.MaxItemId = 7;
			barManager1.StatusBar = bar3;
			bar2.BarName = "Main menu";
			bar2.DockCol = 0;
			bar2.DockRow = 0;
			bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			bar2.OptionsBar.AllowQuickCustomization = false;
			bar2.OptionsBar.DrawDragBorder = false;
			bar2.OptionsBar.MultiLine = true;
			bar2.OptionsBar.UseWholeRow = true;
			bar2.Text = "Main menu";
			bar3.BarName = "Status bar";
			bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
			bar3.DockCol = 0;
			bar3.DockRow = 0;
			bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
			bar3.OptionsBar.AllowQuickCustomization = false;
			bar3.OptionsBar.DrawDragBorder = false;
			bar3.OptionsBar.UseWholeRow = true;
			bar3.Text = "Status bar";
			bar3.Visible = false;
			bar1.BarName = "Custom 4";
			bar1.DockCol = 0;
			bar1.DockRow = 1;
			bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			bar1.OptionsBar.DrawBorder = false;
			bar1.Text = "Custom 4";
			barDockControlTop.CausesValidation = false;
			barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			barDockControlTop.Location = new System.Drawing.Point(0, 0);
			barDockControlTop.Manager = barManager1;
			barDockControlTop.Size = new System.Drawing.Size(789, 53);
			barDockControlBottom.CausesValidation = false;
			barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			barDockControlBottom.Location = new System.Drawing.Point(0, 470);
			barDockControlBottom.Manager = barManager1;
			barDockControlBottom.Size = new System.Drawing.Size(789, 25);
			barDockControlLeft.CausesValidation = false;
			barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			barDockControlLeft.Location = new System.Drawing.Point(0, 53);
			barDockControlLeft.Manager = barManager1;
			barDockControlLeft.Size = new System.Drawing.Size(0, 417);
			barDockControlRight.CausesValidation = false;
			barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			barDockControlRight.Location = new System.Drawing.Point(789, 53);
			barDockControlRight.Manager = barManager1;
			barDockControlRight.Size = new System.Drawing.Size(0, 417);
			barButtonItem1.Caption = "导入图片";
			barButtonItem1.Id = 5;
			barButtonItem1.Name = "barButtonItem1";
			barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem1_ItemClick);
			barButtonItem2.Caption = "删除图片";
			barButtonItem2.Id = 6;
			barButtonItem2.Name = "barButtonItem2";
			barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(barButtonItem2_ItemClick);
			panelControl1.Controls.Add(listView1);
			panelControl1.Controls.Add(webBrowser1);
			panelControl1.Controls.Add(propertyGrid1);
			panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			panelControl1.Location = new System.Drawing.Point(0, 53);
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new System.Drawing.Size(789, 417);
			panelControl1.TabIndex = 5;
			panelControl1.TabStop = true;
			listView1.Dock = System.Windows.Forms.DockStyle.Left;
			listView1.Location = new System.Drawing.Point(368, 2);
			listView1.Name = "listView1";
			listView1.Size = new System.Drawing.Size(176, 413);
			listView1.TabIndex = 10;
			listView1.UseCompatibleStateImageBehavior = false;
			listView1.Visible = false;
			listView1.ItemActivate += new System.EventHandler(listView1_ItemActivate);
			listView1.MouseDown += new System.Windows.Forms.MouseEventHandler(listView1_MouseDown);
			webBrowser1.Dock = System.Windows.Forms.DockStyle.Left;
			webBrowser1.Location = new System.Drawing.Point(174, 2);
			webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.ScriptErrorsSuppressed = true;
			webBrowser1.Size = new System.Drawing.Size(194, 413);
			webBrowser1.TabIndex = 9;
			webBrowser1.Visible = false;
			webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
			webBrowser1.NewWindow += new System.ComponentModel.CancelEventHandler(webBrowser1_NewWindow);
			propertyGrid1.Dock = System.Windows.Forms.DockStyle.Left;
			propertyGrid1.Enabled = false;
			propertyGrid1.HelpVisible = false;
			propertyGrid1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			propertyGrid1.Location = new System.Drawing.Point(2, 2);
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			propertyGrid1.Size = new System.Drawing.Size(172, 413);
			propertyGrid1.TabIndex = 7;
			propertyGrid1.ToolbarVisible = false;
			propertyGrid1.Visible = false;
			imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			imageList1.ImageSize = new System.Drawing.Size(16, 16);
			imageList1.TransparentColor = System.Drawing.Color.Transparent;
			popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[2]
			{
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem1),
				new DevExpress.XtraBars.LinkPersistInfo(barButtonItem2)
			});
			popupMenu1.Manager = barManager1;
			popupMenu1.Name = "popupMenu1";
			xtraOpenFileDialog1.FileName = null;
			xtraOpenFileDialog1.Filter = "图像文件(*.gif;*.jpg;*.jpeg;*.png;*.psd)|*.gif;*.jpg;*.jpeg;*.png;*.psd|所有文件(*.*)|*.*";
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(789, 495);
			base.Controls.Add(panelControl1);
			base.Controls.Add(barDockControlLeft);
			base.Controls.Add(barDockControlRight);
			base.Controls.Add(barDockControlBottom);
			base.Controls.Add(barDockControlTop);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MinimizeBox = false;
			base.Name = "InfoForm";
			base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(DataForm_FormClosed);
			base.Load += new System.EventHandler(InfoForm_Load);
			base.Shown += new System.EventHandler(InfoForm_Shown);
			((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
			((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(performLayout: false);
			((System.ComponentModel.ISupportInitialize)popupMenu1).EndInit();
			ResumeLayout(performLayout: false);
			PerformLayout();
		}
	}
}

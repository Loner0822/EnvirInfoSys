using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using ucPropertyGrid;

namespace EnvirInfoSys
{
	public class DataForm : XtraForm
	{
		private InputLanguageCollection langs = InputLanguage.InstalledInputLanguages;

		private AccessHelper ahp1 = null;

		private AccessHelper ahp2 = null;

		private AccessHelper ahp3 = null;

		private AccessHelper ahp4 = null;

		public bool Update_Data = false;

		public bool CanEdit = true;

		public string Node_GUID = "";

		public string Icon_GUID = "";

		public string Node_Name = "";

		public string JdCode = "";

		public string unitid = "";

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

		private Dictionary<string, string> Real_Value;

		private Dictionary<string, string> Show_DB;

		private IContainer components = null;

		private PanelControl panelControl1;

		private SimpleButton simpleButton2;

		private SimpleButton simpleButton1;

		private PropertyGrid propertyGrid1;

		public DataForm()
		{
			InitializeComponent();
		}

		public void ReNew()
		{
			FDName_Value = new Dictionary<string, string>();
			PropertyManageCls propertyManageCls = (PropertyManageCls)propertyGrid1.SelectedObject;
			foreach (Property item in propertyManageCls)
			{
				FDName_Value.Add(item.FdName, item.Value.ToString());
			}
			base.DialogResult = DialogResult.OK;
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
				if (dataTable.Rows.Count > 0)
				{
					text2 = dataTable.Rows[0]["UPGUID"].ToString() + "_" + propguid;
				}
			}
			sql = "select DATATYPE from  ZSK_DATATYPE_" + text + " where ISDELETE = 0 and UPGUID = '" + text2 + "'";
			dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
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

		public void Load_Prop()
		{
			ahp1 = new AccessHelper(AccessPath1);
			ahp2 = new AccessHelper(AccessPath2);
			ahp3 = new AccessHelper(AccessPath3);
			ahp4 = new AccessHelper(AccessPath4);
			PropertyManageCls propertyManageCls = new PropertyManageCls();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Show_Name = new Dictionary<string, string>();
			Show_FDName = new Dictionary<string, string>();
			Show_Value = new Dictionary<string, string>();
			inherit_GUID = new Dictionary<string, string>();
			Show_DB = new Dictionary<string, string>();
			Real_Value = new Dictionary<string, string>();
			dictionary = Get_Prop_Type();
			List<string> list = Get_Prop_List(dictionary);
			if (!CanEdit)
			{
				propertyGrid1.Enabled = false;
			}
			else
			{
				propertyGrid1.Enabled = true;
			}
			List<string> list2 = new List<string>(dictionary.Keys);
			for (int i = 0; i < list.Count; i++)
			{
				if (Show_Name[list[i]] == "名称")
				{
					continue;
				}
				Property property = new Property(Show_Name[list[i]], "");
				property.DisplayName = Show_Name[list[i]];
				int index = (Show_DB[list[i]] == "H0001Z000K00" || Show_DB[list[i]] == "H0001Z000K01") ? 1 : 2;
				property.Category = dictionary[list2[index]];
				property.FdName = Show_FDName[list[i]];
				if (!Update_Data)
				{
					property.Value = Show_Value[list[i]];
				}
				else
				{
					string sql = "select " + Show_FDName[list[i]] + " from " + JdCode + " where ISDELETE = 0 and PGUID = '" + Node_GUID + "'";
					DataTable dataTable = ahp1.ExecuteDataTable(sql, (OleDbParameter[])null);
					if (dataTable.Rows.Count > 0)
					{
						property.Value = dataTable.Rows[0][Show_FDName[list[i]]].ToString();
					}
					else
					{
						property.Value = "";
					}
				}
				string text = Get_Data_Type(list[i]);
				switch (text)
				{
				case "数字":
				{
					Dictionary<string, string> dictionary2 = Get_dw(list[i]);
					dictionary2["defvalue"] = property.Value.ToString();
					if (property.Value.ToString() != "")
					{
						Property property2 = property;
						property2.Value += dictionary2["danwei"];
					}
					property.isNum = true;
					property.Editor = new PropertyGridNumber(dictionary2);
					break;
				}
				case "可选项":
				{
					string text2 = Get_fw(list[i]);
					property.Converter = new DropDownListConverter(text2.Split(','));
					break;
				}
				case "时间":
					property.Editor = new PropertyGridDateTimePickerItem();
					break;
				case "日期":
					property.Editor = new PropertyGridDateItem();
					break;
				case "多选":
				{
					string allOptions = Get_fw(list[i]);
					property.Editor = new PropertyGridMultiSelect(allOptions);
					break;
				}
				}
				property.ReadOnly = false;
				propertyManageCls.Add(property);
			}
			propertyGrid1.SelectedObject = propertyManageCls;
		}

		private void DataForm_Shown(object sender, EventArgs e)
		{
			Load_Prop();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FDName_Value = new Dictionary<string, string>();
			PropertyManageCls propertyManageCls = (PropertyManageCls)propertyGrid1.SelectedObject;
			foreach (Property item in propertyManageCls)
			{
				if (!item.isNum)
				{
					FDName_Value.Add(item.FdName, item.Value.ToString());
				}
				else
				{
					string text = string.Empty;
					string text2 = item.Value.ToString();
					foreach (char c in text2)
					{
						if (('0' <= c && c <= '9') || c == '.')
						{
							text += c;
						}
					}
					FDName_Value.Add(item.FdName, text);
				}
			}
			base.DialogResult = DialogResult.OK;
		}

		public void Close_Conn()
		{
			ahp1.CloseConn();
			ahp2.CloseConn();
			ahp3.CloseConn();
			ahp4.CloseConn();
		}

		private void DataForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Close_Conn();
		}

		private void simpleButton3_Click(object sender, EventArgs e)
		{
			foreach (Property item in (PropertyManageCls)propertyGrid1.SelectedObject)
			{
				XtraMessageBox.Show(item.ReadOnly.ToString());
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
			System.ComponentModel.ComponentResourceManager componentResourceManager = new System.ComponentModel.ComponentResourceManager(typeof(EnvirInfoSys.DataForm));
			panelControl1 = new DevExpress.XtraEditors.PanelControl();
			simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			propertyGrid1 = new System.Windows.Forms.PropertyGrid();
			((System.ComponentModel.ISupportInitialize)panelControl1).BeginInit();
			panelControl1.SuspendLayout();
			SuspendLayout();
			panelControl1.Controls.Add(simpleButton2);
			panelControl1.Controls.Add(simpleButton1);
			panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panelControl1.Location = new System.Drawing.Point(0, 442);
			panelControl1.Name = "panelControl1";
			panelControl1.Size = new System.Drawing.Size(411, 50);
			panelControl1.TabIndex = 5;
			simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			simpleButton2.Location = new System.Drawing.Point(286, 7);
			simpleButton2.Name = "simpleButton2";
			simpleButton2.Size = new System.Drawing.Size(112, 34);
			simpleButton2.TabIndex = 1;
			simpleButton2.Text = "取消";
			simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			simpleButton1.Location = new System.Drawing.Point(168, 7);
			simpleButton1.Name = "simpleButton1";
			simpleButton1.Size = new System.Drawing.Size(112, 34);
			simpleButton1.TabIndex = 0;
			simpleButton1.Text = "确认";
			simpleButton1.Click += new System.EventHandler(button1_Click);
			propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
			propertyGrid1.Enabled = false;
			propertyGrid1.HelpVisible = false;
			propertyGrid1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
			propertyGrid1.Location = new System.Drawing.Point(0, 0);
			propertyGrid1.Name = "propertyGrid1";
			propertyGrid1.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			propertyGrid1.Size = new System.Drawing.Size(411, 442);
			propertyGrid1.TabIndex = 6;
			propertyGrid1.ToolbarVisible = false;
			base.AcceptButton = simpleButton1;
			base.AutoScaleDimensions = new System.Drawing.SizeF(10f, 22f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = simpleButton2;
			base.ClientSize = new System.Drawing.Size(411, 492);
			base.Controls.Add(propertyGrid1);
			base.Controls.Add(panelControl1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "DataForm";
			Text = "属性定义";
			base.Shown += new System.EventHandler(DataForm_Shown);
			((System.ComponentModel.ISupportInitialize)panelControl1).EndInit();
			panelControl1.ResumeLayout(performLayout: false);
			ResumeLayout(performLayout: false);
		}
	}
}

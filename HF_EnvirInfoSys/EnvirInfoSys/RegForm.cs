using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	public class RegForm : XtraForm
	{
		public string unitid = "";

		public string levelid = "";

		public string nodeid = "";

		public string markerguid = "";

		public bool unLock = false;

		public List<string> Reg_Guid;

		public Dictionary<string, string> Reg_Name;

		public string textName = "";

		public string regaddr = "";

		public string regguid = "";

		private AccessHelper ahp = null;

		private string WorkPath = AppDomain.CurrentDomain.BaseDirectory;

		private IContainer components = null;

		private LabelControl labelControl1;

		private TextEdit textEdit1;

		private SimpleButton simpleButton1;

		private SimpleButton simpleButton2;

		public RegForm()
		{
			InitializeComponent();
		}

		public void Draw_Form()
		{
			for (int i = 0; i < Reg_Name.Count; i++)
			{
				ucRegBox ucRegBox = new ucRegBox();
				ucRegBox.Parent = this;
				ucRegBox.Top = 10;
				ucRegBox.Left = 150 * i;
				ucRegBox.Name = Reg_Guid[i];
				ucRegBox.ucRegBox_Text(Reg_Name[Reg_Guid[i]]);
				ucRegBox.SelectedChange += ucRB_SelectedChange;
			}
			base.Width = Math.Max(400, 150 * Reg_Name.Count + 30);
		}

		private void ucRB_SelectedChange(object sender, EventArgs e, string pguid, int level)
		{
			if (level + 1 < Reg_Guid.Count)
			{
				string sql = "select PGUID, ORGNAME from RG_单位注册 where ISDELETE = 0 and UPPGUID = '" + pguid + "'";
				DataTable dataTable = ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					dictionary[dataTable.Rows[i]["PGUID"].ToString()] = dataTable.Rows[i]["ORGNAME"].ToString();
				}
				foreach (object control in base.Controls)
				{
					if (control is ucRegBox)
					{
						ucRegBox ucRegBox = (ucRegBox)control;
						if (ucRegBox.Name == Reg_Guid[level + 1])
						{
							ucRegBox.ucRegBox_Refresh(dictionary, level + 1);
							break;
						}
					}
				}
			}
		}

		private void Lock_Reg(string pguid, ref int step)
		{
			if (pguid == unitid)
			{
				step = 0;
				foreach (object control in base.Controls)
				{
					if (control is ucRegBox)
					{
						ucRegBox ucRegBox = (ucRegBox)control;
						if (ucRegBox.Name == Reg_Guid[step])
						{
							ucRegBox.ucRegBox_SelectAndLock(pguid, unLock);
							break;
						}
					}
				}
				return;
			}
			string sql = "select UPPGUID from RG_单位注册 where ISDELETE = 0 and PGUID = '" + pguid + "'";
			DataTable dataTable = ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				string pguid2 = dataTable.Rows[0]["UPPGUID"].ToString();
				Lock_Reg(pguid2, ref step);
				step++;
				foreach (object control2 in base.Controls)
				{
					if (control2 is ucRegBox)
					{
						ucRegBox ucRegBox = (ucRegBox)control2;
						if (ucRegBox.Name == Reg_Guid[step])
						{
							ucRegBox.ucRegBox_SelectAndLock(pguid, unLock);
							break;
						}
					}
				}
			}
		}

		private void RegForm_Load(object sender, EventArgs e)
		{
		}

		private void RegForm_Shown(object sender, EventArgs e)
		{
			ahp = new AccessHelper(WorkPath + "data\\PersonMange.mdb");
			textEdit1.Text = textName;
			string sql = "select PGUID, ORGNAME from RG_单位注册 where ISDELETE = 0 and PGUID = '" + unitid + "'";
			DataTable dataTable = ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string key = dataTable.Rows[i]["PGUID"].ToString();
				dictionary[key] = dataTable.Rows[i]["ORGNAME"].ToString();
			}
			foreach (object control in base.Controls)
			{
				if (control is ucRegBox)
				{
					ucRegBox ucRegBox = (ucRegBox)control;
					if (ucRegBox.Name == levelid)
					{
						ucRegBox.ucRegBox_Refresh(dictionary, 0);
						break;
					}
				}
			}
			int step = 0;
			Lock_Reg(nodeid, ref step);
		}

		private void RegForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			ahp.CloseConn();
		}

		private void simpleButton1_Click(object sender, EventArgs e)
		{
			regaddr = "";
			textName = textEdit1.Text;
			for (int i = 0; i < Reg_Guid.Count; i++)
			{
				foreach (object control in base.Controls)
				{
					if (control is ucRegBox)
					{
						ucRegBox ucRegBox = (ucRegBox)control;
						if (ucRegBox.Name == Reg_Guid[i] && ucRegBox.unitguid != "")
						{
							regaddr = regaddr + ucRegBox.unitname + ";";
							regguid = ucRegBox.unitguid;
						}
					}
				}
			}
			if (textEdit1.Text != "")
			{
				AccessHelper accessHelper = new AccessHelper(WorkPath + "data\\ENVIR_H0001Z000E00.mdb");
				string sql = "select PGUID from ENVIRICONDATA_H0001Z000E00 where ISDELETE = 0 and REGGUID = '" + regguid + "' and MAKRENAME = '" + textEdit1.Text + "' and PGUID <> '" + markerguid + "'";
				DataTable dataTable = accessHelper.ExecuteDataTable(sql, (OleDbParameter[])null);
				accessHelper.CloseConn();
				if (dataTable.Rows.Count > 0)
				{
					XtraMessageBox.Show("该名称已被使用!");
					textEdit1.Focus();
					textEdit1.SelectAll();
				}
				else
				{
					base.DialogResult = DialogResult.OK;
					Close();
				}
			}
			else
			{
				XtraMessageBox.Show("请输入注册名称!");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegForm));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelControl1.Location = new System.Drawing.Point(32, 105);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(90, 22);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "注册名称：";
            // 
            // textEdit1
            // 
            this.textEdit1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textEdit1.ImeMode = System.Windows.Forms.ImeMode.Hangul;
            this.textEdit1.Location = new System.Drawing.Point(128, 101);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(150, 30);
            this.textEdit1.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Location = new System.Drawing.Point(309, 98);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(112, 34);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(449, 98);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(112, 34);
            this.simpleButton2.TabIndex = 3;
            this.simpleButton2.Text = "取消";
            // 
            // RegForm
            // 
            this.AcceptButton = this.simpleButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.simpleButton2;
            this.ClientSize = new System.Drawing.Size(593, 164);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegForm";
            this.Text = "注册信息";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RegForm_FormClosed);
            this.Load += new System.EventHandler(this.RegForm_Load);
            this.Shown += new System.EventHandler(this.RegForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
	}
}

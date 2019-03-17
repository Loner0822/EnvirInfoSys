using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EnvirInfoSys
{
	public class PropertyGridNumber : UITypeEditor
	{
		private TextEdit textedit = new TextEdit();

		private string originvalue = "";

		private string danwei = "";

		private double? upper = null;

		private double? limit = null;

		private int? afterdecpoint = 0;

		public PropertyGridNumber(Dictionary<string, string> dicdata)
		{
			textedit.KeyPress += textedit_KeyPress;
			danwei = dicdata["danwei"];
			originvalue = dicdata["defvalue"];
			if (dicdata["upper"] != string.Empty)
			{
				upper = double.Parse(dicdata["upper"]);
			}
			if (dicdata["limit"] != string.Empty)
			{
				limit = double.Parse(dicdata["limit"]);
			}
			if (dicdata["afterdecpoint"] != string.Empty)
			{
				afterdecpoint = int.Parse(dicdata["afterdecpoint"]);
			}
			textedit.ToolTip = "单位：" + danwei;
			textedit.Properties.Mask.UseMaskAsDisplayFormat = true;
			textedit.Properties.Mask.MaskType = MaskType.RegEx;
			textedit.Properties.Mask.EditMask = "[0-9]{1,}[.]{0,1}[0-9]{0," + afterdecpoint.ToString() + "}";
			textedit.Text = dicdata["defvalue"];
		}

		private void textedit_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar < '0' || e.KeyChar > '9')
			{
				e.Handled = true;
			}
			if (e.KeyChar == '.')
			{
				e.Handled = false;
			}
			if (e.KeyChar == '\b')
			{
				e.Handled = false;
			}
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			try
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					windowsFormsEditorService.DropDownControl(textedit);
					if (!Check_Input(textedit.Text))
					{
						textedit.Text = "";
					}
					if (textedit.Text != "")
					{
						originvalue = textedit.Text;
						return textedit.Text + danwei;
					}
					if (originvalue == "")
					{
						return "";
					}
					return originvalue + danwei;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("PropertyGridNumber Error : " + ex.Message);
				return value;
			}
			return value;
		}

		private bool Check_Input(string num)
		{
			double num2 = double.Parse(num);
			if (upper.HasValue && num2 > upper)
			{
				XtraMessageBox.Show("该值已超出上限，上限为" + upper.ToString());
				return false;
			}
			if (limit.HasValue && num2 < limit)
			{
				XtraMessageBox.Show("该值已低于下限，下限为" + limit.ToString());
				return false;
			}
			return true;
		}
	}
}

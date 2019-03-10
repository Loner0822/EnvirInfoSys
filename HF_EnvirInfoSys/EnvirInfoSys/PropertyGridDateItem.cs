using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using ucPropertyGrid;

namespace EnvirInfoSys
{
	public class PropertyGridDateItem : UITypeEditor
	{
		private MonthCalendar dateControl = new MonthCalendar();

		public PropertyGridDateItem()
		{
			dateControl.MaxSelectionCount = 1;
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
					dateControl.DateSelected += dateControl_DateSelected;
					if (value is string)
					{
						if (value.Equals(""))
						{
							dateControl.SelectionStart = DateTime.Now.Date;
						}
						else
						{
							dateControl.SelectionStart = DateTime.Parse(value as string);
						}
						windowsFormsEditorService.DropDownControl(dateControl);
						return dateControl.SelectionStart.Date.ToString("yyyy-MM-dd");
					}
					if (value is DateTime)
					{
						dateControl.SelectionStart = (DateTime)value;
						windowsFormsEditorService.DropDownControl(dateControl);
						return dateControl.SelectionStart.Date.ToString("yyyy-MM-dd");
					}
				}
			}
			catch (Exception ex)
			{
				XtraMessageBox.Show("当前日期格式错误！\n标准格式：" + DateTime.Now.ToLongDateString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				Console.WriteLine("PropertyGridDateItem Error : " + ex.Message);
				return value;
			}
			return value;
		}

		private void dateControl_DateSelected(object sender, DateRangeEventArgs e)
		{
			API.keybd_event(27, 0, 0, 0);
			API.keybd_event(27, 0, 2, 0);
		}
	}
}

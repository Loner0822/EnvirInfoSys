using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace EnvirInfoSys
{
	public class PropertyGridDateTimePickerItem : UITypeEditor
	{
		private DateTimePicker dateControl = new DateTimePicker();

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
					if (value is string)
					{
						if (value.Equals(""))
						{
							value = "00:00:00";
						}
						else
						{
							dateControl.Text = value.ToString();
						}
						dateControl.Format = DateTimePickerFormat.Time;
						dateControl.ShowUpDown = true;
						windowsFormsEditorService.DropDownControl(dateControl);
						return dateControl.Text;
					}
					if (value is DateTime)
					{
						dateControl.Text = value.ToString();
						windowsFormsEditorService.DropDownControl(dateControl);
						return dateControl.Text;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("PropertyGridDateItem Error : " + ex.Message);
				return value;
			}
			return value;
		}
	}
}

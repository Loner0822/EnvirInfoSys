using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using ucPropertyGrid;

namespace EnvirInfoSys
{
	public class PropertyGridMultiSelect : UITypeEditor
	{
		private string allOptions = string.Empty;

		public PropertyGridMultiSelect()
		{
		}

		public PropertyGridMultiSelect(string allOptions)
		{
			this.allOptions = allOptions;
		}

		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
			if (windowsFormsEditorService != null)
			{
				ucMultiSelect ucMultiSelect = new ucMultiSelect(allOptions, value.ToString());
				windowsFormsEditorService.DropDownControl(ucMultiSelect);
				return ucMultiSelect.SelectedOptions;
			}
			return value;
		}
	}
}

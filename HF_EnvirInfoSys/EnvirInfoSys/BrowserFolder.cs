using DevExpress.Dialogs.Core.Localization;

namespace EnvirInfoSys
{
	public class BrowserFolder : DialogsLocalizer
	{
		public override string GetLocalizedString(DialogsStringId id)
		{
			switch (id)
			{
			case DialogsStringId.MakeNewFolderButtonText:
				return "新建文件夹";
			case DialogsStringId.OkButtonText:
				return "确认";
			case DialogsStringId.OpenFileDialogOkButtonText:
				return "打开";
			case DialogsStringId.CancelButtonText:
				return "取消";
			case DialogsStringId.FileNameLabelText:
				return "打开文件";
			case DialogsStringId.NewFolderButtonText:
				return "新建文件夹";
			case DialogsStringId.OpenFileDialogDefaultTitle:
				return "打开文件";
			default:
				return id.ToString();
			}
		}
	}
}

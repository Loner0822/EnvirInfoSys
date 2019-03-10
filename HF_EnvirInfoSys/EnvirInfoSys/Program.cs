using DevExpress.Dialogs.Core.Localization;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Localization;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EnvirInfoSys
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			bool createdNew;
			Mutex mutex = new Mutex(initiallyOwned: true, "OnlyRunOneInstance", out createdNew);
			if (createdNew)
			{
				SkinManager.EnableFormSkins();
				SkinManager.EnableMdiFormSkins();
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-Hans");
				Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-Hans");
				Localizer.Active = new MessageboxClass();
				DialogsLocalizer.Active = new BrowserFolder();
				GridLocalizer.Active = new GridViewer();
				IniOperator iniOperator = new IniOperator(AppDomain.CurrentDomain.BaseDirectory + "RegInfo.ini");
				string skinStyle = iniOperator.ReadString("Individuation", "skin", "DevExpress Style");
				UserLookAndFeel.Default.SetSkinStyle(skinStyle);
				BonusSkins.Register();
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
				Application.ThreadException += Application_ThreadException;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(defaultValue: false);
				Application.Run(new MainForm());
				mutex.ReleaseMutex();
			}
			else
			{
				XtraMessageBox.Show("程序已启动!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			string exceptionMsg = GetExceptionMsg(e.Exception, e.ToString());
			XtraMessageBox.Show(exceptionMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}

		private static string GetExceptionMsg(Exception ex, string backStr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("****************************异常文本****************************");
			stringBuilder.AppendLine("【出现时间】：" + DateTime.Now.ToString());
			if (ex != null)
			{
				stringBuilder.AppendLine("【异常类型】：" + ex.GetType().Name);
				stringBuilder.AppendLine("【异常信息】：" + ex.Message);
				stringBuilder.AppendLine("【堆栈调用】：" + ex.StackTrace);
				stringBuilder.AppendLine("【异常方法】：" + ex.TargetSite);
			}
			else
			{
				stringBuilder.AppendLine("【未处理异常】：" + backStr);
			}
			stringBuilder.AppendLine("***************************************************************");
			LogHelper.WriteErrorLog(stringBuilder.ToString());
			return stringBuilder.ToString();
		}
	}
}

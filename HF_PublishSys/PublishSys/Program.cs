using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PublishSys
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			bool createdNew;
			Mutex mutex = new Mutex(initiallyOwned: true, "OnlyRunOneInstanceFB", out createdNew);
			if (createdNew)
			{
				Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
				Application.ThreadException += Application_ThreadException;
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(defaultValue: false);
				Application.Run(new PubForm());
				mutex.ReleaseMutex();
			}
			else
			{
				MessageBox.Show("程序已启动!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			string exceptionMsg = GetExceptionMsg(e.Exception, e.ToString());
			MessageBox.Show(exceptionMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
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

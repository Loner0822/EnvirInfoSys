using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PublishSys
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Mutex mutex = new System.Threading.Mutex(true, "OnlyRunOneInstance", out bool isRuned);
            if (isRuned)
            {
                DevExpress.Skins.SkinManager.EnableFormSkins();
                DevExpress.Skins.SkinManager.EnableMdiFormSkins();
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-Hans");
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-Hans");
                //DevExpress.XtraEditors.Controls.Localizer.Active = new MessageboxClass();
                //DevExpress.Dialogs.Core.Localization.DialogsLocalizer.Active = new BrowserFolder();
                //DevExpress.XtraGrid.Localization.GridLocalizer.Active = new GridViewer();

                IniOperator inip = new IniOperator(AppDomain.CurrentDomain.BaseDirectory + "RegInfo.ini");
                string skinstyle = inip.ReadString("Individuation", "skin", "DevExpress Style");
                UserLookAndFeel.Default.SetSkinStyle(skinstyle);

                // 指定程序处理异常的方式：
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                // 处理UI线程异常
                Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new MainForm());
                mutex.ReleaseMutex();
            }
            else
            {
                XtraMessageBox.Show("程序已启动!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string exceptionMsg = GetExceptionMsg(e.Exception, e.ToString());
            XtraMessageBox.Show(exceptionMsg, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            Environment.Exit(0);
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

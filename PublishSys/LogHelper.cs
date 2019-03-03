using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishSys
{
    /// <summary>
    /// 日志操作类
    /// </summary>
    public static class LogHelper
    {
        private static readonly string errorLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logFile">日志文件路径</param>
        /// <param name="message">日志信息</param>
        public static void WriteLog(string logFile, string message)
        {
            using (StreamWriter sw = new StreamWriter(logFile, true))
            {
                sw.WriteLine("[" + DateTime.Now + "]");
                sw.WriteLine(message);
                sw.WriteLine("================================================================================");
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void WriteErrorLog(string message)
        {
            WriteLog(errorLogFile, message);
        }

    }
}

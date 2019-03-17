using System;
using System.IO;

namespace PublishSys
{
	public static class LogHelper
	{
		private static readonly string errorLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.log");

		public static void WriteLog(string logFile, string message)
		{
			using (StreamWriter streamWriter = new StreamWriter(logFile, append: true))
			{
				streamWriter.WriteLine("[" + DateTime.Now + "]");
				streamWriter.WriteLine(message);
				streamWriter.WriteLine("================================================================================");
			}
		}

		public static void WriteErrorLog(string message)
		{
			WriteLog(errorLogFile, message);
		}
	}
}

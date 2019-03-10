using System;
using System.Data.OleDb;

namespace EnvirInfoSys
{
	public class ComputerInfo
	{
		public static string UserName = null;

		public static string OSName = null;

		public static string PhyAddr = null;

		public static string IPv4 = null;

		public static string IPv6 = null;

		public static string UnitID = null;

		public static void WriteLog(string Event, string Remark)
		{
			string sql = "insert into LOG_H0001Z000E00 (PGUID, S_UDTIME, USERNAME, OSNAME, PHYADDRESS, IPV4ADDRESS, IPV6ADDRESS, RUNTIME, EVENT, REMARK, UNITID) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + UserName + "', '" + OSName + "', '" + PhyAddr + "', '" + IPv4 + "', '" + IPv6 + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + Event + "', '" + Remark + "', '" + UnitID + "')";
			FileReader.log_ahp.ExecuteSql(sql, (OleDbParameter[])null);
		}
	}
}

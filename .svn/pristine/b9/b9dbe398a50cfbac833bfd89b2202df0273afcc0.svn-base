using System;
using System.Data;
using System.Data.OleDb;

namespace EnvirInfoSys
{
	public class LineData
	{
		public string Type
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public string Color
		{
			get;
			set;
		}

		public double Opacity
		{
			get;
			set;
		}

		public void Get_NewLine()
		{
			Type = "实线";
			Width = 1;
			Color = "#000000";
			Opacity = 1.0;
		}

		public void Load_Line(string markerguid)
		{
			string sql = "select * from ENVIRLINE_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
			DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				Type = dataTable.Rows[0]["LINETYPE"].ToString();
				Width = int.Parse(dataTable.Rows[0]["LINEWIDTH"].ToString());
				Color = dataTable.Rows[0]["LINECOLOR"].ToString();
				Opacity = double.Parse(dataTable.Rows[0]["LINEOPACITY"].ToString());
			}
			else
			{
				Type = null;
				Width = 0;
				Color = null;
				Opacity = 0.0;
			}
		}

		public void Save_Line(string markerguid)
		{
			string sql = "select PGUID from ENVIRLINE_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
			DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				sql = "update ENVIRLINE_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LINETYPE = '" + Type + "', LINEWIDTH = " + Width + ", LINECOLOR = '" + Color + "', LINEOPACITY = '" + Opacity.ToString() + "' where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
				FileReader.often_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
			else
			{
				sql = "insert into ENVIRLINE_H0001Z000E00 (PGUID, S_UDTIME, UPGUID, LINETYPE, LINEWIDTH, LINECOLOR, LINEOPACITY) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + markerguid + "', '" + Type + "', " + Width + ", '" + Color + "', '" + Opacity.ToString() + "')";
				FileReader.often_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
		}
	}
}

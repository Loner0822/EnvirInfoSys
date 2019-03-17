using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace EnvirInfoSys
{
	public class ToPointData
	{
		public double lat
		{
			get;
			set;
		}

		public double lng
		{
			get;
			set;
		}

		public LineData line_data
		{
			get;
			set;
		}

		public bool arrow
		{
			get;
			set;
		}

		public Dictionary<string, object> ToDic()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("lat", lat);
			dictionary.Add("lng", lng);
			dictionary.Add("type", line_data.Type);
			dictionary.Add("width", line_data.Width);
			dictionary.Add("color", line_data.Color);
			dictionary.Add("opacity", line_data.Opacity);
			dictionary.Add("arrow", false);
			return dictionary;
		}

		public void Load_Line(string markerguid)
		{
			LineData lineData = new LineData();
			string sql = "select * from ENVIRLINE_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
			DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				lineData.Type = dataTable.Rows[0]["LINETYPE"].ToString();
				lineData.Width = int.Parse(dataTable.Rows[0]["LINEWIDTH"].ToString());
				lineData.Color = dataTable.Rows[0]["LINECOLOR"].ToString();
				lineData.Opacity = double.Parse(dataTable.Rows[0]["LINEOPACITY"].ToString());
			}
			else
			{
				lineData = null;
			}
			line_data = lineData;
		}

		public void Save_Line(string markerguid, double lat, double lng, bool isAdd)
		{
			string sql = "select PGUID from ENVIRLINE_H0001Z000E00 where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
			DataTable dataTable = FileReader.often_ahp.ExecuteDataTable(sql, (OleDbParameter[])null);
			if (dataTable.Rows.Count > 0)
			{
				sql = "update ENVIRLINE_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', LINETYPE = '" + line_data.Type + "', LINEWIDTH = " + line_data.Width + ", LINECOLOR = '" + line_data.Color + "', LINEOPACITY = '" + line_data.Opacity.ToString() + "' where ISDELETE = 0 and UPGUID = '" + markerguid + "'";
				FileReader.often_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
			else
			{
				sql = "insert into ENVIRLINE_H0001Z000E00 (PGUID, S_UDTIME, UPGUID, LINETYPE, LINEWIDTH, LINECOLOR, LINEOPACITY) values ('" + Guid.NewGuid().ToString("B") + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + markerguid + "', '" + line_data.Type + "', " + line_data.Width + ", '" + line_data.Color + "', '" + line_data.Opacity.ToString() + "')";
				FileReader.often_ahp.ExecuteSql(sql, (OleDbParameter[])null);
			}
			string text = "0";
			if (isAdd)
			{
				text = "1";
			}
			sql = "update ENVIRICONDATA_H0001Z000E00 set S_UDTIME = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', POINTLAT = '" + lat.ToString() + "', POINTLNG = '" + lng + "', POINTARROW = 0, POINTLINE = " + text + " where ISDELETE = 0 and PGUID = '" + markerguid + "'";
			FileReader.often_ahp.ExecuteSql(sql, (OleDbParameter[])null);
		}
	}
}

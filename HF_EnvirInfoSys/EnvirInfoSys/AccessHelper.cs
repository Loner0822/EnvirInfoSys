using System;
using System.Data;
using System.Data.OleDb;

namespace EnvirInfoSys
{
	public class AccessHelper
	{
		private string conStr = "";

		public OleDbConnection con;

		public AccessHelper(string dbpath)
		{
			conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbpath + ";Persist Security Info=False;";
			con = new OleDbConnection(conStr);
			con.Open();
		}

		public bool Exists(string sql, params OleDbParameter[] pms)
		{
			object obj = ExecuteScalar(sql, pms);
			int num = 0;
			if (obj != null)
			{
				num = int.Parse(obj.ToString());
			}
			if (num > 0)
			{
				return true;
			}
			return false;
		}

		public int GetMaxID(string fieldName, string tableName)
		{
			string sql = "SELECT MAX(" + fieldName + ")+1 FROM " + tableName;
			object obj = ExecuteScalar(sql);
			if (obj == null)
			{
				return 1;
			}
			return int.Parse(obj.ToString());
		}

		public int ExecuteSql(string sql, params OleDbParameter[] pms)
		{
			using (OleDbCommand oleDbCommand = new OleDbCommand(sql, con))
			{
				if (pms != null)
				{
					oleDbCommand.Parameters.AddRange(pms);
				}
				return oleDbCommand.ExecuteNonQuery();
			}
		}

		public object ExecuteScalar(string sql, params OleDbParameter[] pms)
		{
			using (OleDbCommand oleDbCommand = new OleDbCommand(sql, con))
			{
				if (pms != null)
				{
					oleDbCommand.Parameters.AddRange(pms);
				}
				return oleDbCommand.ExecuteScalar();
			}
		}

		public OleDbDataReader ExecuteReader(string sql, params OleDbParameter[] pms)
		{
			try
			{
				using (OleDbCommand oleDbCommand = new OleDbCommand(sql, con))
				{
					if (pms != null)
					{
						oleDbCommand.Parameters.AddRange(pms);
					}
					return oleDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
				}
			}
			catch (Exception)
			{
				if (con != null)
				{
					con.Close();
					con.Dispose();
				}
				throw;
			}
		}

		public DataTable ExecuteDataTable(string sql, params OleDbParameter[] pms)
		{
			DataTable dataTable = new DataTable();
			using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sql, con))
			{
				if (pms != null)
				{
					oleDbDataAdapter.SelectCommand.Parameters.AddRange(pms);
				}
				oleDbDataAdapter.Fill(dataTable);
			}
			return dataTable;
		}

		public DataSet Query(string sql, params OleDbParameter[] pms)
		{
			DataSet dataSet = new DataSet();
			using (OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sql, con))
			{
				if (pms != null)
				{
					oleDbDataAdapter.SelectCommand.Parameters.AddRange(pms);
				}
				oleDbDataAdapter.Fill(dataSet);
			}
			return dataSet;
		}

		public DataTable GetOleDbSchemaTable(out DataSet dts)
		{
			DataSet dataSet = new DataSet();
			DataTable oleDbSchemaTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[4]
			{
				null,
				null,
				null,
				"TABLE"
			});
			for (int i = 0; i < oleDbSchemaTable.Rows.Count; i++)
			{
				DataRow dataRow = oleDbSchemaTable.Rows[i];
				DataTable oleDbSchemaTable2 = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[4]
				{
					null,
					null,
					dataRow["TABLE_NAME"].ToString(),
					null
				});
				oleDbSchemaTable2.TableName = dataRow["TABLE_NAME"].ToString();
				dataSet.Tables.Add(oleDbSchemaTable2);
			}
			dts = dataSet;
			return oleDbSchemaTable;
		}

		public bool ColExists(string tableName, string colFileld)
		{
			DataTable dataTable = ExecuteDataTable("SELECT TOP 1 * FROM " + tableName + " WHERE 1<>1");
			return dataTable.Columns.Contains(colFileld);
		}

		public void CloseConn()
		{
			if (con != null)
			{
				con.Close();
				con.Dispose();
			}
		}
	}
}

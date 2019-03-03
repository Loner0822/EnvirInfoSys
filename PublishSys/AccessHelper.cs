using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PublishSys
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

        #region 公用方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="sql">SELECT COUNT(*) FROM DB_Name WHERE ...</param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public bool Exists(string sql, params OleDbParameter[] pms)
        {
            object obj = ExecuteScalar(sql, pms);
            int n = 0;
            if (obj != null)
            {
                n = int.Parse(obj.ToString());
            }
            if (n > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetMaxID(string fieldName, string tableName)
        {
            string sql = "SELECT MAX(" + fieldName + ")+1 FROM " + tableName;
            object obj = ExecuteScalar(sql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        #endregion

        public int ExecuteSql(string sql, params OleDbParameter[] pms)
        {
            //using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    //con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(string sql, params OleDbParameter[] pms)
        {
            //using (OleDbConnection con = new OleDbConnection(conStr))
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    //con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public OleDbDataReader ExecuteReader(string sql, params OleDbParameter[] pms)
        {
            //using (OleDbConnection con = new OleDbConnection(conStr))
            try
            {
                using (OleDbCommand cmd = new OleDbCommand(sql, con))
                {
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }

                    //con.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
            catch (Exception)
            {
                // 关闭连接前需要判断一下是否为null
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
            DataTable dt = new DataTable();

            using (OleDbDataAdapter oda = new OleDbDataAdapter(sql, con))
            {
                if (pms != null)
                {
                    oda.SelectCommand.Parameters.AddRange(pms);
                }
                oda.Fill(dt);
                
            }
            return dt;
        }

        public DataSet Query(string sql, params OleDbParameter[] pms)
        {
            DataSet ds = new DataSet();

            using (OleDbDataAdapter oda = new OleDbDataAdapter(sql, con))
            {
                if (pms != null)
                {
                    oda.SelectCommand.Parameters.AddRange(pms);
                }
                oda.Fill(ds);
            }

            return ds;
        }

        /// <summary>
        /// 获取数据库中所有表信息、和表的列信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetOleDbSchemaTable(out DataSet dts)
        {
            DataTable dt;
            DataSet ds = new DataSet();
            //using (OleDbConnection con = new OleDbConnection(conStr))
            {
                //con.Open();
                dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    DataTable tab = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, dr["TABLE_NAME"].ToString(), null });
                    tab.TableName = dr["TABLE_NAME"].ToString();
                    ds.Tables.Add(tab);
                }
            }
            dts = ds;
            return dt;
        }

        /// <summary>
        /// 判断数据表中字段是否存在。
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <param name="colFileld">字段名</param>
        /// <returns></returns>
        public bool ColExists(string tableName, string colFileld)
        {
            DataTable dt = this.ExecuteDataTable("SELECT TOP 1 * FROM " + tableName + " WHERE 1<>1");
            return dt.Columns.Contains(colFileld);
        }

        public void CloseConn() {
            if (con != null)
            {
                con.Close();
                con.ConnectionString = "";
                con.Dispose();
                con = null;
            }
        }
    }
}

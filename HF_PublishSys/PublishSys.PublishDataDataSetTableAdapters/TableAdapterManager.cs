#define DEBUG
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace PublishSys.PublishDataDataSetTableAdapters
{
	[HelpKeyword("vs.data.TableAdapterManager")]
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	[Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class TableAdapterManager : Component
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public enum UpdateOrderOption
		{
			InsertUpdateDelete,
			UpdateInsertDelete
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private class SelfReferenceComparer : IComparer<DataRow>
		{
			private DataRelation _relation;

			private int _childFirst;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal SelfReferenceComparer(DataRelation relation, bool childFirst)
			{
				_relation = relation;
				if (childFirst)
				{
					_childFirst = -1;
				}
				else
				{
					_childFirst = 1;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			private DataRow GetRoot(DataRow row, out int distance)
			{
				Debug.Assert(row != null);
				DataRow result = row;
				distance = 0;
				IDictionary<DataRow, DataRow> dictionary = new Dictionary<DataRow, DataRow>();
				dictionary[row] = row;
				DataRow parentRow = row.GetParentRow(_relation, DataRowVersion.Default);
				while (parentRow != null && !dictionary.ContainsKey(parentRow))
				{
					distance++;
					result = parentRow;
					dictionary[parentRow] = parentRow;
					parentRow = parentRow.GetParentRow(_relation, DataRowVersion.Default);
				}
				if (distance == 0)
				{
					dictionary.Clear();
					dictionary[row] = row;
					parentRow = row.GetParentRow(_relation, DataRowVersion.Original);
					while (parentRow != null && !dictionary.ContainsKey(parentRow))
					{
						distance++;
						result = parentRow;
						dictionary[parentRow] = parentRow;
						parentRow = parentRow.GetParentRow(_relation, DataRowVersion.Original);
					}
				}
				return result;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public int Compare(DataRow row1, DataRow row2)
			{
				if (object.ReferenceEquals(row1, row2))
				{
					return 0;
				}
				if (row1 == null)
				{
					return -1;
				}
				if (row2 == null)
				{
					return 1;
				}
				int distance = 0;
				DataRow root = GetRoot(row1, out distance);
				int distance2 = 0;
				DataRow root2 = GetRoot(row2, out distance2);
				if (object.ReferenceEquals(root, root2))
				{
					return _childFirst * distance.CompareTo(distance2);
				}
				Debug.Assert(root.Table != null && root2.Table != null);
				if (root.Table.Rows.IndexOf(root) < root2.Table.Rows.IndexOf(root2))
				{
					return -1;
				}
				return 1;
			}
		}

		private UpdateOrderOption _updateOrder;

		private bool _backupDataSetBeforeUpdate;

		private IDbConnection _connection;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public UpdateOrderOption UpdateOrder
		{
			get
			{
				return _updateOrder;
			}
			set
			{
				_updateOrder = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool BackupDataSetBeforeUpdate
		{
			get
			{
				return _backupDataSetBeforeUpdate;
			}
			set
			{
				_backupDataSetBeforeUpdate = value;
			}
		}

		[Browsable(false)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public IDbConnection Connection
		{
			get
			{
				if (_connection != null)
				{
					return _connection;
				}
				return null;
			}
			set
			{
				_connection = value;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		[Browsable(false)]
		public int TableAdapterInstanceCount
		{
			get
			{
				return 0;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private int UpdateUpdatedRows(PublishDataDataSet dataSet, List<DataRow> allChangedRows, List<DataRow> allAddedRows)
		{
			return 0;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private int UpdateInsertedRows(PublishDataDataSet dataSet, List<DataRow> allAddedRows)
		{
			return 0;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private int UpdateDeletedRows(PublishDataDataSet dataSet, List<DataRow> allChangedRows)
		{
			return 0;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private DataRow[] GetRealUpdatedRows(DataRow[] updatedRows, List<DataRow> allAddedRows)
		{
			if (updatedRows == null || updatedRows.Length < 1)
			{
				return updatedRows;
			}
			if (allAddedRows == null || allAddedRows.Count < 1)
			{
				return updatedRows;
			}
			List<DataRow> list = new List<DataRow>();
			foreach (DataRow item in updatedRows)
			{
				if (!allAddedRows.Contains(item))
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public virtual int UpdateAll(PublishDataDataSet dataSet)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("dataSet");
			}
			if (!dataSet.HasChanges())
			{
				return 0;
			}
			IDbConnection connection = Connection;
			if (connection == null)
			{
				throw new ApplicationException("TableAdapterManager 不包含任何连接信息。请将每个 TableAdapterManager TableAdapter 属性设置为有效的 TableAdapter 实例。");
			}
			bool flag = false;
			if ((connection.State & ConnectionState.Broken) == ConnectionState.Broken)
			{
				connection.Close();
			}
			if (connection.State == ConnectionState.Closed)
			{
				connection.Open();
				flag = true;
			}
			IDbTransaction dbTransaction = connection.BeginTransaction();
			if (dbTransaction == null)
			{
				throw new ApplicationException("事务无法开始。当前的数据连接不支持事务或当前状态不允许事务开始。");
			}
			List<DataRow> list = new List<DataRow>();
			List<DataRow> list2 = new List<DataRow>();
			List<DataAdapter> list3 = new List<DataAdapter>();
			Dictionary<object, IDbConnection> dictionary = new Dictionary<object, IDbConnection>();
			int num = 0;
			DataSet dataSet2 = null;
			if (BackupDataSetBeforeUpdate)
			{
				dataSet2 = new DataSet();
				dataSet2.Merge(dataSet);
			}
			try
			{
				if (UpdateOrder == UpdateOrderOption.UpdateInsertDelete)
				{
					num += UpdateUpdatedRows(dataSet, list, list2);
					num += UpdateInsertedRows(dataSet, list2);
				}
				else
				{
					num += UpdateInsertedRows(dataSet, list2);
					num += UpdateUpdatedRows(dataSet, list, list2);
				}
				num += UpdateDeletedRows(dataSet, list);
				dbTransaction.Commit();
				if (0 < list2.Count)
				{
					DataRow[] array = new DataRow[list2.Count];
					list2.CopyTo(array);
					foreach (DataRow dataRow in array)
					{
						dataRow.AcceptChanges();
					}
				}
				if (0 < list.Count)
				{
					DataRow[] array = new DataRow[list.Count];
					list.CopyTo(array);
					foreach (DataRow dataRow in array)
					{
						dataRow.AcceptChanges();
					}
				}
			}
			catch (Exception ex)
			{
				dbTransaction.Rollback();
				if (BackupDataSetBeforeUpdate)
				{
					Debug.Assert(dataSet2 != null);
					dataSet.Clear();
					dataSet.Merge(dataSet2);
				}
				else if (0 < list2.Count)
				{
					DataRow[] array = new DataRow[list2.Count];
					list2.CopyTo(array);
					foreach (DataRow dataRow in array)
					{
						dataRow.AcceptChanges();
						dataRow.SetAdded();
					}
				}
				throw ex;
			}
			finally
			{
				if (flag)
				{
					connection.Close();
				}
				if (0 < list3.Count)
				{
					DataAdapter[] array2 = new DataAdapter[list3.Count];
					list3.CopyTo(array2);
					foreach (DataAdapter dataAdapter in array2)
					{
						dataAdapter.AcceptChangesDuringUpdate = true;
					}
				}
			}
			return num;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected virtual void SortSelfReferenceRows(DataRow[] rows, DataRelation relation, bool childFirst)
		{
			Array.Sort(rows, new SelfReferenceComparer(relation, childFirst));
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected virtual bool MatchTableAdapterConnection(IDbConnection inputConnection)
		{
			if (_connection != null)
			{
				return true;
			}
			if (Connection == null || inputConnection == null)
			{
				return true;
			}
			if (string.Equals(Connection.ConnectionString, inputConnection.ConnectionString, StringComparison.Ordinal))
			{
				return true;
			}
			return false;
		}
	}
}

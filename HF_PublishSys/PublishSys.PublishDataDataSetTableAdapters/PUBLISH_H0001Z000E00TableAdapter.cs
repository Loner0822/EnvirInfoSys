using PublishSys.Properties;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;

namespace PublishSys.PublishDataDataSetTableAdapters
{
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	[Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[HelpKeyword("vs.data.TableAdapter")]
	[DataObject(true)]
	public class PUBLISH_H0001Z000E00TableAdapter : Component
	{
		private OleDbDataAdapter _adapter;

		private OleDbConnection _connection;

		private OleDbTransaction _transaction;

		private OleDbCommand[] _commandCollection;

		private bool _clearBeforeFill;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected internal OleDbDataAdapter Adapter
		{
			get
			{
				if (_adapter == null)
				{
					InitAdapter();
				}
				return _adapter;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal OleDbConnection Connection
		{
			get
			{
				if (_connection == null)
				{
					InitConnection();
				}
				return _connection;
			}
			set
			{
				_connection = value;
				if (Adapter.InsertCommand != null)
				{
					Adapter.InsertCommand.Connection = value;
				}
				if (Adapter.DeleteCommand != null)
				{
					Adapter.DeleteCommand.Connection = value;
				}
				if (Adapter.UpdateCommand != null)
				{
					Adapter.UpdateCommand.Connection = value;
				}
				for (int i = 0; i < CommandCollection.Length; i++)
				{
					if (CommandCollection[i] != null)
					{
						CommandCollection[i].Connection = value;
					}
				}
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal OleDbTransaction Transaction
		{
			get
			{
				return _transaction;
			}
			set
			{
				_transaction = value;
				for (int i = 0; i < CommandCollection.Length; i++)
				{
					CommandCollection[i].Transaction = _transaction;
				}
				if (Adapter != null && Adapter.DeleteCommand != null)
				{
					Adapter.DeleteCommand.Transaction = _transaction;
				}
				if (Adapter != null && Adapter.InsertCommand != null)
				{
					Adapter.InsertCommand.Transaction = _transaction;
				}
				if (Adapter != null && Adapter.UpdateCommand != null)
				{
					Adapter.UpdateCommand.Transaction = _transaction;
				}
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected OleDbCommand[] CommandCollection
		{
			get
			{
				if (_commandCollection == null)
				{
					InitCommandCollection();
				}
				return _commandCollection;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool ClearBeforeFill
		{
			get
			{
				return _clearBeforeFill;
			}
			set
			{
				_clearBeforeFill = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public PUBLISH_H0001Z000E00TableAdapter()
		{
			ClearBeforeFill = true;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private void InitAdapter()
		{
			_adapter = new OleDbDataAdapter();
			DataTableMapping dataTableMapping = new DataTableMapping();
			dataTableMapping.SourceTable = "Table";
			dataTableMapping.DataSetTable = "PUBLISH_H0001Z000E00";
			dataTableMapping.ColumnMappings.Add("S_UDTIME", "S_UDTIME");
			dataTableMapping.ColumnMappings.Add("UNITNAME", "UNITNAME");
			dataTableMapping.ColumnMappings.Add("VERSION", "VERSION");
			dataTableMapping.ColumnMappings.Add("SYSNAME", "SYSNAME");
			_adapter.TableMappings.Add(dataTableMapping);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private void InitConnection()
		{
			_connection = new OleDbConnection();
			_connection.ConnectionString = Settings.Default.PublishDataConnectionString;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitCommandCollection()
		{
			_commandCollection = new OleDbCommand[2];
			_commandCollection[0] = new OleDbCommand();
			_commandCollection[0].Connection = Connection;
			_commandCollection[0].CommandText = "SELECT S_UDTIME, UNITNAME, VERSION, SYSNAME FROM PUBLISH_H0001Z000E00";
			_commandCollection[0].CommandType = CommandType.Text;
			_commandCollection[1] = new OleDbCommand();
			_commandCollection[1].Connection = Connection;
			_commandCollection[1].CommandText = "SELECT S_UDTIME, UNITNAME, VERSION, SYSNAME FROM PUBLISH_H0001Z000E00 where ISDELETE = 0";
			_commandCollection[1].CommandType = CommandType.Text;
		}

		[DebuggerNonUserCode]
		[DataObjectMethod(DataObjectMethodType.Fill, true)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[HelpKeyword("vs.data.TableAdapter")]
		public virtual int Fill(PublishDataDataSet.PUBLISH_H0001Z000E00DataTable dataTable)
		{
			Adapter.SelectCommand = CommandCollection[0];
			if (ClearBeforeFill)
			{
				dataTable.Clear();
			}
			return Adapter.Fill(dataTable);
		}

		[DataObjectMethod(DataObjectMethodType.Select, true)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[HelpKeyword("vs.data.TableAdapter")]
		public virtual PublishDataDataSet.PUBLISH_H0001Z000E00DataTable GetData()
		{
			Adapter.SelectCommand = CommandCollection[0];
			PublishDataDataSet.PUBLISH_H0001Z000E00DataTable pUBLISH_H0001Z000E00DataTable = new PublishDataDataSet.PUBLISH_H0001Z000E00DataTable();
			Adapter.Fill(pUBLISH_H0001Z000E00DataTable);
			return pUBLISH_H0001Z000E00DataTable;
		}

		[DataObjectMethod(DataObjectMethodType.Fill, false)]
		[HelpKeyword("vs.data.TableAdapter")]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public virtual int FillBy(PublishDataDataSet.PUBLISH_H0001Z000E00DataTable dataTable)
		{
			Adapter.SelectCommand = CommandCollection[1];
			if (ClearBeforeFill)
			{
				dataTable.Clear();
			}
			return Adapter.Fill(dataTable);
		}
	}
}

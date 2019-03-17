using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PublishSys
{
	[Serializable]
	[XmlSchemaProvider("GetTypedDataSetSchema")]
	[HelpKeyword("vs.data.DataSet")]
	[XmlRoot("PublishDataDataSet")]
	[DesignerCategory("code")]
	[ToolboxItem(true)]
	public class PublishDataDataSet : DataSet
	{
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public delegate void PUBLISH_H0001Z000E00RowChangeEventHandler(object sender, PUBLISH_H0001Z000E00RowChangeEvent e);

		[Serializable]
		[XmlSchemaProvider("GetTypedTableSchema")]
		public class PUBLISH_H0001Z000E00DataTable : TypedTableBase<PUBLISH_H0001Z000E00Row>
		{
			private DataColumn columnS_UDTIME;

			private DataColumn columnUNITNAME;

			private DataColumn columnVERSION;

			private DataColumn columnSYSNAME;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn S_UDTIMEColumn
			{
				get
				{
					return columnS_UDTIME;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn UNITNAMEColumn
			{
				get
				{
					return columnUNITNAME;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataColumn VERSIONColumn
			{
				get
				{
					return columnVERSION;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public DataColumn SYSNAMEColumn
			{
				get
				{
					return columnSYSNAME;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[Browsable(false)]
			public int Count
			{
				get
				{
					return base.Rows.Count;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public PUBLISH_H0001Z000E00Row this[int index]
			{
				get
				{
					return (PUBLISH_H0001Z000E00Row)base.Rows[index];
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event PUBLISH_H0001Z000E00RowChangeEventHandler PUBLISH_H0001Z000E00RowChanging;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event PUBLISH_H0001Z000E00RowChangeEventHandler PUBLISH_H0001Z000E00RowChanged;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event PUBLISH_H0001Z000E00RowChangeEventHandler PUBLISH_H0001Z000E00RowDeleting;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public event PUBLISH_H0001Z000E00RowChangeEventHandler PUBLISH_H0001Z000E00RowDeleted;

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public PUBLISH_H0001Z000E00DataTable()
			{
				base.TableName = "PUBLISH_H0001Z000E00";
				BeginInit();
				InitClass();
				EndInit();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal PUBLISH_H0001Z000E00DataTable(DataTable table)
			{
				base.TableName = table.TableName;
				if (table.CaseSensitive != table.DataSet.CaseSensitive)
				{
					base.CaseSensitive = table.CaseSensitive;
				}
				if (table.Locale.ToString() != table.DataSet.Locale.ToString())
				{
					base.Locale = table.Locale;
				}
				if (table.Namespace != table.DataSet.Namespace)
				{
					base.Namespace = table.Namespace;
				}
				base.Prefix = table.Prefix;
				base.MinimumCapacity = table.MinimumCapacity;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected PUBLISH_H0001Z000E00DataTable(SerializationInfo info, StreamingContext context)
				: base(info, context)
			{
				InitVars();
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void AddPUBLISH_H0001Z000E00Row(PUBLISH_H0001Z000E00Row row)
			{
				base.Rows.Add(row);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public PUBLISH_H0001Z000E00Row AddPUBLISH_H0001Z000E00Row(string S_UDTIME, string UNITNAME, string VERSION, string SYSNAME)
			{
				PUBLISH_H0001Z000E00Row pUBLISH_H0001Z000E00Row = (PUBLISH_H0001Z000E00Row)NewRow();
				object[] array2 = pUBLISH_H0001Z000E00Row.ItemArray = new object[4]
				{
					S_UDTIME,
					UNITNAME,
					VERSION,
					SYSNAME
				};
				base.Rows.Add(pUBLISH_H0001Z000E00Row);
				return pUBLISH_H0001Z000E00Row;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public override DataTable Clone()
			{
				PUBLISH_H0001Z000E00DataTable pUBLISH_H0001Z000E00DataTable = (PUBLISH_H0001Z000E00DataTable)base.Clone();
				pUBLISH_H0001Z000E00DataTable.InitVars();
				return pUBLISH_H0001Z000E00DataTable;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataTable CreateInstance()
			{
				return new PUBLISH_H0001Z000E00DataTable();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			internal void InitVars()
			{
				columnS_UDTIME = base.Columns["S_UDTIME"];
				columnUNITNAME = base.Columns["UNITNAME"];
				columnVERSION = base.Columns["VERSION"];
				columnSYSNAME = base.Columns["SYSNAME"];
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			private void InitClass()
			{
				columnS_UDTIME = new DataColumn("S_UDTIME", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnS_UDTIME);
				columnUNITNAME = new DataColumn("UNITNAME", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnUNITNAME);
				columnVERSION = new DataColumn("VERSION", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnVERSION);
				columnSYSNAME = new DataColumn("SYSNAME", typeof(string), null, MappingType.Element);
				base.Columns.Add(columnSYSNAME);
				columnS_UDTIME.MaxLength = 255;
				columnUNITNAME.MaxLength = 255;
				columnVERSION.MaxLength = 255;
				columnSYSNAME.MaxLength = 255;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public PUBLISH_H0001Z000E00Row NewPUBLISH_H0001Z000E00Row()
			{
				return (PUBLISH_H0001Z000E00Row)NewRow();
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
			{
				return new PUBLISH_H0001Z000E00Row(builder);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override Type GetRowType()
			{
				return typeof(PUBLISH_H0001Z000E00Row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			protected override void OnRowChanged(DataRowChangeEventArgs e)
			{
				base.OnRowChanged(e);
				if (this.PUBLISH_H0001Z000E00RowChanged != null)
				{
					this.PUBLISH_H0001Z000E00RowChanged(this, new PUBLISH_H0001Z000E00RowChangeEvent((PUBLISH_H0001Z000E00Row)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowChanging(DataRowChangeEventArgs e)
			{
				base.OnRowChanging(e);
				if (this.PUBLISH_H0001Z000E00RowChanging != null)
				{
					this.PUBLISH_H0001Z000E00RowChanging(this, new PUBLISH_H0001Z000E00RowChangeEvent((PUBLISH_H0001Z000E00Row)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleted(DataRowChangeEventArgs e)
			{
				base.OnRowDeleted(e);
				if (this.PUBLISH_H0001Z000E00RowDeleted != null)
				{
					this.PUBLISH_H0001Z000E00RowDeleted(this, new PUBLISH_H0001Z000E00RowChangeEvent((PUBLISH_H0001Z000E00Row)e.Row, e.Action));
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			protected override void OnRowDeleting(DataRowChangeEventArgs e)
			{
				base.OnRowDeleting(e);
				if (this.PUBLISH_H0001Z000E00RowDeleting != null)
				{
					this.PUBLISH_H0001Z000E00RowDeleting(this, new PUBLISH_H0001Z000E00RowChangeEvent((PUBLISH_H0001Z000E00Row)e.Row, e.Action));
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void RemovePUBLISH_H0001Z000E00Row(PUBLISH_H0001Z000E00Row row)
			{
				base.Rows.Remove(row);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
			{
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
				PublishDataDataSet publishDataDataSet = new PublishDataDataSet();
				XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
				xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
				xmlSchemaAny.MinOccurs = 0m;
				xmlSchemaAny.MaxOccurs = decimal.MaxValue;
				xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny);
				XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
				xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
				xmlSchemaAny2.MinOccurs = 1m;
				xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
				xmlSchemaSequence.Items.Add(xmlSchemaAny2);
				XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
				xmlSchemaAttribute.Name = "namespace";
				xmlSchemaAttribute.FixedValue = publishDataDataSet.Namespace;
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
				XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
				xmlSchemaAttribute2.Name = "tableTypeName";
				xmlSchemaAttribute2.FixedValue = "PUBLISH_H0001Z000E00DataTable";
				xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
				xmlSchemaComplexType.Particle = xmlSchemaSequence;
				XmlSchema schemaSerializable = publishDataDataSet.GetSchemaSerializable();
				if (xs.Contains(schemaSerializable.TargetNamespace))
				{
					MemoryStream memoryStream = new MemoryStream();
					MemoryStream memoryStream2 = new MemoryStream();
					try
					{
						XmlSchema xmlSchema = null;
						schemaSerializable.Write(memoryStream);
						IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
						while (enumerator.MoveNext())
						{
							xmlSchema = (XmlSchema)enumerator.Current;
							memoryStream2.SetLength(0L);
							xmlSchema.Write(memoryStream2);
							if (memoryStream.Length == memoryStream2.Length)
							{
								memoryStream.Position = 0L;
								memoryStream2.Position = 0L;
								while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
								{
								}
								if (memoryStream.Position == memoryStream.Length)
								{
									return xmlSchemaComplexType;
								}
							}
						}
					}
					finally
					{
						memoryStream?.Close();
						memoryStream2?.Close();
					}
				}
				xs.Add(schemaSerializable);
				return xmlSchemaComplexType;
			}
		}

		public class PUBLISH_H0001Z000E00Row : DataRow
		{
			private PUBLISH_H0001Z000E00DataTable tablePUBLISH_H0001Z000E00;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string S_UDTIME
			{
				get
				{
					try
					{
						return (string)base[tablePUBLISH_H0001Z000E00.S_UDTIMEColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("表“PUBLISH_H0001Z000E00”中列“S_UDTIME”的值为 DBNull。", innerException);
					}
				}
				set
				{
					base[tablePUBLISH_H0001Z000E00.S_UDTIMEColumn] = value;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public string UNITNAME
			{
				get
				{
					try
					{
						return (string)base[tablePUBLISH_H0001Z000E00.UNITNAMEColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("表“PUBLISH_H0001Z000E00”中列“UNITNAME”的值为 DBNull。", innerException);
					}
				}
				set
				{
					base[tablePUBLISH_H0001Z000E00.UNITNAMEColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string VERSION
			{
				get
				{
					try
					{
						return (string)base[tablePUBLISH_H0001Z000E00.VERSIONColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("表“PUBLISH_H0001Z000E00”中列“VERSION”的值为 DBNull。", innerException);
					}
				}
				set
				{
					base[tablePUBLISH_H0001Z000E00.VERSIONColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public string SYSNAME
			{
				get
				{
					try
					{
						return (string)base[tablePUBLISH_H0001Z000E00.SYSNAMEColumn];
					}
					catch (InvalidCastException innerException)
					{
						throw new StrongTypingException("表“PUBLISH_H0001Z000E00”中列“SYSNAME”的值为 DBNull。", innerException);
					}
				}
				set
				{
					base[tablePUBLISH_H0001Z000E00.SYSNAMEColumn] = value;
				}
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			internal PUBLISH_H0001Z000E00Row(DataRowBuilder rb)
				: base(rb)
			{
				tablePUBLISH_H0001Z000E00 = (PUBLISH_H0001Z000E00DataTable)base.Table;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsS_UDTIMENull()
			{
				return IsNull(tablePUBLISH_H0001Z000E00.S_UDTIMEColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetS_UDTIMENull()
			{
				base[tablePUBLISH_H0001Z000E00.S_UDTIMEColumn] = Convert.DBNull;
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public bool IsUNITNAMENull()
			{
				return IsNull(tablePUBLISH_H0001Z000E00.UNITNAMEColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetUNITNAMENull()
			{
				base[tablePUBLISH_H0001Z000E00.UNITNAMEColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsVERSIONNull()
			{
				return IsNull(tablePUBLISH_H0001Z000E00.VERSIONColumn);
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public void SetVERSIONNull()
			{
				base[tablePUBLISH_H0001Z000E00.VERSIONColumn] = Convert.DBNull;
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public bool IsSYSNAMENull()
			{
				return IsNull(tablePUBLISH_H0001Z000E00.SYSNAMEColumn);
			}

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public void SetSYSNAMENull()
			{
				base[tablePUBLISH_H0001Z000E00.SYSNAMEColumn] = Convert.DBNull;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public class PUBLISH_H0001Z000E00RowChangeEvent : EventArgs
		{
			private PUBLISH_H0001Z000E00Row eventRow;

			private DataRowAction eventAction;

			[DebuggerNonUserCode]
			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			public PUBLISH_H0001Z000E00Row Row
			{
				get
				{
					return eventRow;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public DataRowAction Action
			{
				get
				{
					return eventAction;
				}
			}

			[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
			[DebuggerNonUserCode]
			public PUBLISH_H0001Z000E00RowChangeEvent(PUBLISH_H0001Z000E00Row row, DataRowAction action)
			{
				eventRow = row;
				eventAction = action;
			}
		}

		private PUBLISH_H0001Z000E00DataTable tablePUBLISH_H0001Z000E00;

		private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

		[Browsable(false)]
		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public PUBLISH_H0001Z000E00DataTable PUBLISH_H0001Z000E00
		{
			get
			{
				return tablePUBLISH_H0001Z000E00;
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DebuggerNonUserCode]
		public override SchemaSerializationMode SchemaSerializationMode
		{
			get
			{
				return _schemaSerializationMode;
			}
			set
			{
				_schemaSerializationMode = value;
			}
		}

		[DebuggerNonUserCode]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public new DataTableCollection Tables
		{
			get
			{
				return base.Tables;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new DataRelationCollection Relations
		{
			get
			{
				return base.Relations;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public PublishDataDataSet()
		{
			BeginInit();
			InitClass();
			CollectionChangeEventHandler value = SchemaChanged;
			base.Tables.CollectionChanged += value;
			base.Relations.CollectionChanged += value;
			EndInit();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected PublishDataDataSet(SerializationInfo info, StreamingContext context)
			: base(info, context, ConstructSchema: false)
		{
			if (IsBinarySerialized(info, context))
			{
				InitVars(initTable: false);
				CollectionChangeEventHandler value = SchemaChanged;
				Tables.CollectionChanged += value;
				Relations.CollectionChanged += value;
				return;
			}
			string s = (string)info.GetValue("XmlSchema", typeof(string));
			if (DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
			{
				DataSet dataSet = new DataSet();
				dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
				if (dataSet.Tables["PUBLISH_H0001Z000E00"] != null)
				{
					base.Tables.Add(new PUBLISH_H0001Z000E00DataTable(dataSet.Tables["PUBLISH_H0001Z000E00"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
				InitVars();
			}
			else
			{
				ReadXmlSchema(new XmlTextReader(new StringReader(s)));
			}
			GetSerializationData(info, context);
			CollectionChangeEventHandler value2 = SchemaChanged;
			base.Tables.CollectionChanged += value2;
			Relations.CollectionChanged += value2;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void InitializeDerivedDataSet()
		{
			BeginInit();
			InitClass();
			EndInit();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public override DataSet Clone()
		{
			PublishDataDataSet publishDataDataSet = (PublishDataDataSet)base.Clone();
			publishDataDataSet.InitVars();
			publishDataDataSet.SchemaSerializationMode = SchemaSerializationMode;
			return publishDataDataSet;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeTables()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override bool ShouldSerializeRelations()
		{
			return false;
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override void ReadXmlSerializable(XmlReader reader)
		{
			if (DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
			{
				Reset();
				DataSet dataSet = new DataSet();
				dataSet.ReadXml(reader);
				if (dataSet.Tables["PUBLISH_H0001Z000E00"] != null)
				{
					base.Tables.Add(new PUBLISH_H0001Z000E00DataTable(dataSet.Tables["PUBLISH_H0001Z000E00"]));
				}
				base.DataSetName = dataSet.DataSetName;
				base.Prefix = dataSet.Prefix;
				base.Namespace = dataSet.Namespace;
				base.Locale = dataSet.Locale;
				base.CaseSensitive = dataSet.CaseSensitive;
				base.EnforceConstraints = dataSet.EnforceConstraints;
				Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
				InitVars();
			}
			else
			{
				ReadXml(reader);
				InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		protected override XmlSchema GetSchemaSerializable()
		{
			MemoryStream memoryStream = new MemoryStream();
			WriteXmlSchema(new XmlTextWriter(memoryStream, null));
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		internal void InitVars()
		{
			InitVars(initTable: true);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars(bool initTable)
		{
			tablePUBLISH_H0001Z000E00 = (PUBLISH_H0001Z000E00DataTable)base.Tables["PUBLISH_H0001Z000E00"];
			if (initTable && tablePUBLISH_H0001Z000E00 != null)
			{
				tablePUBLISH_H0001Z000E00.InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private void InitClass()
		{
			base.DataSetName = "PublishDataDataSet";
			base.Prefix = "";
			base.Namespace = "http://tempuri.org/PublishDataDataSet.xsd";
			base.EnforceConstraints = true;
			SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
			tablePUBLISH_H0001Z000E00 = new PUBLISH_H0001Z000E00DataTable();
			base.Tables.Add(tablePUBLISH_H0001Z000E00);
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		private bool ShouldSerializePUBLISH_H0001Z000E00()
		{
			return false;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void SchemaChanged(object sender, CollectionChangeEventArgs e)
		{
			if (e.Action == CollectionChangeAction.Remove)
			{
				InitVars();
			}
		}

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[DebuggerNonUserCode]
		public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
		{
			PublishDataDataSet publishDataDataSet = new PublishDataDataSet();
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = publishDataDataSet.Namespace;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = publishDataDataSet.GetSchemaSerializable();
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					XmlSchema xmlSchema = null;
					schemaSerializable.Write(memoryStream);
					IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
					while (enumerator.MoveNext())
					{
						xmlSchema = (XmlSchema)enumerator.Current;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								return xmlSchemaComplexType;
							}
						}
					}
				}
				finally
				{
					memoryStream?.Close();
					memoryStream2?.Close();
				}
			}
			xs.Add(schemaSerializable);
			return xmlSchemaComplexType;
		}
	}
}

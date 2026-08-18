using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x020000CE RID: 206
	[XmlSchemaProvider("GetDataTableSchema")]
	[Editor("Microsoft.VSDesigner.Data.Design.DataTableEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowChanging")]
	[DesignTimeVisible(false)]
	[DefaultProperty("TableName")]
	[ToolboxItem(false)]
	[Serializable]
	public class DataTable : MarshalByValueComponent, IListSource, ISupportInitializeNotification, ISupportInitialize, ISerializable, IXmlSerializable
	{
		// Token: 0x06000C58 RID: 3160 RVA: 0x00068C20 File Offset: 0x00068020
		public DataTable()
		{
			GC.SuppressFinalize(this);
			Bid.Trace("<ds.DataTable.DataTable|API> %d#\n", this.ObjectID);
			this.nextRowID = 1L;
			this.recordManager = new RecordManager(this);
			this._culture = CultureInfo.CurrentCulture;
			this.columnCollection = new DataColumnCollection(this);
			this.constraintCollection = new ConstraintCollection(this);
			this.rowCollection = new DataRowCollection(this);
			this.indexes = new List<Index>();
			this.rowBuilder = new DataRowBuilder(this, -1);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00068D34 File Offset: 0x00068134
		public DataTable(string tableName) : this()
		{
			this.tableName = ((tableName == null) ? "" : tableName);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00068D58 File Offset: 0x00068158
		public DataTable(string tableName, string tableNamespace) : this(tableName)
		{
			this.Namespace = tableNamespace;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00068D74 File Offset: 0x00068174
		protected DataTable(SerializationInfo info, StreamingContext context) : this()
		{
			bool isSingleTable = context.Context == null || Convert.ToBoolean(context.Context, CultureInfo.InvariantCulture);
			SerializationFormat remotingFormat = SerializationFormat.Xml;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (name == "DataTable.RemotingFormat")
				{
					remotingFormat = (SerializationFormat)enumerator.Value;
				}
			}
			this.DeserializeDataTable(info, context, isSingleTable, remotingFormat);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00068DE4 File Offset: 0x000681E4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			SerializationFormat remotingFormat = this.RemotingFormat;
			bool isSingleTable = context.Context == null || Convert.ToBoolean(context.Context, CultureInfo.InvariantCulture);
			this.SerializeDataTable(info, context, isSingleTable, remotingFormat);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00068E20 File Offset: 0x00068220
		private void SerializeDataTable(SerializationInfo info, StreamingContext context, bool isSingleTable, SerializationFormat remotingFormat)
		{
			info.AddValue("DataTable.RemotingVersion", new Version(2, 0));
			if (remotingFormat != SerializationFormat.Xml)
			{
				info.AddValue("DataTable.RemotingFormat", remotingFormat);
			}
			if (remotingFormat != SerializationFormat.Xml)
			{
				this.SerializeTableSchema(info, context, isSingleTable);
				if (isSingleTable)
				{
					this.SerializeTableData(info, context, 0);
					return;
				}
			}
			else
			{
				string namespaceURI = "";
				bool flag = false;
				if (this.dataSet == null)
				{
					DataSet dataSet = new DataSet("tmpDataSet");
					dataSet.SetLocaleValue(this._culture, this._cultureUserSet);
					dataSet.CaseSensitive = this.CaseSensitive;
					dataSet.namespaceURI = this.Namespace;
					dataSet.Tables.Add(this);
					flag = true;
				}
				else
				{
					namespaceURI = this.DataSet.Namespace;
					this.DataSet.namespaceURI = this.Namespace;
				}
				info.AddValue("XmlSchema", this.dataSet.GetXmlSchemaForRemoting(this));
				info.AddValue("XmlDiffGram", this.dataSet.GetRemotingDiffGram(this));
				if (flag)
				{
					this.dataSet.Tables.Remove(this);
					return;
				}
				this.dataSet.namespaceURI = namespaceURI;
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x00068F34 File Offset: 0x00068334
		internal void DeserializeDataTable(SerializationInfo info, StreamingContext context, bool isSingleTable, SerializationFormat remotingFormat)
		{
			if (remotingFormat != SerializationFormat.Xml)
			{
				this.DeserializeTableSchema(info, context, isSingleTable);
				if (isSingleTable)
				{
					this.DeserializeTableData(info, context, 0);
					this.ResetIndexes();
					return;
				}
			}
			else
			{
				string text = (string)info.GetValue("XmlSchema", typeof(string));
				string text2 = (string)info.GetValue("XmlDiffGram", typeof(string));
				if (text != null)
				{
					DataSet dataSet = new DataSet();
					dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(text)));
					DataTable dataTable = dataSet.Tables[0];
					dataTable.CloneTo(this, null, false);
					this.Namespace = dataTable.Namespace;
					if (text2 != null)
					{
						dataSet.Tables.Remove(dataSet.Tables[0]);
						dataSet.Tables.Add(this);
						dataSet.ReadXml(new XmlTextReader(new StringReader(text2)), XmlReadMode.DiffGram);
						dataSet.Tables.Remove(this);
					}
				}
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x00069020 File Offset: 0x00068420
		internal void SerializeTableSchema(SerializationInfo info, StreamingContext context, bool isSingleTable)
		{
			info.AddValue("DataTable.TableName", this.TableName);
			info.AddValue("DataTable.Namespace", this.Namespace);
			info.AddValue("DataTable.Prefix", this.Prefix);
			info.AddValue("DataTable.CaseSensitive", this._caseSensitive);
			info.AddValue("DataTable.caseSensitiveAmbient", !this._caseSensitiveUserSet);
			info.AddValue("DataTable.LocaleLCID", this.Locale.LCID);
			info.AddValue("DataTable.MinimumCapacity", this.recordManager.MinimumCapacity);
			info.AddValue("DataTable.NestedInDataSet", this.fNestedInDataset);
			info.AddValue("DataTable.TypeName", this.TypeName.ToString());
			info.AddValue("DataTable.RepeatableElement", this.repeatableElement);
			info.AddValue("DataTable.ExtendedProperties", this.ExtendedProperties);
			info.AddValue("DataTable.Columns.Count", this.Columns.Count);
			if (isSingleTable && !this.CheckForClosureOnExpressionTables(new List<DataTable>
			{
				this
			}))
			{
				throw ExceptionBuilder.CanNotRemoteDataTable();
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			for (int i = 0; i < this.Columns.Count; i++)
			{
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnName", new object[]
				{
					i
				}), this.Columns[i].ColumnName);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Namespace", new object[]
				{
					i
				}), this.Columns[i]._columnUri);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Prefix", new object[]
				{
					i
				}), this.Columns[i].Prefix);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnMapping", new object[]
				{
					i
				}), this.Columns[i].ColumnMapping);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AllowDBNull", new object[]
				{
					i
				}), this.Columns[i].AllowDBNull);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrement", new object[]
				{
					i
				}), this.Columns[i].AutoIncrement);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementStep", new object[]
				{
					i
				}), this.Columns[i].AutoIncrementStep);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementSeed", new object[]
				{
					i
				}), this.Columns[i].AutoIncrementSeed);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Caption", new object[]
				{
					i
				}), this.Columns[i].Caption);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DefaultValue", new object[]
				{
					i
				}), this.Columns[i].DefaultValue);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ReadOnly", new object[]
				{
					i
				}), this.Columns[i].ReadOnly);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.MaxLength", new object[]
				{
					i
				}), this.Columns[i].MaxLength);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DataType", new object[]
				{
					i
				}), this.Columns[i].DataType);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.XmlDataType", new object[]
				{
					i
				}), this.Columns[i].XmlDataType);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.SimpleType", new object[]
				{
					i
				}), this.Columns[i].SimpleType);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DateTimeMode", new object[]
				{
					i
				}), this.Columns[i].DateTimeMode);
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementCurrent", new object[]
				{
					i
				}), this.Columns[i].AutoIncrementCurrent);
				if (isSingleTable)
				{
					info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Expression", new object[]
					{
						i
					}), this.Columns[i].Expression);
				}
				info.AddValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ExtendedProperties", new object[]
				{
					i
				}), this.Columns[i].extendedProperties);
			}
			if (isSingleTable)
			{
				this.SerializeConstraints(info, context, 0, false);
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x00069510 File Offset: 0x00068910
		internal void DeserializeTableSchema(SerializationInfo info, StreamingContext context, bool isSingleTable)
		{
			this.tableName = info.GetString("DataTable.TableName");
			this.tableNamespace = info.GetString("DataTable.Namespace");
			this.tablePrefix = info.GetString("DataTable.Prefix");
			bool boolean = info.GetBoolean("DataTable.CaseSensitive");
			this.SetCaseSensitiveValue(boolean, true, false);
			this._caseSensitiveUserSet = !info.GetBoolean("DataTable.caseSensitiveAmbient");
			int culture = (int)info.GetValue("DataTable.LocaleLCID", typeof(int));
			CultureInfo culture2 = new CultureInfo(culture);
			this.SetLocaleValue(culture2, true, false);
			this._cultureUserSet = true;
			this.MinimumCapacity = info.GetInt32("DataTable.MinimumCapacity");
			this.fNestedInDataset = info.GetBoolean("DataTable.NestedInDataSet");
			string @string = info.GetString("DataTable.TypeName");
			this.typeName = new XmlQualifiedName(@string);
			this.repeatableElement = info.GetBoolean("DataTable.RepeatableElement");
			this.extendedProperties = (PropertyCollection)info.GetValue("DataTable.ExtendedProperties", typeof(PropertyCollection));
			int @int = info.GetInt32("DataTable.Columns.Count");
			string[] array = new string[@int];
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			for (int i = 0; i < @int; i++)
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.ColumnName = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnName", new object[]
				{
					i
				}));
				dataColumn._columnUri = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Namespace", new object[]
				{
					i
				}));
				dataColumn.Prefix = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Prefix", new object[]
				{
					i
				}));
				dataColumn.DataType = (Type)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DataType", new object[]
				{
					i
				}), typeof(Type));
				dataColumn.XmlDataType = (string)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.XmlDataType", new object[]
				{
					i
				}), typeof(string));
				dataColumn.SimpleType = (SimpleType)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.SimpleType", new object[]
				{
					i
				}), typeof(SimpleType));
				dataColumn.ColumnMapping = (MappingType)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ColumnMapping", new object[]
				{
					i
				}), typeof(MappingType));
				dataColumn.DateTimeMode = (DataSetDateTime)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DateTimeMode", new object[]
				{
					i
				}), typeof(DataSetDateTime));
				dataColumn.AllowDBNull = info.GetBoolean(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AllowDBNull", new object[]
				{
					i
				}));
				dataColumn.AutoIncrement = info.GetBoolean(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrement", new object[]
				{
					i
				}));
				dataColumn.AutoIncrementStep = info.GetInt64(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementStep", new object[]
				{
					i
				}));
				dataColumn.AutoIncrementSeed = info.GetInt64(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementSeed", new object[]
				{
					i
				}));
				dataColumn.Caption = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Caption", new object[]
				{
					i
				}));
				dataColumn.DefaultValue = info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.DefaultValue", new object[]
				{
					i
				}), typeof(object));
				dataColumn.ReadOnly = info.GetBoolean(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ReadOnly", new object[]
				{
					i
				}));
				dataColumn.MaxLength = info.GetInt32(string.Format(invariantCulture, "DataTable.DataColumn_{0}.MaxLength", new object[]
				{
					i
				}));
				dataColumn.AutoIncrementCurrent = info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.AutoIncrementCurrent", new object[]
				{
					i
				}), typeof(object));
				if (isSingleTable)
				{
					array[i] = info.GetString(string.Format(invariantCulture, "DataTable.DataColumn_{0}.Expression", new object[]
					{
						i
					}));
				}
				dataColumn.extendedProperties = (PropertyCollection)info.GetValue(string.Format(invariantCulture, "DataTable.DataColumn_{0}.ExtendedProperties", new object[]
				{
					i
				}), typeof(PropertyCollection));
				this.Columns.Add(dataColumn);
			}
			if (isSingleTable)
			{
				for (int j = 0; j < @int; j++)
				{
					if (array[j] != null)
					{
						this.Columns[j].Expression = array[j];
					}
				}
			}
			if (isSingleTable)
			{
				this.DeserializeConstraints(info, context, 0, false);
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000699DC File Offset: 0x00068DDC
		internal void SerializeConstraints(SerializationInfo info, StreamingContext context, int serIndex, bool allConstraints)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.Constraints.Count; i++)
			{
				Constraint constraint = this.Constraints[i];
				UniqueConstraint uniqueConstraint = constraint as UniqueConstraint;
				if (uniqueConstraint != null)
				{
					int[] array = new int[uniqueConstraint.Columns.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = uniqueConstraint.Columns[j].Ordinal;
					}
					arrayList.Add(new ArrayList
					{
						"U",
						uniqueConstraint.ConstraintName,
						array,
						uniqueConstraint.IsPrimaryKey,
						uniqueConstraint.ExtendedProperties
					});
				}
				else
				{
					ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
					bool flag = allConstraints || (foreignKeyConstraint.Table == this && foreignKeyConstraint.RelatedTable == this);
					if (flag)
					{
						int[] array2 = new int[foreignKeyConstraint.RelatedColumns.Length + 1];
						array2[0] = (allConstraints ? this.DataSet.Tables.IndexOf(foreignKeyConstraint.RelatedTable) : 0);
						for (int k = 1; k < array2.Length; k++)
						{
							array2[k] = foreignKeyConstraint.RelatedColumns[k - 1].Ordinal;
						}
						int[] array3 = new int[foreignKeyConstraint.Columns.Length + 1];
						array3[0] = (allConstraints ? this.DataSet.Tables.IndexOf(foreignKeyConstraint.Table) : 0);
						for (int l = 1; l < array3.Length; l++)
						{
							array3[l] = foreignKeyConstraint.Columns[l - 1].Ordinal;
						}
						arrayList.Add(new ArrayList
						{
							"F",
							foreignKeyConstraint.ConstraintName,
							array2,
							array3,
							new int[]
							{
								(int)foreignKeyConstraint.AcceptRejectRule,
								(int)foreignKeyConstraint.UpdateRule,
								(int)foreignKeyConstraint.DeleteRule
							},
							foreignKeyConstraint.ExtendedProperties
						});
					}
				}
			}
			info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.Constraints", new object[]
			{
				serIndex
			}), arrayList);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00069C30 File Offset: 0x00069030
		internal void DeserializeConstraints(SerializationInfo info, StreamingContext context, int serIndex, bool allConstraints)
		{
			ArrayList arrayList = (ArrayList)info.GetValue(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.Constraints", new object[]
			{
				serIndex
			}), typeof(ArrayList));
			foreach (object obj in arrayList)
			{
				ArrayList arrayList2 = (ArrayList)obj;
				string text = (string)arrayList2[0];
				if (text.Equals("U"))
				{
					string name = (string)arrayList2[1];
					int[] array = (int[])arrayList2[2];
					bool isPrimaryKey = (bool)arrayList2[3];
					PropertyCollection propertyCollection = (PropertyCollection)arrayList2[4];
					DataColumn[] array2 = new DataColumn[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = this.Columns[array[i]];
					}
					UniqueConstraint uniqueConstraint = new UniqueConstraint(name, array2, isPrimaryKey);
					uniqueConstraint.extendedProperties = propertyCollection;
					this.Constraints.Add(uniqueConstraint);
				}
				else
				{
					string constraintName = (string)arrayList2[1];
					int[] array3 = (int[])arrayList2[2];
					int[] array4 = (int[])arrayList2[3];
					int[] array5 = (int[])arrayList2[4];
					PropertyCollection propertyCollection2 = (PropertyCollection)arrayList2[5];
					DataTable dataTable = (!allConstraints) ? this : this.DataSet.Tables[array3[0]];
					DataColumn[] array6 = new DataColumn[array3.Length - 1];
					for (int j = 0; j < array6.Length; j++)
					{
						array6[j] = dataTable.Columns[array3[j + 1]];
					}
					DataTable dataTable2 = (!allConstraints) ? this : this.DataSet.Tables[array4[0]];
					DataColumn[] array7 = new DataColumn[array4.Length - 1];
					for (int k = 0; k < array7.Length; k++)
					{
						array7[k] = dataTable2.Columns[array4[k + 1]];
					}
					ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(constraintName, array6, array7);
					foreignKeyConstraint.AcceptRejectRule = (AcceptRejectRule)array5[0];
					foreignKeyConstraint.UpdateRule = (Rule)array5[1];
					foreignKeyConstraint.DeleteRule = (Rule)array5[2];
					foreignKeyConstraint.extendedProperties = propertyCollection2;
					this.Constraints.Add(foreignKeyConstraint, false);
				}
			}
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00069EA4 File Offset: 0x000692A4
		internal void SerializeExpressionColumns(SerializationInfo info, StreamingContext context, int serIndex)
		{
			int count = this.Columns.Count;
			for (int i = 0; i < count; i++)
			{
				info.AddValue(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.DataColumn_{1}.Expression", new object[]
				{
					serIndex,
					i
				}), this.Columns[i].Expression);
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00069F08 File Offset: 0x00069308
		internal void DeserializeExpressionColumns(SerializationInfo info, StreamingContext context, int serIndex)
		{
			int count = this.Columns.Count;
			for (int i = 0; i < count; i++)
			{
				string @string = info.GetString(string.Format(CultureInfo.InvariantCulture, "DataTable_{0}.DataColumn_{1}.Expression", new object[]
				{
					serIndex,
					i
				}));
				if (@string.Length != 0)
				{
					this.Columns[i].Expression = @string;
				}
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00069F78 File Offset: 0x00069378
		internal void SerializeTableData(SerializationInfo info, StreamingContext context, int serIndex)
		{
			int count = this.Columns.Count;
			int count2 = this.Rows.Count;
			int num = 0;
			int num2 = 0;
			BitArray bitArray = new BitArray(count2 * 3, false);
			int i = 0;
			while (i < count2)
			{
				int num3 = i * 3;
				DataRow dataRow = this.Rows[i];
				DataRowState rowState = dataRow.RowState;
				if (rowState <= DataRowState.Added)
				{
					if (rowState != DataRowState.Unchanged)
					{
						if (rowState != DataRowState.Added)
						{
							goto IL_9D;
						}
						bitArray[num3 + 1] = true;
					}
				}
				else if (rowState != DataRowState.Deleted)
				{
					if (rowState != DataRowState.Modified)
					{
						goto IL_9D;
					}
					bitArray[num3] = true;
					num++;
				}
				else
				{
					bitArray[num3] = true;
					bitArray[num3 + 1] = true;
				}
				if (-1 != dataRow.tempRecord)
				{
					bitArray[num3 + 2] = true;
					num2++;
				}
				i++;
				continue;
				IL_9D:
				throw ExceptionBuilder.InvalidRowState(rowState);
			}
			int num4 = count2 + num + num2;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			if (num4 > 0)
			{
				for (int j = 0; j < count; j++)
				{
					object emptyColumnStore = this.Columns[j].GetEmptyColumnStore(num4);
					arrayList.Add(emptyColumnStore);
					BitArray value = new BitArray(num4);
					arrayList2.Add(value);
				}
			}
			int num5 = 0;
			Hashtable hashtable = new Hashtable();
			Hashtable hashtable2 = new Hashtable();
			for (int k = 0; k < count2; k++)
			{
				int num6 = this.Rows[k].CopyValuesIntoStore(arrayList, arrayList2, num5);
				this.GetRowAndColumnErrors(k, hashtable, hashtable2);
				num5 += num6;
			}
			IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.Rows.Count", new object[]
			{
				serIndex
			}), count2);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.Records.Count", new object[]
			{
				serIndex
			}), num4);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.RowStates", new object[]
			{
				serIndex
			}), bitArray);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.Records", new object[]
			{
				serIndex
			}), arrayList);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.NullBits", new object[]
			{
				serIndex
			}), arrayList2);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.RowErrors", new object[]
			{
				serIndex
			}), hashtable);
			info.AddValue(string.Format(invariantCulture, "DataTable_{0}.ColumnErrors", new object[]
			{
				serIndex
			}), hashtable2);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0006A1F0 File Offset: 0x000695F0
		internal void DeserializeTableData(SerializationInfo info, StreamingContext context, int serIndex)
		{
			bool flag = this.enforceConstraints;
			bool flag2 = this.inDataLoad;
			try
			{
				this.enforceConstraints = false;
				this.inDataLoad = true;
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				int @int = info.GetInt32(string.Format(invariantCulture, "DataTable_{0}.Rows.Count", new object[]
				{
					serIndex
				}));
				int int2 = info.GetInt32(string.Format(invariantCulture, "DataTable_{0}.Records.Count", new object[]
				{
					serIndex
				}));
				BitArray bitArray = (BitArray)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.RowStates", new object[]
				{
					serIndex
				}), typeof(BitArray));
				ArrayList arrayList = (ArrayList)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.Records", new object[]
				{
					serIndex
				}), typeof(ArrayList));
				ArrayList arrayList2 = (ArrayList)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.NullBits", new object[]
				{
					serIndex
				}), typeof(ArrayList));
				Hashtable hashtable = (Hashtable)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.RowErrors", new object[]
				{
					serIndex
				}), typeof(Hashtable));
				hashtable.OnDeserialization(this);
				Hashtable hashtable2 = (Hashtable)info.GetValue(string.Format(invariantCulture, "DataTable_{0}.ColumnErrors", new object[]
				{
					serIndex
				}), typeof(Hashtable));
				hashtable2.OnDeserialization(this);
				if (int2 > 0)
				{
					for (int i = 0; i < this.Columns.Count; i++)
					{
						this.Columns[i].SetStorage(arrayList[i], (BitArray)arrayList2[i]);
					}
					int num = 0;
					DataRow[] array = new DataRow[int2];
					for (int j = 0; j < @int; j++)
					{
						DataRow dataRow = this.NewEmptyRow();
						array[num] = dataRow;
						int num2 = j * 3;
						DataRowState dataRowState = this.ConvertToRowState(bitArray, num2);
						if (dataRowState <= DataRowState.Added)
						{
							if (dataRowState != DataRowState.Unchanged)
							{
								if (dataRowState == DataRowState.Added)
								{
									dataRow.oldRecord = -1;
									dataRow.newRecord = num;
									num++;
								}
							}
							else
							{
								dataRow.oldRecord = num;
								dataRow.newRecord = num;
								num++;
							}
						}
						else if (dataRowState != DataRowState.Deleted)
						{
							if (dataRowState == DataRowState.Modified)
							{
								dataRow.oldRecord = num;
								dataRow.newRecord = num + 1;
								array[num + 1] = dataRow;
								num += 2;
							}
						}
						else
						{
							dataRow.oldRecord = num;
							dataRow.newRecord = -1;
							num++;
						}
						if (bitArray[num2 + 2])
						{
							dataRow.tempRecord = num;
							array[num] = dataRow;
							num++;
						}
						else
						{
							dataRow.tempRecord = -1;
						}
						this.Rows.ArrayAdd(dataRow);
						dataRow.rowID = this.nextRowID;
						this.nextRowID += 1L;
						this.ConvertToRowError(j, hashtable, hashtable2);
					}
					this.recordManager.SetRowCache(array);
					this.ResetIndexes();
				}
			}
			finally
			{
				this.enforceConstraints = flag;
				this.inDataLoad = flag2;
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0006A500 File Offset: 0x00069900
		private DataRowState ConvertToRowState(BitArray bitStates, int bitIndex)
		{
			bool flag = bitStates[bitIndex];
			bool flag2 = bitStates[bitIndex + 1];
			if (!flag && !flag2)
			{
				return DataRowState.Unchanged;
			}
			if (!flag && flag2)
			{
				return DataRowState.Added;
			}
			if (flag && !flag2)
			{
				return DataRowState.Modified;
			}
			if (flag && flag2)
			{
				return DataRowState.Deleted;
			}
			throw ExceptionBuilder.InvalidRowBitPattern();
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0006A548 File Offset: 0x00069948
		internal void GetRowAndColumnErrors(int rowIndex, Hashtable rowErrors, Hashtable colErrors)
		{
			DataRow dataRow = this.Rows[rowIndex];
			if (dataRow.HasErrors)
			{
				rowErrors.Add(rowIndex, dataRow.RowError);
				DataColumn[] columnsInError = dataRow.GetColumnsInError();
				if (columnsInError.Length != 0)
				{
					int[] array = new int[columnsInError.Length];
					string[] array2 = new string[columnsInError.Length];
					for (int i = 0; i < columnsInError.Length; i++)
					{
						array[i] = columnsInError[i].Ordinal;
						array2[i] = dataRow.GetColumnError(columnsInError[i]);
					}
					ArrayList arrayList = new ArrayList();
					arrayList.Add(array);
					arrayList.Add(array2);
					colErrors.Add(rowIndex, arrayList);
				}
			}
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0006A5EC File Offset: 0x000699EC
		private void ConvertToRowError(int rowIndex, Hashtable rowErrors, Hashtable colErrors)
		{
			DataRow dataRow = this.Rows[rowIndex];
			if (rowErrors.ContainsKey(rowIndex))
			{
				dataRow.RowError = (string)rowErrors[rowIndex];
			}
			if (colErrors.ContainsKey(rowIndex))
			{
				ArrayList arrayList = (ArrayList)colErrors[rowIndex];
				int[] array = (int[])arrayList[0];
				string[] array2 = (string[])arrayList[1];
				for (int i = 0; i < array.Length; i++)
				{
					dataRow.SetColumnError(array[i], array2[i]);
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0006A684 File Offset: 0x00069A84
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0006A698 File Offset: 0x00069A98
		[ResDescription("DataTableCaseSensitiveDescr")]
		public bool CaseSensitive
		{
			get
			{
				return this._caseSensitive;
			}
			set
			{
				if (this._caseSensitive != value)
				{
					bool caseSensitive = this._caseSensitive;
					bool caseSensitiveUserSet = this._caseSensitiveUserSet;
					this._caseSensitive = value;
					this._caseSensitiveUserSet = true;
					if (this.DataSet != null && !this.DataSet.ValidateCaseConstraint())
					{
						this._caseSensitive = caseSensitive;
						this._caseSensitiveUserSet = caseSensitiveUserSet;
						throw ExceptionBuilder.CannotChangeCaseLocale();
					}
					this.SetCaseSensitiveValue(value, true, true);
				}
				this._caseSensitiveUserSet = true;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0006A704 File Offset: 0x00069B04
		internal bool AreIndexEventsSuspended
		{
			get
			{
				return 0 < this._suspendIndexEvents;
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0006A71C File Offset: 0x00069B1C
		internal void RestoreIndexEvents(bool forceReset)
		{
			Bid.Trace("<ds.DataTable.RestoreIndexEvents|Info> %d#, %d\n", this.ObjectID, this._suspendIndexEvents);
			if (0 < this._suspendIndexEvents)
			{
				this._suspendIndexEvents--;
				if (this._suspendIndexEvents == 0)
				{
					Exception ex = null;
					this.SetShadowIndexes();
					try
					{
						int count = this.shadowIndexes.Count;
						for (int i = 0; i < count; i++)
						{
							Index index = this.shadowIndexes[i];
							try
							{
								if (forceReset || index.HasRemoteAggregate)
								{
									index.Reset();
								}
								else
								{
									index.FireResetEvent();
								}
							}
							catch (Exception ex2)
							{
								if (!ADP.IsCatchableExceptionType(ex2))
								{
									throw;
								}
								ExceptionBuilder.TraceExceptionWithoutRethrow(ex2);
								if (ex == null)
								{
									ex = ex2;
								}
							}
						}
						if (ex != null)
						{
							throw ex;
						}
					}
					finally
					{
						this.RestoreShadowIndexes();
					}
				}
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0006A808 File Offset: 0x00069C08
		internal void SuspendIndexEvents()
		{
			Bid.Trace("<ds.DataTable.SuspendIndexEvents|Info> %d#, %d\n", this.ObjectID, this._suspendIndexEvents);
			this._suspendIndexEvents++;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x0006A83C File Offset: 0x00069C3C
		[Browsable(false)]
		public bool IsInitialized
		{
			get
			{
				return !this.fInitInProgress;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0006A854 File Offset: 0x00069C54
		private bool IsTypedDataTable
		{
			get
			{
				byte isTypedDataTable = this._isTypedDataTable;
				if (isTypedDataTable != 0)
				{
					return isTypedDataTable == 1;
				}
				this._isTypedDataTable = ((base.GetType() != typeof(DataTable)) ? 1 : 2);
				return 1 == this._isTypedDataTable;
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0006A8A0 File Offset: 0x00069CA0
		internal bool SetCaseSensitiveValue(bool isCaseSensitive, bool userSet, bool resetIndexes)
		{
			if (userSet || (!this._caseSensitiveUserSet && this._caseSensitive != isCaseSensitive))
			{
				this._caseSensitive = isCaseSensitive;
				if (isCaseSensitive)
				{
					this._compareFlags = CompareOptions.None;
				}
				else
				{
					this._compareFlags = (CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
				if (resetIndexes)
				{
					this.ResetIndexes();
					foreach (object obj in this.Constraints)
					{
						Constraint constraint = (Constraint)obj;
						constraint.CheckConstraint();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0006A940 File Offset: 0x00069D40
		private void ResetCaseSensitive()
		{
			this.SetCaseSensitiveValue(this.dataSet != null && this.dataSet.CaseSensitive, true, true);
			this._caseSensitiveUserSet = false;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0006A974 File Offset: 0x00069D74
		internal bool ShouldSerializeCaseSensitive()
		{
			return this._caseSensitiveUserSet;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0006A988 File Offset: 0x00069D88
		internal bool SelfNested
		{
			get
			{
				foreach (object obj in this.ParentRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (dataRelation.Nested && dataRelation.ParentTable == this)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0006AA00 File Offset: 0x00069E00
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal List<Index> LiveIndexes
		{
			get
			{
				if (!this.AreIndexEventsSuspended)
				{
					int num = this.indexes.Count - 1;
					while (0 <= num)
					{
						Index index = this.indexes[num];
						if (index.RefCount <= 1)
						{
							index.RemoveRef();
						}
						num--;
					}
				}
				return this.indexes;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0006AA50 File Offset: 0x00069E50
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0006AA64 File Offset: 0x00069E64
		[DefaultValue(SerializationFormat.Xml)]
		public SerializationFormat RemotingFormat
		{
			get
			{
				return this._remotingFormat;
			}
			set
			{
				if (value != SerializationFormat.Binary && value != SerializationFormat.Xml)
				{
					throw ExceptionBuilder.InvalidRemotingFormat(value);
				}
				if (this.DataSet != null && value != this.DataSet.RemotingFormat)
				{
					throw ExceptionBuilder.CanNotSetRemotingFormat();
				}
				this._remotingFormat = value;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0006AAA4 File Offset: 0x00069EA4
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0006AAB8 File Offset: 0x00069EB8
		internal int UKColumnPositionForInference
		{
			get
			{
				return this.ukColumnPositionForInference;
			}
			set
			{
				this.ukColumnPositionForInference = value;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0006AACC File Offset: 0x00069ECC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ResDescription("DataTableChildRelationsDescr")]
		[Browsable(false)]
		public DataRelationCollection ChildRelations
		{
			get
			{
				if (this.childRelationsCollection == null)
				{
					this.childRelationsCollection = new DataRelationCollection.DataTableRelationCollection(this, false);
				}
				return this.childRelationsCollection;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0006AAF4 File Offset: 0x00069EF4
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataTableColumnsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public DataColumnCollection Columns
		{
			get
			{
				return this.columnCollection;
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0006AB08 File Offset: 0x00069F08
		private void ResetColumns()
		{
			this.Columns.Clear();
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x0006AB20 File Offset: 0x00069F20
		private CompareInfo CompareInfo
		{
			get
			{
				if (this._compareInfo == null)
				{
					this._compareInfo = this.Locale.CompareInfo;
				}
				return this._compareInfo;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0006AB4C File Offset: 0x00069F4C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataTableConstraintsDescr")]
		public ConstraintCollection Constraints
		{
			get
			{
				return this.constraintCollection;
			}
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0006AB60 File Offset: 0x00069F60
		private void ResetConstraints()
		{
			this.Constraints.Clear();
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0006AB78 File Offset: 0x00069F78
		[Browsable(false)]
		[ResDescription("DataTableDataSetDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataSet DataSet
		{
			get
			{
				return this.dataSet;
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0006AB8C File Offset: 0x00069F8C
		internal void SetDataSet(DataSet dataSet)
		{
			if (this.dataSet != dataSet)
			{
				this.dataSet = dataSet;
				DataColumnCollection columns = this.Columns;
				for (int i = 0; i < columns.Count; i++)
				{
					columns[i].OnSetDataSet();
				}
				if (this.DataSet != null)
				{
					this.defaultView = null;
				}
				if (dataSet != null)
				{
					this._remotingFormat = dataSet.RemotingFormat;
				}
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0006ABEC File Offset: 0x00069FEC
		[ResDescription("DataTableDefaultViewDescr")]
		[Browsable(false)]
		public DataView DefaultView
		{
			get
			{
				DataView dataView = this.defaultView;
				if (dataView == null)
				{
					if (this.dataSet != null)
					{
						dataView = this.dataSet.DefaultViewManager.CreateDataView(this);
					}
					else
					{
						dataView = new DataView(this, true);
						dataView.SetIndex2("", DataViewRowState.CurrentRows, null, true);
					}
					dataView = Interlocked.CompareExchange<DataView>(ref this.defaultView, dataView, null);
					if (dataView == null)
					{
						dataView = this.defaultView;
					}
				}
				return dataView;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0006AC50 File Offset: 0x0006A050
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0006AC64 File Offset: 0x0006A064
		[ResDescription("DataTableDisplayExpressionDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue("")]
		public string DisplayExpression
		{
			get
			{
				return this.DisplayExpressionInternal;
			}
			set
			{
				if (value != null && value.Length > 0)
				{
					this.displayExpression = new DataExpression(this, value);
					return;
				}
				this.displayExpression = null;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0006AC94 File Offset: 0x0006A094
		internal string DisplayExpressionInternal
		{
			get
			{
				if (this.displayExpression == null)
				{
					return "";
				}
				return this.displayExpression.Expression;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0006ACBC File Offset: 0x0006A0BC
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0006ACF0 File Offset: 0x0006A0F0
		internal bool EnforceConstraints
		{
			get
			{
				if (this.SuspendEnforceConstraints)
				{
					return false;
				}
				if (this.dataSet != null)
				{
					return this.dataSet.EnforceConstraints;
				}
				return this.enforceConstraints;
			}
			set
			{
				if (this.dataSet == null && this.enforceConstraints != value)
				{
					if (value)
					{
						this.EnableConstraints();
					}
					this.enforceConstraints = value;
				}
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0006AD20 File Offset: 0x0006A120
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0006AD34 File Offset: 0x0006A134
		internal bool SuspendEnforceConstraints
		{
			get
			{
				return this._suspendEnforceConstraints;
			}
			set
			{
				this._suspendEnforceConstraints = value;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0006AD48 File Offset: 0x0006A148
		internal void EnableConstraints()
		{
			bool flag = false;
			foreach (object obj in this.Constraints)
			{
				Constraint constraint = (Constraint)obj;
				if (constraint is UniqueConstraint)
				{
					flag |= constraint.IsConstraintViolated();
				}
			}
			foreach (object obj2 in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj2;
				if (!dataColumn.AllowDBNull)
				{
					flag |= dataColumn.IsNotAllowDBNullViolated();
				}
				if (dataColumn.MaxLength >= 0)
				{
					flag |= dataColumn.IsMaxLengthViolated();
				}
			}
			if (flag)
			{
				this.EnforceConstraints = false;
				throw ExceptionBuilder.EnforceConstraint();
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0006AE40 File Offset: 0x0006A240
		[Browsable(false)]
		[ResDescription("ExtendedPropertiesDescr")]
		[ResCategory("DataCategory_Data")]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				if (this.extendedProperties == null)
				{
					this.extendedProperties = new PropertyCollection();
				}
				return this.extendedProperties;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0006AE68 File Offset: 0x0006A268
		internal IFormatProvider FormatProvider
		{
			get
			{
				if (this._formatProvider == null)
				{
					CultureInfo cultureInfo = this.Locale;
					if (cultureInfo.IsNeutralCulture)
					{
						cultureInfo = CultureInfo.InvariantCulture;
					}
					this._formatProvider = cultureInfo;
				}
				return this._formatProvider;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0006AEA0 File Offset: 0x0006A2A0
		[Browsable(false)]
		[ResDescription("DataTableHasErrorsDescr")]
		public bool HasErrors
		{
			get
			{
				for (int i = 0; i < this.Rows.Count; i++)
				{
					if (this.Rows[i].HasErrors)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0006AEDC File Offset: 0x0006A2DC
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0006AEF0 File Offset: 0x0006A2F0
		[ResDescription("DataTableLocaleDescr")]
		public CultureInfo Locale
		{
			get
			{
				return this._culture;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataTable.set_Locale|API> %d#\n", this.ObjectID);
				try
				{
					bool cultureUserSet = true;
					if (value == null)
					{
						cultureUserSet = false;
						value = ((this.dataSet != null) ? this.dataSet.Locale : this._culture);
					}
					if (this._culture != value && !this._culture.Equals(value))
					{
						bool flag = false;
						bool flag2 = false;
						CultureInfo culture = this._culture;
						bool cultureUserSet2 = this._cultureUserSet;
						try
						{
							this._cultureUserSet = true;
							this.SetLocaleValue(value, true, false);
							if (this.DataSet == null || this.DataSet.ValidateLocaleConstraint())
							{
								flag = false;
								this.SetLocaleValue(value, true, true);
								flag = true;
							}
						}
						catch
						{
							flag2 = true;
							throw;
						}
						finally
						{
							if (!flag)
							{
								try
								{
									this.SetLocaleValue(culture, true, true);
								}
								catch (Exception e)
								{
									if (!ADP.IsCatchableExceptionType(e))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(e);
								}
								this._cultureUserSet = cultureUserSet2;
								if (!flag2)
								{
									throw ExceptionBuilder.CannotChangeCaseLocale(null);
								}
							}
						}
						this.SetLocaleValue(value, true, true);
					}
					this._cultureUserSet = cultureUserSet;
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0006B058 File Offset: 0x0006A458
		internal bool SetLocaleValue(CultureInfo culture, bool userSet, bool resetIndexes)
		{
			if (userSet || resetIndexes || (!this._cultureUserSet && !this._culture.Equals(culture)))
			{
				this._culture = culture;
				this._compareInfo = null;
				this._formatProvider = null;
				this._hashCodeProvider = null;
				foreach (object obj in this.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					dataColumn._hashCode = this.GetSpecialHashCode(dataColumn.ColumnName);
				}
				if (resetIndexes)
				{
					this.ResetIndexes();
					foreach (object obj2 in this.Constraints)
					{
						Constraint constraint = (Constraint)obj2;
						constraint.CheckConstraint();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0006B168 File Offset: 0x0006A568
		internal bool ShouldSerializeLocale()
		{
			return this._cultureUserSet;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0006B17C File Offset: 0x0006A57C
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0006B194 File Offset: 0x0006A594
		[ResDescription("DataTableMinimumCapacityDescr")]
		[ResCategory("DataCategory_Data")]
		[DefaultValue(50)]
		public int MinimumCapacity
		{
			get
			{
				return this.recordManager.MinimumCapacity;
			}
			set
			{
				if (value != this.recordManager.MinimumCapacity)
				{
					this.recordManager.MinimumCapacity = value;
				}
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0006B1BC File Offset: 0x0006A5BC
		internal int RecordCapacity
		{
			get
			{
				return this.recordManager.RecordCapacity;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C95 RID: 3221 RVA: 0x0006B1D4 File Offset: 0x0006A5D4
		// (set) Token: 0x06000C96 RID: 3222 RVA: 0x0006B1E8 File Offset: 0x0006A5E8
		internal int ElementColumnCount
		{
			get
			{
				return this.elementColumnCount;
			}
			set
			{
				if (value > 0 && this.xmlText != null)
				{
					throw ExceptionBuilder.TableCannotAddToSimpleContent();
				}
				this.elementColumnCount = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x0006B210 File Offset: 0x0006A610
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[ResDescription("DataTableParentRelationsDescr")]
		public DataRelationCollection ParentRelations
		{
			get
			{
				if (this.parentRelationsCollection == null)
				{
					this.parentRelationsCollection = new DataRelationCollection.DataTableRelationCollection(this, true);
				}
				return this.parentRelationsCollection;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0006B238 File Offset: 0x0006A638
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0006B24C File Offset: 0x0006A64C
		internal bool MergingData
		{
			get
			{
				return this.mergingData;
			}
			set
			{
				this.mergingData = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0006B260 File Offset: 0x0006A660
		internal DataRelation[] NestedParentRelations
		{
			get
			{
				return this._nestedParentRelations;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0006B274 File Offset: 0x0006A674
		internal bool SchemaLoading
		{
			get
			{
				return this.schemaLoading;
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0006B288 File Offset: 0x0006A688
		internal void CacheNestedParent()
		{
			this._nestedParentRelations = this.FindNestedParentRelations();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0006B2A4 File Offset: 0x0006A6A4
		private DataRelation[] FindNestedParentRelations()
		{
			List<DataRelation> list = null;
			foreach (object obj in this.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					if (list == null)
					{
						list = new List<DataRelation>();
					}
					list.Add(dataRelation);
				}
			}
			if (list == null || list.Count == 0)
			{
				return DataTable.EmptyArrayDataRelation;
			}
			return list.ToArray();
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0006B334 File Offset: 0x0006A734
		internal int NestedParentsCount
		{
			get
			{
				int num = 0;
				foreach (object obj in this.ParentRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (dataRelation.Nested)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C9F RID: 3231 RVA: 0x0006B3A4 File Offset: 0x0006A7A4
		// (set) Token: 0x06000CA0 RID: 3232 RVA: 0x0006B3D0 File Offset: 0x0006A7D0
		[TypeConverter(typeof(PrimaryKeyTypeConverter))]
		[ResCategory("DataCategory_Data")]
		[Editor("Microsoft.VSDesigner.Data.Design.PrimaryKeyEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("DataTablePrimaryKeyDescr")]
		public DataColumn[] PrimaryKey
		{
			get
			{
				UniqueConstraint uniqueConstraint = this.primaryKey;
				if (uniqueConstraint != null)
				{
					return uniqueConstraint.Key.ToArray();
				}
				return DataTable.zeroColumns;
			}
			set
			{
				UniqueConstraint uniqueConstraint = null;
				if (this.fInitInProgress && value != null)
				{
					this.delayedSetPrimaryKey = value;
					return;
				}
				if (value != null && value.Length != 0)
				{
					int num = 0;
					int num2 = 0;
					while (num2 < value.Length && value[num2] != null)
					{
						num++;
						num2++;
					}
					if (num != 0)
					{
						DataColumn[] array = value;
						if (num != value.Length)
						{
							array = new DataColumn[num];
							for (int i = 0; i < num; i++)
							{
								array[i] = value[i];
							}
						}
						uniqueConstraint = new UniqueConstraint(array);
						if (uniqueConstraint.Table != this)
						{
							throw ExceptionBuilder.TableForeignPrimaryKey();
						}
					}
				}
				if (uniqueConstraint == this.primaryKey || (uniqueConstraint != null && uniqueConstraint.Equals(this.primaryKey)))
				{
					return;
				}
				UniqueConstraint uniqueConstraint2;
				if ((uniqueConstraint2 = (UniqueConstraint)this.Constraints.FindConstraint(uniqueConstraint)) != null)
				{
					uniqueConstraint.ColumnsReference.CopyTo(uniqueConstraint2.Key.ColumnsReference, 0);
					uniqueConstraint = uniqueConstraint2;
				}
				UniqueConstraint uniqueConstraint3 = this.primaryKey;
				this.primaryKey = null;
				if (uniqueConstraint3 != null)
				{
					uniqueConstraint3.ConstraintIndex.RemoveRef();
					if (this.loadIndex != null)
					{
						this.loadIndex.RemoveRef();
						this.loadIndex = null;
					}
					if (this.loadIndexwithOriginalAdded != null)
					{
						this.loadIndexwithOriginalAdded.RemoveRef();
						this.loadIndexwithOriginalAdded = null;
					}
					if (this.loadIndexwithCurrentDeleted != null)
					{
						this.loadIndexwithCurrentDeleted.RemoveRef();
						this.loadIndexwithCurrentDeleted = null;
					}
					this.Constraints.Remove(uniqueConstraint3);
				}
				if (uniqueConstraint != null && uniqueConstraint2 == null)
				{
					this.Constraints.Add(uniqueConstraint);
				}
				this.primaryKey = uniqueConstraint;
				this._primaryIndex = ((uniqueConstraint != null) ? uniqueConstraint.Key.GetIndexDesc() : DataTable.zeroIndexField);
				if (this.primaryKey != null)
				{
					uniqueConstraint.ConstraintIndex.AddRef();
					for (int j = 0; j < uniqueConstraint.ColumnsReference.Length; j++)
					{
						uniqueConstraint.ColumnsReference[j].AllowDBNull = false;
					}
				}
			}
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0006B594 File Offset: 0x0006A994
		private bool ShouldSerializePrimaryKey()
		{
			return this.primaryKey != null;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0006B5AC File Offset: 0x0006A9AC
		private void ResetPrimaryKey()
		{
			this.PrimaryKey = null;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0006B5C0 File Offset: 0x0006A9C0
		[ResDescription("DataTableRowsDescr")]
		[Browsable(false)]
		public DataRowCollection Rows
		{
			get
			{
				return this.rowCollection;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0006B5D4 File Offset: 0x0006A9D4
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0006B5E8 File Offset: 0x0006A9E8
		[RefreshProperties(RefreshProperties.All)]
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataTableTableNameDescr")]
		[DefaultValue("")]
		public string TableName
		{
			get
			{
				return this.tableName;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataTable.set_TableName|API> %d#, value='%ls'\n", this.ObjectID, value);
				try
				{
					if (value == null)
					{
						value = "";
					}
					CultureInfo locale = this.Locale;
					if (string.Compare(this.tableName, value, true, locale) != 0)
					{
						if (this.dataSet != null)
						{
							if (value.Length == 0)
							{
								throw ExceptionBuilder.NoTableName();
							}
							if (string.Compare(value, this.dataSet.DataSetName, true, this.dataSet.Locale) == 0 && !this.fNestedInDataset)
							{
								throw ExceptionBuilder.DatasetConflictingName(this.dataSet.DataSetName);
							}
							DataRelation[] nestedParentRelations = this.NestedParentRelations;
							if (nestedParentRelations.Length == 0)
							{
								this.dataSet.Tables.RegisterName(value, this.Namespace);
							}
							else
							{
								foreach (DataRelation dataRelation in nestedParentRelations)
								{
									if (!dataRelation.ParentTable.Columns.CanRegisterName(value))
									{
										throw ExceptionBuilder.CannotAddDuplicate2(value);
									}
								}
								this.dataSet.Tables.RegisterName(value, this.Namespace);
								foreach (DataRelation dataRelation2 in nestedParentRelations)
								{
									dataRelation2.ParentTable.Columns.RegisterColumnName(value, null);
									dataRelation2.ParentTable.Columns.UnregisterName(this.TableName);
								}
							}
							if (this.tableName.Length != 0)
							{
								this.dataSet.Tables.UnregisterName(this.tableName);
							}
						}
						this.RaisePropertyChanging("TableName");
						this.tableName = value;
						this.encodedTableName = null;
					}
					else if (string.Compare(this.tableName, value, false, locale) != 0)
					{
						this.RaisePropertyChanging("TableName");
						this.tableName = value;
						this.encodedTableName = null;
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0006B7C0 File Offset: 0x0006ABC0
		internal string EncodedTableName
		{
			get
			{
				string text = this.encodedTableName;
				if (text == null)
				{
					text = XmlConvert.EncodeLocalName(this.TableName);
					this.encodedTableName = text;
				}
				return text;
			}
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0006B7EC File Offset: 0x0006ABEC
		private string GetInheritedNamespace(List<DataTable> visitedTables)
		{
			DataRelation[] nestedParentRelations = this.NestedParentRelations;
			if (nestedParentRelations.Length != 0)
			{
				foreach (DataRelation dataRelation in nestedParentRelations)
				{
					if (dataRelation.ParentTable.tableNamespace != null)
					{
						return dataRelation.ParentTable.tableNamespace;
					}
				}
				int num = 0;
				while (num < nestedParentRelations.Length && (nestedParentRelations[num].ParentTable == this || visitedTables.Contains(nestedParentRelations[num].ParentTable)))
				{
					num++;
				}
				if (num < nestedParentRelations.Length)
				{
					DataTable parentTable = nestedParentRelations[num].ParentTable;
					if (!visitedTables.Contains(parentTable))
					{
						visitedTables.Add(parentTable);
					}
					return parentTable.GetInheritedNamespace(visitedTables);
				}
			}
			if (this.DataSet != null)
			{
				return this.DataSet.Namespace;
			}
			return string.Empty;
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0006B8A0 File Offset: 0x0006ACA0
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0006B8C8 File Offset: 0x0006ACC8
		[ResDescription("DataTableNamespaceDescr")]
		[ResCategory("DataCategory_Data")]
		public string Namespace
		{
			get
			{
				if (this.tableNamespace == null)
				{
					return this.GetInheritedNamespace(new List<DataTable>());
				}
				return this.tableNamespace;
			}
			set
			{
				IntPtr intPtr;
				Bid.ScopeEnter(out intPtr, "<ds.DataTable.set_Namespace|API> %d#, value='%ls'\n", this.ObjectID, value);
				try
				{
					if (value != this.tableNamespace)
					{
						if (this.dataSet != null)
						{
							string text = (value == null) ? this.GetInheritedNamespace(new List<DataTable>()) : value;
							if (text != this.Namespace)
							{
								if (this.dataSet.Tables.Contains(this.TableName, text, true, true))
								{
									throw ExceptionBuilder.DuplicateTableName2(this.TableName, text);
								}
								this.CheckCascadingNamespaceConflict(text);
							}
						}
						this.CheckNamespaceValidityForNestedRelations(value);
						this.DoRaiseNamespaceChange();
					}
					this.tableNamespace = value;
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0006B988 File Offset: 0x0006AD88
		internal bool IsNamespaceInherited()
		{
			return this.tableNamespace == null;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0006B9A0 File Offset: 0x0006ADA0
		internal void CheckCascadingNamespaceConflict(string realNamespace)
		{
			foreach (object obj in this.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested && dataRelation.ChildTable != this && dataRelation.ChildTable.tableNamespace == null)
				{
					DataTable childTable = dataRelation.ChildTable;
					if (this.dataSet.Tables.Contains(childTable.TableName, realNamespace, false, true))
					{
						throw ExceptionBuilder.DuplicateTableName2(this.TableName, realNamespace);
					}
					childTable.CheckCascadingNamespaceConflict(realNamespace);
				}
			}
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0006BA54 File Offset: 0x0006AE54
		internal void CheckNamespaceValidityForNestedRelations(string realNamespace)
		{
			foreach (object obj in this.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					if (realNamespace != null)
					{
						dataRelation.ChildTable.CheckNamespaceValidityForNestedParentRelations(realNamespace, this);
					}
					else
					{
						dataRelation.ChildTable.CheckNamespaceValidityForNestedParentRelations(this.GetInheritedNamespace(new List<DataTable>()), this);
					}
				}
			}
			if (realNamespace == null)
			{
				this.CheckNamespaceValidityForNestedParentRelations(this.GetInheritedNamespace(new List<DataTable>()), this);
			}
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0006BAF8 File Offset: 0x0006AEF8
		internal void CheckNamespaceValidityForNestedParentRelations(string ns, DataTable parentTable)
		{
			foreach (object obj in this.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested && dataRelation.ParentTable != parentTable && dataRelation.ParentTable.Namespace != ns)
				{
					throw ExceptionBuilder.InValidNestedRelation(this.TableName);
				}
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0006BB88 File Offset: 0x0006AF88
		internal void DoRaiseNamespaceChange()
		{
			this.RaisePropertyChanging("Namespace");
			foreach (object obj in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn._columnUri == null)
				{
					dataColumn.RaisePropertyChanging("Namespace");
				}
			}
			foreach (object obj2 in this.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj2;
				if (dataRelation.Nested && dataRelation.ChildTable != this)
				{
					DataTable childTable = dataRelation.ChildTable;
					dataRelation.ChildTable.DoRaiseNamespaceChange();
				}
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0006BC78 File Offset: 0x0006B078
		private bool ShouldSerializeNamespace()
		{
			return this.tableNamespace != null;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0006BC90 File Offset: 0x0006B090
		private void ResetNamespace()
		{
			this.Namespace = null;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0006BCA4 File Offset: 0x0006B0A4
		public virtual void BeginInit()
		{
			this.fInitInProgress = true;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0006BCB8 File Offset: 0x0006B0B8
		public virtual void EndInit()
		{
			if (this.dataSet == null || !this.dataSet.fInitInProgress)
			{
				this.Columns.FinishInitCollection();
				this.Constraints.FinishInitConstraints();
				foreach (object obj in this.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.Computed)
					{
						dataColumn.Expression = dataColumn.Expression;
					}
				}
			}
			this.fInitInProgress = false;
			if (this.delayedSetPrimaryKey != null)
			{
				this.PrimaryKey = this.delayedSetPrimaryKey;
				this.delayedSetPrimaryKey = null;
			}
			if (this.delayedViews.Count > 0)
			{
				foreach (DataView dataView in this.delayedViews)
				{
					dataView.EndInit();
				}
				this.delayedViews.Clear();
			}
			this.OnInitialized();
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0006BDE4 File Offset: 0x0006B1E4
		// (set) Token: 0x06000CB4 RID: 3252 RVA: 0x0006BDF8 File Offset: 0x0006B1F8
		[DefaultValue("")]
		[ResDescription("DataTablePrefixDescr")]
		[ResCategory("DataCategory_Data")]
		public string Prefix
		{
			get
			{
				return this.tablePrefix;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				Bid.Trace("<ds.DataTable.set_Prefix|API> %d#, value='%ls'\n", this.ObjectID, value);
				if (XmlConvert.DecodeName(value) == value && XmlConvert.EncodeName(value) != value)
				{
					throw ExceptionBuilder.InvalidPrefix(value);
				}
				this.tablePrefix = value;
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0006BE4C File Offset: 0x0006B24C
		// (set) Token: 0x06000CB6 RID: 3254 RVA: 0x0006BE60 File Offset: 0x0006B260
		internal DataColumn XmlText
		{
			get
			{
				return this.xmlText;
			}
			set
			{
				if (this.xmlText != value)
				{
					if (this.xmlText != null)
					{
						if (value != null)
						{
							throw ExceptionBuilder.MultipleTextOnlyColumns();
						}
						this.Columns.Remove(this.xmlText);
					}
					else if (value != this.Columns[value.ColumnName])
					{
						this.Columns.Add(value);
					}
					this.xmlText = value;
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0006BEC4 File Offset: 0x0006B2C4
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0006BED8 File Offset: 0x0006B2D8
		internal decimal MaxOccurs
		{
			get
			{
				return this.maxOccurs;
			}
			set
			{
				this.maxOccurs = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0006BEEC File Offset: 0x0006B2EC
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0006BF00 File Offset: 0x0006B300
		internal decimal MinOccurs
		{
			get
			{
				return this.minOccurs;
			}
			set
			{
				this.minOccurs = value;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0006BF14 File Offset: 0x0006B314
		internal void SetKeyValues(DataKey key, object[] keyValues, int record)
		{
			for (int i = 0; i < keyValues.Length; i++)
			{
				key.ColumnsReference[i][record] = keyValues[i];
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0006BF44 File Offset: 0x0006B344
		internal DataRow FindByIndex(Index ndx, object[] key)
		{
			Range range = ndx.FindRecords(key);
			if (range.IsNull)
			{
				return null;
			}
			return this.recordManager[ndx.GetRecord(range.Min)];
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x0006BF7C File Offset: 0x0006B37C
		internal DataRow FindMergeTarget(DataRow row, DataKey key, Index ndx)
		{
			DataRow result = null;
			if (key.HasValue)
			{
				int record = (row.oldRecord == -1) ? row.newRecord : row.oldRecord;
				object[] keyValues = key.GetKeyValues(record);
				result = this.FindByIndex(ndx, keyValues);
			}
			return result;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0006BFC0 File Offset: 0x0006B3C0
		private void SetMergeRecords(DataRow row, int newRecord, int oldRecord, DataRowAction action)
		{
			if (newRecord != -1)
			{
				this.SetNewRecord(row, newRecord, action, true, true, false);
				this.SetOldRecord(row, oldRecord);
				return;
			}
			this.SetOldRecord(row, oldRecord);
			if (row.newRecord != -1)
			{
				this.SetNewRecord(row, newRecord, action, true, true, false);
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0006C008 File Offset: 0x0006B408
		internal DataRow MergeRow(DataRow row, DataRow targetRow, bool preserveChanges, Index idxSearch)
		{
			if (targetRow == null)
			{
				targetRow = this.NewEmptyRow();
				targetRow.oldRecord = this.recordManager.ImportRecord(row.Table, row.oldRecord);
				targetRow.newRecord = targetRow.oldRecord;
				if (row.oldRecord != row.newRecord)
				{
					targetRow.newRecord = this.recordManager.ImportRecord(row.Table, row.newRecord);
				}
				this.InsertRow(targetRow, -1L);
			}
			else
			{
				int tempRecord = targetRow.tempRecord;
				targetRow.tempRecord = -1;
				try
				{
					DataRowState rowState = targetRow.RowState;
					int num = (rowState == DataRowState.Added) ? targetRow.newRecord : targetRow.oldRecord;
					if (targetRow.RowState == DataRowState.Unchanged && row.RowState == DataRowState.Unchanged)
					{
						int num2 = targetRow.oldRecord;
						int num3 = preserveChanges ? this.recordManager.CopyRecord(this, num2, -1) : targetRow.newRecord;
						num2 = this.recordManager.CopyRecord(row.Table, row.oldRecord, targetRow.oldRecord);
						this.SetMergeRecords(targetRow, num3, num2, DataRowAction.Change);
					}
					else if (row.newRecord == -1)
					{
						int num2 = targetRow.oldRecord;
						int num3;
						if (preserveChanges)
						{
							num3 = ((targetRow.RowState == DataRowState.Unchanged) ? this.recordManager.CopyRecord(this, num2, -1) : targetRow.newRecord);
						}
						else
						{
							num3 = -1;
						}
						num2 = this.recordManager.CopyRecord(row.Table, row.oldRecord, num2);
						if (num != ((rowState == DataRowState.Added) ? num3 : num2))
						{
							this.SetMergeRecords(targetRow, num3, num2, (num3 == -1) ? DataRowAction.Delete : DataRowAction.Change);
							idxSearch.Reset();
							int num4 = (rowState == DataRowState.Added) ? num3 : num2;
						}
						else
						{
							this.SetMergeRecords(targetRow, num3, num2, (num3 == -1) ? DataRowAction.Delete : DataRowAction.Change);
						}
					}
					else
					{
						int num2 = targetRow.oldRecord;
						int num3 = targetRow.newRecord;
						if (targetRow.RowState == DataRowState.Unchanged)
						{
							num3 = this.recordManager.CopyRecord(this, num2, -1);
						}
						num2 = this.recordManager.CopyRecord(row.Table, row.oldRecord, num2);
						if (!preserveChanges)
						{
							num3 = this.recordManager.CopyRecord(row.Table, row.newRecord, num3);
						}
						this.SetMergeRecords(targetRow, num3, num2, DataRowAction.Change);
					}
					if (rowState == DataRowState.Added && targetRow.oldRecord != -1)
					{
						idxSearch.Reset();
					}
				}
				finally
				{
					targetRow.tempRecord = tempRecord;
				}
			}
			if (row.HasErrors)
			{
				if (targetRow.RowError.Length == 0)
				{
					targetRow.RowError = row.RowError;
				}
				else
				{
					DataRow dataRow = targetRow;
					dataRow.RowError = dataRow.RowError + " ]:[ " + row.RowError;
				}
				DataColumn[] columnsInError = row.GetColumnsInError();
				for (int i = 0; i < columnsInError.Length; i++)
				{
					DataColumn column = targetRow.Table.Columns[columnsInError[i].ColumnName];
					targetRow.SetColumnError(column, row.GetColumnError(columnsInError[i]));
				}
			}
			else if (!preserveChanges)
			{
				targetRow.ClearErrors();
			}
			return targetRow;
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0006C2DC File Offset: 0x0006B6DC
		public void AcceptChanges()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.AcceptChanges|API> %d#\n", this.ObjectID);
			try
			{
				DataRow[] array = new DataRow[this.Rows.Count];
				this.Rows.CopyTo(array, 0);
				this.SuspendIndexEvents();
				try
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].rowID != -1L)
						{
							array[i].AcceptChanges();
						}
					}
				}
				finally
				{
					this.RestoreIndexEvents(false);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0006C38C File Offset: 0x0006B78C
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected virtual DataTable CreateInstance()
		{
			return (DataTable)Activator.CreateInstance(base.GetType(), true);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0006C3AC File Offset: 0x0006B7AC
		public virtual DataTable Clone()
		{
			return this.Clone(null);
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0006C3C0 File Offset: 0x0006B7C0
		internal DataTable Clone(DataSet cloneDS)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.Clone|INFO> %d#, cloneDS=%d\n", this.ObjectID, (cloneDS != null) ? cloneDS.ObjectID : 0);
			DataTable result;
			try
			{
				DataTable dataTable = this.CreateInstance();
				if (dataTable.Columns.Count > 0)
				{
					dataTable.Reset();
				}
				result = this.CloneTo(dataTable, cloneDS, false);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0006C438 File Offset: 0x0006B838
		private DataTable IncrementalCloneTo(DataTable sourceTable, DataTable targetTable)
		{
			foreach (object obj in sourceTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (targetTable.Columns[dataColumn.ColumnName] == null)
				{
					targetTable.Columns.Add(dataColumn.Clone());
				}
			}
			return targetTable;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0006C4BC File Offset: 0x0006B8BC
		private DataTable CloneHierarchy(DataTable sourceTable, DataSet ds, Hashtable visitedMap)
		{
			if (visitedMap == null)
			{
				visitedMap = new Hashtable();
			}
			if (visitedMap.Contains(sourceTable))
			{
				return (DataTable)visitedMap[sourceTable];
			}
			DataTable dataTable = ds.Tables[sourceTable.TableName, sourceTable.Namespace];
			if (dataTable != null && dataTable.Columns.Count > 0)
			{
				dataTable = this.IncrementalCloneTo(sourceTable, dataTable);
			}
			else
			{
				if (dataTable == null)
				{
					dataTable = new DataTable();
					ds.Tables.Add(dataTable);
				}
				dataTable = sourceTable.CloneTo(dataTable, ds, true);
			}
			visitedMap[sourceTable] = dataTable;
			foreach (object obj in sourceTable.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				DataTable dataTable2 = this.CloneHierarchy(dataRelation.ChildTable, ds, visitedMap);
			}
			return dataTable;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0006C5A8 File Offset: 0x0006B9A8
		private DataTable CloneTo(DataTable clone, DataSet cloneDS, bool skipExpressionColumns)
		{
			clone.tableName = this.tableName;
			clone.tableNamespace = this.tableNamespace;
			clone.tablePrefix = this.tablePrefix;
			clone.fNestedInDataset = this.fNestedInDataset;
			clone._culture = this._culture;
			clone._cultureUserSet = this._cultureUserSet;
			clone._compareInfo = this._compareInfo;
			clone._compareFlags = this._compareFlags;
			clone._formatProvider = this._formatProvider;
			clone._hashCodeProvider = this._hashCodeProvider;
			clone._caseSensitive = this._caseSensitive;
			clone._caseSensitiveUserSet = this._caseSensitiveUserSet;
			clone.displayExpression = this.displayExpression;
			clone.typeName = this.typeName;
			clone.repeatableElement = this.repeatableElement;
			clone.MinimumCapacity = this.MinimumCapacity;
			clone.RemotingFormat = this.RemotingFormat;
			DataColumnCollection columns = this.Columns;
			for (int i = 0; i < columns.Count; i++)
			{
				clone.Columns.Add(columns[i].Clone());
			}
			if (!skipExpressionColumns && cloneDS == null)
			{
				for (int j = 0; j < columns.Count; j++)
				{
					clone.Columns[columns[j].ColumnName].Expression = columns[j].Expression;
				}
			}
			DataColumn[] array = this.PrimaryKey;
			if (array.Length != 0)
			{
				DataColumn[] array2 = new DataColumn[array.Length];
				for (int k = 0; k < array.Length; k++)
				{
					array2[k] = clone.Columns[array[k].Ordinal];
				}
				clone.PrimaryKey = array2;
			}
			for (int l = 0; l < this.Constraints.Count; l++)
			{
				ForeignKeyConstraint foreignKeyConstraint = this.Constraints[l] as ForeignKeyConstraint;
				UniqueConstraint uniqueConstraint = this.Constraints[l] as UniqueConstraint;
				if (foreignKeyConstraint != null)
				{
					if (foreignKeyConstraint.Table == foreignKeyConstraint.RelatedTable)
					{
						ForeignKeyConstraint constraint = foreignKeyConstraint.Clone(clone);
						Constraint constraint2 = clone.Constraints.FindConstraint(constraint);
						if (constraint2 != null)
						{
							constraint2.ConstraintName = this.Constraints[l].ConstraintName;
						}
					}
				}
				else if (uniqueConstraint != null)
				{
					UniqueConstraint uniqueConstraint2 = uniqueConstraint.Clone(clone);
					Constraint constraint3 = clone.Constraints.FindConstraint(uniqueConstraint2);
					if (constraint3 != null)
					{
						constraint3.ConstraintName = this.Constraints[l].ConstraintName;
						foreach (object key in uniqueConstraint2.ExtendedProperties.Keys)
						{
							constraint3.ExtendedProperties[key] = uniqueConstraint2.ExtendedProperties[key];
						}
					}
				}
			}
			for (int m = 0; m < this.Constraints.Count; m++)
			{
				if (!clone.Constraints.Contains(this.Constraints[m].ConstraintName, true))
				{
					ForeignKeyConstraint foreignKeyConstraint2 = this.Constraints[m] as ForeignKeyConstraint;
					UniqueConstraint uniqueConstraint3 = this.Constraints[m] as UniqueConstraint;
					if (foreignKeyConstraint2 != null)
					{
						if (foreignKeyConstraint2.Table == foreignKeyConstraint2.RelatedTable)
						{
							ForeignKeyConstraint foreignKeyConstraint3 = foreignKeyConstraint2.Clone(clone);
							if (foreignKeyConstraint3 != null)
							{
								clone.Constraints.Add(foreignKeyConstraint3);
							}
						}
					}
					else if (uniqueConstraint3 != null)
					{
						clone.Constraints.Add(uniqueConstraint3.Clone(clone));
					}
				}
			}
			if (this.extendedProperties != null)
			{
				foreach (object key2 in this.extendedProperties.Keys)
				{
					clone.ExtendedProperties[key2] = this.extendedProperties[key2];
				}
			}
			return clone;
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0006C9AC File Offset: 0x0006BDAC
		public DataTable Copy()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.Copy|API> %d#\n", this.ObjectID);
			DataTable result;
			try
			{
				DataTable dataTable = this.Clone();
				foreach (object obj in this.Rows)
				{
					DataRow row = (DataRow)obj;
					this.CopyRow(dataTable, row);
				}
				result = dataTable;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000CC8 RID: 3272 RVA: 0x0006CA54 File Offset: 0x0006BE54
		// (remove) Token: 0x06000CC9 RID: 3273 RVA: 0x0006CA88 File Offset: 0x0006BE88
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataTableColumnChangingDescr")]
		public event DataColumnChangeEventHandler ColumnChanging
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_ColumnChanging|API> %d#\n", this.ObjectID);
				this.onColumnChangingDelegate = (DataColumnChangeEventHandler)Delegate.Combine(this.onColumnChangingDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_ColumnChanging|API> %d#\n", this.ObjectID);
				this.onColumnChangingDelegate = (DataColumnChangeEventHandler)Delegate.Remove(this.onColumnChangingDelegate, value);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000CCA RID: 3274 RVA: 0x0006CABC File Offset: 0x0006BEBC
		// (remove) Token: 0x06000CCB RID: 3275 RVA: 0x0006CAF0 File Offset: 0x0006BEF0
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataTableColumnChangedDescr")]
		public event DataColumnChangeEventHandler ColumnChanged
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_ColumnChanged|API> %d#\n", this.ObjectID);
				this.onColumnChangedDelegate = (DataColumnChangeEventHandler)Delegate.Combine(this.onColumnChangedDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_ColumnChanged|API> %d#\n", this.ObjectID);
				this.onColumnChangedDelegate = (DataColumnChangeEventHandler)Delegate.Remove(this.onColumnChangedDelegate, value);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000CCC RID: 3276 RVA: 0x0006CB24 File Offset: 0x0006BF24
		// (remove) Token: 0x06000CCD RID: 3277 RVA: 0x0006CB48 File Offset: 0x0006BF48
		[ResCategory("DataCategory_Action")]
		[ResDescription("DataSetInitializedDescr")]
		public event EventHandler Initialized
		{
			add
			{
				this.onInitialized = (EventHandler)Delegate.Combine(this.onInitialized, value);
			}
			remove
			{
				this.onInitialized = (EventHandler)Delegate.Remove(this.onInitialized, value);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000CCE RID: 3278 RVA: 0x0006CB6C File Offset: 0x0006BF6C
		// (remove) Token: 0x06000CCF RID: 3279 RVA: 0x0006CBA0 File Offset: 0x0006BFA0
		internal event PropertyChangedEventHandler PropertyChanging
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_PropertyChanging|INFO> %d#\n", this.ObjectID);
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Combine(this.onPropertyChangingDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_PropertyChanging|INFO> %d#\n", this.ObjectID);
				this.onPropertyChangingDelegate = (PropertyChangedEventHandler)Delegate.Remove(this.onPropertyChangingDelegate, value);
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000CD0 RID: 3280 RVA: 0x0006CBD4 File Offset: 0x0006BFD4
		// (remove) Token: 0x06000CD1 RID: 3281 RVA: 0x0006CC08 File Offset: 0x0006C008
		[ResDescription("DataTableRowChangedDescr")]
		[ResCategory("DataCategory_Data")]
		public event DataRowChangeEventHandler RowChanged
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_RowChanged|API> %d#\n", this.ObjectID);
				this.onRowChangedDelegate = (DataRowChangeEventHandler)Delegate.Combine(this.onRowChangedDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_RowChanged|API> %d#\n", this.ObjectID);
				this.onRowChangedDelegate = (DataRowChangeEventHandler)Delegate.Remove(this.onRowChangedDelegate, value);
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000CD2 RID: 3282 RVA: 0x0006CC3C File Offset: 0x0006C03C
		// (remove) Token: 0x06000CD3 RID: 3283 RVA: 0x0006CC70 File Offset: 0x0006C070
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataTableRowChangingDescr")]
		public event DataRowChangeEventHandler RowChanging
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_RowChanging|API> %d#\n", this.ObjectID);
				this.onRowChangingDelegate = (DataRowChangeEventHandler)Delegate.Combine(this.onRowChangingDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_RowChanging|API> %d#\n", this.ObjectID);
				this.onRowChangingDelegate = (DataRowChangeEventHandler)Delegate.Remove(this.onRowChangingDelegate, value);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000CD4 RID: 3284 RVA: 0x0006CCA4 File Offset: 0x0006C0A4
		// (remove) Token: 0x06000CD5 RID: 3285 RVA: 0x0006CCD8 File Offset: 0x0006C0D8
		[ResDescription("DataTableRowDeletingDescr")]
		[ResCategory("DataCategory_Data")]
		public event DataRowChangeEventHandler RowDeleting
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_RowDeleting|API> %d#\n", this.ObjectID);
				this.onRowDeletingDelegate = (DataRowChangeEventHandler)Delegate.Combine(this.onRowDeletingDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_RowDeleting|API> %d#\n", this.ObjectID);
				this.onRowDeletingDelegate = (DataRowChangeEventHandler)Delegate.Remove(this.onRowDeletingDelegate, value);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000CD6 RID: 3286 RVA: 0x0006CD0C File Offset: 0x0006C10C
		// (remove) Token: 0x06000CD7 RID: 3287 RVA: 0x0006CD40 File Offset: 0x0006C140
		[ResDescription("DataTableRowDeletedDescr")]
		[ResCategory("DataCategory_Data")]
		public event DataRowChangeEventHandler RowDeleted
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_RowDeleted|API> %d#\n", this.ObjectID);
				this.onRowDeletedDelegate = (DataRowChangeEventHandler)Delegate.Combine(this.onRowDeletedDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_RowDeleted|API> %d#\n", this.ObjectID);
				this.onRowDeletedDelegate = (DataRowChangeEventHandler)Delegate.Remove(this.onRowDeletedDelegate, value);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000CD8 RID: 3288 RVA: 0x0006CD74 File Offset: 0x0006C174
		// (remove) Token: 0x06000CD9 RID: 3289 RVA: 0x0006CDA8 File Offset: 0x0006C1A8
		[ResDescription("DataTableRowsClearingDescr")]
		[ResCategory("DataCategory_Data")]
		public event DataTableClearEventHandler TableClearing
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_TableClearing|API> %d#\n", this.ObjectID);
				this.onTableClearingDelegate = (DataTableClearEventHandler)Delegate.Combine(this.onTableClearingDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_TableClearing|API> %d#\n", this.ObjectID);
				this.onTableClearingDelegate = (DataTableClearEventHandler)Delegate.Remove(this.onTableClearingDelegate, value);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000CDA RID: 3290 RVA: 0x0006CDDC File Offset: 0x0006C1DC
		// (remove) Token: 0x06000CDB RID: 3291 RVA: 0x0006CE10 File Offset: 0x0006C210
		[ResCategory("DataCategory_Data")]
		[ResDescription("DataTableRowsClearedDescr")]
		public event DataTableClearEventHandler TableCleared
		{
			add
			{
				Bid.Trace("<ds.DataTable.add_TableCleared|API> %d#\n", this.ObjectID);
				this.onTableClearedDelegate = (DataTableClearEventHandler)Delegate.Combine(this.onTableClearedDelegate, value);
			}
			remove
			{
				Bid.Trace("<ds.DataTable.remove_TableCleared|API> %d#\n", this.ObjectID);
				this.onTableClearedDelegate = (DataTableClearEventHandler)Delegate.Remove(this.onTableClearedDelegate, value);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000CDC RID: 3292 RVA: 0x0006CE44 File Offset: 0x0006C244
		// (remove) Token: 0x06000CDD RID: 3293 RVA: 0x0006CE68 File Offset: 0x0006C268
		[ResDescription("DataTableRowsNewRowDescr")]
		[ResCategory("DataCategory_Data")]
		public event DataTableNewRowEventHandler TableNewRow
		{
			add
			{
				this.onTableNewRowDelegate = (DataTableNewRowEventHandler)Delegate.Combine(this.onTableNewRowDelegate, value);
			}
			remove
			{
				this.onTableNewRowDelegate = (DataTableNewRowEventHandler)Delegate.Remove(this.onTableNewRowDelegate, value);
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0006CE8C File Offset: 0x0006C28C
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x0006CEA0 File Offset: 0x0006C2A0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				ISite site = this.Site;
				if (value == null && site != null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						for (int i = 0; i < this.Columns.Count; i++)
						{
							if (this.Columns[i].Site != null)
							{
								container.Remove(this.Columns[i]);
							}
						}
					}
				}
				base.Site = value;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0006CF08 File Offset: 0x0006C308
		internal DataRow AddRecords(int oldRecord, int newRecord)
		{
			DataRow dataRow;
			if (oldRecord == -1 && newRecord == -1)
			{
				dataRow = this.NewRow(-1);
				this.AddRow(dataRow);
			}
			else
			{
				dataRow = this.NewEmptyRow();
				dataRow.oldRecord = oldRecord;
				dataRow.newRecord = newRecord;
				this.InsertRow(dataRow, -1L);
			}
			return dataRow;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0006CF50 File Offset: 0x0006C350
		internal void AddRow(DataRow row)
		{
			this.AddRow(row, -1);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0006CF68 File Offset: 0x0006C368
		internal void AddRow(DataRow row, int proposedID)
		{
			this.InsertRow(row, proposedID, -1);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0006CF80 File Offset: 0x0006C380
		internal void InsertRow(DataRow row, int proposedID, int pos)
		{
			this.InsertRow(row, (long)proposedID, pos, true);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0006CF98 File Offset: 0x0006C398
		internal void InsertRow(DataRow row, long proposedID, int pos, bool fireEvent)
		{
			Exception ex = null;
			if (row == null)
			{
				throw ExceptionBuilder.ArgumentNull("row");
			}
			if (row.Table != this)
			{
				throw ExceptionBuilder.RowAlreadyInOtherCollection();
			}
			if (row.rowID != -1L)
			{
				throw ExceptionBuilder.RowAlreadyInTheCollection();
			}
			row.BeginEdit();
			int tempRecord = row.tempRecord;
			row.tempRecord = -1;
			if (proposedID == -1L)
			{
				proposedID = this.nextRowID;
			}
			bool flag;
			if (flag = (this.nextRowID <= proposedID))
			{
				this.nextRowID = checked(proposedID + 1L);
			}
			try
			{
				try
				{
					row.rowID = proposedID;
					this.SetNewRecordWorker(row, tempRecord, DataRowAction.Add, false, false, pos, fireEvent, out ex);
				}
				catch
				{
					if (flag && this.nextRowID == proposedID + 1L)
					{
						this.nextRowID = proposedID;
					}
					row.rowID = -1L;
					row.tempRecord = tempRecord;
					throw;
				}
				if (ex != null)
				{
					throw ex;
				}
				if (this.EnforceConstraints && !this.inLoad)
				{
					int count = this.columnCollection.Count;
					for (int i = 0; i < count; i++)
					{
						DataColumn dataColumn = this.columnCollection[i];
						if (dataColumn.Computed)
						{
							dataColumn.CheckColumnConstraint(row, DataRowAction.Add);
						}
					}
				}
			}
			finally
			{
				row.ResetLastChangedColumn();
			}
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0006D0E0 File Offset: 0x0006C4E0
		internal void CheckNotModifying(DataRow row)
		{
			if (row.tempRecord != -1)
			{
				row.EndEdit();
			}
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0006D0FC File Offset: 0x0006C4FC
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0006D110 File Offset: 0x0006C510
		internal void Clear(bool clearAll)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.Clear|INFO> %d#, clearAll=%d{bool}\n", this.ObjectID, clearAll);
			try
			{
				this.rowDiffId = null;
				if (this.dataSet != null)
				{
					this.dataSet.OnClearFunctionCalled(this);
				}
				bool flag = this.Rows.Count != 0;
				DataTableClearEventArgs e = null;
				if (flag)
				{
					e = new DataTableClearEventArgs(this);
					this.OnTableClearing(e);
				}
				if (this.dataSet != null && this.dataSet.EnforceConstraints)
				{
					ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.dataSet, this);
					while (parentForeignKeyConstraintEnumerator.GetNext())
					{
						ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
						foreignKeyConstraint.CheckCanClearParentTable(this);
					}
				}
				this.recordManager.Clear(clearAll);
				foreach (object obj in this.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					dataRow.oldRecord = -1;
					dataRow.newRecord = -1;
					dataRow.tempRecord = -1;
					dataRow.rowID = -1L;
					dataRow.RBTreeNodeId = 0;
				}
				this.Rows.ArrayClear();
				this.ResetIndexes();
				if (flag)
				{
					this.OnTableCleared(e);
				}
				foreach (object obj2 in this.Columns)
				{
					DataColumn column = (DataColumn)obj2;
					this.EvaluateDependentExpressions(column);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0006D2C8 File Offset: 0x0006C6C8
		internal void CascadeAll(DataRow row, DataRowAction action)
		{
			if (this.DataSet != null && this.DataSet.fEnableCascading)
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.dataSet, this);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint().CheckCascade(row, action);
				}
			}
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0006D310 File Offset: 0x0006C710
		internal void CommitRow(DataRow row)
		{
			DataRowChangeEventArgs args = this.OnRowChanging(null, row, DataRowAction.Commit);
			if (!this.inDataLoad)
			{
				this.CascadeAll(row, DataRowAction.Commit);
			}
			this.SetOldRecord(row, row.newRecord);
			this.OnRowChanged(args, row, DataRowAction.Commit);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0006D350 File Offset: 0x0006C750
		internal int Compare(string s1, string s2)
		{
			return this.Compare(s1, s2, null);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x0006D368 File Offset: 0x0006C768
		internal int Compare(string s1, string s2, CompareInfo comparer)
		{
			if (s1 == s2)
			{
				return 0;
			}
			if (s1 == null)
			{
				return -1;
			}
			if (s2 == null)
			{
				return 1;
			}
			int i = s1.Length;
			int num = s2.Length;
			while (i > 0)
			{
				if (s1[i - 1] != ' ' && s1[i - 1] != '\u3000')
				{
					IL_6C:
					while (num > 0 && (s2[num - 1] == ' ' || s2[num - 1] == '\u3000'))
					{
						num--;
					}
					return (comparer ?? this.CompareInfo).Compare(s1, 0, i, s2, 0, num, this._compareFlags);
				}
				i--;
			}
			goto IL_6C;
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0006D404 File Offset: 0x0006C804
		internal int IndexOf(string s1, string s2)
		{
			return this.CompareInfo.IndexOf(s1, s2, this._compareFlags);
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x0006D424 File Offset: 0x0006C824
		internal bool IsSuffix(string s1, string s2)
		{
			return this.CompareInfo.IsSuffix(s1, s2, this._compareFlags);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0006D444 File Offset: 0x0006C844
		public object Compute(string expression, string filter)
		{
			DataRow[] rows = this.Select(filter, "", DataViewRowState.CurrentRows);
			DataExpression dataExpression = new DataExpression(this, expression);
			return dataExpression.Evaluate(rows);
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0006D470 File Offset: 0x0006C870
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000CF0 RID: 3312 RVA: 0x0006D480 File Offset: 0x0006C880
		internal void CopyRow(DataTable table, DataRow row)
		{
			int num = -1;
			int newRecord = -1;
			if (row == null)
			{
				return;
			}
			if (row.oldRecord != -1)
			{
				num = table.recordManager.ImportRecord(row.Table, row.oldRecord);
			}
			if (row.newRecord != -1)
			{
				if (row.newRecord != row.oldRecord)
				{
					newRecord = table.recordManager.ImportRecord(row.Table, row.newRecord);
				}
				else
				{
					newRecord = num;
				}
			}
			DataRow dataRow = table.AddRecords(num, newRecord);
			if (row.HasErrors)
			{
				dataRow.RowError = row.RowError;
				DataColumn[] columnsInError = row.GetColumnsInError();
				for (int i = 0; i < columnsInError.Length; i++)
				{
					DataColumn column = dataRow.Table.Columns[columnsInError[i].ColumnName];
					dataRow.SetColumnError(column, row.GetColumnError(columnsInError[i]));
				}
			}
		}

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0006D54C File Offset: 0x0006C94C
		internal void DeleteRow(DataRow row)
		{
			if (row.newRecord == -1)
			{
				throw ExceptionBuilder.RowAlreadyDeleted();
			}
			this.SetNewRecord(row, -1, DataRowAction.Delete, false, true, false);
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0006D574 File Offset: 0x0006C974
		private void CheckPrimaryKey()
		{
			if (this.primaryKey == null)
			{
				throw ExceptionBuilder.TableMissingPrimaryKey();
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0006D590 File Offset: 0x0006C990
		internal DataRow FindByPrimaryKey(object[] values)
		{
			this.CheckPrimaryKey();
			return this.FindRow(this.primaryKey.Key, values);
		}

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0006D5B8 File Offset: 0x0006C9B8
		internal DataRow FindByPrimaryKey(object value)
		{
			this.CheckPrimaryKey();
			return this.FindRow(this.primaryKey.Key, value);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x0006D5E0 File Offset: 0x0006C9E0
		private DataRow FindRow(DataKey key, object[] values)
		{
			Index index = this.GetIndex(this.NewIndexDesc(key));
			Range range = index.FindRecords(values);
			if (range.IsNull)
			{
				return null;
			}
			return this.recordManager[index.GetRecord(range.Min)];
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0006D628 File Offset: 0x0006CA28
		private DataRow FindRow(DataKey key, object value)
		{
			Index index = this.GetIndex(this.NewIndexDesc(key));
			Range range = index.FindRecords(value);
			if (range.IsNull)
			{
				return null;
			}
			return this.recordManager[index.GetRecord(range.Min)];
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0006D670 File Offset: 0x0006CA70
		internal string FormatSortString(IndexField[] indexDesc)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (IndexField indexField in indexDesc)
			{
				if (0 < stringBuilder.Length)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(indexField.Column.ColumnName);
				if (indexField.IsDescending)
				{
					stringBuilder.Append(" DESC");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0006D6DC File Offset: 0x0006CADC
		internal void FreeRecord(ref int record)
		{
			this.recordManager.FreeRecord(ref record);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0006D6F8 File Offset: 0x0006CAF8
		public DataTable GetChanges()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.GetChanges|API> %d#\n", this.ObjectID);
			DataTable result;
			try
			{
				DataTable dataTable = this.Clone();
				for (int i = 0; i < this.Rows.Count; i++)
				{
					DataRow dataRow = this.Rows[i];
					if (dataRow.oldRecord != dataRow.newRecord)
					{
						dataTable.ImportRow(dataRow);
					}
				}
				if (dataTable.Rows.Count == 0)
				{
					result = null;
				}
				else
				{
					result = dataTable;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0006D794 File Offset: 0x0006CB94
		public DataTable GetChanges(DataRowState rowStates)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.GetChanges|API> %d#, rowStates=%d{ds.DataRowState}\n", this.ObjectID, (int)rowStates);
			DataTable result;
			try
			{
				DataTable dataTable = this.Clone();
				for (int i = 0; i < this.Rows.Count; i++)
				{
					DataRow dataRow = this.Rows[i];
					if ((dataRow.RowState & rowStates) != (DataRowState)0)
					{
						dataTable.ImportRow(dataRow);
					}
				}
				if (dataTable.Rows.Count == 0)
				{
					result = null;
				}
				else
				{
					result = dataTable;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0006D82C File Offset: 0x0006CC2C
		public DataRow[] GetErrors()
		{
			List<DataRow> list = new List<DataRow>();
			for (int i = 0; i < this.Rows.Count; i++)
			{
				DataRow dataRow = this.Rows[i];
				if (dataRow.HasErrors)
				{
					list.Add(dataRow);
				}
			}
			DataRow[] array = this.NewRowArray(list.Count);
			list.CopyTo(array);
			return array;
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0006D888 File Offset: 0x0006CC88
		internal Index GetIndex(IndexField[] indexDesc)
		{
			return this.GetIndex(indexDesc, DataViewRowState.CurrentRows, null);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0006D8A0 File Offset: 0x0006CCA0
		internal Index GetIndex(string sort, DataViewRowState recordStates, IFilter rowFilter)
		{
			return this.GetIndex(this.ParseSortString(sort), recordStates, rowFilter);
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0006D8BC File Offset: 0x0006CCBC
		internal Index GetIndex(IndexField[] indexDesc, DataViewRowState recordStates, IFilter rowFilter)
		{
			this.indexesLock.AcquireReaderLock(-1);
			try
			{
				for (int i = 0; i < this.indexes.Count; i++)
				{
					Index index = this.indexes[i];
					if (index != null && index.Equal(indexDesc, recordStates, rowFilter))
					{
						return index;
					}
				}
			}
			finally
			{
				this.indexesLock.ReleaseReaderLock();
			}
			Index index2 = new Index(this, indexDesc, recordStates, rowFilter);
			index2.AddRef();
			return index2;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0006D948 File Offset: 0x0006CD48
		IList IListSource.GetList()
		{
			return this.DefaultView;
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0006D95C File Offset: 0x0006CD5C
		internal List<DataViewListener> GetListeners()
		{
			return this._dataViewListeners;
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0006D970 File Offset: 0x0006CD70
		internal int GetSpecialHashCode(string name)
		{
			int num = 0;
			while (num < name.Length && '\u3000' > name[num])
			{
				num++;
			}
			if (name.Length == num)
			{
				if (this._hashCodeProvider == null)
				{
					this._hashCodeProvider = StringComparer.Create(this.Locale, true);
				}
				return this._hashCodeProvider.GetHashCode(name);
			}
			return 0;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0006D9D0 File Offset: 0x0006CDD0
		public void ImportRow(DataRow row)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.ImportRow|API> %d#\n", this.ObjectID);
			try
			{
				int num = -1;
				int num2 = -1;
				if (row != null)
				{
					if (row.oldRecord != -1)
					{
						num = this.recordManager.ImportRecord(row.Table, row.oldRecord);
					}
					if (row.newRecord != -1)
					{
						if (row.RowState != DataRowState.Unchanged)
						{
							num2 = this.recordManager.ImportRecord(row.Table, row.newRecord);
						}
						else
						{
							num2 = num;
						}
					}
					if (num != -1 || num2 != -1)
					{
						DataRow dataRow = this.AddRecords(num, num2);
						if (row.HasErrors)
						{
							dataRow.RowError = row.RowError;
							DataColumn[] columnsInError = row.GetColumnsInError();
							for (int i = 0; i < columnsInError.Length; i++)
							{
								DataColumn column = dataRow.Table.Columns[columnsInError[i].ColumnName];
								dataRow.SetColumnError(column, row.GetColumnError(columnsInError[i]));
							}
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0006DADC File Offset: 0x0006CEDC
		internal void InsertRow(DataRow row, long proposedID)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.InsertRow|INFO> %d#, row=%d\n", this.ObjectID, row.ObjectID);
			try
			{
				if (row.Table != this)
				{
					throw ExceptionBuilder.RowAlreadyInOtherCollection();
				}
				if (row.rowID != -1L)
				{
					throw ExceptionBuilder.RowAlreadyInTheCollection();
				}
				if (row.oldRecord == -1 && row.newRecord == -1)
				{
					throw ExceptionBuilder.RowEmpty();
				}
				if (proposedID == -1L)
				{
					proposedID = this.nextRowID;
				}
				row.rowID = proposedID;
				if (this.nextRowID <= proposedID)
				{
					this.nextRowID = checked(proposedID + 1L);
				}
				DataRowChangeEventArgs args = null;
				if (row.newRecord != -1)
				{
					row.tempRecord = row.newRecord;
					row.newRecord = -1;
					try
					{
						args = this.RaiseRowChanging(null, row, DataRowAction.Add, true);
					}
					catch
					{
						row.tempRecord = -1;
						throw;
					}
					row.newRecord = row.tempRecord;
					row.tempRecord = -1;
				}
				if (row.oldRecord != -1)
				{
					this.recordManager[row.oldRecord] = row;
				}
				if (row.newRecord != -1)
				{
					this.recordManager[row.newRecord] = row;
				}
				this.Rows.ArrayAdd(row);
				if (row.RowState == DataRowState.Unchanged)
				{
					this.RecordStateChanged(row.oldRecord, DataViewRowState.None, DataViewRowState.Unchanged);
				}
				else
				{
					this.RecordStateChanged(row.oldRecord, DataViewRowState.None, row.GetRecordState(row.oldRecord), row.newRecord, DataViewRowState.None, row.GetRecordState(row.newRecord));
				}
				if (this.dependentColumns != null && this.dependentColumns.Count > 0)
				{
					this.EvaluateExpressions(row, DataRowAction.Add, null);
				}
				this.RaiseRowChanged(args, row, DataRowAction.Add);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0006DC9C File Offset: 0x0006D09C
		private IndexField[] NewIndexDesc(DataKey key)
		{
			IndexField[] indexDesc = key.GetIndexDesc();
			IndexField[] array = new IndexField[indexDesc.Length];
			Array.Copy(indexDesc, 0, array, 0, indexDesc.Length);
			return array;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0006DCC8 File Offset: 0x0006D0C8
		internal int NewRecord()
		{
			return this.NewRecord(-1);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0006DCDC File Offset: 0x0006D0DC
		internal int NewUninitializedRecord()
		{
			return this.recordManager.NewRecordBase();
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0006DCF4 File Offset: 0x0006D0F4
		internal int NewRecordFromArray(object[] value)
		{
			int count = this.columnCollection.Count;
			if (count < value.Length)
			{
				throw ExceptionBuilder.ValueArrayLength();
			}
			int num = this.recordManager.NewRecordBase();
			int result;
			try
			{
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] != null)
					{
						this.columnCollection[i][num] = value[i];
					}
					else
					{
						this.columnCollection[i].Init(num);
					}
				}
				for (int j = value.Length; j < count; j++)
				{
					this.columnCollection[j].Init(num);
				}
				result = num;
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableOrSecurityExceptionType(e))
				{
					this.FreeRecord(ref num);
				}
				throw;
			}
			return result;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0006DDBC File Offset: 0x0006D1BC
		internal int NewRecord(int sourceRecord)
		{
			int num = this.recordManager.NewRecordBase();
			int count = this.columnCollection.Count;
			if (-1 == sourceRecord)
			{
				for (int i = 0; i < count; i++)
				{
					this.columnCollection[i].Init(num);
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					this.columnCollection[j].Copy(sourceRecord, num);
				}
			}
			return num;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0006DE28 File Offset: 0x0006D228
		internal DataRow NewEmptyRow()
		{
			this.rowBuilder._record = -1;
			DataRow dataRow = this.NewRowFromBuilder(this.rowBuilder);
			if (this.dataSet != null)
			{
				this.DataSet.OnDataRowCreated(dataRow);
			}
			return dataRow;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0006DE64 File Offset: 0x0006D264
		private DataRow NewUninitializedRow()
		{
			return this.NewRow(this.NewUninitializedRecord());
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0006DE80 File Offset: 0x0006D280
		public DataRow NewRow()
		{
			DataRow dataRow = this.NewRow(-1);
			this.NewRowCreated(dataRow);
			return dataRow;
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0006DEA0 File Offset: 0x0006D2A0
		internal DataRow CreateEmptyRow()
		{
			DataRow dataRow = this.NewUninitializedRow();
			foreach (object obj in this.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (!XmlToDatasetMap.IsMappedColumn(dataColumn))
				{
					if (!dataColumn.AutoIncrement)
					{
						if (dataColumn.AllowDBNull)
						{
							dataRow[dataColumn] = DBNull.Value;
						}
						else if (dataColumn.DefaultValue != null)
						{
							dataRow[dataColumn] = dataColumn.DefaultValue;
						}
					}
					else
					{
						dataColumn.Init(dataRow.tempRecord);
					}
				}
			}
			return dataRow;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0006DF50 File Offset: 0x0006D350
		private void NewRowCreated(DataRow row)
		{
			if (this.onTableNewRowDelegate != null)
			{
				DataTableNewRowEventArgs e = new DataTableNewRowEventArgs(row);
				this.OnTableNewRow(e);
			}
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0006DF74 File Offset: 0x0006D374
		internal DataRow NewRow(int record)
		{
			if (-1 == record)
			{
				record = this.NewRecord(-1);
			}
			this.rowBuilder._record = record;
			DataRow dataRow = this.NewRowFromBuilder(this.rowBuilder);
			this.recordManager[record] = dataRow;
			if (this.dataSet != null)
			{
				this.DataSet.OnDataRowCreated(dataRow);
			}
			return dataRow;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0006DFCC File Offset: 0x0006D3CC
		protected virtual DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new DataRow(builder);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0006DFE0 File Offset: 0x0006D3E0
		protected virtual Type GetRowType()
		{
			return typeof(DataRow);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0006DFF8 File Offset: 0x0006D3F8
		[MethodImpl(MethodImplOptions.NoInlining)]
		protected internal DataRow[] NewRowArray(int size)
		{
			if (this.IsTypedDataTable)
			{
				if (size == 0)
				{
					if (this.EmptyDataRowArray == null)
					{
						this.EmptyDataRowArray = (DataRow[])Array.CreateInstance(this.GetRowType(), 0);
					}
					return this.EmptyDataRowArray;
				}
				return (DataRow[])Array.CreateInstance(this.GetRowType(), size);
			}
			else
			{
				if (size != 0)
				{
					return new DataRow[size];
				}
				return DataTable.zeroRows;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0006E058 File Offset: 0x0006D458
		internal bool NeedColumnChangeEvents
		{
			get
			{
				return this.IsTypedDataTable || this.onColumnChangingDelegate != null || this.onColumnChangedDelegate != null;
			}
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0006E080 File Offset: 0x0006D480
		protected internal virtual void OnColumnChanging(DataColumnChangeEventArgs e)
		{
			if (this.onColumnChangingDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnColumnChanging|INFO> %d#\n", this.ObjectID);
				this.onColumnChangingDelegate(this, e);
			}
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0006E0B4 File Offset: 0x0006D4B4
		protected internal virtual void OnColumnChanged(DataColumnChangeEventArgs e)
		{
			if (this.onColumnChangedDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnColumnChanged|INFO> %d#\n", this.ObjectID);
				this.onColumnChangedDelegate(this, e);
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0006E0E8 File Offset: 0x0006D4E8
		protected virtual void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.onPropertyChangingDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnPropertyChanging|INFO> %d#\n", this.ObjectID);
				this.onPropertyChangingDelegate(this, pcevent);
			}
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0006E11C File Offset: 0x0006D51C
		internal void OnRemoveColumnInternal(DataColumn column)
		{
			this.OnRemoveColumn(column);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0006E130 File Offset: 0x0006D530
		protected virtual void OnRemoveColumn(DataColumn column)
		{
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0006E140 File Offset: 0x0006D540
		private DataRowChangeEventArgs OnRowChanged(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			if (this.onRowChangedDelegate != null || this.IsTypedDataTable)
			{
				if (args == null)
				{
					args = new DataRowChangeEventArgs(eRow, eAction);
				}
				this.OnRowChanged(args);
			}
			return args;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0006E174 File Offset: 0x0006D574
		private DataRowChangeEventArgs OnRowChanging(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			if (this.onRowChangingDelegate != null || this.IsTypedDataTable)
			{
				if (args == null)
				{
					args = new DataRowChangeEventArgs(eRow, eAction);
				}
				this.OnRowChanging(args);
			}
			return args;
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0006E1A8 File Offset: 0x0006D5A8
		protected virtual void OnRowChanged(DataRowChangeEventArgs e)
		{
			if (this.onRowChangedDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnRowChanged|INFO> %d#\n", this.ObjectID);
				this.onRowChangedDelegate(this, e);
			}
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0006E1DC File Offset: 0x0006D5DC
		protected virtual void OnRowChanging(DataRowChangeEventArgs e)
		{
			if (this.onRowChangingDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnRowChanging|INFO> %d#\n", this.ObjectID);
				this.onRowChangingDelegate(this, e);
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0006E210 File Offset: 0x0006D610
		protected virtual void OnRowDeleting(DataRowChangeEventArgs e)
		{
			if (this.onRowDeletingDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnRowDeleting|INFO> %d#\n", this.ObjectID);
				this.onRowDeletingDelegate(this, e);
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0006E244 File Offset: 0x0006D644
		protected virtual void OnRowDeleted(DataRowChangeEventArgs e)
		{
			if (this.onRowDeletedDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnRowDeleted|INFO> %d#\n", this.ObjectID);
				this.onRowDeletedDelegate(this, e);
			}
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0006E278 File Offset: 0x0006D678
		protected virtual void OnTableCleared(DataTableClearEventArgs e)
		{
			if (this.onTableClearedDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnTableCleared|INFO> %d#\n", this.ObjectID);
				this.onTableClearedDelegate(this, e);
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0006E2AC File Offset: 0x0006D6AC
		protected virtual void OnTableClearing(DataTableClearEventArgs e)
		{
			if (this.onTableClearingDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnTableClearing|INFO> %d#\n", this.ObjectID);
				this.onTableClearingDelegate(this, e);
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0006E2E0 File Offset: 0x0006D6E0
		protected virtual void OnTableNewRow(DataTableNewRowEventArgs e)
		{
			if (this.onTableNewRowDelegate != null)
			{
				Bid.Trace("<ds.DataTable.OnTableNewRow|INFO> %d#\n", this.ObjectID);
				this.onTableNewRowDelegate(this, e);
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0006E314 File Offset: 0x0006D714
		private void OnInitialized()
		{
			if (this.onInitialized != null)
			{
				Bid.Trace("<ds.DataTable.OnInitialized|INFO> %d#\n", this.ObjectID);
				this.onInitialized(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0006E34C File Offset: 0x0006D74C
		internal IndexField[] ParseSortString(string sortString)
		{
			IndexField[] array = DataTable.zeroIndexField;
			if (sortString != null && 0 < sortString.Length)
			{
				string[] array2 = sortString.Split(new char[]
				{
					','
				});
				array = new IndexField[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i].Trim();
					int length = text.Length;
					bool isDescending = false;
					if (length >= 5 && string.Compare(text, length - 4, " ASC", 0, 4, StringComparison.OrdinalIgnoreCase) == 0)
					{
						text = text.Substring(0, length - 4).Trim();
					}
					else if (length >= 6 && string.Compare(text, length - 5, " DESC", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
					{
						isDescending = true;
						text = text.Substring(0, length - 5).Trim();
					}
					if (text.StartsWith("[", StringComparison.Ordinal))
					{
						if (!text.EndsWith("]", StringComparison.Ordinal))
						{
							throw ExceptionBuilder.InvalidSortString(array2[i]);
						}
						text = text.Substring(1, text.Length - 2);
					}
					DataColumn dataColumn = this.Columns[text];
					if (dataColumn == null)
					{
						throw ExceptionBuilder.ColumnOutOfRange(text);
					}
					array[i] = new IndexField(dataColumn, isDescending);
				}
			}
			return array;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0006E46C File Offset: 0x0006D86C
		internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0006E488 File Offset: 0x0006D888
		internal void RecordChanged(int record)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this.shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this.shadowIndexes[i];
					if (0 < index.RefCount)
					{
						index.RecordChanged(record);
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0006E4F8 File Offset: 0x0006D8F8
		internal void RecordChanged(int[] oldIndex, int[] newIndex)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this.shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this.shadowIndexes[i];
					if (0 < index.RefCount)
					{
						index.RecordChanged(oldIndex[i], newIndex[i]);
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0006E56C File Offset: 0x0006D96C
		internal void RecordStateChanged(int record, DataViewRowState oldState, DataViewRowState newState)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this.shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this.shadowIndexes[i];
					if (0 < index.RefCount)
					{
						index.RecordStateChanged(record, oldState, newState);
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0006E5DC File Offset: 0x0006D9DC
		internal void RecordStateChanged(int record1, DataViewRowState oldState1, DataViewRowState newState1, int record2, DataViewRowState oldState2, DataViewRowState newState2)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this.shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this.shadowIndexes[i];
					if (0 < index.RefCount)
					{
						if (record1 != -1 && record2 != -1)
						{
							index.RecordStateChanged(record1, oldState1, newState1, record2, oldState2, newState2);
						}
						else if (record1 != -1)
						{
							index.RecordStateChanged(record1, oldState1, newState1);
						}
						else if (record2 != -1)
						{
							index.RecordStateChanged(record2, oldState2, newState2);
						}
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0006E67C File Offset: 0x0006DA7C
		internal int[] RemoveRecordFromIndexes(DataRow row, DataRowVersion version)
		{
			int num = this.LiveIndexes.Count;
			int[] array = new int[num];
			int recordFromVersion = row.GetRecordFromVersion(version);
			DataViewRowState recordState = row.GetRecordState(recordFromVersion);
			while (--num >= 0)
			{
				if (row.HasVersion(version) && (recordState & this.indexes[num].RecordStates) != DataViewRowState.None)
				{
					int index = this.indexes[num].GetIndex(recordFromVersion);
					if (index > -1)
					{
						array[num] = index;
						this.indexes[num].DeleteRecordFromIndex(index);
					}
					else
					{
						array[num] = -1;
					}
				}
				else
				{
					array[num] = -1;
				}
			}
			return array;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0006E710 File Offset: 0x0006DB10
		internal int[] InsertRecordToIndexes(DataRow row, DataRowVersion version)
		{
			int num = this.LiveIndexes.Count;
			int[] array = new int[num];
			int recordFromVersion = row.GetRecordFromVersion(version);
			DataViewRowState recordState = row.GetRecordState(recordFromVersion);
			while (--num >= 0)
			{
				if (row.HasVersion(version))
				{
					if ((recordState & this.indexes[num].RecordStates) != DataViewRowState.None)
					{
						array[num] = this.indexes[num].InsertRecordToIndex(recordFromVersion);
					}
					else
					{
						array[num] = -1;
					}
				}
			}
			return array;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0006E784 File Offset: 0x0006DB84
		internal void SilentlySetValue(DataRow dr, DataColumn dc, DataRowVersion version, object newValue)
		{
			int recordFromVersion = dr.GetRecordFromVersion(version);
			if ((DataStorage.IsTypeCustomType(dc.DataType) && newValue != dc[recordFromVersion]) || !dc.CompareValueTo(recordFromVersion, newValue, true))
			{
				int[] oldIndex = dr.Table.RemoveRecordFromIndexes(dr, version);
				dc.SetValue(recordFromVersion, newValue);
				int[] newIndex = dr.Table.InsertRecordToIndexes(dr, version);
				if (dr.HasVersion(version))
				{
					if (version != DataRowVersion.Original)
					{
						dr.Table.RecordChanged(oldIndex, newIndex);
					}
					if (dc.dependentColumns != null)
					{
						dc.Table.EvaluateDependentExpressions(dc.dependentColumns, dr, version, null);
					}
				}
			}
			dr.ResetLastChangedColumn();
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0006E82C File Offset: 0x0006DC2C
		public void RejectChanges()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.RejectChanges|API> %d#\n", this.ObjectID);
			try
			{
				DataRow[] array = new DataRow[this.Rows.Count];
				this.Rows.CopyTo(array, 0);
				for (int i = 0; i < array.Length; i++)
				{
					this.RollbackRow(array[i]);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0006E8A8 File Offset: 0x0006DCA8
		internal void RemoveRow(DataRow row, bool check)
		{
			if (row.rowID == -1L)
			{
				throw ExceptionBuilder.RowAlreadyRemoved();
			}
			if (check && this.dataSet != null)
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.dataSet, this);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint().CheckCanRemoveParentRow(row);
				}
			}
			int num = row.oldRecord;
			int newRecord = row.newRecord;
			DataViewRowState recordState = row.GetRecordState(num);
			DataViewRowState recordState2 = row.GetRecordState(newRecord);
			row.oldRecord = -1;
			row.newRecord = -1;
			if (num == newRecord)
			{
				num = -1;
			}
			this.RecordStateChanged(num, recordState, DataViewRowState.None, newRecord, recordState2, DataViewRowState.None);
			this.FreeRecord(ref num);
			this.FreeRecord(ref newRecord);
			row.rowID = -1L;
			this.Rows.ArrayRemove(row);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0006E958 File Offset: 0x0006DD58
		public virtual void Reset()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.Reset|API> %d#\n", this.ObjectID);
			try
			{
				this.Clear();
				this.ResetConstraints();
				DataRelationCollection dataRelationCollection = this.ParentRelations;
				int i = dataRelationCollection.Count;
				while (i > 0)
				{
					i--;
					dataRelationCollection.RemoveAt(i);
				}
				dataRelationCollection = this.ChildRelations;
				i = dataRelationCollection.Count;
				while (i > 0)
				{
					i--;
					dataRelationCollection.RemoveAt(i);
				}
				this.Columns.Clear();
				this.indexes.Clear();
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0006EA00 File Offset: 0x0006DE00
		internal void ResetIndexes()
		{
			this.ResetInternalIndexes(null);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0006EA14 File Offset: 0x0006DE14
		internal void ResetInternalIndexes(DataColumn column)
		{
			this.SetShadowIndexes();
			try
			{
				int count = this.shadowIndexes.Count;
				for (int i = 0; i < count; i++)
				{
					Index index = this.shadowIndexes[i];
					if (0 < index.RefCount)
					{
						if (column == null)
						{
							index.Reset();
						}
						else
						{
							bool flag = false;
							foreach (IndexField indexField in index.IndexFields)
							{
								if (column == indexField.Column)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								index.Reset();
							}
						}
					}
				}
			}
			finally
			{
				this.RestoreShadowIndexes();
			}
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0006EAC4 File Offset: 0x0006DEC4
		internal void RollbackRow(DataRow row)
		{
			row.CancelEdit();
			this.SetNewRecord(row, row.oldRecord, DataRowAction.Rollback, false, true, false);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0006EAE8 File Offset: 0x0006DEE8
		private DataRowChangeEventArgs RaiseRowChanged(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			try
			{
				if (this.UpdatingCurrent(eRow, eAction) && (this.IsTypedDataTable || this.onRowChangedDelegate != null))
				{
					args = this.OnRowChanged(args, eRow, eAction);
				}
				else if (DataRowAction.Delete == eAction && eRow.newRecord == -1 && (this.IsTypedDataTable || this.onRowDeletedDelegate != null))
				{
					if (args == null)
					{
						args = new DataRowChangeEventArgs(eRow, eAction);
					}
					this.OnRowDeleted(args);
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
			}
			return args;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0006EB80 File Offset: 0x0006DF80
		private DataRowChangeEventArgs RaiseRowChanging(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction)
		{
			if (this.UpdatingCurrent(eRow, eAction) && (this.IsTypedDataTable || this.onRowChangingDelegate != null))
			{
				eRow.inChangingEvent = true;
				try
				{
					return this.OnRowChanging(args, eRow, eAction);
				}
				finally
				{
					eRow.inChangingEvent = false;
				}
			}
			if (DataRowAction.Delete == eAction && eRow.newRecord != -1 && (this.IsTypedDataTable || this.onRowDeletingDelegate != null))
			{
				eRow.inDeletingEvent = true;
				try
				{
					if (args == null)
					{
						args = new DataRowChangeEventArgs(eRow, eAction);
					}
					this.OnRowDeleting(args);
				}
				finally
				{
					eRow.inDeletingEvent = false;
				}
			}
			return args;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0006EC3C File Offset: 0x0006E03C
		private DataRowChangeEventArgs RaiseRowChanging(DataRowChangeEventArgs args, DataRow eRow, DataRowAction eAction, bool fireEvent)
		{
			if (this.EnforceConstraints && !this.inLoad)
			{
				int count = this.columnCollection.Count;
				for (int i = 0; i < count; i++)
				{
					DataColumn dataColumn = this.columnCollection[i];
					if (!dataColumn.Computed || eAction != DataRowAction.Add)
					{
						dataColumn.CheckColumnConstraint(eRow, eAction);
					}
				}
				int count2 = this.constraintCollection.Count;
				for (int j = 0; j < count2; j++)
				{
					this.constraintCollection[j].CheckConstraint(eRow, eAction);
				}
			}
			if (fireEvent)
			{
				args = this.RaiseRowChanging(args, eRow, eAction);
			}
			if (!this.inDataLoad && !this.MergingData && eAction != DataRowAction.Nothing && eAction != DataRowAction.ChangeOriginal)
			{
				this.CascadeAll(eRow, eAction);
			}
			return args;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0006ECF0 File Offset: 0x0006E0F0
		public DataRow[] Select()
		{
			Bid.Trace("<ds.DataTable.Select|API> %d#\n", this.ObjectID);
			return new Select(this, "", "", DataViewRowState.CurrentRows).SelectRows();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0006ED24 File Offset: 0x0006E124
		public DataRow[] Select(string filterExpression)
		{
			Bid.Trace("<ds.DataTable.Select|API> %d#, filterExpression='%ls'\n", this.ObjectID, filterExpression);
			return new Select(this, filterExpression, "", DataViewRowState.CurrentRows).SelectRows();
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0006ED58 File Offset: 0x0006E158
		public DataRow[] Select(string filterExpression, string sort)
		{
			Bid.Trace("<ds.DataTable.Select|API> %d#, filterExpression='%ls', sort='%ls'\n", this.ObjectID, filterExpression, sort);
			return new Select(this, filterExpression, sort, DataViewRowState.CurrentRows).SelectRows();
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0006ED88 File Offset: 0x0006E188
		public DataRow[] Select(string filterExpression, string sort, DataViewRowState recordStates)
		{
			Bid.Trace("<ds.DataTable.Select|API> %d#, filterExpression='%ls', sort='%ls', recordStates=%d{ds.DataViewRowState}\n", this.ObjectID, filterExpression, sort, (int)recordStates);
			return new Select(this, filterExpression, sort, recordStates).SelectRows();
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0006EDB8 File Offset: 0x0006E1B8
		internal void SetNewRecord(DataRow row, int proposedRecord, DataRowAction action = DataRowAction.Change, bool isInMerge = false, bool fireEvent = true, bool suppressEnsurePropertyChanged = false)
		{
			Exception ex = null;
			this.SetNewRecordWorker(row, proposedRecord, action, isInMerge, suppressEnsurePropertyChanged, -1, fireEvent, out ex);
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0006EDE0 File Offset: 0x0006E1E0
		private void SetNewRecordWorker(DataRow row, int proposedRecord, DataRowAction action, bool isInMerge, bool suppressEnsurePropertyChanged, int position, bool fireEvent, out Exception deferredException)
		{
			deferredException = null;
			if (row.tempRecord != proposedRecord)
			{
				if (!this.inDataLoad)
				{
					row.CheckInTable();
					this.CheckNotModifying(row);
				}
				if (proposedRecord == row.newRecord)
				{
					if (isInMerge)
					{
						this.RaiseRowChanged(null, row, action);
					}
					return;
				}
				row.tempRecord = proposedRecord;
			}
			DataRowChangeEventArgs args = null;
			try
			{
				row._action = action;
				args = this.RaiseRowChanging(null, row, action, fireEvent);
			}
			catch
			{
				row.tempRecord = -1;
				throw;
			}
			finally
			{
				row._action = DataRowAction.Nothing;
			}
			row.tempRecord = -1;
			int num = row.newRecord;
			int num2 = (proposedRecord != -1) ? proposedRecord : ((row.RowState != DataRowState.Unchanged) ? row.oldRecord : -1);
			if (action == DataRowAction.Add)
			{
				if (position == -1)
				{
					this.Rows.ArrayAdd(row);
				}
				else
				{
					this.Rows.ArrayInsert(row, position);
				}
			}
			List<DataRow> list = null;
			if ((action == DataRowAction.Delete || action == DataRowAction.Change) && this.dependentColumns != null && this.dependentColumns.Count > 0)
			{
				list = new List<DataRow>();
				for (int i = 0; i < this.ParentRelations.Count; i++)
				{
					DataRelation dataRelation = this.ParentRelations[i];
					if (dataRelation.ChildTable == row.Table)
					{
						list.InsertRange(list.Count, row.GetParentRows(dataRelation));
					}
				}
				for (int j = 0; j < this.ChildRelations.Count; j++)
				{
					DataRelation dataRelation2 = this.ChildRelations[j];
					if (dataRelation2.ParentTable == row.Table)
					{
						list.InsertRange(list.Count, row.GetChildRows(dataRelation2));
					}
				}
			}
			if (!suppressEnsurePropertyChanged && !row.HasPropertyChanged && row.newRecord != proposedRecord && -1 != proposedRecord && -1 != row.newRecord)
			{
				row.LastChangedColumn = null;
				row.LastChangedColumn = null;
			}
			if (this.LiveIndexes.Count != 0)
			{
				if (-1 == num && -1 != proposedRecord && -1 != row.oldRecord && proposedRecord != row.oldRecord)
				{
					num = row.oldRecord;
				}
				DataViewRowState recordState = row.GetRecordState(num);
				DataViewRowState recordState2 = row.GetRecordState(num2);
				row.newRecord = proposedRecord;
				if (proposedRecord != -1)
				{
					this.recordManager[proposedRecord] = row;
				}
				DataViewRowState recordState3 = row.GetRecordState(num);
				DataViewRowState recordState4 = row.GetRecordState(num2);
				this.RecordStateChanged(num, recordState, recordState3, num2, recordState2, recordState4);
			}
			else
			{
				row.newRecord = proposedRecord;
				if (proposedRecord != -1)
				{
					this.recordManager[proposedRecord] = row;
				}
			}
			row.ResetLastChangedColumn();
			if (-1 != num && num != row.oldRecord && num != row.tempRecord && num != row.newRecord && row == this.recordManager[num])
			{
				this.FreeRecord(ref num);
			}
			if (row.RowState == DataRowState.Detached && row.rowID != -1L)
			{
				this.RemoveRow(row, false);
			}
			if (this.dependentColumns != null && this.dependentColumns.Count > 0)
			{
				try
				{
					this.EvaluateExpressions(row, action, list);
				}
				catch (Exception ex)
				{
					if (action != DataRowAction.Add)
					{
						throw ex;
					}
					deferredException = ex;
				}
			}
			try
			{
				if (fireEvent)
				{
					this.RaiseRowChanged(args, row, action);
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ExceptionBuilder.TraceExceptionWithoutRethrow(e);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0006F148 File Offset: 0x0006E548
		internal void SetOldRecord(DataRow row, int proposedRecord)
		{
			if (!this.inDataLoad)
			{
				row.CheckInTable();
				this.CheckNotModifying(row);
			}
			if (proposedRecord == row.oldRecord)
			{
				return;
			}
			int num = row.oldRecord;
			try
			{
				if (this.LiveIndexes.Count != 0)
				{
					if (-1 == num && -1 != proposedRecord && -1 != row.newRecord && proposedRecord != row.newRecord)
					{
						num = row.newRecord;
					}
					DataViewRowState recordState = row.GetRecordState(num);
					DataViewRowState recordState2 = row.GetRecordState(proposedRecord);
					row.oldRecord = proposedRecord;
					if (proposedRecord != -1)
					{
						this.recordManager[proposedRecord] = row;
					}
					DataViewRowState recordState3 = row.GetRecordState(num);
					DataViewRowState recordState4 = row.GetRecordState(proposedRecord);
					this.RecordStateChanged(num, recordState, recordState3, proposedRecord, recordState2, recordState4);
				}
				else
				{
					row.oldRecord = proposedRecord;
					if (proposedRecord != -1)
					{
						this.recordManager[proposedRecord] = row;
					}
				}
			}
			finally
			{
				if (num != -1 && num != row.tempRecord && num != row.oldRecord && num != row.newRecord)
				{
					this.FreeRecord(ref num);
				}
				if (row.RowState == DataRowState.Detached && row.rowID != -1L)
				{
					this.RemoveRow(row, false);
				}
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0006F26C File Offset: 0x0006E66C
		private void RestoreShadowIndexes()
		{
			this.shadowCount--;
			if (this.shadowCount == 0)
			{
				this.shadowIndexes = null;
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0006F298 File Offset: 0x0006E698
		private void SetShadowIndexes()
		{
			if (this.shadowIndexes == null)
			{
				this.shadowIndexes = this.LiveIndexes;
				this.shadowCount = 1;
				return;
			}
			this.shadowCount++;
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0006F2D0 File Offset: 0x0006E6D0
		internal void ShadowIndexCopy()
		{
			if (this.shadowIndexes == this.indexes)
			{
				this.shadowIndexes = new List<Index>(this.indexes);
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0006F2FC File Offset: 0x0006E6FC
		public override string ToString()
		{
			if (this.displayExpression == null)
			{
				return this.TableName;
			}
			return this.TableName + " + " + this.DisplayExpressionInternal;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0006F330 File Offset: 0x0006E730
		public void BeginLoadData()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.BeginLoadData|API> %d#\n", this.ObjectID);
			try
			{
				if (!this.inDataLoad)
				{
					this.inDataLoad = true;
					this.loadIndex = null;
					this.initialLoad = (this.Rows.Count == 0);
					if (this.initialLoad)
					{
						this.SuspendIndexEvents();
					}
					else
					{
						if (this.primaryKey != null)
						{
							this.loadIndex = this.primaryKey.Key.GetSortIndex(DataViewRowState.OriginalRows);
						}
						if (this.loadIndex != null)
						{
							this.loadIndex.AddRef();
						}
					}
					if (this.DataSet != null)
					{
						this.savedEnforceConstraints = this.DataSet.EnforceConstraints;
						this.DataSet.EnforceConstraints = false;
					}
					else
					{
						this.EnforceConstraints = false;
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0006F418 File Offset: 0x0006E818
		public void EndLoadData()
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.EndLoadData|API> %d#\n", this.ObjectID);
			try
			{
				if (this.inDataLoad)
				{
					if (this.loadIndex != null)
					{
						this.loadIndex.RemoveRef();
					}
					if (this.loadIndexwithOriginalAdded != null)
					{
						this.loadIndexwithOriginalAdded.RemoveRef();
					}
					if (this.loadIndexwithCurrentDeleted != null)
					{
						this.loadIndexwithCurrentDeleted.RemoveRef();
					}
					this.loadIndex = null;
					this.loadIndexwithOriginalAdded = null;
					this.loadIndexwithCurrentDeleted = null;
					this.inDataLoad = false;
					this.RestoreIndexEvents(false);
					if (this.DataSet != null)
					{
						this.DataSet.EnforceConstraints = this.savedEnforceConstraints;
					}
					else
					{
						this.EnforceConstraints = true;
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0006F4EC File Offset: 0x0006E8EC
		public DataRow LoadDataRow(object[] values, bool fAcceptChanges)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.LoadDataRow|API> %d#, fAcceptChanges=%d{bool}\n", this.ObjectID, fAcceptChanges);
			DataRow result;
			try
			{
				if (this.inDataLoad)
				{
					int num = this.NewRecordFromArray(values);
					DataRow dataRow;
					if (this.loadIndex != null)
					{
						int num2 = this.loadIndex.FindRecord(num);
						if (num2 != -1)
						{
							int record = this.loadIndex.GetRecord(num2);
							dataRow = this.recordManager[record];
							dataRow.CancelEdit();
							if (dataRow.RowState == DataRowState.Deleted)
							{
								this.SetNewRecord(dataRow, dataRow.oldRecord, DataRowAction.Rollback, false, true, false);
							}
							this.SetNewRecord(dataRow, num, DataRowAction.Change, false, true, false);
							if (fAcceptChanges)
							{
								dataRow.AcceptChanges();
							}
							return dataRow;
						}
					}
					dataRow = this.NewRow(num);
					this.AddRow(dataRow);
					if (fAcceptChanges)
					{
						dataRow.AcceptChanges();
					}
					result = dataRow;
				}
				else
				{
					DataRow dataRow = this.UpdatingAdd(values);
					if (fAcceptChanges)
					{
						dataRow.AcceptChanges();
					}
					result = dataRow;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0006F5E4 File Offset: 0x0006E9E4
		public DataRow LoadDataRow(object[] values, LoadOption loadOption)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.LoadDataRow|API> %d#, loadOption=%d{ds.LoadOption}\n", this.ObjectID, (int)loadOption);
			DataRow result;
			try
			{
				Index searchIndex = null;
				if (this.primaryKey != null)
				{
					if (loadOption == LoadOption.Upsert)
					{
						if (this.loadIndexwithCurrentDeleted == null)
						{
							this.loadIndexwithCurrentDeleted = this.primaryKey.Key.GetSortIndex(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedCurrent);
							if (this.loadIndexwithCurrentDeleted != null)
							{
								this.loadIndexwithCurrentDeleted.AddRef();
							}
						}
						searchIndex = this.loadIndexwithCurrentDeleted;
					}
					else
					{
						if (this.loadIndexwithOriginalAdded == null)
						{
							this.loadIndexwithOriginalAdded = this.primaryKey.Key.GetSortIndex(DataViewRowState.Unchanged | DataViewRowState.Added | DataViewRowState.Deleted | DataViewRowState.ModifiedOriginal);
							if (this.loadIndexwithOriginalAdded != null)
							{
								this.loadIndexwithOriginalAdded.AddRef();
							}
						}
						searchIndex = this.loadIndexwithOriginalAdded;
					}
				}
				if (this.inDataLoad && !this.AreIndexEventsSuspended)
				{
					this.SuspendIndexEvents();
				}
				DataRow dataRow = this.LoadRow(values, loadOption, searchIndex);
				result = dataRow;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0006F6DC File Offset: 0x0006EADC
		internal DataRow UpdatingAdd(object[] values)
		{
			Index index = null;
			if (this.primaryKey != null)
			{
				index = this.primaryKey.Key.GetSortIndex(DataViewRowState.OriginalRows);
			}
			if (index == null)
			{
				return this.Rows.Add(values);
			}
			int num = this.NewRecordFromArray(values);
			int num2 = index.FindRecord(num);
			if (num2 != -1)
			{
				int record = index.GetRecord(num2);
				DataRow dataRow = this.recordManager[record];
				dataRow.RejectChanges();
				this.SetNewRecord(dataRow, num, DataRowAction.Change, false, true, false);
				return dataRow;
			}
			DataRow dataRow2 = this.NewRow(num);
			this.Rows.Add(dataRow2);
			return dataRow2;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0006F774 File Offset: 0x0006EB74
		internal bool UpdatingCurrent(DataRow row, DataRowAction action)
		{
			return action == DataRowAction.Add || action == DataRowAction.Change || action == DataRowAction.Rollback || action == DataRowAction.ChangeOriginal || action == DataRowAction.ChangeCurrentAndOriginal;
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0006F79C File Offset: 0x0006EB9C
		internal DataColumn AddUniqueKey(int position)
		{
			if (this._colUnique != null)
			{
				return this._colUnique;
			}
			DataColumn[] array = this.PrimaryKey;
			if (array.Length == 1)
			{
				return array[0];
			}
			string columnName = XMLSchema.GenUniqueColumnName(this.TableName + "_Id", this);
			DataColumn dataColumn = new DataColumn(columnName, typeof(int), null, MappingType.Hidden);
			dataColumn.Prefix = this.tablePrefix;
			dataColumn.AutoIncrement = true;
			dataColumn.AllowDBNull = false;
			dataColumn.Unique = true;
			if (position == -1)
			{
				this.Columns.Add(dataColumn);
			}
			else
			{
				for (int i = this.Columns.Count - 1; i >= position; i--)
				{
					this.Columns[i].SetOrdinalInternal(i + 1);
				}
				this.Columns.AddAt(position, dataColumn);
				dataColumn.SetOrdinalInternal(position);
			}
			if (array.Length == 0)
			{
				this.PrimaryKey = new DataColumn[]
				{
					dataColumn
				};
			}
			this._colUnique = dataColumn;
			return this._colUnique;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0006F888 File Offset: 0x0006EC88
		internal DataColumn AddUniqueKey()
		{
			return this.AddUniqueKey(-1);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0006F89C File Offset: 0x0006EC9C
		internal DataColumn AddForeignKey(DataColumn parentKey)
		{
			string columnName = XMLSchema.GenUniqueColumnName(parentKey.ColumnName, this);
			DataColumn dataColumn = new DataColumn(columnName, parentKey.DataType, null, MappingType.Hidden);
			this.Columns.Add(dataColumn);
			return dataColumn;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0006F8D4 File Offset: 0x0006ECD4
		internal void UpdatePropertyDescriptorCollectionCache()
		{
			this.propertyDescriptorCollectionCache = null;
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0006F8E8 File Offset: 0x0006ECE8
		internal PropertyDescriptorCollection GetPropertyDescriptorCollection(Attribute[] attributes)
		{
			if (this.propertyDescriptorCollectionCache == null)
			{
				int count = this.Columns.Count;
				int count2 = this.ChildRelations.Count;
				PropertyDescriptor[] array = new PropertyDescriptor[count + count2];
				for (int i = 0; i < count; i++)
				{
					array[i] = new DataColumnPropertyDescriptor(this.Columns[i]);
				}
				for (int j = 0; j < count2; j++)
				{
					array[count + j] = new DataRelationPropertyDescriptor(this.ChildRelations[j]);
				}
				this.propertyDescriptorCollectionCache = new PropertyDescriptorCollection(array);
			}
			return this.propertyDescriptorCollectionCache;
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0006F978 File Offset: 0x0006ED78
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x0006F9A0 File Offset: 0x0006EDA0
		internal XmlQualifiedName TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return (XmlQualifiedName)this.typeName;
				}
				return XmlQualifiedName.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0006F9B4 File Offset: 0x0006EDB4
		public void Merge(DataTable table)
		{
			this.Merge(table, false, MissingSchemaAction.Add);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0006F9CC File Offset: 0x0006EDCC
		public void Merge(DataTable table, bool preserveChanges)
		{
			this.Merge(table, preserveChanges, MissingSchemaAction.Add);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0006F9E4 File Offset: 0x0006EDE4
		public void Merge(DataTable table, bool preserveChanges, MissingSchemaAction missingSchemaAction)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.Merge|API> %d#, table=%d, preserveChanges=%d{bool}, missingSchemaAction=%d{ds.MissingSchemaAction}\n", this.ObjectID, (table != null) ? table.ObjectID : 0, preserveChanges, (int)missingSchemaAction);
			try
			{
				if (table == null)
				{
					throw ExceptionBuilder.ArgumentNull("table");
				}
				if (missingSchemaAction - MissingSchemaAction.Add > 3)
				{
					throw ADP.InvalidMissingSchemaAction(missingSchemaAction);
				}
				Merger merger = new Merger(this, preserveChanges, missingSchemaAction);
				merger.MergeTable(table);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0006FA64 File Offset: 0x0006EE64
		public void Load(IDataReader reader)
		{
			this.Load(reader, LoadOption.PreserveChanges, null);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0006FA7C File Offset: 0x0006EE7C
		public void Load(IDataReader reader, LoadOption loadOption)
		{
			this.Load(reader, loadOption, null);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0006FA94 File Offset: 0x0006EE94
		public virtual void Load(IDataReader reader, LoadOption loadOption, FillErrorEventHandler errorHandler)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.Load|API> %d#, loadOption=%d{ds.LoadOption}\n", this.ObjectID, (int)loadOption);
			try
			{
				if (this.PrimaryKey.Length == 0)
				{
					DataTableReader dataTableReader = reader as DataTableReader;
					if (dataTableReader != null && dataTableReader.CurrentDataTable == this)
					{
						return;
					}
				}
				LoadAdapter loadAdapter = new LoadAdapter();
				loadAdapter.FillLoadOption = loadOption;
				loadAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
				if (errorHandler != null)
				{
					loadAdapter.FillError += errorHandler;
				}
				loadAdapter.FillFromReader(new DataTable[]
				{
					this
				}, reader, 0, 0);
				if (!reader.IsClosed && !reader.NextResult())
				{
					reader.Close();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0006FB40 File Offset: 0x0006EF40
		private DataRow LoadRow(object[] values, LoadOption loadOption, Index searchIndex)
		{
			DataRow dataRow = null;
			int num2;
			if (searchIndex != null)
			{
				int[] array = new int[0];
				if (this.primaryKey != null)
				{
					array = new int[this.primaryKey.ColumnsReference.Length];
					for (int i = 0; i < this.primaryKey.ColumnsReference.Length; i++)
					{
						array[i] = this.primaryKey.ColumnsReference[i].Ordinal;
					}
				}
				object[] array2 = new object[array.Length];
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = values[array[j]];
				}
				Range range = searchIndex.FindRecords(array2);
				if (!range.IsNull)
				{
					int num = 0;
					for (int k = range.Min; k <= range.Max; k++)
					{
						int record = searchIndex.GetRecord(k);
						dataRow = this.recordManager[record];
						num2 = this.NewRecordFromArray(values);
						for (int l = 0; l < values.Length; l++)
						{
							if (values[l] == null)
							{
								this.columnCollection[l].Copy(record, num2);
							}
						}
						for (int m = values.Length; m < this.columnCollection.Count; m++)
						{
							this.columnCollection[m].Copy(record, num2);
						}
						if (loadOption != LoadOption.Upsert || dataRow.RowState != DataRowState.Deleted)
						{
							this.SetDataRowWithLoadOption(dataRow, num2, loadOption, true);
						}
						else
						{
							num++;
						}
					}
					if (num == 0)
					{
						return dataRow;
					}
				}
			}
			num2 = this.NewRecordFromArray(values);
			dataRow = this.NewRow(num2);
			DataRowAction eAction;
			if (loadOption - LoadOption.OverwriteChanges > 1)
			{
				if (loadOption != LoadOption.Upsert)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
				}
				eAction = DataRowAction.Add;
			}
			else
			{
				eAction = DataRowAction.ChangeCurrentAndOriginal;
			}
			DataRowChangeEventArgs args = this.RaiseRowChanging(null, dataRow, eAction);
			this.InsertRow(dataRow, -1L, -1, false);
			if (loadOption - LoadOption.OverwriteChanges > 1)
			{
				if (loadOption != LoadOption.Upsert)
				{
					throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
				}
			}
			else
			{
				this.SetOldRecord(dataRow, num2);
			}
			this.RaiseRowChanged(args, dataRow, eAction);
			return dataRow;
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0006FD24 File Offset: 0x0006F124
		private void SetDataRowWithLoadOption(DataRow dataRow, int recordNo, LoadOption loadOption, bool checkReadOnly)
		{
			bool flag = false;
			if (checkReadOnly)
			{
				foreach (object obj in this.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.ReadOnly && !dataColumn.Computed)
					{
						switch (loadOption)
						{
						case LoadOption.OverwriteChanges:
							if (dataRow[dataColumn, DataRowVersion.Current] != dataColumn[recordNo] || dataRow[dataColumn, DataRowVersion.Original] != dataColumn[recordNo])
							{
								flag = true;
							}
							break;
						case LoadOption.PreserveChanges:
							if (dataRow[dataColumn, DataRowVersion.Original] != dataColumn[recordNo])
							{
								flag = true;
							}
							break;
						case LoadOption.Upsert:
							if (dataRow[dataColumn, DataRowVersion.Current] != dataColumn[recordNo])
							{
								flag = true;
							}
							break;
						}
					}
				}
			}
			DataRowChangeEventArgs args = null;
			DataRowAction dataRowAction = DataRowAction.Nothing;
			int tempRecord = dataRow.tempRecord;
			dataRow.tempRecord = recordNo;
			switch (loadOption)
			{
			case LoadOption.OverwriteChanges:
				dataRowAction = DataRowAction.ChangeCurrentAndOriginal;
				break;
			case LoadOption.PreserveChanges:
			{
				DataRowState rowState = dataRow.RowState;
				if (rowState == DataRowState.Unchanged)
				{
					dataRowAction = DataRowAction.ChangeCurrentAndOriginal;
				}
				else
				{
					dataRowAction = DataRowAction.ChangeOriginal;
				}
				break;
			}
			case LoadOption.Upsert:
			{
				DataRowState rowState2 = dataRow.RowState;
				if (rowState2 != DataRowState.Unchanged)
				{
					if (rowState2 == DataRowState.Deleted)
					{
						break;
					}
				}
				else
				{
					using (IEnumerator enumerator2 = dataRow.Table.Columns.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							object obj2 = enumerator2.Current;
							DataColumn dataColumn2 = (DataColumn)obj2;
							if (dataColumn2.Compare(dataRow.newRecord, recordNo) != 0)
							{
								dataRowAction = DataRowAction.Change;
								break;
							}
						}
						break;
					}
				}
				dataRowAction = DataRowAction.Change;
				break;
			}
			default:
				throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
			}
			try
			{
				args = this.RaiseRowChanging(null, dataRow, dataRowAction);
				if (dataRowAction == DataRowAction.Nothing)
				{
					dataRow.inChangingEvent = true;
					try
					{
						args = this.OnRowChanging(args, dataRow, dataRowAction);
					}
					finally
					{
						dataRow.inChangingEvent = false;
					}
				}
			}
			finally
			{
				if (DataRowState.Detached == dataRow.RowState)
				{
					if (-1 != tempRecord)
					{
						this.FreeRecord(ref tempRecord);
					}
				}
				else if (dataRow.tempRecord != recordNo)
				{
					if (-1 != tempRecord)
					{
						this.FreeRecord(ref tempRecord);
					}
					if (-1 != recordNo)
					{
						this.FreeRecord(ref recordNo);
					}
					recordNo = dataRow.tempRecord;
				}
				else
				{
					dataRow.tempRecord = tempRecord;
				}
			}
			if (dataRow.tempRecord != -1)
			{
				dataRow.CancelEdit();
			}
			switch (loadOption)
			{
			case LoadOption.OverwriteChanges:
				this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
				this.SetOldRecord(dataRow, recordNo);
				break;
			case LoadOption.PreserveChanges:
				if (dataRow.RowState == DataRowState.Unchanged)
				{
					this.SetOldRecord(dataRow, recordNo);
					this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
				}
				else
				{
					this.SetOldRecord(dataRow, recordNo);
				}
				break;
			case LoadOption.Upsert:
				if (dataRow.RowState == DataRowState.Unchanged)
				{
					this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
					if (!dataRow.HasChanges())
					{
						this.SetOldRecord(dataRow, recordNo);
					}
				}
				else
				{
					if (dataRow.RowState == DataRowState.Deleted)
					{
						dataRow.RejectChanges();
					}
					this.SetNewRecord(dataRow, recordNo, DataRowAction.Change, false, false, false);
				}
				break;
			default:
				throw ExceptionBuilder.ArgumentOutOfRange("LoadOption");
			}
			if (flag)
			{
				string @string = Res.GetString("Load_ReadOnlyDataModified");
				if (dataRow.RowError.Length == 0)
				{
					dataRow.RowError = @string;
				}
				else
				{
					dataRow.RowError = dataRow.RowError + " ]:[ " + @string;
				}
				foreach (object obj3 in this.Columns)
				{
					DataColumn dataColumn3 = (DataColumn)obj3;
					if (dataColumn3.ReadOnly && !dataColumn3.Computed)
					{
						dataRow.SetColumnError(dataColumn3, @string);
					}
				}
			}
			args = this.RaiseRowChanged(args, dataRow, dataRowAction);
			if (dataRowAction == DataRowAction.Nothing)
			{
				dataRow.inChangingEvent = true;
				try
				{
					this.OnRowChanged(args, dataRow, dataRowAction);
				}
				finally
				{
					dataRow.inChangingEvent = false;
				}
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00070150 File Offset: 0x0006F550
		public DataTableReader CreateDataReader()
		{
			return new DataTableReader(this);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00070164 File Offset: 0x0006F564
		public void WriteXml(Stream stream)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema, false);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0007017C File Offset: 0x0006F57C
		public void WriteXml(Stream stream, bool writeHierarchy)
		{
			this.WriteXml(stream, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00070194 File Offset: 0x0006F594
		public void WriteXml(TextWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000701AC File Offset: 0x0006F5AC
		public void WriteXml(TextWriter writer, bool writeHierarchy)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000701C4 File Offset: 0x0006F5C4
		public void WriteXml(XmlWriter writer)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, false);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000701DC File Offset: 0x0006F5DC
		public void WriteXml(XmlWriter writer, bool writeHierarchy)
		{
			this.WriteXml(writer, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x000701F4 File Offset: 0x0006F5F4
		public void WriteXml(string fileName)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema, false);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0007020C File Offset: 0x0006F60C
		public void WriteXml(string fileName, bool writeHierarchy)
		{
			this.WriteXml(fileName, XmlWriteMode.IgnoreSchema, writeHierarchy);
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x00070224 File Offset: 0x0006F624
		public void WriteXml(Stream stream, XmlWriteMode mode)
		{
			this.WriteXml(stream, mode, false);
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0007023C File Offset: 0x0006F63C
		public void WriteXml(Stream stream, XmlWriteMode mode, bool writeHierarchy)
		{
			if (stream != null)
			{
				this.WriteXml(new XmlTextWriter(stream, null)
				{
					Formatting = Formatting.Indented
				}, mode, writeHierarchy);
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x00070264 File Offset: 0x0006F664
		public void WriteXml(TextWriter writer, XmlWriteMode mode)
		{
			this.WriteXml(writer, mode, false);
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0007027C File Offset: 0x0006F67C
		public void WriteXml(TextWriter writer, XmlWriteMode mode, bool writeHierarchy)
		{
			if (writer != null)
			{
				this.WriteXml(new XmlTextWriter(writer)
				{
					Formatting = Formatting.Indented
				}, mode, writeHierarchy);
			}
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x000702A4 File Offset: 0x0006F6A4
		public void WriteXml(XmlWriter writer, XmlWriteMode mode)
		{
			this.WriteXml(writer, mode, false);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000702BC File Offset: 0x0006F6BC
		public void WriteXml(XmlWriter writer, XmlWriteMode mode, bool writeHierarchy)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.WriteXml|API> %d#, mode=%d{ds.XmlWriteMode}\n", this.ObjectID, (int)mode);
			try
			{
				if (this.tableName.Length == 0)
				{
					throw ExceptionBuilder.CanNotSerializeDataTableWithEmptyName();
				}
				if (writer != null)
				{
					if (mode == XmlWriteMode.DiffGram)
					{
						new NewDiffgramGen(this, writeHierarchy).Save(writer, this);
					}
					else if (mode == XmlWriteMode.WriteSchema)
					{
						DataSet dataSet = null;
						string text = this.tableNamespace;
						if (this.DataSet == null)
						{
							dataSet = new DataSet();
							dataSet.SetLocaleValue(this._culture, this._cultureUserSet);
							dataSet.CaseSensitive = this.CaseSensitive;
							dataSet.Namespace = this.Namespace;
							dataSet.RemotingFormat = this.RemotingFormat;
							dataSet.Tables.Add(this);
						}
						if (writer != null)
						{
							XmlDataTreeWriter xmlDataTreeWriter = new XmlDataTreeWriter(this, writeHierarchy);
							xmlDataTreeWriter.Save(writer, true);
						}
						if (dataSet != null)
						{
							dataSet.Tables.Remove(this);
							this.tableNamespace = text;
						}
					}
					else
					{
						XmlDataTreeWriter xmlDataTreeWriter2 = new XmlDataTreeWriter(this, writeHierarchy);
						xmlDataTreeWriter2.Save(writer, false);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000703CC File Offset: 0x0006F7CC
		public void WriteXml(string fileName, XmlWriteMode mode)
		{
			this.WriteXml(fileName, mode, false);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x000703E4 File Offset: 0x0006F7E4
		public void WriteXml(string fileName, XmlWriteMode mode, bool writeHierarchy)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.WriteXml|API> %d#, fileName='%ls', mode=%d{ds.XmlWriteMode}\n", this.ObjectID, fileName, (int)mode);
			try
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlTextWriter.WriteStartDocument(true);
					this.WriteXml(xmlTextWriter, mode, writeHierarchy);
					xmlTextWriter.WriteEndDocument();
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00070474 File Offset: 0x0006F874
		public void WriteXmlSchema(Stream stream)
		{
			this.WriteXmlSchema(stream, false);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0007048C File Offset: 0x0006F88C
		public void WriteXmlSchema(Stream stream, bool writeHierarchy)
		{
			if (stream == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(stream, null)
			{
				Formatting = Formatting.Indented
			}, writeHierarchy);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000704B4 File Offset: 0x0006F8B4
		public void WriteXmlSchema(TextWriter writer)
		{
			this.WriteXmlSchema(writer, false);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000704CC File Offset: 0x0006F8CC
		public void WriteXmlSchema(TextWriter writer, bool writeHierarchy)
		{
			if (writer == null)
			{
				return;
			}
			this.WriteXmlSchema(new XmlTextWriter(writer)
			{
				Formatting = Formatting.Indented
			}, writeHierarchy);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000704F4 File Offset: 0x0006F8F4
		private bool CheckForClosureOnExpressions(DataTable dt, bool writeHierarchy)
		{
			List<DataTable> list = new List<DataTable>();
			list.Add(dt);
			if (writeHierarchy)
			{
				this.CreateTableList(dt, list);
			}
			return this.CheckForClosureOnExpressionTables(list);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00070520 File Offset: 0x0006F920
		private bool CheckForClosureOnExpressionTables(List<DataTable> tableList)
		{
			foreach (DataTable dataTable in tableList)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (dataColumn.Expression.Length != 0)
					{
						DataColumn[] dependency = dataColumn.DataExpression.GetDependency();
						for (int i = 0; i < dependency.Length; i++)
						{
							if (!tableList.Contains(dependency[i].Table))
							{
								return false;
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0007060C File Offset: 0x0006FA0C
		public void WriteXmlSchema(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, false);
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00070624 File Offset: 0x0006FA24
		public void WriteXmlSchema(XmlWriter writer, bool writeHierarchy)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.WriteXmlSchema|API> %d#\n", this.ObjectID);
			try
			{
				if (this.tableName.Length == 0)
				{
					throw ExceptionBuilder.CanNotSerializeDataTableWithEmptyName();
				}
				if (!this.CheckForClosureOnExpressions(this, writeHierarchy))
				{
					throw ExceptionBuilder.CanNotSerializeDataTableHierarchy();
				}
				DataSet dataSet = null;
				string text = this.tableNamespace;
				if (this.DataSet == null)
				{
					dataSet = new DataSet();
					dataSet.SetLocaleValue(this._culture, this._cultureUserSet);
					dataSet.CaseSensitive = this.CaseSensitive;
					dataSet.Namespace = this.Namespace;
					dataSet.RemotingFormat = this.RemotingFormat;
					dataSet.Tables.Add(this);
				}
				if (writer != null)
				{
					XmlTreeGen xmlTreeGen = new XmlTreeGen(SchemaFormat.Public);
					xmlTreeGen.Save(null, this, writer, writeHierarchy);
				}
				if (dataSet != null)
				{
					dataSet.Tables.Remove(this);
					this.tableNamespace = text;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00070710 File Offset: 0x0006FB10
		public void WriteXmlSchema(string fileName)
		{
			this.WriteXmlSchema(fileName, false);
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00070728 File Offset: 0x0006FB28
		public void WriteXmlSchema(string fileName, bool writeHierarchy)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(fileName, null);
			try
			{
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.WriteStartDocument(true);
				this.WriteXmlSchema(xmlTextWriter, writeHierarchy);
				xmlTextWriter.WriteEndDocument();
			}
			finally
			{
				xmlTextWriter.Close();
			}
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00070780 File Offset: 0x0006FB80
		public XmlReadMode ReadXml(Stream stream)
		{
			if (stream == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(stream)
			{
				XmlResolver = null
			}, false);
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x000707A8 File Offset: 0x0006FBA8
		public XmlReadMode ReadXml(TextReader reader)
		{
			if (reader == null)
			{
				return XmlReadMode.Auto;
			}
			return this.ReadXml(new XmlTextReader(reader)
			{
				XmlResolver = null
			}, false);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x000707D0 File Offset: 0x0006FBD0
		public XmlReadMode ReadXml(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			xmlTextReader.XmlResolver = null;
			XmlReadMode result;
			try
			{
				result = this.ReadXml(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
			return result;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0007081C File Offset: 0x0006FC1C
		public XmlReadMode ReadXml(XmlReader reader)
		{
			return this.ReadXml(reader, false);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00070834 File Offset: 0x0006FC34
		private void RestoreConstraint(bool originalEnforceConstraint)
		{
			if (this.DataSet != null)
			{
				this.DataSet.EnforceConstraints = originalEnforceConstraint;
				return;
			}
			this.EnforceConstraints = originalEnforceConstraint;
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x00070860 File Offset: 0x0006FC60
		private bool IsEmptyXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				if (reader.AttributeCount == 0 || (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					return true;
				}
				if (reader.AttributeCount == 1)
				{
					reader.MoveToAttribute(0);
					if (this.Namespace == reader.Value && this.Prefix == reader.LocalName && reader.Prefix == "xmlns" && reader.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x00070904 File Offset: 0x0006FD04
		internal XmlReadMode ReadXml(XmlReader reader, bool denyResolving)
		{
			IDisposable disposable = null;
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.ReadXml|INFO> %d#, denyResolving=%d{bool}\n", this.ObjectID, denyResolving);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				DataTable.RowDiffIdUsageSection rowDiffIdUsageSection = default(DataTable.RowDiffIdUsageSection);
				try
				{
					bool flag = false;
					bool flag2 = false;
					bool isXdr = false;
					XmlReadMode xmlReadMode = XmlReadMode.Auto;
					rowDiffIdUsageSection.Prepare(this);
					if (reader == null)
					{
						result = xmlReadMode;
					}
					else
					{
						bool flag3;
						if (this.DataSet != null)
						{
							flag3 = this.DataSet.EnforceConstraints;
							this.DataSet.EnforceConstraints = false;
						}
						else
						{
							flag3 = this.EnforceConstraints;
							this.EnforceConstraints = false;
						}
						if (reader is XmlTextReader)
						{
							((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
						}
						XmlDocument xmlDocument = new XmlDocument();
						XmlDataLoader xmlDataLoader = null;
						reader.MoveToContent();
						if (this.Columns.Count == 0 && this.IsEmptyXml(reader))
						{
							reader.Read();
							result = xmlReadMode;
						}
						else
						{
							if (reader.NodeType == XmlNodeType.Element)
							{
								int depth = reader.Depth;
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (this.Columns.Count != 0)
									{
										this.ReadXmlDiffgram(reader);
										this.ReadEndElement(reader);
										this.RestoreConstraint(flag3);
										return XmlReadMode.DiffGram;
									}
									if (reader.IsEmptyElement)
									{
										reader.Read();
										return XmlReadMode.DiffGram;
									}
									throw ExceptionBuilder.DataTableInferenceNotSupported();
								}
								else
								{
									if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
									{
										this.ReadXDRSchema(reader);
										this.RestoreConstraint(flag3);
										return XmlReadMode.ReadSchema;
									}
									if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
									{
										this.ReadXmlSchema(reader, denyResolving);
										this.RestoreConstraint(flag3);
										return XmlReadMode.ReadSchema;
									}
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										if (this.DataSet != null)
										{
											this.DataSet.RestoreEnforceConstraints(flag3);
										}
										else
										{
											this.enforceConstraints = flag3;
										}
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									XmlElement xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
									if (reader.HasAttributes)
									{
										int attributeCount = reader.AttributeCount;
										for (int i = 0; i < attributeCount; i++)
										{
											reader.MoveToAttribute(i);
											if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
											{
												xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
											}
											else
											{
												XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
												xmlAttribute.Prefix = reader.Prefix;
												xmlAttribute.Value = reader.GetAttribute(i);
											}
										}
									}
									reader.Read();
									while (this.MoveToElement(reader, depth))
									{
										if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
										{
											this.ReadXmlDiffgram(reader);
											this.ReadEndElement(reader);
											this.RestoreConstraint(flag3);
											return XmlReadMode.DiffGram;
										}
										if (!flag2 && !flag && reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
										{
											this.ReadXDRSchema(reader);
											flag2 = true;
											isXdr = true;
										}
										else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
										{
											this.ReadXmlSchema(reader, denyResolving);
											flag2 = true;
										}
										else
										{
											if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
											{
												if (this.DataSet != null)
												{
													this.DataSet.RestoreEnforceConstraints(flag3);
												}
												else
												{
													this.enforceConstraints = flag3;
												}
												throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
											}
											if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
											{
												this.ReadXmlDiffgram(reader);
												xmlReadMode = XmlReadMode.DiffGram;
											}
											else
											{
												flag = true;
												if (!flag2 && this.Columns.Count == 0)
												{
													XmlNode newChild = xmlDocument.ReadNode(reader);
													xmlElement.AppendChild(newChild);
												}
												else
												{
													if (xmlDataLoader == null)
													{
														xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, false);
													}
													xmlDataLoader.LoadData(reader);
													if (flag2)
													{
														xmlReadMode = XmlReadMode.ReadSchema;
													}
													else
													{
														xmlReadMode = XmlReadMode.IgnoreSchema;
													}
												}
											}
										}
									}
									this.ReadEndElement(reader);
									xmlDocument.AppendChild(xmlElement);
									if (!flag2 && this.Columns.Count == 0)
									{
										if (this.IsEmptyXml(reader))
										{
											reader.Read();
											return xmlReadMode;
										}
										throw ExceptionBuilder.DataTableInferenceNotSupported();
									}
									else if (xmlDataLoader == null)
									{
										xmlDataLoader = new XmlDataLoader(this, isXdr, false);
									}
								}
							}
							this.RestoreConstraint(flag3);
							result = xmlReadMode;
						}
					}
				}
				finally
				{
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x00070DE8 File Offset: 0x000701E8
		internal XmlReadMode ReadXml(XmlReader reader, XmlReadMode mode, bool denyResolving)
		{
			IDisposable disposable = null;
			DataTable.RowDiffIdUsageSection rowDiffIdUsageSection = default(DataTable.RowDiffIdUsageSection);
			XmlReadMode result;
			try
			{
				disposable = TypeLimiter.EnterRestrictedScope(this);
				bool flag = false;
				bool flag2 = false;
				bool isXdr = false;
				int depth = -1;
				XmlReadMode xmlReadMode = mode;
				rowDiffIdUsageSection.Prepare(this);
				if (reader == null)
				{
					result = xmlReadMode;
				}
				else
				{
					bool flag3;
					if (this.DataSet != null)
					{
						flag3 = this.DataSet.EnforceConstraints;
						this.DataSet.EnforceConstraints = false;
					}
					else
					{
						flag3 = this.EnforceConstraints;
						this.EnforceConstraints = false;
					}
					if (reader is XmlTextReader)
					{
						((XmlTextReader)reader).WhitespaceHandling = WhitespaceHandling.Significant;
					}
					XmlDocument xmlDocument = new XmlDocument();
					if (mode != XmlReadMode.Fragment && reader.NodeType == XmlNodeType.Element)
					{
						depth = reader.Depth;
					}
					reader.MoveToContent();
					if (this.Columns.Count == 0 && this.IsEmptyXml(reader))
					{
						reader.Read();
						result = xmlReadMode;
					}
					else
					{
						XmlDataLoader xmlDataLoader = null;
						if (reader.NodeType == XmlNodeType.Element)
						{
							XmlElement xmlElement;
							if (mode == XmlReadMode.Fragment)
							{
								xmlDocument.AppendChild(xmlDocument.CreateElement("ds_sqlXmlWraPPeR"));
								xmlElement = xmlDocument.DocumentElement;
							}
							else
							{
								if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										if (this.Columns.Count == 0)
										{
											if (reader.IsEmptyElement)
											{
												reader.Read();
												return XmlReadMode.DiffGram;
											}
											throw ExceptionBuilder.DataTableInferenceNotSupported();
										}
										else
										{
											this.ReadXmlDiffgram(reader);
											this.ReadEndElement(reader);
										}
									}
									else
									{
										reader.Skip();
									}
									this.RestoreConstraint(flag3);
									return xmlReadMode;
								}
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXDRSchema(reader);
									}
									else
									{
										reader.Skip();
									}
									this.RestoreConstraint(flag3);
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXmlSchema(reader, denyResolving);
									}
									else
									{
										reader.Skip();
									}
									this.RestoreConstraint(flag3);
									return xmlReadMode;
								}
								if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
								{
									if (this.DataSet != null)
									{
										this.DataSet.RestoreEnforceConstraints(flag3);
									}
									else
									{
										this.enforceConstraints = flag3;
									}
									throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
								}
								xmlElement = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
								if (reader.HasAttributes)
								{
									int attributeCount = reader.AttributeCount;
									for (int i = 0; i < attributeCount; i++)
									{
										reader.MoveToAttribute(i);
										if (reader.NamespaceURI.Equals("http://www.w3.org/2000/xmlns/"))
										{
											xmlElement.SetAttribute(reader.Name, reader.GetAttribute(i));
										}
										else
										{
											XmlAttribute xmlAttribute = xmlElement.SetAttributeNode(reader.LocalName, reader.NamespaceURI);
											xmlAttribute.Prefix = reader.Prefix;
											xmlAttribute.Value = reader.GetAttribute(i);
										}
									}
								}
								reader.Read();
							}
							while (this.MoveToElement(reader, depth))
							{
								if (reader.LocalName == "Schema" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-data")
								{
									if (!flag && !flag2 && mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXDRSchema(reader);
										flag = true;
										isXdr = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
								{
									if (mode != XmlReadMode.IgnoreSchema && mode != XmlReadMode.InferSchema)
									{
										this.ReadXmlSchema(reader, denyResolving);
										flag = true;
									}
									else
									{
										reader.Skip();
									}
								}
								else if (reader.LocalName == "diffgram" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
								{
									if (mode == XmlReadMode.DiffGram || mode == XmlReadMode.IgnoreSchema)
									{
										if (this.Columns.Count == 0)
										{
											if (reader.IsEmptyElement)
											{
												reader.Read();
												return XmlReadMode.DiffGram;
											}
											throw ExceptionBuilder.DataTableInferenceNotSupported();
										}
										else
										{
											this.ReadXmlDiffgram(reader);
											xmlReadMode = XmlReadMode.DiffGram;
										}
									}
									else
									{
										reader.Skip();
									}
								}
								else
								{
									if (reader.LocalName == "schema" && reader.NamespaceURI.StartsWith("http://www.w3.org/", StringComparison.Ordinal))
									{
										if (this.DataSet != null)
										{
											this.DataSet.RestoreEnforceConstraints(flag3);
										}
										else
										{
											this.enforceConstraints = flag3;
										}
										throw ExceptionBuilder.DataSetUnsupportedSchema("http://www.w3.org/2001/XMLSchema");
									}
									if (mode == XmlReadMode.DiffGram)
									{
										reader.Skip();
									}
									else
									{
										flag2 = true;
										if (mode == XmlReadMode.InferSchema)
										{
											XmlNode newChild = xmlDocument.ReadNode(reader);
											xmlElement.AppendChild(newChild);
										}
										else
										{
											if (this.Columns.Count == 0)
											{
												throw ExceptionBuilder.DataTableInferenceNotSupported();
											}
											if (xmlDataLoader == null)
											{
												xmlDataLoader = new XmlDataLoader(this, isXdr, xmlElement, mode == XmlReadMode.IgnoreSchema);
											}
											xmlDataLoader.LoadData(reader);
										}
									}
								}
							}
							this.ReadEndElement(reader);
							xmlDocument.AppendChild(xmlElement);
							if (xmlDataLoader == null)
							{
								xmlDataLoader = new XmlDataLoader(this, isXdr, mode == XmlReadMode.IgnoreSchema);
							}
							if (mode == XmlReadMode.DiffGram)
							{
								this.RestoreConstraint(flag3);
								return xmlReadMode;
							}
							if (mode == XmlReadMode.InferSchema && this.Columns.Count == 0)
							{
								throw ExceptionBuilder.DataTableInferenceNotSupported();
							}
						}
						this.RestoreConstraint(flag3);
						result = xmlReadMode;
					}
				}
			}
			finally
			{
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x00071324 File Offset: 0x00070724
		internal void ReadEndElement(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace)
			{
				reader.Skip();
			}
			if (reader.NodeType == XmlNodeType.None)
			{
				reader.Skip();
				return;
			}
			if (reader.NodeType == XmlNodeType.EndElement)
			{
				reader.ReadEndElement();
			}
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00071364 File Offset: 0x00070764
		internal void ReadXDRSchema(XmlReader reader)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode xmlNode = xmlDocument.ReadNode(reader);
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00071380 File Offset: 0x00070780
		internal bool MoveToElement(XmlReader reader, int depth)
		{
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.Element && reader.Depth > depth)
			{
				reader.Read();
			}
			return reader.NodeType == XmlNodeType.Element;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000713C4 File Offset: 0x000707C4
		private void ReadXmlDiffgram(XmlReader reader)
		{
			int depth = reader.Depth;
			bool flag = this.EnforceConstraints;
			this.EnforceConstraints = false;
			bool flag2;
			DataTable dataTable;
			if (this.Rows.Count == 0)
			{
				flag2 = true;
				dataTable = this;
			}
			else
			{
				flag2 = false;
				dataTable = this.Clone();
				dataTable.EnforceConstraints = false;
			}
			dataTable.Rows.nullInList = 0;
			reader.MoveToContent();
			if (reader.LocalName != "diffgram" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1")
			{
				return;
			}
			reader.Read();
			if (reader.NodeType == XmlNodeType.Whitespace)
			{
				this.MoveToElement(reader, reader.Depth - 1);
			}
			dataTable.fInLoadDiffgram = true;
			if (reader.Depth > depth)
			{
				if (reader.NamespaceURI != "urn:schemas-microsoft-com:xml-diffgram-v1" && reader.NamespaceURI != "urn:schemas-microsoft-com:xml-msdata")
				{
					XmlDocument xmlDocument = new XmlDocument();
					XmlElement topNode = xmlDocument.CreateElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
					reader.Read();
					if (reader.Depth - 1 > depth)
					{
						new XmlDataLoader(dataTable, false, topNode, false)
						{
							isDiffgram = true
						}.LoadData(reader);
					}
					this.ReadEndElement(reader);
				}
				if ((reader.LocalName == "before" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1") || (reader.LocalName == "errors" && reader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					XMLDiffLoader xmldiffLoader = new XMLDiffLoader();
					xmldiffLoader.LoadDiffGram(dataTable, reader);
				}
				while (reader.Depth > depth)
				{
					reader.Read();
				}
				this.ReadEndElement(reader);
			}
			if (dataTable.Rows.nullInList > 0)
			{
				throw ExceptionBuilder.RowInsertMissing(dataTable.TableName);
			}
			dataTable.fInLoadDiffgram = false;
			List<DataTable> list = new List<DataTable>();
			list.Add(this);
			this.CreateTableList(this, list);
			for (int i = 0; i < list.Count; i++)
			{
				DataRelation[] nestedParentRelations = list[i].NestedParentRelations;
				foreach (DataRelation dataRelation in nestedParentRelations)
				{
					if (dataRelation != null && dataRelation.ParentTable == list[i])
					{
						foreach (object obj in list[i].Rows)
						{
							DataRow dataRow = (DataRow)obj;
							foreach (DataRelation rel in nestedParentRelations)
							{
								dataRow.CheckForLoops(rel);
							}
						}
					}
				}
			}
			if (!flag2)
			{
				this.Merge(dataTable);
			}
			this.EnforceConstraints = flag;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x00071694 File Offset: 0x00070A94
		internal void ReadXSDSchema(XmlReader reader, bool denyResolving)
		{
			XmlSchemaSet xmlSchemaSet = new XmlSchemaSet();
			while (reader.LocalName == "schema" && reader.NamespaceURI == "http://www.w3.org/2001/XMLSchema")
			{
				XmlSchema schema = XmlSchema.Read(reader, null);
				xmlSchemaSet.Add(schema);
				this.ReadEndElement(reader);
			}
			xmlSchemaSet.Compile();
			XSDSchema xsdschema = new XSDSchema();
			xsdschema.LoadSchema(xmlSchemaSet, this);
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000716F8 File Offset: 0x00070AF8
		public void ReadXmlSchema(Stream stream)
		{
			if (stream == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(stream), false);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x00071718 File Offset: 0x00070B18
		public void ReadXmlSchema(TextReader reader)
		{
			if (reader == null)
			{
				return;
			}
			this.ReadXmlSchema(new XmlTextReader(reader), false);
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00071738 File Offset: 0x00070B38
		public void ReadXmlSchema(string fileName)
		{
			XmlTextReader xmlTextReader = new XmlTextReader(fileName);
			try
			{
				this.ReadXmlSchema(xmlTextReader, false);
			}
			finally
			{
				xmlTextReader.Close();
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0007177C File Offset: 0x00070B7C
		public void ReadXmlSchema(XmlReader reader)
		{
			this.ReadXmlSchema(reader, false);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00071794 File Offset: 0x00070B94
		internal void ReadXmlSchema(XmlReader reader, bool denyResolving)
		{
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<ds.DataTable.ReadXmlSchema|INFO> %d#, denyResolving=%d{bool}\n", this.ObjectID, denyResolving);
			try
			{
				DataSet dataSet = new DataSet();
				SerializationFormat remotingFormat = this.RemotingFormat;
				dataSet.ReadXmlSchema(reader, denyResolving);
				string mainTableName = dataSet.MainTableName;
				if (!ADP.IsEmpty(this.tableName) || !ADP.IsEmpty(mainTableName))
				{
					DataTable dataTable = null;
					if (!ADP.IsEmpty(this.tableName))
					{
						if (!ADP.IsEmpty(this.Namespace))
						{
							dataTable = dataSet.Tables[this.tableName, this.Namespace];
						}
						else
						{
							int num = dataSet.Tables.InternalIndexOf(this.tableName);
							if (num > -1)
							{
								dataTable = dataSet.Tables[num];
							}
						}
					}
					else
					{
						string text = "";
						int num2 = mainTableName.IndexOf(':');
						if (num2 > -1)
						{
							text = mainTableName.Substring(0, num2);
						}
						string name = mainTableName.Substring(num2 + 1, mainTableName.Length - num2 - 1);
						dataTable = dataSet.Tables[name, text];
					}
					if (dataTable == null)
					{
						string text2 = string.Empty;
						if (!ADP.IsEmpty(this.tableName))
						{
							text2 = ((this.Namespace.Length > 0) ? (this.Namespace + ":" + this.tableName) : this.tableName);
						}
						else
						{
							text2 = mainTableName;
						}
						throw ExceptionBuilder.TableNotFound(text2);
					}
					dataTable._remotingFormat = remotingFormat;
					List<DataTable> list = new List<DataTable>();
					list.Add(dataTable);
					this.CreateTableList(dataTable, list);
					List<DataRelation> list2 = new List<DataRelation>();
					this.CreateRelationList(list, list2);
					if (list2.Count == 0)
					{
						if (this.Columns.Count == 0)
						{
							DataTable dataTable2 = dataTable;
							if (dataTable2 != null)
							{
								dataTable2.CloneTo(this, null, false);
							}
							if (this.DataSet == null && this.tableNamespace == null)
							{
								this.tableNamespace = dataTable2.Namespace;
							}
						}
					}
					else
					{
						if (ADP.IsEmpty(this.TableName))
						{
							this.TableName = dataTable.TableName;
							if (!ADP.IsEmpty(dataTable.Namespace))
							{
								this.Namespace = dataTable.Namespace;
							}
						}
						if (this.DataSet == null)
						{
							DataSet dataSet2 = new DataSet(dataSet.DataSetName);
							dataSet2.SetLocaleValue(dataSet.Locale, dataSet.ShouldSerializeLocale());
							dataSet2.CaseSensitive = dataSet.CaseSensitive;
							dataSet2.Namespace = dataSet.Namespace;
							dataSet2.mainTableName = dataSet.mainTableName;
							dataSet2.RemotingFormat = dataSet.RemotingFormat;
							dataSet2.Tables.Add(this);
						}
						DataTable dataTable3 = this.CloneHierarchy(dataTable, this.DataSet, null);
						foreach (DataTable dataTable4 in list)
						{
							DataTable dataTable5 = this.DataSet.Tables[dataTable4.tableName, dataTable4.Namespace];
							DataTable dataTable6 = dataSet.Tables[dataTable4.tableName, dataTable4.Namespace];
							foreach (object obj in dataTable6.Constraints)
							{
								Constraint constraint = (Constraint)obj;
								ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
								if (foreignKeyConstraint != null && foreignKeyConstraint.Table != foreignKeyConstraint.RelatedTable && list.Contains(foreignKeyConstraint.Table) && list.Contains(foreignKeyConstraint.RelatedTable))
								{
									ForeignKeyConstraint foreignKeyConstraint2 = (ForeignKeyConstraint)foreignKeyConstraint.Clone(dataTable5.DataSet);
									if (!dataTable5.Constraints.Contains(foreignKeyConstraint2.ConstraintName))
									{
										dataTable5.Constraints.Add(foreignKeyConstraint2);
									}
								}
							}
						}
						foreach (DataRelation dataRelation in list2)
						{
							if (!this.DataSet.Relations.Contains(dataRelation.RelationName))
							{
								this.DataSet.Relations.Add(dataRelation.Clone(this.DataSet));
							}
						}
						foreach (DataTable dataTable7 in list)
						{
							foreach (object obj2 in dataTable7.Columns)
							{
								DataColumn dataColumn = (DataColumn)obj2;
								bool flag = false;
								if (dataColumn.Expression.Length != 0)
								{
									DataColumn[] dependency = dataColumn.DataExpression.GetDependency();
									for (int i = 0; i < dependency.Length; i++)
									{
										if (!list.Contains(dependency[i].Table))
										{
											flag = true;
											break;
										}
									}
								}
								if (!flag)
								{
									this.DataSet.Tables[dataTable7.TableName, dataTable7.Namespace].Columns[dataColumn.ColumnName].Expression = dataColumn.Expression;
								}
							}
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00071D34 File Offset: 0x00071134
		private void CreateTableList(DataTable currentTable, List<DataTable> tableList)
		{
			foreach (object obj in currentTable.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!tableList.Contains(dataRelation.ChildTable))
				{
					tableList.Add(dataRelation.ChildTable);
					this.CreateTableList(dataRelation.ChildTable, tableList);
				}
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00071DBC File Offset: 0x000711BC
		private void CreateRelationList(List<DataTable> tableList, List<DataRelation> relationList)
		{
			foreach (DataTable dataTable in tableList)
			{
				foreach (object obj in dataTable.ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (tableList.Contains(dataRelation.ChildTable) && tableList.Contains(dataRelation.ParentTable))
					{
						relationList.Add(dataRelation);
					}
				}
			}
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x00071E80 File Offset: 0x00071280
		public static XmlSchemaComplexType GetDataTableSchema(XmlSchemaSet schemaSet)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
			xmlSchemaAny.MinOccurs = 1m;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			return xmlSchemaComplexType;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00071F14 File Offset: 0x00071314
		XmlSchema IXmlSerializable.GetSchema()
		{
			return this.GetSchema();
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00071F28 File Offset: 0x00071328
		protected virtual XmlSchema GetSchema()
		{
			if (base.GetType() == typeof(DataTable))
			{
				return null;
			}
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = new XmlTextWriter(memoryStream, null);
			if (xmlWriter != null)
			{
				new XmlTreeGen(SchemaFormat.WebService).Save(this, xmlWriter);
			}
			memoryStream.Position = 0L;
			return XmlSchema.Read(new XmlTextReader(memoryStream), null);
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00071F80 File Offset: 0x00071380
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			IXmlTextParser xmlTextParser = reader as IXmlTextParser;
			bool normalized = true;
			if (xmlTextParser != null)
			{
				normalized = xmlTextParser.Normalized;
				xmlTextParser.Normalized = false;
			}
			this.ReadXmlSerializable(reader);
			if (xmlTextParser != null)
			{
				xmlTextParser.Normalized = normalized;
			}
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00071FB8 File Offset: 0x000713B8
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			this.WriteXmlSchema(writer, false);
			this.WriteXml(writer, XmlWriteMode.DiffGram, false);
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00071FD8 File Offset: 0x000713D8
		protected virtual void ReadXmlSerializable(XmlReader reader)
		{
			this.ReadXml(reader, XmlReadMode.DiffGram, true);
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x00071FF0 File Offset: 0x000713F0
		internal Hashtable RowDiffId
		{
			get
			{
				if (this.rowDiffId == null)
				{
					this.rowDiffId = new Hashtable();
				}
				return this.rowDiffId;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x00072018 File Offset: 0x00071418
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x0007202C File Offset: 0x0007142C
		internal void AddDependentColumn(DataColumn expressionColumn)
		{
			if (this.dependentColumns == null)
			{
				this.dependentColumns = new List<DataColumn>();
			}
			if (!this.dependentColumns.Contains(expressionColumn))
			{
				this.dependentColumns.Add(expressionColumn);
			}
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00072068 File Offset: 0x00071468
		internal void RemoveDependentColumn(DataColumn expressionColumn)
		{
			if (this.dependentColumns != null && this.dependentColumns.Contains(expressionColumn))
			{
				this.dependentColumns.Remove(expressionColumn);
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00072098 File Offset: 0x00071498
		internal void EvaluateExpressions()
		{
			if (this.dependentColumns != null && 0 < this.dependentColumns.Count)
			{
				foreach (object obj in this.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.oldRecord != -1 && dataRow.oldRecord != dataRow.newRecord)
					{
						this.EvaluateDependentExpressions(this.dependentColumns, dataRow, DataRowVersion.Original, null);
					}
					if (dataRow.newRecord != -1)
					{
						this.EvaluateDependentExpressions(this.dependentColumns, dataRow, DataRowVersion.Current, null);
					}
					if (dataRow.tempRecord != -1)
					{
						this.EvaluateDependentExpressions(this.dependentColumns, dataRow, DataRowVersion.Proposed, null);
					}
				}
			}
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00072174 File Offset: 0x00071574
		internal void EvaluateExpressions(DataRow row, DataRowAction action, List<DataRow> cachedRows)
		{
			if (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && (row.oldRecord != -1 || row.newRecord != -1)))
			{
				if (row.oldRecord != -1 && row.oldRecord != row.newRecord)
				{
					this.EvaluateDependentExpressions(this.dependentColumns, row, DataRowVersion.Original, cachedRows);
				}
				if (row.newRecord != -1)
				{
					this.EvaluateDependentExpressions(this.dependentColumns, row, DataRowVersion.Current, cachedRows);
				}
				if (row.tempRecord != -1)
				{
					this.EvaluateDependentExpressions(this.dependentColumns, row, DataRowVersion.Proposed, cachedRows);
				}
				return;
			}
			if ((action == DataRowAction.Delete || (action == DataRowAction.Rollback && row.oldRecord == -1 && row.newRecord == -1)) && this.dependentColumns != null)
			{
				foreach (DataColumn dataColumn in this.dependentColumns)
				{
					if (dataColumn.DataExpression != null && dataColumn.DataExpression.HasLocalAggregate() && dataColumn.Table == this)
					{
						for (int i = 0; i < this.Rows.Count; i++)
						{
							DataRow dataRow = this.Rows[i];
							if (dataRow.oldRecord != -1 && dataRow.oldRecord != dataRow.newRecord)
							{
								this.EvaluateDependentExpressions(this.dependentColumns, dataRow, DataRowVersion.Original, null);
							}
						}
						for (int j = 0; j < this.Rows.Count; j++)
						{
							DataRow dataRow2 = this.Rows[j];
							if (dataRow2.tempRecord != -1)
							{
								this.EvaluateDependentExpressions(this.dependentColumns, dataRow2, DataRowVersion.Proposed, null);
							}
						}
						for (int k = 0; k < this.Rows.Count; k++)
						{
							DataRow dataRow3 = this.Rows[k];
							if (dataRow3.newRecord != -1)
							{
								this.EvaluateDependentExpressions(this.dependentColumns, dataRow3, DataRowVersion.Current, null);
							}
						}
						break;
					}
				}
				if (cachedRows != null)
				{
					foreach (DataRow dataRow4 in cachedRows)
					{
						if (dataRow4.oldRecord != -1 && dataRow4.oldRecord != dataRow4.newRecord)
						{
							dataRow4.Table.EvaluateDependentExpressions(dataRow4.Table.dependentColumns, dataRow4, DataRowVersion.Original, null);
						}
						if (dataRow4.newRecord != -1)
						{
							dataRow4.Table.EvaluateDependentExpressions(dataRow4.Table.dependentColumns, dataRow4, DataRowVersion.Current, null);
						}
						if (dataRow4.tempRecord != -1)
						{
							dataRow4.Table.EvaluateDependentExpressions(dataRow4.Table.dependentColumns, dataRow4, DataRowVersion.Proposed, null);
						}
					}
				}
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00072458 File Offset: 0x00071858
		internal void EvaluateExpressions(DataColumn column)
		{
			int count = column.table.Rows.Count;
			if (column.DataExpression.IsTableAggregate() && count > 0)
			{
				object value = column.DataExpression.Evaluate();
				for (int i = 0; i < count; i++)
				{
					DataRow dataRow = column.table.Rows[i];
					if (dataRow.oldRecord != -1 && dataRow.oldRecord != dataRow.newRecord)
					{
						column[dataRow.oldRecord] = value;
					}
					if (dataRow.newRecord != -1)
					{
						column[dataRow.newRecord] = value;
					}
					if (dataRow.tempRecord != -1)
					{
						column[dataRow.tempRecord] = value;
					}
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					DataRow dataRow2 = column.table.Rows[j];
					if (dataRow2.oldRecord != -1 && dataRow2.oldRecord != dataRow2.newRecord)
					{
						column[dataRow2.oldRecord] = column.DataExpression.Evaluate(dataRow2, DataRowVersion.Original);
					}
					if (dataRow2.newRecord != -1)
					{
						column[dataRow2.newRecord] = column.DataExpression.Evaluate(dataRow2, DataRowVersion.Current);
					}
					if (dataRow2.tempRecord != -1)
					{
						column[dataRow2.tempRecord] = column.DataExpression.Evaluate(dataRow2, DataRowVersion.Proposed);
					}
				}
			}
			column.Table.ResetInternalIndexes(column);
			this.EvaluateDependentExpressions(column);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000725CC File Offset: 0x000719CC
		internal void EvaluateDependentExpressions(DataColumn column)
		{
			if (column.dependentColumns != null)
			{
				foreach (DataColumn dataColumn in column.dependentColumns)
				{
					if (dataColumn.table != null && column != dataColumn)
					{
						this.EvaluateExpressions(dataColumn);
					}
				}
			}
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00072640 File Offset: 0x00071A40
		internal void EvaluateDependentExpressions(List<DataColumn> columns, DataRow row, DataRowVersion version, List<DataRow> cachedRows)
		{
			if (columns == null)
			{
				return;
			}
			int count = columns.Count;
			for (int i = 0; i < count; i++)
			{
				if (columns[i].Table == this)
				{
					DataColumn dataColumn = columns[i];
					if (dataColumn.DataExpression != null && dataColumn.DataExpression.HasLocalAggregate())
					{
						DataRowVersion dataRowVersion = (version == DataRowVersion.Proposed) ? DataRowVersion.Default : version;
						bool flag = dataColumn.DataExpression.IsTableAggregate();
						object newValue = null;
						if (flag)
						{
							newValue = dataColumn.DataExpression.Evaluate(row, dataRowVersion);
						}
						for (int j = 0; j < this.Rows.Count; j++)
						{
							DataRow dataRow = this.Rows[j];
							if (dataRow.RowState != DataRowState.Deleted && (dataRowVersion != DataRowVersion.Original || (dataRow.oldRecord != -1 && dataRow.oldRecord != dataRow.newRecord)))
							{
								if (!flag)
								{
									newValue = dataColumn.DataExpression.Evaluate(dataRow, dataRowVersion);
								}
								this.SilentlySetValue(dataRow, dataColumn, dataRowVersion, newValue);
							}
						}
					}
					else if (row.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || (row.oldRecord != -1 && row.oldRecord != row.newRecord)))
					{
						this.SilentlySetValue(row, dataColumn, version, (dataColumn.DataExpression == null) ? dataColumn.DefaultValue : dataColumn.DataExpression.Evaluate(row, version));
					}
				}
			}
			count = columns.Count;
			for (int k = 0; k < count; k++)
			{
				DataColumn dataColumn2 = columns[k];
				if (dataColumn2.Table != this || (dataColumn2.DataExpression != null && !dataColumn2.DataExpression.HasLocalAggregate()))
				{
					DataRowVersion dataRowVersion2 = (version == DataRowVersion.Proposed) ? DataRowVersion.Default : version;
					if (cachedRows != null)
					{
						foreach (DataRow dataRow2 in cachedRows)
						{
							if (dataRow2.Table == dataColumn2.Table && (dataRowVersion2 != DataRowVersion.Original || dataRow2.newRecord != dataRow2.oldRecord) && dataRow2 != null && dataRow2.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow2.oldRecord != -1))
							{
								object newValue2 = dataColumn2.DataExpression.Evaluate(dataRow2, dataRowVersion2);
								this.SilentlySetValue(dataRow2, dataColumn2, dataRowVersion2, newValue2);
							}
						}
					}
					for (int l = 0; l < this.ParentRelations.Count; l++)
					{
						DataRelation dataRelation = this.ParentRelations[l];
						if (dataRelation.ParentTable == dataColumn2.Table)
						{
							foreach (DataRow dataRow3 in row.GetParentRows(dataRelation, version))
							{
								if ((cachedRows == null || !cachedRows.Contains(dataRow3)) && (dataRowVersion2 != DataRowVersion.Original || dataRow3.newRecord != dataRow3.oldRecord) && dataRow3 != null && dataRow3.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow3.oldRecord != -1))
								{
									object newValue3 = dataColumn2.DataExpression.Evaluate(dataRow3, dataRowVersion2);
									this.SilentlySetValue(dataRow3, dataColumn2, dataRowVersion2, newValue3);
								}
							}
						}
					}
					for (int n = 0; n < this.ChildRelations.Count; n++)
					{
						DataRelation dataRelation2 = this.ChildRelations[n];
						if (dataRelation2.ChildTable == dataColumn2.Table)
						{
							foreach (DataRow dataRow4 in row.GetChildRows(dataRelation2, version))
							{
								if ((cachedRows == null || !cachedRows.Contains(dataRow4)) && (dataRowVersion2 != DataRowVersion.Original || dataRow4.newRecord != dataRow4.oldRecord) && dataRow4 != null && dataRow4.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow4.oldRecord != -1))
								{
									object newValue4 = dataColumn2.DataExpression.Evaluate(dataRow4, dataRowVersion2);
									this.SilentlySetValue(dataRow4, dataColumn2, dataRowVersion2, newValue4);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x040003A6 RID: 934
		private DataSet dataSet;

		// Token: 0x040003A7 RID: 935
		private DataView defaultView;

		// Token: 0x040003A8 RID: 936
		internal long nextRowID;

		// Token: 0x040003A9 RID: 937
		internal readonly DataRowCollection rowCollection;

		// Token: 0x040003AA RID: 938
		internal readonly DataColumnCollection columnCollection;

		// Token: 0x040003AB RID: 939
		private readonly ConstraintCollection constraintCollection;

		// Token: 0x040003AC RID: 940
		private int elementColumnCount;

		// Token: 0x040003AD RID: 941
		internal DataRelationCollection parentRelationsCollection;

		// Token: 0x040003AE RID: 942
		internal DataRelationCollection childRelationsCollection;

		// Token: 0x040003AF RID: 943
		internal readonly RecordManager recordManager;

		// Token: 0x040003B0 RID: 944
		internal readonly List<Index> indexes;

		// Token: 0x040003B1 RID: 945
		private List<Index> shadowIndexes;

		// Token: 0x040003B2 RID: 946
		private int shadowCount;

		// Token: 0x040003B3 RID: 947
		internal PropertyCollection extendedProperties;

		// Token: 0x040003B4 RID: 948
		private string tableName = "";

		// Token: 0x040003B5 RID: 949
		internal string tableNamespace;

		// Token: 0x040003B6 RID: 950
		private string tablePrefix = "";

		// Token: 0x040003B7 RID: 951
		internal DataExpression displayExpression;

		// Token: 0x040003B8 RID: 952
		internal bool fNestedInDataset = true;

		// Token: 0x040003B9 RID: 953
		private CultureInfo _culture;

		// Token: 0x040003BA RID: 954
		private bool _cultureUserSet;

		// Token: 0x040003BB RID: 955
		private CompareInfo _compareInfo;

		// Token: 0x040003BC RID: 956
		private CompareOptions _compareFlags = CompareOptions.IgnoreCase | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x040003BD RID: 957
		private IFormatProvider _formatProvider;

		// Token: 0x040003BE RID: 958
		private StringComparer _hashCodeProvider;

		// Token: 0x040003BF RID: 959
		private bool _caseSensitive;

		// Token: 0x040003C0 RID: 960
		private bool _caseSensitiveUserSet;

		// Token: 0x040003C1 RID: 961
		internal string encodedTableName;

		// Token: 0x040003C2 RID: 962
		internal DataColumn xmlText;

		// Token: 0x040003C3 RID: 963
		internal DataColumn _colUnique;

		// Token: 0x040003C4 RID: 964
		internal bool textOnly;

		// Token: 0x040003C5 RID: 965
		internal decimal minOccurs = 1m;

		// Token: 0x040003C6 RID: 966
		internal decimal maxOccurs = 1m;

		// Token: 0x040003C7 RID: 967
		internal bool repeatableElement;

		// Token: 0x040003C8 RID: 968
		private object typeName;

		// Token: 0x040003C9 RID: 969
		private static readonly int[] zeroIntegers = new int[0];

		// Token: 0x040003CA RID: 970
		internal static readonly DataColumn[] zeroColumns = new DataColumn[0];

		// Token: 0x040003CB RID: 971
		internal static readonly DataRow[] zeroRows = new DataRow[0];

		// Token: 0x040003CC RID: 972
		internal UniqueConstraint primaryKey;

		// Token: 0x040003CD RID: 973
		internal static readonly IndexField[] zeroIndexField = new IndexField[0];

		// Token: 0x040003CE RID: 974
		internal IndexField[] _primaryIndex = DataTable.zeroIndexField;

		// Token: 0x040003CF RID: 975
		private DataColumn[] delayedSetPrimaryKey;

		// Token: 0x040003D0 RID: 976
		private Index loadIndex;

		// Token: 0x040003D1 RID: 977
		private Index loadIndexwithOriginalAdded;

		// Token: 0x040003D2 RID: 978
		private Index loadIndexwithCurrentDeleted;

		// Token: 0x040003D3 RID: 979
		private int _suspendIndexEvents;

		// Token: 0x040003D4 RID: 980
		private bool savedEnforceConstraints;

		// Token: 0x040003D5 RID: 981
		private bool inDataLoad;

		// Token: 0x040003D6 RID: 982
		private bool initialLoad;

		// Token: 0x040003D7 RID: 983
		private bool schemaLoading;

		// Token: 0x040003D8 RID: 984
		private bool enforceConstraints = true;

		// Token: 0x040003D9 RID: 985
		internal bool _suspendEnforceConstraints;

		// Token: 0x040003DA RID: 986
		protected internal bool fInitInProgress;

		// Token: 0x040003DB RID: 987
		private bool inLoad;

		// Token: 0x040003DC RID: 988
		internal bool fInLoadDiffgram;

		// Token: 0x040003DD RID: 989
		private byte _isTypedDataTable;

		// Token: 0x040003DE RID: 990
		private DataRow[] EmptyDataRowArray;

		// Token: 0x040003DF RID: 991
		private PropertyDescriptorCollection propertyDescriptorCollectionCache;

		// Token: 0x040003E0 RID: 992
		private static readonly DataRelation[] EmptyArrayDataRelation = new DataRelation[0];

		// Token: 0x040003E1 RID: 993
		private DataRelation[] _nestedParentRelations = DataTable.EmptyArrayDataRelation;

		// Token: 0x040003E2 RID: 994
		internal List<DataColumn> dependentColumns;

		// Token: 0x040003E3 RID: 995
		private bool mergingData;

		// Token: 0x040003E4 RID: 996
		private DataRowChangeEventHandler onRowChangedDelegate;

		// Token: 0x040003E5 RID: 997
		private DataRowChangeEventHandler onRowChangingDelegate;

		// Token: 0x040003E6 RID: 998
		private DataRowChangeEventHandler onRowDeletingDelegate;

		// Token: 0x040003E7 RID: 999
		private DataRowChangeEventHandler onRowDeletedDelegate;

		// Token: 0x040003E8 RID: 1000
		private DataColumnChangeEventHandler onColumnChangedDelegate;

		// Token: 0x040003E9 RID: 1001
		private DataColumnChangeEventHandler onColumnChangingDelegate;

		// Token: 0x040003EA RID: 1002
		private DataTableClearEventHandler onTableClearingDelegate;

		// Token: 0x040003EB RID: 1003
		private DataTableClearEventHandler onTableClearedDelegate;

		// Token: 0x040003EC RID: 1004
		private DataTableNewRowEventHandler onTableNewRowDelegate;

		// Token: 0x040003ED RID: 1005
		private PropertyChangedEventHandler onPropertyChangingDelegate;

		// Token: 0x040003EE RID: 1006
		private EventHandler onInitialized;

		// Token: 0x040003EF RID: 1007
		private readonly DataRowBuilder rowBuilder;

		// Token: 0x040003F0 RID: 1008
		private const string KEY_XMLSCHEMA = "XmlSchema";

		// Token: 0x040003F1 RID: 1009
		private const string KEY_XMLDIFFGRAM = "XmlDiffGram";

		// Token: 0x040003F2 RID: 1010
		private const string KEY_NAME = "TableName";

		// Token: 0x040003F3 RID: 1011
		internal readonly List<DataView> delayedViews = new List<DataView>();

		// Token: 0x040003F4 RID: 1012
		private readonly List<DataViewListener> _dataViewListeners = new List<DataViewListener>();

		// Token: 0x040003F5 RID: 1013
		internal Hashtable rowDiffId;

		// Token: 0x040003F6 RID: 1014
		internal readonly ReaderWriterLock indexesLock = new ReaderWriterLock();

		// Token: 0x040003F7 RID: 1015
		internal int ukColumnPositionForInference = -1;

		// Token: 0x040003F8 RID: 1016
		private SerializationFormat _remotingFormat;

		// Token: 0x040003F9 RID: 1017
		private static int _objectTypeCount;

		// Token: 0x040003FA RID: 1018
		private readonly int _objectID = Interlocked.Increment(ref DataTable._objectTypeCount);

		// Token: 0x0200034B RID: 843
		internal struct RowDiffIdUsageSection
		{
			// Token: 0x060033FF RID: 13311 RVA: 0x0013FDE0 File Offset: 0x0013F1E0
			internal void Prepare(DataTable table)
			{
				this._targetTable = table;
				table.rowDiffId = null;
			}

			// Token: 0x06003400 RID: 13312 RVA: 0x0013FDFC File Offset: 0x0013F1FC
			[Conditional("DEBUG")]
			internal void Cleanup()
			{
				if (this._targetTable != null)
				{
					this._targetTable.rowDiffId = null;
				}
			}

			// Token: 0x06003401 RID: 13313 RVA: 0x0013FE20 File Offset: 0x0013F220
			[Conditional("DEBUG")]
			internal static void Assert(string message)
			{
			}

			// Token: 0x04001EB9 RID: 7865
			private DataTable _targetTable;
		}

		// Token: 0x0200034C RID: 844
		internal struct DSRowDiffIdUsageSection
		{
			// Token: 0x06003402 RID: 13314 RVA: 0x0013FE30 File Offset: 0x0013F230
			internal void Prepare(DataSet ds)
			{
				this._targetDS = ds;
				for (int i = 0; i < ds.Tables.Count; i++)
				{
					DataTable dataTable = ds.Tables[i];
					dataTable.rowDiffId = null;
				}
			}

			// Token: 0x06003403 RID: 13315 RVA: 0x0013FE70 File Offset: 0x0013F270
			[Conditional("DEBUG")]
			internal void Cleanup()
			{
				if (this._targetDS != null)
				{
					for (int i = 0; i < this._targetDS.Tables.Count; i++)
					{
						DataTable dataTable = this._targetDS.Tables[i];
						dataTable.rowDiffId = null;
					}
				}
			}

			// Token: 0x04001EBA RID: 7866
			private DataSet _targetDS;
		}
	}
}

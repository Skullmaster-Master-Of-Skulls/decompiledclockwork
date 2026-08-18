using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x020000F7 RID: 247
	internal sealed class XmlDataTreeWriter
	{
		// Token: 0x06000E3A RID: 3642 RVA: 0x00220848 File Offset: 0x0021FC48
		internal XmlDataTreeWriter(DataSet ds)
		{
			this._ds = ds;
			this.topLevelTables = ds.TopLevelTables();
			foreach (object obj in ds.Tables)
			{
				DataTable value = (DataTable)obj;
				this._dTables.Add(value);
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x002208D8 File Offset: 0x0021FCD8
		internal XmlDataTreeWriter(DataSet ds, DataTable dt)
		{
			this._ds = ds;
			this._dt = dt;
			this._dTables.Add(dt);
			this.topLevelTables = ds.TopLevelTables();
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00220928 File Offset: 0x0021FD28
		internal XmlDataTreeWriter(DataTable dt, bool writeHierarchy)
		{
			this._dt = dt;
			this.fFromTable = true;
			if (dt.DataSet == null)
			{
				this._dTables.Add(dt);
				this.topLevelTables = new DataTable[]
				{
					dt
				};
				return;
			}
			this._ds = dt.DataSet;
			this._dTables.Add(dt);
			if (writeHierarchy)
			{
				this._writeHierarchy = true;
				this.CreateTablesHierarchy(dt);
				this.topLevelTables = this.CreateToplevelTables();
				return;
			}
			this.topLevelTables = new DataTable[]
			{
				dt
			};
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x002209C8 File Offset: 0x0021FDC8
		private DataTable[] CreateToplevelTables()
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this._dTables.Count; i++)
			{
				DataTable dataTable = (DataTable)this._dTables[i];
				if (dataTable.ParentRelations.Count == 0)
				{
					arrayList.Add(dataTable);
				}
				else
				{
					bool flag = false;
					for (int j = 0; j < dataTable.ParentRelations.Count; j++)
					{
						if (dataTable.ParentRelations[j].Nested)
						{
							if (dataTable.ParentRelations[j].ParentTable == dataTable)
							{
								flag = false;
								break;
							}
							flag = true;
						}
					}
					if (!flag)
					{
						arrayList.Add(dataTable);
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return new DataTable[0];
			}
			DataTable[] array = new DataTable[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00220AA8 File Offset: 0x0021FEA8
		private void CreateTablesHierarchy(DataTable dt)
		{
			foreach (object obj in dt.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!this._dTables.Contains(dataRelation.ChildTable))
				{
					this._dTables.Add(dataRelation.ChildTable);
					this.CreateTablesHierarchy(dataRelation.ChildTable);
				}
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00220B38 File Offset: 0x0021FF38
		internal static bool RowHasErrors(DataRow row)
		{
			int count = row.Table.Columns.Count;
			if (row.HasErrors && row.RowError.Length > 0)
			{
				return true;
			}
			for (int i = 0; i < count; i++)
			{
				DataColumn column = row.Table.Columns[i];
				string columnError = row.GetColumnError(column);
				if (columnError != null && columnError.Length != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00220BA8 File Offset: 0x0021FFA8
		internal void SaveDiffgramData(XmlWriter xw, Hashtable rowsOrder)
		{
			this._xmlw = DataTextWriter.CreateWriter(xw);
			this.isDiffgram = true;
			this.rowsOrder = rowsOrder;
			int num = this.topLevelTables.Length;
			string prefix = (this._ds != null) ? ((this._ds.Namespace.Length == 0) ? "" : this._ds.Prefix) : ((this._dt.Namespace.Length == 0) ? "" : this._dt.Prefix);
			if (this._ds == null || this._ds.DataSetName == null || this._ds.DataSetName.Length == 0)
			{
				this._xmlw.WriteStartElement(prefix, "DocumentElement", (this._dt.Namespace == null) ? "" : this._dt.Namespace);
			}
			else
			{
				this._xmlw.WriteStartElement(prefix, XmlConvert.EncodeLocalName(this._ds.DataSetName), this._ds.Namespace);
			}
			for (int i = 0; i < this._dTables.Count; i++)
			{
				DataTable dataTable = (DataTable)this._dTables[i];
				foreach (object obj in dataTable.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						int nestedParentCount = dataRow.GetNestedParentCount();
						if (nestedParentCount == 0)
						{
							DataTable dataTable2 = (DataTable)this._dTables[i];
							this.XmlDataRowWriter(dataRow, dataTable2.EncodedTableName);
						}
						else if (nestedParentCount > 1)
						{
							throw ExceptionBuilder.MultipleParentRows((dataTable.Namespace.Length == 0) ? dataTable.TableName : (dataTable.Namespace + dataTable.TableName));
						}
					}
				}
			}
			this._xmlw.WriteEndElement();
			this._xmlw.Flush();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00220DB8 File Offset: 0x002201B8
		internal void Save(XmlWriter xw, bool writeSchema)
		{
			this._xmlw = DataTextWriter.CreateWriter(xw);
			int num = this.topLevelTables.Length;
			bool flag = true;
			string prefix = (this._ds != null) ? ((this._ds.Namespace.Length == 0) ? "" : this._ds.Prefix) : ((this._dt.Namespace.Length == 0) ? "" : this._dt.Prefix);
			if (!writeSchema && this._ds != null && this._ds.fTopLevelTable && num == 1 && this._ds.TopLevelTables()[0].Rows.Count == 1)
			{
				flag = false;
			}
			if (flag)
			{
				if (this._ds == null)
				{
					this._xmlw.WriteStartElement(prefix, "DocumentElement", this._dt.Namespace);
				}
				else if (this._ds.DataSetName == null || this._ds.DataSetName.Length == 0)
				{
					this._xmlw.WriteStartElement(prefix, "DocumentElement", this._ds.Namespace);
				}
				else
				{
					this._xmlw.WriteStartElement(prefix, XmlConvert.EncodeLocalName(this._ds.DataSetName), this._ds.Namespace);
				}
				for (int i = 0; i < this._dTables.Count; i++)
				{
					if (((DataTable)this._dTables[i]).xmlText != null)
					{
						this._xmlw.WriteAttributeString("xmlns", "xsi", "http://www.w3.org/2000/xmlns/", "http://www.w3.org/2001/XMLSchema-instance");
						break;
					}
				}
				if (writeSchema)
				{
					if (!this.fFromTable)
					{
						new XmlTreeGen(SchemaFormat.Public).Save(this._ds, this._xmlw);
					}
					else
					{
						new XmlTreeGen(SchemaFormat.Public).Save(null, this._dt, this._xmlw, this._writeHierarchy);
					}
				}
			}
			for (int j = 0; j < this._dTables.Count; j++)
			{
				foreach (object obj in ((DataTable)this._dTables[j]).Rows)
				{
					DataRow dataRow = (DataRow)obj;
					if (dataRow.RowState != DataRowState.Deleted)
					{
						int nestedParentCount = dataRow.GetNestedParentCount();
						if (nestedParentCount == 0)
						{
							this.XmlDataRowWriter(dataRow, ((DataTable)this._dTables[j]).EncodedTableName);
						}
						else if (nestedParentCount > 1)
						{
							DataTable dataTable = (DataTable)this._dTables[j];
							throw ExceptionBuilder.MultipleParentRows((dataTable.Namespace.Length == 0) ? dataTable.TableName : (dataTable.Namespace + dataTable.TableName));
						}
					}
				}
			}
			if (flag)
			{
				this._xmlw.WriteEndElement();
			}
			this._xmlw.Flush();
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x002210B8 File Offset: 0x002204B8
		private ArrayList GetNestedChildRelations(DataRow row)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in row.Table.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.Nested)
				{
					arrayList.Add(dataRelation);
				}
			}
			return arrayList;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00221138 File Offset: 0x00220538
		internal void XmlDataRowWriter(DataRow row, string encodedTableName)
		{
			string prefix = (row.Table.Namespace.Length == 0) ? "" : row.Table.Prefix;
			this._xmlw.WriteStartElement(prefix, encodedTableName, row.Table.Namespace);
			if (this.isDiffgram)
			{
				this._xmlw.WriteAttributeString("diffgr", "id", "urn:schemas-microsoft-com:xml-diffgram-v1", row.Table.TableName + row.rowID.ToString(CultureInfo.InvariantCulture));
				this._xmlw.WriteAttributeString("msdata", "rowOrder", "urn:schemas-microsoft-com:xml-msdata", this.rowsOrder[row].ToString());
				if (row.RowState == DataRowState.Added)
				{
					this._xmlw.WriteAttributeString("diffgr", "hasChanges", "urn:schemas-microsoft-com:xml-diffgram-v1", "inserted");
				}
				if (row.RowState == DataRowState.Modified)
				{
					this._xmlw.WriteAttributeString("diffgr", "hasChanges", "urn:schemas-microsoft-com:xml-diffgram-v1", "modified");
				}
				if (XmlDataTreeWriter.RowHasErrors(row))
				{
					this._xmlw.WriteAttributeString("diffgr", "hasErrors", "urn:schemas-microsoft-com:xml-diffgram-v1", "true");
				}
			}
			foreach (object obj in row.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.columnMapping == MappingType.Attribute)
				{
					object obj2 = row[dataColumn];
					string prefix2 = (dataColumn.Namespace.Length == 0) ? "" : dataColumn.Prefix;
					if (obj2 != DBNull.Value && (!dataColumn.ImplementsINullable || !DataStorage.IsObjectSqlNull(obj2)))
					{
						XmlTreeGen.ValidateColumnMapping(dataColumn.DataType);
						this._xmlw.WriteAttributeString(prefix2, dataColumn.EncodedColumnName, dataColumn.Namespace, dataColumn.ConvertObjectToXml(obj2));
					}
				}
				if (this.isDiffgram && dataColumn.columnMapping == MappingType.Hidden)
				{
					object obj2 = row[dataColumn];
					if (obj2 != DBNull.Value && (!dataColumn.ImplementsINullable || !DataStorage.IsObjectSqlNull(obj2)))
					{
						XmlTreeGen.ValidateColumnMapping(dataColumn.DataType);
						this._xmlw.WriteAttributeString("msdata", "hidden" + dataColumn.EncodedColumnName, "urn:schemas-microsoft-com:xml-msdata", dataColumn.ConvertObjectToXml(obj2));
					}
				}
			}
			foreach (object obj3 in row.Table.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj3;
				if (dataColumn2.columnMapping != MappingType.Hidden)
				{
					object obj2 = row[dataColumn2];
					string prefix3 = (dataColumn2.Namespace.Length == 0) ? "" : dataColumn2.Prefix;
					bool flag = true;
					if ((obj2 == DBNull.Value || (dataColumn2.ImplementsINullable && DataStorage.IsObjectSqlNull(obj2))) && dataColumn2.ColumnMapping == MappingType.SimpleContent)
					{
						this._xmlw.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
					}
					if (obj2 != DBNull.Value && (!dataColumn2.ImplementsINullable || !DataStorage.IsObjectSqlNull(obj2)) && dataColumn2.columnMapping != MappingType.Attribute)
					{
						if (dataColumn2.columnMapping != MappingType.SimpleContent && (!dataColumn2.IsCustomType || !dataColumn2.IsValueCustomTypeInstance(obj2) || typeof(IXmlSerializable).IsAssignableFrom(obj2.GetType())))
						{
							this._xmlw.WriteStartElement(prefix3, dataColumn2.EncodedColumnName, dataColumn2.Namespace);
							flag = false;
						}
						Type type = obj2.GetType();
						if (!dataColumn2.IsCustomType)
						{
							if ((type == typeof(char) || type == typeof(string)) && XmlDataTreeWriter.PreserveSpace(obj2))
							{
								this._xmlw.WriteAttributeString("xml", "space", "http://www.w3.org/XML/1998/namespace", "preserve");
							}
							this._xmlw.WriteString(dataColumn2.ConvertObjectToXml(obj2));
						}
						else if (dataColumn2.IsValueCustomTypeInstance(obj2))
						{
							if (!flag && type != dataColumn2.DataType)
							{
								this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", type.AssemblyQualifiedName);
							}
							if (!flag)
							{
								dataColumn2.ConvertObjectToXml(obj2, this._xmlw, null);
							}
							else
							{
								if (obj2.GetType() != dataColumn2.DataType)
								{
									throw ExceptionBuilder.PolymorphismNotSupported(type.AssemblyQualifiedName);
								}
								XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(dataColumn2.EncodedColumnName);
								xmlRootAttribute.Namespace = dataColumn2.Namespace;
								dataColumn2.ConvertObjectToXml(obj2, this._xmlw, xmlRootAttribute);
							}
						}
						else
						{
							if (type == typeof(Type) || type == typeof(Guid) || type == typeof(char) || DataStorage.IsSqlType(type))
							{
								this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", type.FullName);
							}
							else if (obj2 is Type)
							{
								this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", "Type");
							}
							else
							{
								string value = "xs:" + XmlTreeGen.XmlDataTypeName(type);
								this._xmlw.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", value);
								this._xmlw.WriteAttributeString("xmlns:xs", "http://www.w3.org/2001/XMLSchema");
							}
							if (!DataStorage.IsSqlType(type))
							{
								this._xmlw.WriteString(dataColumn2.ConvertObjectToXml(obj2));
							}
							else
							{
								dataColumn2.ConvertObjectToXml(obj2, this._xmlw, null);
							}
						}
						if (dataColumn2.columnMapping != MappingType.SimpleContent && !flag)
						{
							this._xmlw.WriteEndElement();
						}
					}
				}
			}
			if (this._ds != null)
			{
				foreach (object obj4 in this.GetNestedChildRelations(row))
				{
					DataRelation dataRelation = (DataRelation)obj4;
					foreach (DataRow row2 in row.GetChildRows(dataRelation))
					{
						this.XmlDataRowWriter(row2, dataRelation.ChildTable.EncodedTableName);
					}
				}
			}
			this._xmlw.WriteEndElement();
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00221798 File Offset: 0x00220B98
		internal static bool PreserveSpace(object value)
		{
			string text = value.ToString();
			if (text.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!char.IsWhiteSpace(text, i))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000A86 RID: 2694
		private XmlWriter _xmlw;

		// Token: 0x04000A87 RID: 2695
		private DataSet _ds;

		// Token: 0x04000A88 RID: 2696
		private DataTable _dt;

		// Token: 0x04000A89 RID: 2697
		private ArrayList _dTables = new ArrayList();

		// Token: 0x04000A8A RID: 2698
		private DataTable[] topLevelTables;

		// Token: 0x04000A8B RID: 2699
		private bool fFromTable;

		// Token: 0x04000A8C RID: 2700
		private bool isDiffgram;

		// Token: 0x04000A8D RID: 2701
		private Hashtable rowsOrder;

		// Token: 0x04000A8E RID: 2702
		private bool _writeHierarchy;
	}
}

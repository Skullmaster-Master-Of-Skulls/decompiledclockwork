using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x0200013D RID: 317
	internal sealed class NewDiffgramGen
	{
		// Token: 0x06001295 RID: 4757 RVA: 0x000922CC File Offset: 0x000916CC
		internal NewDiffgramGen(DataSet ds)
		{
			this._ds = ds;
			this._dt = null;
			this._doc = new XmlDocument();
			for (int i = 0; i < ds.Tables.Count; i++)
			{
				this._tables.Add(ds.Tables[i]);
			}
			this.DoAssignments(this._tables);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x00092340 File Offset: 0x00091740
		internal NewDiffgramGen(DataTable dt, bool writeHierarchy)
		{
			this._ds = null;
			this._dt = dt;
			this._doc = new XmlDocument();
			this._tables.Add(dt);
			if (writeHierarchy)
			{
				this._writeHierarchy = true;
				this.CreateTableHierarchy(dt);
			}
			this.DoAssignments(this._tables);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x000923A4 File Offset: 0x000917A4
		private void CreateTableHierarchy(DataTable dt)
		{
			foreach (object obj in dt.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!this._tables.Contains(dataRelation.ChildTable))
				{
					this._tables.Add(dataRelation.ChildTable);
					this.CreateTableHierarchy(dataRelation.ChildTable);
				}
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00092434 File Offset: 0x00091834
		private void DoAssignments(ArrayList tables)
		{
			int num = 0;
			for (int i = 0; i < tables.Count; i++)
			{
				num += ((DataTable)tables[i]).Rows.Count;
			}
			this.rowsOrder = new Hashtable(num);
			for (int j = 0; j < tables.Count; j++)
			{
				DataTable dataTable = (DataTable)tables[j];
				DataRowCollection rows = dataTable.Rows;
				num = rows.Count;
				for (int k = 0; k < num; k++)
				{
					this.rowsOrder[rows[k]] = k;
				}
			}
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x000924D0 File Offset: 0x000918D0
		private bool EmptyData()
		{
			for (int i = 0; i < this._tables.Count; i++)
			{
				if (((DataTable)this._tables[i]).Rows.Count > 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00092514 File Offset: 0x00091914
		internal void Save(XmlWriter xmlw)
		{
			this.Save(xmlw, null);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0009252C File Offset: 0x0009192C
		internal void Save(XmlWriter xmlw, DataTable table)
		{
			this._xmlw = DataTextWriter.CreateWriter(xmlw);
			this._xmlw.WriteStartElement("diffgr", "diffgram", "urn:schemas-microsoft-com:xml-diffgram-v1");
			this._xmlw.WriteAttributeString("xmlns", "msdata", null, "urn:schemas-microsoft-com:xml-msdata");
			if (!this.EmptyData())
			{
				if (table != null)
				{
					new XmlDataTreeWriter(table, this._writeHierarchy).SaveDiffgramData(this._xmlw, this.rowsOrder);
				}
				else
				{
					new XmlDataTreeWriter(this._ds).SaveDiffgramData(this._xmlw, this.rowsOrder);
				}
				if (table == null)
				{
					for (int i = 0; i < this._ds.Tables.Count; i++)
					{
						this.GenerateTable(this._ds.Tables[i]);
					}
				}
				else
				{
					for (int j = 0; j < this._tables.Count; j++)
					{
						this.GenerateTable((DataTable)this._tables[j]);
					}
				}
				if (this.fBefore)
				{
					this._xmlw.WriteEndElement();
				}
				if (table == null)
				{
					for (int k = 0; k < this._ds.Tables.Count; k++)
					{
						this.GenerateTableErrors(this._ds.Tables[k]);
					}
				}
				else
				{
					for (int l = 0; l < this._tables.Count; l++)
					{
						this.GenerateTableErrors((DataTable)this._tables[l]);
					}
				}
				if (this.fErrors)
				{
					this._xmlw.WriteEndElement();
				}
			}
			this._xmlw.WriteEndElement();
			this._xmlw.Flush();
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x000926C8 File Offset: 0x00091AC8
		private void GenerateTable(DataTable table)
		{
			int count = table.Rows.Count;
			if (count <= 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				this.GenerateRow(table.Rows[i]);
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00092704 File Offset: 0x00091B04
		private void GenerateTableErrors(DataTable table)
		{
			int count = table.Rows.Count;
			int count2 = table.Columns.Count;
			if (count <= 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				bool flag = false;
				DataRow dataRow = table.Rows[i];
				string prefix = (table.Namespace.Length != 0) ? table.Prefix : string.Empty;
				if (dataRow.HasErrors && dataRow.RowError.Length > 0)
				{
					if (!this.fErrors)
					{
						this._xmlw.WriteStartElement("diffgr", "errors", "urn:schemas-microsoft-com:xml-diffgram-v1");
						this.fErrors = true;
					}
					this._xmlw.WriteStartElement(prefix, dataRow.Table.EncodedTableName, dataRow.Table.Namespace);
					this._xmlw.WriteAttributeString("diffgr", "id", "urn:schemas-microsoft-com:xml-diffgram-v1", dataRow.Table.TableName + dataRow.rowID.ToString(CultureInfo.InvariantCulture));
					this._xmlw.WriteAttributeString("diffgr", "Error", "urn:schemas-microsoft-com:xml-diffgram-v1", dataRow.RowError);
					flag = true;
				}
				if (count2 > 0)
				{
					for (int j = 0; j < count2; j++)
					{
						DataColumn dataColumn = table.Columns[j];
						string columnError = dataRow.GetColumnError(dataColumn);
						string prefix2 = (dataColumn.Namespace.Length != 0) ? dataColumn.Prefix : string.Empty;
						if (columnError != null && columnError.Length != 0)
						{
							if (!flag)
							{
								if (!this.fErrors)
								{
									this._xmlw.WriteStartElement("diffgr", "errors", "urn:schemas-microsoft-com:xml-diffgram-v1");
									this.fErrors = true;
								}
								this._xmlw.WriteStartElement(prefix, dataRow.Table.EncodedTableName, dataRow.Table.Namespace);
								this._xmlw.WriteAttributeString("diffgr", "id", "urn:schemas-microsoft-com:xml-diffgram-v1", dataRow.Table.TableName + dataRow.rowID.ToString(CultureInfo.InvariantCulture));
								flag = true;
							}
							this._xmlw.WriteStartElement(prefix2, dataColumn.EncodedColumnName, dataColumn.Namespace);
							this._xmlw.WriteAttributeString("diffgr", "Error", "urn:schemas-microsoft-com:xml-diffgram-v1", columnError);
							this._xmlw.WriteEndElement();
						}
					}
					if (flag)
					{
						this._xmlw.WriteEndElement();
					}
				}
			}
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0009297C File Offset: 0x00091D7C
		private void GenerateRow(DataRow row)
		{
			DataRowState rowState = row.RowState;
			if (rowState == DataRowState.Unchanged || rowState == DataRowState.Added)
			{
				return;
			}
			if (!this.fBefore)
			{
				this._xmlw.WriteStartElement("diffgr", "before", "urn:schemas-microsoft-com:xml-diffgram-v1");
				this.fBefore = true;
			}
			DataTable table = row.Table;
			int count = table.Columns.Count;
			string value = table.TableName + row.rowID.ToString(CultureInfo.InvariantCulture);
			string text = null;
			if (rowState == DataRowState.Deleted && row.Table.NestedParentRelations.Length != 0)
			{
				DataRow nestedParentRow = row.GetNestedParentRow(DataRowVersion.Original);
				if (nestedParentRow != null)
				{
					text = nestedParentRow.Table.TableName + nestedParentRow.rowID.ToString(CultureInfo.InvariantCulture);
				}
			}
			string prefix = (table.Namespace.Length != 0) ? table.Prefix : string.Empty;
			DBNull dbnull = (table.XmlText == null) ? DBNull.Value : row[table.XmlText, DataRowVersion.Original];
			this._xmlw.WriteStartElement(prefix, row.Table.EncodedTableName, row.Table.Namespace);
			this._xmlw.WriteAttributeString("diffgr", "id", "urn:schemas-microsoft-com:xml-diffgram-v1", value);
			if (rowState == DataRowState.Deleted && XmlDataTreeWriter.RowHasErrors(row))
			{
				this._xmlw.WriteAttributeString("diffgr", "hasErrors", "urn:schemas-microsoft-com:xml-diffgram-v1", "true");
			}
			if (text != null)
			{
				this._xmlw.WriteAttributeString("diffgr", "parentId", "urn:schemas-microsoft-com:xml-diffgram-v1", text);
			}
			this._xmlw.WriteAttributeString("msdata", "rowOrder", "urn:schemas-microsoft-com:xml-msdata", this.rowsOrder[row].ToString());
			for (int i = 0; i < count; i++)
			{
				if (row.Table.Columns[i].ColumnMapping == MappingType.Attribute || row.Table.Columns[i].ColumnMapping == MappingType.Hidden)
				{
					this.GenerateColumn(row, row.Table.Columns[i], DataRowVersion.Original);
				}
			}
			for (int j = 0; j < count; j++)
			{
				if (row.Table.Columns[j].ColumnMapping == MappingType.Element || row.Table.Columns[j].ColumnMapping == MappingType.SimpleContent)
				{
					this.GenerateColumn(row, row.Table.Columns[j], DataRowVersion.Original);
				}
			}
			this._xmlw.WriteEndElement();
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00092BFC File Offset: 0x00091FFC
		private void GenerateColumn(DataRow row, DataColumn col, DataRowVersion version)
		{
			string columnValueAsString = col.GetColumnValueAsString(row, version);
			if (columnValueAsString == null)
			{
				if (col.ColumnMapping == MappingType.SimpleContent)
				{
					this._xmlw.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				}
				return;
			}
			string prefix = (col.Namespace.Length != 0) ? col.Prefix : string.Empty;
			switch (col.ColumnMapping)
			{
			case MappingType.Element:
			{
				bool flag = true;
				object obj = row[col, version];
				if (!col.IsCustomType || !col.IsValueCustomTypeInstance(obj) || typeof(IXmlSerializable).IsAssignableFrom(obj.GetType()))
				{
					this._xmlw.WriteStartElement(prefix, col.EncodedColumnName, col.Namespace);
					flag = false;
				}
				Type type = obj.GetType();
				if (!col.IsCustomType)
				{
					if ((type == typeof(char) || type == typeof(string)) && XmlDataTreeWriter.PreserveSpace(columnValueAsString))
					{
						this._xmlw.WriteAttributeString("xml", "space", "http://www.w3.org/XML/1998/namespace", "preserve");
					}
					this._xmlw.WriteString(columnValueAsString);
				}
				else if (obj != DBNull.Value && (!col.ImplementsINullable || !DataStorage.IsObjectSqlNull(obj)))
				{
					if (col.IsValueCustomTypeInstance(obj))
					{
						if (!flag && obj.GetType() != col.DataType)
						{
							this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", DataStorage.GetQualifiedName(type));
						}
						if (!flag)
						{
							col.ConvertObjectToXml(obj, this._xmlw, null);
						}
						else
						{
							if (obj.GetType() != col.DataType)
							{
								throw ExceptionBuilder.PolymorphismNotSupported(type.AssemblyQualifiedName);
							}
							XmlRootAttribute xmlRootAttribute = new XmlRootAttribute(col.EncodedColumnName);
							xmlRootAttribute.Namespace = col.Namespace;
							col.ConvertObjectToXml(obj, this._xmlw, xmlRootAttribute);
						}
					}
					else
					{
						if (type == typeof(Type) || type == typeof(Guid) || type == typeof(char) || DataStorage.IsSqlType(type))
						{
							this._xmlw.WriteAttributeString("msdata", "InstanceType", "urn:schemas-microsoft-com:xml-msdata", type.FullName);
						}
						else if (obj is Type)
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
							this._xmlw.WriteString(col.ConvertObjectToXml(obj));
						}
						else
						{
							col.ConvertObjectToXml(obj, this._xmlw, null);
						}
					}
				}
				if (!flag)
				{
					this._xmlw.WriteEndElement();
				}
				return;
			}
			case MappingType.Attribute:
				this._xmlw.WriteAttributeString(prefix, col.EncodedColumnName, col.Namespace, columnValueAsString);
				return;
			case MappingType.SimpleContent:
				this._xmlw.WriteString(columnValueAsString);
				return;
			case MappingType.Hidden:
				this._xmlw.WriteAttributeString("msdata", "hidden" + col.EncodedColumnName, "urn:schemas-microsoft-com:xml-msdata", columnValueAsString);
				return;
			default:
				return;
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00092F50 File Offset: 0x00092350
		internal static string QualifiedName(string prefix, string name)
		{
			if (prefix != null)
			{
				return prefix + ":" + name;
			}
			return name;
		}

		// Token: 0x04000758 RID: 1880
		internal XmlDocument _doc;

		// Token: 0x04000759 RID: 1881
		internal DataSet _ds;

		// Token: 0x0400075A RID: 1882
		internal DataTable _dt;

		// Token: 0x0400075B RID: 1883
		internal XmlWriter _xmlw;

		// Token: 0x0400075C RID: 1884
		private bool fBefore;

		// Token: 0x0400075D RID: 1885
		private bool fErrors;

		// Token: 0x0400075E RID: 1886
		internal Hashtable rowsOrder;

		// Token: 0x0400075F RID: 1887
		private ArrayList _tables = new ArrayList();

		// Token: 0x04000760 RID: 1888
		private bool _writeHierarchy;
	}
}

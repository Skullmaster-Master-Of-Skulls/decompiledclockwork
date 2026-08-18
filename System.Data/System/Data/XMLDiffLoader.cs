using System;
using System.Collections;
using System.Data.Common;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x020000F1 RID: 241
	internal sealed class XMLDiffLoader
	{
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0021ACD8 File Offset: 0x0021A0D8
		internal void LoadDiffGram(DataSet ds, XmlReader dataTextReader)
		{
			XmlReader xmlReader = DataTextReader.CreateReader(dataTextReader);
			this.dataSet = ds;
			while (xmlReader.LocalName == "before")
			{
				if (!(xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					break;
				}
				this.ProcessDiffs(ds, xmlReader);
				xmlReader.Read();
			}
			while (xmlReader.LocalName == "errors" && xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
			{
				this.ProcessErrors(ds, xmlReader);
				xmlReader.Read();
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0021AD68 File Offset: 0x0021A168
		private void CreateTablesHierarchy(DataTable dt)
		{
			foreach (object obj in dt.ChildRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (!this.tables.Contains(dataRelation.ChildTable))
				{
					this.tables.Add(dataRelation.ChildTable);
					this.CreateTablesHierarchy(dataRelation.ChildTable);
				}
			}
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0021ADF8 File Offset: 0x0021A1F8
		internal void LoadDiffGram(DataTable dt, XmlReader dataTextReader)
		{
			XmlReader xmlReader = DataTextReader.CreateReader(dataTextReader);
			this.dataTable = dt;
			this.tables = new ArrayList();
			this.tables.Add(dt);
			this.CreateTablesHierarchy(dt);
			while (xmlReader.LocalName == "before")
			{
				if (!(xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1"))
				{
					break;
				}
				this.ProcessDiffs(this.tables, xmlReader);
				xmlReader.Read();
			}
			while (xmlReader.LocalName == "errors" && xmlReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
			{
				this.ProcessErrors(this.tables, xmlReader);
				xmlReader.Read();
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0021AEA8 File Offset: 0x0021A2A8
		internal void ProcessDiffs(DataSet ds, XmlReader ssync)
		{
			int pos = -1;
			int i = ssync.Depth;
			ssync.Read();
			this.SkipWhitespaces(ssync);
			while (i < ssync.Depth)
			{
				DataTable dataTable = null;
				int depth = ssync.Depth;
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				bool flag = ssync.GetAttribute("hasErrors", "urn:schemas-microsoft-com:xml-diffgram-v1") == "true";
				int num = this.ReadOldRowData(ds, ref dataTable, ref pos, ssync);
				if (num != -1)
				{
					if (dataTable == null)
					{
						throw ExceptionBuilder.DiffgramMissingSQL();
					}
					DataRow dataRow = (DataRow)dataTable.RowDiffId[attribute];
					if (dataRow != null)
					{
						dataRow.oldRecord = num;
						dataTable.recordManager[num] = dataRow;
					}
					else
					{
						dataRow = dataTable.NewEmptyRow();
						dataTable.recordManager[num] = dataRow;
						dataRow.oldRecord = num;
						dataRow.newRecord = num;
						dataTable.Rows.DiffInsertAt(dataRow, pos);
						dataRow.Delete();
						if (flag)
						{
							dataTable.RowDiffId[attribute] = dataRow;
						}
					}
				}
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0021AFB8 File Offset: 0x0021A3B8
		internal void ProcessDiffs(ArrayList tableList, XmlReader ssync)
		{
			int pos = -1;
			int i = ssync.Depth;
			ssync.Read();
			while (i < ssync.Depth)
			{
				DataTable dataTable = null;
				int depth = ssync.Depth;
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				bool flag = ssync.GetAttribute("hasErrors", "urn:schemas-microsoft-com:xml-diffgram-v1") == "true";
				int num = this.ReadOldRowData(this.dataSet, ref dataTable, ref pos, ssync);
				if (num != -1)
				{
					if (dataTable == null)
					{
						throw ExceptionBuilder.DiffgramMissingSQL();
					}
					DataRow dataRow = (DataRow)dataTable.RowDiffId[attribute];
					if (dataRow != null)
					{
						dataRow.oldRecord = num;
						dataTable.recordManager[num] = dataRow;
					}
					else
					{
						dataRow = dataTable.NewEmptyRow();
						dataTable.recordManager[num] = dataRow;
						dataRow.oldRecord = num;
						dataRow.newRecord = num;
						dataTable.Rows.DiffInsertAt(dataRow, pos);
						dataRow.Delete();
						if (flag)
						{
							dataTable.RowDiffId[attribute] = dataRow;
						}
					}
				}
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0021B0B8 File Offset: 0x0021A4B8
		internal void ProcessErrors(DataSet ds, XmlReader ssync)
		{
			int i = ssync.Depth;
			ssync.Read();
			while (i < ssync.Depth)
			{
				DataTable table = ds.Tables.GetTable(XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI);
				if (table == null)
				{
					throw ExceptionBuilder.DiffgramMissingSQL();
				}
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				DataRow dataRow = (DataRow)table.RowDiffId[attribute];
				string attribute2 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
				if (attribute2 != null)
				{
					dataRow.RowError = attribute2;
				}
				int j = ssync.Depth;
				ssync.Read();
				while (j < ssync.Depth)
				{
					if (XmlNodeType.Element == ssync.NodeType)
					{
						DataColumn column = table.Columns[XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI];
						string attribute3 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
						dataRow.SetColumnError(column, attribute3);
					}
					ssync.Read();
				}
				while (ssync.NodeType == XmlNodeType.EndElement && i < ssync.Depth)
				{
					ssync.Read();
				}
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0021B1D8 File Offset: 0x0021A5D8
		internal void ProcessErrors(ArrayList dt, XmlReader ssync)
		{
			int i = ssync.Depth;
			ssync.Read();
			while (i < ssync.Depth)
			{
				DataTable table = this.GetTable(XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI);
				if (table == null)
				{
					throw ExceptionBuilder.DiffgramMissingSQL();
				}
				string attribute = ssync.GetAttribute("id", "urn:schemas-microsoft-com:xml-diffgram-v1");
				DataRow dataRow = (DataRow)table.RowDiffId[attribute];
				if (dataRow == null)
				{
					for (int j = 0; j < dt.Count; j++)
					{
						dataRow = (DataRow)((DataTable)dt[j]).RowDiffId[attribute];
						if (dataRow != null)
						{
							table = dataRow.Table;
							break;
						}
					}
				}
				string attribute2 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
				if (attribute2 != null)
				{
					dataRow.RowError = attribute2;
				}
				int k = ssync.Depth;
				ssync.Read();
				while (k < ssync.Depth)
				{
					if (XmlNodeType.Element == ssync.NodeType)
					{
						DataColumn column = table.Columns[XmlConvert.DecodeName(ssync.LocalName), ssync.NamespaceURI];
						string attribute3 = ssync.GetAttribute("Error", "urn:schemas-microsoft-com:xml-diffgram-v1");
						dataRow.SetColumnError(column, attribute3);
					}
					ssync.Read();
				}
				while (ssync.NodeType == XmlNodeType.EndElement && i < ssync.Depth)
				{
					ssync.Read();
				}
			}
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0021B328 File Offset: 0x0021A728
		private DataTable GetTable(string tableName, string ns)
		{
			if (this.tables == null)
			{
				return this.dataSet.Tables.GetTable(tableName, ns);
			}
			if (this.tables.Count == 0)
			{
				return (DataTable)this.tables[0];
			}
			for (int i = 0; i < this.tables.Count; i++)
			{
				DataTable dataTable = (DataTable)this.tables[i];
				if (string.Compare(dataTable.TableName, tableName, StringComparison.Ordinal) == 0 && string.Compare(dataTable.Namespace, ns, StringComparison.Ordinal) == 0)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0021B3B8 File Offset: 0x0021A7B8
		private int ReadOldRowData(DataSet ds, ref DataTable table, ref int pos, XmlReader row)
		{
			if (ds != null)
			{
				table = ds.Tables.GetTable(XmlConvert.DecodeName(row.LocalName), row.NamespaceURI);
			}
			else
			{
				table = this.GetTable(XmlConvert.DecodeName(row.LocalName), row.NamespaceURI);
			}
			if (table == null)
			{
				row.Skip();
				return -1;
			}
			int depth = row.Depth;
			if (table == null)
			{
				throw ExceptionBuilder.DiffgramMissingTable(XmlConvert.DecodeName(row.LocalName));
			}
			string attribute = row.GetAttribute("rowOrder", "urn:schemas-microsoft-com:xml-msdata");
			if (!ADP.IsEmpty(attribute))
			{
				pos = (int)Convert.ChangeType(attribute, typeof(int), null);
			}
			int num = table.NewRecord();
			foreach (object obj in table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dataColumn[num] = DBNull.Value;
			}
			foreach (object obj2 in table.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				if (dataColumn2.ColumnMapping != MappingType.Element && dataColumn2.ColumnMapping != MappingType.SimpleContent)
				{
					if (dataColumn2.ColumnMapping == MappingType.Hidden)
					{
						attribute = row.GetAttribute("hidden" + dataColumn2.EncodedColumnName, "urn:schemas-microsoft-com:xml-msdata");
					}
					else
					{
						attribute = row.GetAttribute(dataColumn2.EncodedColumnName, dataColumn2.Namespace);
					}
					if (attribute != null)
					{
						dataColumn2[num] = dataColumn2.ConvertXmlToObject(attribute);
					}
				}
			}
			row.Read();
			this.SkipWhitespaces(row);
			int depth2 = row.Depth;
			if (depth2 <= depth)
			{
				if (depth2 == depth && row.NodeType == XmlNodeType.EndElement)
				{
					row.Read();
					this.SkipWhitespaces(row);
				}
				return num;
			}
			if (table.XmlText != null)
			{
				DataColumn xmlText = table.XmlText;
				xmlText[num] = xmlText.ConvertXmlToObject(row.ReadString());
			}
			else
			{
				while (row.Depth > depth)
				{
					string text = XmlConvert.DecodeName(row.LocalName);
					string namespaceURI = row.NamespaceURI;
					DataColumn dataColumn3 = table.Columns[text, namespaceURI];
					if (dataColumn3 == null)
					{
						while (row.NodeType != XmlNodeType.EndElement && row.LocalName != text && row.NamespaceURI != namespaceURI)
						{
							row.Read();
						}
						row.Read();
					}
					else if (dataColumn3.IsCustomType)
					{
						bool flag = dataColumn3.DataType == typeof(object) || row.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata") != null || row.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance") != null;
						bool flag2 = false;
						if (dataColumn3.Table.DataSet != null && dataColumn3.Table.DataSet.UdtIsWrapped)
						{
							row.Read();
							flag2 = true;
						}
						XmlRootAttribute xmlRootAttribute = null;
						if (!flag && !dataColumn3.ImplementsIXMLSerializable)
						{
							if (flag2)
							{
								xmlRootAttribute = new XmlRootAttribute(row.LocalName);
								xmlRootAttribute.Namespace = row.NamespaceURI;
							}
							else
							{
								xmlRootAttribute = new XmlRootAttribute(dataColumn3.EncodedColumnName);
								xmlRootAttribute.Namespace = dataColumn3.Namespace;
							}
						}
						dataColumn3[num] = dataColumn3.ConvertXmlToObject(row, xmlRootAttribute);
						if (flag2)
						{
							row.Read();
						}
					}
					else
					{
						int depth3 = row.Depth;
						row.Read();
						if (row.Depth > depth3)
						{
							if (row.NodeType == XmlNodeType.Text || row.NodeType == XmlNodeType.Whitespace || row.NodeType == XmlNodeType.SignificantWhitespace)
							{
								string s = row.ReadString();
								dataColumn3[num] = dataColumn3.ConvertXmlToObject(s);
								row.Read();
							}
						}
						else if (dataColumn3.DataType == typeof(string))
						{
							dataColumn3[num] = string.Empty;
						}
					}
				}
			}
			row.Read();
			this.SkipWhitespaces(row);
			return num;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0021B7F8 File Offset: 0x0021ABF8
		internal void SkipWhitespaces(XmlReader reader)
		{
			while (reader.NodeType == XmlNodeType.Whitespace || reader.NodeType == XmlNodeType.SignificantWhitespace)
			{
				reader.Read();
			}
		}

		// Token: 0x0400099F RID: 2463
		private ArrayList tables;

		// Token: 0x040009A0 RID: 2464
		private DataSet dataSet;

		// Token: 0x040009A1 RID: 2465
		private DataTable dataTable;
	}
}

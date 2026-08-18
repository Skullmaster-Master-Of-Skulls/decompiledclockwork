using System;
using System.Collections;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data
{
	// Token: 0x02000137 RID: 311
	internal sealed class XmlDataLoader
	{
		// Token: 0x0600123B RID: 4667 RVA: 0x0008B860 File Offset: 0x0008AC60
		internal XmlDataLoader(DataSet dataset, bool IsXdr, bool ignoreSchema)
		{
			this.dataSet = dataset;
			this.nodeToRowMap = new Hashtable();
			this.fIsXdr = IsXdr;
			this.ignoreSchema = ignoreSchema;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0008B894 File Offset: 0x0008AC94
		internal XmlDataLoader(DataSet dataset, bool IsXdr, XmlElement topNode, bool ignoreSchema)
		{
			this.dataSet = dataset;
			this.nodeToRowMap = new Hashtable();
			this.fIsXdr = IsXdr;
			this.childRowsStack = new Stack(50);
			this.topMostNode = topNode;
			this.ignoreSchema = ignoreSchema;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0008B8DC File Offset: 0x0008ACDC
		internal XmlDataLoader(DataTable datatable, bool IsXdr, bool ignoreSchema)
		{
			this.dataSet = null;
			this.dataTable = datatable;
			this.isTableLevel = true;
			this.nodeToRowMap = new Hashtable();
			this.fIsXdr = IsXdr;
			this.ignoreSchema = ignoreSchema;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0008B920 File Offset: 0x0008AD20
		internal XmlDataLoader(DataTable datatable, bool IsXdr, XmlElement topNode, bool ignoreSchema)
		{
			this.dataSet = null;
			this.dataTable = datatable;
			this.isTableLevel = true;
			this.nodeToRowMap = new Hashtable();
			this.fIsXdr = IsXdr;
			this.childRowsStack = new Stack(50);
			this.topMostNode = topNode;
			this.ignoreSchema = ignoreSchema;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0008B978 File Offset: 0x0008AD78
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x0008B98C File Offset: 0x0008AD8C
		internal bool FromInference
		{
			get
			{
				return this.fromInference;
			}
			set
			{
				this.fromInference = value;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0008B9A0 File Offset: 0x0008ADA0
		private void AttachRows(DataRow parentRow, XmlNode parentElement)
		{
			if (parentElement == null)
			{
				return;
			}
			for (XmlNode xmlNode = parentElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement e = (XmlElement)xmlNode;
					DataRow rowFromElement = this.GetRowFromElement(e);
					if (rowFromElement != null && rowFromElement.RowState == DataRowState.Detached)
					{
						if (parentRow != null)
						{
							rowFromElement.SetNestedParentRow(parentRow, false);
						}
						rowFromElement.Table.Rows.Add(rowFromElement);
					}
					else if (rowFromElement == null)
					{
						this.AttachRows(parentRow, xmlNode);
					}
					this.AttachRows(rowFromElement, xmlNode);
				}
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0008BA1C File Offset: 0x0008AE1C
		private int CountNonNSAttributes(XmlNode node)
		{
			int num = 0;
			for (int i = 0; i < node.Attributes.Count; i++)
			{
				XmlAttribute xmlAttribute = node.Attributes[i];
				if (!this.FExcludedNamespace(node.Attributes[i].NamespaceURI))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x0008BA6C File Offset: 0x0008AE6C
		private string GetValueForTextOnlyColums(XmlNode n)
		{
			string text = null;
			while (n != null && (n.NodeType == XmlNodeType.Whitespace || !this.IsTextLikeNode(n.NodeType)))
			{
				n = n.NextSibling;
			}
			if (n != null)
			{
				if (this.IsTextLikeNode(n.NodeType) && (n.NextSibling == null || !this.IsTextLikeNode(n.NodeType)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && this.IsTextLikeNode(n.NodeType))
					{
						stringBuilder.Append(n.Value);
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0008BB18 File Offset: 0x0008AF18
		private string GetInitialTextFromNodes(ref XmlNode n)
		{
			string text = null;
			if (n != null)
			{
				while (n.NodeType == XmlNodeType.Whitespace)
				{
					n = n.NextSibling;
				}
				if (this.IsTextLikeNode(n.NodeType) && (n.NextSibling == null || !this.IsTextLikeNode(n.NodeType)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && this.IsTextLikeNode(n.NodeType))
					{
						stringBuilder.Append(n.Value);
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0008BBC4 File Offset: 0x0008AFC4
		private DataColumn GetTextOnlyColumn(DataRow row)
		{
			DataColumnCollection columns = row.Table.Columns;
			int count = columns.Count;
			for (int i = 0; i < count; i++)
			{
				DataColumn dataColumn = columns[i];
				if (this.IsTextOnly(dataColumn))
				{
					return dataColumn;
				}
			}
			return null;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0008BC04 File Offset: 0x0008B004
		internal DataRow GetRowFromElement(XmlElement e)
		{
			return (DataRow)this.nodeToRowMap[e];
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0008BC24 File Offset: 0x0008B024
		internal bool FColumnElement(XmlElement e)
		{
			if (this.nodeToSchemaMap.GetColumnSchema(e, this.FIgnoreNamespace(e)) == null)
			{
				return false;
			}
			if (this.CountNonNSAttributes(e) > 0)
			{
				return false;
			}
			for (XmlNode xmlNode = e.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode is XmlElement)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0008BC74 File Offset: 0x0008B074
		private bool FExcludedNamespace(string ns)
		{
			return ns.Equals("http://www.w3.org/2000/xmlns/") || (this.htableExcludedNS != null && this.htableExcludedNS.Contains(ns));
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0008BCA8 File Offset: 0x0008B0A8
		private bool FIgnoreNamespace(XmlNode node)
		{
			if (!this.fIsXdr)
			{
				return false;
			}
			XmlNode xmlNode;
			if (node is XmlAttribute)
			{
				xmlNode = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				xmlNode = node;
			}
			return xmlNode.NamespaceURI.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0008BCF0 File Offset: 0x0008B0F0
		private bool FIgnoreNamespace(XmlReader node)
		{
			return this.fIsXdr && node.NamespaceURI.StartsWith("x-schema:#", StringComparison.Ordinal);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0008BD1C File Offset: 0x0008B11C
		internal bool IsTextLikeNode(XmlNodeType n)
		{
			if (n - XmlNodeType.Text > 1)
			{
				if (n == XmlNodeType.EntityReference)
				{
					throw ExceptionBuilder.FoundEntity();
				}
				if (n - XmlNodeType.Whitespace > 1)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0008BD48 File Offset: 0x0008B148
		internal bool IsTextOnly(DataColumn c)
		{
			return c.ColumnMapping == MappingType.SimpleContent;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0008BD64 File Offset: 0x0008B164
		internal void LoadData(XmlDocument xdoc)
		{
			if (xdoc.DocumentElement == null)
			{
				return;
			}
			bool enforceConstraints;
			if (this.isTableLevel)
			{
				enforceConstraints = this.dataTable.EnforceConstraints;
				this.dataTable.EnforceConstraints = false;
			}
			else
			{
				enforceConstraints = this.dataSet.EnforceConstraints;
				this.dataSet.EnforceConstraints = false;
				this.dataSet.fInReadXml = true;
			}
			if (this.isTableLevel)
			{
				this.nodeToSchemaMap = new XmlToDatasetMap(this.dataTable, xdoc.NameTable);
			}
			else
			{
				this.nodeToSchemaMap = new XmlToDatasetMap(this.dataSet, xdoc.NameTable);
			}
			DataRow dataRow = null;
			if (this.isTableLevel || (this.dataSet != null && this.dataSet.fTopLevelTable))
			{
				XmlElement documentElement = xdoc.DocumentElement;
				DataTable dataTable = (DataTable)this.nodeToSchemaMap.GetSchemaForNode(documentElement, this.FIgnoreNamespace(documentElement));
				if (dataTable != null)
				{
					dataRow = dataTable.CreateEmptyRow();
					this.nodeToRowMap[documentElement] = dataRow;
					this.LoadRowData(dataRow, documentElement);
					dataTable.Rows.Add(dataRow);
				}
			}
			this.LoadRows(dataRow, xdoc.DocumentElement);
			this.AttachRows(dataRow, xdoc.DocumentElement);
			if (this.isTableLevel)
			{
				this.dataTable.EnforceConstraints = enforceConstraints;
				return;
			}
			this.dataSet.fInReadXml = false;
			this.dataSet.EnforceConstraints = enforceConstraints;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0008BEAC File Offset: 0x0008B2AC
		private void LoadRowData(DataRow row, XmlElement rowElement)
		{
			DataTable table = row.Table;
			if (this.FromInference)
			{
				table.Prefix = rowElement.Prefix;
			}
			Hashtable hashtable = new Hashtable();
			row.BeginEdit();
			XmlNode xmlNode = rowElement.FirstChild;
			DataColumn textOnlyColumn = this.GetTextOnlyColumn(row);
			if (textOnlyColumn != null)
			{
				hashtable[textOnlyColumn] = textOnlyColumn;
				string valueForTextOnlyColums = this.GetValueForTextOnlyColums(xmlNode);
				if (XMLSchema.GetBooleanAttribute(rowElement, "nil", "http://www.w3.org/2001/XMLSchema-instance", false) && ADP.IsEmpty(valueForTextOnlyColums))
				{
					row[textOnlyColumn] = DBNull.Value;
				}
				else
				{
					this.SetRowValueFromXmlText(row, textOnlyColumn, valueForTextOnlyColums);
				}
			}
			while (xmlNode != null && xmlNode != rowElement)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					object obj = this.nodeToSchemaMap.GetSchemaForNode(xmlElement, this.FIgnoreNamespace(xmlElement));
					if (obj is DataTable && this.FColumnElement(xmlElement))
					{
						obj = this.nodeToSchemaMap.GetColumnSchema(xmlElement, this.FIgnoreNamespace(xmlElement));
					}
					if (obj == null || obj is DataColumn)
					{
						xmlNode = xmlElement.FirstChild;
						if (obj != null && obj is DataColumn)
						{
							DataColumn dataColumn = (DataColumn)obj;
							if (dataColumn.Table == row.Table && dataColumn.ColumnMapping != MappingType.Attribute && hashtable[dataColumn] == null)
							{
								hashtable[dataColumn] = dataColumn;
								string valueForTextOnlyColums2 = this.GetValueForTextOnlyColums(xmlNode);
								if (XMLSchema.GetBooleanAttribute(xmlElement, "nil", "http://www.w3.org/2001/XMLSchema-instance", false) && ADP.IsEmpty(valueForTextOnlyColums2))
								{
									row[dataColumn] = DBNull.Value;
								}
								else
								{
									this.SetRowValueFromXmlText(row, dataColumn, valueForTextOnlyColums2);
								}
							}
						}
						else if (obj == null && xmlNode != null)
						{
							continue;
						}
						if (xmlNode == null)
						{
							xmlNode = xmlElement;
						}
					}
				}
				while (xmlNode != rowElement && xmlNode.NextSibling == null)
				{
					xmlNode = xmlNode.ParentNode;
				}
				if (xmlNode != rowElement)
				{
					xmlNode = xmlNode.NextSibling;
				}
			}
			foreach (object obj2 in rowElement.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj2;
				object columnSchema = this.nodeToSchemaMap.GetColumnSchema(xmlAttribute, this.FIgnoreNamespace(xmlAttribute));
				if (columnSchema != null && columnSchema is DataColumn)
				{
					DataColumn dataColumn2 = (DataColumn)columnSchema;
					if (dataColumn2.ColumnMapping == MappingType.Attribute && hashtable[dataColumn2] == null)
					{
						hashtable[dataColumn2] = dataColumn2;
						xmlNode = xmlAttribute.FirstChild;
						this.SetRowValueFromXmlText(row, dataColumn2, this.GetInitialTextFromNodes(ref xmlNode));
					}
				}
			}
			foreach (object obj3 in row.Table.Columns)
			{
				DataColumn dataColumn3 = (DataColumn)obj3;
				if (hashtable[dataColumn3] == null && XmlToDatasetMap.IsMappedColumn(dataColumn3))
				{
					if (!dataColumn3.AutoIncrement)
					{
						if (dataColumn3.AllowDBNull)
						{
							row[dataColumn3] = DBNull.Value;
						}
						else
						{
							row[dataColumn3] = dataColumn3.DefaultValue;
						}
					}
					else
					{
						dataColumn3.Init(row.tempRecord);
					}
				}
			}
			row.EndEdit();
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0008C1D8 File Offset: 0x0008B5D8
		private void LoadRows(DataRow parentRow, XmlNode parentElement)
		{
			if (parentElement == null)
			{
				return;
			}
			if ((parentElement.LocalName == "schema" && parentElement.NamespaceURI == "http://www.w3.org/2001/XMLSchema") || (parentElement.LocalName == "sync" && parentElement.NamespaceURI == "urn:schemas-microsoft-com:xml-updategram") || (parentElement.LocalName == "Schema" && parentElement.NamespaceURI == "urn:schemas-microsoft-com:xml-data"))
			{
				return;
			}
			for (XmlNode xmlNode = parentElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode is XmlElement)
				{
					XmlElement xmlElement = (XmlElement)xmlNode;
					object schemaForNode = this.nodeToSchemaMap.GetSchemaForNode(xmlElement, this.FIgnoreNamespace(xmlElement));
					if (schemaForNode != null && schemaForNode is DataTable)
					{
						DataRow dataRow = this.GetRowFromElement(xmlElement);
						if (dataRow == null)
						{
							if (parentRow != null && this.FColumnElement(xmlElement))
							{
								goto IL_F5;
							}
							dataRow = ((DataTable)schemaForNode).CreateEmptyRow();
							this.nodeToRowMap[xmlElement] = dataRow;
							this.LoadRowData(dataRow, xmlElement);
						}
						this.LoadRows(dataRow, xmlNode);
					}
					else
					{
						this.LoadRows(null, xmlNode);
					}
				}
				IL_F5:;
			}
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0008C2E8 File Offset: 0x0008B6E8
		private void SetRowValueFromXmlText(DataRow row, DataColumn col, string xmlText)
		{
			row[col] = col.ConvertXmlToObject(xmlText);
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0008C304 File Offset: 0x0008B704
		internal void LoadTopMostRow(ref bool[] foundColumns)
		{
			object schemaForNode = this.nodeToSchemaMap.GetSchemaForNode(this.topMostNode, this.FIgnoreNamespace(this.topMostNode));
			if (schemaForNode is DataTable)
			{
				DataTable dataTable = (DataTable)schemaForNode;
				this.topMostRow = dataTable.CreateEmptyRow();
				foundColumns = new bool[this.topMostRow.Table.Columns.Count];
				foreach (object obj in this.topMostNode.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					object columnSchema = this.nodeToSchemaMap.GetColumnSchema(xmlAttribute, this.FIgnoreNamespace(xmlAttribute));
					if (columnSchema != null && columnSchema is DataColumn)
					{
						DataColumn dataColumn = (DataColumn)columnSchema;
						if (dataColumn.ColumnMapping == MappingType.Attribute)
						{
							XmlNode firstChild = xmlAttribute.FirstChild;
							this.SetRowValueFromXmlText(this.topMostRow, dataColumn, this.GetInitialTextFromNodes(ref firstChild));
							foundColumns[dataColumn.Ordinal] = true;
						}
					}
				}
			}
			this.topMostNode = null;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0008C424 File Offset: 0x0008B824
		private void InitNameTable()
		{
			XmlNameTable nameTable = this.dataReader.NameTable;
			this.XSD_XMLNS_NS = nameTable.Add("http://www.w3.org/2000/xmlns/");
			this.XDR_SCHEMA = nameTable.Add("Schema");
			this.XDRNS = nameTable.Add("urn:schemas-microsoft-com:xml-data");
			this.SQL_SYNC = nameTable.Add("sync");
			this.UPDGNS = nameTable.Add("urn:schemas-microsoft-com:xml-updategram");
			this.XSD_SCHEMA = nameTable.Add("schema");
			this.XSDNS = nameTable.Add("http://www.w3.org/2001/XMLSchema");
			this.DFFNS = nameTable.Add("urn:schemas-microsoft-com:xml-diffgram-v1");
			this.MSDNS = nameTable.Add("urn:schemas-microsoft-com:xml-msdata");
			this.DIFFID = nameTable.Add("id");
			this.HASCHANGES = nameTable.Add("hasChanges");
			this.ROWORDER = nameTable.Add("rowOrder");
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0008C50C File Offset: 0x0008B90C
		internal void LoadData(XmlReader reader)
		{
			this.dataReader = DataTextReader.CreateReader(reader);
			int depth = this.dataReader.Depth;
			bool enforceConstraints = this.isTableLevel ? this.dataTable.EnforceConstraints : this.dataSet.EnforceConstraints;
			this.InitNameTable();
			if (this.nodeToSchemaMap == null)
			{
				this.nodeToSchemaMap = (this.isTableLevel ? new XmlToDatasetMap(this.dataReader.NameTable, this.dataTable) : new XmlToDatasetMap(this.dataReader.NameTable, this.dataSet));
			}
			if (this.isTableLevel)
			{
				this.dataTable.EnforceConstraints = false;
			}
			else
			{
				this.dataSet.EnforceConstraints = false;
				this.dataSet.fInReadXml = true;
			}
			if (this.topMostNode != null)
			{
				if (!this.isDiffgram && !this.isTableLevel)
				{
					DataTable dataTable = this.nodeToSchemaMap.GetSchemaForNode(this.topMostNode, this.FIgnoreNamespace(this.topMostNode)) as DataTable;
					if (dataTable != null)
					{
						this.LoadTopMostTable(dataTable);
					}
				}
				this.topMostNode = null;
			}
			while (!this.dataReader.EOF && this.dataReader.Depth >= depth)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					this.dataReader.Read();
				}
				else
				{
					DataTable tableForNode = this.nodeToSchemaMap.GetTableForNode(this.dataReader, this.FIgnoreNamespace(this.dataReader));
					if (tableForNode == null)
					{
						if (!this.ProcessXsdSchema())
						{
							this.dataReader.Read();
						}
					}
					else
					{
						this.LoadTable(tableForNode, false);
					}
				}
			}
			if (this.isTableLevel)
			{
				this.dataTable.EnforceConstraints = enforceConstraints;
				return;
			}
			this.dataSet.fInReadXml = false;
			this.dataSet.EnforceConstraints = enforceConstraints;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0008C6BC File Offset: 0x0008BABC
		private void LoadTopMostTable(DataTable table)
		{
			bool flag = this.isTableLevel || this.dataSet.DataSetName != table.TableName;
			bool flag2 = false;
			int num = this.dataReader.Depth - 1;
			int i = this.childRowsStack.Count;
			DataColumnCollection columns = table.Columns;
			object[] array = new object[columns.Count];
			DataColumn dataColumn;
			using (IEnumerator enumerator = this.topMostNode.Attributes.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					dataColumn = (this.nodeToSchemaMap.GetColumnSchema(xmlAttribute, this.FIgnoreNamespace(xmlAttribute)) as DataColumn);
					if (dataColumn != null && dataColumn.ColumnMapping == MappingType.Attribute)
					{
						XmlNode firstChild = xmlAttribute.FirstChild;
						array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(this.GetInitialTextFromNodes(ref firstChild));
						flag2 = true;
					}
				}
				goto IL_1F6;
			}
			IL_E8:
			XmlNodeType nodeType = this.dataReader.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
			{
				object columnSchema = this.nodeToSchemaMap.GetColumnSchema(table, this.dataReader, this.FIgnoreNamespace(this.dataReader));
				dataColumn = (columnSchema as DataColumn);
				if (dataColumn != null)
				{
					if (array[dataColumn.Ordinal] == null)
					{
						this.LoadColumn(dataColumn, array);
						flag2 = true;
						goto IL_1F6;
					}
					this.dataReader.Read();
					goto IL_1F6;
				}
				else
				{
					DataTable dataTable = columnSchema as DataTable;
					if (dataTable != null)
					{
						this.LoadTable(dataTable, true);
						flag2 = true;
						goto IL_1F6;
					}
					if (this.ProcessXsdSchema())
					{
						goto IL_1F6;
					}
					if (!flag2 && !flag)
					{
						return;
					}
					this.dataReader.Read();
					goto IL_1F6;
				}
				break;
			}
			case XmlNodeType.Attribute:
				goto IL_1EA;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			case XmlNodeType.EntityReference:
				throw ExceptionBuilder.FoundEntity();
			default:
				if (nodeType - XmlNodeType.Whitespace > 1)
				{
					goto IL_1EA;
				}
				break;
			}
			string s = this.dataReader.ReadString();
			dataColumn = table.xmlText;
			if (dataColumn != null && array[dataColumn.Ordinal] == null)
			{
				array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(s);
				goto IL_1F6;
			}
			goto IL_1F6;
			IL_1EA:
			this.dataReader.Read();
			IL_1F6:
			if (num >= this.dataReader.Depth)
			{
				this.dataReader.Read();
				for (int j = array.Length - 1; j >= 0; j--)
				{
					if (array[j] == null)
					{
						dataColumn = columns[j];
						if (dataColumn.AllowDBNull && dataColumn.ColumnMapping != MappingType.Hidden && !dataColumn.AutoIncrement)
						{
							array[j] = DBNull.Value;
						}
					}
				}
				DataRow parentRow = table.Rows.AddWithColumnEvents(array);
				while (i < this.childRowsStack.Count)
				{
					DataRow dataRow = (DataRow)this.childRowsStack.Pop();
					bool flag3 = dataRow.RowState == DataRowState.Unchanged;
					dataRow.SetNestedParentRow(parentRow, false);
					if (flag3)
					{
						dataRow.oldRecord = dataRow.newRecord;
					}
				}
				return;
			}
			goto IL_E8;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0008C98C File Offset: 0x0008BD8C
		private void LoadTable(DataTable table, bool isNested)
		{
			int i = this.dataReader.Depth;
			int j = this.childRowsStack.Count;
			DataColumnCollection columns = table.Columns;
			object[] array = new object[columns.Count];
			int pos = -1;
			string key = string.Empty;
			string text = null;
			bool flag = false;
			for (int k = this.dataReader.AttributeCount - 1; k >= 0; k--)
			{
				this.dataReader.MoveToAttribute(k);
				DataColumn dataColumn = this.nodeToSchemaMap.GetColumnSchema(table, this.dataReader, this.FIgnoreNamespace(this.dataReader)) as DataColumn;
				if (dataColumn != null && dataColumn.ColumnMapping == MappingType.Attribute)
				{
					array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(this.dataReader.Value);
				}
				if (this.isDiffgram)
				{
					if (this.dataReader.NamespaceURI == "urn:schemas-microsoft-com:xml-diffgram-v1")
					{
						string localName = this.dataReader.LocalName;
						if (!(localName == "id"))
						{
							if (!(localName == "hasChanges"))
							{
								if (localName == "hasErrors")
								{
									flag = (bool)Convert.ChangeType(this.dataReader.Value, typeof(bool), CultureInfo.InvariantCulture);
								}
							}
							else
							{
								text = this.dataReader.Value;
							}
						}
						else
						{
							key = this.dataReader.Value;
						}
					}
					else if (this.dataReader.NamespaceURI == "urn:schemas-microsoft-com:xml-msdata")
					{
						if (this.dataReader.LocalName == "rowOrder")
						{
							pos = (int)Convert.ChangeType(this.dataReader.Value, typeof(int), CultureInfo.InvariantCulture);
						}
						else if (this.dataReader.LocalName.StartsWith("hidden", StringComparison.Ordinal))
						{
							dataColumn = columns[XmlConvert.DecodeName(this.dataReader.LocalName.Substring(6))];
							if (dataColumn != null && dataColumn.ColumnMapping == MappingType.Hidden)
							{
								array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(this.dataReader.Value);
							}
						}
					}
				}
			}
			if (this.dataReader.Read() && i < this.dataReader.Depth)
			{
				while (i < this.dataReader.Depth)
				{
					XmlNodeType nodeType = this.dataReader.NodeType;
					DataColumn dataColumn;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					{
						object columnSchema = this.nodeToSchemaMap.GetColumnSchema(table, this.dataReader, this.FIgnoreNamespace(this.dataReader));
						dataColumn = (columnSchema as DataColumn);
						if (dataColumn != null)
						{
							if (array[dataColumn.Ordinal] == null)
							{
								this.LoadColumn(dataColumn, array);
								continue;
							}
							this.dataReader.Read();
							continue;
						}
						else
						{
							DataTable dataTable = columnSchema as DataTable;
							if (dataTable != null)
							{
								this.LoadTable(dataTable, true);
								continue;
							}
							if (this.ProcessXsdSchema())
							{
								continue;
							}
							DataTable tableForNode = this.nodeToSchemaMap.GetTableForNode(this.dataReader, this.FIgnoreNamespace(this.dataReader));
							if (tableForNode != null)
							{
								this.LoadTable(tableForNode, false);
								continue;
							}
							this.dataReader.Read();
							continue;
						}
						break;
					}
					case XmlNodeType.Attribute:
						goto IL_36C;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					case XmlNodeType.EntityReference:
						throw ExceptionBuilder.FoundEntity();
					default:
						if (nodeType - XmlNodeType.Whitespace > 1)
						{
							goto IL_36C;
						}
						break;
					}
					string s = this.dataReader.ReadString();
					dataColumn = table.xmlText;
					if (dataColumn != null && array[dataColumn.Ordinal] == null)
					{
						array[dataColumn.Ordinal] = dataColumn.ConvertXmlToObject(s);
						continue;
					}
					continue;
					IL_36C:
					this.dataReader.Read();
				}
				this.dataReader.Read();
			}
			DataRow dataRow;
			if (this.isDiffgram)
			{
				dataRow = table.NewRow(table.NewUninitializedRecord());
				dataRow.BeginEdit();
				for (int l = array.Length - 1; l >= 0; l--)
				{
					DataColumn dataColumn = columns[l];
					dataColumn[dataRow.tempRecord] = ((array[l] != null) ? array[l] : DBNull.Value);
				}
				dataRow.EndEdit();
				table.Rows.DiffInsertAt(dataRow, pos);
				if (text == null)
				{
					dataRow.oldRecord = dataRow.newRecord;
				}
				if (text == "modified" || flag)
				{
					table.RowDiffId[key] = dataRow;
				}
			}
			else
			{
				for (int m = array.Length - 1; m >= 0; m--)
				{
					if (array[m] == null)
					{
						DataColumn dataColumn = columns[m];
						if (dataColumn.AllowDBNull && dataColumn.ColumnMapping != MappingType.Hidden && !dataColumn.AutoIncrement)
						{
							array[m] = DBNull.Value;
						}
					}
				}
				dataRow = table.Rows.AddWithColumnEvents(array);
			}
			while (j < this.childRowsStack.Count)
			{
				DataRow dataRow2 = (DataRow)this.childRowsStack.Pop();
				bool flag2 = dataRow2.RowState == DataRowState.Unchanged;
				dataRow2.SetNestedParentRow(dataRow, false);
				if (flag2)
				{
					dataRow2.oldRecord = dataRow2.newRecord;
				}
			}
			if (isNested)
			{
				this.childRowsStack.Push(dataRow);
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0008CE78 File Offset: 0x0008C278
		private void LoadColumn(DataColumn column, object[] foundColumns)
		{
			string text = string.Empty;
			string text2 = null;
			int i = this.dataReader.Depth;
			if (this.dataReader.AttributeCount > 0)
			{
				text2 = this.dataReader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			}
			if (column.IsCustomType)
			{
				object obj = null;
				string text3 = null;
				string text4 = null;
				XmlRootAttribute xmlRootAttribute = null;
				if (this.dataReader.AttributeCount > 0)
				{
					text3 = this.dataReader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
					text4 = this.dataReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				}
				bool flag = !column.ImplementsIXMLSerializable && (!(column.DataType == typeof(object)) && text4 == null) && text3 == null;
				if (text2 != null && XmlConvert.ToBoolean(text2))
				{
					if (!flag && text4 != null && text4.Length > 0)
					{
						obj = SqlUdtStorage.GetStaticNullForUdtType(DataStorage.GetType(text4));
					}
					if (obj == null)
					{
						obj = DBNull.Value;
					}
					if (!this.dataReader.IsEmptyElement)
					{
						while (this.dataReader.Read() && i < this.dataReader.Depth)
						{
						}
					}
					this.dataReader.Read();
				}
				else
				{
					bool flag2 = false;
					if (column.Table.DataSet != null && column.Table.DataSet.UdtIsWrapped)
					{
						this.dataReader.Read();
						flag2 = true;
					}
					if (flag)
					{
						if (flag2)
						{
							xmlRootAttribute = new XmlRootAttribute(this.dataReader.LocalName);
							xmlRootAttribute.Namespace = this.dataReader.NamespaceURI;
						}
						else
						{
							xmlRootAttribute = new XmlRootAttribute(column.EncodedColumnName);
							xmlRootAttribute.Namespace = column.Namespace;
						}
					}
					obj = column.ConvertXmlToObject(this.dataReader, xmlRootAttribute);
					if (flag2)
					{
						this.dataReader.Read();
					}
				}
				foundColumns[column.Ordinal] = obj;
				return;
			}
			if (this.dataReader.Read() && i < this.dataReader.Depth)
			{
				while (i < this.dataReader.Depth)
				{
					XmlNodeType nodeType = this.dataReader.NodeType;
					switch (nodeType)
					{
					case XmlNodeType.Element:
					{
						if (this.ProcessXsdSchema())
						{
							continue;
						}
						object columnSchema = this.nodeToSchemaMap.GetColumnSchema(column.Table, this.dataReader, this.FIgnoreNamespace(this.dataReader));
						DataColumn dataColumn = columnSchema as DataColumn;
						if (dataColumn != null)
						{
							if (foundColumns[dataColumn.Ordinal] == null)
							{
								this.LoadColumn(dataColumn, foundColumns);
								continue;
							}
							this.dataReader.Read();
							continue;
						}
						else
						{
							DataTable dataTable = columnSchema as DataTable;
							if (dataTable != null)
							{
								this.LoadTable(dataTable, true);
								continue;
							}
							DataTable tableForNode = this.nodeToSchemaMap.GetTableForNode(this.dataReader, this.FIgnoreNamespace(this.dataReader));
							if (tableForNode != null)
							{
								this.LoadTable(tableForNode, false);
								continue;
							}
							this.dataReader.Read();
							continue;
						}
						break;
					}
					case XmlNodeType.Attribute:
						goto IL_368;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						break;
					case XmlNodeType.EntityReference:
						throw ExceptionBuilder.FoundEntity();
					default:
						if (nodeType - XmlNodeType.Whitespace > 1)
						{
							goto IL_368;
						}
						break;
					}
					if (text.Length != 0)
					{
						this.dataReader.ReadString();
						continue;
					}
					text = this.dataReader.Value;
					StringBuilder stringBuilder = null;
					while (this.dataReader.Read() && i < this.dataReader.Depth && this.IsTextLikeNode(this.dataReader.NodeType))
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(text);
						}
						stringBuilder.Append(this.dataReader.Value);
					}
					if (stringBuilder != null)
					{
						text = stringBuilder.ToString();
						continue;
					}
					continue;
					IL_368:
					this.dataReader.Read();
				}
				this.dataReader.Read();
			}
			if (text.Length == 0 && text2 != null && XmlConvert.ToBoolean(text2))
			{
				foundColumns[column.Ordinal] = DBNull.Value;
				return;
			}
			foundColumns[column.Ordinal] = column.ConvertXmlToObject(text);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0008D24C File Offset: 0x0008C64C
		private bool ProcessXsdSchema()
		{
			if (this.dataReader.LocalName == this.XSD_SCHEMA && this.dataReader.NamespaceURI == this.XSDNS)
			{
				if (this.ignoreSchema)
				{
					this.dataReader.Skip();
				}
				else if (this.isTableLevel)
				{
					this.dataTable.ReadXSDSchema(this.dataReader, false);
					this.nodeToSchemaMap = new XmlToDatasetMap(this.dataReader.NameTable, this.dataTable);
				}
				else
				{
					this.dataSet.ReadXSDSchema(this.dataReader, false);
					this.nodeToSchemaMap = new XmlToDatasetMap(this.dataReader.NameTable, this.dataSet);
				}
			}
			else
			{
				if ((this.dataReader.LocalName != this.XDR_SCHEMA || this.dataReader.NamespaceURI != this.XDRNS) && (this.dataReader.LocalName != this.SQL_SYNC || this.dataReader.NamespaceURI != this.UPDGNS))
				{
					return false;
				}
				this.dataReader.Skip();
			}
			return true;
		}

		// Token: 0x0400065F RID: 1631
		private DataSet dataSet;

		// Token: 0x04000660 RID: 1632
		private XmlToDatasetMap nodeToSchemaMap;

		// Token: 0x04000661 RID: 1633
		private Hashtable nodeToRowMap;

		// Token: 0x04000662 RID: 1634
		private Stack childRowsStack;

		// Token: 0x04000663 RID: 1635
		private Hashtable htableExcludedNS;

		// Token: 0x04000664 RID: 1636
		private bool fIsXdr;

		// Token: 0x04000665 RID: 1637
		internal bool isDiffgram;

		// Token: 0x04000666 RID: 1638
		private DataRow topMostRow;

		// Token: 0x04000667 RID: 1639
		private XmlElement topMostNode;

		// Token: 0x04000668 RID: 1640
		private bool ignoreSchema;

		// Token: 0x04000669 RID: 1641
		private DataTable dataTable;

		// Token: 0x0400066A RID: 1642
		private bool isTableLevel;

		// Token: 0x0400066B RID: 1643
		private bool fromInference;

		// Token: 0x0400066C RID: 1644
		private XmlReader dataReader;

		// Token: 0x0400066D RID: 1645
		private object XSD_XMLNS_NS;

		// Token: 0x0400066E RID: 1646
		private object XDR_SCHEMA;

		// Token: 0x0400066F RID: 1647
		private object XDRNS;

		// Token: 0x04000670 RID: 1648
		private object SQL_SYNC;

		// Token: 0x04000671 RID: 1649
		private object UPDGNS;

		// Token: 0x04000672 RID: 1650
		private object XSD_SCHEMA;

		// Token: 0x04000673 RID: 1651
		private object XSDNS;

		// Token: 0x04000674 RID: 1652
		private object DFFNS;

		// Token: 0x04000675 RID: 1653
		private object MSDNS;

		// Token: 0x04000676 RID: 1654
		private object DIFFID;

		// Token: 0x04000677 RID: 1655
		private object HASCHANGES;

		// Token: 0x04000678 RID: 1656
		private object ROWORDER;
	}
}

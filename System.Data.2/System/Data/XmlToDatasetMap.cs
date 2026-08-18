using System;
using System.Collections;
using System.Xml;

namespace System.Data
{
	// Token: 0x02000145 RID: 325
	internal sealed class XmlToDatasetMap
	{
		// Token: 0x0600133B RID: 4923 RVA: 0x000994B0 File Offset: 0x000988B0
		public XmlToDatasetMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataSet, nameTable);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000994CC File Offset: 0x000988CC
		public XmlToDatasetMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this.BuildIdentityMap(nameTable, dataSet);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x000994E8 File Offset: 0x000988E8
		public XmlToDatasetMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataTable, nameTable);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00099504 File Offset: 0x00098904
		public XmlToDatasetMap(XmlNameTable nameTable, DataTable dataTable)
		{
			this.BuildIdentityMap(nameTable, dataTable);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00099520 File Offset: 0x00098920
		internal static bool IsMappedColumn(DataColumn c)
		{
			return c.ColumnMapping != MappingType.Hidden;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0009953C File Offset: 0x0009893C
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(DataTable table, XmlNameTable nameTable)
		{
			string text = nameTable.Get(table.EncodedTableName);
			string namespaceURI = nameTable.Get(table.Namespace);
			if (text == null)
			{
				return null;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this.tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, namespaceURI)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00099584 File Offset: 0x00098984
		private XmlToDatasetMap.TableSchemaInfo AddTableSchema(XmlNameTable nameTable, DataTable table)
		{
			string encodedTableName = table.EncodedTableName;
			string text = nameTable.Get(encodedTableName);
			if (text == null)
			{
				text = nameTable.Add(encodedTableName);
			}
			table.encodedTableName = text;
			string text2 = nameTable.Get(table.Namespace);
			if (text2 == null)
			{
				text2 = nameTable.Add(table.Namespace);
			}
			else if (table.tableNamespace != null)
			{
				table.tableNamespace = text2;
			}
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = new XmlToDatasetMap.TableSchemaInfo(table);
			this.tableSchemaMap[new XmlToDatasetMap.XmlNodeIdentety(text, text2)] = tableSchemaInfo;
			return tableSchemaInfo;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000995FC File Offset: 0x000989FC
		private bool AddColumnSchema(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text = nameTable.Get(col.EncodedColumnName);
			string namespaceURI = nameTable.Get(col.Namespace);
			if (text == null)
			{
				return false;
			}
			XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, namespaceURI);
			columns[key] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00099658 File Offset: 0x00098A58
		private bool AddColumnSchema(XmlNameTable nameTable, DataColumn col, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string array = XmlConvert.EncodeLocalName(col.ColumnName);
			string text = nameTable.Get(array);
			if (text == null)
			{
				text = nameTable.Add(array);
			}
			col.encodedColumnName = text;
			string text2 = nameTable.Get(col.Namespace);
			if (text2 == null)
			{
				text2 = nameTable.Add(col.Namespace);
			}
			else if (col._columnUri != null)
			{
				col._columnUri = text2;
			}
			XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
			columns[key] = col;
			if (col.ColumnName.StartsWith("xml", StringComparison.OrdinalIgnoreCase))
			{
				this.HandleSpecialColumn(col, nameTable, columns);
			}
			return true;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000996E8 File Offset: 0x00098AE8
		private void BuildIdentityMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this.tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
				}
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000997D4 File Offset: 0x00098BD4
		private void BuildIdentityMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this.tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(dataSet.Tables.Count);
			string text = nameTable.Get(dataSet.Namespace);
			if (text == null)
			{
				text = nameTable.Add(dataSet.Namespace);
			}
			dataSet.namespaceURI = text;
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string array = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text2 = nameTable.Get(array);
							if (text2 == null)
							{
								text2 = nameTable.Add(array);
							}
							string text3 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text3 == null)
							{
								text3 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text2, text3);
							tableSchemaInfo.ColumnsSchemaMap[key] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000999C8 File Offset: 0x00098DC8
		private void BuildIdentityMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this.tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(1);
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(dataTable, nameTable);
			if (tableSchemaInfo != null)
			{
				foreach (object obj in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					if (XmlToDatasetMap.IsMappedColumn(dataColumn))
					{
						this.AddColumnSchema(dataColumn, nameTable, tableSchemaInfo.ColumnsSchemaMap);
					}
				}
			}
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00099A58 File Offset: 0x00098E58
		private void BuildIdentityMap(XmlNameTable nameTable, DataTable dataTable)
		{
			ArrayList selfAndDescendants = this.GetSelfAndDescendants(dataTable);
			this.tableSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(selfAndDescendants.Count);
			foreach (object obj in selfAndDescendants)
			{
				DataTable dataTable2 = (DataTable)obj;
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = this.AddTableSchema(nameTable, dataTable2);
				if (tableSchemaInfo != null)
				{
					foreach (object obj2 in dataTable2.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj2;
						if (XmlToDatasetMap.IsMappedColumn(dataColumn))
						{
							this.AddColumnSchema(nameTable, dataColumn, tableSchemaInfo.ColumnsSchemaMap);
						}
					}
					foreach (object obj3 in dataTable2.ChildRelations)
					{
						DataRelation dataRelation = (DataRelation)obj3;
						if (dataRelation.Nested)
						{
							string array = XmlConvert.EncodeLocalName(dataRelation.ChildTable.TableName);
							string text = nameTable.Get(array);
							if (text == null)
							{
								text = nameTable.Add(array);
							}
							string text2 = nameTable.Get(dataRelation.ChildTable.Namespace);
							if (text2 == null)
							{
								text2 = nameTable.Add(dataRelation.ChildTable.Namespace);
							}
							XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, text2);
							tableSchemaInfo.ColumnsSchemaMap[key] = dataRelation.ChildTable;
						}
					}
				}
			}
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00099C24 File Offset: 0x00099024
		private ArrayList GetSelfAndDescendants(DataTable dt)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(dt);
			for (int i = 0; i < arrayList.Count; i++)
			{
				foreach (object obj in ((DataTable)arrayList[i]).ChildRelations)
				{
					DataRelation dataRelation = (DataRelation)obj;
					if (!arrayList.Contains(dataRelation.ChildTable))
					{
						arrayList.Add(dataRelation.ChildTable);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00099CCC File Offset: 0x000990CC
		public object GetColumnSchema(XmlNode node, bool fIgnoreNamespace)
		{
			XmlNode xmlNode = (node.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)node).OwnerElement : node.ParentNode;
			while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element)
			{
				XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this.tableSchemaMap[xmlNode.LocalName] : this.tableSchemaMap[xmlNode]);
				xmlNode = xmlNode.ParentNode;
				if (tableSchemaInfo != null)
				{
					if (fIgnoreNamespace)
					{
						return tableSchemaInfo.ColumnsSchemaMap[node.LocalName];
					}
					return tableSchemaInfo.ColumnsSchemaMap[node];
				}
			}
			return null;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00099D5C File Offset: 0x0009915C
		public object GetColumnSchema(DataTable table, XmlReader dataReader, bool fIgnoreNamespace)
		{
			if (this.lastTableSchemaInfo == null || this.lastTableSchemaInfo.TableSchema != table)
			{
				this.lastTableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this.tableSchemaMap[table.EncodedTableName] : this.tableSchemaMap[table]);
			}
			if (fIgnoreNamespace)
			{
				return this.lastTableSchemaInfo.ColumnsSchemaMap[dataReader.LocalName];
			}
			return this.lastTableSchemaInfo.ColumnsSchemaMap[dataReader];
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00099DD8 File Offset: 0x000991D8
		public object GetSchemaForNode(XmlNode node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = null;
			if (node.NodeType == XmlNodeType.Element)
			{
				tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this.tableSchemaMap[node.LocalName] : this.tableSchemaMap[node]);
			}
			if (tableSchemaInfo != null)
			{
				return tableSchemaInfo.TableSchema;
			}
			return this.GetColumnSchema(node, fIgnoreNamespace);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00099E2C File Offset: 0x0009922C
		public DataTable GetTableForNode(XmlReader node, bool fIgnoreNamespace)
		{
			XmlToDatasetMap.TableSchemaInfo tableSchemaInfo = (XmlToDatasetMap.TableSchemaInfo)(fIgnoreNamespace ? this.tableSchemaMap[node.LocalName] : this.tableSchemaMap[node]);
			if (tableSchemaInfo != null)
			{
				this.lastTableSchemaInfo = tableSchemaInfo;
				return this.lastTableSchemaInfo.TableSchema;
			}
			return null;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00099E78 File Offset: 0x00099278
		private void HandleSpecialColumn(DataColumn col, XmlNameTable nameTable, XmlToDatasetMap.XmlNodeIdHashtable columns)
		{
			string text;
			if ('x' == col.ColumnName[0])
			{
				text = "_x0078_";
			}
			else
			{
				text = "_x0058_";
			}
			text += col.ColumnName.Substring(1);
			if (nameTable.Get(text) == null)
			{
				nameTable.Add(text);
			}
			string namespaceURI = nameTable.Get(col.Namespace);
			XmlToDatasetMap.XmlNodeIdentety key = new XmlToDatasetMap.XmlNodeIdentety(text, namespaceURI);
			columns[key] = col;
		}

		// Token: 0x04000783 RID: 1923
		private XmlToDatasetMap.XmlNodeIdHashtable tableSchemaMap;

		// Token: 0x04000784 RID: 1924
		private XmlToDatasetMap.TableSchemaInfo lastTableSchemaInfo;

		// Token: 0x02000366 RID: 870
		private sealed class XmlNodeIdentety
		{
			// Token: 0x0600344B RID: 13387 RVA: 0x00140A8C File Offset: 0x0013FE8C
			public XmlNodeIdentety(string localName, string namespaceURI)
			{
				this.LocalName = localName;
				this.NamespaceURI = namespaceURI;
			}

			// Token: 0x0600344C RID: 13388 RVA: 0x00140AB0 File Offset: 0x0013FEB0
			public override int GetHashCode()
			{
				return this.LocalName.GetHashCode();
			}

			// Token: 0x0600344D RID: 13389 RVA: 0x00140AC8 File Offset: 0x0013FEC8
			public override bool Equals(object obj)
			{
				XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = (XmlToDatasetMap.XmlNodeIdentety)obj;
				return string.Compare(this.LocalName, xmlNodeIdentety.LocalName, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.NamespaceURI, xmlNodeIdentety.NamespaceURI, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x04001F12 RID: 7954
			public string LocalName;

			// Token: 0x04001F13 RID: 7955
			public string NamespaceURI;
		}

		// Token: 0x02000367 RID: 871
		internal sealed class XmlNodeIdHashtable : Hashtable
		{
			// Token: 0x0600344E RID: 13390 RVA: 0x00140B08 File Offset: 0x0013FF08
			public XmlNodeIdHashtable(int capacity) : base(capacity)
			{
			}

			// Token: 0x17000846 RID: 2118
			public object this[XmlNode node]
			{
				get
				{
					this.id.LocalName = node.LocalName;
					this.id.NamespaceURI = node.NamespaceURI;
					return this[this.id];
				}
			}

			// Token: 0x17000847 RID: 2119
			public object this[XmlReader dataReader]
			{
				get
				{
					this.id.LocalName = dataReader.LocalName;
					this.id.NamespaceURI = dataReader.NamespaceURI;
					return this[this.id];
				}
			}

			// Token: 0x17000848 RID: 2120
			public object this[DataTable table]
			{
				get
				{
					this.id.LocalName = table.EncodedTableName;
					this.id.NamespaceURI = table.Namespace;
					return this[this.id];
				}
			}

			// Token: 0x17000849 RID: 2121
			public object this[string name]
			{
				get
				{
					this.id.LocalName = name;
					this.id.NamespaceURI = string.Empty;
					return this[this.id];
				}
			}

			// Token: 0x04001F14 RID: 7956
			private XmlToDatasetMap.XmlNodeIdentety id = new XmlToDatasetMap.XmlNodeIdentety(string.Empty, string.Empty);
		}

		// Token: 0x02000368 RID: 872
		private sealed class TableSchemaInfo
		{
			// Token: 0x06003453 RID: 13395 RVA: 0x00140C20 File Offset: 0x00140020
			public TableSchemaInfo(DataTable tableSchema)
			{
				this.TableSchema = tableSchema;
				this.ColumnsSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(tableSchema.Columns.Count);
			}

			// Token: 0x04001F15 RID: 7957
			public DataTable TableSchema;

			// Token: 0x04001F16 RID: 7958
			public XmlToDatasetMap.XmlNodeIdHashtable ColumnsSchemaMap;
		}
	}
}

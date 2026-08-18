using System;
using System.Collections;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000FE RID: 254
	internal sealed class XmlToDatasetMap
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00226EA8 File Offset: 0x002262A8
		public XmlToDatasetMap(DataSet dataSet, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataSet, nameTable);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00226EC8 File Offset: 0x002262C8
		public XmlToDatasetMap(XmlNameTable nameTable, DataSet dataSet)
		{
			this.BuildIdentityMap(nameTable, dataSet);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00226EE8 File Offset: 0x002262E8
		public XmlToDatasetMap(DataTable dataTable, XmlNameTable nameTable)
		{
			this.BuildIdentityMap(dataTable, nameTable);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00226F08 File Offset: 0x00226308
		public XmlToDatasetMap(XmlNameTable nameTable, DataTable dataTable)
		{
			this.BuildIdentityMap(nameTable, dataTable);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00226F28 File Offset: 0x00226328
		internal static bool IsMappedColumn(DataColumn c)
		{
			return c.ColumnMapping != MappingType.Hidden;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00226F48 File Offset: 0x00226348
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

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00226F98 File Offset: 0x00226398
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

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00227018 File Offset: 0x00226418
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

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00227078 File Offset: 0x00226478
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

		// Token: 0x06000EDA RID: 3802 RVA: 0x00227108 File Offset: 0x00226508
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

		// Token: 0x06000EDB RID: 3803 RVA: 0x002271F8 File Offset: 0x002265F8
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

		// Token: 0x06000EDC RID: 3804 RVA: 0x002273F8 File Offset: 0x002267F8
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

		// Token: 0x06000EDD RID: 3805 RVA: 0x00227488 File Offset: 0x00226888
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

		// Token: 0x06000EDE RID: 3806 RVA: 0x00227658 File Offset: 0x00226A58
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

		// Token: 0x06000EDF RID: 3807 RVA: 0x00227708 File Offset: 0x00226B08
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

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00227798 File Offset: 0x00226B98
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

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00227818 File Offset: 0x00226C18
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

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00227878 File Offset: 0x00226C78
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

		// Token: 0x06000EE3 RID: 3811 RVA: 0x002278C8 File Offset: 0x00226CC8
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

		// Token: 0x04000AAA RID: 2730
		private XmlToDatasetMap.XmlNodeIdHashtable tableSchemaMap;

		// Token: 0x04000AAB RID: 2731
		private XmlToDatasetMap.TableSchemaInfo lastTableSchemaInfo;

		// Token: 0x020000FF RID: 255
		private sealed class XmlNodeIdentety
		{
			// Token: 0x06000EE4 RID: 3812 RVA: 0x00227938 File Offset: 0x00226D38
			public XmlNodeIdentety(string localName, string namespaceURI)
			{
				this.LocalName = localName;
				this.NamespaceURI = namespaceURI;
			}

			// Token: 0x06000EE5 RID: 3813 RVA: 0x00227968 File Offset: 0x00226D68
			public override int GetHashCode()
			{
				return this.LocalName.GetHashCode();
			}

			// Token: 0x06000EE6 RID: 3814 RVA: 0x00227988 File Offset: 0x00226D88
			public override bool Equals(object obj)
			{
				XmlToDatasetMap.XmlNodeIdentety xmlNodeIdentety = (XmlToDatasetMap.XmlNodeIdentety)obj;
				return string.Compare(this.LocalName, xmlNodeIdentety.LocalName, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.NamespaceURI, xmlNodeIdentety.NamespaceURI, StringComparison.OrdinalIgnoreCase) == 0;
			}

			// Token: 0x04000AAC RID: 2732
			public string LocalName;

			// Token: 0x04000AAD RID: 2733
			public string NamespaceURI;
		}

		// Token: 0x02000100 RID: 256
		internal sealed class XmlNodeIdHashtable : Hashtable
		{
			// Token: 0x06000EE7 RID: 3815 RVA: 0x002279C8 File Offset: 0x00226DC8
			public XmlNodeIdHashtable(int capacity) : base(capacity)
			{
			}

			// Token: 0x1700022F RID: 559
			public object this[XmlNode node]
			{
				get
				{
					this.id.LocalName = node.LocalName;
					this.id.NamespaceURI = node.NamespaceURI;
					return this[this.id];
				}
			}

			// Token: 0x17000230 RID: 560
			public object this[XmlReader dataReader]
			{
				get
				{
					this.id.LocalName = dataReader.LocalName;
					this.id.NamespaceURI = dataReader.NamespaceURI;
					return this[this.id];
				}
			}

			// Token: 0x17000231 RID: 561
			public object this[DataTable table]
			{
				get
				{
					this.id.LocalName = table.EncodedTableName;
					this.id.NamespaceURI = table.Namespace;
					return this[this.id];
				}
			}

			// Token: 0x17000232 RID: 562
			public object this[string name]
			{
				get
				{
					this.id.LocalName = name;
					this.id.NamespaceURI = string.Empty;
					return this[this.id];
				}
			}

			// Token: 0x04000AAE RID: 2734
			private XmlToDatasetMap.XmlNodeIdentety id = new XmlToDatasetMap.XmlNodeIdentety(string.Empty, string.Empty);
		}

		// Token: 0x02000101 RID: 257
		private sealed class TableSchemaInfo
		{
			// Token: 0x06000EEC RID: 3820 RVA: 0x00227AF8 File Offset: 0x00226EF8
			public TableSchemaInfo(DataTable tableSchema)
			{
				this.TableSchema = tableSchema;
				this.ColumnsSchemaMap = new XmlToDatasetMap.XmlNodeIdHashtable(tableSchema.Columns.Count);
			}

			// Token: 0x04000AAF RID: 2735
			public DataTable TableSchema;

			// Token: 0x04000AB0 RID: 2736
			public XmlToDatasetMap.XmlNodeIdHashtable ColumnsSchemaMap;
		}
	}
}

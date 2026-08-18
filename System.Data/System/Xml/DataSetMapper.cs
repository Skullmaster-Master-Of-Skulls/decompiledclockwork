using System;
using System.Collections;
using System.Data;

namespace System.Xml
{
	// Token: 0x02000386 RID: 902
	internal sealed class DataSetMapper
	{
		// Token: 0x06002FC6 RID: 12230 RVA: 0x002D5DF8 File Offset: 0x002D51F8
		internal DataSetMapper()
		{
			this.tableSchemaMap = new Hashtable();
			this.columnSchemaMap = new Hashtable();
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x002D5E28 File Offset: 0x002D5228
		internal void SetupMapping(XmlDataDocument xd, DataSet ds)
		{
			if (this.IsMapped())
			{
				this.tableSchemaMap = new Hashtable();
				this.columnSchemaMap = new Hashtable();
			}
			this.doc = xd;
			this.dataSet = ds;
			foreach (object obj in this.dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				this.AddTableSchema(dataTable);
				foreach (object obj2 in dataTable.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj2;
					if (!DataSetMapper.IsNotMapped(dataColumn))
					{
						this.AddColumnSchema(dataColumn);
					}
				}
			}
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x002D5F28 File Offset: 0x002D5328
		internal bool IsMapped()
		{
			return this.dataSet != null;
		}

		// Token: 0x06002FC9 RID: 12233 RVA: 0x002D5F48 File Offset: 0x002D5348
		internal DataTable SearchMatchingTableSchema(string localName, string namespaceURI)
		{
			object identity = DataSetMapper.GetIdentity(localName, namespaceURI);
			return (DataTable)this.tableSchemaMap[identity];
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x002D5F78 File Offset: 0x002D5378
		internal DataTable SearchMatchingTableSchema(XmlBoundElement rowElem, XmlBoundElement elem)
		{
			DataTable dataTable = this.SearchMatchingTableSchema(elem.LocalName, elem.NamespaceURI);
			if (dataTable == null)
			{
				return null;
			}
			if (rowElem == null)
			{
				return dataTable;
			}
			if (this.GetColumnSchemaForNode(rowElem, elem) == null)
			{
				return dataTable;
			}
			foreach (object obj in elem.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				if (xmlAttribute.NamespaceURI != "http://www.w3.org/2000/xmlns/")
				{
					return dataTable;
				}
			}
			for (XmlNode xmlNode = elem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x002D6048 File Offset: 0x002D5448
		internal DataColumn GetColumnSchemaForNode(XmlBoundElement rowElem, XmlNode node)
		{
			object identity = DataSetMapper.GetIdentity(rowElem.LocalName, rowElem.NamespaceURI);
			object identity2 = DataSetMapper.GetIdentity(node.LocalName, node.NamespaceURI);
			Hashtable hashtable = (Hashtable)this.columnSchemaMap[identity];
			if (hashtable == null)
			{
				return null;
			}
			DataColumn dataColumn = (DataColumn)hashtable[identity2];
			if (dataColumn == null)
			{
				return null;
			}
			MappingType columnMapping = dataColumn.ColumnMapping;
			if (node.NodeType == XmlNodeType.Attribute && columnMapping == MappingType.Attribute)
			{
				return dataColumn;
			}
			if (node.NodeType == XmlNodeType.Element && columnMapping == MappingType.Element)
			{
				return dataColumn;
			}
			return null;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x002D60D8 File Offset: 0x002D54D8
		internal DataTable GetTableSchemaForElement(XmlElement elem)
		{
			XmlBoundElement xmlBoundElement = elem as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return this.GetTableSchemaForElement(xmlBoundElement);
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x002D60F8 File Offset: 0x002D54F8
		internal DataTable GetTableSchemaForElement(XmlBoundElement be)
		{
			DataRow row = be.Row;
			if (row != null)
			{
				return row.Table;
			}
			return null;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x002D6118 File Offset: 0x002D5518
		internal static bool IsNotMapped(DataColumn c)
		{
			return c.ColumnMapping == MappingType.Hidden;
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x002D6138 File Offset: 0x002D5538
		internal DataRow GetRowFromElement(XmlElement e)
		{
			XmlBoundElement xmlBoundElement = e as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.Row;
			}
			return null;
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x002D6158 File Offset: 0x002D5558
		internal DataRow GetRowFromElement(XmlBoundElement be)
		{
			return be.Row;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x002D6178 File Offset: 0x002D5578
		internal bool GetRegion(XmlNode node, out XmlBoundElement rowElem)
		{
			while (node != null)
			{
				XmlBoundElement xmlBoundElement = node as XmlBoundElement;
				if (xmlBoundElement != null && this.GetRowFromElement(xmlBoundElement) != null)
				{
					rowElem = xmlBoundElement;
					return true;
				}
				if (node.NodeType == XmlNodeType.Attribute)
				{
					node = ((XmlAttribute)node).OwnerElement;
				}
				else
				{
					node = node.ParentNode;
				}
			}
			rowElem = null;
			return false;
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x002D61C8 File Offset: 0x002D55C8
		internal bool IsRegionRadical(XmlBoundElement rowElem)
		{
			if (rowElem.ElementState == ElementState.Defoliated)
			{
				return true;
			}
			DataTable tableSchemaForElement = this.GetTableSchemaForElement(rowElem);
			DataColumnCollection columns = tableSchemaForElement.Columns;
			int num = 0;
			int count = rowElem.Attributes.Count;
			for (int i = 0; i < count; i++)
			{
				XmlAttribute xmlAttribute = rowElem.Attributes[i];
				if (!xmlAttribute.Specified)
				{
					return false;
				}
				DataColumn columnSchemaForNode = this.GetColumnSchemaForNode(rowElem, xmlAttribute);
				if (columnSchemaForNode == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode))
				{
					return false;
				}
				XmlNode firstChild = xmlAttribute.FirstChild;
				if (firstChild == null || firstChild.NodeType != XmlNodeType.Text || firstChild.NextSibling != null)
				{
					return false;
				}
			}
			num = 0;
			for (XmlNode xmlNode = rowElem.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.NodeType != XmlNodeType.Element)
				{
					return false;
				}
				XmlElement xmlElement = xmlNode as XmlElement;
				if (this.GetRowFromElement(xmlElement) != null)
				{
					IL_139:
					while (xmlNode != null)
					{
						if (xmlNode.NodeType != XmlNodeType.Element)
						{
							return false;
						}
						if (this.GetRowFromElement((XmlElement)xmlNode) == null)
						{
							return false;
						}
						xmlNode = xmlNode.NextSibling;
					}
					return true;
				}
				DataColumn columnSchemaForNode2 = this.GetColumnSchemaForNode(rowElem, xmlElement);
				if (columnSchemaForNode2 == null)
				{
					return false;
				}
				if (!this.IsNextColumn(columns, ref num, columnSchemaForNode2))
				{
					return false;
				}
				if (xmlElement.HasAttributes)
				{
					return false;
				}
				XmlNode firstChild2 = xmlElement.FirstChild;
				if (firstChild2 == null || firstChild2.NodeType != XmlNodeType.Text || firstChild2.NextSibling != null)
				{
					return false;
				}
			}
			goto IL_139;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x002D6318 File Offset: 0x002D5718
		private void AddTableSchema(DataTable table)
		{
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			this.tableSchemaMap[identity] = table;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x002D6348 File Offset: 0x002D5748
		private void AddColumnSchema(DataColumn col)
		{
			DataTable table = col.Table;
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			object identity2 = DataSetMapper.GetIdentity(col.EncodedColumnName, col.Namespace);
			Hashtable hashtable = (Hashtable)this.columnSchemaMap[identity];
			if (hashtable == null)
			{
				hashtable = new Hashtable();
				this.columnSchemaMap[identity] = hashtable;
			}
			hashtable[identity2] = col;
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x002D63B8 File Offset: 0x002D57B8
		private static object GetIdentity(string localName, string namespaceURI)
		{
			return localName + ":" + namespaceURI;
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x002D63D8 File Offset: 0x002D57D8
		private bool IsNextColumn(DataColumnCollection columns, ref int iColumn, DataColumn col)
		{
			while (iColumn < columns.Count)
			{
				if (columns[iColumn] == col)
				{
					iColumn++;
					return true;
				}
				iColumn++;
			}
			return false;
		}

		// Token: 0x04001DA2 RID: 7586
		internal const string strReservedXmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04001DA3 RID: 7587
		private Hashtable tableSchemaMap;

		// Token: 0x04001DA4 RID: 7588
		private Hashtable columnSchemaMap;

		// Token: 0x04001DA5 RID: 7589
		private XmlDataDocument doc;

		// Token: 0x04001DA6 RID: 7590
		private DataSet dataSet;
	}
}

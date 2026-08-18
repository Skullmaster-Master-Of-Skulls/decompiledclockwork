using System;
using System.Collections;
using System.Data;

namespace System.Xml
{
	// Token: 0x02000084 RID: 132
	internal sealed class DataSetMapper
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0004A958 File Offset: 0x00049D58
		internal DataSetMapper()
		{
			this.tableSchemaMap = new Hashtable();
			this.columnSchemaMap = new Hashtable();
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0004A984 File Offset: 0x00049D84
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

		// Token: 0x06000664 RID: 1636 RVA: 0x0004AA7C File Offset: 0x00049E7C
		internal bool IsMapped()
		{
			return this.dataSet != null;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0004AA94 File Offset: 0x00049E94
		internal DataTable SearchMatchingTableSchema(string localName, string namespaceURI)
		{
			object identity = DataSetMapper.GetIdentity(localName, namespaceURI);
			return (DataTable)this.tableSchemaMap[identity];
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x0004AABC File Offset: 0x00049EBC
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

		// Token: 0x06000667 RID: 1639 RVA: 0x0004AB80 File Offset: 0x00049F80
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

		// Token: 0x06000668 RID: 1640 RVA: 0x0004AC04 File Offset: 0x0004A004
		internal DataTable GetTableSchemaForElement(XmlElement elem)
		{
			XmlBoundElement xmlBoundElement = elem as XmlBoundElement;
			if (xmlBoundElement == null)
			{
				return null;
			}
			return this.GetTableSchemaForElement(xmlBoundElement);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0004AC24 File Offset: 0x0004A024
		internal DataTable GetTableSchemaForElement(XmlBoundElement be)
		{
			DataRow row = be.Row;
			if (row != null)
			{
				return row.Table;
			}
			return null;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0004AC44 File Offset: 0x0004A044
		internal static bool IsNotMapped(DataColumn c)
		{
			return c.ColumnMapping == MappingType.Hidden;
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0004AC5C File Offset: 0x0004A05C
		internal DataRow GetRowFromElement(XmlElement e)
		{
			XmlBoundElement xmlBoundElement = e as XmlBoundElement;
			if (xmlBoundElement != null)
			{
				return xmlBoundElement.Row;
			}
			return null;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0004AC7C File Offset: 0x0004A07C
		internal DataRow GetRowFromElement(XmlBoundElement be)
		{
			return be.Row;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0004AC90 File Offset: 0x0004A090
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

		// Token: 0x0600066E RID: 1646 RVA: 0x0004ACE0 File Offset: 0x0004A0E0
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

		// Token: 0x0600066F RID: 1647 RVA: 0x0004AE2C File Offset: 0x0004A22C
		private void AddTableSchema(DataTable table)
		{
			object identity = DataSetMapper.GetIdentity(table.EncodedTableName, table.Namespace);
			this.tableSchemaMap[identity] = table;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0004AE58 File Offset: 0x0004A258
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

		// Token: 0x06000671 RID: 1649 RVA: 0x0004AEC0 File Offset: 0x0004A2C0
		private static object GetIdentity(string localName, string namespaceURI)
		{
			return localName + ":" + namespaceURI;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0004AEDC File Offset: 0x0004A2DC
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

		// Token: 0x04000275 RID: 629
		private Hashtable tableSchemaMap;

		// Token: 0x04000276 RID: 630
		private Hashtable columnSchemaMap;

		// Token: 0x04000277 RID: 631
		private XmlDataDocument doc;

		// Token: 0x04000278 RID: 632
		private DataSet dataSet;

		// Token: 0x04000279 RID: 633
		internal const string strReservedXmlns = "http://www.w3.org/2000/xmlns/";
	}
}

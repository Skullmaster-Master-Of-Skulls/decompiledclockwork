using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200008D RID: 141
	internal sealed class XPathNodePointer : IXmlDataVirtualNode
	{
		// Token: 0x0600072E RID: 1838 RVA: 0x0004F3F4 File Offset: 0x0004E7F4
		static XPathNodePointer()
		{
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map = new int[20];
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[0] = -1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[1] = 1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[2] = 2;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[3] = 4;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[4] = 4;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[5] = -1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[6] = -1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[7] = 7;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[8] = 8;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[9] = 0;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[10] = -1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[11] = 0;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[12] = -1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[13] = 6;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[14] = 5;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[15] = -1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[16] = -1;
			XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[17] = -1;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0004F4C4 File Offset: 0x0004E8C4
		private XPathNodeType DecideXPNodeTypeForTextNodes(XmlNode node)
		{
			XPathNodeType result = XPathNodeType.Whitespace;
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				if (nodeType - XmlNodeType.Text <= 1)
				{
					return XPathNodeType.Text;
				}
				if (nodeType != XmlNodeType.Whitespace)
				{
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						return result;
					}
					result = XPathNodeType.SignificantWhitespace;
				}
				node = this._doc.SafeNextSibling(node);
			}
			return result;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0004F508 File Offset: 0x0004E908
		private XPathNodeType ConvertNodeType(XmlNode node)
		{
			if (XmlDataDocument.IsTextNode(node.NodeType))
			{
				return this.DecideXPNodeTypeForTextNodes(node);
			}
			int num = XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)node.NodeType];
			if (num != 2)
			{
				return (XPathNodeType)num;
			}
			if (node.NamespaceURI == XPathNodePointer.s_strReservedXmlns)
			{
				return XPathNodeType.Namespace;
			}
			return XPathNodeType.Attribute;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0004F554 File Offset: 0x0004E954
		private bool IsNamespaceNode(XmlNodeType nt, string ns)
		{
			return nt == XmlNodeType.Attribute && ns == XPathNodePointer.s_strReservedXmlns;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0004F578 File Offset: 0x0004E978
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XmlDataDocument doc, XmlNode node) : this(owner, doc, node, null, false, null)
		{
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0004F594 File Offset: 0x0004E994
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XPathNodePointer pointer) : this(owner, pointer._doc, pointer._node, pointer._column, pointer._fOnValue, pointer._parentOfNS)
		{
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0004F5C8 File Offset: 0x0004E9C8
		private XPathNodePointer(DataDocumentXPathNavigator owner, XmlDataDocument doc, XmlNode node, DataColumn c, bool bOnValue, XmlBoundElement parentOfNS)
		{
			this._owner = new WeakReference(owner);
			this._doc = doc;
			this._node = node;
			this._column = c;
			this._fOnValue = bOnValue;
			this._parentOfNS = parentOfNS;
			this._doc.AddPointer(this);
			this._bNeedFoliate = false;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0004F620 File Offset: 0x0004EA20
		internal XPathNodePointer Clone(DataDocumentXPathNavigator owner)
		{
			this.RealFoliate();
			return new XPathNodePointer(owner, this);
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0004F63C File Offset: 0x0004EA3C
		internal bool IsEmptyElement
		{
			get
			{
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Element && ((XmlElement)this._node).IsEmpty;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0004F67C File Offset: 0x0004EA7C
		internal XPathNodeType NodeType
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return XPathNodeType.All;
				}
				if (this._column == null)
				{
					return this.ConvertNodeType(this._node);
				}
				if (this._fOnValue)
				{
					return XPathNodeType.Text;
				}
				if (this._column.ColumnMapping != MappingType.Attribute)
				{
					return XPathNodeType.Element;
				}
				if (this._column.Namespace == XPathNodePointer.s_strReservedXmlns)
				{
					return XPathNodeType.Namespace;
				}
				return XPathNodeType.Attribute;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0004F6E4 File Offset: 0x0004EAE4
		internal string LocalName
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XmlNodeType nodeType = this._node.NodeType;
					if (this.IsNamespaceNode(nodeType, this._node.NamespaceURI) && this._node.LocalName == XPathNodePointer.s_strXmlNS)
					{
						return string.Empty;
					}
					if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.Attribute || nodeType == XmlNodeType.ProcessingInstruction)
					{
						return this._node.LocalName;
					}
					return string.Empty;
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.EncodedColumnName);
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0004F794 File Offset: 0x0004EB94
		internal string Name
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XmlNodeType nodeType = this._node.NodeType;
					if (this.IsNamespaceNode(nodeType, this._node.NamespaceURI))
					{
						if (this._node.LocalName == XPathNodePointer.s_strXmlNS)
						{
							return string.Empty;
						}
						return this._node.LocalName;
					}
					else
					{
						if (nodeType == XmlNodeType.Element || nodeType == XmlNodeType.Attribute || nodeType == XmlNodeType.ProcessingInstruction)
						{
							return this._node.Name;
						}
						return string.Empty;
					}
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.EncodedColumnName);
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x0004F850 File Offset: 0x0004EC50
		internal string NamespaceURI
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					XPathNodeType xpathNodeType = this.ConvertNodeType(this._node);
					if (xpathNodeType == XPathNodeType.Element || xpathNodeType == XPathNodeType.Root || xpathNodeType == XPathNodeType.Attribute)
					{
						return this._node.NamespaceURI;
					}
					return string.Empty;
				}
				else
				{
					if (this._fOnValue)
					{
						return string.Empty;
					}
					if (this._column.Namespace == XPathNodePointer.s_strReservedXmlns)
					{
						return string.Empty;
					}
					return this._doc.NameTable.Add(this._column.Namespace);
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0004F8EC File Offset: 0x0004ECEC
		internal string Prefix
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column != null)
				{
					return string.Empty;
				}
				if (this.IsNamespaceNode(this._node.NodeType, this._node.NamespaceURI))
				{
					return string.Empty;
				}
				return this._node.Prefix;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x0004F94C File Offset: 0x0004ED4C
		internal string Value
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return null;
				}
				if (this._column == null)
				{
					string text = this._node.Value;
					if (XmlDataDocument.IsTextNode(this._node.NodeType))
					{
						if (this._node.ParentNode == null)
						{
							return text;
						}
						XmlNode xmlNode = this._doc.SafeNextSibling(this._node);
						while (xmlNode != null && XmlDataDocument.IsTextNode(xmlNode.NodeType))
						{
							text += xmlNode.Value;
							xmlNode = this._doc.SafeNextSibling(xmlNode);
						}
					}
					return text;
				}
				if (this._column.ColumnMapping != MappingType.Attribute && !this._fOnValue)
				{
					return null;
				}
				DataRow row = this.Row;
				DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
				object value = row[this._column, version];
				if (!Convert.IsDBNull(value))
				{
					return this._column.ConvertObjectToXml(value);
				}
				return null;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0004FA40 File Offset: 0x0004EE40
		internal string InnerText
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return string.Empty;
				}
				if (this._column == null)
				{
					if (this._node.NodeType != XmlNodeType.Document)
					{
						return this._node.InnerText;
					}
					XmlElement documentElement = ((XmlDocument)this._node).DocumentElement;
					if (documentElement != null)
					{
						return documentElement.InnerText;
					}
					return string.Empty;
				}
				else
				{
					DataRow row = this.Row;
					DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
					object value = row[this._column, version];
					if (!Convert.IsDBNull(value))
					{
						return this._column.ConvertObjectToXml(value);
					}
					return string.Empty;
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x0004FAEC File Offset: 0x0004EEEC
		internal string BaseURI
		{
			get
			{
				this.RealFoliate();
				if (this._node != null)
				{
					return this._node.BaseURI;
				}
				return string.Empty;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x0004FB18 File Offset: 0x0004EF18
		internal string XmlLang
		{
			get
			{
				this.RealFoliate();
				XmlNode xmlNode = this._node;
				while (xmlNode != null)
				{
					XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
					if (xmlBoundElement != null)
					{
						if (xmlBoundElement.ElementState == ElementState.Defoliated)
						{
							DataRow row = xmlBoundElement.Row;
							using (IEnumerator enumerator = row.Table.Columns.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									object obj = enumerator.Current;
									DataColumn dataColumn = (DataColumn)obj;
									if (dataColumn.Prefix == "xml" && dataColumn.EncodedColumnName == "lang")
									{
										object obj2 = row[dataColumn];
										if (obj2 == DBNull.Value)
										{
											break;
										}
										return (string)obj2;
									}
								}
								goto IL_D3;
							}
						}
						if (xmlBoundElement.HasAttribute("xml:lang"))
						{
							return xmlBoundElement.GetAttribute("xml:lang");
						}
					}
					IL_D3:
					if (xmlNode.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0004FC40 File Offset: 0x0004F040
		private XmlBoundElement GetRowElement()
		{
			XmlBoundElement result;
			if (this._column != null)
			{
				result = (this._node as XmlBoundElement);
				return result;
			}
			this._doc.Mapper.GetRegion(this._node, out result);
			return result;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0004FC80 File Offset: 0x0004F080
		private DataRow Row
		{
			get
			{
				XmlBoundElement rowElement = this.GetRowElement();
				if (rowElement == null)
				{
					return null;
				}
				return rowElement.Row;
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0004FCA0 File Offset: 0x0004F0A0
		internal bool MoveTo(XPathNodePointer pointer)
		{
			if (this._doc != pointer._doc)
			{
				return false;
			}
			this._node = pointer._node;
			this._column = pointer._column;
			this._fOnValue = pointer._fOnValue;
			this._bNeedFoliate = pointer._bNeedFoliate;
			return true;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0004FCF0 File Offset: 0x0004F0F0
		private void MoveTo(XmlNode node)
		{
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0004FD14 File Offset: 0x0004F114
		private void MoveTo(XmlNode node, DataColumn column, bool _fOnValue)
		{
			this._node = node;
			this._column = column;
			this._fOnValue = _fOnValue;
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0004FD38 File Offset: 0x0004F138
		private bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0004FD60 File Offset: 0x0004F160
		private int ColumnCount(DataRow row, bool fAttribute)
		{
			DataColumn dataColumn = null;
			int num = 0;
			while ((dataColumn = this.NextColumn(row, dataColumn, fAttribute)) != null)
			{
				if (dataColumn.Namespace != XPathNodePointer.s_strReservedXmlns)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x0004FD98 File Offset: 0x0004F198
		internal int AttributeCount
		{
			get
			{
				this.RealFoliate();
				if (this._node == null || this._column != null || this._node.NodeType != XmlNodeType.Element)
				{
					return 0;
				}
				if (!this.IsFoliated(this._node))
				{
					return this.ColumnCount(this.Row, true);
				}
				int num = 0;
				foreach (object obj in this._node.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (xmlAttribute.NamespaceURI != XPathNodePointer.s_strReservedXmlns)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0004FE5C File Offset: 0x0004F25C
		internal DataColumn NextColumn(DataRow row, DataColumn col, bool fAttribute)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				return null;
			}
			DataTable table = row.Table;
			DataColumnCollection columns = table.Columns;
			int i = (col != null) ? (col.Ordinal + 1) : 0;
			int count = columns.Count;
			DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
			while (i < count)
			{
				DataColumn dataColumn = columns[i];
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && !Convert.IsDBNull(row[dataColumn, version]))
				{
					return dataColumn;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0004FEF0 File Offset: 0x0004F2F0
		internal DataColumn PreviousColumn(DataRow row, DataColumn col, bool fAttribute)
		{
			if (row.RowState == DataRowState.Deleted)
			{
				return null;
			}
			DataTable table = row.Table;
			DataColumnCollection columns = table.Columns;
			int i = (col != null) ? (col.Ordinal - 1) : (columns.Count - 1);
			int count = columns.Count;
			DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
			while (i >= 0)
			{
				DataColumn dataColumn = columns[i];
				if (!this._doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && !Convert.IsDBNull(row[dataColumn, version]))
				{
					return dataColumn;
				}
				i--;
			}
			return null;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0004FF8C File Offset: 0x0004F38C
		internal bool MoveToAttribute(string localName, string namespaceURI)
		{
			this.RealFoliate();
			if (namespaceURI == XPathNodePointer.s_strReservedXmlns)
			{
				return false;
			}
			if (this._node != null && (this._column == null || this._column.ColumnMapping == MappingType.Attribute) && this._node.NodeType == XmlNodeType.Element)
			{
				if (!this.IsFoliated(this._node))
				{
					DataColumn dataColumn = null;
					while ((dataColumn = this.NextColumn(this.Row, dataColumn, true)) != null)
					{
						if (dataColumn.EncodedColumnName == localName && dataColumn.Namespace == namespaceURI)
						{
							this.MoveTo(this._node, dataColumn, false);
							return true;
						}
					}
				}
				else
				{
					XmlNode namedItem = this._node.Attributes.GetNamedItem(localName, namespaceURI);
					if (namedItem != null)
					{
						this.MoveTo(namedItem, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00050054 File Offset: 0x0004F454
		internal bool MoveToNextAttribute(bool bFirst)
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (bFirst && (this._column != null || this._node.NodeType != XmlNodeType.Element))
				{
					return false;
				}
				if (!bFirst)
				{
					if (this._column != null && this._column.ColumnMapping != MappingType.Attribute)
					{
						return false;
					}
					if (this._column == null && this._node.NodeType != XmlNodeType.Attribute)
					{
						return false;
					}
				}
				if (!this.IsFoliated(this._node))
				{
					DataColumn dataColumn = this._column;
					while ((dataColumn = this.NextColumn(this.Row, dataColumn, true)) != null)
					{
						if (dataColumn.Namespace != XPathNodePointer.s_strReservedXmlns)
						{
							this.MoveTo(this._node, dataColumn, false);
							return true;
						}
					}
					return false;
				}
				if (bFirst)
				{
					XmlAttributeCollection attributes = this._node.Attributes;
					using (IEnumerator enumerator = attributes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							if (xmlAttribute.NamespaceURI != XPathNodePointer.s_strReservedXmlns)
							{
								this.MoveTo(xmlAttribute, null, false);
								return true;
							}
						}
						return false;
					}
				}
				XmlAttributeCollection attributes2 = ((XmlAttribute)this._node).OwnerElement.Attributes;
				bool flag = false;
				foreach (object obj2 in attributes2)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj2;
					if (flag && xmlAttribute2.NamespaceURI != XPathNodePointer.s_strReservedXmlns)
					{
						this.MoveTo(xmlAttribute2, null, false);
						return true;
					}
					if (xmlAttribute2 == this._node)
					{
						flag = true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00050230 File Offset: 0x0004F630
		private bool IsValidChild(XmlNode parent, XmlNode child)
		{
			int num = XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)child.NodeType];
			if (num == -1)
			{
				return false;
			}
			int num2 = XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType];
			if (num2 != 0)
			{
				return num2 == 1 && (num == 1 || num == 4 || num == 8 || num == 6 || num == 5 || num == 7);
			}
			return num == 1 || num == 8 || num == 7;
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00050294 File Offset: 0x0004F694
		private bool IsValidChild(XmlNode parent, DataColumn c)
		{
			int num = XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType];
			if (num != 0)
			{
				return num == 1 && (c.ColumnMapping == MappingType.Element || c.ColumnMapping == MappingType.SimpleContent);
			}
			return c.ColumnMapping == MappingType.Element;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000502D8 File Offset: 0x0004F6D8
		internal bool MoveToNextSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue)
					{
						return false;
					}
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, this._column, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
					XmlNode xmlNode = this._doc.SafeFirstChild(this._node);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode2 = this._node;
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode == null)
					{
						return false;
					}
					bool flag = XmlDataDocument.IsTextNode(this._node.NodeType);
					do
					{
						xmlNode2 = this._doc.SafeNextSibling(xmlNode2);
					}
					while ((xmlNode2 != null && flag && XmlDataDocument.IsTextNode(xmlNode2.NodeType)) || (xmlNode2 != null && !this.IsValidChild(parentNode, xmlNode2)));
					if (xmlNode2 != null)
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x000503E4 File Offset: 0x0004F7E4
		internal bool MoveToPreviousSibling()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue)
					{
						return false;
					}
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.PreviousColumn(row, this._column, false); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				else
				{
					XmlNode xmlNode = this._node;
					XmlNode parentNode = this._node.ParentNode;
					if (parentNode == null)
					{
						return false;
					}
					bool flag = XmlDataDocument.IsTextNode(this._node.NodeType);
					do
					{
						xmlNode = this._doc.SafePreviousSibling(xmlNode);
					}
					while ((xmlNode != null && flag && XmlDataDocument.IsTextNode(xmlNode.NodeType)) || (xmlNode != null && !this.IsValidChild(parentNode, xmlNode)));
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
					if (!this.IsFoliated(parentNode) && parentNode is XmlBoundElement)
					{
						DataRow row2 = ((XmlBoundElement)parentNode).Row;
						if (row2 != null)
						{
							DataColumn dataColumn2 = this.PreviousColumn(row2, null, false);
							if (dataColumn2 != null)
							{
								this.MoveTo(parentNode, dataColumn2, this._doc.IsTextOnly(dataColumn2));
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00050518 File Offset: 0x0004F918
		internal bool MoveToFirst()
		{
			this.RealFoliate();
			if (this._node != null)
			{
				DataRow dataRow = null;
				XmlNode xmlNode;
				if (this._column != null)
				{
					dataRow = this.Row;
					xmlNode = this._node;
				}
				else
				{
					xmlNode = this._node.ParentNode;
					if (xmlNode == null)
					{
						return false;
					}
					if (!this.IsFoliated(xmlNode) && xmlNode is XmlBoundElement)
					{
						dataRow = ((XmlBoundElement)xmlNode).Row;
					}
				}
				if (dataRow != null)
				{
					for (DataColumn dataColumn = this.NextColumn(dataRow, null, false); dataColumn != null; dataColumn = this.NextColumn(dataRow, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				for (XmlNode xmlNode2 = this._doc.SafeFirstChild(xmlNode); xmlNode2 != null; xmlNode2 = this._doc.SafeNextSibling(xmlNode2))
				{
					if (this.IsValidChild(xmlNode, xmlNode2))
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x000505FC File Offset: 0x0004F9FC
		internal bool HasChildren
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return false;
				}
				if (this._column != null)
				{
					return this._column.ColumnMapping != MappingType.Attribute && this._column.ColumnMapping != MappingType.Hidden && !this._fOnValue;
				}
				if (!this.IsFoliated(this._node))
				{
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, null, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							return true;
						}
					}
				}
				for (XmlNode xmlNode = this._doc.SafeFirstChild(this._node); xmlNode != null; xmlNode = this._doc.SafeNextSibling(xmlNode))
				{
					if (this.IsValidChild(this._node, xmlNode))
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000506C0 File Offset: 0x0004FAC0
		internal bool MoveToFirstChild()
		{
			this.RealFoliate();
			if (this._node == null)
			{
				return false;
			}
			if (this._column == null)
			{
				if (!this.IsFoliated(this._node))
				{
					DataRow row = this.Row;
					for (DataColumn dataColumn = this.NextColumn(row, null, false); dataColumn != null; dataColumn = this.NextColumn(row, dataColumn, false))
					{
						if (this.IsValidChild(this._node, dataColumn))
						{
							this.MoveTo(this._node, dataColumn, this._doc.IsTextOnly(dataColumn));
							return true;
						}
					}
				}
				for (XmlNode xmlNode = this._doc.SafeFirstChild(this._node); xmlNode != null; xmlNode = this._doc.SafeNextSibling(xmlNode))
				{
					if (this.IsValidChild(this._node, xmlNode))
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				return false;
			}
			if (this._column.ColumnMapping == MappingType.Attribute || this._column.ColumnMapping == MappingType.Hidden)
			{
				return false;
			}
			if (this._fOnValue)
			{
				return false;
			}
			this._fOnValue = true;
			return true;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000507AC File Offset: 0x0004FBAC
		internal bool MoveToParent()
		{
			this.RealFoliate();
			if (this.NodeType == XPathNodeType.Namespace)
			{
				this.MoveTo(this._parentOfNS);
				return true;
			}
			if (this._node != null)
			{
				if (this._column != null)
				{
					if (this._fOnValue && !this._doc.IsTextOnly(this._column))
					{
						this.MoveTo(this._node, this._column, false);
						return true;
					}
					this.MoveTo(this._node, null, false);
					return true;
				}
				else
				{
					XmlNode xmlNode;
					if (this._node.NodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)this._node).OwnerElement;
					}
					else
					{
						xmlNode = this._node.ParentNode;
					}
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00050868 File Offset: 0x0004FC68
		private XmlNode GetParent(XmlNode node)
		{
			XPathNodeType xpathNodeType = this.ConvertNodeType(node);
			if (xpathNodeType == XPathNodeType.Namespace)
			{
				return this._parentOfNS;
			}
			if (xpathNodeType == XPathNodeType.Attribute)
			{
				return ((XmlAttribute)node).OwnerElement;
			}
			return node.ParentNode;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000508A0 File Offset: 0x0004FCA0
		internal void MoveToRoot()
		{
			XmlNode node = this._node;
			for (XmlNode xmlNode = this._node; xmlNode != null; xmlNode = this.GetParent(xmlNode))
			{
				node = xmlNode;
			}
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000508E0 File Offset: 0x0004FCE0
		internal bool IsSamePosition(XPathNodePointer pointer)
		{
			this.RealFoliate();
			pointer.RealFoliate();
			if (this._column == null && pointer._column == null)
			{
				return pointer._node == this._node && pointer._parentOfNS == this._parentOfNS;
			}
			return pointer._doc == this._doc && pointer._node == this._node && pointer._column == this._column && pointer._fOnValue == this._fOnValue && pointer._parentOfNS == this._parentOfNS;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00050970 File Offset: 0x0004FD70
		private XmlNodeOrder CompareNamespacePosition(XPathNodePointer other)
		{
			XPathNodePointer xpathNodePointer = this.Clone((DataDocumentXPathNavigator)this._owner.Target);
			XPathNodePointer xpathNodePointer2 = other.Clone((DataDocumentXPathNavigator)other._owner.Target);
			while (xpathNodePointer.MoveToNextNamespace(XPathNamespaceScope.All))
			{
				if (xpathNodePointer.IsSamePosition(other))
				{
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.After;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000509C4 File Offset: 0x0004FDC4
		private static XmlNode GetRoot(XmlNode node, ref int depth)
		{
			depth = 0;
			XmlNode xmlNode = node;
			XmlNode xmlNode2 = (xmlNode.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode).OwnerElement : xmlNode.ParentNode;
			while (xmlNode2 != null)
			{
				xmlNode = xmlNode2;
				xmlNode2 = xmlNode.ParentNode;
				depth++;
			}
			return xmlNode;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00050A08 File Offset: 0x0004FE08
		internal XmlNodeOrder ComparePosition(XPathNodePointer other)
		{
			this.RealFoliate();
			other.RealFoliate();
			if (this.IsSamePosition(other))
			{
				return XmlNodeOrder.Same;
			}
			XmlNode xmlNode;
			XmlNode xmlNode2;
			if (this.NodeType == XPathNodeType.Namespace && other.NodeType == XPathNodeType.Namespace)
			{
				if (this._parentOfNS == other._parentOfNS)
				{
					return this.CompareNamespacePosition(other);
				}
				xmlNode = this._parentOfNS;
				xmlNode2 = other._parentOfNS;
			}
			else if (this.NodeType == XPathNodeType.Namespace)
			{
				if (this._parentOfNS == other._node)
				{
					if (other._column == null)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
				else
				{
					xmlNode = this._parentOfNS;
					xmlNode2 = other._node;
				}
			}
			else if (other.NodeType == XPathNodeType.Namespace)
			{
				if (this._node == other._parentOfNS)
				{
					if (this._column == null)
					{
						return XmlNodeOrder.Before;
					}
					return XmlNodeOrder.After;
				}
				else
				{
					xmlNode = this._node;
					xmlNode2 = other._parentOfNS;
				}
			}
			else if (this._node == other._node)
			{
				if (this._column == other._column)
				{
					if (this._fOnValue)
					{
						return XmlNodeOrder.After;
					}
					return XmlNodeOrder.Before;
				}
				else
				{
					if (this._column == null)
					{
						return XmlNodeOrder.Before;
					}
					if (other._column == null)
					{
						return XmlNodeOrder.After;
					}
					if (this._column.Ordinal < other._column.Ordinal)
					{
						return XmlNodeOrder.Before;
					}
					return XmlNodeOrder.After;
				}
			}
			else
			{
				xmlNode = this._node;
				xmlNode2 = other._node;
			}
			if (xmlNode == null || xmlNode2 == null)
			{
				return XmlNodeOrder.Unknown;
			}
			int num = -1;
			int num2 = -1;
			XmlNode root = XPathNodePointer.GetRoot(xmlNode, ref num);
			XmlNode root2 = XPathNodePointer.GetRoot(xmlNode2, ref num2);
			if (root != root2)
			{
				return XmlNodeOrder.Unknown;
			}
			if (num > num2)
			{
				while (xmlNode != null && num > num2)
				{
					xmlNode = ((xmlNode.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode).OwnerElement : xmlNode.ParentNode);
					num--;
				}
				if (xmlNode == xmlNode2)
				{
					return XmlNodeOrder.After;
				}
			}
			else if (num2 > num)
			{
				while (xmlNode2 != null && num2 > num)
				{
					xmlNode2 = ((xmlNode2.NodeType == XmlNodeType.Attribute) ? ((XmlAttribute)xmlNode2).OwnerElement : xmlNode2.ParentNode);
					num2--;
				}
				if (xmlNode == xmlNode2)
				{
					return XmlNodeOrder.Before;
				}
			}
			XmlNode xmlNode3 = this.GetParent(xmlNode);
			XmlNode xmlNode4 = this.GetParent(xmlNode2);
			while (xmlNode3 != null && xmlNode4 != null)
			{
				if (xmlNode3 == xmlNode4)
				{
					while (xmlNode != null)
					{
						XmlNode nextSibling = xmlNode.NextSibling;
						if (nextSibling == xmlNode2)
						{
							return XmlNodeOrder.Before;
						}
						xmlNode = nextSibling;
					}
					return XmlNodeOrder.After;
				}
				xmlNode = xmlNode3;
				xmlNode2 = xmlNode4;
				xmlNode3 = xmlNode.ParentNode;
				xmlNode4 = xmlNode2.ParentNode;
			}
			return XmlNodeOrder.Unknown;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00050C20 File Offset: 0x00050020
		internal XmlNode Node
		{
			get
			{
				this.RealFoliate();
				if (this._node == null)
				{
					return null;
				}
				XmlBoundElement rowElement = this.GetRowElement();
				if (rowElement != null)
				{
					bool isFoliationEnabled = this._doc.IsFoliationEnabled;
					this._doc.IsFoliationEnabled = true;
					this._doc.Foliate(rowElement, ElementState.StrongFoliation);
					this._doc.IsFoliationEnabled = isFoliationEnabled;
				}
				this.RealFoliate();
				return this._node;
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00050C84 File Offset: 0x00050084
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this._node;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00050CA0 File Offset: 0x000500A0
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this._column;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00050CBC File Offset: 0x000500BC
		void IXmlDataVirtualNode.OnFoliated(XmlNode foliatedNode)
		{
			if (this._node == foliatedNode)
			{
				if (this._column == null)
				{
					return;
				}
				this._bNeedFoliate = true;
			}
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x00050CE4 File Offset: 0x000500E4
		private void RealFoliate()
		{
			if (!this._bNeedFoliate)
			{
				return;
			}
			this._bNeedFoliate = false;
			XmlNode xmlNode;
			if (this._doc.IsTextOnly(this._column))
			{
				xmlNode = this._node.FirstChild;
			}
			else
			{
				if (this._column.ColumnMapping == MappingType.Attribute)
				{
					xmlNode = this._node.Attributes.GetNamedItem(this._column.EncodedColumnName, this._column.Namespace);
				}
				else
				{
					xmlNode = this._node.FirstChild;
					while (xmlNode != null && (!(xmlNode.LocalName == this._column.EncodedColumnName) || !(xmlNode.NamespaceURI == this._column.Namespace)))
					{
						xmlNode = xmlNode.NextSibling;
					}
				}
				if (xmlNode != null && this._fOnValue)
				{
					xmlNode = xmlNode.FirstChild;
				}
			}
			if (xmlNode == null)
			{
				throw new InvalidOperationException(Res.GetString("DataDom_Foliation"));
			}
			this._node = xmlNode;
			this._column = null;
			this._fOnValue = false;
			this._bNeedFoliate = false;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00050DE8 File Offset: 0x000501E8
		private string GetNamespace(XmlBoundElement be, string name)
		{
			if (be == null)
			{
				return null;
			}
			if (be.IsFoliated)
			{
				XmlAttribute attributeNode = be.GetAttributeNode(name, XPathNodePointer.s_strReservedXmlns);
				if (attributeNode != null)
				{
					return attributeNode.Value;
				}
				return null;
			}
			else
			{
				DataRow row = be.Row;
				if (row == null)
				{
					return null;
				}
				for (DataColumn dataColumn = this.PreviousColumn(row, null, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
				{
					if (dataColumn.Namespace == XPathNodePointer.s_strReservedXmlns)
					{
						DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
						return dataColumn.ConvertObjectToXml(row[dataColumn, version]);
					}
				}
				return null;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00050E7C File Offset: 0x0005027C
		internal string GetNamespace(string name)
		{
			if (name == "xml")
			{
				return XPathNodePointer.s_strReservedXml;
			}
			if (name == "xmlns")
			{
				return XPathNodePointer.s_strReservedXmlns;
			}
			if (name != null && name.Length == 0)
			{
				name = "xmlns";
			}
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			XmlNodeType nodeType = xmlNode.NodeType;
			while (xmlNode != null)
			{
				while (xmlNode != null && (nodeType = xmlNode.NodeType) != XmlNodeType.Element)
				{
					if (nodeType == XmlNodeType.Attribute)
					{
						xmlNode = ((XmlAttribute)xmlNode).OwnerElement;
					}
					else
					{
						xmlNode = xmlNode.ParentNode;
					}
				}
				if (xmlNode != null)
				{
					string @namespace = this.GetNamespace((XmlBoundElement)xmlNode, name);
					if (@namespace != null)
					{
						return @namespace;
					}
					xmlNode = xmlNode.ParentNode;
				}
			}
			return string.Empty;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00050F28 File Offset: 0x00050328
		internal bool MoveToNamespace(string name)
		{
			this._parentOfNS = (this._node as XmlBoundElement);
			if (this._parentOfNS == null)
			{
				return false;
			}
			string text = name;
			if (text == "xmlns")
			{
				text = "xmlns:xmlns";
			}
			if (text == null || text.Length == 0)
			{
			}
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			XmlNodeType nodeType = xmlNode.NodeType;
			while (xmlNode != null)
			{
				XmlBoundElement xmlBoundElement = xmlNode as XmlBoundElement;
				if (xmlBoundElement != null)
				{
					if (xmlBoundElement.IsFoliated)
					{
						XmlAttribute attributeNode = xmlBoundElement.GetAttributeNode(name, XPathNodePointer.s_strReservedXmlns);
						if (attributeNode != null)
						{
							this.MoveTo(attributeNode);
							return true;
						}
					}
					else
					{
						DataRow row = xmlBoundElement.Row;
						if (row == null)
						{
							return false;
						}
						for (DataColumn dataColumn = this.PreviousColumn(row, null, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
						{
							if (dataColumn.Namespace == XPathNodePointer.s_strReservedXmlns && dataColumn.ColumnName == name)
							{
								this.MoveTo(xmlBoundElement, dataColumn, false);
								return true;
							}
						}
					}
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
			}
			this._parentOfNS = null;
			return false;
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0005103C File Offset: 0x0005043C
		private bool MoveToNextNamespace(XmlBoundElement be, DataColumn col, XmlAttribute curAttr)
		{
			if (be != null)
			{
				if (be.IsFoliated)
				{
					XmlAttributeCollection attributes = be.Attributes;
					bool flag = false;
					if (curAttr == null)
					{
						flag = true;
					}
					int i = attributes.Count;
					while (i > 0)
					{
						i--;
						XmlAttribute xmlAttribute = attributes[i];
						if (flag && xmlAttribute.NamespaceURI == XPathNodePointer.s_strReservedXmlns && !this.DuplicateNS(be, xmlAttribute.LocalName))
						{
							this.MoveTo(xmlAttribute);
							return true;
						}
						if (xmlAttribute == curAttr)
						{
							flag = true;
						}
					}
				}
				else
				{
					DataRow row = be.Row;
					if (row == null)
					{
						return false;
					}
					for (DataColumn dataColumn = this.PreviousColumn(row, col, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
					{
						if (dataColumn.Namespace == XPathNodePointer.s_strReservedXmlns && !this.DuplicateNS(be, dataColumn.ColumnName))
						{
							this.MoveTo(be, dataColumn, false);
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00051110 File Offset: 0x00050510
		internal bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			this.RealFoliate();
			this._parentOfNS = (this._node as XmlBoundElement);
			if (this._parentOfNS == null)
			{
				return false;
			}
			XmlNode xmlNode = this._node;
			while (xmlNode != null)
			{
				XmlBoundElement be = xmlNode as XmlBoundElement;
				if (this.MoveToNextNamespace(be, null, null))
				{
					return true;
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					IL_72:
					this._parentOfNS = null;
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
			}
			if (namespaceScope == XPathNamespaceScope.All)
			{
				this.MoveTo(this._doc.attrXml, null, false);
				return true;
			}
			goto IL_72;
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x00051198 File Offset: 0x00050598
		private bool DuplicateNS(XmlBoundElement endElem, string lname)
		{
			if (this._parentOfNS == null || endElem == null)
			{
				return false;
			}
			XmlBoundElement xmlBoundElement = this._parentOfNS;
			while (xmlBoundElement != null && xmlBoundElement != endElem)
			{
				if (this.GetNamespace(xmlBoundElement, lname) != null)
				{
					return true;
				}
				XmlNode xmlNode = xmlBoundElement;
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType != XmlNodeType.Element);
				xmlBoundElement = (xmlNode as XmlBoundElement);
			}
			return false;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000511F0 File Offset: 0x000505F0
		internal bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			this.RealFoliate();
			XmlNode xmlNode = this._node;
			if (this._column != null)
			{
				if (namespaceScope == XPathNamespaceScope.Local && this._parentOfNS != this._node)
				{
					return false;
				}
				XmlBoundElement xmlBoundElement = this._node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				for (DataColumn dataColumn = this.PreviousColumn(row, this._column, true); dataColumn != null; dataColumn = this.PreviousColumn(row, dataColumn, true))
				{
					if (dataColumn.Namespace == XPathNodePointer.s_strReservedXmlns)
					{
						this.MoveTo(xmlBoundElement, dataColumn, false);
						return true;
					}
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						break;
					}
				}
				while (xmlNode.NodeType != XmlNodeType.Element);
			}
			else if (this._node.NodeType == XmlNodeType.Attribute)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this._node;
				xmlNode = xmlAttribute.OwnerElement;
				if (xmlNode == null)
				{
					return false;
				}
				if (namespaceScope == XPathNamespaceScope.Local && this._parentOfNS != xmlNode)
				{
					return false;
				}
				if (this.MoveToNextNamespace((XmlBoundElement)xmlNode, null, xmlAttribute))
				{
					return true;
				}
				if (namespaceScope == XPathNamespaceScope.Local)
				{
					return false;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
					if (xmlNode == null)
					{
						break;
					}
				}
				while (xmlNode.NodeType != XmlNodeType.Element);
			}
			while (xmlNode != null)
			{
				XmlBoundElement be = xmlNode as XmlBoundElement;
				if (this.MoveToNextNamespace(be, null, null))
				{
					return true;
				}
				do
				{
					xmlNode = xmlNode.ParentNode;
				}
				while (xmlNode != null && xmlNode.NodeType == XmlNodeType.Element);
			}
			if (namespaceScope == XPathNamespaceScope.All)
			{
				this.MoveTo(this._doc.attrXml, null, false);
				return true;
			}
			return false;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00051348 File Offset: 0x00050748
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this._column != null)
			{
				XmlBoundElement xmlBoundElement = this._node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				int num = (row.RowState == DataRowState.Detached) ? 1024 : 512;
			}
			DataColumn column = this._column;
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x00051390 File Offset: 0x00050790
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000513A4 File Offset: 0x000507A4
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._owner.IsAlive;
		}

		// Token: 0x0400029D RID: 669
		private WeakReference _owner;

		// Token: 0x0400029E RID: 670
		private XmlDataDocument _doc;

		// Token: 0x0400029F RID: 671
		private XmlNode _node;

		// Token: 0x040002A0 RID: 672
		private DataColumn _column;

		// Token: 0x040002A1 RID: 673
		private bool _fOnValue;

		// Token: 0x040002A2 RID: 674
		internal XmlBoundElement _parentOfNS;

		// Token: 0x040002A3 RID: 675
		internal static int[] xmlNodeType_To_XpathNodeType_Map;

		// Token: 0x040002A4 RID: 676
		internal static string s_strReservedXmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x040002A5 RID: 677
		internal static string s_strReservedXml = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x040002A6 RID: 678
		internal static string s_strXmlNS = "xmlns";

		// Token: 0x040002A7 RID: 679
		private bool _bNeedFoliate;
	}
}

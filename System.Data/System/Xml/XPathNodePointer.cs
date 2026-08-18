using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x0200038E RID: 910
	internal sealed class XPathNodePointer : IXmlDataVirtualNode
	{
		// Token: 0x0600308E RID: 12430 RVA: 0x002DAE78 File Offset: 0x002DA278
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

		// Token: 0x0600308F RID: 12431 RVA: 0x002DAF48 File Offset: 0x002DA348
		private XPathNodeType DecideXPNodeTypeForTextNodes(XmlNode node)
		{
			XPathNodeType result = XPathNodeType.Whitespace;
			while (node != null)
			{
				XmlNodeType nodeType = node.NodeType;
				switch (nodeType)
				{
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
					return XPathNodeType.Text;
				default:
					switch (nodeType)
					{
					case XmlNodeType.Whitespace:
						break;
					case XmlNodeType.SignificantWhitespace:
						result = XPathNodeType.SignificantWhitespace;
						break;
					default:
						return result;
					}
					node = this._doc.SafeNextSibling(node);
					break;
				}
			}
			return result;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x002DAFA8 File Offset: 0x002DA3A8
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

		// Token: 0x06003091 RID: 12433 RVA: 0x002DAFF8 File Offset: 0x002DA3F8
		private bool IsNamespaceNode(XmlNodeType nt, string ns)
		{
			return nt == XmlNodeType.Attribute && ns == XPathNodePointer.s_strReservedXmlns;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x002DB028 File Offset: 0x002DA428
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XmlDataDocument doc, XmlNode node) : this(owner, doc, node, null, false, null)
		{
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x002DB048 File Offset: 0x002DA448
		internal XPathNodePointer(DataDocumentXPathNavigator owner, XPathNodePointer pointer) : this(owner, pointer._doc, pointer._node, pointer._column, pointer._fOnValue, pointer._parentOfNS)
		{
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x002DB088 File Offset: 0x002DA488
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

		// Token: 0x06003095 RID: 12437 RVA: 0x002DB0E8 File Offset: 0x002DA4E8
		internal XPathNodePointer Clone(DataDocumentXPathNavigator owner)
		{
			this.RealFoliate();
			return new XPathNodePointer(owner, this);
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x002DB108 File Offset: 0x002DA508
		internal bool IsEmptyElement
		{
			get
			{
				return this._node != null && this._column == null && this._node.NodeType == XmlNodeType.Element && ((XmlElement)this._node).IsEmpty;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x002DB148 File Offset: 0x002DA548
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

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x002DB1B8 File Offset: 0x002DA5B8
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

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x002DB268 File Offset: 0x002DA668
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

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x002DB328 File Offset: 0x002DA728
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

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x002DB3C8 File Offset: 0x002DA7C8
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x002DB428 File Offset: 0x002DA828
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

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x002DB528 File Offset: 0x002DA928
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

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x002DB5D8 File Offset: 0x002DA9D8
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

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x002DB608 File Offset: 0x002DAA08
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
										if (obj2 != DBNull.Value)
										{
											return (string)obj2;
										}
										break;
									}
								}
								goto IL_D1;
							}
						}
						if (xmlBoundElement.HasAttribute("xml:lang"))
						{
							return xmlBoundElement.GetAttribute("xml:lang");
						}
					}
					IL_D1:
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

		// Token: 0x060030A0 RID: 12448 RVA: 0x002DB738 File Offset: 0x002DAB38
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x002DB778 File Offset: 0x002DAB78
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

		// Token: 0x060030A2 RID: 12450 RVA: 0x002DB798 File Offset: 0x002DAB98
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

		// Token: 0x060030A3 RID: 12451 RVA: 0x002DB7E8 File Offset: 0x002DABE8
		private void MoveTo(XmlNode node)
		{
			this._node = node;
			this._column = null;
			this._fOnValue = false;
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x002DB818 File Offset: 0x002DAC18
		private void MoveTo(XmlNode node, DataColumn column, bool _fOnValue)
		{
			this._node = node;
			this._column = column;
			this._fOnValue = _fOnValue;
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x002DB848 File Offset: 0x002DAC48
		private bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x002DB878 File Offset: 0x002DAC78
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

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x002DB8B8 File Offset: 0x002DACB8
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

		// Token: 0x060030A8 RID: 12456 RVA: 0x002DB988 File Offset: 0x002DAD88
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

		// Token: 0x060030A9 RID: 12457 RVA: 0x002DBA28 File Offset: 0x002DAE28
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

		// Token: 0x060030AA RID: 12458 RVA: 0x002DBAC8 File Offset: 0x002DAEC8
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

		// Token: 0x060030AB RID: 12459 RVA: 0x002DBB98 File Offset: 0x002DAF98
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

		// Token: 0x060030AC RID: 12460 RVA: 0x002DBD78 File Offset: 0x002DB178
		private bool IsValidChild(XmlNode parent, XmlNode child)
		{
			int num = XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)child.NodeType];
			if (num == -1)
			{
				return false;
			}
			switch (XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType])
			{
			case 0:
				return num == 1 || num == 8 || num == 7;
			case 1:
				return num == 1 || num == 4 || num == 8 || num == 6 || num == 5 || num == 7;
			default:
				return false;
			}
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x002DBDE8 File Offset: 0x002DB1E8
		private bool IsValidChild(XmlNode parent, DataColumn c)
		{
			switch (XPathNodePointer.xmlNodeType_To_XpathNodeType_Map[(int)parent.NodeType])
			{
			case 0:
				return c.ColumnMapping == MappingType.Element;
			case 1:
				return c.ColumnMapping == MappingType.Element || c.ColumnMapping == MappingType.SimpleContent;
			default:
				return false;
			}
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x002DBE38 File Offset: 0x002DB238
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

		// Token: 0x060030AF RID: 12463 RVA: 0x002DBF48 File Offset: 0x002DB348
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

		// Token: 0x060030B0 RID: 12464 RVA: 0x002DC088 File Offset: 0x002DB488
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

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x002DC178 File Offset: 0x002DB578
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

		// Token: 0x060030B2 RID: 12466 RVA: 0x002DC248 File Offset: 0x002DB648
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

		// Token: 0x060030B3 RID: 12467 RVA: 0x002DC338 File Offset: 0x002DB738
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

		// Token: 0x060030B4 RID: 12468 RVA: 0x002DC3F8 File Offset: 0x002DB7F8
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

		// Token: 0x060030B5 RID: 12469 RVA: 0x002DC438 File Offset: 0x002DB838
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

		// Token: 0x060030B6 RID: 12470 RVA: 0x002DC478 File Offset: 0x002DB878
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

		// Token: 0x060030B7 RID: 12471 RVA: 0x002DC508 File Offset: 0x002DB908
		private XmlNodeOrder CompareNamespacePosition(XPathNodePointer other)
		{
			XPathNodePointer xpathNodePointer = this.Clone((DataDocumentXPathNavigator)this._owner.Target);
			other.Clone((DataDocumentXPathNavigator)other._owner.Target);
			while (xpathNodePointer.MoveToNextNamespace(XPathNamespaceScope.All))
			{
				if (xpathNodePointer.IsSamePosition(other))
				{
					return XmlNodeOrder.Before;
				}
			}
			return XmlNodeOrder.After;
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x002DC568 File Offset: 0x002DB968
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

		// Token: 0x060030B9 RID: 12473 RVA: 0x002DC5B8 File Offset: 0x002DB9B8
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

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x002DC7D8 File Offset: 0x002DBBD8
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

		// Token: 0x060030BB RID: 12475 RVA: 0x002DC848 File Offset: 0x002DBC48
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this._node;
		}

		// Token: 0x060030BC RID: 12476 RVA: 0x002DC868 File Offset: 0x002DBC68
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this._column;
		}

		// Token: 0x060030BD RID: 12477 RVA: 0x002DC888 File Offset: 0x002DBC88
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

		// Token: 0x060030BE RID: 12478 RVA: 0x002DC8B8 File Offset: 0x002DBCB8
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

		// Token: 0x060030BF RID: 12479 RVA: 0x002DC9C8 File Offset: 0x002DBDC8
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

		// Token: 0x060030C0 RID: 12480 RVA: 0x002DCA68 File Offset: 0x002DBE68
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

		// Token: 0x060030C1 RID: 12481 RVA: 0x002DCB18 File Offset: 0x002DBF18
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

		// Token: 0x060030C2 RID: 12482 RVA: 0x002DCC38 File Offset: 0x002DC038
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

		// Token: 0x060030C3 RID: 12483 RVA: 0x002DCD18 File Offset: 0x002DC118
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

		// Token: 0x060030C4 RID: 12484 RVA: 0x002DCDA8 File Offset: 0x002DC1A8
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

		// Token: 0x060030C5 RID: 12485 RVA: 0x002DCE08 File Offset: 0x002DC208
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

		// Token: 0x060030C6 RID: 12486 RVA: 0x002DCF68 File Offset: 0x002DC368
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this._column != null)
			{
				XmlBoundElement xmlBoundElement = this._node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				DataRowState rowState = row.RowState;
			}
			DataColumn column = this._column;
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x002DCFA8 File Offset: 0x002DC3A8
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x002DCFC8 File Offset: 0x002DC3C8
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._owner.IsAlive;
		}

		// Token: 0x04001DCA RID: 7626
		private WeakReference _owner;

		// Token: 0x04001DCB RID: 7627
		private XmlDataDocument _doc;

		// Token: 0x04001DCC RID: 7628
		private XmlNode _node;

		// Token: 0x04001DCD RID: 7629
		private DataColumn _column;

		// Token: 0x04001DCE RID: 7630
		private bool _fOnValue;

		// Token: 0x04001DCF RID: 7631
		internal XmlBoundElement _parentOfNS;

		// Token: 0x04001DD0 RID: 7632
		internal static int[] xmlNodeType_To_XpathNodeType_Map;

		// Token: 0x04001DD1 RID: 7633
		internal static string s_strReservedXmlns = "http://www.w3.org/2000/xmlns/";

		// Token: 0x04001DD2 RID: 7634
		internal static string s_strReservedXml = "http://www.w3.org/XML/1998/namespace";

		// Token: 0x04001DD3 RID: 7635
		internal static string s_strXmlNS = "xmlns";

		// Token: 0x04001DD4 RID: 7636
		private bool _bNeedFoliate;
	}
}

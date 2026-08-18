using System;
using System.Data;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x02000083 RID: 131
	internal sealed class DataPointer : IXmlDataVirtualNode
	{
		// Token: 0x06000638 RID: 1592 RVA: 0x00049C0C File Offset: 0x0004900C
		internal DataPointer(XmlDataDocument doc, XmlNode node)
		{
			this.doc = doc;
			this.node = node;
			this.column = null;
			this.fOnValue = false;
			this.bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00049C4C File Offset: 0x0004904C
		internal DataPointer(DataPointer pointer)
		{
			this.doc = pointer.doc;
			this.node = pointer.node;
			this.column = pointer.column;
			this.fOnValue = pointer.fOnValue;
			this.bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00049CA0 File Offset: 0x000490A0
		internal void AddPointer()
		{
			this.doc.AddPointer(this);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00049CBC File Offset: 0x000490BC
		private XmlBoundElement GetRowElement()
		{
			XmlBoundElement result;
			if (this.column != null)
			{
				result = (this.node as XmlBoundElement);
				return result;
			}
			this.doc.Mapper.GetRegion(this.node, out result);
			return result;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600063C RID: 1596 RVA: 0x00049CFC File Offset: 0x000490FC
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

		// Token: 0x0600063D RID: 1597 RVA: 0x00049D1C File Offset: 0x0004911C
		private static bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00049D44 File Offset: 0x00049144
		internal void MoveTo(DataPointer pointer)
		{
			this.doc = pointer.doc;
			this.node = pointer.node;
			this.column = pointer.column;
			this.fOnValue = pointer.fOnValue;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00049D84 File Offset: 0x00049184
		private void MoveTo(XmlNode node)
		{
			this.node = node;
			this.column = null;
			this.fOnValue = false;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00049DA8 File Offset: 0x000491A8
		private void MoveTo(XmlNode node, DataColumn column, bool fOnValue)
		{
			this.node = node;
			this.column = column;
			this.fOnValue = fOnValue;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00049DCC File Offset: 0x000491CC
		private DataColumn NextColumn(DataRow row, DataColumn col, bool fAttribute, bool fNulls)
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
				if (!this.doc.IsNotMapped(dataColumn) && dataColumn.ColumnMapping == MappingType.Attribute == fAttribute && (fNulls || !Convert.IsDBNull(row[dataColumn, version])))
				{
					return dataColumn;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00049E64 File Offset: 0x00049264
		private DataColumn NthColumn(DataRow row, bool fAttribute, int iColumn, bool fNulls)
		{
			DataColumn dataColumn = null;
			checked
			{
				while ((dataColumn = this.NextColumn(row, dataColumn, fAttribute, fNulls)) != null)
				{
					if (iColumn == 0)
					{
						return dataColumn;
					}
					iColumn--;
				}
				return null;
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00049E90 File Offset: 0x00049290
		private int ColumnCount(DataRow row, bool fAttribute, bool fNulls)
		{
			DataColumn col = null;
			int num = 0;
			while ((col = this.NextColumn(row, col, fAttribute, fNulls)) != null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00049EB8 File Offset: 0x000492B8
		internal bool MoveToFirstChild()
		{
			this.RealFoliate();
			if (this.node == null)
			{
				return false;
			}
			if (this.column != null)
			{
				if (this.fOnValue)
				{
					return false;
				}
				this.fOnValue = true;
				return true;
			}
			else
			{
				if (!DataPointer.IsFoliated(this.node))
				{
					DataColumn dataColumn = this.NextColumn(this.Row, null, false, false);
					if (dataColumn != null)
					{
						this.MoveTo(this.node, dataColumn, this.doc.IsTextOnly(dataColumn));
						return true;
					}
				}
				XmlNode xmlNode = this.doc.SafeFirstChild(this.node);
				if (xmlNode != null)
				{
					this.MoveTo(xmlNode);
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00049F4C File Offset: 0x0004934C
		internal bool MoveToNextSibling()
		{
			this.RealFoliate();
			if (this.node != null)
			{
				if (this.column != null)
				{
					if (this.fOnValue && !this.doc.IsTextOnly(this.column))
					{
						return false;
					}
					DataColumn dataColumn = this.NextColumn(this.Row, this.column, false, false);
					if (dataColumn != null)
					{
						this.MoveTo(this.node, dataColumn, false);
						return true;
					}
					XmlNode xmlNode = this.doc.SafeFirstChild(this.node);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode2 = this.doc.SafeNextSibling(this.node);
					if (xmlNode2 != null)
					{
						this.MoveTo(xmlNode2);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00049FF4 File Offset: 0x000493F4
		internal bool MoveToParent()
		{
			this.RealFoliate();
			if (this.node != null)
			{
				if (this.column != null)
				{
					if (this.fOnValue && !this.doc.IsTextOnly(this.column))
					{
						this.MoveTo(this.node, this.column, false);
						return true;
					}
					if (this.column.ColumnMapping != MappingType.Attribute)
					{
						this.MoveTo(this.node, null, false);
						return true;
					}
				}
				else
				{
					XmlNode parentNode = this.node.ParentNode;
					if (parentNode != null)
					{
						this.MoveTo(parentNode);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0004A080 File Offset: 0x00049480
		internal bool MoveToOwnerElement()
		{
			this.RealFoliate();
			if (this.node != null)
			{
				if (this.column != null)
				{
					if (this.fOnValue || this.doc.IsTextOnly(this.column) || this.column.ColumnMapping != MappingType.Attribute)
					{
						return false;
					}
					this.MoveTo(this.node, null, false);
					return true;
				}
				else if (this.node.NodeType == XmlNodeType.Attribute)
				{
					XmlNode ownerElement = ((XmlAttribute)this.node).OwnerElement;
					if (ownerElement != null)
					{
						this.MoveTo(ownerElement, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0004A10C File Offset: 0x0004950C
		internal int AttributeCount
		{
			get
			{
				this.RealFoliate();
				if (this.node == null || this.column != null || this.node.NodeType != XmlNodeType.Element)
				{
					return 0;
				}
				if (!DataPointer.IsFoliated(this.node))
				{
					return this.ColumnCount(this.Row, true, false);
				}
				return this.node.Attributes.Count;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0004A16C File Offset: 0x0004956C
		internal bool MoveToAttribute(int i)
		{
			this.RealFoliate();
			if (i < 0)
			{
				return false;
			}
			if (this.node != null && (this.column == null || this.column.ColumnMapping == MappingType.Attribute) && this.node.NodeType == XmlNodeType.Element)
			{
				if (!DataPointer.IsFoliated(this.node))
				{
					DataColumn dataColumn = this.NthColumn(this.Row, true, i, false);
					if (dataColumn != null)
					{
						this.MoveTo(this.node, dataColumn, false);
						return true;
					}
				}
				else
				{
					XmlNode xmlNode = this.node.Attributes.Item(i);
					if (xmlNode != null)
					{
						this.MoveTo(xmlNode, null, false);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x0004A208 File Offset: 0x00049608
		internal XmlNodeType NodeType
		{
			get
			{
				this.RealFoliate();
				if (this.node == null)
				{
					return XmlNodeType.None;
				}
				if (this.column == null)
				{
					return this.node.NodeType;
				}
				if (this.fOnValue)
				{
					return XmlNodeType.Text;
				}
				if (this.column.ColumnMapping == MappingType.Attribute)
				{
					return XmlNodeType.Attribute;
				}
				return XmlNodeType.Element;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0004A254 File Offset: 0x00049654
		internal string LocalName
		{
			get
			{
				this.RealFoliate();
				if (this.node == null)
				{
					return string.Empty;
				}
				if (this.column == null)
				{
					string localName = this.node.LocalName;
					if (this.IsLocalNameEmpty(this.node.NodeType))
					{
						return string.Empty;
					}
					return localName;
				}
				else
				{
					if (this.fOnValue)
					{
						return string.Empty;
					}
					return this.doc.NameTable.Add(this.column.EncodedColumnName);
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x0004A2D0 File Offset: 0x000496D0
		internal string NamespaceURI
		{
			get
			{
				this.RealFoliate();
				if (this.node == null)
				{
					return string.Empty;
				}
				if (this.column == null)
				{
					return this.node.NamespaceURI;
				}
				if (this.fOnValue)
				{
					return string.Empty;
				}
				return this.doc.NameTable.Add(this.column.Namespace);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0004A330 File Offset: 0x00049730
		internal string Name
		{
			get
			{
				this.RealFoliate();
				if (this.node == null)
				{
					return string.Empty;
				}
				if (this.column == null)
				{
					string name = this.node.Name;
					if (this.IsLocalNameEmpty(this.node.NodeType))
					{
						return string.Empty;
					}
					return name;
				}
				else
				{
					string prefix = this.Prefix;
					string localName = this.LocalName;
					if (prefix == null || prefix.Length <= 0)
					{
						return localName;
					}
					if (localName != null && localName.Length > 0)
					{
						return this.doc.NameTable.Add(prefix + ":" + localName);
					}
					return prefix;
				}
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0004A3C8 File Offset: 0x000497C8
		private bool IsLocalNameEmpty(XmlNodeType nt)
		{
			switch (nt)
			{
			case XmlNodeType.None:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Comment:
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			case XmlNodeType.EndElement:
			case XmlNodeType.EndEntity:
				return true;
			case XmlNodeType.Element:
			case XmlNodeType.Attribute:
			case XmlNodeType.EntityReference:
			case XmlNodeType.Entity:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.DocumentType:
			case XmlNodeType.Notation:
			case XmlNodeType.XmlDeclaration:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0004A42C File Offset: 0x0004982C
		internal string Prefix
		{
			get
			{
				this.RealFoliate();
				if (this.node == null)
				{
					return string.Empty;
				}
				if (this.column == null)
				{
					return this.node.Prefix;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000650 RID: 1616 RVA: 0x0004A468 File Offset: 0x00049868
		internal string Value
		{
			get
			{
				this.RealFoliate();
				if (this.node == null)
				{
					return null;
				}
				if (this.column == null)
				{
					return this.node.Value;
				}
				if (this.column.ColumnMapping != MappingType.Attribute && !this.fOnValue)
				{
					return null;
				}
				DataRow row = this.Row;
				DataRowVersion version = (row.RowState == DataRowState.Detached) ? DataRowVersion.Proposed : DataRowVersion.Current;
				object value = row[this.column, version];
				if (!Convert.IsDBNull(value))
				{
					return this.column.ConvertObjectToXml(value);
				}
				return null;
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0004A4F4 File Offset: 0x000498F4
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this.node;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0004A510 File Offset: 0x00049910
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this.column;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0004A52C File Offset: 0x0004992C
		internal XmlNode GetNode()
		{
			return this.node;
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x0004A540 File Offset: 0x00049940
		internal bool IsEmptyElement
		{
			get
			{
				this.RealFoliate();
				return this.node != null && this.column == null && this.node.NodeType == XmlNodeType.Element && ((XmlElement)this.node).IsEmpty;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0004A584 File Offset: 0x00049984
		internal bool IsDefault
		{
			get
			{
				this.RealFoliate();
				return this.node != null && this.column == null && this.node.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.node).Specified;
			}
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0004A5CC File Offset: 0x000499CC
		void IXmlDataVirtualNode.OnFoliated(XmlNode foliatedNode)
		{
			if (this.node == foliatedNode)
			{
				if (this.column == null)
				{
					return;
				}
				this.bNeedFoliate = true;
			}
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0004A5F4 File Offset: 0x000499F4
		internal void RealFoliate()
		{
			if (!this.bNeedFoliate)
			{
				return;
			}
			XmlNode xmlNode;
			if (this.doc.IsTextOnly(this.column))
			{
				xmlNode = this.node.FirstChild;
			}
			else
			{
				if (this.column.ColumnMapping == MappingType.Attribute)
				{
					xmlNode = this.node.Attributes.GetNamedItem(this.column.EncodedColumnName, this.column.Namespace);
				}
				else
				{
					xmlNode = this.node.FirstChild;
					while (xmlNode != null && (!(xmlNode.LocalName == this.column.EncodedColumnName) || !(xmlNode.NamespaceURI == this.column.Namespace)))
					{
						xmlNode = xmlNode.NextSibling;
					}
				}
				if (xmlNode != null && this.fOnValue)
				{
					xmlNode = xmlNode.FirstChild;
				}
			}
			if (xmlNode == null)
			{
				throw new InvalidOperationException(Res.GetString("DataDom_Foliation"));
			}
			this.node = xmlNode;
			this.column = null;
			this.fOnValue = false;
			this.bNeedFoliate = false;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x0004A6F0 File Offset: 0x00049AF0
		internal string PublicId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Entity)
				{
					return ((XmlEntity)this.node).PublicId;
				}
				if (nodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this.node).PublicId;
				}
				if (nodeType != XmlNodeType.Notation)
				{
					return null;
				}
				return ((XmlNotation)this.node).PublicId;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0004A748 File Offset: 0x00049B48
		internal string SystemId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				if (nodeType == XmlNodeType.Entity)
				{
					return ((XmlEntity)this.node).SystemId;
				}
				if (nodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this.node).SystemId;
				}
				if (nodeType != XmlNodeType.Notation)
				{
					return null;
				}
				return ((XmlNotation)this.node).SystemId;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x0004A7A0 File Offset: 0x00049BA0
		internal string InternalSubset
		{
			get
			{
				if (this.NodeType == XmlNodeType.DocumentType)
				{
					return ((XmlDocumentType)this.node).InternalSubset;
				}
				return null;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0004A7CC File Offset: 0x00049BCC
		internal XmlDeclaration Declaration
		{
			get
			{
				XmlNode xmlNode = this.doc.SafeFirstChild(this.doc);
				if (xmlNode != null && xmlNode.NodeType == XmlNodeType.XmlDeclaration)
				{
					return (XmlDeclaration)xmlNode;
				}
				return null;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0004A800 File Offset: 0x00049C00
		internal string Encoding
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this.node).Encoding;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Encoding;
					}
				}
				return null;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0004A844 File Offset: 0x00049C44
		internal string Standalone
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this.node).Standalone;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Standalone;
					}
				}
				return null;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x0004A888 File Offset: 0x00049C88
		internal string Version
		{
			get
			{
				if (this.NodeType == XmlNodeType.XmlDeclaration)
				{
					return ((XmlDeclaration)this.node).Version;
				}
				if (this.NodeType == XmlNodeType.Document)
				{
					XmlDeclaration declaration = this.Declaration;
					if (declaration != null)
					{
						return declaration.Version;
					}
				}
				return null;
			}
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0004A8CC File Offset: 0x00049CCC
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this.column != null)
			{
				XmlBoundElement xmlBoundElement = this.node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				ElementState elementState = xmlBoundElement.ElementState;
				int num = (row.RowState == DataRowState.Detached) ? 1024 : 512;
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0004A914 File Offset: 0x00049D14
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._isInUse;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0004A928 File Offset: 0x00049D28
		internal void SetNoLongerUse()
		{
			this.node = null;
			this.column = null;
			this.fOnValue = false;
			this.bNeedFoliate = false;
			this._isInUse = false;
		}

		// Token: 0x0400026F RID: 623
		private XmlDataDocument doc;

		// Token: 0x04000270 RID: 624
		private XmlNode node;

		// Token: 0x04000271 RID: 625
		private DataColumn column;

		// Token: 0x04000272 RID: 626
		private bool fOnValue;

		// Token: 0x04000273 RID: 627
		private bool bNeedFoliate;

		// Token: 0x04000274 RID: 628
		private bool _isInUse;
	}
}

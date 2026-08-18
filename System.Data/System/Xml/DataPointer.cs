using System;
using System.Data;
using System.Diagnostics;

namespace System.Xml
{
	// Token: 0x02000385 RID: 901
	internal sealed class DataPointer : IXmlDataVirtualNode
	{
		// Token: 0x06002F9C RID: 12188 RVA: 0x002D4F88 File Offset: 0x002D4388
		internal DataPointer(XmlDataDocument doc, XmlNode node)
		{
			this.doc = doc;
			this.node = node;
			this.column = null;
			this.fOnValue = false;
			this.bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x002D4FC8 File Offset: 0x002D43C8
		internal DataPointer(DataPointer pointer)
		{
			this.doc = pointer.doc;
			this.node = pointer.node;
			this.column = pointer.column;
			this.fOnValue = pointer.fOnValue;
			this.bNeedFoliate = false;
			this._isInUse = true;
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x002D5028 File Offset: 0x002D4428
		internal void AddPointer()
		{
			this.doc.AddPointer(this);
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x002D5048 File Offset: 0x002D4448
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

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x002D5088 File Offset: 0x002D4488
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

		// Token: 0x06002FA1 RID: 12193 RVA: 0x002D50A8 File Offset: 0x002D44A8
		private static bool IsFoliated(XmlNode node)
		{
			return node == null || !(node is XmlBoundElement) || ((XmlBoundElement)node).IsFoliated;
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x002D50D8 File Offset: 0x002D44D8
		internal void MoveTo(DataPointer pointer)
		{
			this.doc = pointer.doc;
			this.node = pointer.node;
			this.column = pointer.column;
			this.fOnValue = pointer.fOnValue;
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x002D5118 File Offset: 0x002D4518
		private void MoveTo(XmlNode node)
		{
			this.node = node;
			this.column = null;
			this.fOnValue = false;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x002D5148 File Offset: 0x002D4548
		private void MoveTo(XmlNode node, DataColumn column, bool fOnValue)
		{
			this.node = node;
			this.column = column;
			this.fOnValue = fOnValue;
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x002D5178 File Offset: 0x002D4578
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

		// Token: 0x06002FA6 RID: 12198 RVA: 0x002D5218 File Offset: 0x002D4618
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

		// Token: 0x06002FA7 RID: 12199 RVA: 0x002D5248 File Offset: 0x002D4648
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

		// Token: 0x06002FA8 RID: 12200 RVA: 0x002D5278 File Offset: 0x002D4678
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

		// Token: 0x06002FA9 RID: 12201 RVA: 0x002D5318 File Offset: 0x002D4718
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

		// Token: 0x06002FAA RID: 12202 RVA: 0x002D53C8 File Offset: 0x002D47C8
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

		// Token: 0x06002FAB RID: 12203 RVA: 0x002D5458 File Offset: 0x002D4858
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

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x002D54E8 File Offset: 0x002D48E8
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

		// Token: 0x06002FAD RID: 12205 RVA: 0x002D5548 File Offset: 0x002D4948
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

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x002D55E8 File Offset: 0x002D49E8
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

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x002D5638 File Offset: 0x002D4A38
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

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x002D56B8 File Offset: 0x002D4AB8
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

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x002D5718 File Offset: 0x002D4B18
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

		// Token: 0x06002FB2 RID: 12210 RVA: 0x002D57B8 File Offset: 0x002D4BB8
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

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x002D5828 File Offset: 0x002D4C28
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

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x002D5868 File Offset: 0x002D4C68
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

		// Token: 0x06002FB5 RID: 12213 RVA: 0x002D58F8 File Offset: 0x002D4CF8
		bool IXmlDataVirtualNode.IsOnNode(XmlNode nodeToCheck)
		{
			this.RealFoliate();
			return nodeToCheck == this.node;
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x002D5918 File Offset: 0x002D4D18
		bool IXmlDataVirtualNode.IsOnColumn(DataColumn col)
		{
			this.RealFoliate();
			return col == this.column;
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x002D5938 File Offset: 0x002D4D38
		internal XmlNode GetNode()
		{
			return this.node;
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x002D5958 File Offset: 0x002D4D58
		internal bool IsEmptyElement
		{
			get
			{
				this.RealFoliate();
				return this.node != null && this.column == null && this.node.NodeType == XmlNodeType.Element && ((XmlElement)this.node).IsEmpty;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x002D59A8 File Offset: 0x002D4DA8
		internal bool IsDefault
		{
			get
			{
				this.RealFoliate();
				return this.node != null && this.column == null && this.node.NodeType == XmlNodeType.Attribute && !((XmlAttribute)this.node).Specified;
			}
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x002D59F8 File Offset: 0x002D4DF8
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

		// Token: 0x06002FBB RID: 12219 RVA: 0x002D5A28 File Offset: 0x002D4E28
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

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x002D5B28 File Offset: 0x002D4F28
		internal string PublicId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				XmlNodeType xmlNodeType = nodeType;
				if (xmlNodeType != XmlNodeType.Entity)
				{
					switch (xmlNodeType)
					{
					case XmlNodeType.DocumentType:
						return ((XmlDocumentType)this.node).PublicId;
					case XmlNodeType.Notation:
						return ((XmlNotation)this.node).PublicId;
					}
					return null;
				}
				return ((XmlEntity)this.node).PublicId;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x002D5B98 File Offset: 0x002D4F98
		internal string SystemId
		{
			get
			{
				XmlNodeType nodeType = this.NodeType;
				XmlNodeType xmlNodeType = nodeType;
				if (xmlNodeType != XmlNodeType.Entity)
				{
					switch (xmlNodeType)
					{
					case XmlNodeType.DocumentType:
						return ((XmlDocumentType)this.node).SystemId;
					case XmlNodeType.Notation:
						return ((XmlNotation)this.node).SystemId;
					}
					return null;
				}
				return ((XmlEntity)this.node).SystemId;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x002D5C08 File Offset: 0x002D5008
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

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x002D5C38 File Offset: 0x002D5038
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

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x002D5C78 File Offset: 0x002D5078
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

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x002D5CC8 File Offset: 0x002D50C8
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

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x002D5D18 File Offset: 0x002D5118
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

		// Token: 0x06002FC3 RID: 12227 RVA: 0x002D5D68 File Offset: 0x002D5168
		[Conditional("DEBUG")]
		private void AssertValid()
		{
			if (this.column != null)
			{
				XmlBoundElement xmlBoundElement = this.node as XmlBoundElement;
				DataRow row = xmlBoundElement.Row;
				ElementState elementState = xmlBoundElement.ElementState;
				DataRowState rowState = row.RowState;
			}
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x002D5DA8 File Offset: 0x002D51A8
		bool IXmlDataVirtualNode.IsInUse()
		{
			return this._isInUse;
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x002D5DC8 File Offset: 0x002D51C8
		internal void SetNoLongerUse()
		{
			this.node = null;
			this.column = null;
			this.fOnValue = false;
			this.bNeedFoliate = false;
			this._isInUse = false;
		}

		// Token: 0x04001D9C RID: 7580
		private XmlDataDocument doc;

		// Token: 0x04001D9D RID: 7581
		private XmlNode node;

		// Token: 0x04001D9E RID: 7582
		private DataColumn column;

		// Token: 0x04001D9F RID: 7583
		private bool fOnValue;

		// Token: 0x04001DA0 RID: 7584
		private bool bNeedFoliate;

		// Token: 0x04001DA1 RID: 7585
		private bool _isInUse;
	}
}

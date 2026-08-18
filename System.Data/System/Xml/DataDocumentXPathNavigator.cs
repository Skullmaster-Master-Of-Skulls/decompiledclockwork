using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000383 RID: 899
	internal sealed class DataDocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x06002F74 RID: 12148 RVA: 0x002D4968 File Offset: 0x002D3D68
		internal DataDocumentXPathNavigator(XmlDataDocument doc, XmlNode node)
		{
			this._curNode = new XPathNodePointer(this, doc, node);
			this._temp = new XPathNodePointer(this, doc, node);
			this._doc = doc;
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x002D49A8 File Offset: 0x002D3DA8
		private DataDocumentXPathNavigator(DataDocumentXPathNavigator other)
		{
			this._curNode = other._curNode.Clone(this);
			this._temp = other._temp.Clone(this);
			this._doc = other._doc;
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x002D49F8 File Offset: 0x002D3DF8
		public override XPathNavigator Clone()
		{
			return new DataDocumentXPathNavigator(this);
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x002D4A18 File Offset: 0x002D3E18
		internal XPathNodePointer CurNode
		{
			get
			{
				return this._curNode;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002F78 RID: 12152 RVA: 0x002D4A38 File Offset: 0x002D3E38
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x002D4A58 File Offset: 0x002D3E58
		public override XPathNodeType NodeType
		{
			get
			{
				return this._curNode.NodeType;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x002D4A78 File Offset: 0x002D3E78
		public override string LocalName
		{
			get
			{
				return this._curNode.LocalName;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x002D4A98 File Offset: 0x002D3E98
		public override string NamespaceURI
		{
			get
			{
				return this._curNode.NamespaceURI;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002F7C RID: 12156 RVA: 0x002D4AB8 File Offset: 0x002D3EB8
		public override string Name
		{
			get
			{
				return this._curNode.Name;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x002D4AD8 File Offset: 0x002D3ED8
		public override string Prefix
		{
			get
			{
				return this._curNode.Prefix;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x002D4AF8 File Offset: 0x002D3EF8
		public override string Value
		{
			get
			{
				XPathNodeType nodeType = this._curNode.NodeType;
				if (nodeType == XPathNodeType.Element || nodeType == XPathNodeType.Root)
				{
					return this._curNode.InnerText;
				}
				return this._curNode.Value;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x002D4B38 File Offset: 0x002D3F38
		public override string BaseURI
		{
			get
			{
				return this._curNode.BaseURI;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x002D4B58 File Offset: 0x002D3F58
		public override string XmlLang
		{
			get
			{
				return this._curNode.XmlLang;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x002D4B78 File Offset: 0x002D3F78
		public override bool IsEmptyElement
		{
			get
			{
				return this._curNode.IsEmptyElement;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x002D4B98 File Offset: 0x002D3F98
		public override XmlNameTable NameTable
		{
			get
			{
				return this._doc.NameTable;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06002F83 RID: 12163 RVA: 0x002D4BB8 File Offset: 0x002D3FB8
		public override bool HasAttributes
		{
			get
			{
				return this._curNode.AttributeCount > 0;
			}
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x002D4BD8 File Offset: 0x002D3FD8
		public override string GetAttribute(string localName, string namespaceURI)
		{
			if (this._curNode.NodeType != XPathNodeType.Element)
			{
				return string.Empty;
			}
			this._temp.MoveTo(this._curNode);
			if (this._temp.MoveToAttribute(localName, namespaceURI))
			{
				return this._temp.Value;
			}
			return string.Empty;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x002D4C38 File Offset: 0x002D4038
		public override string GetNamespace(string name)
		{
			return this._curNode.GetNamespace(name);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x002D4C58 File Offset: 0x002D4058
		public override bool MoveToNamespace(string name)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNamespace(name);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x002D4C88 File Offset: 0x002D4088
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToFirstNamespace(namespaceScope);
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x002D4CB8 File Offset: 0x002D40B8
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Namespace && this._curNode.MoveToNextNamespace(namespaceScope);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x002D4CE8 File Offset: 0x002D40E8
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x002D4D18 File Offset: 0x002D4118
		public override bool MoveToFirstAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNextAttribute(true);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x002D4D48 File Offset: 0x002D4148
		public override bool MoveToNextAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Attribute && this._curNode.MoveToNextAttribute(false);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x002D4D78 File Offset: 0x002D4178
		public override bool MoveToNext()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToNextSibling();
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x002D4DA8 File Offset: 0x002D41A8
		public override bool MoveToPrevious()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToPreviousSibling();
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x002D4DD8 File Offset: 0x002D41D8
		public override bool MoveToFirst()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToFirst();
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x002D4E08 File Offset: 0x002D4208
		public override bool HasChildren
		{
			get
			{
				return this._curNode.HasChildren;
			}
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x002D4E28 File Offset: 0x002D4228
		public override bool MoveToFirstChild()
		{
			return this._curNode.MoveToFirstChild();
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x002D4E48 File Offset: 0x002D4248
		public override bool MoveToParent()
		{
			return this._curNode.MoveToParent();
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x002D4E68 File Offset: 0x002D4268
		public override void MoveToRoot()
		{
			this._curNode.MoveToRoot();
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x002D4E88 File Offset: 0x002D4288
		public override bool MoveTo(XPathNavigator other)
		{
			if (other == null)
			{
				return false;
			}
			DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
			if (dataDocumentXPathNavigator == null)
			{
				return false;
			}
			if (this._curNode.MoveTo(dataDocumentXPathNavigator.CurNode))
			{
				this._doc = this._curNode.Document;
				return true;
			}
			return false;
		}

		// Token: 0x06002F94 RID: 12180 RVA: 0x002D4ED8 File Offset: 0x002D42D8
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x06002F95 RID: 12181 RVA: 0x002D4EE8 File Offset: 0x002D42E8
		public override bool IsSamePosition(XPathNavigator other)
		{
			if (other == null)
			{
				return false;
			}
			DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
			return dataDocumentXPathNavigator != null && this._doc == dataDocumentXPathNavigator.Document && this._curNode.IsSamePosition(dataDocumentXPathNavigator.CurNode);
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x002D4F28 File Offset: 0x002D4328
		XmlNode IHasXmlNode.GetNode()
		{
			return this._curNode.Node;
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x002D4F48 File Offset: 0x002D4348
		public override XmlNodeOrder ComparePosition(XPathNavigator other)
		{
			if (other == null)
			{
				return XmlNodeOrder.Unknown;
			}
			DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
			if (dataDocumentXPathNavigator == null || dataDocumentXPathNavigator.Document != this._doc)
			{
				return XmlNodeOrder.Unknown;
			}
			return this._curNode.ComparePosition(dataDocumentXPathNavigator.CurNode);
		}

		// Token: 0x04001D99 RID: 7577
		private XPathNodePointer _curNode;

		// Token: 0x04001D9A RID: 7578
		private XmlDataDocument _doc;

		// Token: 0x04001D9B RID: 7579
		private XPathNodePointer _temp;
	}
}

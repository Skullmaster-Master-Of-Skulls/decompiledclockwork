using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x02000082 RID: 130
	internal sealed class DataDocumentXPathNavigator : XPathNavigator, IHasXmlNode
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x000496E8 File Offset: 0x00048AE8
		internal DataDocumentXPathNavigator(XmlDataDocument doc, XmlNode node)
		{
			this._curNode = new XPathNodePointer(this, doc, node);
			this._temp = new XPathNodePointer(this, doc, node);
			this._doc = doc;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00049720 File Offset: 0x00048B20
		private DataDocumentXPathNavigator(DataDocumentXPathNavigator other)
		{
			this._curNode = other._curNode.Clone(this);
			this._temp = other._temp.Clone(this);
			this._doc = other._doc;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00049764 File Offset: 0x00048B64
		public override XPathNavigator Clone()
		{
			return new DataDocumentXPathNavigator(this);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00049778 File Offset: 0x00048B78
		internal XPathNodePointer CurNode
		{
			get
			{
				return this._curNode;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0004978C File Offset: 0x00048B8C
		internal XmlDataDocument Document
		{
			get
			{
				return this._doc;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x000497A0 File Offset: 0x00048BA0
		public override XPathNodeType NodeType
		{
			get
			{
				return this._curNode.NodeType;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x000497B8 File Offset: 0x00048BB8
		public override string LocalName
		{
			get
			{
				return this._curNode.LocalName;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x000497D0 File Offset: 0x00048BD0
		public override string NamespaceURI
		{
			get
			{
				return this._curNode.NamespaceURI;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x000497E8 File Offset: 0x00048BE8
		public override string Name
		{
			get
			{
				return this._curNode.Name;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00049800 File Offset: 0x00048C00
		public override string Prefix
		{
			get
			{
				return this._curNode.Prefix;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00049818 File Offset: 0x00048C18
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

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00049850 File Offset: 0x00048C50
		public override string BaseURI
		{
			get
			{
				return this._curNode.BaseURI;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00049868 File Offset: 0x00048C68
		public override string XmlLang
		{
			get
			{
				return this._curNode.XmlLang;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00049880 File Offset: 0x00048C80
		public override bool IsEmptyElement
		{
			get
			{
				return this._curNode.IsEmptyElement;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00049898 File Offset: 0x00048C98
		public override XmlNameTable NameTable
		{
			get
			{
				return this._doc.NameTable;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x000498B0 File Offset: 0x00048CB0
		public override bool HasAttributes
		{
			get
			{
				return this._curNode.AttributeCount > 0;
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000498CC File Offset: 0x00048CCC
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

		// Token: 0x06000625 RID: 1573 RVA: 0x00049920 File Offset: 0x00048D20
		public override string GetNamespace(string name)
		{
			return this._curNode.GetNamespace(name);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0004993C File Offset: 0x00048D3C
		public override bool MoveToNamespace(string name)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNamespace(name);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00049968 File Offset: 0x00048D68
		public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToFirstNamespace(namespaceScope);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00049994 File Offset: 0x00048D94
		public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
		{
			return this._curNode.NodeType == XPathNodeType.Namespace && this._curNode.MoveToNextNamespace(namespaceScope);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000499C0 File Offset: 0x00048DC0
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000499EC File Offset: 0x00048DEC
		public override bool MoveToFirstAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Element && this._curNode.MoveToNextAttribute(true);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00049A18 File Offset: 0x00048E18
		public override bool MoveToNextAttribute()
		{
			return this._curNode.NodeType == XPathNodeType.Attribute && this._curNode.MoveToNextAttribute(false);
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00049A44 File Offset: 0x00048E44
		public override bool MoveToNext()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToNextSibling();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00049A6C File Offset: 0x00048E6C
		public override bool MoveToPrevious()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToPreviousSibling();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00049A94 File Offset: 0x00048E94
		public override bool MoveToFirst()
		{
			return this._curNode.NodeType != XPathNodeType.Attribute && this._curNode.MoveToFirst();
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x00049ABC File Offset: 0x00048EBC
		public override bool HasChildren
		{
			get
			{
				return this._curNode.HasChildren;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00049AD4 File Offset: 0x00048ED4
		public override bool MoveToFirstChild()
		{
			return this._curNode.MoveToFirstChild();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00049AEC File Offset: 0x00048EEC
		public override bool MoveToParent()
		{
			return this._curNode.MoveToParent();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00049B04 File Offset: 0x00048F04
		public override void MoveToRoot()
		{
			this._curNode.MoveToRoot();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00049B1C File Offset: 0x00048F1C
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

		// Token: 0x06000634 RID: 1588 RVA: 0x00049B64 File Offset: 0x00048F64
		public override bool MoveToId(string id)
		{
			return false;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00049B74 File Offset: 0x00048F74
		public override bool IsSamePosition(XPathNavigator other)
		{
			if (other == null)
			{
				return false;
			}
			DataDocumentXPathNavigator dataDocumentXPathNavigator = other as DataDocumentXPathNavigator;
			return dataDocumentXPathNavigator != null && this._doc == dataDocumentXPathNavigator.Document && this._curNode.IsSamePosition(dataDocumentXPathNavigator.CurNode);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00049BB4 File Offset: 0x00048FB4
		XmlNode IHasXmlNode.GetNode()
		{
			return this._curNode.Node;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00049BCC File Offset: 0x00048FCC
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

		// Token: 0x0400026C RID: 620
		private XPathNodePointer _curNode;

		// Token: 0x0400026D RID: 621
		private XmlDataDocument _doc;

		// Token: 0x0400026E RID: 622
		private XPathNodePointer _temp;
	}
}

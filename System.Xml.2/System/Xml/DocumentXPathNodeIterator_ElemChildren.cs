using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F7 RID: 247
	internal class DocumentXPathNodeIterator_ElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06001121 RID: 4385 RVA: 0x000488B2 File Offset: 0x00046AB2
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNavigator nav, string localNameAtom, string nsAtom) : base(nav)
		{
			this.localNameAtom = localNameAtom;
			this.nsAtom = nsAtom;
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000488C9 File Offset: 0x00046AC9
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNodeIterator_ElemChildren other) : base(other)
		{
			this.localNameAtom = other.localNameAtom;
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000488EA File Offset: 0x00046AEA
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren(this);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000488F2 File Offset: 0x00046AF2
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.LocalName, this.localNameAtom) && Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x040004C7 RID: 1223
		protected string localNameAtom;

		// Token: 0x040004C8 RID: 1224
		protected string nsAtom;
	}
}

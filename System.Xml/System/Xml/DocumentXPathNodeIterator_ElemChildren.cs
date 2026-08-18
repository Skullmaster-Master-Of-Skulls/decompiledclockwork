using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C6 RID: 198
	internal class DocumentXPathNodeIterator_ElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x0003508E File Offset: 0x0003408E
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNavigator nav, string localNameAtom, string nsAtom) : base(nav)
		{
			this.localNameAtom = localNameAtom;
			this.nsAtom = nsAtom;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000350A5 File Offset: 0x000340A5
		internal DocumentXPathNodeIterator_ElemChildren(DocumentXPathNodeIterator_ElemChildren other) : base(other)
		{
			this.localNameAtom = other.localNameAtom;
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000350C6 File Offset: 0x000340C6
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren(this);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000350CE File Offset: 0x000340CE
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.LocalName, this.localNameAtom) && Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x040008E4 RID: 2276
		protected string localNameAtom;

		// Token: 0x040008E5 RID: 2277
		protected string nsAtom;
	}
}

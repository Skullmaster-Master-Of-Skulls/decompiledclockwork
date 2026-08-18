using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C4 RID: 196
	internal class DocumentXPathNodeIterator_ElemChildren_NoLocalName : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x00034FE2 File Offset: 0x00033FE2
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNavigator nav, string nsAtom) : base(nav)
		{
			this.nsAtom = nsAtom;
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x00034FF2 File Offset: 0x00033FF2
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNodeIterator_ElemChildren_NoLocalName other) : base(other)
		{
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00035007 File Offset: 0x00034007
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003500F File Offset: 0x0003400F
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x040008E3 RID: 2275
		private string nsAtom;
	}
}

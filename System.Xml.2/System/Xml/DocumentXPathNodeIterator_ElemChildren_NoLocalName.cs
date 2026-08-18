using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F5 RID: 245
	internal class DocumentXPathNodeIterator_ElemChildren_NoLocalName : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x00048806 File Offset: 0x00046A06
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNavigator nav, string nsAtom) : base(nav)
		{
			this.nsAtom = nsAtom;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00048816 File Offset: 0x00046A16
		internal DocumentXPathNodeIterator_ElemChildren_NoLocalName(DocumentXPathNodeIterator_ElemChildren_NoLocalName other) : base(other)
		{
			this.nsAtom = other.nsAtom;
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0004882B File Offset: 0x00046A2B
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_NoLocalName(this);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00048833 File Offset: 0x00046A33
		protected override bool Match(XmlNode node)
		{
			return Ref.Equal(node.NamespaceURI, this.nsAtom);
		}

		// Token: 0x040004C6 RID: 1222
		private string nsAtom;
	}
}

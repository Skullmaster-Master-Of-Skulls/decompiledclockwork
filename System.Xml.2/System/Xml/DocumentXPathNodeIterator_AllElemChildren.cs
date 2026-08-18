using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F3 RID: 243
	internal class DocumentXPathNodeIterator_AllElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06001111 RID: 4369 RVA: 0x00048776 File Offset: 0x00046976
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNavigator nav) : base(nav)
		{
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0004877F File Offset: 0x0004697F
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNodeIterator_AllElemChildren other) : base(other)
		{
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00048788 File Offset: 0x00046988
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00048790 File Offset: 0x00046990
		protected override bool Match(XmlNode node)
		{
			return node.NodeType == XmlNodeType.Element;
		}
	}
}

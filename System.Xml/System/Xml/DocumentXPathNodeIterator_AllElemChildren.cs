using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C2 RID: 194
	internal class DocumentXPathNodeIterator_AllElemChildren : DocumentXPathNodeIterator_ElemDescendants
	{
		// Token: 0x06000B73 RID: 2931 RVA: 0x00034F52 File Offset: 0x00033F52
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNavigator nav) : base(nav)
		{
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00034F5B File Offset: 0x00033F5B
		internal DocumentXPathNodeIterator_AllElemChildren(DocumentXPathNodeIterator_AllElemChildren other) : base(other)
		{
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00034F64 File Offset: 0x00033F64
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren(this);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00034F6C File Offset: 0x00033F6C
		protected override bool Match(XmlNode node)
		{
			return node.NodeType == XmlNodeType.Element;
		}
	}
}

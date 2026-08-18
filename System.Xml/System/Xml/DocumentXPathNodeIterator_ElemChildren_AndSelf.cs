using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C7 RID: 199
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf : DocumentXPathNodeIterator_ElemChildren
	{
		// Token: 0x06000B87 RID: 2951 RVA: 0x000350F6 File Offset: 0x000340F6
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNavigator nav, string localNameAtom, string nsAtom) : base(nav, localNameAtom, nsAtom)
		{
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00035101 File Offset: 0x00034101
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNodeIterator_ElemChildren_AndSelf other) : base(other)
		{
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003510A File Offset: 0x0003410A
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00035114 File Offset: 0x00034114
		public override bool MoveNext()
		{
			if (this.CurrentPosition == 0)
			{
				DocumentXPathNavigator documentXPathNavigator = (DocumentXPathNavigator)this.Current;
				XmlNode xmlNode = (XmlNode)documentXPathNavigator.UnderlyingObject;
				if (xmlNode.NodeType == XmlNodeType.Element && this.Match(xmlNode))
				{
					base.SetPosition(1);
					return true;
				}
			}
			return base.MoveNext();
		}
	}
}

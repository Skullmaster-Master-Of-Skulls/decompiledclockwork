using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C3 RID: 195
	internal sealed class DocumentXPathNodeIterator_AllElemChildren_AndSelf : DocumentXPathNodeIterator_AllElemChildren
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00034F77 File Offset: 0x00033F77
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNavigator nav) : base(nav)
		{
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00034F80 File Offset: 0x00033F80
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNodeIterator_AllElemChildren_AndSelf other) : base(other)
		{
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00034F89 File Offset: 0x00033F89
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00034F94 File Offset: 0x00033F94
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

using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F8 RID: 248
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf : DocumentXPathNodeIterator_ElemChildren
	{
		// Token: 0x06001125 RID: 4389 RVA: 0x0004891A File Offset: 0x00046B1A
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNavigator nav, string localNameAtom, string nsAtom) : base(nav, localNameAtom, nsAtom)
		{
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00048925 File Offset: 0x00046B25
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf(DocumentXPathNodeIterator_ElemChildren_AndSelf other) : base(other)
		{
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004892E File Offset: 0x00046B2E
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf(this);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00048938 File Offset: 0x00046B38
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

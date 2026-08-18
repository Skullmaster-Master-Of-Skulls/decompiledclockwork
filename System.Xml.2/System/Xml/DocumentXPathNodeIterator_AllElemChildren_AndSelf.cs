using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F4 RID: 244
	internal sealed class DocumentXPathNodeIterator_AllElemChildren_AndSelf : DocumentXPathNodeIterator_AllElemChildren
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x0004879B File Offset: 0x0004699B
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNavigator nav) : base(nav)
		{
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000487A4 File Offset: 0x000469A4
		internal DocumentXPathNodeIterator_AllElemChildren_AndSelf(DocumentXPathNodeIterator_AllElemChildren_AndSelf other) : base(other)
		{
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000487AD File Offset: 0x000469AD
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_AllElemChildren_AndSelf(this);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000487B8 File Offset: 0x000469B8
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

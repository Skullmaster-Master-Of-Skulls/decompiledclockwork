using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000F6 RID: 246
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName : DocumentXPathNodeIterator_ElemChildren_NoLocalName
	{
		// Token: 0x0600111D RID: 4381 RVA: 0x00048846 File Offset: 0x00046A46
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNavigator nav, string nsAtom) : base(nav, nsAtom)
		{
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00048850 File Offset: 0x00046A50
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName other) : base(other)
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00048859 File Offset: 0x00046A59
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00048864 File Offset: 0x00046A64
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

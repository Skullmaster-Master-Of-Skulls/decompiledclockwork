using System;
using System.Xml.XPath;

namespace System.Xml
{
	// Token: 0x020000C5 RID: 197
	internal sealed class DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName : DocumentXPathNodeIterator_ElemChildren_NoLocalName
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x00035022 File Offset: 0x00034022
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNavigator nav, string nsAtom) : base(nav, nsAtom)
		{
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0003502C File Offset: 0x0003402C
		internal DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName other) : base(other)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00035035 File Offset: 0x00034035
		public override XPathNodeIterator Clone()
		{
			return new DocumentXPathNodeIterator_ElemChildren_AndSelf_NoLocalName(this);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x00035040 File Offset: 0x00034040
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

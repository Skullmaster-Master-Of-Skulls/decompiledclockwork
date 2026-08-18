using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000040 RID: 64
	internal class XPathAncestorIterator : XPathAxisIterator
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00007C66 File Offset: 0x00005E66
		public XPathAncestorIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : base(nav, type, matchSelf)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00007C71 File Offset: 0x00005E71
		public XPathAncestorIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00007C7E File Offset: 0x00005E7E
		public XPathAncestorIterator(XPathAncestorIterator other) : base(other)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00007C88 File Offset: 0x00005E88
		public override bool MoveNext()
		{
			if (this.first)
			{
				this.first = false;
				if (this.matchSelf && this.Matches)
				{
					this.position = 1;
					return true;
				}
			}
			while (this.nav.MoveToParent())
			{
				if (this.Matches)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00007CE3 File Offset: 0x00005EE3
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorIterator(this);
		}
	}
}

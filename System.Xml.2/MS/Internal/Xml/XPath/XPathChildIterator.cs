using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000044 RID: 68
	internal class XPathChildIterator : XPathAxisIterator
	{
		// Token: 0x06000212 RID: 530 RVA: 0x00008131 File Offset: 0x00006331
		public XPathChildIterator(XPathNavigator nav, XPathNodeType type) : base(nav, type, false)
		{
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000813C File Offset: 0x0000633C
		public XPathChildIterator(XPathNavigator nav, string name, string namespaceURI) : base(nav, name, namespaceURI, false)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008148 File Offset: 0x00006348
		public XPathChildIterator(XPathChildIterator it) : base(it)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008151 File Offset: 0x00006351
		public override XPathNodeIterator Clone()
		{
			return new XPathChildIterator(this);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000815C File Offset: 0x0000635C
		public override bool MoveNext()
		{
			while (this.first ? this.nav.MoveToFirstChild() : this.nav.MoveToNext())
			{
				this.first = false;
				if (this.Matches)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}
	}
}

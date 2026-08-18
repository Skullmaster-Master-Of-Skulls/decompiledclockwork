using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000167 RID: 359
	internal class XPathChildIterator : XPathAxisIterator
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x0005369C File Offset: 0x0005269C
		public XPathChildIterator(XPathNavigator nav, XPathNodeType type) : base(nav, type, false)
		{
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x000536A7 File Offset: 0x000526A7
		public XPathChildIterator(XPathNavigator nav, string name, string namespaceURI) : base(nav, name, namespaceURI, false)
		{
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000536B3 File Offset: 0x000526B3
		public XPathChildIterator(XPathChildIterator it) : base(it)
		{
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000536BC File Offset: 0x000526BC
		public override XPathNodeIterator Clone()
		{
			return new XPathChildIterator(this);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000536C4 File Offset: 0x000526C4
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

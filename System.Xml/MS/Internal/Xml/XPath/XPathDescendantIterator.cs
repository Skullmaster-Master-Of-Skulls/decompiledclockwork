using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000168 RID: 360
	internal class XPathDescendantIterator : XPathAxisIterator
	{
		// Token: 0x06001350 RID: 4944 RVA: 0x00053715 File Offset: 0x00052715
		public XPathDescendantIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : base(nav, type, matchSelf)
		{
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x00053720 File Offset: 0x00052720
		public XPathDescendantIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0005372D File Offset: 0x0005272D
		public XPathDescendantIterator(XPathDescendantIterator it) : base(it)
		{
			this.level = it.level;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00053742 File Offset: 0x00052742
		public override XPathNodeIterator Clone()
		{
			return new XPathDescendantIterator(this);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0005374C File Offset: 0x0005274C
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
			for (;;)
			{
				if (!this.nav.MoveToFirstChild())
				{
					while (this.level != 0)
					{
						if (this.nav.MoveToNext())
						{
							goto IL_78;
						}
						this.nav.MoveToParent();
						this.level--;
					}
					break;
				}
				this.level++;
				IL_78:
				if (this.Matches)
				{
					goto Block_7;
				}
			}
			return false;
			Block_7:
			this.position++;
			return true;
		}

		// Token: 0x04000BEF RID: 3055
		private int level;
	}
}

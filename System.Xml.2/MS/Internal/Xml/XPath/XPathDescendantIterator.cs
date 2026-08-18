using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000045 RID: 69
	internal class XPathDescendantIterator : XPathAxisIterator
	{
		// Token: 0x06000217 RID: 535 RVA: 0x000081AD File Offset: 0x000063AD
		public XPathDescendantIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : base(nav, type, matchSelf)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000081B8 File Offset: 0x000063B8
		public XPathDescendantIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000081C5 File Offset: 0x000063C5
		public XPathDescendantIterator(XPathDescendantIterator it) : base(it)
		{
			this.level = it.level;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000081DA File Offset: 0x000063DA
		public override XPathNodeIterator Clone()
		{
			return new XPathDescendantIterator(this);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000081E4 File Offset: 0x000063E4
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

		// Token: 0x040000DE RID: 222
		private int level;
	}
}

using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000164 RID: 356
	internal class XPathAncestorIterator : XPathAxisIterator
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x00053365 File Offset: 0x00052365
		public XPathAncestorIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : base(nav, type, matchSelf)
		{
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00053370 File Offset: 0x00052370
		public XPathAncestorIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0005337D File Offset: 0x0005237D
		public XPathAncestorIterator(XPathAncestorIterator other) : base(other)
		{
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00053388 File Offset: 0x00052388
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

		// Token: 0x06001337 RID: 4919 RVA: 0x000533E3 File Offset: 0x000523E3
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorIterator(this);
		}
	}
}

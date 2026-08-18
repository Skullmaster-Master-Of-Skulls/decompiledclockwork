using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000108 RID: 264
	internal class XPathDocumentKindDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06001000 RID: 4096 RVA: 0x0004974D File Offset: 0x0004874D
		public XPathDocumentKindDescendantIterator(XPathDocumentNavigator root, XPathNodeType typ, bool matchSelf) : base(root)
		{
			this.typ = typ;
			this.matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this.end = new XPathDocumentNavigator(root);
				this.end.MoveToNonDescendant();
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00049784 File Offset: 0x00048784
		public XPathDocumentKindDescendantIterator(XPathDocumentKindDescendantIterator iter) : base(iter)
		{
			this.end = iter.end;
			this.typ = iter.typ;
			this.matchSelf = iter.matchSelf;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000497B1 File Offset: 0x000487B1
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindDescendantIterator(this);
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000497BC File Offset: 0x000487BC
		public override bool MoveNext()
		{
			if (this.matchSelf)
			{
				this.matchSelf = false;
				if (this.ctxt.IsKindMatch(this.typ))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this.typ, this.end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04000A95 RID: 2709
		private XPathDocumentNavigator end;

		// Token: 0x04000A96 RID: 2710
		private XPathNodeType typ;

		// Token: 0x04000A97 RID: 2711
		private bool matchSelf;
	}
}

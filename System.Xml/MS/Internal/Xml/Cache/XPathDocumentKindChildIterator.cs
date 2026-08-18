using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000106 RID: 262
	internal class XPathDocumentKindChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06000FF8 RID: 4088 RVA: 0x000495B6 File Offset: 0x000485B6
		public XPathDocumentKindChildIterator(XPathDocumentNavigator parent, XPathNodeType typ) : base(parent)
		{
			this.typ = typ;
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000495C6 File Offset: 0x000485C6
		public XPathDocumentKindChildIterator(XPathDocumentKindChildIterator iter) : base(iter)
		{
			this.typ = iter.typ;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000495DB File Offset: 0x000485DB
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindChildIterator(this);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x000495E4 File Offset: 0x000485E4
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this.typ))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this.typ))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x04000A90 RID: 2704
		private XPathNodeType typ;
	}
}

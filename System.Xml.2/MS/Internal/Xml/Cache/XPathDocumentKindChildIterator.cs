using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000051 RID: 81
	internal class XPathDocumentKindChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000ADEA File Offset: 0x00008FEA
		public XPathDocumentKindChildIterator(XPathDocumentNavigator parent, XPathNodeType typ) : base(parent)
		{
			this.typ = typ;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000ADFA File Offset: 0x00008FFA
		public XPathDocumentKindChildIterator(XPathDocumentKindChildIterator iter) : base(iter)
		{
			this.typ = iter.typ;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000AE0F File Offset: 0x0000900F
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentKindChildIterator(this);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000AE18 File Offset: 0x00009018
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

		// Token: 0x0400011E RID: 286
		private XPathNodeType typ;
	}
}

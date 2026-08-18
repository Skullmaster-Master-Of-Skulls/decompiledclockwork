using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x0200004F RID: 79
	internal abstract class XPathDocumentBaseIterator : XPathNodeIterator
	{
		// Token: 0x0600029F RID: 671 RVA: 0x0000ACEB File Offset: 0x00008EEB
		protected XPathDocumentBaseIterator(XPathDocumentNavigator ctxt)
		{
			this.ctxt = new XPathDocumentNavigator(ctxt);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000ACFF File Offset: 0x00008EFF
		protected XPathDocumentBaseIterator(XPathDocumentBaseIterator iter)
		{
			this.ctxt = new XPathDocumentNavigator(iter.ctxt);
			this.pos = iter.pos;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000AD24 File Offset: 0x00008F24
		public override XPathNavigator Current
		{
			get
			{
				return this.ctxt;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000AD2C File Offset: 0x00008F2C
		public override int CurrentPosition
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x0400011A RID: 282
		protected XPathDocumentNavigator ctxt;

		// Token: 0x0400011B RID: 283
		protected int pos;
	}
}

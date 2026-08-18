using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000104 RID: 260
	internal abstract class XPathDocumentBaseIterator : XPathNodeIterator
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x000494B9 File Offset: 0x000484B9
		protected XPathDocumentBaseIterator(XPathDocumentNavigator ctxt)
		{
			this.ctxt = new XPathDocumentNavigator(ctxt);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000494CD File Offset: 0x000484CD
		protected XPathDocumentBaseIterator(XPathDocumentBaseIterator iter)
		{
			this.ctxt = new XPathDocumentNavigator(iter.ctxt);
			this.pos = iter.pos;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x000494F2 File Offset: 0x000484F2
		public override XPathNavigator Current
		{
			get
			{
				return this.ctxt;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x000494FA File Offset: 0x000484FA
		public override int CurrentPosition
		{
			get
			{
				return this.pos;
			}
		}

		// Token: 0x04000A8C RID: 2700
		protected XPathDocumentNavigator ctxt;

		// Token: 0x04000A8D RID: 2701
		protected int pos;
	}
}

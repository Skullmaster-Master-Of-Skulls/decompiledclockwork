using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000050 RID: 80
	internal class XPathDocumentElementChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000AD34 File Offset: 0x00008F34
		public XPathDocumentElementChildIterator(XPathDocumentNavigator parent, string name, string namespaceURI) : base(parent)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.localName = parent.NameTable.Get(name);
			this.namespaceUri = namespaceURI;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000AD64 File Offset: 0x00008F64
		public XPathDocumentElementChildIterator(XPathDocumentElementChildIterator iter) : base(iter)
		{
			this.localName = iter.localName;
			this.namespaceUri = iter.namespaceUri;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000AD85 File Offset: 0x00008F85
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementChildIterator(this);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000AD90 File Offset: 0x00008F90
		public override bool MoveNext()
		{
			if (this.pos == 0)
			{
				if (!this.ctxt.MoveToChild(this.localName, this.namespaceUri))
				{
					return false;
				}
			}
			else if (!this.ctxt.MoveToNext(this.localName, this.namespaceUri))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x0400011C RID: 284
		private string localName;

		// Token: 0x0400011D RID: 285
		private string namespaceUri;
	}
}

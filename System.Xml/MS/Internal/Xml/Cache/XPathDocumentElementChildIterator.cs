using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000105 RID: 261
	internal class XPathDocumentElementChildIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x00049502 File Offset: 0x00048502
		public XPathDocumentElementChildIterator(XPathDocumentNavigator parent, string name, string namespaceURI) : base(parent)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.localName = parent.NameTable.Get(name);
			this.namespaceUri = namespaceURI;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00049532 File Offset: 0x00048532
		public XPathDocumentElementChildIterator(XPathDocumentElementChildIterator iter) : base(iter)
		{
			this.localName = iter.localName;
			this.namespaceUri = iter.namespaceUri;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00049553 File Offset: 0x00048553
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementChildIterator(this);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0004955C File Offset: 0x0004855C
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

		// Token: 0x04000A8E RID: 2702
		private string localName;

		// Token: 0x04000A8F RID: 2703
		private string namespaceUri;
	}
}

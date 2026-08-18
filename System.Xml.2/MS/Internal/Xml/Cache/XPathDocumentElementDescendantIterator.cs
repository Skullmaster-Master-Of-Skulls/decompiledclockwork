using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000052 RID: 82
	internal class XPathDocumentElementDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000AE68 File Offset: 0x00009068
		public XPathDocumentElementDescendantIterator(XPathDocumentNavigator root, string name, string namespaceURI, bool matchSelf) : base(root)
		{
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.localName = root.NameTable.Get(name);
			this.namespaceUri = namespaceURI;
			this.matchSelf = matchSelf;
			if (root.NodeType != XPathNodeType.Root)
			{
				this.end = new XPathDocumentNavigator(root);
				this.end.MoveToNonDescendant();
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000AECB File Offset: 0x000090CB
		public XPathDocumentElementDescendantIterator(XPathDocumentElementDescendantIterator iter) : base(iter)
		{
			this.end = iter.end;
			this.localName = iter.localName;
			this.namespaceUri = iter.namespaceUri;
			this.matchSelf = iter.matchSelf;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000AF04 File Offset: 0x00009104
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementDescendantIterator(this);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000AF0C File Offset: 0x0000910C
		public override bool MoveNext()
		{
			if (this.matchSelf)
			{
				this.matchSelf = false;
				if (this.ctxt.IsElementMatch(this.localName, this.namespaceUri))
				{
					this.pos++;
					return true;
				}
			}
			if (!this.ctxt.MoveToFollowing(this.localName, this.namespaceUri, this.end))
			{
				return false;
			}
			this.pos++;
			return true;
		}

		// Token: 0x0400011F RID: 287
		private XPathDocumentNavigator end;

		// Token: 0x04000120 RID: 288
		private string localName;

		// Token: 0x04000121 RID: 289
		private string namespaceUri;

		// Token: 0x04000122 RID: 290
		private bool matchSelf;
	}
}

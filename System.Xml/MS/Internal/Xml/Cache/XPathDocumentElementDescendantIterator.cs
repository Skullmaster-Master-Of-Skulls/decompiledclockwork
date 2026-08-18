using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000107 RID: 263
	internal class XPathDocumentElementDescendantIterator : XPathDocumentBaseIterator
	{
		// Token: 0x06000FFC RID: 4092 RVA: 0x00049634 File Offset: 0x00048634
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

		// Token: 0x06000FFD RID: 4093 RVA: 0x00049697 File Offset: 0x00048697
		public XPathDocumentElementDescendantIterator(XPathDocumentElementDescendantIterator iter) : base(iter)
		{
			this.end = iter.end;
			this.localName = iter.localName;
			this.namespaceUri = iter.namespaceUri;
			this.matchSelf = iter.matchSelf;
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x000496D0 File Offset: 0x000486D0
		public override XPathNodeIterator Clone()
		{
			return new XPathDocumentElementDescendantIterator(this);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x000496D8 File Offset: 0x000486D8
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

		// Token: 0x04000A91 RID: 2705
		private XPathDocumentNavigator end;

		// Token: 0x04000A92 RID: 2706
		private string localName;

		// Token: 0x04000A93 RID: 2707
		private string namespaceUri;

		// Token: 0x04000A94 RID: 2708
		private bool matchSelf;
	}
}

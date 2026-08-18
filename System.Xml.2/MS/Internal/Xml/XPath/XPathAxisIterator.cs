using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000043 RID: 67
	internal abstract class XPathAxisIterator : XPathNodeIterator
	{
		// Token: 0x0600020B RID: 523 RVA: 0x00007F9E File Offset: 0x0000619E
		public XPathAxisIterator(XPathNavigator nav, bool matchSelf)
		{
			this.nav = nav;
			this.matchSelf = matchSelf;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007FBB File Offset: 0x000061BB
		public XPathAxisIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : this(nav, matchSelf)
		{
			this.type = type;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007FCC File Offset: 0x000061CC
		public XPathAxisIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf) : this(nav, matchSelf)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.name = name;
			this.uri = namespaceURI;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008004 File Offset: 0x00006204
		public XPathAxisIterator(XPathAxisIterator it)
		{
			this.nav = it.nav.Clone();
			this.type = it.type;
			this.name = it.name;
			this.uri = it.uri;
			this.position = it.position;
			this.matchSelf = it.matchSelf;
			this.first = it.first;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00008077 File Offset: 0x00006277
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000807F File Offset: 0x0000627F
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00008088 File Offset: 0x00006288
		protected virtual bool Matches
		{
			get
			{
				if (this.name == null)
				{
					return this.type == this.nav.NodeType || this.type == XPathNodeType.All || (this.type == XPathNodeType.Text && (this.nav.NodeType == XPathNodeType.Whitespace || this.nav.NodeType == XPathNodeType.SignificantWhitespace));
				}
				return this.nav.NodeType == XPathNodeType.Element && (this.name.Length == 0 || this.name == this.nav.LocalName) && this.uri == this.nav.NamespaceURI;
			}
		}

		// Token: 0x040000D7 RID: 215
		internal XPathNavigator nav;

		// Token: 0x040000D8 RID: 216
		internal XPathNodeType type;

		// Token: 0x040000D9 RID: 217
		internal string name;

		// Token: 0x040000DA RID: 218
		internal string uri;

		// Token: 0x040000DB RID: 219
		internal int position;

		// Token: 0x040000DC RID: 220
		internal bool matchSelf;

		// Token: 0x040000DD RID: 221
		internal bool first = true;
	}
}

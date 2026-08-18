using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000163 RID: 355
	internal abstract class XPathAxisIterator : XPathNodeIterator
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x000531D4 File Offset: 0x000521D4
		public XPathAxisIterator(XPathNavigator nav, bool matchSelf)
		{
			this.nav = nav;
			this.matchSelf = matchSelf;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000531F1 File Offset: 0x000521F1
		public XPathAxisIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf) : this(nav, matchSelf)
		{
			this.type = type;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00053202 File Offset: 0x00052202
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

		// Token: 0x0600132F RID: 4911 RVA: 0x00053238 File Offset: 0x00052238
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

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x000532AB File Offset: 0x000522AB
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x000532B3 File Offset: 0x000522B3
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x000532BC File Offset: 0x000522BC
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

		// Token: 0x04000BE5 RID: 3045
		internal XPathNavigator nav;

		// Token: 0x04000BE6 RID: 3046
		internal XPathNodeType type;

		// Token: 0x04000BE7 RID: 3047
		internal string name;

		// Token: 0x04000BE8 RID: 3048
		internal string uri;

		// Token: 0x04000BE9 RID: 3049
		internal int position;

		// Token: 0x04000BEA RID: 3050
		internal bool matchSelf;

		// Token: 0x04000BEB RID: 3051
		internal bool first = true;
	}
}

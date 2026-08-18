using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x020002DB RID: 731
	public abstract class XsltContext : XmlNamespaceManager
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x000E8070 File Offset: 0x000E6270
		protected XsltContext(NameTable table) : base(table)
		{
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000E8079 File Offset: 0x000E6279
		protected XsltContext() : base(new NameTable())
		{
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000E8086 File Offset: 0x000E6286
		internal XsltContext(bool dummy)
		{
		}

		// Token: 0x06002BCC RID: 11212
		public abstract IXsltContextVariable ResolveVariable(string prefix, string name);

		// Token: 0x06002BCD RID: 11213
		public abstract IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes);

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06002BCE RID: 11214
		public abstract bool Whitespace { get; }

		// Token: 0x06002BCF RID: 11215
		public abstract bool PreserveWhitespace(XPathNavigator node);

		// Token: 0x06002BD0 RID: 11216
		public abstract int CompareDocument(string baseUri, string nextbaseUri);
	}
}

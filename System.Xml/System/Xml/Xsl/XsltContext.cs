using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x02000132 RID: 306
	public abstract class XsltContext : XmlNamespaceManager
	{
		// Token: 0x060011CA RID: 4554 RVA: 0x0004E861 File Offset: 0x0004D861
		protected XsltContext(NameTable table) : base(table)
		{
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0004E86A File Offset: 0x0004D86A
		protected XsltContext() : base(new NameTable())
		{
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0004E877 File Offset: 0x0004D877
		internal XsltContext(bool dummy)
		{
		}

		// Token: 0x060011CD RID: 4557
		public abstract IXsltContextVariable ResolveVariable(string prefix, string name);

		// Token: 0x060011CE RID: 4558
		public abstract IXsltContextFunction ResolveFunction(string prefix, string name, XPathResultType[] ArgTypes);

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060011CF RID: 4559
		public abstract bool Whitespace { get; }

		// Token: 0x060011D0 RID: 4560
		public abstract bool PreserveWhitespace(XPathNavigator node);

		// Token: 0x060011D1 RID: 4561
		public abstract int CompareDocument(string baseUri, string nextbaseUri);
	}
}

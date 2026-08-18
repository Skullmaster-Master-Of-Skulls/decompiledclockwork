using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x02000177 RID: 375
	public interface IXsltContextVariable
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060013F7 RID: 5111
		bool IsLocal { get; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060013F8 RID: 5112
		bool IsParam { get; }

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x060013F9 RID: 5113
		XPathResultType VariableType { get; }

		// Token: 0x060013FA RID: 5114
		object Evaluate(XsltContext xsltContext);
	}
}

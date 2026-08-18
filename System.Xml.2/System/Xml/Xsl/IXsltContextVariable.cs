using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x020002DA RID: 730
	public interface IXsltContextVariable
	{
		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06002BC5 RID: 11205
		bool IsLocal { get; }

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06002BC6 RID: 11206
		bool IsParam { get; }

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06002BC7 RID: 11207
		XPathResultType VariableType { get; }

		// Token: 0x06002BC8 RID: 11208
		object Evaluate(XsltContext xsltContext);
	}
}

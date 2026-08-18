using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x02000176 RID: 374
	public interface IXsltContextFunction
	{
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060013F2 RID: 5106
		int Minargs { get; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060013F3 RID: 5107
		int Maxargs { get; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060013F4 RID: 5108
		XPathResultType ReturnType { get; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060013F5 RID: 5109
		XPathResultType[] ArgTypes { get; }

		// Token: 0x060013F6 RID: 5110
		object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext);
	}
}

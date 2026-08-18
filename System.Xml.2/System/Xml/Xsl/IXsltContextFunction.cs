using System;
using System.Xml.XPath;

namespace System.Xml.Xsl
{
	// Token: 0x020002D9 RID: 729
	public interface IXsltContextFunction
	{
		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06002BC0 RID: 11200
		int Minargs { get; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06002BC1 RID: 11201
		int Maxargs { get; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06002BC2 RID: 11202
		XPathResultType ReturnType { get; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06002BC3 RID: 11203
		XPathResultType[] ArgTypes { get; }

		// Token: 0x06002BC4 RID: 11204
		object Invoke(XsltContext xsltContext, object[] args, XPathNavigator docContext);
	}
}

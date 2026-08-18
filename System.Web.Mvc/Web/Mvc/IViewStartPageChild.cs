using System;

namespace System.Web.Mvc
{
	// Token: 0x020000BB RID: 187
	internal interface IViewStartPageChild
	{
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004F9 RID: 1273
		HtmlHelper<object> Html { get; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004FA RID: 1274
		UrlHelper Url { get; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004FB RID: 1275
		ViewContext ViewContext { get; }
	}
}

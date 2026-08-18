using System;

namespace System.Web.WebPages
{
	// Token: 0x02000050 RID: 80
	internal sealed class RequestBrowserOverrideStore : BrowserOverrideStore
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000803C File Offset: 0x0000623C
		public override string GetOverriddenUserAgent(HttpContextBase httpContext)
		{
			return httpContext.Request.UserAgent;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008049 File Offset: 0x00006249
		public override void SetOverriddenUserAgent(HttpContextBase httpContext, string userAgent)
		{
		}
	}
}

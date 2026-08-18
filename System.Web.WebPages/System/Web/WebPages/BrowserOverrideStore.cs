using System;

namespace System.Web.WebPages
{
	// Token: 0x02000014 RID: 20
	public abstract class BrowserOverrideStore
	{
		// Token: 0x060000B1 RID: 177
		public abstract string GetOverriddenUserAgent(HttpContextBase httpContext);

		// Token: 0x060000B2 RID: 178
		public abstract void SetOverriddenUserAgent(HttpContextBase httpContext, string userAgent);
	}
}

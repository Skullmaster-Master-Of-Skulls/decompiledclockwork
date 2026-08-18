using System;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000020 RID: 32
	internal interface ITokenStore
	{
		// Token: 0x060000FA RID: 250
		AntiForgeryToken GetCookieToken(HttpContextBase httpContext);

		// Token: 0x060000FB RID: 251
		AntiForgeryToken GetFormToken(HttpContextBase httpContext);

		// Token: 0x060000FC RID: 252
		void SaveCookieToken(HttpContextBase httpContext, AntiForgeryToken token);
	}
}

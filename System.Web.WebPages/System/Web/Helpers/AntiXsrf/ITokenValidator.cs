using System;
using System.Security.Principal;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000024 RID: 36
	internal interface ITokenValidator
	{
		// Token: 0x06000109 RID: 265
		AntiForgeryToken GenerateCookieToken();

		// Token: 0x0600010A RID: 266
		AntiForgeryToken GenerateFormToken(HttpContextBase httpContext, IIdentity identity, AntiForgeryToken cookieToken);

		// Token: 0x0600010B RID: 267
		bool IsCookieTokenValid(AntiForgeryToken cookieToken);

		// Token: 0x0600010C RID: 268
		void ValidateTokens(HttpContextBase httpContext, IIdentity identity, AntiForgeryToken cookieToken, AntiForgeryToken formToken);
	}
}

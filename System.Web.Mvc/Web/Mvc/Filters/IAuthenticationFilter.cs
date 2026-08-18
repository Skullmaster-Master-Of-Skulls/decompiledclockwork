using System;

namespace System.Web.Mvc.Filters
{
	// Token: 0x0200007B RID: 123
	public interface IAuthenticationFilter
	{
		// Token: 0x060003CA RID: 970
		void OnAuthentication(AuthenticationContext filterContext);

		// Token: 0x060003CB RID: 971
		void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext);
	}
}

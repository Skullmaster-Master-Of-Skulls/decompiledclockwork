using System;

namespace System.Web.Mvc
{
	// Token: 0x020000FF RID: 255
	public interface IAuthorizationFilter
	{
		// Token: 0x0600068C RID: 1676
		void OnAuthorization(AuthorizationContext filterContext);
	}
}

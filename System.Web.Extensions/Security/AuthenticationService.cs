using System;
using System.Web.ApplicationServices;
using System.Web.Script.Services;
using System.Web.Services;

namespace System.Web.Security
{
	// Token: 0x020000DC RID: 220
	[ScriptService]
	internal sealed class AuthenticationService
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x00029B89 File Offset: 0x00027D89
		[WebMethod]
		public bool Login(string userName, string password, bool createPersistentCookie)
		{
			ApplicationServiceHelper.EnsureAuthenticationServiceEnabled(HttpContext.Current, true);
			if (Membership.ValidateUser(userName, password))
			{
				FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
				return true;
			}
			return false;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00029BA9 File Offset: 0x00027DA9
		[WebMethod]
		public void Logout()
		{
			ApplicationServiceHelper.EnsureAuthenticationServiceEnabled(HttpContext.Current, false);
			FormsAuthentication.SignOut();
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x00029BBB File Offset: 0x00027DBB
		[WebMethod]
		public bool IsLoggedIn()
		{
			ApplicationServiceHelper.EnsureAuthenticationServiceEnabled(HttpContext.Current, false);
			return HttpContext.Current.Request.IsAuthenticated;
		}
	}
}

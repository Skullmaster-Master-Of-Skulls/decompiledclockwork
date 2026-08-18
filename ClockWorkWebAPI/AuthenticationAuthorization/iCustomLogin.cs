using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;

namespace ClockWorkWebAPI.AuthenticationAuthorization
{
	// Token: 0x02000079 RID: 121
	public interface iCustomLogin
	{
		// Token: 0x06000617 RID: 1559
		void CustomLogin(HttpSessionState Session, Cache Cache, HttpRequest Request, HttpResponse Response, ref UserInfo userInfo, string userName, string password, StringDictionary args);
	}
}

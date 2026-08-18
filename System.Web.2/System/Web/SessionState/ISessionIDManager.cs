using System;

namespace System.Web.SessionState
{
	// Token: 0x02000127 RID: 295
	public interface ISessionIDManager
	{
		// Token: 0x060011B0 RID: 4528
		bool InitializeRequest(HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue);

		// Token: 0x060011B1 RID: 4529
		string GetSessionID(HttpContext context);

		// Token: 0x060011B2 RID: 4530
		string CreateSessionID(HttpContext context);

		// Token: 0x060011B3 RID: 4531
		void SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded);

		// Token: 0x060011B4 RID: 4532
		void RemoveSessionID(HttpContext context);

		// Token: 0x060011B5 RID: 4533
		bool Validate(string id);

		// Token: 0x060011B6 RID: 4534
		void Initialize();
	}
}

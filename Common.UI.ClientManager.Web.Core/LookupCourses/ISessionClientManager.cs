using System;
using System.Collections.Generic;
using TechnoPro.Common.UI.Web.Entity.LookupCourses;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.LookupCourses
{
	// Token: 0x02000012 RID: 18
	public interface ISessionClientManager
	{
		// Token: 0x06000037 RID: 55
		SessionView AddSession(int addAmount, SessionView session);

		// Token: 0x06000038 RID: 56
		SessionView SubtractSession(int count, SessionView Session);

		// Token: 0x06000039 RID: 57
		SessionView GetCurrentSession();

		// Token: 0x0600003A RID: 58
		SessionView GetSession(DateTime date);

		// Token: 0x0600003B RID: 59
		SessionView GetSession(string sessionId);

		// Token: 0x0600003C RID: 60
		List<SessionView> GetSessions();

		// Token: 0x0600003D RID: 61
		List<SessionView> GetSessions(TermChooserAvailableSessionMode sessionMode);

		// Token: 0x0600003E RID: 62
		List<SessionView> GetSessions(TermChooserAvailableSessionMode sessionMode, UserInfoForCourses userInfo);

		// Token: 0x0600003F RID: 63
		List<SessionView> GetSessions(int? maxSessionsInThePast, TermChooserAvailableSessionMode sessionMode = TermChooserAvailableSessionMode.TermsWithLoggedInStudentsRegisteredCourses, UserInfoForCourses userInfo = null);
	}
}

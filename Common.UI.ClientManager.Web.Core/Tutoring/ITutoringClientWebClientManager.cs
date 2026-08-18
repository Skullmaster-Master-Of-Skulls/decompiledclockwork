using System;
using TechnoPro.Common.UI.Web.Entity;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Tutoring
{
	// Token: 0x0200000A RID: 10
	public interface ITutoringClientWebClientManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000022 RID: 34
		int TutorAvailabilityScheduleGroupId { get; }

		// Token: 0x06000023 RID: 35
		eClockWorkWebPage EnforceTutoringRedirects(int TutorPersonId, object currentPageObj, eClockWorkWebPage currentPage);

		// Token: 0x06000024 RID: 36
		eClockWorkWebPage EnforceStudentTuteeRedirects(int StudentPersonId, object currentPageObj, eClockWorkWebPage currentPage);
	}
}

using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x0200003F RID: 63
	public interface ISessionClientManager : IWebService
	{
		// Token: 0x060001CC RID: 460
		[Obsolete("Use AcademicTerm client manager instead")]
		AcademicTermDTO GetCurrentAcademicTerm();

		// Token: 0x060001CD RID: 461
		SessionDTO GetCurrentSession();

		// Token: 0x060001CE RID: 462
		SessionDTO AddSession(SessionDTO session, int count);

		// Token: 0x060001CF RID: 463
		SessionDTO SubtractSession(SessionDTO session, int count);

		// Token: 0x060001D0 RID: 464
		SessionDTO GetSessionByDate(DateTime Date);

		// Token: 0x060001D1 RID: 465
		SessionDTO GoToTodaysSession(SessionDTO session);

		// Token: 0x060001D2 RID: 466
		void SetSessionChooserDefaultValue(DateTime DtpNow);

		// Token: 0x060001D3 RID: 467
		DateTime? GetSessionChooserDefaultValue();
	}
}

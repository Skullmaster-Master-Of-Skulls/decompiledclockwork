using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.People
{
	// Token: 0x0200002E RID: 46
	public interface IStudentCommonInfoClientManager : IWebService
	{
		// Token: 0x0600013B RID: 315
		StudentCommonInfoDTO LoadStudentCommonInfo(int PersonId);

		// Token: 0x0600013C RID: 316
		PersonBaseDTO LoadStudentByEmailAddress(string EmailAddress);

		// Token: 0x0600013D RID: 317
		IList<StudentWithCommonInfoDTO> LoadMyStudents(int CounsellorPersonId, DateTime StartDate, DateTime EndDate, bool ShowStudentsIHaveAppsWith, bool ShowStudentsIAmAdvisorFor, bool IncludeCancelledAppointments = false, bool IncludeNoShowAppointments = true, int OverrideAssignedAdvisorControlId = 0);

		// Token: 0x0600013E RID: 318
		IList<StudentWithCommonInfoDTO> LoadStudentsWithCommonInfo(IList<int> PersonIds);
	}
}

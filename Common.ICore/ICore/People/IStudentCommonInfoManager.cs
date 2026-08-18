using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.People
{
	// Token: 0x02000055 RID: 85
	public interface IStudentCommonInfoManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000218 RID: 536
		StudentCommonInfo LoadStudentCommonInfo(int PersonId);

		// Token: 0x06000219 RID: 537
		PersonBase LoadStudentByEmailAddress(string EmailAddress);

		// Token: 0x0600021A RID: 538
		IList<StudentWithCommonInfo> LoadMyStudents(int CounsellorPersonId, DateTime StartDate, DateTime EndDate, bool ShowStudentsIHaveAppsWith, bool ShowStudentsIAmAdvisorFor, bool IncludeCancelledAppointments = false, bool IncludeNoShowAppointments = true, int OverrideAssignedAdvisorControlId = 0);

		// Token: 0x0600021B RID: 539
		StudentWithCommonInfo LoadStudentWithCommonInfo(int PersonId);

		// Token: 0x0600021C RID: 540
		IList<StudentWithCommonInfo> LoadStudentsWithCommonInfo(IList<int> PersonIds);

		// Token: 0x0600021D RID: 541
		Task<IList<StudentWithCommonInfo>> LoadStudentsWithCommonInfoAsync(IList<int> PersonIds);
	}
}

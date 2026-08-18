using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.People
{
	// Token: 0x02000042 RID: 66
	public interface IStudentCommonInfoDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000134 RID: 308
		StudentCommonInfo LoadStudentCommonInfo(int PersonId);

		// Token: 0x06000135 RID: 309
		IList<StudentWithCommonInfo> LoadMyStudents(int CounsellorPersonId, DateTime StartDate, DateTime EndDate, bool ShowStudentsIHaveAppsWith, bool ShowStudentsIAmAdvisorFor, bool IncludeCancelledAppointments = false, bool IncludeNoShowAppointments = true, int OverrideAssignedAdvisorControlId = 0);

		// Token: 0x06000136 RID: 310
		StudentWithCommonInfo LoadStudentWithCommonInfo(int PersonId);

		// Token: 0x06000137 RID: 311
		IList<StudentWithCommonInfo> LoadStudentsWithCommonInfo(IList<int> PersonIds);

		// Token: 0x06000138 RID: 312
		Task<IList<StudentWithCommonInfo>> LoadStudentsWithCommonInfoAsync(IList<int> PersonIds);
	}
}

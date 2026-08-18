using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000C7 RID: 199
	public interface IExamRequestManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000609 RID: 1545
		IList<ExamRequest> LoadRequestsByDateRange(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600060A RID: 1546
		int CreateExamRequest(int PersonId, int LuCourseId);

		// Token: 0x0600060B RID: 1547
		void DeleteExamRequest(int ExamRequestId);

		// Token: 0x0600060C RID: 1548
		IList<ExamRequest> LoadRequestsByCourse(int LuCourseId);

		// Token: 0x0600060D RID: 1549
		IList<PersonBase> LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(int LuCourseId, out IList<int> PersonIdsWhoSubmittedExamRequest);
	}
}

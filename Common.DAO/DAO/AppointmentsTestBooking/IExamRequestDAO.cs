using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000BA RID: 186
	public interface IExamRequestDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004FD RID: 1277
		IList<ExamRequest> LoadRequestsByDateRange(DateTime StartDate, DateTime EndDate);

		// Token: 0x060004FE RID: 1278
		int CreateExamRequest(int PersonId, int LuCourseId);

		// Token: 0x060004FF RID: 1279
		void DeleteExamRequest(int ExamRequestId);

		// Token: 0x06000500 RID: 1280
		IList<ExamRequest> LoadRequestsByCourse(int LuCourseId);
	}
}

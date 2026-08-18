using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x02000087 RID: 135
	public interface IExamRequestClientManager : IWebService
	{
		// Token: 0x060003FF RID: 1023
		IList<ExamRequestDTO> LoadRequestsByDateRange(DateTime StartDate, DateTime EndDate);

		// Token: 0x06000400 RID: 1024
		int CreateExamRequest(int PersonId, int LuCourseId);

		// Token: 0x06000401 RID: 1025
		void DeleteExamRequest(int ExamRequestId);

		// Token: 0x06000402 RID: 1026
		IList<ExamRequestDTO> LoadRequestsByCourse(int LuCourseId);

		// Token: 0x06000403 RID: 1027
		IList<PersonBaseDTO> LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(int LuCourseId, out IList<int> PersonIdsWhoSubmittedExamRequest);
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.StudentAccommodationRequests
{
	// Token: 0x02000015 RID: 21
	public interface IStudentAccommodationReqClientManager : IWebService
	{
		// Token: 0x06000081 RID: 129
		IList<CourseRegistrationWithAccommodationRequestDTO> LoadCourseRegistrationsWithRequestByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations);

		// Token: 0x06000082 RID: 130
		int AddRequest(int StudentPersonId, StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest);

		// Token: 0x06000083 RID: 131
		IList<StudentCourseAccommodationRequestDTO> LoadRequestsByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000084 RID: 132
		StudentCourseAccommodationRequestDTO LoadRequestById(int StudentCourseAccommodationRequestId);

		// Token: 0x06000085 RID: 133
		void DeleteRequest(int StudentCourseAccommodationRequestId);

		// Token: 0x06000086 RID: 134
		void UpdateRequest(StudentCourseAccommodationRequestDTO StudentCourseAccommodationRequest);

		// Token: 0x06000087 RID: 135
		IList<StudentCourseAccommodationRequestDTO> LoadCourseRegistrationsWithRequestByStatus(eStudentCourseAccommodationRequestStatusDTO Statuses, Range<DateTime> restrictCourseDates);

		// Token: 0x06000088 RID: 136
		void UpdateRequestStatus(eStudentCourseAccommodationRequestStatusDTO Statuses, int StudentAccommodationRequestId);

		// Token: 0x06000089 RID: 137
		StudentCourseAccommodationRequestHistoryDTO LoadStudentCourseAccommodationRequestHistory(int PersonId, int LuCourseId);
	}
}

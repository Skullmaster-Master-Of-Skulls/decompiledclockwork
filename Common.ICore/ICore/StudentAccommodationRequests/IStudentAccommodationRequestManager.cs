using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.ICore.StudentAccommodationRequests
{
	// Token: 0x02000032 RID: 50
	public interface IStudentAccommodationRequestManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000154 RID: 340
		int AddRequest(int StudentPersonId, StudentCourseAccommodationRequest CourseAccommodationRequest);

		// Token: 0x06000155 RID: 341
		void UpdateRequest(StudentCourseAccommodationRequest CourseAccommodationRequest);

		// Token: 0x06000156 RID: 342
		void DeleteRequest(int StudentCourseAccommodationRequestId);

		// Token: 0x06000157 RID: 343
		StudentCourseAccommodationRequest LoadRequestById(int StudentCourseAccommodationRequestId);

		// Token: 0x06000158 RID: 344
		IList<StudentCourseAccommodationRequest> LoadRequestsByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000159 RID: 345
		IList<CourseRegistrationWithAccommodationRequest> LoadCourseRegistrationsWithRequestByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate, bool LoadAccommodations);

		// Token: 0x0600015A RID: 346
		IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatus(eStudentCourseAccommodationRequestStatus statuses, Range<DateTime> RestrictToCourseDates = null);

		// Token: 0x0600015B RID: 347
		void UpdateRequestStatus(int StudentAccommodationRequestId, eStudentCourseAccommodationRequestStatus NewStatus);

		// Token: 0x0600015C RID: 348
		StudentCourseAccommodationRequestHistory LoadStudentCourseAccommodationRequestHistory(int PersonId, int LuCourseId);

		// Token: 0x0600015D RID: 349
		StudentCourseAccommodationRequest LoadRequestByStudentAndCourse(int StudentPersonId, int LuCourseId);

		// Token: 0x0600015E RID: 350
		IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatusWithCourseDatesInFuture(eStudentCourseAccommodationRequestStatus statuses);
	}
}

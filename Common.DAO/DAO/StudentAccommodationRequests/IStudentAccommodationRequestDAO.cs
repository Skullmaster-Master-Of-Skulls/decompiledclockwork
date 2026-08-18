using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.StudentAccommodationRequests
{
	// Token: 0x0200002B RID: 43
	public interface IStudentAccommodationRequestDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000B1 RID: 177
		int AddRequest(int StudentPersonId, StudentCourseAccommodationRequest CourseAccommodationRequest, out bool wasInserted);

		// Token: 0x060000B2 RID: 178
		void UpdateRequest(StudentCourseAccommodationRequest CourseAccommodationRequest);

		// Token: 0x060000B3 RID: 179
		void DeleteRequest(int StudentCourseAccommodationRequestId);

		// Token: 0x060000B4 RID: 180
		StudentCourseAccommodationRequest LoadRequestById(int StudentCourseAccommodationRequestId);

		// Token: 0x060000B5 RID: 181
		IList<StudentCourseAccommodationRequest> LoadRequestsByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060000B6 RID: 182
		IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatus(Range<DateTime> RestrictToCourseDates, eStudentCourseAccommodationRequestStatus statuses);

		// Token: 0x060000B7 RID: 183
		void UpdateRequestStatus(int StudentAccommodationRequestId, eStudentCourseAccommodationRequestStatus NewStatus);

		// Token: 0x060000B8 RID: 184
		Task AddArchiveEntryForUpdateAsync(StudentCourseAccommodationRequest updatedRequest, int whoAmIPid);

		// Token: 0x060000B9 RID: 185
		Task AddArchiveEntryForNewEntry(StudentCourseAccommodationRequest newRequest, int whoAmIPid);

		// Token: 0x060000BA RID: 186
		Task AddArchiveEntryForDeletedEntry(StudentCourseAccommodationRequest deletedRequest, int whoAmIPid);

		// Token: 0x060000BB RID: 187
		StudentCourseAccommodationRequestHistory LoadStudentCourseAccommodationRequestHistory(int PersonId, int LuCourseId);

		// Token: 0x060000BC RID: 188
		StudentCourseAccommodationRequest LoadRequestByStudentAndCourse(int StudentPersonId, int LuCourseId);

		// Token: 0x060000BD RID: 189
		IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatusWithCourseDatesInFuture(DateTime minCourseDate, eStudentCourseAccommodationRequestStatus statuses);
	}
}

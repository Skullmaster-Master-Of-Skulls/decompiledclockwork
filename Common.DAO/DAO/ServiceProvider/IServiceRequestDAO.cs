using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.DAO.ServiceProvider
{
	// Token: 0x02000037 RID: 55
	public interface IServiceRequestDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000E0 RID: 224
		IList<SPRequest> LoadRequests(DateTime StartDate, DateTime EndDate, bool IncludeAssigned, bool IncludeUnassigned, params int[] SPProviderTypeId);

		// Token: 0x060000E1 RID: 225
		SPRequest LoadRequestById(int SPRequestId);

		// Token: 0x060000E2 RID: 226
		SPRequestWithSubItems LoadRequestWithSubItemsById(int SPRequestId);

		// Token: 0x060000E3 RID: 227
		int CreateRequestCourse(int SPRequestId, SPRequestCourse RequestCourse);

		// Token: 0x060000E4 RID: 228
		int CreateRequestEvent(int SPRequestId, SPRequestEvent RequestEvent);

		// Token: 0x060000E5 RID: 229
		int CreateRequest(SPRequestWithSubItems RequestWithSubItems, bool CreateSubItems);

		// Token: 0x060000E6 RID: 230
		void UpdateRequest(SPRequestWithSubItems RequestWithSubItems, bool UpdateSubItems);

		// Token: 0x060000E7 RID: 231
		void DeleteRequest(int SPRequestId);

		// Token: 0x060000E8 RID: 232
		void UpdateRequest(SPRequest Request);

		// Token: 0x060000E9 RID: 233
		void UpdateRequestCourse(SPRequestCourse RequestCourse);

		// Token: 0x060000EA RID: 234
		void UpdateRequestEvent(SPRequestEvent RequestEvent);

		// Token: 0x060000EB RID: 235
		void DeleteRequestCourse(int SPRequestCourseId);

		// Token: 0x060000EC RID: 236
		void DeleteRequestEvent(int SPRequestEventId);

		// Token: 0x060000ED RID: 237
		void UnAssignRequestEvent(int SPRequestEventId);

		// Token: 0x060000EE RID: 238
		void UnAssignRequestCourse(int SPRequestCourseId);

		// Token: 0x060000EF RID: 239
		int AssignRequestEvent(int SPRequestEventId, SPRequestEventAssignment EventAssignment);

		// Token: 0x060000F0 RID: 240
		int AssignRequestCourse(int SPRequestCourseId, SPRequestCourseAssignment CourseAssignment);

		// Token: 0x060000F1 RID: 241
		void MergeDuplicateRequestsForTwoStudents(int PersonIdNew, int PersonIdOld);
	}
}

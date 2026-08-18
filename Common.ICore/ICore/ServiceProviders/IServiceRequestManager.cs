using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.ICore.ServiceProviders
{
	// Token: 0x02000041 RID: 65
	public interface IServiceRequestManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001A9 RID: 425
		SPRequestWithSubItems LoadRequestById(int SPRequestId, bool IncludeSubItems);

		// Token: 0x060001AA RID: 426
		SPRequestWithSubItems LoadRequestByStudentAndProviderType(int PersonId, int SPProviderTypeId, bool IncludeSubItems);

		// Token: 0x060001AB RID: 427
		IList<SPRequest> LoadRequests(DateTime StartDate, DateTime EndDate, bool IncludeAssigned, bool IncludeUnassigned, params int[] SPProviderTypeIds);

		// Token: 0x060001AC RID: 428
		int CreateRequest(SPRequestWithSubItems RequestWithSubItems, bool CreateSubItems);

		// Token: 0x060001AD RID: 429
		void UpdateRequest(SPRequestWithSubItems RequestWithSubItems, bool UpdateSubItems);

		// Token: 0x060001AE RID: 430
		void DeleteRequest(int SPRequestId);

		// Token: 0x060001AF RID: 431
		int CreateRequestCourse(int SPRequestId, SPRequestCourse RequestCourse);

		// Token: 0x060001B0 RID: 432
		void DeleteRequestCourse(int SPRequestCourseId);

		// Token: 0x060001B1 RID: 433
		void UpdateRequestCourse(SPRequestCourse RequestCourse);

		// Token: 0x060001B2 RID: 434
		int CreateRequestEvent(int SPRequestId, SPRequestEvent RequestEvent);

		// Token: 0x060001B3 RID: 435
		void DeleteRequestEvent(int SPRequestEventId);

		// Token: 0x060001B4 RID: 436
		void UpdateRequestEvent(SPRequestEvent RequestEvent);

		// Token: 0x060001B5 RID: 437
		void AssignOrUnassignRequestCourse(int SPRequestCourseId, SPRequestCourseAssignment CourseAssignment);

		// Token: 0x060001B6 RID: 438
		void AssignOrUnassignRequestEvent(int SPRequestEventId, SPRequestEventAssignment EventAssignment);

		// Token: 0x060001B7 RID: 439
		void MergeDuplicateRequestsForTwoStudents(int PersonIdNew, int PersonIdOld);
	}
}

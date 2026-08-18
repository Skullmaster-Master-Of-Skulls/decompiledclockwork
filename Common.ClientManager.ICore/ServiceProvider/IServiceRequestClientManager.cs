using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.ServiceProvider
{
	// Token: 0x0200001E RID: 30
	public interface IServiceRequestClientManager : IWebService
	{
		// Token: 0x060000BA RID: 186
		SPRequestWithSubItemsDTO LoadRequestById(int SPRequestId, bool IncludeSubItems);

		// Token: 0x060000BB RID: 187
		SPRequestWithSubItemsDTO LoadRequestByStudentAndProviderType(int PersonId, int SPProviderTypeId, bool IncludeSubItems);

		// Token: 0x060000BC RID: 188
		IList<SPRequestDTO> LoadRequests(DateTime StartDate, DateTime EndDate, bool IncludeSubItems, bool IncludeAssigned, bool IncludeUnassigned, params int[] SPProviderTypeId);

		// Token: 0x060000BD RID: 189
		int CreateRequest(SPRequestWithSubItemsDTO RequestWithSubItems, bool CreateSubItems);

		// Token: 0x060000BE RID: 190
		void UpdateRequest(SPRequestWithSubItemsDTO RequestWithSubItems, bool UpdateSubItems);

		// Token: 0x060000BF RID: 191
		void DeleteRequest(int SPRequestId);

		// Token: 0x060000C0 RID: 192
		int CreateRequestCourse(int SPRequestId, SPRequestCourseDTO RequestCourse);

		// Token: 0x060000C1 RID: 193
		void DeleteRequestCourse(int SPRequestCourseId);

		// Token: 0x060000C2 RID: 194
		void UpdateRequestCourse(SPRequestCourseDTO RequestCourse);

		// Token: 0x060000C3 RID: 195
		int CreateRequestEvent(int SPRequestId, SPRequestEventDTO RequestEvent);

		// Token: 0x060000C4 RID: 196
		void DeleteRequestEvent(int SPRequestEventId);

		// Token: 0x060000C5 RID: 197
		void UpdateRequestEvent(SPRequestEventDTO RequestEvent);

		// Token: 0x060000C6 RID: 198
		void AssignOrUnassignRequestCourse(int SPRequestCourseId, SPRequestCourseAssignmentDTO CourseAssignment);

		// Token: 0x060000C7 RID: 199
		void AssignOrUnassignRequestEvent(int SPRequestEventId, SPRequestEventAssignmentDTO EventAssignment);

		// Token: 0x060000C8 RID: 200
		void MergeDuplicateRequestsForTwoStudents(int PersonIdNew, int PersonIdOld);
	}
}

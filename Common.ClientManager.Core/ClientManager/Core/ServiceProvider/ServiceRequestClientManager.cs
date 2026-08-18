using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.ServiceProvider
{
	// Token: 0x02000021 RID: 33
	public class ServiceRequestClientManager : IServiceRequestClientManager, IWebService
	{
		// Token: 0x06000101 RID: 257 RVA: 0x00006024 File Offset: 0x00004224
		public SPRequestWithSubItemsDTO LoadRequestById(int SPRequestId, bool IncludeSubItems)
		{
			LoadRequestByIdReq loadRequestByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestByIdReq>();
			loadRequestByIdReq.SPRequestId = SPRequestId;
			loadRequestByIdReq.IncludeSubItems = IncludeSubItems;
			return ClientServiceFactory.GetClientInstance<IServiceRequest>().LoadRequestById(loadRequestByIdReq).RequestWithSubItems;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006064 File Offset: 0x00004264
		public SPRequestWithSubItemsDTO LoadRequestByStudentAndProviderType(int PersonId, int SPProviderTypeId, bool IncludeSubItems)
		{
			LoadRequestByStudentAndProviderTypeReq loadRequestByStudentAndProviderTypeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestByStudentAndProviderTypeReq>();
			loadRequestByStudentAndProviderTypeReq.PersonId = PersonId;
			loadRequestByStudentAndProviderTypeReq.IncludeSubItems = IncludeSubItems;
			loadRequestByStudentAndProviderTypeReq.SPProviderTypeId = SPProviderTypeId;
			return ClientServiceFactory.GetClientInstance<IServiceRequest>().LoadRequestByStudentAndProviderType(loadRequestByStudentAndProviderTypeReq).RequestWithSubItems;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000060AC File Offset: 0x000042AC
		public IList<SPRequestDTO> LoadRequests(DateTime StartDate, DateTime EndDate, bool IncludeSubItems, bool IncludeAssigned, bool IncludeUnassigned, params int[] SPProviderTypeId)
		{
			LoadRequestsReq loadRequestsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadRequestsReq>();
			loadRequestsReq.StartDate = StartDate;
			loadRequestsReq.EndDate = EndDate;
			loadRequestsReq.IncludeSubItems = IncludeSubItems;
			loadRequestsReq.IncludeUnassigned = IncludeUnassigned;
			loadRequestsReq.IncludeAssigned = IncludeAssigned;
			loadRequestsReq.SPProviderTypeIds = SPProviderTypeId;
			return ClientServiceFactory.GetClientInstance<IServiceRequest>().LoadRequests(loadRequestsReq).Requests;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000610C File Offset: 0x0000430C
		public int CreateRequest(SPRequestWithSubItemsDTO RequestWithSubItems, bool CreateSubItems)
		{
			CreateRequestReq createRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateRequestReq>();
			createRequestReq.RequestWithSubItems = RequestWithSubItems;
			createRequestReq.CreateSubItems = CreateSubItems;
			return ClientServiceFactory.GetClientInstance<IServiceRequest>().CreateRequest(createRequestReq).SPRequestId;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000614C File Offset: 0x0000434C
		public void UpdateRequest(SPRequestWithSubItemsDTO RequestWithSubItems, bool UpdateSubItems)
		{
			UpdateRequestReq updateRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestReq>();
			updateRequestReq.RequestWithSubItems = RequestWithSubItems;
			updateRequestReq.UpdateSubItems = UpdateSubItems;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().UpdateRequest(updateRequestReq);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006184 File Offset: 0x00004384
		public void DeleteRequest(int SPRequestId)
		{
			DeleteRequestReq deleteRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteRequestReq>();
			deleteRequestReq.SPRequestId = SPRequestId;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().DeleteRequest(deleteRequestReq);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000061B4 File Offset: 0x000043B4
		public int CreateRequestCourse(int SPRequestId, SPRequestCourseDTO RequestCourse)
		{
			CreateRequestCourseReq createRequestCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateRequestCourseReq>();
			createRequestCourseReq.SPRequestId = SPRequestId;
			createRequestCourseReq.RequestCourse = RequestCourse;
			return ClientServiceFactory.GetClientInstance<IServiceRequest>().CreateRequestCourse(createRequestCourseReq).SPRequestCourseId;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000061F4 File Offset: 0x000043F4
		public void DeleteRequestCourse(int SPRequestCourseId)
		{
			DeleteRequestCourseReq deleteRequestCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteRequestCourseReq>();
			deleteRequestCourseReq.SPRequestCourseId = SPRequestCourseId;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().DeleteRequestCourse(deleteRequestCourseReq);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006224 File Offset: 0x00004424
		public void UpdateRequestCourse(SPRequestCourseDTO RequestCourse)
		{
			UpdateRequestCourseReq updateRequestCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestCourseReq>();
			updateRequestCourseReq.RequestCourse = RequestCourse;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().UpdateRequestCourse(updateRequestCourseReq);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00006254 File Offset: 0x00004454
		public int CreateRequestEvent(int SPRequestId, SPRequestEventDTO RequestEvent)
		{
			CreateRequestEventReq createRequestEventReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateRequestEventReq>();
			createRequestEventReq.SPRequestId = SPRequestId;
			createRequestEventReq.RequestEvent = RequestEvent;
			return ClientServiceFactory.GetClientInstance<IServiceRequest>().CreateRequestEvent(createRequestEventReq).SPRequestEventId;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006294 File Offset: 0x00004494
		public void DeleteRequestEvent(int SPRequestEventId)
		{
			DeleteRequestEventReq deleteRequestEventReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteRequestEventReq>();
			deleteRequestEventReq.SPRequestEventId = SPRequestEventId;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().DeleteRequestEvent(deleteRequestEventReq);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000062C4 File Offset: 0x000044C4
		public void UpdateRequestEvent(SPRequestEventDTO RequestEvent)
		{
			UpdateRequestEventReq updateRequestEventReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestEventReq>();
			updateRequestEventReq.RequestEvent = RequestEvent;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().UpdateRequestEvent(updateRequestEventReq);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000062F4 File Offset: 0x000044F4
		public void AssignOrUnassignRequestCourse(int SPRequestCourseId, SPRequestCourseAssignmentDTO CourseAssignment)
		{
			AssignOrUnassignRequestCourseReq assignOrUnassignRequestCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignOrUnassignRequestCourseReq>();
			assignOrUnassignRequestCourseReq.SPRequestCourseId = SPRequestCourseId;
			assignOrUnassignRequestCourseReq.RequestCourseAssignment = CourseAssignment;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().AssignOrUnassignRequestCourse(assignOrUnassignRequestCourseReq);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000632C File Offset: 0x0000452C
		public void AssignOrUnassignRequestEvent(int SPRequestEventId, SPRequestEventAssignmentDTO EventAssignment)
		{
			AssignOrUnassignRequestEventReq assignOrUnassignRequestEventReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignOrUnassignRequestEventReq>();
			assignOrUnassignRequestEventReq.SPRequestEventId = SPRequestEventId;
			assignOrUnassignRequestEventReq.RequestEventAssignment = EventAssignment;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().AssignOrUnassignRequestEvent(assignOrUnassignRequestEventReq);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006364 File Offset: 0x00004564
		public void MergeDuplicateRequestsForTwoStudents(int PersonIdNew, int PersonIdOld)
		{
			MergeDuplicateRequestsForTwoStudentsReq mergeDuplicateRequestsForTwoStudentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MergeDuplicateRequestsForTwoStudentsReq>();
			mergeDuplicateRequestsForTwoStudentsReq.PersonIdNew = PersonIdNew;
			mergeDuplicateRequestsForTwoStudentsReq.PersonIdOld = PersonIdOld;
			ClientServiceFactory.GetClientInstance<IServiceRequest>().MergeDuplicateRequestsForTwoStudents(mergeDuplicateRequestsForTwoStudentsReq);
		}
	}
}

using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.ServiceProvider;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.ServiceProvider
{
	// Token: 0x0200001B RID: 27
	public class ServiceRequestRestClientManager : BearerTokenRestProxy<IServiceRequestClientManager>, IServiceRequestClientManager, IWebService
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004311 File Offset: 0x00002511
		public ServiceRequestRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000431B File Offset: 0x0000251B
		public ServiceRequestRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004326 File Offset: 0x00002526
		public SPRequestWithSubItemsDTO LoadRequestById(int SPRequestId, bool IncludeSubItems)
		{
			return base.Get<SPRequestWithSubItemsDTO>(string.Format("servicerequest/requestid/{0}/includesubitems/{1}", SPRequestId, IncludeSubItems), true);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004345 File Offset: 0x00002545
		public SPRequestWithSubItemsDTO LoadRequestByStudentAndProviderType(int PersonId, int SPProviderTypeId, bool IncludeSubItems)
		{
			return base.Get<SPRequestWithSubItemsDTO>(string.Format("servicerequest/studentpersonid/{0}/providertypeid/{1}/includesubitems/{2}", PersonId, SPProviderTypeId, IncludeSubItems), true);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000436C File Offset: 0x0000256C
		public IList<SPRequestDTO> LoadRequests(DateTime StartDate, DateTime EndDate, bool IncludeSubItems, bool IncludeAssigned, bool IncludeUnassigned, params int[] SPProviderTypeId)
		{
			return base.GetMany<SPRequestDTO>(string.Format("servicerequest/range/{0}/{1}?includeassigned={2}&includeunassigned={3}&providertypeids={4}", new object[]
			{
				StartDate,
				EndDate,
				IncludeAssigned,
				IncludeUnassigned,
				SPProviderTypeId.CommaSeparatedValuesWithoutSpace<int>()
			}), true);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000043C0 File Offset: 0x000025C0
		public int CreateRequest(SPRequestWithSubItemsDTO RequestWithSubItems, bool CreateSubItems)
		{
			CreateRequestReq createRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateRequestReq>();
			createRequestReq.RequestWithSubItems = RequestWithSubItems;
			createRequestReq.CreateSubItems = CreateSubItems;
			return base.Post<CreateRequestReq, int>(createRequestReq, "servicerequest");
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000043F4 File Offset: 0x000025F4
		public void UpdateRequest(SPRequestWithSubItemsDTO RequestWithSubItems, bool UpdateSubItems)
		{
			UpdateRequestReq updateRequestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRequestReq>();
			updateRequestReq.RequestWithSubItems = RequestWithSubItems;
			updateRequestReq.UpdateSubItems = UpdateSubItems;
			base.Put<UpdateRequestReq>(updateRequestReq, "servicerequest");
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004426 File Offset: 0x00002626
		public void DeleteRequest(int SPRequestId)
		{
			base.Delete(string.Format("servicerequest/requestid/{0}", SPRequestId));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004440 File Offset: 0x00002640
		public int CreateRequestCourse(int SPRequestId, SPRequestCourseDTO RequestCourse)
		{
			CreateRequestCourseReq createRequestCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateRequestCourseReq>();
			createRequestCourseReq.SPRequestId = SPRequestId;
			createRequestCourseReq.RequestCourse = RequestCourse;
			return base.Post<CreateRequestCourseReq, int>(createRequestCourseReq, "servicerequest/course");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004472 File Offset: 0x00002672
		public void DeleteRequestCourse(int SPRequestCourseId)
		{
			base.Delete(string.Format("servicerequest/course/courseid/{0}", SPRequestCourseId));
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000448A File Offset: 0x0000268A
		public void UpdateRequestCourse(SPRequestCourseDTO RequestCourse)
		{
			base.Put<SPRequestCourseDTO>(RequestCourse, "servicerequest/course");
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004498 File Offset: 0x00002698
		public int CreateRequestEvent(int SPRequestId, SPRequestEventDTO RequestEvent)
		{
			CreateRequestEventReq createRequestEventReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateRequestEventReq>();
			createRequestEventReq.SPRequestId = SPRequestId;
			createRequestEventReq.RequestEvent = RequestEvent;
			return base.Post<CreateRequestEventReq, int>(createRequestEventReq, "servicerequest/event");
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000044CA File Offset: 0x000026CA
		public void DeleteRequestEvent(int SPRequestEventId)
		{
			base.Delete(string.Format("servicerequest/event/eventid/{0}", SPRequestEventId));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000044E2 File Offset: 0x000026E2
		public void UpdateRequestEvent(SPRequestEventDTO RequestEvent)
		{
			base.Put<SPRequestEventDTO>(RequestEvent, "servicerequest/event");
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000044F0 File Offset: 0x000026F0
		public void AssignOrUnassignRequestCourse(int SPRequestCourseId, SPRequestCourseAssignmentDTO CourseAssignment)
		{
			AssignOrUnassignRequestCourseReq assignOrUnassignRequestCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignOrUnassignRequestCourseReq>();
			assignOrUnassignRequestCourseReq.SPRequestCourseId = SPRequestCourseId;
			assignOrUnassignRequestCourseReq.RequestCourseAssignment = CourseAssignment;
			base.Post<AssignOrUnassignRequestCourseReq>(assignOrUnassignRequestCourseReq, "servicerequest/assignedorunassignedcourse");
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004524 File Offset: 0x00002724
		public void AssignOrUnassignRequestEvent(int SPRequestEventId, SPRequestEventAssignmentDTO EventAssignment)
		{
			AssignOrUnassignRequestEventReq assignOrUnassignRequestEventReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignOrUnassignRequestEventReq>();
			assignOrUnassignRequestEventReq.SPRequestEventId = SPRequestEventId;
			assignOrUnassignRequestEventReq.RequestEventAssignment = EventAssignment;
			base.Post<AssignOrUnassignRequestEventReq>(assignOrUnassignRequestEventReq, "servicerequest/assignedorunassignedevent");
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004558 File Offset: 0x00002758
		public void MergeDuplicateRequestsForTwoStudents(int PersonIdNew, int PersonIdOld)
		{
			MergeDuplicateRequestsForTwoStudentsReq mergeDuplicateRequestsForTwoStudentsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MergeDuplicateRequestsForTwoStudentsReq>();
			mergeDuplicateRequestsForTwoStudentsReq.PersonIdNew = PersonIdNew;
			mergeDuplicateRequestsForTwoStudentsReq.PersonIdOld = PersonIdOld;
			base.Post<MergeDuplicateRequestsForTwoStudentsReq>(mergeDuplicateRequestsForTwoStudentsReq, "servicerequest/mergeduplicaterequestsfortwostudents");
		}
	}
}

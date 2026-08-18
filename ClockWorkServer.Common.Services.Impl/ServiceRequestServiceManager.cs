using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Core.Mappers.ServiceProvider;
using TechnoPro.Common.Core.ServiceProvider;
using TechnoPro.Common.ICore.ServiceProviders;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000087 RID: 135
	public class ServiceRequestServiceManager : IServiceRequest, IService
	{
		// Token: 0x060004EC RID: 1260 RVA: 0x000172D8 File Offset: 0x000154D8
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000172EC File Offset: 0x000154EC
		public LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			SPRequestWithSubItems sprequestWithSubItems = serviceRequestManager.LoadRequestById(Request.SPRequestId, Request.IncludeSubItems);
			return new LoadRequestByIdResp
			{
				RequestWithSubItems = ((sprequestWithSubItems == null) ? null : sprequestWithSubItems.ToDTO())
			};
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00017338 File Offset: 0x00015538
		public LoadRequestByStudentAndProviderTypeResp LoadRequestByStudentAndProviderType(LoadRequestByStudentAndProviderTypeReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			SPRequestWithSubItems sprequestWithSubItems = serviceRequestManager.LoadRequestByStudentAndProviderType(Request.PersonId, Request.SPProviderTypeId, Request.IncludeSubItems);
			return new LoadRequestByStudentAndProviderTypeResp
			{
				RequestWithSubItems = ((sprequestWithSubItems == null) ? null : sprequestWithSubItems.ToDTO())
			};
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00017388 File Offset: 0x00015588
		public LoadRequestsResp LoadRequests(LoadRequestsReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			IList<SPRequest> list = serviceRequestManager.LoadRequests(Request.StartDate, Request.EndDate, Request.IncludeAssigned, Request.IncludeUnassigned, (Request.SPProviderTypeIds == null) ? new int[0] : Request.SPProviderTypeIds.ToArray<int>());
			LoadRequestsResp loadRequestsResp = new LoadRequestsResp();
			IList<SPRequestDTO> requests;
			if (list != null)
			{
				requests = list.ToList<SPRequest>().ConvertAll<SPRequestDTO>((SPRequest f) => f.ToDTO());
			}
			else
			{
				requests = null;
			}
			loadRequestsResp.Requests = requests;
			return loadRequestsResp;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001741C File Offset: 0x0001561C
		public CreateRequestResp CreateRequest(CreateRequestReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			int sprequestId = serviceRequestManager.CreateRequest(Request.RequestWithSubItems.ToDomainObject(), Request.CreateSubItems);
			return new CreateRequestResp
			{
				SPRequestId = sprequestId
			};
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00017460 File Offset: 0x00015660
		public UpdateRequestResp UpdateRequest(UpdateRequestReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.UpdateRequest(Request.RequestWithSubItems.ToDomainObject(), Request.UpdateSubItems);
			return new UpdateRequestResp();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001749C File Offset: 0x0001569C
		public DeleteRequestResp DeleteRequest(DeleteRequestReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.DeleteRequest(Request.SPRequestId);
			return new DeleteRequestResp();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000174CC File Offset: 0x000156CC
		public CreateRequestCourseResp CreateRequestCourse(CreateRequestCourseReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			int sprequestCourseId = serviceRequestManager.CreateRequestCourse(Request.SPRequestId, Request.RequestCourse.ToDomainObject());
			return new CreateRequestCourseResp
			{
				SPRequestCourseId = sprequestCourseId
			};
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00017510 File Offset: 0x00015710
		public DeleteRequestCourseResp DeleteRequestCourse(DeleteRequestCourseReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.DeleteRequestCourse(Request.SPRequestCourseId);
			return new DeleteRequestCourseResp();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00017540 File Offset: 0x00015740
		public UpdateRequestCourseResp UpdateRequestCourse(UpdateRequestCourseReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.UpdateRequestCourse(Request.RequestCourse.ToDomainObject());
			return new UpdateRequestCourseResp();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00017578 File Offset: 0x00015778
		public CreateRequestEventResp CreateRequestEvent(CreateRequestEventReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			int sprequestEventId = serviceRequestManager.CreateRequestEvent(Request.SPRequestId, Request.RequestEvent.ToDomainObject());
			return new CreateRequestEventResp
			{
				SPRequestEventId = sprequestEventId
			};
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000175BC File Offset: 0x000157BC
		public DeleteRequestEventResp DeleteRequestEvent(DeleteRequestEventReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.DeleteRequestEvent(Request.SPRequestEventId);
			return new DeleteRequestEventResp();
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x000175EC File Offset: 0x000157EC
		public UpdateRequestEventResp UpdateRequestEvent(UpdateRequestEventReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.UpdateRequestEvent(Request.RequestEvent.ToDomainObject());
			return new UpdateRequestEventResp();
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017624 File Offset: 0x00015824
		public AssignOrUnassignRequestCourseResp AssignOrUnassignRequestCourse(AssignOrUnassignRequestCourseReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.AssignOrUnassignRequestCourse(Request.SPRequestCourseId, Request.RequestCourseAssignment.ToDomainObject());
			return new AssignOrUnassignRequestCourseResp();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00017660 File Offset: 0x00015860
		public AssignOrUnassignRequestEventResp AssignOrUnassignRequestEvent(AssignOrUnassignRequestEventReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.AssignOrUnassignRequestEvent(Request.SPRequestEventId, Request.RequestEventAssignment.ToDomainObject());
			return new AssignOrUnassignRequestEventResp();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001769C File Offset: 0x0001589C
		public MergeDuplicateRequestsForTwoStudentsResp MergeDuplicateRequestsForTwoStudents(MergeDuplicateRequestsForTwoStudentsReq Request)
		{
			IServiceRequestManager serviceRequestManager = new ServiceRequestManager(Request.GetOperationContext());
			serviceRequestManager.MergeDuplicateRequestsForTwoStudents(Request.PersonIdNew, Request.PersonIdOld);
			return new MergeDuplicateRequestsForTwoStudentsResp();
		}
	}
}

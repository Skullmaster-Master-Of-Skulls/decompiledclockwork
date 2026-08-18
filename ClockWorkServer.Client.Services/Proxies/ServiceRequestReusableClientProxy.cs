using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000131 RID: 305
	public class ServiceRequestReusableClientProxy : WCFTokenBasedReusableClientProxy<IServiceRequest>, IServiceRequest, IService
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x0001E056 File Offset: 0x0001C256
		public ServiceRequestReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0001E061 File Offset: 0x0001C261
		public ServiceRequestReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001E070 File Offset: 0x0001C270
		public AssignOrUnassignRequestCourseResp AssignOrUnassignRequestCourse(AssignOrUnassignRequestCourseReq Request)
		{
			return this.WrapServiceMethod<AssignOrUnassignRequestCourseResp>(() => this.Proxy.AssignOrUnassignRequestCourse(Request));
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0001E0A8 File Offset: 0x0001C2A8
		public AssignOrUnassignRequestEventResp AssignOrUnassignRequestEvent(AssignOrUnassignRequestEventReq Request)
		{
			return this.WrapServiceMethod<AssignOrUnassignRequestEventResp>(() => this.Proxy.AssignOrUnassignRequestEvent(Request));
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0001E0E0 File Offset: 0x0001C2E0
		public CreateRequestResp CreateRequest(CreateRequestReq Request)
		{
			return this.WrapServiceMethod<CreateRequestResp>(() => this.Proxy.CreateRequest(Request));
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0001E118 File Offset: 0x0001C318
		public CreateRequestCourseResp CreateRequestCourse(CreateRequestCourseReq Request)
		{
			return this.WrapServiceMethod<CreateRequestCourseResp>(() => this.Proxy.CreateRequestCourse(Request));
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0001E150 File Offset: 0x0001C350
		public CreateRequestEventResp CreateRequestEvent(CreateRequestEventReq Request)
		{
			return this.WrapServiceMethod<CreateRequestEventResp>(() => this.Proxy.CreateRequestEvent(Request));
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0001E188 File Offset: 0x0001C388
		public DeleteRequestResp DeleteRequest(DeleteRequestReq Request)
		{
			return this.WrapServiceMethod<DeleteRequestResp>(() => this.Proxy.DeleteRequest(Request));
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0001E1C0 File Offset: 0x0001C3C0
		public DeleteRequestEventResp DeleteRequestEvent(DeleteRequestEventReq Request)
		{
			return this.WrapServiceMethod<DeleteRequestEventResp>(() => this.Proxy.DeleteRequestEvent(Request));
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0001E1F8 File Offset: 0x0001C3F8
		public LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request)
		{
			return this.WrapServiceMethod<LoadRequestByIdResp>(() => this.Proxy.LoadRequestById(Request));
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0001E230 File Offset: 0x0001C430
		public LoadRequestByStudentAndProviderTypeResp LoadRequestByStudentAndProviderType(LoadRequestByStudentAndProviderTypeReq Request)
		{
			return this.WrapServiceMethod<LoadRequestByStudentAndProviderTypeResp>(() => this.Proxy.LoadRequestByStudentAndProviderType(Request));
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0001E268 File Offset: 0x0001C468
		public LoadRequestsResp LoadRequests(LoadRequestsReq Request)
		{
			return this.WrapServiceMethod<LoadRequestsResp>(() => this.Proxy.LoadRequests(Request));
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0001E2A0 File Offset: 0x0001C4A0
		public MergeDuplicateRequestsForTwoStudentsResp MergeDuplicateRequestsForTwoStudents(MergeDuplicateRequestsForTwoStudentsReq Request)
		{
			return this.WrapServiceMethod<MergeDuplicateRequestsForTwoStudentsResp>(() => this.Proxy.MergeDuplicateRequestsForTwoStudents(Request));
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0001E2D8 File Offset: 0x0001C4D8
		public UpdateRequestResp UpdateRequest(UpdateRequestReq Request)
		{
			return this.WrapServiceMethod<UpdateRequestResp>(() => this.Proxy.UpdateRequest(Request));
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0001E310 File Offset: 0x0001C510
		public UpdateRequestCourseResp UpdateRequestCourse(UpdateRequestCourseReq Request)
		{
			return this.WrapServiceMethod<UpdateRequestCourseResp>(() => this.Proxy.UpdateRequestCourse(Request));
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001E348 File Offset: 0x0001C548
		public UpdateRequestEventResp UpdateRequestEvent(UpdateRequestEventReq Request)
		{
			return this.WrapServiceMethod<UpdateRequestEventResp>(() => this.Proxy.UpdateRequestEvent(Request));
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001E380 File Offset: 0x0001C580
		public DeleteRequestCourseResp DeleteRequestCourse(DeleteRequestCourseReq Request)
		{
			return this.WrapServiceMethod<DeleteRequestCourseResp>(() => this.Proxy.DeleteRequestCourse(Request));
		}
	}
}

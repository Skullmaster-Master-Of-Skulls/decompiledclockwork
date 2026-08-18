using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000132 RID: 306
	internal class ServiceRequestClientBaseProxy : ClientBase<IServiceRequest>, IServiceRequest, IService
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x0001E3B8 File Offset: 0x0001C5B8
		public ServiceRequestClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001E3C3 File Offset: 0x0001C5C3
		public ServiceRequestClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
		public AssignOrUnassignRequestCourseResp AssignOrUnassignRequestCourse(AssignOrUnassignRequestCourseReq Request)
		{
			return base.Channel.AssignOrUnassignRequestCourse(Request);
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001E3F0 File Offset: 0x0001C5F0
		public AssignOrUnassignRequestEventResp AssignOrUnassignRequestEvent(AssignOrUnassignRequestEventReq Request)
		{
			return base.Channel.AssignOrUnassignRequestEvent(Request);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001E410 File Offset: 0x0001C610
		public CreateRequestResp CreateRequest(CreateRequestReq Request)
		{
			return base.Channel.CreateRequest(Request);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001E430 File Offset: 0x0001C630
		public CreateRequestCourseResp CreateRequestCourse(CreateRequestCourseReq Request)
		{
			return base.Channel.CreateRequestCourse(Request);
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0001E450 File Offset: 0x0001C650
		public CreateRequestEventResp CreateRequestEvent(CreateRequestEventReq Request)
		{
			return base.Channel.CreateRequestEvent(Request);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0001E470 File Offset: 0x0001C670
		public DeleteRequestResp DeleteRequest(DeleteRequestReq Request)
		{
			return base.Channel.DeleteRequest(Request);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0001E490 File Offset: 0x0001C690
		public DeleteRequestCourseResp DeleteRequestCourse(DeleteRequestCourseReq Request)
		{
			return base.Channel.DeleteRequestCourse(Request);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0001E4B0 File Offset: 0x0001C6B0
		public DeleteRequestEventResp DeleteRequestEvent(DeleteRequestEventReq Request)
		{
			return base.Channel.DeleteRequestEvent(Request);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0001E4D0 File Offset: 0x0001C6D0
		public LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request)
		{
			return base.Channel.LoadRequestById(Request);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x0001E4F0 File Offset: 0x0001C6F0
		public LoadRequestByStudentAndProviderTypeResp LoadRequestByStudentAndProviderType(LoadRequestByStudentAndProviderTypeReq Request)
		{
			return base.Channel.LoadRequestByStudentAndProviderType(Request);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0001E510 File Offset: 0x0001C710
		public LoadRequestsResp LoadRequests(LoadRequestsReq Request)
		{
			return base.Channel.LoadRequests(Request);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0001E530 File Offset: 0x0001C730
		public MergeDuplicateRequestsForTwoStudentsResp MergeDuplicateRequestsForTwoStudents(MergeDuplicateRequestsForTwoStudentsReq Request)
		{
			return base.Channel.MergeDuplicateRequestsForTwoStudents(Request);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0001E550 File Offset: 0x0001C750
		public UpdateRequestResp UpdateRequest(UpdateRequestReq Request)
		{
			return base.Channel.UpdateRequest(Request);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0001E570 File Offset: 0x0001C770
		public UpdateRequestCourseResp UpdateRequestCourse(UpdateRequestCourseReq Request)
		{
			return base.Channel.UpdateRequestCourse(Request);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x0001E590 File Offset: 0x0001C790
		public UpdateRequestEventResp UpdateRequestEvent(UpdateRequestEventReq Request)
		{
			return base.Channel.UpdateRequestEvent(Request);
		}
	}
}

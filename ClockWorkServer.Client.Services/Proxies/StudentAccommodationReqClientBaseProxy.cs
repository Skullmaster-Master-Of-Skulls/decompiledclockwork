using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000142 RID: 322
	internal class StudentAccommodationReqClientBaseProxy : ClientBase<IStudentAccommodationReq>, IStudentAccommodationReq, IService
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x0001EEAC File Offset: 0x0001D0AC
		public StudentAccommodationReqClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0001EEB7 File Offset: 0x0001D0B7
		public StudentAccommodationReqClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
		public AddRequestResp AddRequest(AddRequestReq Request)
		{
			return base.Channel.AddRequest(Request);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0001EEE2 File Offset: 0x0001D0E2
		public void DeleteRequest(DeleteRequestReq Request)
		{
			base.Channel.DeleteRequest(Request);
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		public LoadCourseRegistrationsWithRequestByStatusResp LoadCourseRegistrationsWithRequestByStatus(LoadCourseRegistrationsWithRequestByStatusReq Request)
		{
			return base.Channel.LoadCourseRegistrationsWithRequestByStatus(Request);
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0001EF14 File Offset: 0x0001D114
		public LoadCourseRegistrationsWithRequestByStudentAndDateResp LoadCourseRegistrationsWithRequestByStudentAndDate(LoadCourseRegistrationsWithRequestByStudentAndDateReq Request)
		{
			return base.Channel.LoadCourseRegistrationsWithRequestByStudentAndDate(Request);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0001EF34 File Offset: 0x0001D134
		public LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request)
		{
			return base.Channel.LoadRequestById(Request);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0001EF54 File Offset: 0x0001D154
		public LoadRequestsByStudentAndDateResp LoadRequestsByStudentAndDate(LoadRequestsByStudentAndDateReq Request)
		{
			return base.Channel.LoadRequestsByStudentAndDate(Request);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0001EF72 File Offset: 0x0001D172
		public void UpdateRequest(UpdateRequestReq Request)
		{
			base.Channel.UpdateRequest(Request);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0001EF82 File Offset: 0x0001D182
		public void UpdateRequestStatus(UpdateRequestStatusReq Request)
		{
			base.Channel.UpdateRequestStatus(Request);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0001EF94 File Offset: 0x0001D194
		public LoadStudentCourseAccommodationRequestHistoryResp LoadStudentCourseAccommodationRequestHistory(LoadStudentCourseAccommodationRequestHistoryReq Request)
		{
			return base.Channel.LoadStudentCourseAccommodationRequestHistory(Request);
		}
	}
}

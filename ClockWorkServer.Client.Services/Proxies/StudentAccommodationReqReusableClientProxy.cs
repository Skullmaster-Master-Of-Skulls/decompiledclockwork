using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000141 RID: 321
	public class StudentAccommodationReqReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentAccommodationReq>, IStudentAccommodationReq, IService
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x0001EC9A File Offset: 0x0001CE9A
		public StudentAccommodationReqReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0001ECA5 File Offset: 0x0001CEA5
		public StudentAccommodationReqReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
		public AddRequestResp AddRequest(AddRequestReq Request)
		{
			return this.WrapServiceMethod<AddRequestResp>(() => this.Proxy.AddRequest(Request));
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0001ECEC File Offset: 0x0001CEEC
		public void DeleteRequest(DeleteRequestReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteRequest(Request);
			});
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0001ED24 File Offset: 0x0001CF24
		public LoadCourseRegistrationsWithRequestByStatusResp LoadCourseRegistrationsWithRequestByStatus(LoadCourseRegistrationsWithRequestByStatusReq Request)
		{
			return this.WrapServiceMethod<LoadCourseRegistrationsWithRequestByStatusResp>(() => this.Proxy.LoadCourseRegistrationsWithRequestByStatus(Request));
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		public LoadCourseRegistrationsWithRequestByStudentAndDateResp LoadCourseRegistrationsWithRequestByStudentAndDate(LoadCourseRegistrationsWithRequestByStudentAndDateReq Request)
		{
			return this.WrapServiceMethod<LoadCourseRegistrationsWithRequestByStudentAndDateResp>(() => this.Proxy.LoadCourseRegistrationsWithRequestByStudentAndDate(Request));
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0001ED94 File Offset: 0x0001CF94
		public LoadRequestByIdResp LoadRequestById(LoadRequestByIdReq Request)
		{
			return this.WrapServiceMethod<LoadRequestByIdResp>(() => this.Proxy.LoadRequestById(Request));
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0001EDCC File Offset: 0x0001CFCC
		public LoadRequestsByStudentAndDateResp LoadRequestsByStudentAndDate(LoadRequestsByStudentAndDateReq Request)
		{
			return this.WrapServiceMethod<LoadRequestsByStudentAndDateResp>(() => this.Proxy.LoadRequestsByStudentAndDate(Request));
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0001EE04 File Offset: 0x0001D004
		public void UpdateRequest(UpdateRequestReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateRequest(Request);
			});
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0001EE3C File Offset: 0x0001D03C
		public void UpdateRequestStatus(UpdateRequestStatusReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateRequestStatus(Request);
			});
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0001EE74 File Offset: 0x0001D074
		public LoadStudentCourseAccommodationRequestHistoryResp LoadStudentCourseAccommodationRequestHistory(LoadStudentCourseAccommodationRequestHistoryReq Request)
		{
			return this.WrapServiceMethod<LoadStudentCourseAccommodationRequestHistoryResp>(() => this.Proxy.LoadStudentCourseAccommodationRequestHistory(Request));
		}
	}
}

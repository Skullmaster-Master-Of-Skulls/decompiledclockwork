using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000082 RID: 130
	public class AccommodationsReusableClientProxy : WCFTokenBasedReusableClientProxy<IAccommodations>, IAccommodations, IService
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0000EF76 File Offset: 0x0000D176
		public AccommodationsReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000EF81 File Offset: 0x0000D181
		public AccommodationsReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000EF90 File Offset: 0x0000D190
		public LoadAccommodationChangesResp LoadAccommodationChanges(LoadAccommodationChangesReq Request)
		{
			return this.WrapServiceMethod<LoadAccommodationChangesResp>(() => this.Proxy.LoadAccommodationChanges(Request));
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		public LoadStudentsRegisteredCoursesWithAccommodationsResp LoadStudentsRegisteredCoursesWithAccommodations(LoadStudentsRegisteredCoursesWithAccommodationsReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsRegisteredCoursesWithAccommodationsResp>(() => this.Proxy.LoadStudentsRegisteredCoursesWithAccommodations(Request));
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000F000 File Offset: 0x0000D200
		public LoadAccommodationsByStudentAndCourseOrTemplateResp LoadAccommodationsByStudentAndCourseOrTemplate(LoadAccommodationsByStudentAndCourseOrTemplateReq Request)
		{
			return this.WrapServiceMethod<LoadAccommodationsByStudentAndCourseOrTemplateResp>(() => this.Proxy.LoadAccommodationsByStudentAndCourseOrTemplate(Request));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0000F038 File Offset: 0x0000D238
		public void ClearAccommodations(ClearAccommodationsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearAccommodations(Request);
			});
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0000F070 File Offset: 0x0000D270
		public LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsResp LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsResp>(() => this.Proxy.LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(Request));
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		public void MarkAccommodationLetterIssued(MarkAccommodationLetterIssuedReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MarkAccommodationLetterIssued(Request);
			});
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0000F0E0 File Offset: 0x0000D2E0
		public void MergeOrReplaceAccommodations(MergeOrReplaceAccommodationsReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.MergeOrReplaceAccommodations(Request);
			});
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000F118 File Offset: 0x0000D318
		public GetStudentAccommodationsExpiryDateResp GetStudentAccommodationsExpiryDate(GetStudentAccommodationsExpiryDateReq Request)
		{
			return this.WrapServiceMethod<GetStudentAccommodationsExpiryDateResp>(() => this.Proxy.GetStudentAccommodationsExpiryDate(Request));
		}
	}
}

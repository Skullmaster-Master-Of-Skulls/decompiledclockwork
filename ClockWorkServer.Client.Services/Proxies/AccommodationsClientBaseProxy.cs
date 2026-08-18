using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000083 RID: 131
	internal class AccommodationsClientBaseProxy : ClientBase<IAccommodations>, IAccommodations, IService
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x0000F150 File Offset: 0x0000D350
		public AccommodationsClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0000F15B File Offset: 0x0000D35B
		public AccommodationsClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000F168 File Offset: 0x0000D368
		public LoadAccommodationChangesResp LoadAccommodationChanges(LoadAccommodationChangesReq Request)
		{
			return base.Channel.LoadAccommodationChanges(Request);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000F188 File Offset: 0x0000D388
		public LoadStudentsRegisteredCoursesWithAccommodationsResp LoadStudentsRegisteredCoursesWithAccommodations(LoadStudentsRegisteredCoursesWithAccommodationsReq Request)
		{
			return base.Channel.LoadStudentsRegisteredCoursesWithAccommodations(Request);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		public LoadAccommodationsByStudentAndCourseOrTemplateResp LoadAccommodationsByStudentAndCourseOrTemplate(LoadAccommodationsByStudentAndCourseOrTemplateReq Request)
		{
			return base.Channel.LoadAccommodationsByStudentAndCourseOrTemplate(Request);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0000F1C6 File Offset: 0x0000D3C6
		public void ClearAccommodations(ClearAccommodationsReq Request)
		{
			base.Channel.ClearAccommodations(Request);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0000F1D8 File Offset: 0x0000D3D8
		public LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsResp LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(LoadStudentsRegisteredCoursesWithAccommodationsAndRequestsReq Request)
		{
			return base.Channel.LoadStudentsRegisteredCoursesWithAccommodationsAndRequests(Request);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
		public void MarkAccommodationLetterIssued(MarkAccommodationLetterIssuedReq Request)
		{
			base.Channel.MarkAccommodationLetterIssued(Request);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0000F206 File Offset: 0x0000D406
		public void MergeOrReplaceAccommodations(MergeOrReplaceAccommodationsReq Request)
		{
			base.Channel.MergeOrReplaceAccommodations(Request);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0000F218 File Offset: 0x0000D418
		public GetStudentAccommodationsExpiryDateResp GetStudentAccommodationsExpiryDate(GetStudentAccommodationsExpiryDateReq Request)
		{
			return base.Channel.GetStudentAccommodationsExpiryDate(Request);
		}
	}
}

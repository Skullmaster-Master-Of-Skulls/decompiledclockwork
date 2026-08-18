using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200002E RID: 46
	public class ExamRequestClientProxy : WCFTokenBasedReusableClientProxy<IExamRequest>, IExamRequest, IService
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000836E File Offset: 0x0000656E
		public ExamRequestClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008379 File Offset: 0x00006579
		public ExamRequestClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00008388 File Offset: 0x00006588
		public CreateExamRequestResp CreateExamRequest(CreateExamRequestReq Request)
		{
			return this.WrapServiceMethod<CreateExamRequestResp>(() => this.Proxy.CreateExamRequest(Request));
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000083C0 File Offset: 0x000065C0
		public LoadRequestsByDateRangeResp LoadRequestsByDateRange(LoadRequestsByDateRangeReq Request)
		{
			return this.WrapServiceMethod<LoadRequestsByDateRangeResp>(() => this.Proxy.LoadRequestsByDateRange(Request));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000083F8 File Offset: 0x000065F8
		public void DeleteExamRequest(DeleteExamRequestReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteExamRequest(Request);
			});
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008430 File Offset: 0x00006630
		public LoadRequestsByCourseResp LoadRequestsByCourse(LoadRequestsByCourseReq Request)
		{
			return this.WrapServiceMethod<LoadRequestsByCourseResp>(() => this.Proxy.LoadRequestsByCourse(Request));
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008468 File Offset: 0x00006668
		public LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp>(() => this.Proxy.LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(Request));
		}
	}
}

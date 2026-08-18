using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200002F RID: 47
	internal class ExamRequestClientBaseProxy : ClientBase<IExamRequest>, IExamRequest, IService
	{
		// Token: 0x06000281 RID: 641 RVA: 0x000084A0 File Offset: 0x000066A0
		public ExamRequestClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000084AB File Offset: 0x000066AB
		public ExamRequestClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000084B8 File Offset: 0x000066B8
		public CreateExamRequestResp CreateExamRequest(CreateExamRequestReq Request)
		{
			return base.Channel.CreateExamRequest(Request);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000084D8 File Offset: 0x000066D8
		public LoadRequestsByDateRangeResp LoadRequestsByDateRange(LoadRequestsByDateRangeReq Request)
		{
			return base.Channel.LoadRequestsByDateRange(Request);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x000084F6 File Offset: 0x000066F6
		public void DeleteExamRequest(DeleteExamRequestReq Request)
		{
			base.Channel.DeleteExamRequest(Request);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008508 File Offset: 0x00006708
		public LoadRequestsByCourseResp LoadRequestsByCourse(LoadRequestsByCourseReq Request)
		{
			return base.Channel.LoadRequestsByCourse(Request);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00008528 File Offset: 0x00006728
		public LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsResp LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequestsReq Request)
		{
			return base.Channel.LoadStudentsRegisteredInCourseWithStudentListWhoSubmittedExamRequests(Request);
		}
	}
}

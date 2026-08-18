using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000035 RID: 53
	internal class TestBookingClientBaseProxy : ClientBase<ITestBooking>, ITestBooking, IService
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00008E54 File Offset: 0x00007054
		public TestBookingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00008E5F File Offset: 0x0000705F
		public TestBookingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00008E6C File Offset: 0x0000706C
		public LoadTestAccommodationsResp LoadTestAccommodations(LoadTestAccommodationsReq request)
		{
			return base.Channel.LoadTestAccommodations(request);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00008E8C File Offset: 0x0000708C
		public LoadTestBookingMailMergeInfoByDateResp LoadTestBookingMailMergeInfoByDate(LoadTestBookingMailMergeInfoByDateReq Request)
		{
			return base.Channel.LoadTestBookingMailMergeInfoByDate(Request);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008EAC File Offset: 0x000070AC
		public LoadTestByAppointmentIdResp LoadTestByAppointmentId(LoadTestByAppointmentIdReq request)
		{
			return base.Channel.LoadTestByAppointmentId(request);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00008ECC File Offset: 0x000070CC
		public LoadTestsResp LoadTests(LoadTestsReq request)
		{
			return base.Channel.LoadTests(request);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00008EEA File Offset: 0x000070EA
		public void UpdateTest(UpdateTestReq request)
		{
			base.Channel.UpdateTest(request);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00008EFA File Offset: 0x000070FA
		public void DeleteTest(DeleteTestReq Request)
		{
			base.Channel.DeleteTest(Request);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00008F0C File Offset: 0x0000710C
		public LoadTestsByExamIdResp LoadTestsByExamId(LoadTestsByExamIdReq Request)
		{
			return base.Channel.LoadTestsByExamId(Request);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00008F2C File Offset: 0x0000712C
		public LoadTestsByAppointmentIdsResp LoadTestsByAppointmentIds(LoadTestsByAppointmentIdsReq Request)
		{
			return base.Channel.LoadTestsByAppointmentIds(Request);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00008F4C File Offset: 0x0000714C
		public LoadBasicTestsByAppointmentIdsResp LoadBasicTestsByAppointmentIds(LoadBasicTestsByAppointmentIdsReq Request)
		{
			return base.Channel.LoadBasicTestsByAppointmentIds(Request);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00008F6C File Offset: 0x0000716C
		public LoadAllExamStatusesResp LoadAllExamStatuses(LoadAllExamStatusesReq Request)
		{
			return base.Channel.LoadAllExamStatuses(Request);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00008F8C File Offset: 0x0000718C
		public LoadAccommodationsByTestResp LoadAccommodationsByTest(LoadAccommodationsByTestReq Request)
		{
			return base.Channel.LoadAccommodationsByTest(Request);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00008FAC File Offset: 0x000071AC
		public LoadTestAndAllowedAccommodationsResp LoadTestAndAllowedAccommodations(LoadTestAndAllowedAccommodationsReq Request)
		{
			return base.Channel.LoadTestAndAllowedAccommodations(Request);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00008FCC File Offset: 0x000071CC
		public LoadTestForEditByAppointmentIdResp LoadTestForEditByAppointmentId(LoadTestForEditByAppointmentIdReq Request)
		{
			return base.Channel.LoadTestForEditByAppointmentId(Request);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00008FEC File Offset: 0x000071EC
		public UpdateTestAccommodationsResp UpdateTestAccommodations(UpdateTestAccommodationsReq Request)
		{
			return base.Channel.UpdateTestAccommodations(Request);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000900C File Offset: 0x0000720C
		public CreateTestResp CreateTest(CreateTestReq Request)
		{
			return base.Channel.CreateTest(Request);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000902C File Offset: 0x0000722C
		public LoadTestsByStudentResp LoadTestsByStudent(LoadTestsByStudentReq Request)
		{
			return base.Channel.LoadTestsByStudent(Request);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000904C File Offset: 0x0000724C
		public LoadStudentsWritingExamResp LoadStudentsWritingExam(LoadStudentsWritingExamReq Request)
		{
			return base.Channel.LoadStudentsWritingExam(Request);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000906C File Offset: 0x0000726C
		public LoadInstructorAcknowledgedStudentResp LoadInstructorAcknowledgedStudent(LoadInstructorAcknowledgedStudentReq Request)
		{
			return base.Channel.LoadInstructorAcknowledgedStudent(Request);
		}
	}
}

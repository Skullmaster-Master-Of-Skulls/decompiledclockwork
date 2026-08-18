using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000034 RID: 52
	public class TestBookingReusableClientProxy : WCFTokenBasedReusableClientProxy<ITestBooking>, ITestBooking, IService
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00008A4A File Offset: 0x00006C4A
		public TestBookingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00008A55 File Offset: 0x00006C55
		public TestBookingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00008A64 File Offset: 0x00006C64
		public LoadTestAccommodationsResp LoadTestAccommodations(LoadTestAccommodationsReq request)
		{
			return this.WrapServiceMethod<LoadTestAccommodationsResp>(() => this.Proxy.LoadTestAccommodations(request));
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00008A9C File Offset: 0x00006C9C
		public LoadTestBookingMailMergeInfoByDateResp LoadTestBookingMailMergeInfoByDate(LoadTestBookingMailMergeInfoByDateReq Request)
		{
			return this.WrapServiceMethod<LoadTestBookingMailMergeInfoByDateResp>(() => this.Proxy.LoadTestBookingMailMergeInfoByDate(Request));
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00008AD4 File Offset: 0x00006CD4
		public LoadTestByAppointmentIdResp LoadTestByAppointmentId(LoadTestByAppointmentIdReq request)
		{
			return this.WrapServiceMethod<LoadTestByAppointmentIdResp>(() => this.Proxy.LoadTestByAppointmentId(request));
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00008B0C File Offset: 0x00006D0C
		public LoadTestsResp LoadTests(LoadTestsReq request)
		{
			return this.WrapServiceMethod<LoadTestsResp>(() => this.Proxy.LoadTests(request));
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00008B44 File Offset: 0x00006D44
		public void UpdateTest(UpdateTestReq request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateTest(request);
			});
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00008B7C File Offset: 0x00006D7C
		public void DeleteTest(DeleteTestReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteTest(Request);
			});
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00008BB4 File Offset: 0x00006DB4
		public LoadTestsByExamIdResp LoadTestsByExamId(LoadTestsByExamIdReq Request)
		{
			return this.WrapServiceMethod<LoadTestsByExamIdResp>(() => this.Proxy.LoadTestsByExamId(Request));
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00008BEC File Offset: 0x00006DEC
		public LoadTestsByAppointmentIdsResp LoadTestsByAppointmentIds(LoadTestsByAppointmentIdsReq Request)
		{
			return this.WrapServiceMethod<LoadTestsByAppointmentIdsResp>(() => this.Proxy.LoadTestsByAppointmentIds(Request));
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00008C24 File Offset: 0x00006E24
		public LoadBasicTestsByAppointmentIdsResp LoadBasicTestsByAppointmentIds(LoadBasicTestsByAppointmentIdsReq Request)
		{
			return this.WrapServiceMethod<LoadBasicTestsByAppointmentIdsResp>(() => this.Proxy.LoadBasicTestsByAppointmentIds(Request));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00008C5C File Offset: 0x00006E5C
		public LoadAllExamStatusesResp LoadAllExamStatuses(LoadAllExamStatusesReq Request)
		{
			return this.WrapServiceMethod<LoadAllExamStatusesResp>(() => this.Proxy.LoadAllExamStatuses(Request));
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00008C94 File Offset: 0x00006E94
		public LoadAccommodationsByTestResp LoadAccommodationsByTest(LoadAccommodationsByTestReq Request)
		{
			return this.WrapServiceMethod<LoadAccommodationsByTestResp>(() => this.Proxy.LoadAccommodationsByTest(Request));
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00008CCC File Offset: 0x00006ECC
		public LoadTestAndAllowedAccommodationsResp LoadTestAndAllowedAccommodations(LoadTestAndAllowedAccommodationsReq Request)
		{
			return this.WrapServiceMethod<LoadTestAndAllowedAccommodationsResp>(() => this.Proxy.LoadTestAndAllowedAccommodations(Request));
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00008D04 File Offset: 0x00006F04
		public LoadTestForEditByAppointmentIdResp LoadTestForEditByAppointmentId(LoadTestForEditByAppointmentIdReq Request)
		{
			return this.WrapServiceMethod<LoadTestForEditByAppointmentIdResp>(() => this.Proxy.LoadTestForEditByAppointmentId(Request));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00008D3C File Offset: 0x00006F3C
		public UpdateTestAccommodationsResp UpdateTestAccommodations(UpdateTestAccommodationsReq Request)
		{
			return this.WrapServiceMethod<UpdateTestAccommodationsResp>(() => this.Proxy.UpdateTestAccommodations(Request));
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00008D74 File Offset: 0x00006F74
		public CreateTestResp CreateTest(CreateTestReq Request)
		{
			return this.WrapServiceMethod<CreateTestResp>(() => this.Proxy.CreateTest(Request));
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00008DAC File Offset: 0x00006FAC
		public LoadTestsByStudentResp LoadTestsByStudent(LoadTestsByStudentReq Request)
		{
			return this.WrapServiceMethod<LoadTestsByStudentResp>(() => this.Proxy.LoadTestsByStudent(Request));
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00008DE4 File Offset: 0x00006FE4
		public LoadStudentsWritingExamResp LoadStudentsWritingExam(LoadStudentsWritingExamReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsWritingExamResp>(() => this.Proxy.LoadStudentsWritingExam(Request));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00008E1C File Offset: 0x0000701C
		public LoadInstructorAcknowledgedStudentResp LoadInstructorAcknowledgedStudent(LoadInstructorAcknowledgedStudentReq Request)
		{
			return this.WrapServiceMethod<LoadInstructorAcknowledgedStudentResp>(() => this.Proxy.LoadInstructorAcknowledgedStudent(Request));
		}
	}
}

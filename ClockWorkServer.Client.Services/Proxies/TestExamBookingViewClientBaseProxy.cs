using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000037 RID: 55
	internal class TestExamBookingViewClientBaseProxy : ClientBase<ITestExamBookingView>, ITestExamBookingView, IService
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x000093B4 File Offset: 0x000075B4
		public TestExamBookingViewClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000093BF File Offset: 0x000075BF
		public TestExamBookingViewClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000093CC File Offset: 0x000075CC
		public LoadTestsFullResp LoadTestsFull(LoadTestsFullReq Request)
		{
			return base.Channel.LoadTestsFull(Request);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000093EC File Offset: 0x000075EC
		public LoadTestsSmallResp LoadTestsSmall(LoadTestsSmallReq Request)
		{
			return base.Channel.LoadTestsSmall(Request);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000940C File Offset: 0x0000760C
		public LoadClassTestDefinitionsSmallResp LoadClassTestDefinitionsSmall(LoadClassTestDefinitionsSmallReq Request)
		{
			return base.Channel.LoadClassTestDefinitionsSmall(Request);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000942C File Offset: 0x0000762C
		public LoadUnbookedStudentsSmallResp LoadUnbookedStudentsSmall(LoadUnbookedStudentsSmallReq Request)
		{
			return base.Channel.LoadUnbookedStudentsSmall(Request);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000944C File Offset: 0x0000764C
		public LoadTestFullByAppIdResp LoadTestFullByAppId(LoadTestFullByAppIdReq request)
		{
			return base.Channel.LoadTestFullByAppId(request);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000946C File Offset: 0x0000766C
		public LoadTestSmallByAppIdResp LoadTestSmallByAppId(LoadTestSmallByAppIdReq request)
		{
			return base.Channel.LoadTestSmallByAppId(request);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000948C File Offset: 0x0000768C
		public LoadClassTestDefinitionSmallByExamIdResp LoadClassTestDefinitionSmallByExamId(LoadClassTestDefinitionSmallByExamIdReq request)
		{
			return base.Channel.LoadClassTestDefinitionSmallByExamId(request);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000094AC File Offset: 0x000076AC
		public LoadTestsFullByExamIdResp LoadTestsFullByExamId(LoadTestsFullByExamIdReq Request)
		{
			return base.Channel.LoadTestsFullByExamId(Request);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000094CC File Offset: 0x000076CC
		public LoadTestsSmallByExamIdResp LoadTestsSmallByExamId(LoadTestsSmallByExamIdReq Request)
		{
			return base.Channel.LoadTestsSmallByExamId(Request);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000094EC File Offset: 0x000076EC
		public LoadTestsFullByAppointmentIdsResp LoadTestsFullByAppointmentIds(LoadTestsFullByAppointmentIdsReq Request)
		{
			return base.Channel.LoadTestsFullByAppointmentIds(Request);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000950C File Offset: 0x0000770C
		public LoadTestsSmallByAppointmentIdsResp LoadTestsSmallByAppointmentIds(LoadTestsSmallByAppointmentIdsReq Request)
		{
			return base.Channel.LoadTestsSmallByAppointmentIds(Request);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000952A File Offset: 0x0000772A
		public void SaveTestExamBookingLayoutToCentralizedSetting(SaveTestExamBookingLayoutToCentralizedSettingReq Request)
		{
			base.Channel.SaveTestExamBookingLayoutToCentralizedSetting(Request);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000953A File Offset: 0x0000773A
		public void ClearTestExamBookingLayoutInCentralizedSetting(ClearTestExamBookingLayoutInCentralizedSettingReq Request)
		{
			base.Channel.ClearTestExamBookingLayoutInCentralizedSetting(Request);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000954C File Offset: 0x0000774C
		public LoadUnbookedTestExamStudentsResp LoadUnbookedTestExamStudents(LoadUnbookedTestExamStudentsReq Request)
		{
			return base.Channel.LoadUnbookedTestExamStudents(Request);
		}
	}
}

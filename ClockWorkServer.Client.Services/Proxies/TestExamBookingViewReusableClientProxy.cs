using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000036 RID: 54
	public class TestExamBookingViewReusableClientProxy : WCFTokenBasedReusableClientProxy<ITestExamBookingView>, ITestExamBookingView, IService
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x0000908A File Offset: 0x0000728A
		public TestExamBookingViewReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00009095 File Offset: 0x00007295
		public TestExamBookingViewReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000090A4 File Offset: 0x000072A4
		public LoadTestsFullResp LoadTestsFull(LoadTestsFullReq Request)
		{
			return this.WrapServiceMethod<LoadTestsFullResp>(() => this.Proxy.LoadTestsFull(Request));
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000090DC File Offset: 0x000072DC
		public LoadTestsSmallResp LoadTestsSmall(LoadTestsSmallReq Request)
		{
			return this.WrapServiceMethod<LoadTestsSmallResp>(() => this.Proxy.LoadTestsSmall(Request));
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00009114 File Offset: 0x00007314
		public LoadClassTestDefinitionsSmallResp LoadClassTestDefinitionsSmall(LoadClassTestDefinitionsSmallReq Request)
		{
			return this.WrapServiceMethod<LoadClassTestDefinitionsSmallResp>(() => this.Proxy.LoadClassTestDefinitionsSmall(Request));
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000914C File Offset: 0x0000734C
		public LoadUnbookedStudentsSmallResp LoadUnbookedStudentsSmall(LoadUnbookedStudentsSmallReq Request)
		{
			return this.WrapServiceMethod<LoadUnbookedStudentsSmallResp>(() => this.Proxy.LoadUnbookedStudentsSmall(Request));
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00009184 File Offset: 0x00007384
		public LoadTestFullByAppIdResp LoadTestFullByAppId(LoadTestFullByAppIdReq request)
		{
			return this.WrapServiceMethod<LoadTestFullByAppIdResp>(() => this.Proxy.LoadTestFullByAppId(request));
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000091BC File Offset: 0x000073BC
		public LoadTestSmallByAppIdResp LoadTestSmallByAppId(LoadTestSmallByAppIdReq request)
		{
			return this.WrapServiceMethod<LoadTestSmallByAppIdResp>(() => this.Proxy.LoadTestSmallByAppId(request));
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000091F4 File Offset: 0x000073F4
		public LoadClassTestDefinitionSmallByExamIdResp LoadClassTestDefinitionSmallByExamId(LoadClassTestDefinitionSmallByExamIdReq request)
		{
			return this.WrapServiceMethod<LoadClassTestDefinitionSmallByExamIdResp>(() => this.Proxy.LoadClassTestDefinitionSmallByExamId(request));
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000922C File Offset: 0x0000742C
		public LoadTestsFullByExamIdResp LoadTestsFullByExamId(LoadTestsFullByExamIdReq Request)
		{
			return this.WrapServiceMethod<LoadTestsFullByExamIdResp>(() => this.Proxy.LoadTestsFullByExamId(Request));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00009264 File Offset: 0x00007464
		public LoadTestsSmallByExamIdResp LoadTestsSmallByExamId(LoadTestsSmallByExamIdReq Request)
		{
			return this.WrapServiceMethod<LoadTestsSmallByExamIdResp>(() => this.Proxy.LoadTestsSmallByExamId(Request));
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000929C File Offset: 0x0000749C
		public LoadTestsFullByAppointmentIdsResp LoadTestsFullByAppointmentIds(LoadTestsFullByAppointmentIdsReq Request)
		{
			return this.WrapServiceMethod<LoadTestsFullByAppointmentIdsResp>(() => this.Proxy.LoadTestsFullByAppointmentIds(Request));
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000092D4 File Offset: 0x000074D4
		public LoadTestsSmallByAppointmentIdsResp LoadTestsSmallByAppointmentIds(LoadTestsSmallByAppointmentIdsReq Request)
		{
			return this.WrapServiceMethod<LoadTestsSmallByAppointmentIdsResp>(() => this.Proxy.LoadTestsSmallByAppointmentIds(Request));
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000930C File Offset: 0x0000750C
		public void SaveTestExamBookingLayoutToCentralizedSetting(SaveTestExamBookingLayoutToCentralizedSettingReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.SaveTestExamBookingLayoutToCentralizedSetting(Request);
			});
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00009344 File Offset: 0x00007544
		public void ClearTestExamBookingLayoutInCentralizedSetting(ClearTestExamBookingLayoutInCentralizedSettingReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.ClearTestExamBookingLayoutInCentralizedSetting(Request);
			});
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000937C File Offset: 0x0000757C
		public LoadUnbookedTestExamStudentsResp LoadUnbookedTestExamStudents(LoadUnbookedTestExamStudentsReq Request)
		{
			return this.WrapServiceMethod<LoadUnbookedTestExamStudentsResp>(() => this.Proxy.LoadUnbookedTestExamStudents(Request));
		}
	}
}

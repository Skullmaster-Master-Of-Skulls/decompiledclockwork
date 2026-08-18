using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000114 RID: 276
	public class PerformanceTestReusableClientProxy : WCFTokenBasedReusableClientProxy<IPerformanceTest>, IPerformanceTest, IService
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001B67A File Offset: 0x0001987A
		public PerformanceTestReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001B685 File Offset: 0x00019885
		public PerformanceTestReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0001B694 File Offset: 0x00019894
		public SearchForPersonPerformanceTestResp SearchForPersonPerformanceTest(SearchForPersonPerformanceTestReq Request)
		{
			return this.WrapServiceMethod<SearchForPersonPerformanceTestResp>(() => this.Proxy.SearchForPersonPerformanceTest(Request));
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001B6CC File Offset: 0x000198CC
		public LoadAppointmentsPerformanceTestResp LoadAppointmentsPerformanceTest(LoadAppointmentsPerformanceTestReq Request)
		{
			return this.WrapServiceMethod<LoadAppointmentsPerformanceTestResp>(() => this.Proxy.LoadAppointmentsPerformanceTest(Request));
		}
	}
}

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.PerformanceTesting;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000115 RID: 277
	internal class PerformanceTestClientBaseProxy : ClientBase<IPerformanceTest>, IPerformanceTest, IService
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001B704 File Offset: 0x00019904
		public PerformanceTestClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001B70F File Offset: 0x0001990F
		public PerformanceTestClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001B71C File Offset: 0x0001991C
		public SearchForPersonPerformanceTestResp SearchForPersonPerformanceTest(SearchForPersonPerformanceTestReq Request)
		{
			return base.Channel.SearchForPersonPerformanceTest(Request);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0001B73C File Offset: 0x0001993C
		public LoadAppointmentsPerformanceTestResp LoadAppointmentsPerformanceTest(LoadAppointmentsPerformanceTestReq Request)
		{
			return base.Channel.LoadAppointmentsPerformanceTest(Request);
		}
	}
}

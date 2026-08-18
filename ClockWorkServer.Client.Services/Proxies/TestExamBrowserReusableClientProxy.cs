using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000038 RID: 56
	public class TestExamBrowserReusableClientProxy : WCFTokenBasedReusableClientProxy<ITestExamBrowser>, ITestExamBrowser, IService
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000956A File Offset: 0x0000776A
		public TestExamBrowserReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00009575 File Offset: 0x00007775
		public TestExamBrowserReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00009584 File Offset: 0x00007784
		public LoadTestExamRowResp LoadTestExamRow(LoadTestExamRowReq Request)
		{
			return this.WrapServiceMethod<LoadTestExamRowResp>(() => this.Proxy.LoadTestExamRow(Request));
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000095BC File Offset: 0x000077BC
		public LoadTestExamRowsResp LoadTestExamRows(LoadTestExamRowsReq Request)
		{
			return this.WrapServiceMethod<LoadTestExamRowsResp>(() => this.Proxy.LoadTestExamRows(Request));
		}
	}
}

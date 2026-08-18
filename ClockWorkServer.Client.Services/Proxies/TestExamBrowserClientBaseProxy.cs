using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000039 RID: 57
	internal class TestExamBrowserClientBaseProxy : ClientBase<ITestExamBrowser>, ITestExamBrowser, IService
	{
		// Token: 0x060002FA RID: 762 RVA: 0x000095F4 File Offset: 0x000077F4
		public TestExamBrowserClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000095FF File Offset: 0x000077FF
		public TestExamBrowserClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000960C File Offset: 0x0000780C
		public LoadTestExamRowResp LoadTestExamRow(LoadTestExamRowReq Request)
		{
			return base.Channel.LoadTestExamRow(Request);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000962C File Offset: 0x0000782C
		public LoadTestExamRowsResp LoadTestExamRows(LoadTestExamRowsReq Request)
		{
			return base.Channel.LoadTestExamRows(Request);
		}
	}
}

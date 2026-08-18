using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200003E RID: 62
	public class FinalExamsViewReusableClientProxy : WCFTokenBasedReusableClientProxy<IFinalExamsView>, IFinalExamsView, IService
	{
		// Token: 0x0600031E RID: 798 RVA: 0x00009AAA File Offset: 0x00007CAA
		public FinalExamsViewReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00009AB5 File Offset: 0x00007CB5
		public FinalExamsViewReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00009AC4 File Offset: 0x00007CC4
		public LoadFinalExamsLightResp LoadFinalExamsLight(LoadFinalExamsLightReq Request)
		{
			return this.WrapServiceMethod<LoadFinalExamsLightResp>(() => this.Proxy.LoadFinalExamsLight(Request));
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00009AFC File Offset: 0x00007CFC
		public LoadUnbookedFinalExamsResp LoadUnbookedFinalExams(LoadUnbookedFinalExamsReq Request)
		{
			return this.WrapServiceMethod<LoadUnbookedFinalExamsResp>(() => this.Proxy.LoadUnbookedFinalExams(Request));
		}
	}
}

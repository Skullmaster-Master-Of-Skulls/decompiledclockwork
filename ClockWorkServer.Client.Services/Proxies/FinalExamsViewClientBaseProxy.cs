using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200003F RID: 63
	internal class FinalExamsViewClientBaseProxy : ClientBase<IFinalExamsView>, IFinalExamsView, IService
	{
		// Token: 0x06000322 RID: 802 RVA: 0x00009B34 File Offset: 0x00007D34
		public FinalExamsViewClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00009B3F File Offset: 0x00007D3F
		public FinalExamsViewClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00009B4C File Offset: 0x00007D4C
		public LoadFinalExamsLightResp LoadFinalExamsLight(LoadFinalExamsLightReq Request)
		{
			return base.Channel.LoadFinalExamsLight(Request);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00009B6C File Offset: 0x00007D6C
		public LoadUnbookedFinalExamsResp LoadUnbookedFinalExams(LoadUnbookedFinalExamsReq Request)
		{
			return base.Channel.LoadUnbookedFinalExams(Request);
		}
	}
}

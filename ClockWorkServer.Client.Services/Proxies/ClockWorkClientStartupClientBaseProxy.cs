using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200013C RID: 316
	internal class ClockWorkClientStartupClientBaseProxy : ClientBase<IClockWorkClientStartup>, IClockWorkClientStartup, IService
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		public ClockWorkClientStartupClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0001EB07 File Offset: 0x0001CD07
		public ClockWorkClientStartupClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0001EB14 File Offset: 0x0001CD14
		public GetClockWorkClientStartupResp GetClockWorkClientStartup(GetClockWorkClientStartupReq Request)
		{
			return base.Channel.GetClockWorkClientStartup(Request);
		}
	}
}

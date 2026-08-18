using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200013B RID: 315
	public class ClockWorkClientStartupReusableClientProxy : WCFTokenBasedReusableClientProxy<IClockWorkClientStartup>, IClockWorkClientStartup, IService
	{
		// Token: 0x06000C40 RID: 3136 RVA: 0x0001EAAA File Offset: 0x0001CCAA
		public ClockWorkClientStartupReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0001EAB5 File Offset: 0x0001CCB5
		public ClockWorkClientStartupReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0001EAC4 File Offset: 0x0001CCC4
		public GetClockWorkClientStartupResp GetClockWorkClientStartup(GetClockWorkClientStartupReq Request)
		{
			return this.WrapServiceMethod<GetClockWorkClientStartupResp>(() => this.Proxy.GetClockWorkClientStartup(Request));
		}
	}
}

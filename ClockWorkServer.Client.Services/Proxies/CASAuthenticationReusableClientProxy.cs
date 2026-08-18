using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200004E RID: 78
	public class CASAuthenticationReusableClientProxy : WCFTokenBasedReusableClientProxy<ICASAuthentication>, ICASAuthentication, IService
	{
		// Token: 0x060003D8 RID: 984 RVA: 0x0000B4C1 File Offset: 0x000096C1
		public CASAuthenticationReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000B4CC File Offset: 0x000096CC
		public CASAuthenticationReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public CASAuthenticationParameters.AuthenticateCASResp AuthenticateCAS(CASAuthenticationParameters.AuthenticateCASReq Request)
		{
			return this.WrapServiceMethod<CASAuthenticationParameters.AuthenticateCASResp>(() => this.Proxy.AuthenticateCAS(Request));
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000B510 File Offset: 0x00009710
		public CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsResp AuthenticateCASWithOverrideOptions(CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq Request)
		{
			return this.WrapServiceMethod<CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsResp>(() => this.Proxy.AuthenticateCASWithOverrideOptions(Request));
		}
	}
}

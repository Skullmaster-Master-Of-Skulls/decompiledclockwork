using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x0200004F RID: 79
	internal class CASAuthenticationClientBaseProxy : ClientBase<ICASAuthentication>, ICASAuthentication, IService
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0000B548 File Offset: 0x00009748
		public CASAuthenticationClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000B553 File Offset: 0x00009753
		public CASAuthenticationClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000B560 File Offset: 0x00009760
		public CASAuthenticationParameters.AuthenticateCASResp AuthenticateCAS(CASAuthenticationParameters.AuthenticateCASReq Request)
		{
			return base.Channel.AuthenticateCAS(Request);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000B580 File Offset: 0x00009780
		public CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsResp AuthenticateCASWithOverrideOptions(CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq Request)
		{
			return base.Channel.AuthenticateCASWithOverrideOptions(Request);
		}
	}
}

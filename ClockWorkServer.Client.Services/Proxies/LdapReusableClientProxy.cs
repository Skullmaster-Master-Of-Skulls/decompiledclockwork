using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000A5 RID: 165
	public class LdapReusableClientProxy : WCFTokenBasedReusableClientProxy<ILdap>, ILdap, IService
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x00011DD7 File Offset: 0x0000FFD7
		public LdapReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00011DE2 File Offset: 0x0000FFE2
		public LdapReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00011DF0 File Offset: 0x0000FFF0
		public LdapLoginResp LdapLogin(LdapLoginReq Request)
		{
			return this.WrapServiceMethod<LdapLoginResp>(() => this.Proxy.LdapLogin(Request));
		}
	}
}

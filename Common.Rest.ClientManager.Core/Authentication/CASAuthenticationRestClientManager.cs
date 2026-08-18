using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Authentication
{
	// Token: 0x02000069 RID: 105
	public class CASAuthenticationRestClientManager : BearerTokenRestProxy<ICASAuthenticationClientManager>, ICASAuthenticationClientManager, IWebService
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000BDD9 File Offset: 0x00009FD9
		public CASAuthenticationRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000BDE3 File Offset: 0x00009FE3
		public CASAuthenticationRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000BDF0 File Offset: 0x00009FF0
		public CASAuthenticationResultDTO AuthenticateCASWithOverrideOptions(CASAuthenticationOptionsDTO AuthenticationOptions, string ticket)
		{
			CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq authenticateCASWithOverrideOptionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq>();
			authenticateCASWithOverrideOptionsReq.Ticket = ticket;
			authenticateCASWithOverrideOptionsReq.OverrideOptions = AuthenticationOptions;
			return base.Post<CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq, CASAuthenticationResultDTO>(authenticateCASWithOverrideOptionsReq, "casauthentication/authenticatewithoverrideoptions");
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000BE24 File Offset: 0x0000A024
		public CASAuthenticationResultDTO AuthenticateCAS(string ticket)
		{
			CASAuthenticationParameters.AuthenticateCASReq authenticateCASReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CASAuthenticationParameters.AuthenticateCASReq>();
			authenticateCASReq.Ticket = ticket;
			return base.Post<CASAuthenticationParameters.AuthenticateCASReq, CASAuthenticationResultDTO>(authenticateCASReq, "casauthentication/authenticate");
		}
	}
}

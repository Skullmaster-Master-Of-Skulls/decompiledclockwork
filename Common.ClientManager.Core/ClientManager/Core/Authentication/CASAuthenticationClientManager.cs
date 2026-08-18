using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Authentication;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Authentication
{
	// Token: 0x0200007F RID: 127
	public class CASAuthenticationClientManager : ICASAuthenticationClientManager, IWebService
	{
		// Token: 0x06000494 RID: 1172 RVA: 0x00014EF8 File Offset: 0x000130F8
		public CASAuthenticationResultDTO AuthenticateCASWithOverrideOptions(CASAuthenticationOptionsDTO AuthenticationOptions, string ticket)
		{
			CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq authenticateCASWithOverrideOptionsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq>();
			authenticateCASWithOverrideOptionsReq.Ticket = ticket;
			authenticateCASWithOverrideOptionsReq.OverrideOptions = AuthenticationOptions;
			return ClientServiceFactory.GetClientInstance<ICASAuthentication>().AuthenticateCASWithOverrideOptions(authenticateCASWithOverrideOptionsReq).AuthenticationResult;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00014F38 File Offset: 0x00013138
		public CASAuthenticationResultDTO AuthenticateCAS(string ticket)
		{
			CASAuthenticationParameters.AuthenticateCASReq authenticateCASReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CASAuthenticationParameters.AuthenticateCASReq>();
			authenticateCASReq.Ticket = ticket;
			return ClientServiceFactory.GetClientInstance<ICASAuthentication>().AuthenticateCAS(authenticateCASReq).AuthenticationResult;
		}
	}
}

using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication.ContractParameters;
using TechnoPro.Common.Core.AuthenticationCAS;
using TechnoPro.Common.Core.Mappers.Authentication;
using TechnoPro.Common.ICore.Authentication;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000024 RID: 36
	public class CASAuthenticationServiceManager : ICASAuthentication, IService
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x0000855C File Offset: 0x0000675C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00008570 File Offset: 0x00006770
		public CASAuthenticationParameters.AuthenticateCASResp AuthenticateCAS(CASAuthenticationParameters.AuthenticateCASReq Request)
		{
			ICASAuthManager icasauthManager = new CASAuthManager(Request.GetOperationContext());
			return new CASAuthenticationParameters.AuthenticateCASResp
			{
				AuthenticationResult = icasauthManager.AuthenticateCAS(Request.Ticket).ToDTO()
			};
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000085AC File Offset: 0x000067AC
		public CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsResp AuthenticateCASWithOverrideOptions(CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsReq Request)
		{
			ICASAuthManager icasauthManager = new CASAuthManager(Request.GetOperationContext());
			return new CASAuthenticationParameters.AuthenticateCASWithOverrideOptionsResp
			{
				AuthenticationResult = icasauthManager.AuthenticateCAS(Request.OverrideOptions.ToDomainObject(), Request.Ticket).ToDTO()
			};
		}
	}
}

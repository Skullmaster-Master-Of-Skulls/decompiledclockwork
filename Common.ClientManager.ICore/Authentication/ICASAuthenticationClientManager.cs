using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Authentication
{
	// Token: 0x02000078 RID: 120
	public interface ICASAuthenticationClientManager : IWebService
	{
		// Token: 0x06000378 RID: 888
		CASAuthenticationResultDTO AuthenticateCASWithOverrideOptions(CASAuthenticationOptionsDTO AuthenticationOptions, string ticket);

		// Token: 0x06000379 RID: 889
		CASAuthenticationResultDTO AuthenticateCAS(string ticket);
	}
}

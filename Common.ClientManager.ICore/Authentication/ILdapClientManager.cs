using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Authentication;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Authentication
{
	// Token: 0x0200007A RID: 122
	public interface ILdapClientManager : IWebService
	{
		// Token: 0x06000384 RID: 900
		LdapAuthenticationResultDTO LdapLogin(LdapConnectionInfoDTO ConnectionInfo, string UserName, string Password);
	}
}

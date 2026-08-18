using System;
using TechnoPro.Common.Public.Entities.Authentication.Authentication;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Authentication
{
	// Token: 0x02000017 RID: 23
	public interface ISamlAuthWebClientManager
	{
		// Token: 0x06000052 RID: 82
		PortalGuardAuthenticationContext GetPortalGuardAuthenticationContext();

		// Token: 0x06000053 RID: 83
		string GenerateRequest(PortalGuardAuthenticationContext portalGuardAuthenticationContext, bool encodeAuthRequest);

		// Token: 0x06000054 RID: 84
		string GenerateLogoutRequest(PortalGuardAuthenticationContext portalGuardAuthenticationContext, bool encodeAuthRequest);
	}
}

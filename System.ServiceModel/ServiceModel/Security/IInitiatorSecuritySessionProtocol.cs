using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x02000311 RID: 785
	internal interface IInitiatorSecuritySessionProtocol
	{
		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001B24 RID: 6948
		// (set) Token: 0x06001B25 RID: 6949
		bool ReturnCorrelationState { get; set; }

		// Token: 0x06001B26 RID: 6950
		SecurityToken GetOutgoingSessionToken();

		// Token: 0x06001B27 RID: 6951
		void SetIdentityCheckAuthenticator(SecurityTokenAuthenticator tokenAuthenticator);

		// Token: 0x06001B28 RID: 6952
		void SetOutgoingSessionToken(SecurityToken token);

		// Token: 0x06001B29 RID: 6953
		List<SecurityToken> GetIncomingSessionTokens();

		// Token: 0x06001B2A RID: 6954
		void SetIncomingSessionTokens(List<SecurityToken> tokens);
	}
}

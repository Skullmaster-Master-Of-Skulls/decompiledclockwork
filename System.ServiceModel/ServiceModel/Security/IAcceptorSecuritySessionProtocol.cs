using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000310 RID: 784
	internal interface IAcceptorSecuritySessionProtocol
	{
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001B1F RID: 6943
		// (set) Token: 0x06001B20 RID: 6944
		bool ReturnCorrelationState { get; set; }

		// Token: 0x06001B21 RID: 6945
		SecurityToken GetOutgoingSessionToken();

		// Token: 0x06001B22 RID: 6946
		void SetOutgoingSessionToken(SecurityToken token);

		// Token: 0x06001B23 RID: 6947
		void SetSessionTokenAuthenticator(UniqueId sessionId, SecurityTokenAuthenticator sessionTokenAuthenticator, SecurityTokenResolver sessionTokenResolver);
	}
}

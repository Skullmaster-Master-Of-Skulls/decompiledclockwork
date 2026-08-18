using System;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A6 RID: 422
	public abstract class SecurityTokenManager
	{
		// Token: 0x06000DAF RID: 3503
		public abstract SecurityTokenProvider CreateSecurityTokenProvider(SecurityTokenRequirement tokenRequirement);

		// Token: 0x06000DB0 RID: 3504
		public abstract SecurityTokenSerializer CreateSecurityTokenSerializer(SecurityTokenVersion version);

		// Token: 0x06000DB1 RID: 3505
		public abstract SecurityTokenAuthenticator CreateSecurityTokenAuthenticator(SecurityTokenRequirement tokenRequirement, out SecurityTokenResolver outOfBandTokenResolver);
	}
}

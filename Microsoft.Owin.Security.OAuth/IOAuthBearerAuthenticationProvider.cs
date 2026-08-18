using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000024 RID: 36
	public interface IOAuthBearerAuthenticationProvider
	{
		// Token: 0x06000105 RID: 261
		Task RequestToken(OAuthRequestTokenContext context);

		// Token: 0x06000106 RID: 262
		Task ValidateIdentity(OAuthValidateIdentityContext context);

		// Token: 0x06000107 RID: 263
		Task ApplyChallenge(OAuthChallengeContext context);
	}
}

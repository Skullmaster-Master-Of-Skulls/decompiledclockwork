using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000022 RID: 34
	public interface IOAuthAuthorizationServerProvider
	{
		// Token: 0x060000C2 RID: 194
		Task MatchEndpoint(OAuthMatchEndpointContext context);

		// Token: 0x060000C3 RID: 195
		Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context);

		// Token: 0x060000C4 RID: 196
		Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context);

		// Token: 0x060000C5 RID: 197
		Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context);

		// Token: 0x060000C6 RID: 198
		Task ValidateTokenRequest(OAuthValidateTokenRequestContext context);

		// Token: 0x060000C7 RID: 199
		Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context);

		// Token: 0x060000C8 RID: 200
		Task GrantRefreshToken(OAuthGrantRefreshTokenContext context);

		// Token: 0x060000C9 RID: 201
		Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context);

		// Token: 0x060000CA RID: 202
		Task GrantClientCredentials(OAuthGrantClientCredentialsContext context);

		// Token: 0x060000CB RID: 203
		Task GrantCustomExtension(OAuthGrantCustomExtensionContext context);

		// Token: 0x060000CC RID: 204
		Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context);

		// Token: 0x060000CD RID: 205
		Task TokenEndpoint(OAuthTokenEndpointContext context);

		// Token: 0x060000CE RID: 206
		Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context);

		// Token: 0x060000CF RID: 207
		Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context);
	}
}

using System;
using System.Threading.Tasks;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000023 RID: 35
	public class OAuthAuthorizationServerProvider : IOAuthAuthorizationServerProvider
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00006CDC File Offset: 0x00004EDC
		public OAuthAuthorizationServerProvider()
		{
			this.OnMatchEndpoint = ((OAuthMatchEndpointContext context) => Task.FromResult<object>(null));
			this.OnValidateClientRedirectUri = ((OAuthValidateClientRedirectUriContext context) => Task.FromResult<object>(null));
			this.OnValidateClientAuthentication = ((OAuthValidateClientAuthenticationContext context) => Task.FromResult<object>(null));
			this.OnValidateAuthorizeRequest = DefaultBehavior.ValidateAuthorizeRequest;
			this.OnValidateTokenRequest = DefaultBehavior.ValidateTokenRequest;
			this.OnGrantAuthorizationCode = DefaultBehavior.GrantAuthorizationCode;
			this.OnGrantResourceOwnerCredentials = ((OAuthGrantResourceOwnerCredentialsContext context) => Task.FromResult<object>(null));
			this.OnGrantRefreshToken = DefaultBehavior.GrantRefreshToken;
			this.OnGrantClientCredentials = ((OAuthGrantClientCredentialsContext context) => Task.FromResult<object>(null));
			this.OnGrantCustomExtension = ((OAuthGrantCustomExtensionContext context) => Task.FromResult<object>(null));
			this.OnAuthorizeEndpoint = ((OAuthAuthorizeEndpointContext context) => Task.FromResult<object>(null));
			this.OnTokenEndpoint = ((OAuthTokenEndpointContext context) => Task.FromResult<object>(null));
			this.OnAuthorizationEndpointResponse = ((OAuthAuthorizationEndpointResponseContext context) => Task.FromResult<object>(null));
			this.OnTokenEndpointResponse = ((OAuthTokenEndpointResponseContext context) => Task.FromResult<object>(null));
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00006E79 File Offset: 0x00005079
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00006E81 File Offset: 0x00005081
		public Func<OAuthMatchEndpointContext, Task> OnMatchEndpoint { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006E8A File Offset: 0x0000508A
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00006E92 File Offset: 0x00005092
		public Func<OAuthValidateClientRedirectUriContext, Task> OnValidateClientRedirectUri { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00006E9B File Offset: 0x0000509B
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00006EA3 File Offset: 0x000050A3
		public Func<OAuthValidateClientAuthenticationContext, Task> OnValidateClientAuthentication { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00006EAC File Offset: 0x000050AC
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00006EB4 File Offset: 0x000050B4
		public Func<OAuthValidateAuthorizeRequestContext, Task> OnValidateAuthorizeRequest { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00006EBD File Offset: 0x000050BD
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00006EC5 File Offset: 0x000050C5
		public Func<OAuthValidateTokenRequestContext, Task> OnValidateTokenRequest { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00006ECE File Offset: 0x000050CE
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00006ED6 File Offset: 0x000050D6
		public Func<OAuthGrantAuthorizationCodeContext, Task> OnGrantAuthorizationCode { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00006EDF File Offset: 0x000050DF
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00006EE7 File Offset: 0x000050E7
		public Func<OAuthGrantResourceOwnerCredentialsContext, Task> OnGrantResourceOwnerCredentials { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00006EF0 File Offset: 0x000050F0
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00006EF8 File Offset: 0x000050F8
		public Func<OAuthGrantClientCredentialsContext, Task> OnGrantClientCredentials { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00006F01 File Offset: 0x00005101
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00006F09 File Offset: 0x00005109
		public Func<OAuthGrantRefreshTokenContext, Task> OnGrantRefreshToken { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006F12 File Offset: 0x00005112
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00006F1A File Offset: 0x0000511A
		public Func<OAuthGrantCustomExtensionContext, Task> OnGrantCustomExtension { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00006F23 File Offset: 0x00005123
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00006F2B File Offset: 0x0000512B
		public Func<OAuthAuthorizeEndpointContext, Task> OnAuthorizeEndpoint { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00006F34 File Offset: 0x00005134
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00006F3C File Offset: 0x0000513C
		public Func<OAuthTokenEndpointContext, Task> OnTokenEndpoint { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00006F45 File Offset: 0x00005145
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00006F4D File Offset: 0x0000514D
		public Func<OAuthAuthorizationEndpointResponseContext, Task> OnAuthorizationEndpointResponse { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00006F56 File Offset: 0x00005156
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00006F5E File Offset: 0x0000515E
		public Func<OAuthTokenEndpointResponseContext, Task> OnTokenEndpointResponse { get; set; }

		// Token: 0x060000ED RID: 237 RVA: 0x00006F67 File Offset: 0x00005167
		public virtual Task MatchEndpoint(OAuthMatchEndpointContext context)
		{
			return this.OnMatchEndpoint(context);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006F75 File Offset: 0x00005175
		public virtual Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
		{
			return this.OnValidateClientRedirectUri(context);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006F83 File Offset: 0x00005183
		public virtual Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			return this.OnValidateClientAuthentication(context);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006F91 File Offset: 0x00005191
		public virtual Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
		{
			return this.OnValidateAuthorizeRequest(context);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006F9F File Offset: 0x0000519F
		public virtual Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
		{
			return this.OnValidateTokenRequest(context);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006FAD File Offset: 0x000051AD
		public virtual Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
		{
			return this.OnGrantAuthorizationCode(context);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006FBB File Offset: 0x000051BB
		public virtual Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
		{
			return this.OnGrantRefreshToken(context);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006FC9 File Offset: 0x000051C9
		public virtual Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			return this.OnGrantResourceOwnerCredentials(context);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006FD7 File Offset: 0x000051D7
		public virtual Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
		{
			return this.OnGrantClientCredentials(context);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006FE5 File Offset: 0x000051E5
		public virtual Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
		{
			return this.OnGrantCustomExtension(context);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006FF3 File Offset: 0x000051F3
		public virtual Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
		{
			return this.OnAuthorizeEndpoint(context);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00007001 File Offset: 0x00005201
		public virtual Task TokenEndpoint(OAuthTokenEndpointContext context)
		{
			return this.OnTokenEndpoint(context);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000700F File Offset: 0x0000520F
		public virtual Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context)
		{
			return this.OnAuthorizationEndpointResponse(context);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000701D File Offset: 0x0000521D
		public virtual Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
		{
			return this.OnTokenEndpointResponse(context);
		}
	}
}

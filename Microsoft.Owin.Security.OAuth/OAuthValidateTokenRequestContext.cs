using System;
using Microsoft.Owin.Security.OAuth.Messages;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000033 RID: 51
	public class OAuthValidateTokenRequestContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000159 RID: 345 RVA: 0x000075E0 File Offset: 0x000057E0
		public OAuthValidateTokenRequestContext(IOwinContext context, OAuthAuthorizationServerOptions options, TokenEndpointRequest tokenRequest, BaseValidatingClientContext clientContext) : base(context, options)
		{
			this.TokenRequest = tokenRequest;
			this.ClientContext = clientContext;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000075F9 File Offset: 0x000057F9
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00007601 File Offset: 0x00005801
		public TokenEndpointRequest TokenRequest { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000760A File Offset: 0x0000580A
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00007612 File Offset: 0x00005812
		public BaseValidatingClientContext ClientContext { get; private set; }
	}
}

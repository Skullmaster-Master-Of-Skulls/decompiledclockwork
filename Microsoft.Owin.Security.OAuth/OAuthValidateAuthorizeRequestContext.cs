using System;
using Microsoft.Owin.Security.OAuth.Messages;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200002F RID: 47
	public class OAuthValidateAuthorizeRequestContext : BaseValidatingContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000145 RID: 325 RVA: 0x000074B6 File Offset: 0x000056B6
		public OAuthValidateAuthorizeRequestContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthorizeEndpointRequest authorizeRequest, OAuthValidateClientRedirectUriContext clientContext) : base(context, options)
		{
			this.AuthorizeRequest = authorizeRequest;
			this.ClientContext = clientContext;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000074CF File Offset: 0x000056CF
		// (set) Token: 0x06000147 RID: 327 RVA: 0x000074D7 File Offset: 0x000056D7
		public AuthorizeEndpointRequest AuthorizeRequest { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000074E0 File Offset: 0x000056E0
		// (set) Token: 0x06000149 RID: 329 RVA: 0x000074E8 File Offset: 0x000056E8
		public OAuthValidateClientRedirectUriContext ClientContext { get; private set; }
	}
}

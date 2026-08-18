using System;
using Microsoft.Owin.Security.OAuth.Messages;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000021 RID: 33
	public class OAuthAuthorizeEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00006C69 File Offset: 0x00004E69
		public OAuthAuthorizeEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthorizeEndpointRequest authorizeRequest) : base(context, options)
		{
			this.AuthorizeRequest = authorizeRequest;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006C7A File Offset: 0x00004E7A
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00006C82 File Offset: 0x00004E82
		public AuthorizeEndpointRequest AuthorizeRequest { get; private set; }
	}
}

using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200002B RID: 43
	public class OAuthMatchEndpointContext : EndpointContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000125 RID: 293 RVA: 0x000072F8 File Offset: 0x000054F8
		public OAuthMatchEndpointContext(IOwinContext context, OAuthAuthorizationServerOptions options) : base(context, options)
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00007302 File Offset: 0x00005502
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000730A File Offset: 0x0000550A
		public bool IsAuthorizeEndpoint { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00007313 File Offset: 0x00005513
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000731B File Offset: 0x0000551B
		public bool IsTokenEndpoint { get; private set; }

		// Token: 0x0600012A RID: 298 RVA: 0x00007324 File Offset: 0x00005524
		public void MatchesAuthorizeEndpoint()
		{
			this.IsAuthorizeEndpoint = true;
			this.IsTokenEndpoint = false;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00007334 File Offset: 0x00005534
		public void MatchesTokenEndpoint()
		{
			this.IsAuthorizeEndpoint = false;
			this.IsTokenEndpoint = true;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00007344 File Offset: 0x00005544
		public void MatchesNothing()
		{
			this.IsAuthorizeEndpoint = false;
			this.IsTokenEndpoint = false;
		}
	}
}

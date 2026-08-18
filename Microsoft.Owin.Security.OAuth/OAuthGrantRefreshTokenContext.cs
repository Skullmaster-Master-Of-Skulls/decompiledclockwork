using System;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000028 RID: 40
	public class OAuthGrantRefreshTokenContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00007181 File Offset: 0x00005381
		public OAuthGrantRefreshTokenContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket, string clientId) : base(context, options, ticket)
		{
			this.ClientId = clientId;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00007194 File Offset: 0x00005394
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000719C File Offset: 0x0000539C
		public string ClientId { get; private set; }
	}
}

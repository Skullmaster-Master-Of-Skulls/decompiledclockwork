using System;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000027 RID: 39
	public class OAuthGrantAuthorizationCodeContext : BaseValidatingTicketContext<OAuthAuthorizationServerOptions>
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00007176 File Offset: 0x00005376
		public OAuthGrantAuthorizationCodeContext(IOwinContext context, OAuthAuthorizationServerOptions options, AuthenticationTicket ticket) : base(context, options, ticket)
		{
		}
	}
}

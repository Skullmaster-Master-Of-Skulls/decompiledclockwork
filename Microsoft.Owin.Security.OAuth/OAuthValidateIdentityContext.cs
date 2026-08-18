using System;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x02000031 RID: 49
	public class OAuthValidateIdentityContext : BaseValidatingTicketContext<OAuthBearerAuthenticationOptions>
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00007567 File Offset: 0x00005767
		public OAuthValidateIdentityContext(IOwinContext context, OAuthBearerAuthenticationOptions options, AuthenticationTicket ticket) : base(context, options, ticket)
		{
		}
	}
}

using System;
using System.Security.Claims;

namespace Microsoft.Owin.Security.OAuth
{
	// Token: 0x0200001E RID: 30
	public abstract class BaseValidatingTicketContext<TOptions> : BaseValidatingContext<TOptions>
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00006A15 File Offset: 0x00004C15
		protected BaseValidatingTicketContext(IOwinContext context, TOptions options, AuthenticationTicket ticket) : base(context, options)
		{
			this.Ticket = ticket;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00006A26 File Offset: 0x00004C26
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00006A2E File Offset: 0x00004C2E
		public AuthenticationTicket Ticket { get; private set; }

		// Token: 0x060000AB RID: 171 RVA: 0x00006A37 File Offset: 0x00004C37
		public bool Validated(AuthenticationTicket ticket)
		{
			this.Ticket = ticket;
			return this.Validated();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006A48 File Offset: 0x00004C48
		public bool Validated(ClaimsIdentity identity)
		{
			AuthenticationProperties properties = (this.Ticket != null) ? this.Ticket.Properties : new AuthenticationProperties();
			return this.Validated(new AuthenticationTicket(identity, properties));
		}
	}
}

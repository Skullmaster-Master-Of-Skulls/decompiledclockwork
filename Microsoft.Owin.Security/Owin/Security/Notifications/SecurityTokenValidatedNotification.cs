using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x0200002A RID: 42
	public class SecurityTokenValidatedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x060000AD RID: 173 RVA: 0x00004319 File Offset: 0x00002519
		public SecurityTokenValidatedNotification(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004323 File Offset: 0x00002523
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000432B File Offset: 0x0000252B
		public AuthenticationTicket AuthenticationTicket { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004334 File Offset: 0x00002534
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000433C File Offset: 0x0000253C
		public TMessage ProtocolMessage { get; set; }
	}
}

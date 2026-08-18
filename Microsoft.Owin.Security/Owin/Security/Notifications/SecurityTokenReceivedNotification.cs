using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000029 RID: 41
	public class SecurityTokenReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x060000AA RID: 170 RVA: 0x000042FE File Offset: 0x000024FE
		public SecurityTokenReceivedNotification(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004308 File Offset: 0x00002508
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004310 File Offset: 0x00002510
		public TMessage ProtocolMessage { get; set; }
	}
}

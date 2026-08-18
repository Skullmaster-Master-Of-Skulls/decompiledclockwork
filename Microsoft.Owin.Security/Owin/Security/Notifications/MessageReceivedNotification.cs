using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000026 RID: 38
	public class MessageReceivedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x000042A3 File Offset: 0x000024A3
		public MessageReceivedNotification(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000042AD File Offset: 0x000024AD
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000042B5 File Offset: 0x000024B5
		public TMessage ProtocolMessage { get; set; }
	}
}

using System;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000025 RID: 37
	public class AuthenticationFailedNotification<TMessage, TOptions> : BaseNotification<TOptions>
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004277 File Offset: 0x00002477
		public AuthenticationFailedNotification(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004281 File Offset: 0x00002481
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004289 File Offset: 0x00002489
		public TMessage ProtocolMessage { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00004292 File Offset: 0x00002492
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000429A File Offset: 0x0000249A
		public Exception Exception { get; set; }
	}
}

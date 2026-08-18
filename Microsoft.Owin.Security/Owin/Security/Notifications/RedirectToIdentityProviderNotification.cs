using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000028 RID: 40
	public class RedirectToIdentityProviderNotification<TMessage, TOptions> : BaseContext<TOptions>
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x000042BE File Offset: 0x000024BE
		public RedirectToIdentityProviderNotification(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000042C8 File Offset: 0x000024C8
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000042D0 File Offset: 0x000024D0
		public TMessage ProtocolMessage { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000042D9 File Offset: 0x000024D9
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000042E1 File Offset: 0x000024E1
		public NotificationResultState State { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000042EA File Offset: 0x000024EA
		public bool HandledResponse
		{
			get
			{
				return this.State == NotificationResultState.HandledResponse;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000042F5 File Offset: 0x000024F5
		public void HandleResponse()
		{
			this.State = NotificationResultState.HandledResponse;
		}
	}
}

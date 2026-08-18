using System;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Notifications
{
	// Token: 0x02000024 RID: 36
	public class BaseNotification<TOptions> : BaseContext<TOptions>
	{
		// Token: 0x06000094 RID: 148 RVA: 0x00004234 File Offset: 0x00002434
		protected BaseNotification(IOwinContext context, TOptions options) : base(context, options)
		{
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000423E File Offset: 0x0000243E
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004246 File Offset: 0x00002446
		public NotificationResultState State { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000424F File Offset: 0x0000244F
		public bool HandledResponse
		{
			get
			{
				return this.State == NotificationResultState.HandledResponse;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000425A File Offset: 0x0000245A
		public bool Skipped
		{
			get
			{
				return this.State == NotificationResultState.Skipped;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004265 File Offset: 0x00002465
		public void HandleResponse()
		{
			this.State = NotificationResultState.HandledResponse;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000426E File Offset: 0x0000246E
		public void SkipToNextMiddleware()
		{
			this.State = NotificationResultState.Skipped;
		}
	}
}

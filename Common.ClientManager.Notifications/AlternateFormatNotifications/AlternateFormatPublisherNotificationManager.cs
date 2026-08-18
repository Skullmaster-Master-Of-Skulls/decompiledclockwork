using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TechnoPro.Common.ClientManager.Notifications.AlternateFormatNotifications
{
	// Token: 0x02000024 RID: 36
	public class AlternateFormatPublisherNotificationManager
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00003D9A File Offset: 0x00001F9A
		public static AlternateFormatPublisherNotificationManager Current
		{
			get
			{
				if (AlternateFormatPublisherNotificationManager._currentInstance == null)
				{
					AlternateFormatPublisherNotificationManager._currentInstance = new AlternateFormatPublisherNotificationManager();
				}
				return AlternateFormatPublisherNotificationManager._currentInstance;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000028FC File Offset: 0x00000AFC
		protected AlternateFormatPublisherNotificationManager()
		{
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000105 RID: 261 RVA: 0x00003DB4 File Offset: 0x00001FB4
		// (remove) Token: 0x06000106 RID: 262 RVA: 0x00003DEC File Offset: 0x00001FEC
		public event EventHandler<EventArgs> OnAlternateFormatPublisherListChanged;

		// Token: 0x06000107 RID: 263 RVA: 0x00003E24 File Offset: 0x00002024
		internal void FireOnAlternateFormatPublishersChanged()
		{
			EventHandler<EventArgs> onAlternateFormatPublisherListChanged = this.OnAlternateFormatPublisherListChanged;
			if (onAlternateFormatPublisherListChanged != null)
			{
				onAlternateFormatPublisherListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00003E48 File Offset: 0x00002048
		public Task NotifyOnAlternateFormatPublisherListChangedAsync()
		{
			AlternateFormatPublisherNotificationManager.<NotifyOnAlternateFormatPublisherListChangedAsync>d__8 <NotifyOnAlternateFormatPublisherListChangedAsync>d__;
			<NotifyOnAlternateFormatPublisherListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnAlternateFormatPublisherListChangedAsync>d__.<>4__this = this;
			<NotifyOnAlternateFormatPublisherListChangedAsync>d__.<>1__state = -1;
			<NotifyOnAlternateFormatPublisherListChangedAsync>d__.<>t__builder.Start<AlternateFormatPublisherNotificationManager.<NotifyOnAlternateFormatPublisherListChangedAsync>d__8>(ref <NotifyOnAlternateFormatPublisherListChangedAsync>d__);
			return <NotifyOnAlternateFormatPublisherListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0400006F RID: 111
		private static AlternateFormatPublisherNotificationManager _currentInstance;
	}
}

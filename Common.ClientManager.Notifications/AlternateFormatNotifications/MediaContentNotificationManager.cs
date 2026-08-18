using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TechnoPro.Common.ClientManager.Notifications.AlternateFormatNotifications
{
	// Token: 0x02000025 RID: 37
	public class MediaContentNotificationManager
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00003E8B File Offset: 0x0000208B
		public static MediaContentNotificationManager Current
		{
			get
			{
				if (MediaContentNotificationManager._currentInstance == null)
				{
					MediaContentNotificationManager._currentInstance = new MediaContentNotificationManager();
				}
				return MediaContentNotificationManager._currentInstance;
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000028FC File Offset: 0x00000AFC
		protected MediaContentNotificationManager()
		{
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600010B RID: 267 RVA: 0x00003EA4 File Offset: 0x000020A4
		// (remove) Token: 0x0600010C RID: 268 RVA: 0x00003EDC File Offset: 0x000020DC
		public event EventHandler<EventArgs> OnMediaContentListChanged;

		// Token: 0x0600010D RID: 269 RVA: 0x00003F14 File Offset: 0x00002114
		internal void FireOnMediaContentChanged()
		{
			EventHandler<EventArgs> onMediaContentListChanged = this.OnMediaContentListChanged;
			if (onMediaContentListChanged != null)
			{
				onMediaContentListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00003F38 File Offset: 0x00002138
		public Task NotifyOnMediaContentListChangedAsync()
		{
			MediaContentNotificationManager.<NotifyOnMediaContentListChangedAsync>d__8 <NotifyOnMediaContentListChangedAsync>d__;
			<NotifyOnMediaContentListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnMediaContentListChangedAsync>d__.<>4__this = this;
			<NotifyOnMediaContentListChangedAsync>d__.<>1__state = -1;
			<NotifyOnMediaContentListChangedAsync>d__.<>t__builder.Start<MediaContentNotificationManager.<NotifyOnMediaContentListChangedAsync>d__8>(ref <NotifyOnMediaContentListChangedAsync>d__);
			return <NotifyOnMediaContentListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000071 RID: 113
		private static MediaContentNotificationManager _currentInstance;
	}
}

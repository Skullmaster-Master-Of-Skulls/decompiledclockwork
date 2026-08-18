using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TechnoPro.Common.ClientManager.Notifications.AlternateFormatNotifications
{
	// Token: 0x02000026 RID: 38
	public class MediaJobsNotificationManager
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00003F7B File Offset: 0x0000217B
		public static MediaJobsNotificationManager Current
		{
			get
			{
				if (MediaJobsNotificationManager._currentInstance == null)
				{
					MediaJobsNotificationManager._currentInstance = new MediaJobsNotificationManager();
				}
				return MediaJobsNotificationManager._currentInstance;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000028FC File Offset: 0x00000AFC
		protected MediaJobsNotificationManager()
		{
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000111 RID: 273 RVA: 0x00003F94 File Offset: 0x00002194
		// (remove) Token: 0x06000112 RID: 274 RVA: 0x00003FCC File Offset: 0x000021CC
		public event EventHandler<EventArgs> OnInProgressMediaJobListChanged;

		// Token: 0x06000113 RID: 275 RVA: 0x00004004 File Offset: 0x00002204
		internal void FireOnInProgressMediaJobListChanged()
		{
			EventHandler<EventArgs> onInProgressMediaJobListChanged = this.OnInProgressMediaJobListChanged;
			if (onInProgressMediaJobListChanged != null)
			{
				onInProgressMediaJobListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004028 File Offset: 0x00002228
		public Task NotifyOnInProgressMediaJobListChangedAsync()
		{
			MediaJobsNotificationManager.<NotifyOnInProgressMediaJobListChangedAsync>d__8 <NotifyOnInProgressMediaJobListChangedAsync>d__;
			<NotifyOnInProgressMediaJobListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnInProgressMediaJobListChangedAsync>d__.<>4__this = this;
			<NotifyOnInProgressMediaJobListChangedAsync>d__.<>1__state = -1;
			<NotifyOnInProgressMediaJobListChangedAsync>d__.<>t__builder.Start<MediaJobsNotificationManager.<NotifyOnInProgressMediaJobListChangedAsync>d__8>(ref <NotifyOnInProgressMediaJobListChangedAsync>d__);
			return <NotifyOnInProgressMediaJobListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000115 RID: 277 RVA: 0x0000406C File Offset: 0x0000226C
		// (remove) Token: 0x06000116 RID: 278 RVA: 0x000040A4 File Offset: 0x000022A4
		public event EventHandler<EventArgs> OnCompletedMediaJobListChanged;

		// Token: 0x06000117 RID: 279 RVA: 0x000040DC File Offset: 0x000022DC
		internal void FireOnCompletedMediaJobListChanged()
		{
			EventHandler<EventArgs> onCompletedMediaJobListChanged = this.OnCompletedMediaJobListChanged;
			if (onCompletedMediaJobListChanged != null)
			{
				onCompletedMediaJobListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004100 File Offset: 0x00002300
		public Task NotifyOnCompletedMediaJobListChangedAsync()
		{
			MediaJobsNotificationManager.<NotifyOnCompletedMediaJobListChangedAsync>d__13 <NotifyOnCompletedMediaJobListChangedAsync>d__;
			<NotifyOnCompletedMediaJobListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnCompletedMediaJobListChangedAsync>d__.<>4__this = this;
			<NotifyOnCompletedMediaJobListChangedAsync>d__.<>1__state = -1;
			<NotifyOnCompletedMediaJobListChangedAsync>d__.<>t__builder.Start<MediaJobsNotificationManager.<NotifyOnCompletedMediaJobListChangedAsync>d__13>(ref <NotifyOnCompletedMediaJobListChangedAsync>d__);
			return <NotifyOnCompletedMediaJobListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06000119 RID: 281 RVA: 0x00004144 File Offset: 0x00002344
		// (remove) Token: 0x0600011A RID: 282 RVA: 0x0000417C File Offset: 0x0000237C
		public event EventHandler<EventArgs> OnCancelledMediaJobListChanged;

		// Token: 0x0600011B RID: 283 RVA: 0x000041B4 File Offset: 0x000023B4
		internal void FireOnCancelledMediaJobListChanged()
		{
			EventHandler<EventArgs> onCancelledMediaJobListChanged = this.OnCancelledMediaJobListChanged;
			if (onCancelledMediaJobListChanged != null)
			{
				onCancelledMediaJobListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000041D8 File Offset: 0x000023D8
		public Task NotifyOnCancelledMediaJobListChangedAsync()
		{
			MediaJobsNotificationManager.<NotifyOnCancelledMediaJobListChangedAsync>d__18 <NotifyOnCancelledMediaJobListChangedAsync>d__;
			<NotifyOnCancelledMediaJobListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnCancelledMediaJobListChangedAsync>d__.<>4__this = this;
			<NotifyOnCancelledMediaJobListChangedAsync>d__.<>1__state = -1;
			<NotifyOnCancelledMediaJobListChangedAsync>d__.<>t__builder.Start<MediaJobsNotificationManager.<NotifyOnCancelledMediaJobListChangedAsync>d__18>(ref <NotifyOnCancelledMediaJobListChangedAsync>d__);
			return <NotifyOnCancelledMediaJobListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000073 RID: 115
		private static MediaJobsNotificationManager _currentInstance;
	}
}

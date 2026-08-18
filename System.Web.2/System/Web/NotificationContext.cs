using System;

namespace System.Web
{
	// Token: 0x020000DE RID: 222
	internal class NotificationContext
	{
		// Token: 0x06000E2C RID: 3628 RVA: 0x000284AF File Offset: 0x000266AF
		internal NotificationContext(int flags, bool isReEntry)
		{
			this.CurrentNotificationFlags = flags;
			this.IsReEntry = isReEntry;
		}

		// Token: 0x0400054A RID: 1354
		internal bool IsPostNotification;

		// Token: 0x0400054B RID: 1355
		internal RequestNotification CurrentNotification;

		// Token: 0x0400054C RID: 1356
		internal int CurrentModuleIndex;

		// Token: 0x0400054D RID: 1357
		internal int CurrentModuleEventIndex;

		// Token: 0x0400054E RID: 1358
		internal int CurrentNotificationFlags;

		// Token: 0x0400054F RID: 1359
		internal HttpAsyncResult AsyncResult;

		// Token: 0x04000550 RID: 1360
		internal bool PendingAsyncCompletion;

		// Token: 0x04000551 RID: 1361
		internal Exception Error;

		// Token: 0x04000552 RID: 1362
		internal bool RequestCompleted;

		// Token: 0x04000553 RID: 1363
		internal bool IsReEntry;
	}
}

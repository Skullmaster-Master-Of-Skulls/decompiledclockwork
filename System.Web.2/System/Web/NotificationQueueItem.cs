using System;

namespace System.Web
{
	// Token: 0x02000073 RID: 115
	internal sealed class NotificationQueueItem
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x0000AD6C File Offset: 0x00008F6C
		internal NotificationQueueItem(FileChangeEventHandler callback, FileAction action, string filename)
		{
			this.Callback = callback;
			this.Action = action;
			this.Filename = filename;
		}

		// Token: 0x04000213 RID: 531
		internal readonly FileChangeEventHandler Callback;

		// Token: 0x04000214 RID: 532
		internal readonly string Filename;

		// Token: 0x04000215 RID: 533
		internal readonly FileAction Action;
	}
}

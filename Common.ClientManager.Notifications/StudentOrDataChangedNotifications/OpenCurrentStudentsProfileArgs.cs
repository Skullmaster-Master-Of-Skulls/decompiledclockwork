using System;

namespace TechnoPro.Common.ClientManager.Notifications.StudentOrDataChangedNotifications
{
	// Token: 0x02000009 RID: 9
	public class OpenCurrentStudentsProfileArgs : EventArgs
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002F2F File Offset: 0x0000112F
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002F37 File Offset: 0x00001137
		public int ScreenNum { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002F40 File Offset: 0x00001140
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002F48 File Offset: 0x00001148
		public string Title { get; set; }
	}
}

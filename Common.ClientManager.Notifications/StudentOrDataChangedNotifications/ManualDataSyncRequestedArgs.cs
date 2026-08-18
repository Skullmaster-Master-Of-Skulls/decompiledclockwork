using System;

namespace TechnoPro.Common.ClientManager.Notifications.StudentOrDataChangedNotifications
{
	// Token: 0x02000008 RID: 8
	public class ManualDataSyncRequestedArgs : EventArgs
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002EFC File Offset: 0x000010FC
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00002F04 File Offset: 0x00001104
		public string StudentNumber { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002F0D File Offset: 0x0000110D
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00002F15 File Offset: 0x00001115
		public bool SyncCourses { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002F1E File Offset: 0x0000111E
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00002F26 File Offset: 0x00001126
		public bool SyncData { get; set; }
	}
}

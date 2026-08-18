using System;

namespace TechnoPro.Common.ClientManager.Notifications.StudentOrDataChangedNotifications
{
	// Token: 0x02000006 RID: 6
	public class CurrentStudentEventArgs : EventArgs
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002EB8 File Offset: 0x000010B8
		// (set) Token: 0x06000053 RID: 83 RVA: 0x00002EC0 File Offset: 0x000010C0
		public int PersonId { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00002EC9 File Offset: 0x000010C9
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002ED1 File Offset: 0x000010D1
		public bool RegisterStudentChangeEventWithSystem { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002EDA File Offset: 0x000010DA
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002EE2 File Offset: 0x000010E2
		public bool RememberLastSelectedPid { get; set; }
	}
}

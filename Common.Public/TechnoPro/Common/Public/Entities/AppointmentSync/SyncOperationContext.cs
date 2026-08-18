using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004D6 RID: 1238
	public class SyncOperationContext : OperationContext
	{
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x00028173 File Offset: 0x00026373
		// (set) Token: 0x0600254F RID: 9551 RVA: 0x0002817B File Offset: 0x0002637B
		public SyncApplicationSettings SyncSettings { get; set; }

		// Token: 0x17000F78 RID: 3960
		// (get) Token: 0x06002550 RID: 9552 RVA: 0x00028184 File Offset: 0x00026384
		// (set) Token: 0x06002551 RID: 9553 RVA: 0x0002818C File Offset: 0x0002638C
		public eApplicationSyncProviderName AppSyncProviderName { get; set; }

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x00028195 File Offset: 0x00026395
		// (set) Token: 0x06002553 RID: 9555 RVA: 0x0002819D File Offset: 0x0002639D
		public virtual string CalendarUsername { get; set; }
	}
}

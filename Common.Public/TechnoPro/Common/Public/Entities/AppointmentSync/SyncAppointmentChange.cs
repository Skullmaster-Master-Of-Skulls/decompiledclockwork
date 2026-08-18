using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004D5 RID: 1237
	public class SyncAppointmentChange
	{
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x00028140 File Offset: 0x00026340
		// (set) Token: 0x06002548 RID: 9544 RVA: 0x00028148 File Offset: 0x00026348
		public eSyncAppointmentChangeType ChangeType { get; set; }

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x00028151 File Offset: 0x00026351
		// (set) Token: 0x0600254A RID: 9546 RVA: 0x00028159 File Offset: 0x00026359
		public string AppointmentIdStr { get; set; }

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x00028162 File Offset: 0x00026362
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x0002816A File Offset: 0x0002636A
		public int AppointmentId { get; set; }
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004D9 RID: 1241
	public class ClockWorkExternalApplicationSyncAction
	{
		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06002564 RID: 9572 RVA: 0x0002821D File Offset: 0x0002641D
		// (set) Token: 0x06002565 RID: 9573 RVA: 0x00028225 File Offset: 0x00026425
		public eClockWorkExternalApplicationSyncActionType SyncAction { get; set; }

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0002822E File Offset: 0x0002642E
		// (set) Token: 0x06002567 RID: 9575 RVA: 0x00028236 File Offset: 0x00026436
		public ExternalAppointment ExternalAppointment { get; set; }

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x0002823F File Offset: 0x0002643F
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x00028247 File Offset: 0x00026447
		public ClockWorkSyncAppointment ClockWorkAppointment { get; set; }
	}
}

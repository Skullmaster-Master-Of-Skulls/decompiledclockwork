using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync.FastSync
{
	// Token: 0x020004E7 RID: 1255
	public class ClockWorkSyncAppointmentChangeRequest
	{
		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x000289D5 File Offset: 0x00026BD5
		// (set) Token: 0x06002602 RID: 9730 RVA: 0x000289DD File Offset: 0x00026BDD
		public int ClockWorkPersonId { get; set; }

		// Token: 0x17000FCC RID: 4044
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x000289E6 File Offset: 0x00026BE6
		// (set) Token: 0x06002604 RID: 9732 RVA: 0x000289EE File Offset: 0x00026BEE
		public DateTime? ClockWorkSyncState { get; set; }
	}
}

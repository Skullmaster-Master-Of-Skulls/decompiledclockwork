using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsRecurring
{
	// Token: 0x0200054F RID: 1359
	public class RecurringInstanceSetModifyBehaviour
	{
		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x06002BDE RID: 11230 RVA: 0x00030EFF File Offset: 0x0002F0FF
		// (set) Token: 0x06002BDF RID: 11231 RVA: 0x00030F07 File Offset: 0x0002F107
		public eRecurringInstanceSetPropertyModifyBehaviour PrivateChangeBehaviour { get; set; }

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x00030F10 File Offset: 0x0002F110
		// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x00030F18 File Offset: 0x0002F118
		public eRecurringInstanceSetPropertyModifyBehaviour LockedChangeBehaviour { get; set; }

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x00030F21 File Offset: 0x0002F121
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x00030F29 File Offset: 0x0002F129
		public eRecurringInstanceSetPropertyModifyBehaviour AttendeesChangeBehaviour { get; set; }
	}
}

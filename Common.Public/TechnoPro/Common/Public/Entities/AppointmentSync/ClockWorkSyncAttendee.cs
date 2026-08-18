using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004DD RID: 1245
	public class ClockWorkSyncAttendee : BusinessBase<int>
	{
		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x00028411 File Offset: 0x00026611
		// (set) Token: 0x0600258E RID: 9614 RVA: 0x00028419 File Offset: 0x00026619
		public ClockWorkSyncPersonBase Attendee { get; set; }

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x00028422 File Offset: 0x00026622
		// (set) Token: 0x06002590 RID: 9616 RVA: 0x0002842A File Offset: 0x0002662A
		public bool IsNoShow { get; set; }

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x00028434 File Offset: 0x00026634
		// (set) Token: 0x06002592 RID: 9618 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AttendeeId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x0002844C File Offset: 0x0002664C
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x00028454 File Offset: 0x00026654
		public int MiscCode { get; set; }
	}
}

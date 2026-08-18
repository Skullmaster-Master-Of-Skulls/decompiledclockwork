using System;

namespace TechnoPro.Common.Public.Entities.Veteran
{
	// Token: 0x0200010F RID: 271
	public class ChangeInBenefitRequest : BusinessBase<int, int>
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000F374 File Offset: 0x0000D574
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0000E258 File Offset: 0x0000C458
		public int PersonId
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

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000F38C File Offset: 0x0000D58C
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public int AppointmentId
		{
			get
			{
				return this.SecondId;
			}
			set
			{
				this.SecondId = value;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000F3AF File Offset: 0x0000D5AF
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0000F3B7 File Offset: 0x0000D5B7
		public eVeteranRequestStatus Status { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		public DateTime DateEntered { get; set; }
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x02000484 RID: 1156
	public class AvailabilityScheduleContext : BusinessBase<int, int>
	{
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060022D5 RID: 8917 RVA: 0x000269D4 File Offset: 0x00024BD4
		// (set) Token: 0x060022D6 RID: 8918 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x000269EC File Offset: 0x00024BEC
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x0000F3A4 File Offset: 0x0000D5A4
		public virtual int AvailabilityGroupId
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
	}
}

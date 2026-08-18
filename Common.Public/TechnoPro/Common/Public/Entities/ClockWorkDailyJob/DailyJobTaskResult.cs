using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkDailyJob
{
	// Token: 0x0200045F RID: 1119
	public class DailyJobTaskResult : BusinessBase<int>
	{
		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x00026198 File Offset: 0x00024398
		// (set) Token: 0x06002216 RID: 8726 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int WindowsTaskJobResultId
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

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x000261B0 File Offset: 0x000243B0
		// (set) Token: 0x06002218 RID: 8728 RVA: 0x000261B8 File Offset: 0x000243B8
		public int WindowsTaskJobId { get; set; }

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x000261C1 File Offset: 0x000243C1
		// (set) Token: 0x0600221A RID: 8730 RVA: 0x000261C9 File Offset: 0x000243C9
		public int ReportId { get; set; }

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x000261D2 File Offset: 0x000243D2
		// (set) Token: 0x0600221C RID: 8732 RVA: 0x000261DA File Offset: 0x000243DA
		public int TaskGroupId { get; set; }

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000261E3 File Offset: 0x000243E3
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x000261EB File Offset: 0x000243EB
		public DateTime RunStartDate { get; set; }

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600221F RID: 8735 RVA: 0x000261F4 File Offset: 0x000243F4
		// (set) Token: 0x06002220 RID: 8736 RVA: 0x000261FC File Offset: 0x000243FC
		public DateTime RunEndDate { get; set; }

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x00026205 File Offset: 0x00024405
		// (set) Token: 0x06002222 RID: 8738 RVA: 0x0002620D File Offset: 0x0002440D
		public bool Successful { get; set; }

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x00026216 File Offset: 0x00024416
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x0002621E File Offset: 0x0002441E
		public string RunResult { get; set; }
	}
}

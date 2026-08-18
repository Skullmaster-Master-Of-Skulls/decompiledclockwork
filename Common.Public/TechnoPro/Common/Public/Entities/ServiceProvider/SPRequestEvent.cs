using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001EF RID: 495
	public class SPRequestEvent : BusinessBase<int>
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0001662C File Offset: 0x0001482C
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRequestEventId
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

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00016644 File Offset: 0x00014844
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0001664C File Offset: 0x0001484C
		public DateTime StartDateTime { get; set; }

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00016655 File Offset: 0x00014855
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0001665D File Offset: 0x0001485D
		public DateTime EndDateTime { get; set; }

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00016666 File Offset: 0x00014866
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0001666E File Offset: 0x0001486E
		public SPRequestStatusType RequestStatus { get; set; }

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00016677 File Offset: 0x00014877
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0001667F File Offset: 0x0001487F
		public SPRequestAssignmentStatusType AssignmentStatus { get; set; }

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00016688 File Offset: 0x00014888
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x00016690 File Offset: 0x00014890
		public SPUrgencyLevelType UrgencyLevel { get; set; }

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00016699 File Offset: 0x00014899
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x000166A1 File Offset: 0x000148A1
		public string Notes { get; set; }

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x000166AA File Offset: 0x000148AA
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x000166B2 File Offset: 0x000148B2
		public SPRequestEventAssignment Assignment { get; set; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x000166BB File Offset: 0x000148BB
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x000166C3 File Offset: 0x000148C3
		public bool IsRequired { get; set; }
	}
}

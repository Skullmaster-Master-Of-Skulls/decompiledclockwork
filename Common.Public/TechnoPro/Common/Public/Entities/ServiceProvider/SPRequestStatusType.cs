using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001F1 RID: 497
	public class SPRequestStatusType : BusinessBase<int>
	{
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00016728 File Offset: 0x00014928
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRequestStatusTypeId
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

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00016740 File Offset: 0x00014940
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x00016748 File Offset: 0x00014948
		public string Title { get; set; }

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x00016751 File Offset: 0x00014951
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x00016759 File Offset: 0x00014959
		public string Description { get; set; }

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x00016762 File Offset: 0x00014962
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x0001676A File Offset: 0x0001496A
		public bool AssignmentIsRequired { get; set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00016773 File Offset: 0x00014973
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x0001677B File Offset: 0x0001497B
		public SPUrgencyLevelType UrgencyLevel { get; set; }
	}
}

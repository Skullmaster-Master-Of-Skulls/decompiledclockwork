using System;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001EC RID: 492
	public class SPRequestAssignmentStatusType : BusinessBase<int>
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000164D0 File Offset: 0x000146D0
		// (set) Token: 0x06000E5B RID: 3675 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRequestAssignmentStatusTypeId
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

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x000164E8 File Offset: 0x000146E8
		// (set) Token: 0x06000E5D RID: 3677 RVA: 0x000164F0 File Offset: 0x000146F0
		public string Title { get; set; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x000164F9 File Offset: 0x000146F9
		// (set) Token: 0x06000E5F RID: 3679 RVA: 0x00016501 File Offset: 0x00014701
		public string Description { get; set; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0001650A File Offset: 0x0001470A
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x00016512 File Offset: 0x00014712
		public bool AssignmentIsCompleted { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0001651B File Offset: 0x0001471B
		// (set) Token: 0x06000E63 RID: 3683 RVA: 0x00016523 File Offset: 0x00014723
		public SPUrgencyLevelType UrgencyLevel { get; set; }
	}
}

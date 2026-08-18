using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001ED RID: 493
	public class SPRequestCourse : BusinessBase<int>
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0001652C File Offset: 0x0001472C
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRequestCourseId
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

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x00016544 File Offset: 0x00014744
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0001654C File Offset: 0x0001474C
		public LookupCourseBase Course { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x00016555 File Offset: 0x00014755
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0001655D File Offset: 0x0001475D
		public SPRequestStatusType RequestStatus { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x00016566 File Offset: 0x00014766
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0001656E File Offset: 0x0001476E
		public SPRequestAssignmentStatusType AssignmentStatus { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x00016577 File Offset: 0x00014777
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0001657F File Offset: 0x0001477F
		public SPUrgencyLevelType UrgencyLevel { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x00016588 File Offset: 0x00014788
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x00016590 File Offset: 0x00014790
		public SPRequestCourseAssignment Assignment { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00016599 File Offset: 0x00014799
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x000165A1 File Offset: 0x000147A1
		public string Notes { get; set; }

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x000165AA File Offset: 0x000147AA
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x000165B2 File Offset: 0x000147B2
		public bool IsRequired { get; set; }
	}
}

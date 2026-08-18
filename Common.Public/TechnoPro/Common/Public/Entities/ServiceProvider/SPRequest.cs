using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.ServiceProvider
{
	// Token: 0x020001EB RID: 491
	public class SPRequest : BusinessBase<int>
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0001640C File Offset: 0x0001460C
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int SPRequestId
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

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00016424 File Offset: 0x00014624
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x0001642C File Offset: 0x0001462C
		public SPProviderType ProviderType { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00016435 File Offset: 0x00014635
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x0001643D File Offset: 0x0001463D
		public PersonBase Student { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00016446 File Offset: 0x00014646
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0001644E File Offset: 0x0001464E
		public DateTime DateEntered { get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00016457 File Offset: 0x00014657
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0001645F File Offset: 0x0001465F
		public PersonBase WhoEntered { get; set; }

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x00016468 File Offset: 0x00014668
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x00016470 File Offset: 0x00014670
		public string Notes { get; set; }

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x00016479 File Offset: 0x00014679
		// (set) Token: 0x06000E50 RID: 3664 RVA: 0x00016481 File Offset: 0x00014681
		public string SpecialInstructions { get; set; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0001648A File Offset: 0x0001468A
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x00016492 File Offset: 0x00014692
		public SPRequestStatusType RequestStatus { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0001649B File Offset: 0x0001469B
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x000164A3 File Offset: 0x000146A3
		public SPRequestAssignmentStatusType AssignmentStatus { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000164AC File Offset: 0x000146AC
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x000164B4 File Offset: 0x000146B4
		public SPUrgencyLevelType UrgencyLevel { get; set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000164BD File Offset: 0x000146BD
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x000164C5 File Offset: 0x000146C5
		public bool IsActive { get; set; }
	}
}

using System;
using TechnoPro.Common.Public.Entities.Academic;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x0200010D RID: 269
	public class VetsStudentCardInfoItem : BusinessBase<Guid>
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000F228 File Offset: 0x0000D428
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid VetsBenefitApplicationId
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

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000F240 File Offset: 0x0000D440
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0000F248 File Offset: 0x0000D448
		public Guid? ChapterId { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000F251 File Offset: 0x0000D451
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0000F259 File Offset: 0x0000D459
		public string ChapterTitle { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000F262 File Offset: 0x0000D462
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000F26A File Offset: 0x0000D46A
		public Semester Semester { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000F273 File Offset: 0x0000D473
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0000F27B File Offset: 0x0000D47B
		public DateTime DateCreated { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000F284 File Offset: 0x0000D484
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000F28C File Offset: 0x0000D48C
		public DateTime? DateLastModified { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000F295 File Offset: 0x0000D495
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0000F29D File Offset: 0x0000D49D
		public bool StudentAgreeCompleted { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000F2A6 File Offset: 0x0000D4A6
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0000F2AE File Offset: 0x0000D4AE
		public bool BenAppCompleted { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000F2B7 File Offset: 0x0000D4B7
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000F2BF File Offset: 0x0000D4BF
		public bool RegistrationCompleted { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public eVetsBenefitApplicationStep? PreferredStep { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000F2D9 File Offset: 0x0000D4D9
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0000F2E1 File Offset: 0x0000D4E1
		public eVetsRequestStatus FinalStatus { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0000F2EA File Offset: 0x0000D4EA
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0000F2F2 File Offset: 0x0000D4F2
		public Guid CurrentProgressStepId { get; set; }
	}
}

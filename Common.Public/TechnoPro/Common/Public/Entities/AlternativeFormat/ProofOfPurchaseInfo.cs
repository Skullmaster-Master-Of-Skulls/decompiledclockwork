using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000580 RID: 1408
	public class ProofOfPurchaseInfo : BusinessBase<int>
	{
		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x00032300 File Offset: 0x00030500
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x0000E258 File Offset: 0x0000C458
		public int ProofOfPurchaseId
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

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x00032318 File Offset: 0x00030518
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x00032320 File Offset: 0x00030520
		public byte[] ProofOfPurchaseReceipt { get; set; }

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x00032329 File Offset: 0x00030529
		// (set) Token: 0x06002D69 RID: 11625 RVA: 0x00032331 File Offset: 0x00030531
		public string Notes { get; set; }

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0003233A File Offset: 0x0003053A
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x00032342 File Offset: 0x00030542
		public PersonBase WhoAcceptedProofOfPurchase { get; set; }

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x0003234B File Offset: 0x0003054B
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x00032353 File Offset: 0x00030553
		public DateTime? WhenWasAccepted { get; set; }

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x0003235C File Offset: 0x0003055C
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x00032364 File Offset: 0x00030564
		public Guid MediaContentUniqueId { get; set; }

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x0003236D File Offset: 0x0003056D
		// (set) Token: 0x06002D71 RID: 11633 RVA: 0x00032375 File Offset: 0x00030575
		public int StudentPersonId { get; set; }

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x0003237E File Offset: 0x0003057E
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x00032386 File Offset: 0x00030586
		public string Filename { get; set; }

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x0003238F File Offset: 0x0003058F
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x00032397 File Offset: 0x00030597
		public string Extension { get; set; }
	}
}

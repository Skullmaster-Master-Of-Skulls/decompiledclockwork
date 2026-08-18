using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x0200057F RID: 1407
	public class MediaContentRequestedInfo : BusinessBase<int>
	{
		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x06002D3F RID: 11583 RVA: 0x000321CC File Offset: 0x000303CC
		// (set) Token: 0x06002D40 RID: 11584 RVA: 0x000321D4 File Offset: 0x000303D4
		public int MediaContentRequestedInfoID { get; set; }

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x06002D41 RID: 11585 RVA: 0x000321DD File Offset: 0x000303DD
		// (set) Token: 0x06002D42 RID: 11586 RVA: 0x000321E5 File Offset: 0x000303E5
		public ProofOfPurchaseInfo ProofOfPurchase { get; set; }

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x000321EE File Offset: 0x000303EE
		// (set) Token: 0x06002D44 RID: 11588 RVA: 0x000321F6 File Offset: 0x000303F6
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x06002D45 RID: 11589 RVA: 0x000321FF File Offset: 0x000303FF
		// (set) Token: 0x06002D46 RID: 11590 RVA: 0x00032207 File Offset: 0x00030407
		public MediaRequestStatus RequestStatus { get; set; }

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x06002D47 RID: 11591 RVA: 0x00032210 File Offset: 0x00030410
		// (set) Token: 0x06002D48 RID: 11592 RVA: 0x00032218 File Offset: 0x00030418
		public bool IsApproved { get; set; }

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x00032221 File Offset: 0x00030421
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x00032229 File Offset: 0x00030429
		public bool IsCompleted { get; set; }

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x06002D4B RID: 11595 RVA: 0x00032232 File Offset: 0x00030432
		// (set) Token: 0x06002D4C RID: 11596 RVA: 0x0003223A File Offset: 0x0003043A
		public bool IsCancelled { get; set; }

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x06002D4D RID: 11597 RVA: 0x00032243 File Offset: 0x00030443
		// (set) Token: 0x06002D4E RID: 11598 RVA: 0x0003224B File Offset: 0x0003044B
		public DateTime? AvailableStartTime { get; set; }

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x00032254 File Offset: 0x00030454
		// (set) Token: 0x06002D50 RID: 11600 RVA: 0x0003225C File Offset: 0x0003045C
		public DateTime? AvailableEndTime { get; set; }

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x00032265 File Offset: 0x00030465
		// (set) Token: 0x06002D52 RID: 11602 RVA: 0x0003226D File Offset: 0x0003046D
		public MediaContentDetail ContentDetailRequested { get; set; }

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x06002D53 RID: 11603 RVA: 0x00032276 File Offset: 0x00030476
		// (set) Token: 0x06002D54 RID: 11604 RVA: 0x0003227E File Offset: 0x0003047E
		public int MediaJobId { get; set; }

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x06002D55 RID: 11605 RVA: 0x00032287 File Offset: 0x00030487
		// (set) Token: 0x06002D56 RID: 11606 RVA: 0x0003228F File Offset: 0x0003048F
		public string MediaJobTitle { get; set; }

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x00032298 File Offset: 0x00030498
		// (set) Token: 0x06002D58 RID: 11608 RVA: 0x000322A0 File Offset: 0x000304A0
		public int StudentRequestId { get; set; }

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x000322A9 File Offset: 0x000304A9
		// (set) Token: 0x06002D5A RID: 11610 RVA: 0x000322B1 File Offset: 0x000304B1
		public SchoolCampus Campus { get; set; }

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000322BA File Offset: 0x000304BA
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x000322C2 File Offset: 0x000304C2
		public PersonBase RequestMadeFromStudent { get; set; }

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000322CB File Offset: 0x000304CB
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x000322D3 File Offset: 0x000304D3
		public DateTime CreatedDatetime { get; set; }

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000322DC File Offset: 0x000304DC
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x000322E4 File Offset: 0x000304E4
		public DateTime? CompletedDateTime { get; set; }

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000322ED File Offset: 0x000304ED
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x000322F5 File Offset: 0x000304F5
		public string CompletionNotes { get; set; }
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000576 RID: 1398
	public class StudentMediaContentFileWithProofOfPurchaseInfo : MediaContentFileWithoutData
	{
		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x00032039 File Offset: 0x00030239
		// (set) Token: 0x06002D13 RID: 11539 RVA: 0x00032041 File Offset: 0x00030241
		public int StudentPersonId { get; set; }

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x0003204A File Offset: 0x0003024A
		// (set) Token: 0x06002D15 RID: 11541 RVA: 0x00032052 File Offset: 0x00030252
		public eStudentMediaContentFileStatus FileStatus { get; set; }

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x0003205B File Offset: 0x0003025B
		// (set) Token: 0x06002D17 RID: 11543 RVA: 0x00032063 File Offset: 0x00030263
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x0003206C File Offset: 0x0003026C
		// (set) Token: 0x06002D19 RID: 11545 RVA: 0x00032074 File Offset: 0x00030274
		public string StudentCompletionRequestNotes { get; set; }
	}
}

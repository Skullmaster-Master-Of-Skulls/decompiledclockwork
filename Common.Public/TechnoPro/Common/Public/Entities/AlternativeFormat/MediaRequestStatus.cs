using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000588 RID: 1416
	[Serializable]
	public enum MediaRequestStatus
	{
		// Token: 0x0400200A RID: 8202
		Created,
		// Token: 0x0400200B RID: 8203
		In_Progress,
		// Token: 0x0400200C RID: 8204
		Completed_but_Pending_of_Proof_of_Purchase,
		// Token: 0x0400200D RID: 8205
		Completed_but_Pending_of_Proof_of_Purchase_Acceptance,
		// Token: 0x0400200E RID: 8206
		Cancelled_by_Student,
		// Token: 0x0400200F RID: 8207
		Rejected_by_Staff,
		// Token: 0x04002010 RID: 8208
		Ready_To_Pick_Up_or_Download,
		// Token: 0x04002011 RID: 8209
		Ready_To_Download,
		// Token: 0x04002012 RID: 8210
		Delivered
	}
}

using System;

namespace System.Data
{
	// Token: 0x020000BF RID: 191
	[Flags]
	public enum DataRowAction
	{
		// Token: 0x0400035B RID: 859
		Nothing = 0,
		// Token: 0x0400035C RID: 860
		Delete = 1,
		// Token: 0x0400035D RID: 861
		Change = 2,
		// Token: 0x0400035E RID: 862
		Rollback = 4,
		// Token: 0x0400035F RID: 863
		Commit = 8,
		// Token: 0x04000360 RID: 864
		Add = 16,
		// Token: 0x04000361 RID: 865
		ChangeOriginal = 32,
		// Token: 0x04000362 RID: 866
		ChangeCurrentAndOriginal = 64
	}
}

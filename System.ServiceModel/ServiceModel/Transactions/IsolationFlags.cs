using System;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001AD RID: 429
	[Flags]
	internal enum IsolationFlags
	{
		// Token: 0x0400172F RID: 5935
		RetainCommitDC = 1,
		// Token: 0x04001730 RID: 5936
		RetainCommit = 2,
		// Token: 0x04001731 RID: 5937
		RetainCommitNo = 3,
		// Token: 0x04001732 RID: 5938
		RetainAbortDC = 4,
		// Token: 0x04001733 RID: 5939
		RetainAbort = 8,
		// Token: 0x04001734 RID: 5940
		RetainAbortNo = 12,
		// Token: 0x04001735 RID: 5941
		RetainDoNotCare = 5,
		// Token: 0x04001736 RID: 5942
		RetainBoth = 10,
		// Token: 0x04001737 RID: 5943
		RetainNone = 15,
		// Token: 0x04001738 RID: 5944
		Optimistic = 16,
		// Token: 0x04001739 RID: 5945
		ReadOnly = 32
	}
}

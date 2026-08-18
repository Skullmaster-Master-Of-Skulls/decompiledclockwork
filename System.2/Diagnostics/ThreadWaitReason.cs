using System;

namespace System.Diagnostics
{
	// Token: 0x0200050A RID: 1290
	public enum ThreadWaitReason
	{
		// Token: 0x040028FA RID: 10490
		Executive,
		// Token: 0x040028FB RID: 10491
		FreePage,
		// Token: 0x040028FC RID: 10492
		PageIn,
		// Token: 0x040028FD RID: 10493
		SystemAllocation,
		// Token: 0x040028FE RID: 10494
		ExecutionDelay,
		// Token: 0x040028FF RID: 10495
		Suspended,
		// Token: 0x04002900 RID: 10496
		UserRequest,
		// Token: 0x04002901 RID: 10497
		EventPairHigh,
		// Token: 0x04002902 RID: 10498
		EventPairLow,
		// Token: 0x04002903 RID: 10499
		LpcReceive,
		// Token: 0x04002904 RID: 10500
		LpcReply,
		// Token: 0x04002905 RID: 10501
		VirtualMemory,
		// Token: 0x04002906 RID: 10502
		PageOut,
		// Token: 0x04002907 RID: 10503
		Unknown
	}
}

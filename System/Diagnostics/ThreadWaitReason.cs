using System;

namespace System.Diagnostics
{
	// Token: 0x0200079D RID: 1949
	public enum ThreadWaitReason
	{
		// Token: 0x040034C4 RID: 13508
		Executive,
		// Token: 0x040034C5 RID: 13509
		FreePage,
		// Token: 0x040034C6 RID: 13510
		PageIn,
		// Token: 0x040034C7 RID: 13511
		SystemAllocation,
		// Token: 0x040034C8 RID: 13512
		ExecutionDelay,
		// Token: 0x040034C9 RID: 13513
		Suspended,
		// Token: 0x040034CA RID: 13514
		UserRequest,
		// Token: 0x040034CB RID: 13515
		EventPairHigh,
		// Token: 0x040034CC RID: 13516
		EventPairLow,
		// Token: 0x040034CD RID: 13517
		LpcReceive,
		// Token: 0x040034CE RID: 13518
		LpcReply,
		// Token: 0x040034CF RID: 13519
		VirtualMemory,
		// Token: 0x040034D0 RID: 13520
		PageOut,
		// Token: 0x040034D1 RID: 13521
		Unknown
	}
}

using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004D1 RID: 1233
	internal enum OpcodeFlags
	{
		// Token: 0x04002596 RID: 9622
		None,
		// Token: 0x04002597 RID: 9623
		Single,
		// Token: 0x04002598 RID: 9624
		Multiple,
		// Token: 0x04002599 RID: 9625
		Branch = 4,
		// Token: 0x0400259A RID: 9626
		Result = 8,
		// Token: 0x0400259B RID: 9627
		Jump = 16,
		// Token: 0x0400259C RID: 9628
		Literal = 32,
		// Token: 0x0400259D RID: 9629
		Select = 64,
		// Token: 0x0400259E RID: 9630
		Deleted = 128,
		// Token: 0x0400259F RID: 9631
		InConditional = 256,
		// Token: 0x040025A0 RID: 9632
		NoContextCopy = 512,
		// Token: 0x040025A1 RID: 9633
		InitialSelect = 1024,
		// Token: 0x040025A2 RID: 9634
		CompressableSelect = 2048,
		// Token: 0x040025A3 RID: 9635
		Fx = 4096
	}
}

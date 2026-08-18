using System;

namespace System.Diagnostics
{
	// Token: 0x020004FE RID: 1278
	public enum ProcessPriorityClass
	{
		// Token: 0x04002899 RID: 10393
		Normal = 32,
		// Token: 0x0400289A RID: 10394
		Idle = 64,
		// Token: 0x0400289B RID: 10395
		High = 128,
		// Token: 0x0400289C RID: 10396
		RealTime = 256,
		// Token: 0x0400289D RID: 10397
		BelowNormal = 16384,
		// Token: 0x0400289E RID: 10398
		AboveNormal = 32768
	}
}

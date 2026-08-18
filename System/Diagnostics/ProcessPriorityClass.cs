using System;

namespace System.Diagnostics
{
	// Token: 0x02000789 RID: 1929
	public enum ProcessPriorityClass
	{
		// Token: 0x0400343B RID: 13371
		Normal = 32,
		// Token: 0x0400343C RID: 13372
		Idle = 64,
		// Token: 0x0400343D RID: 13373
		High = 128,
		// Token: 0x0400343E RID: 13374
		RealTime = 256,
		// Token: 0x0400343F RID: 13375
		BelowNormal = 16384,
		// Token: 0x04003440 RID: 13376
		AboveNormal = 32768
	}
}

using System;

namespace System.Diagnostics
{
	// Token: 0x0200079B RID: 1947
	public enum ThreadPriorityLevel
	{
		// Token: 0x040034B3 RID: 13491
		Idle = -15,
		// Token: 0x040034B4 RID: 13492
		Lowest = -2,
		// Token: 0x040034B5 RID: 13493
		BelowNormal,
		// Token: 0x040034B6 RID: 13494
		Normal,
		// Token: 0x040034B7 RID: 13495
		AboveNormal,
		// Token: 0x040034B8 RID: 13496
		Highest,
		// Token: 0x040034B9 RID: 13497
		TimeCritical = 15
	}
}

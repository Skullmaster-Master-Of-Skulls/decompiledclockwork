using System;

namespace System.Diagnostics
{
	// Token: 0x02000509 RID: 1289
	public enum ThreadState
	{
		// Token: 0x040028F1 RID: 10481
		Initialized,
		// Token: 0x040028F2 RID: 10482
		Ready,
		// Token: 0x040028F3 RID: 10483
		Running,
		// Token: 0x040028F4 RID: 10484
		Standby,
		// Token: 0x040028F5 RID: 10485
		Terminated,
		// Token: 0x040028F6 RID: 10486
		Wait,
		// Token: 0x040028F7 RID: 10487
		Transition,
		// Token: 0x040028F8 RID: 10488
		Unknown
	}
}

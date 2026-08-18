using System;

namespace System.Diagnostics
{
	// Token: 0x0200079C RID: 1948
	public enum ThreadState
	{
		// Token: 0x040034BB RID: 13499
		Initialized,
		// Token: 0x040034BC RID: 13500
		Ready,
		// Token: 0x040034BD RID: 13501
		Running,
		// Token: 0x040034BE RID: 13502
		Standby,
		// Token: 0x040034BF RID: 13503
		Terminated,
		// Token: 0x040034C0 RID: 13504
		Wait,
		// Token: 0x040034C1 RID: 13505
		Transition,
		// Token: 0x040034C2 RID: 13506
		Unknown
	}
}

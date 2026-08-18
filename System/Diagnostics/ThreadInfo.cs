using System;

namespace System.Diagnostics
{
	// Token: 0x02000779 RID: 1913
	internal class ThreadInfo
	{
		// Token: 0x040033D7 RID: 13271
		public int threadId;

		// Token: 0x040033D8 RID: 13272
		public int processId;

		// Token: 0x040033D9 RID: 13273
		public int basePriority;

		// Token: 0x040033DA RID: 13274
		public int currentPriority;

		// Token: 0x040033DB RID: 13275
		public IntPtr startAddress;

		// Token: 0x040033DC RID: 13276
		public ThreadState threadState;

		// Token: 0x040033DD RID: 13277
		public ThreadWaitReason threadWaitReason;
	}
}

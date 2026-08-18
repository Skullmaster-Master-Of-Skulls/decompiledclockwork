using System;

namespace System.Diagnostics
{
	// Token: 0x020004F1 RID: 1265
	internal class ThreadInfo
	{
		// Token: 0x04002878 RID: 10360
		public int threadId;

		// Token: 0x04002879 RID: 10361
		public int processId;

		// Token: 0x0400287A RID: 10362
		public int basePriority;

		// Token: 0x0400287B RID: 10363
		public int currentPriority;

		// Token: 0x0400287C RID: 10364
		public IntPtr startAddress;

		// Token: 0x0400287D RID: 10365
		public ThreadState threadState;

		// Token: 0x0400287E RID: 10366
		public ThreadWaitReason threadWaitReason;
	}
}

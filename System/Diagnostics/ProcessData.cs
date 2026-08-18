using System;

namespace System.Diagnostics
{
	// Token: 0x02000798 RID: 1944
	internal class ProcessData
	{
		// Token: 0x06003C09 RID: 15369 RVA: 0x00100A74 File Offset: 0x000FFA74
		public ProcessData(int pid, long startTime)
		{
			this.ProcessId = pid;
			this.StartupTime = startTime;
		}

		// Token: 0x040034A2 RID: 13474
		public int ProcessId;

		// Token: 0x040034A3 RID: 13475
		public long StartupTime;
	}
}

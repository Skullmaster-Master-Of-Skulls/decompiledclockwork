using System;

namespace System.Diagnostics
{
	// Token: 0x02000505 RID: 1285
	internal class ProcessData
	{
		// Token: 0x060030EE RID: 12526 RVA: 0x000DE48F File Offset: 0x000DC68F
		public ProcessData(int pid, long startTime)
		{
			this.ProcessId = pid;
			this.StartupTime = startTime;
		}

		// Token: 0x040028D8 RID: 10456
		public int ProcessId;

		// Token: 0x040028D9 RID: 10457
		public long StartupTime;
	}
}

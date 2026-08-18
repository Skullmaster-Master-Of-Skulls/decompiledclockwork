using System;

namespace System.Web
{
	// Token: 0x020000E9 RID: 233
	public enum ProcessShutdownReason
	{
		// Token: 0x04000566 RID: 1382
		None,
		// Token: 0x04000567 RID: 1383
		Unexpected,
		// Token: 0x04000568 RID: 1384
		RequestsLimit,
		// Token: 0x04000569 RID: 1385
		RequestQueueLimit,
		// Token: 0x0400056A RID: 1386
		Timeout,
		// Token: 0x0400056B RID: 1387
		IdleTimeout,
		// Token: 0x0400056C RID: 1388
		MemoryLimitExceeded,
		// Token: 0x0400056D RID: 1389
		PingFailed,
		// Token: 0x0400056E RID: 1390
		DeadlockSuspected
	}
}

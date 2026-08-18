using System;

namespace System.Data
{
	// Token: 0x0200010A RID: 266
	public enum IsolationLevel
	{
		// Token: 0x04000575 RID: 1397
		Unspecified = -1,
		// Token: 0x04000576 RID: 1398
		Chaos = 16,
		// Token: 0x04000577 RID: 1399
		ReadUncommitted = 256,
		// Token: 0x04000578 RID: 1400
		ReadCommitted = 4096,
		// Token: 0x04000579 RID: 1401
		RepeatableRead = 65536,
		// Token: 0x0400057A RID: 1402
		Serializable = 1048576,
		// Token: 0x0400057B RID: 1403
		Snapshot = 16777216
	}
}

using System;

namespace System.Data
{
	// Token: 0x020000C1 RID: 193
	public enum IsolationLevel
	{
		// Token: 0x040008AB RID: 2219
		Unspecified = -1,
		// Token: 0x040008AC RID: 2220
		Chaos = 16,
		// Token: 0x040008AD RID: 2221
		ReadUncommitted = 256,
		// Token: 0x040008AE RID: 2222
		ReadCommitted = 4096,
		// Token: 0x040008AF RID: 2223
		RepeatableRead = 65536,
		// Token: 0x040008B0 RID: 2224
		Serializable = 1048576,
		// Token: 0x040008B1 RID: 2225
		Snapshot = 16777216
	}
}

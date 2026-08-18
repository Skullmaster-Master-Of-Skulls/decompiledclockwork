using System;

namespace System.Diagnostics
{
	// Token: 0x020004B5 RID: 1205
	[Flags]
	public enum TraceOptions
	{
		// Token: 0x040026FB RID: 9979
		None = 0,
		// Token: 0x040026FC RID: 9980
		LogicalOperationStack = 1,
		// Token: 0x040026FD RID: 9981
		DateTime = 2,
		// Token: 0x040026FE RID: 9982
		Timestamp = 4,
		// Token: 0x040026FF RID: 9983
		ProcessId = 8,
		// Token: 0x04002700 RID: 9984
		ThreadId = 16,
		// Token: 0x04002701 RID: 9985
		Callstack = 32
	}
}

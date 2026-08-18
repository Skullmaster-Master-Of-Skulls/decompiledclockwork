using System;

namespace System.Diagnostics
{
	// Token: 0x0200029C RID: 668
	public enum TraceLogRetentionOption
	{
		// Token: 0x04000BA7 RID: 2983
		SingleFileUnboundedSize = 2,
		// Token: 0x04000BA8 RID: 2984
		SingleFileBoundedSize = 4,
		// Token: 0x04000BA9 RID: 2985
		UnlimitedSequentialFiles = 0,
		// Token: 0x04000BAA RID: 2986
		LimitedSequentialFiles = 3,
		// Token: 0x04000BAB RID: 2987
		LimitedCircularFiles = 1
	}
}

using System;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000186 RID: 390
	public enum SecurityLogOnType
	{
		// Token: 0x04000423 RID: 1059
		Interactive = 2,
		// Token: 0x04000424 RID: 1060
		Network,
		// Token: 0x04000425 RID: 1061
		Batch,
		// Token: 0x04000426 RID: 1062
		Service,
		// Token: 0x04000427 RID: 1063
		NetworkClearText = 8,
		// Token: 0x04000428 RID: 1064
		NewCredentials
	}
}

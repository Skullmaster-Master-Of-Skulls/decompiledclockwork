using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkServerJob
{
	// Token: 0x02000457 RID: 1111
	[Serializable]
	public enum eClockWorkServerJobResult
	{
		// Token: 0x04001984 RID: 6532
		UnKnown = -2,
		// Token: 0x04001985 RID: 6533
		ProcessKilled,
		// Token: 0x04001986 RID: 6534
		Success,
		// Token: 0x04001987 RID: 6535
		SuccessWithWarnings,
		// Token: 0x04001988 RID: 6536
		Error,
		// Token: 0x04001989 RID: 6537
		Warning,
		// Token: 0x0400198A RID: 6538
		StoppedWhileStillRunning,
		// Token: 0x0400198B RID: 6539
		Timeout,
		// Token: 0x0400198C RID: 6540
		Running
	}
}

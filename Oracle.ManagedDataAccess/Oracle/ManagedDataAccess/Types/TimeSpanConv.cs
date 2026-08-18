using System;

namespace Oracle.ManagedDataAccess.Types
{
	// Token: 0x0200023F RID: 575
	internal static class TimeSpanConv
	{
		// Token: 0x04001981 RID: 6529
		internal const int FSecondsPerMilliSecond = 1000000;

		// Token: 0x04001982 RID: 6530
		internal const int FSecondsPerSecond = 1000000000;

		// Token: 0x04001983 RID: 6531
		internal const int FSecondsPerTick = 100;

		// Token: 0x04001984 RID: 6532
		internal const double TicksPerFSecond = 0.01;
	}
}

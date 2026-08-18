using System;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000038 RID: 56
	[Flags]
	internal enum PerfCounterLevel
	{
		// Token: 0x040001BB RID: 443
		None = 0,
		// Token: 0x040001BC RID: 444
		HardConnectsPerSecond = 1,
		// Token: 0x040001BD RID: 445
		HardDisconnectsPerSecond = 2,
		// Token: 0x040001BE RID: 446
		SoftConnectsPerSecond = 4,
		// Token: 0x040001BF RID: 447
		SoftDisconnectsPerSecond = 8,
		// Token: 0x040001C0 RID: 448
		NumberOfActiveConnectionPools = 16,
		// Token: 0x040001C1 RID: 449
		NumberOfInactiveConnectionPools = 32,
		// Token: 0x040001C2 RID: 450
		NumberOfActiveConnections = 64,
		// Token: 0x040001C3 RID: 451
		NumberOfFreeConnections = 128,
		// Token: 0x040001C4 RID: 452
		NumberOfPooledConnections = 256,
		// Token: 0x040001C5 RID: 453
		NumberOfNonPooledConnections = 512,
		// Token: 0x040001C6 RID: 454
		NumberOfReclaimedConnections = 1024,
		// Token: 0x040001C7 RID: 455
		NumberOfStasisConnections = 2048
	}
}

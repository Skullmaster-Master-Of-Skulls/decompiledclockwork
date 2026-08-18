using System;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000CF RID: 207
	internal enum DeletionRequestor
	{
		// Token: 0x04000AEB RID: 2795
		None,
		// Token: 0x04000AEC RID: 2796
		ClearPool,
		// Token: 0x04000AED RID: 2797
		ConnectionLifetime,
		// Token: 0x04000AEE RID: 2798
		PoolRegulator,
		// Token: 0x04000AEF RID: 2799
		HA,
		// Token: 0x04000AF0 RID: 2800
		Put
	}
}

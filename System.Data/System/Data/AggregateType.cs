using System;

namespace System.Data
{
	// Token: 0x02000059 RID: 89
	internal enum AggregateType
	{
		// Token: 0x040006AD RID: 1709
		None,
		// Token: 0x040006AE RID: 1710
		Sum = 4,
		// Token: 0x040006AF RID: 1711
		Mean,
		// Token: 0x040006B0 RID: 1712
		Min,
		// Token: 0x040006B1 RID: 1713
		Max,
		// Token: 0x040006B2 RID: 1714
		First,
		// Token: 0x040006B3 RID: 1715
		Count,
		// Token: 0x040006B4 RID: 1716
		Var,
		// Token: 0x040006B5 RID: 1717
		StDev
	}
}

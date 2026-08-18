using System;

namespace System.Linq.Parallel
{
	// Token: 0x0200017D RID: 381
	[Flags]
	internal enum QueryAggregationOptions
	{
		// Token: 0x04000818 RID: 2072
		None = 0,
		// Token: 0x04000819 RID: 2073
		Associative = 1,
		// Token: 0x0400081A RID: 2074
		Commutative = 2,
		// Token: 0x0400081B RID: 2075
		AssociativeCommutative = 3
	}
}

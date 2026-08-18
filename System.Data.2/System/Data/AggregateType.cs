using System;

namespace System.Data
{
	// Token: 0x0200008F RID: 143
	internal enum AggregateType
	{
		// Token: 0x040002AC RID: 684
		None,
		// Token: 0x040002AD RID: 685
		Sum = 4,
		// Token: 0x040002AE RID: 686
		Mean,
		// Token: 0x040002AF RID: 687
		Min,
		// Token: 0x040002B0 RID: 688
		Max,
		// Token: 0x040002B1 RID: 689
		First,
		// Token: 0x040002B2 RID: 690
		Count,
		// Token: 0x040002B3 RID: 691
		Var,
		// Token: 0x040002B4 RID: 692
		StDev
	}
}

using System;

namespace System.Data.Common
{
	// Token: 0x02000166 RID: 358
	[Flags]
	public enum SupportedJoinOperators
	{
		// Token: 0x04000CF8 RID: 3320
		None = 0,
		// Token: 0x04000CF9 RID: 3321
		Inner = 1,
		// Token: 0x04000CFA RID: 3322
		LeftOuter = 2,
		// Token: 0x04000CFB RID: 3323
		RightOuter = 4,
		// Token: 0x04000CFC RID: 3324
		FullOuter = 8
	}
}

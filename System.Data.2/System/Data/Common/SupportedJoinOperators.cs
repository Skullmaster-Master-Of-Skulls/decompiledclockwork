using System;

namespace System.Data.Common
{
	// Token: 0x02000326 RID: 806
	[Flags]
	public enum SupportedJoinOperators
	{
		// Token: 0x04001DC6 RID: 7622
		None = 0,
		// Token: 0x04001DC7 RID: 7623
		Inner = 1,
		// Token: 0x04001DC8 RID: 7624
		LeftOuter = 2,
		// Token: 0x04001DC9 RID: 7625
		RightOuter = 4,
		// Token: 0x04001DCA RID: 7626
		FullOuter = 8
	}
}

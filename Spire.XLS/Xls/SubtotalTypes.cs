using System;

namespace Spire.Xls
{
	// Token: 0x02000114 RID: 276
	[Flags]
	public enum SubtotalTypes
	{
		// Token: 0x04000A54 RID: 2644
		None = 0,
		// Token: 0x04000A55 RID: 2645
		Default = 1,
		// Token: 0x04000A56 RID: 2646
		Sum = 2,
		// Token: 0x04000A57 RID: 2647
		Counta = 4,
		// Token: 0x04000A58 RID: 2648
		Average = 8,
		// Token: 0x04000A59 RID: 2649
		Max = 16,
		// Token: 0x04000A5A RID: 2650
		Min = 32,
		// Token: 0x04000A5B RID: 2651
		Product = 64,
		// Token: 0x04000A5C RID: 2652
		Count = 128,
		// Token: 0x04000A5D RID: 2653
		Stdev = 256,
		// Token: 0x04000A5E RID: 2654
		Stdevp = 512,
		// Token: 0x04000A5F RID: 2655
		Var = 1024,
		// Token: 0x04000A60 RID: 2656
		Varp = 2048
	}
}

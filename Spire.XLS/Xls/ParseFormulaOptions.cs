using System;

namespace Spire.Xls
{
	// Token: 0x0200009E RID: 158
	[Flags]
	internal enum ParseFormulaOptions
	{
		// Token: 0x040006C8 RID: 1736
		None = 0,
		// Token: 0x040006C9 RID: 1737
		RootLevel = 1,
		// Token: 0x040006CA RID: 1738
		InArray = 2,
		// Token: 0x040006CB RID: 1739
		InName = 4,
		// Token: 0x040006CC RID: 1740
		ParseOperand = 8,
		// Token: 0x040006CD RID: 1741
		ParseComplexOperand = 16,
		// Token: 0x040006CE RID: 1742
		UseR1C1 = 32,
		// Token: 0x040006CF RID: 1743
		DataValidation = 64
	}
}

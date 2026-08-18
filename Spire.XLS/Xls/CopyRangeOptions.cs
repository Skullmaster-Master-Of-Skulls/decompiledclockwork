using System;

namespace Spire.Xls
{
	// Token: 0x0200009D RID: 157
	[Flags]
	internal enum CopyRangeOptions
	{
		// Token: 0x040006BE RID: 1726
		None = 0,
		// Token: 0x040006BF RID: 1727
		UpdateFormulas = 1,
		// Token: 0x040006C0 RID: 1728
		UpdateMerges = 2,
		// Token: 0x040006C1 RID: 1729
		CopyStyles = 4,
		// Token: 0x040006C2 RID: 1730
		CopyShapes = 8,
		// Token: 0x040006C3 RID: 1731
		CopyErrorIndicators = 16,
		// Token: 0x040006C4 RID: 1732
		CopyConditionalFormats = 32,
		// Token: 0x040006C5 RID: 1733
		CopyDataValidations = 64,
		// Token: 0x040006C6 RID: 1734
		All = 127
	}
}

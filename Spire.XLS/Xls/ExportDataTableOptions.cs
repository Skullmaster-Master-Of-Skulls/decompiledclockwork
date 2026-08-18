using System;

namespace Spire.Xls
{
	// Token: 0x020000A3 RID: 163
	[Flags]
	internal enum ExportDataTableOptions
	{
		// Token: 0x040006F0 RID: 1776
		None = 0,
		// Token: 0x040006F1 RID: 1777
		ColumnNames = 1,
		// Token: 0x040006F2 RID: 1778
		ComputedFormulaValues = 2,
		// Token: 0x040006F3 RID: 1779
		DetectColumnTypes = 4,
		// Token: 0x040006F4 RID: 1780
		DefaultStyleColumnTypes = 8,
		// Token: 0x040006F5 RID: 1781
		PreserveOleDate = 16
	}
}

using System;

namespace Spire.DataExport.XLS.Formula
{
	// Token: 0x0200015C RID: 348
	[Flags]
	internal enum FormulaOptions : ushort
	{
		// Token: 0x0400069D RID: 1693
		RecalculateAlways = 1,
		// Token: 0x0400069E RID: 1694
		CalculateOnLoad = 2,
		// Token: 0x0400069F RID: 1695
		SharedFormula = 8,
		// Token: 0x040006A0 RID: 1696
		All = 11
	}
}

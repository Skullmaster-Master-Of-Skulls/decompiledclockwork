using System;

namespace Spire.Xls
{
	// Token: 0x020000D7 RID: 215
	[Flags]
	public enum IgnoreErrorType
	{
		// Token: 0x0400089E RID: 2206
		None = 0,
		// Token: 0x0400089F RID: 2207
		EvaluateToError = 1,
		// Token: 0x040008A0 RID: 2208
		EmptyCellReferences = 2,
		// Token: 0x040008A1 RID: 2209
		NumberAsText = 4,
		// Token: 0x040008A2 RID: 2210
		OmittedCells = 8,
		// Token: 0x040008A3 RID: 2211
		InconsistentFormula = 16,
		// Token: 0x040008A4 RID: 2212
		TextDate = 32,
		// Token: 0x040008A5 RID: 2213
		UnlockedFormulaCells = 64,
		// Token: 0x040008A6 RID: 2214
		All = 127
	}
}

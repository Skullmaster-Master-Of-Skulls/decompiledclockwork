using System;

namespace Spire.Xls
{
	// Token: 0x020000A8 RID: 168
	[Flags]
	public enum FindType
	{
		// Token: 0x04000708 RID: 1800
		Text = 1,
		// Token: 0x04000709 RID: 1801
		Formula = 2,
		// Token: 0x0400070A RID: 1802
		FormulaStringValue = 4,
		// Token: 0x0400070B RID: 1803
		Error = 8,
		// Token: 0x0400070C RID: 1804
		Number = 16,
		// Token: 0x0400070D RID: 1805
		FormulaValue = 32
	}
}

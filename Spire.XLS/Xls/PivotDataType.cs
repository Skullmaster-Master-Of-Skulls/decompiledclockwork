using System;

namespace Spire.Xls
{
	// Token: 0x02000117 RID: 279
	[Flags]
	public enum PivotDataType
	{
		// Token: 0x04000A7A RID: 2682
		Number = 1,
		// Token: 0x04000A7B RID: 2683
		Integer = 2,
		// Token: 0x04000A7C RID: 2684
		String = 4,
		// Token: 0x04000A7D RID: 2685
		Blank = 8,
		// Token: 0x04000A7E RID: 2686
		Date = 16,
		// Token: 0x04000A7F RID: 2687
		Boolean = 32,
		// Token: 0x04000A80 RID: 2688
		Float = 64,
		// Token: 0x04000A81 RID: 2689
		LongText = 128
	}
}

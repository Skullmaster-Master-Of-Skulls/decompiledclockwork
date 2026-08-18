using System;

namespace Spire.Xls
{
	// Token: 0x020000A2 RID: 162
	[Flags]
	public enum ExcelParseOptions
	{
		// Token: 0x040006EB RID: 1771
		Default = 0,
		// Token: 0x040006EC RID: 1772
		SkipStyles = 1,
		// Token: 0x040006ED RID: 1773
		DoNotParseCharts = 2,
		// Token: 0x040006EE RID: 1774
		DoNotParsePivotTable = 8
	}
}

using System;
using System.ComponentModel;

namespace Spire.Xls
{
	// Token: 0x02000065 RID: 101
	public enum ExcelSheetType
	{
		// Token: 0x0400025F RID: 607
		[Description("ChartSheet")]
		ChartSheet = 2,
		// Token: 0x04000260 RID: 608
		[Description("DialogSheet")]
		DialogSheet,
		// Token: 0x04000261 RID: 609
		[Description("Excel 4.0 International Marcos")]
		Excel4IntlMacroSheet,
		// Token: 0x04000262 RID: 610
		[Description("Excel 4.0 Macros")]
		Excel4MacroSheet,
		// Token: 0x04000263 RID: 611
		[Description("NormalWorksheet")]
		NormalWorksheet = 0
	}
}

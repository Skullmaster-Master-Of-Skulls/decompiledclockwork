using System;
using Spire.Xls.Core.Spreadsheet;

namespace Spire.Xls.Core.Interfaces
{
	// Token: 0x02000007 RID: 7
	public interface IInternalAddtionalFormat : IExtendedFormat
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000022 RID: 34
		OColor BottomBorderColor { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000023 RID: 35
		OColor TopBorderColor { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000024 RID: 36
		OColor LeftBorderColor { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37
		OColor RightBorderColor { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000026 RID: 38
		OColor DiagonalBorderColor { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39
		// (set) Token: 0x06000028 RID: 40
		LineStyleType LeftBorderLineStyle { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41
		// (set) Token: 0x0600002A RID: 42
		LineStyleType RightBorderLineStyle { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43
		// (set) Token: 0x0600002C RID: 44
		LineStyleType TopBorderLineStyle { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45
		// (set) Token: 0x0600002E RID: 46
		LineStyleType BottomBorderLineStyle { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47
		// (set) Token: 0x06000030 RID: 48
		LineStyleType DiagonalUpBorderLineStyle { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		LineStyleType DiagonalDownBorderLineStyle { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000033 RID: 51
		// (set) Token: 0x06000034 RID: 52
		bool DiagonalUpVisible { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000035 RID: 53
		// (set) Token: 0x06000036 RID: 54
		bool DiagonalDownVisible { get; set; }

		// Token: 0x06000037 RID: 55
		void BeginUpdate();

		// Token: 0x06000038 RID: 56
		void EndUpdate();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000039 RID: 57
		XlsWorkbook Workbook { get; }
	}
}

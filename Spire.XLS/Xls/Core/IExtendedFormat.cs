using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x02000008 RID: 8
	public interface IExtendedFormat : IExcelApplication
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003A RID: 58
		IBorders Borders { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003B RID: 59
		// (set) Token: 0x0600003C RID: 60
		ExcelPatternType FillPattern { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003D RID: 61
		IFont Font { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003E RID: 62
		// (set) Token: 0x0600003F RID: 63
		bool FormulaHidden { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000040 RID: 64
		// (set) Token: 0x06000041 RID: 65
		HorizontalAlignType HorizontalAlignment { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000042 RID: 66
		// (set) Token: 0x06000043 RID: 67
		bool IncludeAlignment { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000044 RID: 68
		// (set) Token: 0x06000045 RID: 69
		bool IncludeBorder { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000046 RID: 70
		// (set) Token: 0x06000047 RID: 71
		bool IncludeFont { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000048 RID: 72
		// (set) Token: 0x06000049 RID: 73
		bool IncludeNumberFormat { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004A RID: 74
		// (set) Token: 0x0600004B RID: 75
		bool IncludePatterns { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600004C RID: 76
		// (set) Token: 0x0600004D RID: 77
		bool IncludeProtection { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600004E RID: 78
		// (set) Token: 0x0600004F RID: 79
		int IndentLevel { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000050 RID: 80
		// (set) Token: 0x06000051 RID: 81
		bool IsFirstSymbolApostrophe { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000052 RID: 82
		// (set) Token: 0x06000053 RID: 83
		bool Locked { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000054 RID: 84
		// (set) Token: 0x06000055 RID: 85
		bool JustifyLast { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		string NumberFormat { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000058 RID: 88
		// (set) Token: 0x06000059 RID: 89
		int NumberFormatIndex { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005A RID: 90
		// (set) Token: 0x0600005B RID: 91
		string NumberFormatLocal { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005C RID: 92
		INumberFormat NumberFormatSettings { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005D RID: 93
		// (set) Token: 0x0600005E RID: 94
		ReadingOrderType ReadingOrder { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005F RID: 95
		// (set) Token: 0x06000060 RID: 96
		int Rotation { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000061 RID: 97
		// (set) Token: 0x06000062 RID: 98
		bool ShrinkToFit { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000063 RID: 99
		// (set) Token: 0x06000064 RID: 100
		VerticalAlignType VerticalAlignment { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000065 RID: 101
		// (set) Token: 0x06000066 RID: 102
		bool WrapText { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		ExcelColors PatternKnownColor { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		Color PatternColor { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006B RID: 107
		// (set) Token: 0x0600006C RID: 108
		ExcelColors KnownColor { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006D RID: 109
		// (set) Token: 0x0600006E RID: 110
		Color Color { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006F RID: 111
		bool IsModified { get; }
	}
}

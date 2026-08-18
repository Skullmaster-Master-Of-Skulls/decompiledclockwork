using System;
using System.Drawing;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x0200000B RID: 11
	public interface IFont : IExcelApplication, IOptimizedUpdate
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000073 RID: 115
		// (set) Token: 0x06000074 RID: 116
		bool IsBold { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000075 RID: 117
		// (set) Token: 0x06000076 RID: 118
		ExcelColors KnownColor { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000077 RID: 119
		// (set) Token: 0x06000078 RID: 120
		Color Color { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000079 RID: 121
		// (set) Token: 0x0600007A RID: 122
		bool IsItalic { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007B RID: 123
		// (set) Token: 0x0600007C RID: 124
		double Size { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007D RID: 125
		// (set) Token: 0x0600007E RID: 126
		bool IsStrikethrough { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007F RID: 127
		// (set) Token: 0x06000080 RID: 128
		bool IsSubscript { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000081 RID: 129
		// (set) Token: 0x06000082 RID: 130
		bool IsSuperscript { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000083 RID: 131
		// (set) Token: 0x06000084 RID: 132
		FontUnderlineType Underline { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000085 RID: 133
		// (set) Token: 0x06000086 RID: 134
		string FontName { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		FontVertialAlignmentType VerticalAlignment { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000089 RID: 137
		bool IsAutoColor { get; }

		// Token: 0x0600008A RID: 138
		Font GenerateNativeFont();
	}
}

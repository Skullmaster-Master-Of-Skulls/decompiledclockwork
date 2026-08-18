using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x0200010B RID: 267
	public interface IBorder : IExcelApplication
	{
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000C0E RID: 3086
		// (set) Token: 0x06000C0F RID: 3087
		ExcelColors KnownColor { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000C10 RID: 3088
		OColor OColor { get; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000C11 RID: 3089
		// (set) Token: 0x06000C12 RID: 3090
		Color Color { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000C13 RID: 3091
		// (set) Token: 0x06000C14 RID: 3092
		LineStyleType LineStyle { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000C15 RID: 3093
		// (set) Token: 0x06000C16 RID: 3094
		bool ShowDiagonalLine { get; set; }
	}
}

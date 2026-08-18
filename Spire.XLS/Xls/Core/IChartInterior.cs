using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020001CC RID: 460
	public interface IChartInterior
	{
		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060019BF RID: 6591
		// (set) Token: 0x060019C0 RID: 6592
		Color ForegroundColor { get; set; }

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060019C1 RID: 6593
		// (set) Token: 0x060019C2 RID: 6594
		Color BackgroundColor { get; set; }

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060019C3 RID: 6595
		// (set) Token: 0x060019C4 RID: 6596
		ExcelPatternType Pattern { get; set; }

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060019C5 RID: 6597
		// (set) Token: 0x060019C6 RID: 6598
		ExcelColors ForegroundKnownColor { get; set; }

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060019C7 RID: 6599
		// (set) Token: 0x060019C8 RID: 6600
		ExcelColors BackgroundKnownColor { get; set; }

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060019C9 RID: 6601
		// (set) Token: 0x060019CA RID: 6602
		bool UseDefaultFormat { get; set; }

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060019CB RID: 6603
		// (set) Token: 0x060019CC RID: 6604
		bool SwapColorsOnNegative { get; set; }
	}
}

using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001C8 RID: 456
	public interface IChartDataLabels : IChartTextArea
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060019A0 RID: 6560
		// (set) Token: 0x060019A1 RID: 6561
		bool HasSeriesName { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060019A2 RID: 6562
		// (set) Token: 0x060019A3 RID: 6563
		bool HasCategoryName { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060019A4 RID: 6564
		// (set) Token: 0x060019A5 RID: 6565
		bool HasValue { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060019A6 RID: 6566
		// (set) Token: 0x060019A7 RID: 6567
		bool HasPercentage { get; set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060019A8 RID: 6568
		// (set) Token: 0x060019A9 RID: 6569
		bool HasBubbleSize { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060019AA RID: 6570
		// (set) Token: 0x060019AB RID: 6571
		string Delimiter { get; set; }

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060019AC RID: 6572
		// (set) Token: 0x060019AD RID: 6573
		bool HasLegendKey { get; set; }

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060019AE RID: 6574
		// (set) Token: 0x060019AF RID: 6575
		DataLabelPositionType Position { get; set; }

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060019B0 RID: 6576
		// (set) Token: 0x060019B1 RID: 6577
		bool ShowLeaderLines { get; set; }
	}
}

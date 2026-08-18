using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001D2 RID: 466
	public interface IChartCategoryAxis : IChartValueAxis
	{
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060019FA RID: 6650
		// (set) Token: 0x060019FB RID: 6651
		double CrossingPoint { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060019FC RID: 6652
		// (set) Token: 0x060019FD RID: 6653
		int LabelFrequency { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060019FE RID: 6654
		// (set) Token: 0x060019FF RID: 6655
		int TickMarksFrequency { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001A00 RID: 6656
		// (set) Token: 0x06001A01 RID: 6657
		int TickLabelSpacing { get; set; }

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001A02 RID: 6658
		// (set) Token: 0x06001A03 RID: 6659
		int TickMarkSpacing { get; set; }

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001A04 RID: 6660
		// (set) Token: 0x06001A05 RID: 6661
		bool AxisBetweenCategories { get; set; }

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001A06 RID: 6662
		// (set) Token: 0x06001A07 RID: 6663
		IXLSRange CategoryLabels { get; set; }

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001A08 RID: 6664
		// (set) Token: 0x06001A09 RID: 6665
		object[] EnteredDirectlyCategoryLabels { get; set; }

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001A0A RID: 6666
		// (set) Token: 0x06001A0B RID: 6667
		CategoryType CategoryType { get; set; }

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001A0C RID: 6668
		// (set) Token: 0x06001A0D RID: 6669
		int Offset { get; set; }

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001A0E RID: 6670
		// (set) Token: 0x06001A0F RID: 6671
		ChartBaseUnitType BaseUnit { get; set; }

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001A10 RID: 6672
		// (set) Token: 0x06001A11 RID: 6673
		bool BaseUnitIsAuto { get; set; }

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001A12 RID: 6674
		// (set) Token: 0x06001A13 RID: 6675
		ChartBaseUnitType MajorUnitScale { get; set; }

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001A14 RID: 6676
		// (set) Token: 0x06001A15 RID: 6677
		ChartBaseUnitType MinorUnitScale { get; set; }
	}
}

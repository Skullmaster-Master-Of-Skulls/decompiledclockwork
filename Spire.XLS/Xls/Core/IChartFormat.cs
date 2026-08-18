using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001CE RID: 462
	public interface IChartFormat
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060019DB RID: 6619
		// (set) Token: 0x060019DC RID: 6620
		bool IsVaryColor { get; set; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060019DD RID: 6621
		// (set) Token: 0x060019DE RID: 6622
		int Overlap { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060019DF RID: 6623
		// (set) Token: 0x060019E0 RID: 6624
		int GapWidth { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060019E1 RID: 6625
		// (set) Token: 0x060019E2 RID: 6626
		int FirstSliceAngle { get; set; }

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060019E3 RID: 6627
		// (set) Token: 0x060019E4 RID: 6628
		int DoughnutHoleSize { get; set; }

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060019E5 RID: 6629
		// (set) Token: 0x060019E6 RID: 6630
		int BubbleScale { get; set; }

		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060019E7 RID: 6631
		// (set) Token: 0x060019E8 RID: 6632
		BubbleSizeType SizeRepresents { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060019E9 RID: 6633
		// (set) Token: 0x060019EA RID: 6634
		bool ShowNegativeBubbles { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060019EB RID: 6635
		// (set) Token: 0x060019EC RID: 6636
		bool HasRadarAxisLabels { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060019ED RID: 6637
		// (set) Token: 0x060019EE RID: 6638
		SplitType SplitType { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060019EF RID: 6639
		// (set) Token: 0x060019F0 RID: 6640
		int SplitValue { get; set; }

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060019F1 RID: 6641
		// (set) Token: 0x060019F2 RID: 6642
		int PieSecondSize { get; set; }

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060019F3 RID: 6643
		IChartDropBar FirstDropBar { get; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060019F4 RID: 6644
		IChartDropBar SecondDropBar { get; }

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060019F5 RID: 6645
		IChartBorder PieSeriesLine { get; }
	}
}

using System;
using System.Drawing;

namespace Spire.Xls.Core
{
	// Token: 0x020001C3 RID: 451
	public interface IChartBorder
	{
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x0600194A RID: 6474
		// (set) Token: 0x0600194B RID: 6475
		Color Color { get; set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x0600194C RID: 6476
		// (set) Token: 0x0600194D RID: 6477
		ChartLinePatternType Pattern { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600194E RID: 6478
		// (set) Token: 0x0600194F RID: 6479
		ChartLineWeightType Weight { get; set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001950 RID: 6480
		// (set) Token: 0x06001951 RID: 6481
		bool UseDefaultFormat { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001952 RID: 6482
		// (set) Token: 0x06001953 RID: 6483
		bool UseDefaultLineColor { get; set; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001954 RID: 6484
		// (set) Token: 0x06001955 RID: 6485
		ExcelColors KnownColor { get; set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001956 RID: 6486
		// (set) Token: 0x06001957 RID: 6487
		bool DrawTickLabels { get; set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001958 RID: 6488
		// (set) Token: 0x06001959 RID: 6489
		double Transparency { get; set; }
	}
}

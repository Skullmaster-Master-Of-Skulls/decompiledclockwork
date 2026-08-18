using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001CB RID: 459
	public interface IChartSeriesAxis : IChartAxis
	{
		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060019B9 RID: 6585
		// (set) Token: 0x060019BA RID: 6586
		int LabelsFrequency { get; set; }

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060019BB RID: 6587
		// (set) Token: 0x060019BC RID: 6588
		int TickLabelSpacing { get; set; }

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060019BD RID: 6589
		// (set) Token: 0x060019BE RID: 6590
		int TickMarksFrequency { get; set; }
	}
}

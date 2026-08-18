using System;
using Spire.Xls.Charts;

namespace Spire.Xls.Core
{
	// Token: 0x020001C6 RID: 454
	public interface IChartDataTable
	{
		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600197E RID: 6526
		// (set) Token: 0x0600197F RID: 6527
		bool HasHorzBorder { get; set; }

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001980 RID: 6528
		// (set) Token: 0x06001981 RID: 6529
		bool HasVertBorder { get; set; }

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001982 RID: 6530
		// (set) Token: 0x06001983 RID: 6531
		bool HasBorders { get; set; }

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001984 RID: 6532
		// (set) Token: 0x06001985 RID: 6533
		bool ShowSeriesKeys { get; set; }

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001986 RID: 6534
		ChartTextArea TextArea { get; }
	}
}

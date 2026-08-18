using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001C4 RID: 452
	public interface IChartSerie : IExcelApplication
	{
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x0600195A RID: 6490
		// (set) Token: 0x0600195B RID: 6491
		IXLSRange Values { get; set; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x0600195C RID: 6492
		// (set) Token: 0x0600195D RID: 6493
		IXLSRange CategoryLabels { get; set; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x0600195E RID: 6494
		// (set) Token: 0x0600195F RID: 6495
		IXLSRange Bubbles { get; set; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001960 RID: 6496
		// (set) Token: 0x06001961 RID: 6497
		string Name { get; set; }

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001962 RID: 6498
		CellRange NamedRange { get; }

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001963 RID: 6499
		// (set) Token: 0x06001964 RID: 6500
		bool UsePrimaryAxis { get; set; }

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001965 RID: 6501
		IChartDataPoints DataPoints { get; }

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001966 RID: 6502
		IChartSerieDataFormat Format { get; }

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001967 RID: 6503
		// (set) Token: 0x06001968 RID: 6504
		ExcelChartType SerieType { get; set; }

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001969 RID: 6505
		// (set) Token: 0x0600196A RID: 6506
		object[] EnteredDirectlyValues { get; set; }

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x0600196B RID: 6507
		// (set) Token: 0x0600196C RID: 6508
		object[] EnteredDirectlyCategoryLabels { get; set; }

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x0600196D RID: 6509
		// (set) Token: 0x0600196E RID: 6510
		object[] EnteredDirectlyBubbles { get; set; }

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x0600196F RID: 6511
		IChartErrorBars ErrorBarsY { get; }

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001970 RID: 6512
		// (set) Token: 0x06001971 RID: 6513
		bool HasErrorBarsY { get; set; }

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001972 RID: 6514
		IChartErrorBars ErrorBarsX { get; }

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001973 RID: 6515
		// (set) Token: 0x06001974 RID: 6516
		bool HasErrorBarsX { get; set; }

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001975 RID: 6517
		IChartTrendLines TrendLines { get; }

		// Token: 0x06001976 RID: 6518
		IChartErrorBars ErrorBar(bool bIsY);

		// Token: 0x06001977 RID: 6519
		IChartErrorBars ErrorBar(bool bIsY, ErrorBarIncludeType include);

		// Token: 0x06001978 RID: 6520
		IChartErrorBars ErrorBar(bool bIsY, ErrorBarIncludeType include, ErrorBarType type);

		// Token: 0x06001979 RID: 6521
		IChartErrorBars ErrorBar(bool bIsY, ErrorBarIncludeType include, ErrorBarType type, double numberValue);

		// Token: 0x0600197A RID: 6522
		IChartErrorBars ErrorBar(bool bIsY, IXLSRange plusRange, IXLSRange minusRange);
	}
}

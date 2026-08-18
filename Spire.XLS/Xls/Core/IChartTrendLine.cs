using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000359 RID: 857
	public interface IChartTrendLine
	{
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06003452 RID: 13394
		IChartBorder Border { get; }

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06003453 RID: 13395
		// (set) Token: 0x06003454 RID: 13396
		double Backward { get; set; }

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06003455 RID: 13397
		// (set) Token: 0x06003456 RID: 13398
		double Forward { get; set; }

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003457 RID: 13399
		// (set) Token: 0x06003458 RID: 13400
		bool DisplayEquation { get; set; }

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003459 RID: 13401
		// (set) Token: 0x0600345A RID: 13402
		bool DisplayRSquared { get; set; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x0600345B RID: 13403
		// (set) Token: 0x0600345C RID: 13404
		double Intercept { get; set; }

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x0600345D RID: 13405
		// (set) Token: 0x0600345E RID: 13406
		bool InterceptIsAuto { get; set; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600345F RID: 13407
		// (set) Token: 0x06003460 RID: 13408
		TrendLineType Type { get; set; }

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06003461 RID: 13409
		// (set) Token: 0x06003462 RID: 13410
		int Order { get; set; }

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06003463 RID: 13411
		// (set) Token: 0x06003464 RID: 13412
		bool NameIsAuto { get; set; }

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06003465 RID: 13413
		// (set) Token: 0x06003466 RID: 13414
		string Name { get; set; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06003467 RID: 13415
		IChartTextArea DataLabel { get; }

		// Token: 0x06003468 RID: 13416
		void ClearFormats();
	}
}

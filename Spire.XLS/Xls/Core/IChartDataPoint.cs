using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001BC RID: 444
	public interface IChartDataPoint : IExcelApplication
	{
		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001898 RID: 6296
		IChartDataLabels DataLabels { get; }

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001899 RID: 6297
		int Index { get; }

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x0600189A RID: 6298
		IChartSerieDataFormat DataFormat { get; }

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600189B RID: 6299
		bool IsDefault { get; }
	}
}

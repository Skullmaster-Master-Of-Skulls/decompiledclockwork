using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001D4 RID: 468
	public interface IChartLegendEntries
	{
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001A22 RID: 6690
		int Count { get; }

		// Token: 0x170009B9 RID: 2489
		IChartLegendEntry this[int iIndex]
		{
			get;
		}
	}
}

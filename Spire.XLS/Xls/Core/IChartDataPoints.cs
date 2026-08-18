using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020001D0 RID: 464
	public interface IChartDataPoints : IExcelApplication, IEnumerable
	{
		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060019F8 RID: 6648
		IChartDataPoint DefaultDataPoint { get; }

		// Token: 0x170009A7 RID: 2471
		IChartDataPoint this[int index]
		{
			get;
		}
	}
}

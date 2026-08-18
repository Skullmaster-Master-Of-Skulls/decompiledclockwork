using System;
using Spire.Xls.Charts;

namespace Spire.Xls.Core
{
	// Token: 0x020001CF RID: 463
	public interface IChartGridLine : IChartFillBorder
	{
		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060019F6 RID: 6646
		ChartBorder Border { get; }

		// Token: 0x060019F7 RID: 6647
		void Delete();
	}
}

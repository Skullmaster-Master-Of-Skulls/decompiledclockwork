using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200035A RID: 858
	public interface IChartTrendLines
	{
		// Token: 0x17000CDF RID: 3295
		IChartTrendLine this[int iIndex]
		{
			get;
		}

		// Token: 0x0600346A RID: 13418
		IChartTrendLine Add();

		// Token: 0x0600346B RID: 13419
		IChartTrendLine Add(TrendLineType type);

		// Token: 0x0600346C RID: 13420
		void RemoveAt(int index);

		// Token: 0x0600346D RID: 13421
		void Clear();

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600346E RID: 13422
		int Count { get; }
	}
}

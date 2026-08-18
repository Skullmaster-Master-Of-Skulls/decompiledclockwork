using System;
using System.Collections.Generic;

namespace Spire.Xls.Core
{
	// Token: 0x020001C5 RID: 453
	public interface IChartSeries : IExcelApplication, ICollection<IChartSerie>
	{
		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600197B RID: 6523
		int Count { get; }

		// Token: 0x0600197C RID: 6524
		void RemoveAt(int index);

		// Token: 0x0600197D RID: 6525
		void Remove(string serieName);
	}
}

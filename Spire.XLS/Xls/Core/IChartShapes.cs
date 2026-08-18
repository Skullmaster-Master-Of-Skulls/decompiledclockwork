using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x02000207 RID: 519
	public interface IChartShapes : IEnumerable, IExcelApplication
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06001DB3 RID: 7603
		int Count { get; }

		// Token: 0x17000B0F RID: 2831
		IChartShape this[int index]
		{
			get;
		}

		// Token: 0x06001DB5 RID: 7605
		void RemoveAt(int index);
	}
}

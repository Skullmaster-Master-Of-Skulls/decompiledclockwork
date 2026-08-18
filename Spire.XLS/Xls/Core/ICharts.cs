using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020001EE RID: 494
	public interface ICharts : IEnumerable, IExcelApplication
	{
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06001C30 RID: 7216
		int Count { get; }

		// Token: 0x17000A7D RID: 2685
		IChart this[int index]
		{
			get;
		}

		// Token: 0x17000A7E RID: 2686
		IChart this[string name]
		{
			get;
		}

		// Token: 0x06001C33 RID: 7219
		IChart Add();

		// Token: 0x06001C34 RID: 7220
		IChart Add(string name);

		// Token: 0x06001C35 RID: 7221
		IChart Remove(string name);
	}
}

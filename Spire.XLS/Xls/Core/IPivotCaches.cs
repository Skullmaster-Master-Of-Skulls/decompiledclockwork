using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001FA RID: 506
	public interface IPivotCaches
	{
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06001C94 RID: 7316
		int Count { get; }

		// Token: 0x17000AA4 RID: 2724
		IPivotCache this[int index]
		{
			get;
		}

		// Token: 0x06001C96 RID: 7318
		PivotCache Add(CellRange range);
	}
}

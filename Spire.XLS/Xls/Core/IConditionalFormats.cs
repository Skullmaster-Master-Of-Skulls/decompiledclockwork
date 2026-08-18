using System;
using System.Collections;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x0200020A RID: 522
	public interface IConditionalFormats : IEnumerable, IExcelApplication, IOptimizedUpdate
	{
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06001EA1 RID: 7841
		int Count { get; }

		// Token: 0x17000B4F RID: 2895
		IConditionalFormat this[int index]
		{
			get;
		}

		// Token: 0x06001EA3 RID: 7843
		IConditionalFormat AddCondition();

		// Token: 0x06001EA4 RID: 7844
		void Remove();

		// Token: 0x06001EA5 RID: 7845
		void RemoveAt(int index);
	}
}

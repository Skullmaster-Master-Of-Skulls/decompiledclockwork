using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CD5 RID: 3285
	internal class DescendingSort<T> : IComparer<T>
	{
		// Token: 0x06007AC4 RID: 31428 RVA: 0x001C2AED File Offset: 0x001C0CED
		public DescendingSort(IComparer<T> sort)
		{
			this.sort = sort;
		}

		// Token: 0x06007AC5 RID: 31429 RVA: 0x001C2AFC File Offset: 0x001C0CFC
		public int Compare(T x, T y)
		{
			return -this.sort.Compare(x, y);
		}

		// Token: 0x0400219D RID: 8605
		private IComparer<T> sort;
	}
}

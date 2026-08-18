using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x020019B0 RID: 6576
	internal class ThenByEnumerable<TElement, TKey, TLastKey> : OrderByEnumerable<TElement, TKey>
	{
		// Token: 0x0600FE45 RID: 65093 RVA: 0x00392037 File Offset: 0x00390237
		public ThenByEnumerable(OrderByEnumerable<TElement, TLastKey> source, TFunc<object, TKey> keySelector, IComparer<TKey> comparer, bool descending, bool stableSort) : base(source, keySelector, comparer, descending, stableSort)
		{
		}

		// Token: 0x17004CBB RID: 19643
		// (get) Token: 0x0600FE46 RID: 65094 RVA: 0x00392046 File Offset: 0x00390246
		public OrderByEnumerable<TElement, TLastKey> OrderedSource
		{
			get
			{
				return (OrderByEnumerable<TElement, TLastKey>)this.Source;
			}
		}

		// Token: 0x0600FE47 RID: 65095 RVA: 0x00392054 File Offset: 0x00390254
		internal override int CompareElements(object e1, object e2)
		{
			int num = this.OrderedSource.CompareElements(e1, e2);
			if (num != 0)
			{
				return num;
			}
			return base.CompareElements(e1, e2);
		}

		// Token: 0x0600FE48 RID: 65096 RVA: 0x0039207C File Offset: 0x0039027C
		internal override IEnumerable GetElementsToSort()
		{
			return this.OrderedSource.GetElementsToSort();
		}
	}
}

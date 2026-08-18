using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Web.UI.Functions;

namespace Telerik.Web.UI
{
	// Token: 0x020019AD RID: 6573
	internal interface IOrderedEnumerable<TElement> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x0600FE39 RID: 65081
		IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(TFunc<object, TKey> keySelector, IComparer<TKey> comparer, bool descending, bool stableSort);
	}
}

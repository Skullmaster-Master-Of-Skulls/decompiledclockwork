using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000152 RID: 338
	[__DynamicallyInvokable]
	public interface IOrderedEnumerable<TElement> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000BFD RID: 3069
		[__DynamicallyInvokable]
		IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
	}
}

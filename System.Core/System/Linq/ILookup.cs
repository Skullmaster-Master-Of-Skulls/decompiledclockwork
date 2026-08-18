using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000154 RID: 340
	[__DynamicallyInvokable]
	public interface ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>, IEnumerable
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000BFF RID: 3071
		[__DynamicallyInvokable]
		int Count { [__DynamicallyInvokable] get; }

		// Token: 0x17000225 RID: 549
		[__DynamicallyInvokable]
		IEnumerable<TElement> this[TKey key]
		{
			[__DynamicallyInvokable]
			get;
		}

		// Token: 0x06000C01 RID: 3073
		[__DynamicallyInvokable]
		bool Contains(TKey key);
	}
}

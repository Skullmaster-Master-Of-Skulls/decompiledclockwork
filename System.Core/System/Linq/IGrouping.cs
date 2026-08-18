using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000153 RID: 339
	[__DynamicallyInvokable]
	public interface IGrouping<out TKey, out TElement> : IEnumerable<!1>, IEnumerable
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000BFE RID: 3070
		[__DynamicallyInvokable]
		TKey Key { [__DynamicallyInvokable] get; }
	}
}

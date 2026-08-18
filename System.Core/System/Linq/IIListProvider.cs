using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x02000161 RID: 353
	internal interface IIListProvider<TElement> : IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06000C33 RID: 3123
		TElement[] ToArray();

		// Token: 0x06000C34 RID: 3124
		List<TElement> ToList();

		// Token: 0x06000C35 RID: 3125
		int GetCount(bool onlyIfCheap);
	}
}

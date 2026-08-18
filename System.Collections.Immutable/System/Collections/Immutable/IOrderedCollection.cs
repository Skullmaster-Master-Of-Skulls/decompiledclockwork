using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000018 RID: 24
	internal interface IOrderedCollection<out T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000C2 RID: 194
		int Count { get; }

		// Token: 0x17000027 RID: 39
		T this[int index]
		{
			get;
		}
	}
}

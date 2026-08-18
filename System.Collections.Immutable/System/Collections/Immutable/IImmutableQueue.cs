using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000012 RID: 18
	public interface IImmutableQueue<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000087 RID: 135
		bool IsEmpty { get; }

		// Token: 0x06000088 RID: 136
		IImmutableQueue<T> Clear();

		// Token: 0x06000089 RID: 137
		T Peek();

		// Token: 0x0600008A RID: 138
		IImmutableQueue<T> Enqueue(T value);

		// Token: 0x0600008B RID: 139
		IImmutableQueue<T> Dequeue();
	}
}

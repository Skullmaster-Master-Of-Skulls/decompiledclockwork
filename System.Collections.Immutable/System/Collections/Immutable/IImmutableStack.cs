using System;
using System.Collections.Generic;

namespace System.Collections.Immutable
{
	// Token: 0x02000014 RID: 20
	public interface IImmutableStack<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009B RID: 155
		bool IsEmpty { get; }

		// Token: 0x0600009C RID: 156
		IImmutableStack<T> Clear();

		// Token: 0x0600009D RID: 157
		IImmutableStack<T> Push(T value);

		// Token: 0x0600009E RID: 158
		IImmutableStack<T> Pop();

		// Token: 0x0600009F RID: 159
		T Peek();
	}
}

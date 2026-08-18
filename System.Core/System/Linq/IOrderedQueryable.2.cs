using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200014D RID: 333
	[__DynamicallyInvokable]
	public interface IOrderedQueryable<out T> : IQueryable<T>, IEnumerable<!0>, IEnumerable, IQueryable, IOrderedQueryable
	{
	}
}

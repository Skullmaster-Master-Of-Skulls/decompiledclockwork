using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
	// Token: 0x0200014A RID: 330
	[__DynamicallyInvokable]
	public interface IQueryable<out T> : IEnumerable<!0>, IEnumerable, IQueryable
	{
	}
}

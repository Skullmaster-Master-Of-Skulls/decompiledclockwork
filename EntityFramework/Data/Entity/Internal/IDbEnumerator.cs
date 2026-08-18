using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal
{
	// Token: 0x020002A2 RID: 674
	internal interface IDbEnumerator<out T> : IEnumerator<!0>, IEnumerator, IDbAsyncEnumerator<T>, IDbAsyncEnumerator, IDisposable
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060017EE RID: 6126
		T Current { get; }
	}
}

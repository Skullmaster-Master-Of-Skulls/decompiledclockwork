using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x020002A1 RID: 673
	public interface IDbAsyncEnumerator<out T> : IDbAsyncEnumerator, IDisposable
	{
		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060017ED RID: 6125
		T Current { get; }
	}
}

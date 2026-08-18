using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200029D RID: 669
	public interface IDbAsyncEnumerator : IDisposable
	{
		// Token: 0x060017DB RID: 6107
		Task<bool> MoveNextAsync(CancellationToken cancellationToken);

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060017DC RID: 6108
		object Current { get; }
	}
}

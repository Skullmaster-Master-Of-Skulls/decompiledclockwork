using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x020002A7 RID: 679
	internal class LazyAsyncEnumerator<T> : IDbAsyncEnumerator<T>, IDbAsyncEnumerator, IDisposable
	{
		// Token: 0x06001801 RID: 6145 RVA: 0x000793DC File Offset: 0x000775DC
		public LazyAsyncEnumerator(Func<CancellationToken, Task<ObjectResult<T>>> getObjectResultAsync)
		{
			this._getObjectResultAsync = getObjectResultAsync;
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x000793EC File Offset: 0x000775EC
		public T Current
		{
			get
			{
				if (this._objectResultAsyncEnumerator != null)
				{
					return this._objectResultAsyncEnumerator.Current;
				}
				return default(T);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00079416 File Offset: 0x00077616
		object IDbAsyncEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00079423 File Offset: 0x00077623
		public void Dispose()
		{
			if (this._objectResultAsyncEnumerator != null)
			{
				this._objectResultAsyncEnumerator.Dispose();
			}
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00079438 File Offset: 0x00077638
		public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			if (this._objectResultAsyncEnumerator != null)
			{
				return this._objectResultAsyncEnumerator.MoveNextAsync(cancellationToken);
			}
			return this.FirstMoveNextAsync(cancellationToken);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00079628 File Offset: 0x00077828
		private async Task<bool> FirstMoveNextAsync(CancellationToken cancellationToken)
		{
			ObjectResult<T> objectResult = await this._getObjectResultAsync(cancellationToken).WithCurrentCulture<ObjectResult<T>>();
			try
			{
				this._objectResultAsyncEnumerator = ((IDbAsyncEnumerable<T>)objectResult).GetAsyncEnumerator();
			}
			catch
			{
				objectResult.Dispose();
				throw;
			}
			return await this._objectResultAsyncEnumerator.MoveNextAsync(cancellationToken).WithCurrentCulture<bool>();
		}

		// Token: 0x04000868 RID: 2152
		private readonly Func<CancellationToken, Task<ObjectResult<T>>> _getObjectResultAsync;

		// Token: 0x04000869 RID: 2153
		private IDbAsyncEnumerator<T> _objectResultAsyncEnumerator;
	}
}

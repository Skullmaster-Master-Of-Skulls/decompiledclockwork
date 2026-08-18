using System;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x020006B3 RID: 1715
	internal static class IDbAsyncEnumeratorExtensions
	{
		// Token: 0x0600446B RID: 17515 RVA: 0x00143FB6 File Offset: 0x001421B6
		public static Task<bool> MoveNextAsync(this IDbAsyncEnumerator enumerator)
		{
			Check.NotNull<IDbAsyncEnumerator>(enumerator, "enumerator");
			return enumerator.MoveNextAsync(CancellationToken.None);
		}

		// Token: 0x0600446C RID: 17516 RVA: 0x00143FCF File Offset: 0x001421CF
		internal static IDbAsyncEnumerator<TResult> Cast<TResult>(this IDbAsyncEnumerator source)
		{
			return new IDbAsyncEnumeratorExtensions.CastDbAsyncEnumerator<TResult>(source);
		}

		// Token: 0x020006B4 RID: 1716
		private class CastDbAsyncEnumerator<TResult> : IDbAsyncEnumerator<TResult>, IDbAsyncEnumerator, IDisposable
		{
			// Token: 0x0600446D RID: 17517 RVA: 0x00143FD7 File Offset: 0x001421D7
			public CastDbAsyncEnumerator(IDbAsyncEnumerator sourceEnumerator)
			{
				this._underlyingEnumerator = sourceEnumerator;
			}

			// Token: 0x0600446E RID: 17518 RVA: 0x00143FE6 File Offset: 0x001421E6
			public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
			{
				return this._underlyingEnumerator.MoveNextAsync(cancellationToken);
			}

			// Token: 0x17000A4A RID: 2634
			// (get) Token: 0x0600446F RID: 17519 RVA: 0x00143FF4 File Offset: 0x001421F4
			public TResult Current
			{
				get
				{
					return (TResult)((object)this._underlyingEnumerator.Current);
				}
			}

			// Token: 0x17000A4B RID: 2635
			// (get) Token: 0x06004470 RID: 17520 RVA: 0x00144006 File Offset: 0x00142206
			object IDbAsyncEnumerator.Current
			{
				get
				{
					return this._underlyingEnumerator.Current;
				}
			}

			// Token: 0x06004471 RID: 17521 RVA: 0x00144013 File Offset: 0x00142213
			public void Dispose()
			{
				this._underlyingEnumerator.Dispose();
			}

			// Token: 0x04001933 RID: 6451
			private readonly IDbAsyncEnumerator _underlyingEnumerator;
		}
	}
}

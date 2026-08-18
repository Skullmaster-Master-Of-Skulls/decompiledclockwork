using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x020001C4 RID: 452
	internal sealed class CancellationTokenHelper : IDisposable
	{
		// Token: 0x0600172E RID: 5934 RVA: 0x00048E64 File Offset: 0x00047064
		public CancellationTokenHelper(bool canceled)
		{
			if (canceled)
			{
				this._cts.Cancel();
			}
			this._state = (canceled ? 2 : 0);
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x00048E92 File Offset: 0x00047092
		internal bool IsCancellationRequested
		{
			get
			{
				return this._cts.IsCancellationRequested;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x00048E9F File Offset: 0x0004709F
		internal CancellationToken Token
		{
			get
			{
				return this._cts.Token;
			}
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x00048EAC File Offset: 0x000470AC
		public void Cancel()
		{
			if (Interlocked.CompareExchange(ref this._state, 1, 0) == 0)
			{
				ThreadPool.UnsafeQueueUserWorkItem(delegate(object _)
				{
					try
					{
						this._cts.Cancel();
					}
					catch
					{
					}
					finally
					{
						if (Interlocked.CompareExchange(ref this._state, 2, 1) == 3)
						{
							this._cts.Dispose();
							Interlocked.Exchange(ref this._state, 4);
						}
					}
				}, null);
			}
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00048ED0 File Offset: 0x000470D0
		public void Dispose()
		{
			switch (Interlocked.Exchange(ref this._state, 3))
			{
			case 0:
			case 2:
				this._cts.Dispose();
				Interlocked.Exchange(ref this._state, 4);
				return;
			case 1:
			case 3:
				break;
			case 4:
				Interlocked.Exchange(ref this._state, 4);
				break;
			default:
				return;
			}
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00048F2C File Offset: 0x0004712C
		private static CancellationTokenHelper GetStaticDisposedHelper()
		{
			CancellationTokenHelper cancellationTokenHelper = new CancellationTokenHelper(false);
			cancellationTokenHelper.Dispose();
			return cancellationTokenHelper;
		}

		// Token: 0x040016E9 RID: 5865
		private const int STATE_CREATED = 0;

		// Token: 0x040016EA RID: 5866
		private const int STATE_CANCELING = 1;

		// Token: 0x040016EB RID: 5867
		private const int STATE_CANCELED = 2;

		// Token: 0x040016EC RID: 5868
		private const int STATE_DISPOSING = 3;

		// Token: 0x040016ED RID: 5869
		private const int STATE_DISPOSED = 4;

		// Token: 0x040016EE RID: 5870
		internal static readonly CancellationTokenHelper StaticDisposed = CancellationTokenHelper.GetStaticDisposedHelper();

		// Token: 0x040016EF RID: 5871
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();

		// Token: 0x040016F0 RID: 5872
		private int _state;
	}
}

using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000047 RID: 71
	internal abstract class AspNetSynchronizationContextBase : SynchronizationContext
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000554 RID: 1364
		// (set) Token: 0x06000555 RID: 1365
		internal abstract bool AllowAsyncDuringSyncStages { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000556 RID: 1366
		internal abstract bool Enabled { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00007220 File Offset: 0x00005420
		internal Exception Error
		{
			get
			{
				ExceptionDispatchInfo exceptionDispatchInfo = this.ExceptionDispatchInfo;
				if (exceptionDispatchInfo == null)
				{
					return null;
				}
				return exceptionDispatchInfo.SourceException;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000558 RID: 1368
		internal abstract ExceptionDispatchInfo ExceptionDispatchInfo { get; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000559 RID: 1369
		internal abstract int PendingOperationsCount { get; }

		// Token: 0x0600055A RID: 1370
		internal abstract void ClearError();

		// Token: 0x0600055B RID: 1371
		internal abstract void Disable();

		// Token: 0x0600055C RID: 1372
		internal abstract void Enable();

		// Token: 0x0600055D RID: 1373
		internal abstract bool PendingCompletion(WaitCallback callback);

		// Token: 0x0600055E RID: 1374 RVA: 0x00007240 File Offset: 0x00005440
		internal Task WaitForPendingOperationsAsync()
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			WaitCallback waitCallback = delegate(object _)
			{
				Exception error = this.Error;
				if (error != null)
				{
					this.ClearError();
					tcs.TrySetException(error);
					return;
				}
				tcs.TrySetResult(null);
			};
			if (!this.PendingCompletion(waitCallback))
			{
				waitCallback(null);
			}
			return tcs.Task;
		}

		// Token: 0x0600055F RID: 1375
		internal abstract void SetSyncCaller();

		// Token: 0x06000560 RID: 1376
		internal abstract void ResetSyncCaller();

		// Token: 0x06000561 RID: 1377
		internal abstract void AssociateWithCurrentThread();

		// Token: 0x06000562 RID: 1378
		internal abstract void DisassociateFromCurrentThread();

		// Token: 0x06000563 RID: 1379 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void AllowVoidAsyncOperations()
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00006164 File Offset: 0x00004364
		internal virtual void ProhibitVoidAsyncOperations()
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0000728D File Offset: 0x0000548D
		internal IDisposable AllowVoidAsyncOperationsBlock()
		{
			if (this._allowAsyncOperationsBlockDisposable == null)
			{
				this._allowAsyncOperationsBlockDisposable = new AspNetSynchronizationContextBase.AllowAsyncOperationsBlockDisposable(this);
			}
			this.AllowVoidAsyncOperations();
			return this._allowAsyncOperationsBlockDisposable;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000072AF File Offset: 0x000054AF
		internal IDisposable AcquireThreadLock()
		{
			this.AssociateWithCurrentThread();
			return new DisposableAction(new Action(this.DisassociateFromCurrentThread));
		}

		// Token: 0x0400013D RID: 317
		private AspNetSynchronizationContextBase.AllowAsyncOperationsBlockDisposable _allowAsyncOperationsBlockDisposable;

		// Token: 0x020008BA RID: 2234
		private sealed class AllowAsyncOperationsBlockDisposable : IDisposable
		{
			// Token: 0x060067B7 RID: 26551 RVA: 0x00170589 File Offset: 0x0016E789
			public AllowAsyncOperationsBlockDisposable(AspNetSynchronizationContextBase syncContext)
			{
				this._syncContext = syncContext;
			}

			// Token: 0x060067B8 RID: 26552 RVA: 0x00170598 File Offset: 0x0016E798
			public void Dispose()
			{
				this._syncContext.ProhibitVoidAsyncOperations();
			}

			// Token: 0x040035E3 RID: 13795
			private readonly AspNetSynchronizationContextBase _syncContext;
		}
	}
}

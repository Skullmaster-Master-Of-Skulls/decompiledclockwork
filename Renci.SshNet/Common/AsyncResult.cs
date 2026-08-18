using System;
using System.Threading;

namespace Renci.SshNet.Common
{
	// Token: 0x020000E4 RID: 228
	public abstract class AsyncResult : IAsyncResult
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00020422 File Offset: 0x0001E622
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x0002042A File Offset: 0x0001E62A
		public bool EndInvokeCalled { get; private set; }

		// Token: 0x060009AD RID: 2477 RVA: 0x00020433 File Offset: 0x0001E633
		protected AsyncResult(AsyncCallback asyncCallback, object state)
		{
			this._asyncCallback = asyncCallback;
			this._asyncState = state;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0002044C File Offset: 0x0001E64C
		public void SetAsCompleted(Exception exception, bool completedSynchronously)
		{
			this._exception = exception;
			if (Interlocked.Exchange(ref this._completedState, completedSynchronously ? 1 : 2) != 0)
			{
				throw new InvalidOperationException("You can set a result only once");
			}
			if (this._asyncWaitHandle != null)
			{
				this._asyncWaitHandle.Set();
			}
			if (this._asyncCallback != null)
			{
				this._asyncCallback(this);
			}
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x000204A7 File Offset: 0x0001E6A7
		public void EndInvoke()
		{
			if (!this.IsCompleted)
			{
				this.AsyncWaitHandle.WaitOne();
				this.AsyncWaitHandle.Dispose();
				this._asyncWaitHandle = null;
			}
			this.EndInvokeCalled = true;
			if (this._exception != null)
			{
				throw this._exception;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x000204E5 File Offset: 0x0001E6E5
		public object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000204ED File Offset: 0x0001E6ED
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedState == 1;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000204F8 File Offset: 0x0001E6F8
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this._asyncWaitHandle == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEvent manualResetEvent = new ManualResetEvent(isCompleted);
					if (Interlocked.CompareExchange<ManualResetEvent>(ref this._asyncWaitHandle, manualResetEvent, null) != null)
					{
						manualResetEvent.Dispose();
					}
					else if (!isCompleted && this.IsCompleted)
					{
						this._asyncWaitHandle.Set();
					}
				}
				return this._asyncWaitHandle;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0002054F File Offset: 0x0001E74F
		public bool IsCompleted
		{
			get
			{
				return this._completedState != 0;
			}
		}

		// Token: 0x040003C5 RID: 965
		private readonly AsyncCallback _asyncCallback;

		// Token: 0x040003C6 RID: 966
		private readonly object _asyncState;

		// Token: 0x040003C7 RID: 967
		private const int StatePending = 0;

		// Token: 0x040003C8 RID: 968
		private const int StateCompletedSynchronously = 1;

		// Token: 0x040003C9 RID: 969
		private const int StateCompletedAsynchronously = 2;

		// Token: 0x040003CA RID: 970
		private int _completedState;

		// Token: 0x040003CB RID: 971
		private ManualResetEvent _asyncWaitHandle;

		// Token: 0x040003CC RID: 972
		private Exception _exception;
	}
}

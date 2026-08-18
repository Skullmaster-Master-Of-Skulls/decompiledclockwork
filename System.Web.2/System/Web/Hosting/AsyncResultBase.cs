using System;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System.Web.Hosting
{
	// Token: 0x020007A8 RID: 1960
	internal abstract class AsyncResultBase : IAsyncResult
	{
		// Token: 0x06005D0E RID: 23822 RVA: 0x00142E90 File Offset: 0x00141090
		protected AsyncResultBase(AsyncCallback cb, object state)
		{
			this._callback = cb;
			this._asyncState = state;
			this._mre = new ManualResetEventSlim();
		}

		// Token: 0x06005D0F RID: 23823
		internal abstract void Complete(int bytesCompleted, int hresult, IntPtr pAsyncCompletionContext, bool synchronous);

		// Token: 0x06005D10 RID: 23824 RVA: 0x00142EB4 File Offset: 0x001410B4
		protected void Complete(int hresult, bool synchronous)
		{
			if (Volatile.Read<Thread>(ref this._threadWhichStartedOperation) == Thread.CurrentThread)
			{
				synchronous = true;
			}
			this._hresult = hresult;
			this._completed = true;
			this._completedSynchronously = synchronous;
			this._mre.Set();
			if (this._callback != null)
			{
				this._callback(this);
			}
		}

		// Token: 0x06005D11 RID: 23825 RVA: 0x00142F10 File Offset: 0x00141110
		internal void MarkCallToBeginMethodStarted()
		{
			Thread thread = Interlocked.CompareExchange<Thread>(ref this._threadWhichStartedOperation, Thread.CurrentThread, null);
		}

		// Token: 0x06005D12 RID: 23826 RVA: 0x00142F30 File Offset: 0x00141130
		internal void MarkCallToBeginMethodCompleted()
		{
			Thread thread = Interlocked.Exchange<Thread>(ref this._threadWhichStartedOperation, null);
		}

		// Token: 0x06005D13 RID: 23827 RVA: 0x00142F4C File Offset: 0x0014114C
		internal void ReleaseWaitHandleWhenSignaled()
		{
			try
			{
				this._mre.Wait();
			}
			finally
			{
				this._mre.Dispose();
			}
		}

		// Token: 0x06005D14 RID: 23828 RVA: 0x00142F84 File Offset: 0x00141184
		internal void SetError(Exception error)
		{
			this._error = ExceptionDispatchInfo.Capture(error);
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x06005D15 RID: 23829 RVA: 0x00142F94 File Offset: 0x00141194
		// (set) Token: 0x06005D16 RID: 23830 RVA: 0x00142F9E File Offset: 0x0014119E
		internal int HResult
		{
			get
			{
				return this._hresult;
			}
			set
			{
				this._hresult = value;
			}
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06005D17 RID: 23831 RVA: 0x00142FA9 File Offset: 0x001411A9
		internal ExceptionDispatchInfo Error
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06005D18 RID: 23832 RVA: 0x00142FB3 File Offset: 0x001411B3
		public bool IsCompleted
		{
			get
			{
				return this._completed;
			}
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x06005D19 RID: 23833 RVA: 0x00142FBD File Offset: 0x001411BD
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x06005D1A RID: 23834 RVA: 0x00142FC7 File Offset: 0x001411C7
		public object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x06005D1B RID: 23835 RVA: 0x00142FCF File Offset: 0x001411CF
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return this._mre.WaitHandle;
			}
		}

		// Token: 0x040030F1 RID: 12529
		private ManualResetEventSlim _mre;

		// Token: 0x040030F2 RID: 12530
		private AsyncCallback _callback;

		// Token: 0x040030F3 RID: 12531
		private object _asyncState;

		// Token: 0x040030F4 RID: 12532
		private volatile bool _completed;

		// Token: 0x040030F5 RID: 12533
		private volatile bool _completedSynchronously;

		// Token: 0x040030F6 RID: 12534
		private volatile int _hresult;

		// Token: 0x040030F7 RID: 12535
		private Thread _threadWhichStartedOperation;

		// Token: 0x040030F8 RID: 12536
		private volatile ExceptionDispatchInfo _error;
	}
}

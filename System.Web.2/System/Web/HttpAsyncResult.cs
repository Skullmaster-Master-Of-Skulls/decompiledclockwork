using System;
using System.Threading;

namespace System.Web
{
	// Token: 0x0200007E RID: 126
	internal class HttpAsyncResult : IAsyncResult
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00010B10 File Offset: 0x0000ED10
		internal HttpAsyncResult(AsyncCallback cb, object state)
		{
			this._callback = cb;
			this._asyncState = state;
			this._status = RequestNotificationStatus.Continue;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00010B30 File Offset: 0x0000ED30
		internal HttpAsyncResult(AsyncCallback cb, object state, bool completed, object result, Exception error)
		{
			this._callback = cb;
			this._asyncState = state;
			this._completed = completed;
			this._completedSynchronously = completed;
			this._result = result;
			this._error = error;
			this._status = RequestNotificationStatus.Continue;
			if (this._completed && this._callback != null)
			{
				this._callback(this);
			}
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00010B92 File Offset: 0x0000ED92
		internal void SetComplete()
		{
			this._completed = true;
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00010B9C File Offset: 0x0000ED9C
		internal void Complete(bool synchronous, object result, Exception error, RequestNotificationStatus status)
		{
			if (Volatile.Read<Thread>(ref this._threadWhichStartedOperation) == Thread.CurrentThread)
			{
				synchronous = true;
			}
			this._completed = true;
			this._completedSynchronously = synchronous;
			this._result = result;
			this._error = error;
			this._status = status;
			if (this._callback != null)
			{
				this._callback(this);
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00010BF6 File Offset: 0x0000EDF6
		internal void Complete(bool synchronous, object result, Exception error)
		{
			this.Complete(synchronous, result, error, RequestNotificationStatus.Continue);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00010C02 File Offset: 0x0000EE02
		internal object End()
		{
			if (this._error != null)
			{
				throw new HttpException(null, this._error);
			}
			return this._result;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00010C20 File Offset: 0x0000EE20
		internal void MarkCallToBeginMethodStarted()
		{
			Thread thread = Interlocked.CompareExchange<Thread>(ref this._threadWhichStartedOperation, Thread.CurrentThread, null);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00010C40 File Offset: 0x0000EE40
		internal void MarkCallToBeginMethodCompleted()
		{
			Thread thread = Interlocked.Exchange<Thread>(ref this._threadWhichStartedOperation, null);
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00010C5A File Offset: 0x0000EE5A
		internal Exception Error
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00010C62 File Offset: 0x0000EE62
		internal RequestNotificationStatus Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00010C6A File Offset: 0x0000EE6A
		public bool IsCompleted
		{
			get
			{
				return this._completed;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00010C72 File Offset: 0x0000EE72
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00010C7A File Offset: 0x0000EE7A
		public object AsyncState
		{
			get
			{
				return this._asyncState;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x0000298D File Offset: 0x00000B8D
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000290 RID: 656
		private AsyncCallback _callback;

		// Token: 0x04000291 RID: 657
		private object _asyncState;

		// Token: 0x04000292 RID: 658
		private bool _completed;

		// Token: 0x04000293 RID: 659
		private bool _completedSynchronously;

		// Token: 0x04000294 RID: 660
		private object _result;

		// Token: 0x04000295 RID: 661
		private Exception _error;

		// Token: 0x04000296 RID: 662
		private Thread _threadWhichStartedOperation;

		// Token: 0x04000297 RID: 663
		private RequestNotificationStatus _status;
	}
}

using System;
using System.Net.Http.Properties;
using System.Threading;
using System.Web.Http;

namespace System.Net.Http.Internal
{
	// Token: 0x0200002A RID: 42
	internal abstract class AsyncResult : IAsyncResult
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00006247 File Offset: 0x00004447
		protected AsyncResult(AsyncCallback callback, object state)
		{
			this._callback = callback;
			this._state = state;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000625D File Offset: 0x0000445D
		public object AsyncState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006265 File Offset: 0x00004465
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00006268 File Offset: 0x00004468
		public bool CompletedSynchronously
		{
			get
			{
				return this._completedSynchronously;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006270 File Offset: 0x00004470
		public bool HasCallback
		{
			get
			{
				return this._callback != null;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000627E File Offset: 0x0000447E
		public bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00006288 File Offset: 0x00004488
		protected void Complete(bool completedSynchronously)
		{
			if (this._isCompleted)
			{
				throw Error.InvalidOperation(Resources.AsyncResult_MultipleCompletes, new object[]
				{
					base.GetType().Name
				});
			}
			this._completedSynchronously = completedSynchronously;
			this._isCompleted = true;
			if (this._callback != null)
			{
				try
				{
					this._callback(this);
				}
				catch (Exception innerException)
				{
					throw Error.InvalidOperation(innerException, Resources.AsyncResult_CallbackThrewException, new object[0]);
				}
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00006308 File Offset: 0x00004508
		protected void Complete(bool completedSynchronously, Exception exception)
		{
			this._exception = exception;
			this.Complete(completedSynchronously);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00006318 File Offset: 0x00004518
		protected static TAsyncResult End<TAsyncResult>(IAsyncResult result) where TAsyncResult : AsyncResult
		{
			if (result == null)
			{
				throw Error.ArgumentNull("result");
			}
			TAsyncResult tasyncResult = result as TAsyncResult;
			if (tasyncResult == null)
			{
				throw Error.Argument("result", Resources.AsyncResult_ResultMismatch, new object[0]);
			}
			if (!tasyncResult._isCompleted)
			{
				tasyncResult.AsyncWaitHandle.WaitOne();
			}
			if (tasyncResult._endCalled)
			{
				throw Error.InvalidOperation(Resources.AsyncResult_MultipleEnds, new object[0]);
			}
			tasyncResult._endCalled = true;
			if (tasyncResult._exception != null)
			{
				throw tasyncResult._exception;
			}
			return tasyncResult;
		}

		// Token: 0x0400005A RID: 90
		private AsyncCallback _callback;

		// Token: 0x0400005B RID: 91
		private object _state;

		// Token: 0x0400005C RID: 92
		private bool _isCompleted;

		// Token: 0x0400005D RID: 93
		private bool _completedSynchronously;

		// Token: 0x0400005E RID: 94
		private bool _endCalled;

		// Token: 0x0400005F RID: 95
		private Exception _exception;
	}
}

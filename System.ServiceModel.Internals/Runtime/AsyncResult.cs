using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200000B RID: 11
	internal abstract class AsyncResult : IAsyncResult
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000269A File Offset: 0x0000089A
		protected AsyncResult(AsyncCallback callback, object state)
		{
			this.callback = callback;
			this.state = state;
			this.thisLock = new object();
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000026BB File Offset: 0x000008BB
		public object AsyncState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026C4 File Offset: 0x000008C4
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.manualResetEvent != null)
				{
					return this.manualResetEvent;
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.manualResetEvent == null)
					{
						this.manualResetEvent = new ManualResetEvent(this.isCompleted);
					}
				}
				return this.manualResetEvent;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000272C File Offset: 0x0000092C
		public bool CompletedSynchronously
		{
			get
			{
				return this.completedSynchronously;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002734 File Offset: 0x00000934
		public bool HasCallback
		{
			get
			{
				return this.callback != null;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000273F File Offset: 0x0000093F
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002747 File Offset: 0x00000947
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000274F File Offset: 0x0000094F
		protected Action<AsyncResult, Exception> OnCompleting { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002758 File Offset: 0x00000958
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002760 File Offset: 0x00000960
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002768 File Offset: 0x00000968
		protected Action<AsyncCallback, IAsyncResult> VirtualCallback { get; set; }

		// Token: 0x06000038 RID: 56 RVA: 0x00002774 File Offset: 0x00000974
		protected void Complete(bool completedSynchronously)
		{
			if (this.isCompleted)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncResultCompletedTwice(base.GetType())));
			}
			this.completedSynchronously = completedSynchronously;
			if (this.OnCompleting != null)
			{
				try
				{
					this.OnCompleting(this, this.exception);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.exception = ex;
				}
			}
			if (completedSynchronously)
			{
				this.isCompleted = true;
			}
			else
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					this.isCompleted = true;
					if (this.manualResetEvent != null)
					{
						this.manualResetEvent.Set();
					}
				}
			}
			if (this.callback != null)
			{
				try
				{
					if (this.VirtualCallback != null)
					{
						this.VirtualCallback(this.callback, this);
					}
					else
					{
						this.callback(this);
					}
				}
				catch (Exception innerException)
				{
					if (Fx.IsFatal(innerException))
					{
						throw;
					}
					throw Fx.Exception.AsError(new CallbackException(InternalSR.AsyncCallbackThrewException, innerException));
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000289C File Offset: 0x00000A9C
		protected void Complete(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			this.Complete(completedSynchronously);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028AC File Offset: 0x00000AAC
		private static void AsyncCompletionWrapperCallback(IAsyncResult result)
		{
			if (result == null)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.InvalidNullAsyncResult));
			}
			if (result.CompletedSynchronously)
			{
				return;
			}
			AsyncResult asyncResult = (AsyncResult)result.AsyncState;
			if (!asyncResult.OnContinueAsyncCompletion(result))
			{
				return;
			}
			AsyncResult.AsyncCompletion nextCompletion = asyncResult.GetNextCompletion();
			if (nextCompletion == null)
			{
				AsyncResult.ThrowInvalidAsyncResult(result);
			}
			bool flag = false;
			Exception ex = null;
			try
			{
				flag = nextCompletion(result);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				flag = true;
				ex = ex2;
			}
			if (flag)
			{
				asyncResult.Complete(false, ex);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002940 File Offset: 0x00000B40
		protected virtual bool OnContinueAsyncCompletion(IAsyncResult result)
		{
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002943 File Offset: 0x00000B43
		protected void SetBeforePrepareAsyncCompletionAction(Action beforePrepareAsyncCompletionAction)
		{
			this.beforePrepareAsyncCompletionAction = beforePrepareAsyncCompletionAction;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000294C File Offset: 0x00000B4C
		protected void SetCheckSyncValidationFunc(Func<IAsyncResult, bool> checkSyncValidationFunc)
		{
			this.checkSyncValidationFunc = checkSyncValidationFunc;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002955 File Offset: 0x00000B55
		protected AsyncCallback PrepareAsyncCompletion(AsyncResult.AsyncCompletion callback)
		{
			if (this.beforePrepareAsyncCompletionAction != null)
			{
				this.beforePrepareAsyncCompletionAction();
			}
			this.nextAsyncCompletion = callback;
			if (AsyncResult.asyncCompletionWrapperCallback == null)
			{
				AsyncResult.asyncCompletionWrapperCallback = Fx.ThunkCallback(new AsyncCallback(AsyncResult.AsyncCompletionWrapperCallback));
			}
			return AsyncResult.asyncCompletionWrapperCallback;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002994 File Offset: 0x00000B94
		protected bool CheckSyncContinue(IAsyncResult result)
		{
			AsyncResult.AsyncCompletion asyncCompletion;
			return this.TryContinueHelper(result, out asyncCompletion);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000029AC File Offset: 0x00000BAC
		protected bool SyncContinue(IAsyncResult result)
		{
			AsyncResult.AsyncCompletion asyncCompletion;
			return this.TryContinueHelper(result, out asyncCompletion) && asyncCompletion(result);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000029D0 File Offset: 0x00000BD0
		private bool TryContinueHelper(IAsyncResult result, out AsyncResult.AsyncCompletion callback)
		{
			if (result == null)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.InvalidNullAsyncResult));
			}
			callback = null;
			if (this.checkSyncValidationFunc != null)
			{
				if (!this.checkSyncValidationFunc(result))
				{
					return false;
				}
			}
			else if (!result.CompletedSynchronously)
			{
				return false;
			}
			callback = this.GetNextCompletion();
			if (callback == null)
			{
				AsyncResult.ThrowInvalidAsyncResult("Only call Check/SyncContinue once per async operation (once per PrepareAsyncCompletion).");
			}
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A34 File Offset: 0x00000C34
		private AsyncResult.AsyncCompletion GetNextCompletion()
		{
			AsyncResult.AsyncCompletion result = this.nextAsyncCompletion;
			this.nextAsyncCompletion = null;
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002A50 File Offset: 0x00000C50
		protected static void ThrowInvalidAsyncResult(IAsyncResult result)
		{
			throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.InvalidAsyncResultImplementation(result.GetType())));
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002A6C File Offset: 0x00000C6C
		protected static void ThrowInvalidAsyncResult(string debugText)
		{
			string invalidAsyncResultImplementationGeneric = InternalSR.InvalidAsyncResultImplementationGeneric;
			throw Fx.Exception.AsError(new InvalidOperationException(invalidAsyncResultImplementationGeneric));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002A94 File Offset: 0x00000C94
		protected static TAsyncResult End<TAsyncResult>(IAsyncResult result) where TAsyncResult : AsyncResult
		{
			if (result == null)
			{
				throw Fx.Exception.ArgumentNull("result");
			}
			TAsyncResult tasyncResult = result as TAsyncResult;
			if (tasyncResult == null)
			{
				throw Fx.Exception.Argument("result", InternalSR.InvalidAsyncResult);
			}
			if (tasyncResult.endCalled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.AsyncResultAlreadyEnded));
			}
			tasyncResult.endCalled = true;
			WaitHandle waitHandle = null;
			object obj = tasyncResult.ThisLock;
			lock (obj)
			{
				if (!tasyncResult.isCompleted)
				{
					waitHandle = tasyncResult.AsyncWaitHandle;
				}
			}
			if (waitHandle != null)
			{
				waitHandle.WaitOne();
			}
			if (tasyncResult.manualResetEvent != null)
			{
				tasyncResult.manualResetEvent.Close();
			}
			if (tasyncResult.exception != null)
			{
				throw Fx.Exception.AsError(tasyncResult.exception);
			}
			return tasyncResult;
		}

		// Token: 0x04000012 RID: 18
		private static AsyncCallback asyncCompletionWrapperCallback;

		// Token: 0x04000013 RID: 19
		private AsyncCallback callback;

		// Token: 0x04000014 RID: 20
		private bool completedSynchronously;

		// Token: 0x04000015 RID: 21
		private bool endCalled;

		// Token: 0x04000016 RID: 22
		private Exception exception;

		// Token: 0x04000017 RID: 23
		private bool isCompleted;

		// Token: 0x04000018 RID: 24
		private AsyncResult.AsyncCompletion nextAsyncCompletion;

		// Token: 0x04000019 RID: 25
		private object state;

		// Token: 0x0400001A RID: 26
		private Action beforePrepareAsyncCompletionAction;

		// Token: 0x0400001B RID: 27
		private Func<IAsyncResult, bool> checkSyncValidationFunc;

		// Token: 0x0400001C RID: 28
		private ManualResetEvent manualResetEvent;

		// Token: 0x0400001D RID: 29
		private object thisLock;

		// Token: 0x0200005B RID: 91
		// (Invoke) Token: 0x06000378 RID: 888
		protected delegate bool AsyncCompletion(IAsyncResult result);
	}
}

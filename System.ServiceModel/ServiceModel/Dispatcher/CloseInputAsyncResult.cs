using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A1 RID: 1441
	internal class CloseInputAsyncResult : AsyncResult
	{
		// Token: 0x060037F2 RID: 14322 RVA: 0x000D74D8 File Offset: 0x000D56D8
		public CloseInputAsyncResult(TimeSpan timeout, AsyncCallback otherCallback, object state, InstanceContext[] instances) : base(otherCallback, state)
		{
			this.timeoutHelper = new TimeoutHelper(timeout);
			this.completedSynchronously = true;
			this.count = instances.Length;
			if (this.count == 0)
			{
				base.Complete(true);
				return;
			}
			int i = 0;
			while (i < instances.Length)
			{
				CloseInputAsyncResult.CallbackState state2 = new CloseInputAsyncResult.CallbackState(this, instances[i]);
				IAsyncResult asyncResult;
				try
				{
					asyncResult = instances[i].BeginCloseInput(this.timeoutHelper.RemainingTime(), CloseInputAsyncResult.nestedCallback, state2);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.Decrement(true, ex);
					goto IL_8F;
				}
				goto IL_76;
				IL_8F:
				i++;
				continue;
				IL_76:
				if (asyncResult.CompletedSynchronously)
				{
					instances[i].EndCloseInput(asyncResult);
					this.Decrement(true);
					goto IL_8F;
				}
				goto IL_8F;
			}
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000D7590 File Offset: 0x000D5790
		private static void Callback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			CloseInputAsyncResult.CallbackState callbackState = (CloseInputAsyncResult.CallbackState)result.AsyncState;
			try
			{
				callbackState.Instance.EndCloseInput(result);
				callbackState.Result.Decrement(false);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				callbackState.Result.Decrement(false, ex);
			}
		}

		// Token: 0x060037F4 RID: 14324 RVA: 0x000D75F8 File Offset: 0x000D57F8
		private void Decrement(bool completedSynchronously)
		{
			if (!completedSynchronously)
			{
				this.completedSynchronously = false;
			}
			if (Interlocked.Decrement(ref this.count) == 0)
			{
				if (this.exception != null)
				{
					base.Complete(this.completedSynchronously, this.exception);
					return;
				}
				base.Complete(this.completedSynchronously);
			}
		}

		// Token: 0x060037F5 RID: 14325 RVA: 0x000D7638 File Offset: 0x000D5838
		private void Decrement(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			this.Decrement(completedSynchronously);
		}

		// Token: 0x060037F6 RID: 14326 RVA: 0x000D7648 File Offset: 0x000D5848
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<CloseInputAsyncResult>(result);
		}

		// Token: 0x0400296C RID: 10604
		private bool completedSynchronously;

		// Token: 0x0400296D RID: 10605
		private Exception exception;

		// Token: 0x0400296E RID: 10606
		private static AsyncCallback nestedCallback = Fx.ThunkCallback(new AsyncCallback(CloseInputAsyncResult.Callback));

		// Token: 0x0400296F RID: 10607
		private int count;

		// Token: 0x04002970 RID: 10608
		private TimeoutHelper timeoutHelper;

		// Token: 0x02000CA6 RID: 3238
		private class CallbackState
		{
			// Token: 0x0600792F RID: 31023 RVA: 0x001C46CF File Offset: 0x001C28CF
			public CallbackState(CloseInputAsyncResult result, InstanceContext instance)
			{
				this.result = result;
				this.instance = instance;
			}

			// Token: 0x17001B84 RID: 7044
			// (get) Token: 0x06007930 RID: 31024 RVA: 0x001C46E5 File Offset: 0x001C28E5
			public InstanceContext Instance
			{
				get
				{
					return this.instance;
				}
			}

			// Token: 0x17001B85 RID: 7045
			// (get) Token: 0x06007931 RID: 31025 RVA: 0x001C46ED File Offset: 0x001C28ED
			public CloseInputAsyncResult Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x04004503 RID: 17667
			private InstanceContext instance;

			// Token: 0x04004504 RID: 17668
			private CloseInputAsyncResult result;
		}
	}
}

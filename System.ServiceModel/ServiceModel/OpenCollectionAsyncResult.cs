using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x02000035 RID: 53
	internal class OpenCollectionAsyncResult : AsyncResult
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00008EFC File Offset: 0x000070FC
		public OpenCollectionAsyncResult(TimeSpan timeout, AsyncCallback otherCallback, object state, IList<ICommunicationObject> collection) : base(otherCallback, state)
		{
			this.timeoutHelper = new TimeoutHelper(timeout);
			this.completedSynchronously = true;
			this.count = collection.Count;
			if (this.count == 0)
			{
				base.Complete(true);
				return;
			}
			for (int i = 0; i < collection.Count; i++)
			{
				if (this.exception != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.exception);
				}
				OpenCollectionAsyncResult.CallbackState state2 = new OpenCollectionAsyncResult.CallbackState(this, collection[i]);
				IAsyncResult asyncResult = collection[i].BeginOpen(this.timeoutHelper.RemainingTime(), OpenCollectionAsyncResult.nestedCallback, state2);
				if (asyncResult.CompletedSynchronously)
				{
					collection[i].EndOpen(asyncResult);
					this.Decrement(true);
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008FB8 File Offset: 0x000071B8
		private static void Callback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			OpenCollectionAsyncResult.CallbackState callbackState = (OpenCollectionAsyncResult.CallbackState)result.AsyncState;
			try
			{
				callbackState.Instance.EndOpen(result);
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

		// Token: 0x060001C3 RID: 451 RVA: 0x00009020 File Offset: 0x00007220
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

		// Token: 0x060001C4 RID: 452 RVA: 0x00009060 File Offset: 0x00007260
		private void Decrement(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			this.Decrement(completedSynchronously);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009070 File Offset: 0x00007270
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<OpenCollectionAsyncResult>(result);
		}

		// Token: 0x040001A6 RID: 422
		private bool completedSynchronously;

		// Token: 0x040001A7 RID: 423
		private Exception exception;

		// Token: 0x040001A8 RID: 424
		private static AsyncCallback nestedCallback = Fx.ThunkCallback(new AsyncCallback(OpenCollectionAsyncResult.Callback));

		// Token: 0x040001A9 RID: 425
		private int count;

		// Token: 0x040001AA RID: 426
		private TimeoutHelper timeoutHelper;

		// Token: 0x02000ACB RID: 2763
		private class CallbackState
		{
			// Token: 0x06006E3F RID: 28223 RVA: 0x0019BA07 File Offset: 0x00199C07
			public CallbackState(OpenCollectionAsyncResult result, ICommunicationObject instance)
			{
				this.result = result;
				this.instance = instance;
			}

			// Token: 0x170019B5 RID: 6581
			// (get) Token: 0x06006E40 RID: 28224 RVA: 0x0019BA1D File Offset: 0x00199C1D
			public ICommunicationObject Instance
			{
				get
				{
					return this.instance;
				}
			}

			// Token: 0x170019B6 RID: 6582
			// (get) Token: 0x06006E41 RID: 28225 RVA: 0x0019BA25 File Offset: 0x00199C25
			public OpenCollectionAsyncResult Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x04003F06 RID: 16134
			private ICommunicationObject instance;

			// Token: 0x04003F07 RID: 16135
			private OpenCollectionAsyncResult result;
		}
	}
}

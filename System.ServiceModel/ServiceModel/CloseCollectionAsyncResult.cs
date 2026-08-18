using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x02000034 RID: 52
	internal class CloseCollectionAsyncResult : AsyncResult
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00008D3C File Offset: 0x00006F3C
		public CloseCollectionAsyncResult(TimeSpan timeout, AsyncCallback otherCallback, object state, IList<ICommunicationObject> collection) : base(otherCallback, state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.completedSynchronously = true;
			this.count = collection.Count;
			if (this.count == 0)
			{
				base.Complete(true);
				return;
			}
			int i = 0;
			while (i < collection.Count)
			{
				CloseCollectionAsyncResult.CallbackState state2 = new CloseCollectionAsyncResult.CallbackState(this, collection[i]);
				IAsyncResult asyncResult;
				try
				{
					asyncResult = collection[i].BeginClose(timeoutHelper.RemainingTime(), CloseCollectionAsyncResult.nestedCallback, state2);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.Decrement(true, ex);
					collection[i].Abort();
					goto IL_A0;
				}
				goto IL_89;
				IL_A0:
				i++;
				continue;
				IL_89:
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteClose(collection[i], asyncResult);
					goto IL_A0;
				}
				goto IL_A0;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008E08 File Offset: 0x00007008
		private void CompleteClose(ICommunicationObject communicationObject, IAsyncResult result)
		{
			Exception ex = null;
			try
			{
				communicationObject.EndClose(result);
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
				communicationObject.Abort();
			}
			this.Decrement(result.CompletedSynchronously, ex);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008E54 File Offset: 0x00007054
		private static void Callback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			CloseCollectionAsyncResult.CallbackState callbackState = (CloseCollectionAsyncResult.CallbackState)result.AsyncState;
			callbackState.Result.CompleteClose(callbackState.Instance, result);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00008E88 File Offset: 0x00007088
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

		// Token: 0x060001BE RID: 446 RVA: 0x00008EC8 File Offset: 0x000070C8
		private void Decrement(bool completedSynchronously, Exception exception)
		{
			this.exception = exception;
			this.Decrement(completedSynchronously);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008ED8 File Offset: 0x000070D8
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<CloseCollectionAsyncResult>(result);
		}

		// Token: 0x040001A2 RID: 418
		private bool completedSynchronously;

		// Token: 0x040001A3 RID: 419
		private Exception exception;

		// Token: 0x040001A4 RID: 420
		private static AsyncCallback nestedCallback = Fx.ThunkCallback(new AsyncCallback(CloseCollectionAsyncResult.Callback));

		// Token: 0x040001A5 RID: 421
		private int count;

		// Token: 0x02000ACA RID: 2762
		private class CallbackState
		{
			// Token: 0x06006E3C RID: 28220 RVA: 0x0019B9E1 File Offset: 0x00199BE1
			public CallbackState(CloseCollectionAsyncResult result, ICommunicationObject instance)
			{
				this.result = result;
				this.instance = instance;
			}

			// Token: 0x170019B3 RID: 6579
			// (get) Token: 0x06006E3D RID: 28221 RVA: 0x0019B9F7 File Offset: 0x00199BF7
			public ICommunicationObject Instance
			{
				get
				{
					return this.instance;
				}
			}

			// Token: 0x170019B4 RID: 6580
			// (get) Token: 0x06006E3E RID: 28222 RVA: 0x0019B9FF File Offset: 0x00199BFF
			public CloseCollectionAsyncResult Result
			{
				get
				{
					return this.result;
				}
			}

			// Token: 0x04003F04 RID: 16132
			private ICommunicationObject instance;

			// Token: 0x04003F05 RID: 16133
			private CloseCollectionAsyncResult result;
		}
	}
}

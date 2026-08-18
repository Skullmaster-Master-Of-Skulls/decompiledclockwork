using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000718 RID: 1816
	internal class ChainedAsyncResult : AsyncResult
	{
		// Token: 0x060044ED RID: 17645 RVA: 0x00102B40 File Offset: 0x00100D40
		protected ChainedAsyncResult(TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.timeoutHelper = new TimeoutHelper(timeout);
		}

		// Token: 0x060044EE RID: 17646 RVA: 0x00102B56 File Offset: 0x00100D56
		public ChainedAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, ChainedBeginHandler begin1, ChainedEndHandler end1, ChainedBeginHandler begin2, ChainedEndHandler end2) : base(callback, state)
		{
			this.timeoutHelper = new TimeoutHelper(timeout);
			this.Begin(begin1, end1, begin2, end2);
		}

		// Token: 0x060044EF RID: 17647 RVA: 0x00102B7C File Offset: 0x00100D7C
		protected void Begin(ChainedBeginHandler begin1, ChainedEndHandler end1, ChainedBeginHandler begin2, ChainedEndHandler end2)
		{
			this.end1 = end1;
			this.begin2 = begin2;
			this.end2 = end2;
			IAsyncResult asyncResult = begin1(this.timeoutHelper.RemainingTime(), ChainedAsyncResult.begin1Callback, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			if (this.Begin1Completed(asyncResult))
			{
				base.Complete(true);
			}
		}

		// Token: 0x060044F0 RID: 17648 RVA: 0x00102BD0 File Offset: 0x00100DD0
		private static void Begin1Callback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ChainedAsyncResult chainedAsyncResult = (ChainedAsyncResult)result.AsyncState;
			bool flag = false;
			Exception exception = null;
			try
			{
				flag = chainedAsyncResult.Begin1Completed(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				flag = true;
				exception = ex;
			}
			if (flag)
			{
				chainedAsyncResult.Complete(false, exception);
			}
		}

		// Token: 0x060044F1 RID: 17649 RVA: 0x00102C2C File Offset: 0x00100E2C
		private bool Begin1Completed(IAsyncResult result)
		{
			this.end1(result);
			result = this.begin2(this.timeoutHelper.RemainingTime(), ChainedAsyncResult.begin2Callback, this);
			if (!result.CompletedSynchronously)
			{
				return false;
			}
			this.end2(result);
			return true;
		}

		// Token: 0x060044F2 RID: 17650 RVA: 0x00102C7C File Offset: 0x00100E7C
		private static void Begin2Callback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ChainedAsyncResult chainedAsyncResult = (ChainedAsyncResult)result.AsyncState;
			Exception exception = null;
			try
			{
				chainedAsyncResult.end2(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			chainedAsyncResult.Complete(false, exception);
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x00102CD8 File Offset: 0x00100ED8
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ChainedAsyncResult>(result);
		}

		// Token: 0x04002D3F RID: 11583
		private ChainedBeginHandler begin2;

		// Token: 0x04002D40 RID: 11584
		private ChainedEndHandler end1;

		// Token: 0x04002D41 RID: 11585
		private ChainedEndHandler end2;

		// Token: 0x04002D42 RID: 11586
		private TimeoutHelper timeoutHelper;

		// Token: 0x04002D43 RID: 11587
		private static AsyncCallback begin1Callback = Fx.ThunkCallback(new AsyncCallback(ChainedAsyncResult.Begin1Callback));

		// Token: 0x04002D44 RID: 11588
		private static AsyncCallback begin2Callback = Fx.ThunkCallback(new AsyncCallback(ChainedAsyncResult.Begin2Callback));
	}
}

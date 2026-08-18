using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200093C RID: 2364
	internal class ReliableChannelCloseAsyncResult : AsyncResult
	{
		// Token: 0x06005AD7 RID: 23255 RVA: 0x0014D94C File Offset: 0x0014BB4C
		public ReliableChannelCloseAsyncResult(OperationWithTimeoutBeginCallback[] beginCallbacks, OperationEndCallback[] endCallbacks, IReliableChannelBinder binder, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.binder = binder;
			this.timeoutHelper = new TimeoutHelper(timeout);
			IAsyncResult asyncResult = OperationWithTimeoutComposer.BeginComposeAsyncOperations(this.timeoutHelper.RemainingTime(), beginCallbacks, endCallbacks, ReliableChannelCloseAsyncResult.onComposeAsyncOperationsComplete, this);
			if (asyncResult.CompletedSynchronously && this.CompleteComposeAsyncOperations(asyncResult))
			{
				base.Complete(true);
			}
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x0014D9A8 File Offset: 0x0014BBA8
		private bool CompleteComposeAsyncOperations(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
			result = this.binder.BeginClose(this.timeoutHelper.RemainingTime(), MaskingMode.Handled, ReliableChannelCloseAsyncResult.onBinderCloseComplete, this);
			if (result.CompletedSynchronously)
			{
				this.binder.EndClose(result);
				return true;
			}
			return false;
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x0014D9E6 File Offset: 0x0014BBE6
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<ReliableChannelCloseAsyncResult>(result);
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0014D9F0 File Offset: 0x0014BBF0
		private static void OnBinderCloseComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableChannelCloseAsyncResult reliableChannelCloseAsyncResult = (ReliableChannelCloseAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					reliableChannelCloseAsyncResult.binder.EndClose(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				reliableChannelCloseAsyncResult.Complete(false, exception);
			}
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x0014DA48 File Offset: 0x0014BC48
		private static void OnComposeAsyncOperationsComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableChannelCloseAsyncResult reliableChannelCloseAsyncResult = (ReliableChannelCloseAsyncResult)result.AsyncState;
				bool flag = false;
				Exception ex = null;
				try
				{
					flag = reliableChannelCloseAsyncResult.CompleteComposeAsyncOperations(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (flag || ex != null)
				{
					reliableChannelCloseAsyncResult.Complete(false, ex);
				}
			}
		}

		// Token: 0x040036BC RID: 14012
		private IReliableChannelBinder binder;

		// Token: 0x040036BD RID: 14013
		private static AsyncCallback onBinderCloseComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelCloseAsyncResult.OnBinderCloseComplete));

		// Token: 0x040036BE RID: 14014
		private static AsyncCallback onComposeAsyncOperationsComplete = Fx.ThunkCallback(new AsyncCallback(ReliableChannelCloseAsyncResult.OnComposeAsyncOperationsComplete));

		// Token: 0x040036BF RID: 14015
		private TimeoutHelper timeoutHelper;
	}
}

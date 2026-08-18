using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Security
{
	// Token: 0x02000354 RID: 852
	internal class OperationWithTimeoutAsyncResult : TraceAsyncResult
	{
		// Token: 0x06001F3C RID: 7996 RVA: 0x00074264 File Offset: 0x00072464
		public OperationWithTimeoutAsyncResult(OperationWithTimeoutCallback operationWithTimeout, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.operationWithTimeout = operationWithTimeout;
			this.timeoutHelper = new TimeoutHelper(timeout);
			ActionItem.Schedule(OperationWithTimeoutAsyncResult.scheduledCallback, this);
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00074290 File Offset: 0x00072490
		private static void OnScheduled(object state)
		{
			OperationWithTimeoutAsyncResult operationWithTimeoutAsyncResult = (OperationWithTimeoutAsyncResult)state;
			Exception exception = null;
			try
			{
				using ((operationWithTimeoutAsyncResult.CallbackActivity == null) ? null : ServiceModelActivity.BoundOperation(operationWithTimeoutAsyncResult.CallbackActivity))
				{
					operationWithTimeoutAsyncResult.operationWithTimeout(operationWithTimeoutAsyncResult.timeoutHelper.RemainingTime());
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				exception = ex;
			}
			operationWithTimeoutAsyncResult.Complete(false, exception);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00074314 File Offset: 0x00072514
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<OperationWithTimeoutAsyncResult>(result);
		}

		// Token: 0x04001ED4 RID: 7892
		private static readonly Action<object> scheduledCallback = new Action<object>(OperationWithTimeoutAsyncResult.OnScheduled);

		// Token: 0x04001ED5 RID: 7893
		private TimeoutHelper timeoutHelper;

		// Token: 0x04001ED6 RID: 7894
		private OperationWithTimeoutCallback operationWithTimeout;
	}
}

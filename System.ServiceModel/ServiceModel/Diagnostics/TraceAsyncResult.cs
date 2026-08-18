using System;
using System.Diagnostics;
using System.Runtime;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A93 RID: 2707
	internal abstract class TraceAsyncResult : AsyncResult
	{
		// Token: 0x06006B2D RID: 27437 RVA: 0x0018F6D0 File Offset: 0x0018D8D0
		protected TraceAsyncResult(AsyncCallback callback, object state) : base(callback, state)
		{
			if (TraceUtility.MessageFlowTracingOnly)
			{
				this.CallbackActivity = ServiceModelActivity.CreateLightWeightAsyncActivity(Trace.CorrelationManager.ActivityId);
				base.VirtualCallback = TraceAsyncResult.waitResultCallback;
				return;
			}
			if (DiagnosticUtility.ShouldUseActivity)
			{
				this.CallbackActivity = ServiceModelActivity.Current;
				if (this.CallbackActivity != null)
				{
					base.VirtualCallback = TraceAsyncResult.waitResultCallback;
				}
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06006B2E RID: 27438 RVA: 0x0018F732 File Offset: 0x0018D932
		// (set) Token: 0x06006B2F RID: 27439 RVA: 0x0018F73A File Offset: 0x0018D93A
		public ServiceModelActivity CallbackActivity { get; private set; }

		// Token: 0x06006B30 RID: 27440 RVA: 0x0018F744 File Offset: 0x0018D944
		private static void DoCallback(AsyncCallback callback, IAsyncResult result)
		{
			if (result is TraceAsyncResult)
			{
				TraceAsyncResult traceAsyncResult = result as TraceAsyncResult;
				if (TraceUtility.MessageFlowTracingOnly)
				{
					Trace.CorrelationManager.ActivityId = traceAsyncResult.CallbackActivity.Id;
					traceAsyncResult.CallbackActivity = null;
				}
				using (ServiceModelActivity.BoundOperation(traceAsyncResult.CallbackActivity))
				{
					callback(result);
				}
			}
		}

		// Token: 0x04003CD9 RID: 15577
		private static Action<AsyncCallback, IAsyncResult> waitResultCallback = new Action<AsyncCallback, IAsyncResult>(TraceAsyncResult.DoCallback);
	}
}

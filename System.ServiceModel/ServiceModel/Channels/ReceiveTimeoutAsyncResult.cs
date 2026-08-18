using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A6E RID: 2670
	internal class ReceiveTimeoutAsyncResult : AsyncResult
	{
		// Token: 0x06006955 RID: 26965 RVA: 0x00189276 File Offset: 0x00187476
		internal ReceiveTimeoutAsyncResult(TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
		{
			this.timeoutHelper = new TimeoutHelper(timeout);
		}

		// Token: 0x17001922 RID: 6434
		// (get) Token: 0x06006956 RID: 26966 RVA: 0x0018928C File Offset: 0x0018748C
		internal TimeoutHelper TimeoutHelper
		{
			get
			{
				return this.timeoutHelper;
			}
		}

		// Token: 0x17001923 RID: 6435
		// (get) Token: 0x06006957 RID: 26967 RVA: 0x00189294 File Offset: 0x00187494
		internal AsyncCallback InnerCallback
		{
			get
			{
				if (ReceiveTimeoutAsyncResult.innerCallback == null)
				{
					ReceiveTimeoutAsyncResult.innerCallback = Fx.ThunkCallback(new AsyncCallback(ReceiveTimeoutAsyncResult.Callback));
				}
				return ReceiveTimeoutAsyncResult.innerCallback;
			}
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x06006958 RID: 26968 RVA: 0x001892B8 File Offset: 0x001874B8
		// (set) Token: 0x06006959 RID: 26969 RVA: 0x001892D4 File Offset: 0x001874D4
		internal IAsyncResult InnerResult
		{
			get
			{
				if (this.innerResult == null)
				{
					DiagnosticUtility.FailFast("ReceiveTimeoutAsyncResult.InnerResult: (this.innerResult != null)");
				}
				return this.innerResult;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (this.innerResult == null)
				{
					this.innerResult = value;
					return;
				}
				if (this.innerResult != value)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxAsyncResultsDontMatch0")));
				}
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x0600695A RID: 26970 RVA: 0x00189327 File Offset: 0x00187527
		internal object InnerState
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x0018932C File Offset: 0x0018752C
		private static void Callback(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			ReceiveTimeoutAsyncResult receiveTimeoutAsyncResult = (ReceiveTimeoutAsyncResult)result.AsyncState;
			receiveTimeoutAsyncResult.InnerResult = result;
			receiveTimeoutAsyncResult.Complete(result.CompletedSynchronously);
		}

		// Token: 0x04003C31 RID: 15409
		private TimeoutHelper timeoutHelper;

		// Token: 0x04003C32 RID: 15410
		private IAsyncResult innerResult;

		// Token: 0x04003C33 RID: 15411
		private static AsyncCallback innerCallback = Fx.ThunkCallback(new AsyncCallback(ReceiveTimeoutAsyncResult.Callback));
	}
}

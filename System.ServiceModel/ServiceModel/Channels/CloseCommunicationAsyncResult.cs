using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000757 RID: 1879
	internal class CloseCommunicationAsyncResult : AsyncResult, ICommunicationWaiter, IDisposable
	{
		// Token: 0x060047C9 RID: 18377 RVA: 0x0010A0EC File Offset: 0x001082EC
		public CloseCommunicationAsyncResult(TimeSpan timeout, AsyncCallback callback, object state, object mutex) : base(callback, state)
		{
			this.timeout = timeout;
			this.timeoutHelper = new TimeoutHelper(timeout);
			this.mutex = mutex;
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("SFxCloseTimedOut1", new object[]
				{
					timeout
				})));
			}
			this.timer = new IOThreadTimer(new Action<object>(CloseCommunicationAsyncResult.TimeoutCallback), this, true);
			this.timer.Set(timeout);
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x060047CA RID: 18378 RVA: 0x0010A177 File Offset: 0x00108377
		private object ThisLock
		{
			get
			{
				return this.mutex;
			}
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0010A17F File Offset: 0x0010837F
		public void Dispose()
		{
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x0010A181 File Offset: 0x00108381
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<CloseCommunicationAsyncResult>(result);
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x0010A18C File Offset: 0x0010838C
		public void Signal()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.result != CommunicationWaitResult.Waiting)
				{
					return;
				}
				this.result = CommunicationWaitResult.Succeeded;
			}
			this.timer.Cancel();
			base.Complete(false);
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x0010A1EC File Offset: 0x001083EC
		private void Timeout()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.result != CommunicationWaitResult.Waiting)
				{
					return;
				}
				this.result = CommunicationWaitResult.Expired;
			}
			base.Complete(false, new TimeoutException(SR.GetString("SFxCloseTimedOut1", new object[]
			{
				this.timeout
			})));
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x0010A264 File Offset: 0x00108464
		private static void TimeoutCallback(object state)
		{
			CloseCommunicationAsyncResult closeCommunicationAsyncResult = (CloseCommunicationAsyncResult)state;
			closeCommunicationAsyncResult.Timeout();
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x0010A280 File Offset: 0x00108480
		public CommunicationWaitResult Wait(TimeSpan timeout, bool aborting)
		{
			if (timeout < TimeSpan.Zero)
			{
				return CommunicationWaitResult.Expired;
			}
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.result != CommunicationWaitResult.Waiting)
				{
					return this.result;
				}
				this.result = CommunicationWaitResult.Aborted;
			}
			this.timer.Cancel();
			TimeoutHelper.WaitOne(base.AsyncWaitHandle, timeout);
			base.Complete(false, new ObjectDisposedException(base.GetType().ToString()));
			return this.result;
		}

		// Token: 0x04002DC4 RID: 11716
		private object mutex;

		// Token: 0x04002DC5 RID: 11717
		private CommunicationWaitResult result;

		// Token: 0x04002DC6 RID: 11718
		private IOThreadTimer timer;

		// Token: 0x04002DC7 RID: 11719
		private TimeoutHelper timeoutHelper;

		// Token: 0x04002DC8 RID: 11720
		private TimeSpan timeout;
	}
}

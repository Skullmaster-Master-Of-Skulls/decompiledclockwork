using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000766 RID: 1894
	internal abstract class RequestChannel : ChannelBase, IRequestChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004850 RID: 18512 RVA: 0x0010B7D4 File Offset: 0x001099D4
		protected RequestChannel(ChannelManagerBase channelFactory, EndpointAddress to, Uri via, bool manualAddressing) : base(channelFactory)
		{
			if (!manualAddressing && to == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("to");
			}
			this.manualAddressing = manualAddressing;
			this.to = to;
			this.via = via;
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06004851 RID: 18513 RVA: 0x0010B826 File Offset: 0x00109A26
		protected bool ManualAddressing
		{
			get
			{
				return this.manualAddressing;
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06004852 RID: 18514 RVA: 0x0010B82E File Offset: 0x00109A2E
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06004853 RID: 18515 RVA: 0x0010B836 File Offset: 0x00109A36
		public Uri Via
		{
			get
			{
				return this.via;
			}
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x0010B840 File Offset: 0x00109A40
		protected void AbortPendingRequests()
		{
			IRequestBase[] array = this.CopyPendingRequests(false);
			if (array != null)
			{
				foreach (IRequestBase requestBase in array)
				{
					requestBase.Abort(this);
				}
			}
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x0010B874 File Offset: 0x00109A74
		protected IAsyncResult BeginWaitForPendingRequests(TimeSpan timeout, AsyncCallback callback, object state)
		{
			IRequestBase[] pendingRequests = this.SetupWaitForPendingRequests();
			return new RequestChannel.WaitForPendingRequestsAsyncResult(timeout, this, pendingRequests, callback, state);
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x0010B892 File Offset: 0x00109A92
		protected void EndWaitForPendingRequests(IAsyncResult result)
		{
			RequestChannel.WaitForPendingRequestsAsyncResult.End(result);
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x0010B89C File Offset: 0x00109A9C
		private void FinishClose()
		{
			List<IRequestBase> obj = this.outstandingRequests;
			lock (obj)
			{
				if (!this.closed)
				{
					this.closed = true;
					if (this.closedEvent != null)
					{
						this.closedEvent.Close();
					}
				}
			}
		}

		// Token: 0x06004858 RID: 18520 RVA: 0x0010B8F8 File Offset: 0x00109AF8
		private IRequestBase[] SetupWaitForPendingRequests()
		{
			return this.CopyPendingRequests(true);
		}

		// Token: 0x06004859 RID: 18521 RVA: 0x0010B904 File Offset: 0x00109B04
		protected void WaitForPendingRequests(TimeSpan timeout)
		{
			IRequestBase[] array = this.SetupWaitForPendingRequests();
			if (array != null && !this.closedEvent.WaitOne(timeout, false))
			{
				foreach (IRequestBase requestBase in array)
				{
					requestBase.Abort(this);
				}
			}
			this.FinishClose();
		}

		// Token: 0x0600485A RID: 18522 RVA: 0x0010B94C File Offset: 0x00109B4C
		private IRequestBase[] CopyPendingRequests(bool createEventIfNecessary)
		{
			IRequestBase[] array = null;
			List<IRequestBase> obj = this.outstandingRequests;
			lock (obj)
			{
				if (this.outstandingRequests.Count > 0)
				{
					array = new IRequestBase[this.outstandingRequests.Count];
					this.outstandingRequests.CopyTo(array);
					this.outstandingRequests.Clear();
					if (createEventIfNecessary && this.closedEvent == null)
					{
						this.closedEvent = new ManualResetEvent(false);
					}
				}
			}
			return array;
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x0010B9D8 File Offset: 0x00109BD8
		protected void FaultPendingRequests()
		{
			IRequestBase[] array = this.CopyPendingRequests(false);
			if (array != null)
			{
				foreach (IRequestBase requestBase in array)
				{
					requestBase.Fault(this);
				}
			}
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x0010BA0C File Offset: 0x00109C0C
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IRequestChannel))
			{
				return (T)((object)this);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			return default(T);
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x0010BA55 File Offset: 0x00109C55
		protected override void OnAbort()
		{
			this.AbortPendingRequests();
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x0010BA60 File Offset: 0x00109C60
		private void ReleaseRequest(IRequestBase request)
		{
			if (request != null)
			{
				request.OnReleaseRequest();
			}
			List<IRequestBase> obj = this.outstandingRequests;
			lock (obj)
			{
				this.outstandingRequests.Remove(request);
				if (this.outstandingRequests.Count == 0 && !this.closed && this.closedEvent != null)
				{
					this.closedEvent.Set();
				}
			}
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x0010BADC File Offset: 0x00109CDC
		private void TrackRequest(IRequestBase request)
		{
			List<IRequestBase> obj = this.outstandingRequests;
			lock (obj)
			{
				base.ThrowIfDisposedOrNotOpen();
				this.outstandingRequests.Add(request);
			}
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x0010BB28 File Offset: 0x00109D28
		public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
		{
			return this.BeginRequest(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x0010BB3C File Offset: 0x00109D3C
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowIfDisposedOrNotOpen();
			this.AddHeadersTo(message);
			IAsyncRequest asyncRequest = this.CreateAsyncRequest(message, callback, state);
			this.TrackRequest(asyncRequest);
			bool flag = true;
			try
			{
				asyncRequest.BeginSendRequest(message, timeout);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.ReleaseRequest(asyncRequest);
				}
			}
			return asyncRequest;
		}

		// Token: 0x06004862 RID: 18530
		protected abstract IRequest CreateRequest(Message message);

		// Token: 0x06004863 RID: 18531
		protected abstract IAsyncRequest CreateAsyncRequest(Message message, AsyncCallback callback, object state);

		// Token: 0x06004864 RID: 18532 RVA: 0x0010BBD8 File Offset: 0x00109DD8
		public Message EndRequest(IAsyncResult result)
		{
			if (result == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("result");
			}
			IAsyncRequest asyncRequest = result as IAsyncRequest;
			if (asyncRequest == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("InvalidAsyncResult"));
			}
			Message result2;
			try
			{
				Message message = asyncRequest.End();
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 262165, SR.GetString("TraceCodeRequestChannelReplyReceived"), message);
				}
				result2 = message;
			}
			finally
			{
				this.ReleaseRequest(asyncRequest);
			}
			return result2;
		}

		// Token: 0x06004865 RID: 18533 RVA: 0x0010BC60 File Offset: 0x00109E60
		public Message Request(Message message)
		{
			return this.Request(message, base.DefaultSendTimeout);
		}

		// Token: 0x06004866 RID: 18534 RVA: 0x0010BC70 File Offset: 0x00109E70
		public Message Request(Message message, TimeSpan timeout)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowIfDisposedOrNotOpen();
			this.AddHeadersTo(message);
			IRequest request = this.CreateRequest(message);
			this.TrackRequest(request);
			Message result;
			try
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				TimeSpan timeSpan = timeoutHelper.RemainingTime();
				try
				{
					request.SendRequest(message, timeSpan);
				}
				catch (TimeoutException innerException)
				{
					throw TraceUtility.ThrowHelperError(new TimeoutException(SR.GetString("RequestChannelSendTimedOut", new object[]
					{
						timeSpan
					}), innerException), message);
				}
				timeSpan = timeoutHelper.RemainingTime();
				Message message2;
				try
				{
					message2 = request.WaitForReply(timeSpan);
				}
				catch (TimeoutException innerException2)
				{
					throw TraceUtility.ThrowHelperError(new TimeoutException(SR.GetString("RequestChannelWaitForReplyTimedOut", new object[]
					{
						timeSpan
					}), innerException2), message);
				}
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 262165, SR.GetString("TraceCodeRequestChannelReplyReceived"), message2);
				}
				result = message2;
			}
			finally
			{
				this.ReleaseRequest(request);
			}
			return result;
		}

		// Token: 0x06004867 RID: 18535 RVA: 0x0010BDB0 File Offset: 0x00109FB0
		protected virtual void AddHeadersTo(Message message)
		{
			if (!this.manualAddressing && this.to != null)
			{
				this.to.ApplyTo(message);
			}
		}

		// Token: 0x04002DE4 RID: 11748
		private bool manualAddressing;

		// Token: 0x04002DE5 RID: 11749
		private List<IRequestBase> outstandingRequests = new List<IRequestBase>();

		// Token: 0x04002DE6 RID: 11750
		private EndpointAddress to;

		// Token: 0x04002DE7 RID: 11751
		private Uri via;

		// Token: 0x04002DE8 RID: 11752
		private ManualResetEvent closedEvent;

		// Token: 0x04002DE9 RID: 11753
		private bool closed;

		// Token: 0x02000CE3 RID: 3299
		private class WaitForPendingRequestsAsyncResult : AsyncResult
		{
			// Token: 0x06007A21 RID: 31265 RVA: 0x001C74D8 File Offset: 0x001C56D8
			public WaitForPendingRequestsAsyncResult(TimeSpan timeout, RequestChannel requestChannel, IRequestBase[] pendingRequests, AsyncCallback callback, object state) : base(callback, state)
			{
				this.requestChannel = requestChannel;
				this.pendingRequests = pendingRequests;
				this.timeout = timeout;
				if (this.timeout == TimeSpan.Zero || this.pendingRequests == null)
				{
					this.AbortRequests();
					this.CleanupEvents();
					base.Complete(true);
					return;
				}
				this.waitHandle = ThreadPool.RegisterWaitForSingleObject(this.requestChannel.closedEvent, RequestChannel.WaitForPendingRequestsAsyncResult.completeWaitCallBack, this, TimeoutHelper.ToMilliseconds(timeout), true);
			}

			// Token: 0x06007A22 RID: 31266 RVA: 0x001C7558 File Offset: 0x001C5758
			private void AbortRequests()
			{
				if (this.pendingRequests != null)
				{
					foreach (IRequestBase requestBase in this.pendingRequests)
					{
						requestBase.Abort(this.requestChannel);
					}
				}
			}

			// Token: 0x06007A23 RID: 31267 RVA: 0x001C7592 File Offset: 0x001C5792
			private void CleanupEvents()
			{
				if (this.requestChannel.closedEvent != null)
				{
					if (this.waitHandle != null)
					{
						this.waitHandle.Unregister(this.requestChannel.closedEvent);
					}
					this.requestChannel.FinishClose();
				}
			}

			// Token: 0x06007A24 RID: 31268 RVA: 0x001C75CC File Offset: 0x001C57CC
			private static void OnCompleteWaitCallBack(object state, bool timedOut)
			{
				RequestChannel.WaitForPendingRequestsAsyncResult waitForPendingRequestsAsyncResult = (RequestChannel.WaitForPendingRequestsAsyncResult)state;
				Exception exception = null;
				try
				{
					if (timedOut)
					{
						waitForPendingRequestsAsyncResult.AbortRequests();
					}
					waitForPendingRequestsAsyncResult.CleanupEvents();
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				waitForPendingRequestsAsyncResult.Complete(false, exception);
			}

			// Token: 0x06007A25 RID: 31269 RVA: 0x001C761C File Offset: 0x001C581C
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<RequestChannel.WaitForPendingRequestsAsyncResult>(result);
			}

			// Token: 0x040045DC RID: 17884
			private static WaitOrTimerCallback completeWaitCallBack = new WaitOrTimerCallback(RequestChannel.WaitForPendingRequestsAsyncResult.OnCompleteWaitCallBack);

			// Token: 0x040045DD RID: 17885
			private IRequestBase[] pendingRequests;

			// Token: 0x040045DE RID: 17886
			private RequestChannel requestChannel;

			// Token: 0x040045DF RID: 17887
			private TimeSpan timeout;

			// Token: 0x040045E0 RID: 17888
			private RegisteredWaitHandle waitHandle;
		}
	}
}

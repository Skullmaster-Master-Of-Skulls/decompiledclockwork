using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000764 RID: 1892
	internal class ReplyChannel : InputQueueChannel<RequestContext>, IReplyChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004838 RID: 18488 RVA: 0x0010B459 File Offset: 0x00109659
		public ReplyChannel(ChannelManagerBase channelManager, EndpointAddress localAddress) : base(channelManager)
		{
			this.localAddress = localAddress;
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06004839 RID: 18489 RVA: 0x0010B469 File Offset: 0x00109669
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x0010B474 File Offset: 0x00109674
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IReplyChannel))
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

		// Token: 0x0600483B RID: 18491 RVA: 0x0010B4BD File Offset: 0x001096BD
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x0010B4C6 File Offset: 0x001096C6
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x0010B4CE File Offset: 0x001096CE
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x0010B4D0 File Offset: 0x001096D0
		internal static RequestContext HelpReceiveRequest(IReplyChannel channel, TimeSpan timeout)
		{
			RequestContext result;
			if (channel.TryReceiveRequest(timeout, out result))
			{
				return result;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ReplyChannel.CreateReceiveRequestTimedOutException(channel, timeout));
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x0010B4FB File Offset: 0x001096FB
		internal static IAsyncResult HelpBeginReceiveRequest(IReplyChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReplyChannel.HelpReceiveRequestAsyncResult(channel, timeout, callback, state);
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x0010B506 File Offset: 0x00109706
		internal static RequestContext HelpEndReceiveRequest(IAsyncResult result)
		{
			return ReplyChannel.HelpReceiveRequestAsyncResult.End(result);
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x0010B510 File Offset: 0x00109710
		private static Exception CreateReceiveRequestTimedOutException(IReplyChannel channel, TimeSpan timeout)
		{
			if (channel.LocalAddress != null)
			{
				return new TimeoutException(SR.GetString("ReceiveRequestTimedOut", new object[]
				{
					channel.LocalAddress.Uri.AbsoluteUri,
					timeout
				}));
			}
			return new TimeoutException(SR.GetString("ReceiveRequestTimedOutNoLocalAddress", new object[]
			{
				timeout
			}));
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x0010B57B File Offset: 0x0010977B
		public RequestContext ReceiveRequest()
		{
			return this.ReceiveRequest(base.DefaultReceiveTimeout);
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x0010B58C File Offset: 0x0010978C
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return ReplyChannel.HelpReceiveRequest(this, timeout);
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x0010B5D8 File Offset: 0x001097D8
		public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
		{
			return this.BeginReceiveRequest(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x0010B5E8 File Offset: 0x001097E8
		public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return ReplyChannel.HelpBeginReceiveRequest(this, timeout, callback, state);
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x0010B636 File Offset: 0x00109836
		public RequestContext EndReceiveRequest(IAsyncResult result)
		{
			return ReplyChannel.HelpEndReceiveRequest(result);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x0010B640 File Offset: 0x00109840
		public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.Dequeue(timeout, out context);
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x0010B690 File Offset: 0x00109890
		public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.BeginDequeue(timeout, callback, state);
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x0010B6DE File Offset: 0x001098DE
		public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context)
		{
			return base.EndDequeue(result, out context);
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x0010B6E8 File Offset: 0x001098E8
		public bool WaitForRequest(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.WaitForItem(timeout);
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0010B734 File Offset: 0x00109934
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRange0")));
			}
			base.ThrowPending();
			return base.BeginWaitForItem(timeout, callback, state);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0010B782 File Offset: 0x00109982
		public bool EndWaitForRequest(IAsyncResult result)
		{
			return base.EndWaitForItem(result);
		}

		// Token: 0x04002DE3 RID: 11747
		private EndpointAddress localAddress;

		// Token: 0x02000CE2 RID: 3298
		private class HelpReceiveRequestAsyncResult : AsyncResult
		{
			// Token: 0x06007A1C RID: 31260 RVA: 0x001C73D4 File Offset: 0x001C55D4
			public HelpReceiveRequestAsyncResult(IReplyChannel channel, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channel = channel;
				this.timeout = timeout;
				IAsyncResult asyncResult = channel.BeginTryReceiveRequest(timeout, ReplyChannel.HelpReceiveRequestAsyncResult.onReceiveRequest, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				this.HandleReceiveRequestComplete(asyncResult);
				base.Complete(true);
			}

			// Token: 0x06007A1D RID: 31261 RVA: 0x001C7420 File Offset: 0x001C5620
			public static RequestContext End(IAsyncResult result)
			{
				ReplyChannel.HelpReceiveRequestAsyncResult helpReceiveRequestAsyncResult = AsyncResult.End<ReplyChannel.HelpReceiveRequestAsyncResult>(result);
				return helpReceiveRequestAsyncResult.requestContext;
			}

			// Token: 0x06007A1E RID: 31262 RVA: 0x001C743A File Offset: 0x001C563A
			private void HandleReceiveRequestComplete(IAsyncResult result)
			{
				if (!this.channel.EndTryReceiveRequest(result, out this.requestContext))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ReplyChannel.CreateReceiveRequestTimedOutException(this.channel, this.timeout));
				}
			}

			// Token: 0x06007A1F RID: 31263 RVA: 0x001C746C File Offset: 0x001C566C
			private static void OnReceiveRequest(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ReplyChannel.HelpReceiveRequestAsyncResult helpReceiveRequestAsyncResult = (ReplyChannel.HelpReceiveRequestAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					helpReceiveRequestAsyncResult.HandleReceiveRequestComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				helpReceiveRequestAsyncResult.Complete(false, exception);
			}

			// Token: 0x040045D8 RID: 17880
			private IReplyChannel channel;

			// Token: 0x040045D9 RID: 17881
			private TimeSpan timeout;

			// Token: 0x040045DA RID: 17882
			private static AsyncCallback onReceiveRequest = Fx.ThunkCallback(new AsyncCallback(ReplyChannel.HelpReceiveRequestAsyncResult.OnReceiveRequest));

			// Token: 0x040045DB RID: 17883
			private RequestContext requestContext;
		}
	}
}

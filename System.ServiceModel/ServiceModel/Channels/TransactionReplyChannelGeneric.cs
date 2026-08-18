using System;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A6B RID: 2667
	internal class TransactionReplyChannelGeneric<TChannel> : TransactionChannel<TChannel>, IReplyChannel, IChannel, ICommunicationObject where TChannel : class, IReplyChannel
	{
		// Token: 0x0600693D RID: 26941 RVA: 0x00188ECF File Offset: 0x001870CF
		public TransactionReplyChannelGeneric(ChannelManagerBase channelManager, TChannel innerChannel) : base(channelManager, innerChannel)
		{
		}

		// Token: 0x1700191F RID: 6431
		// (get) Token: 0x0600693E RID: 26942 RVA: 0x00188ED9 File Offset: 0x001870D9
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x0600693F RID: 26943 RVA: 0x00188EEB File Offset: 0x001870EB
		public RequestContext ReceiveRequest()
		{
			return this.ReceiveRequest(base.DefaultReceiveTimeout);
		}

		// Token: 0x06006940 RID: 26944 RVA: 0x00188EF9 File Offset: 0x001870F9
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			return ReplyChannel.HelpReceiveRequest(this, timeout);
		}

		// Token: 0x06006941 RID: 26945 RVA: 0x00188F02 File Offset: 0x00187102
		public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
		{
			return this.BeginReceiveRequest(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x06006942 RID: 26946 RVA: 0x00188F12 File Offset: 0x00187112
		public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return ReplyChannel.HelpBeginReceiveRequest(this, timeout, callback, state);
		}

		// Token: 0x06006943 RID: 26947 RVA: 0x00188F1D File Offset: 0x0018711D
		public RequestContext EndReceiveRequest(IAsyncResult result)
		{
			return ReplyChannel.HelpEndReceiveRequest(result);
		}

		// Token: 0x06006944 RID: 26948 RVA: 0x00188F28 File Offset: 0x00187128
		public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			ReceiveTimeoutAsyncResult receiveTimeoutAsyncResult = new ReceiveTimeoutAsyncResult(timeout, callback, state);
			receiveTimeoutAsyncResult.InnerResult = base.InnerChannel.BeginTryReceiveRequest(timeout, receiveTimeoutAsyncResult.InnerCallback, receiveTimeoutAsyncResult.InnerState);
			return receiveTimeoutAsyncResult;
		}

		// Token: 0x06006945 RID: 26949 RVA: 0x00188F64 File Offset: 0x00187164
		private RequestContext FinishReceiveRequest(RequestContext innerContext, TimeSpan timeout)
		{
			if (innerContext == null)
			{
				return null;
			}
			try
			{
				this.ReadTransactionDataFromMessage(innerContext.RequestMessage, MessageDirection.Input);
			}
			catch (FaultException ex)
			{
				string action = ex.Action ?? innerContext.RequestMessage.Version.Addressing.DefaultFaultAction;
				Message message = Message.CreateMessage(innerContext.RequestMessage.Version, ex.CreateMessageFault(), action);
				try
				{
					innerContext.Reply(message, timeout);
				}
				finally
				{
					message.Close();
				}
				throw;
			}
			return new TransactionRequestContext(this, this, innerContext, this.DefaultCloseTimeout, base.DefaultSendTimeout);
		}

		// Token: 0x06006946 RID: 26950 RVA: 0x00189004 File Offset: 0x00187204
		public bool EndTryReceiveRequest(IAsyncResult asyncResult, out RequestContext requestContext)
		{
			if (asyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("asyncResult");
			}
			ReceiveTimeoutAsyncResult receiveTimeoutAsyncResult = asyncResult as ReceiveTimeoutAsyncResult;
			if (receiveTimeoutAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("AsyncEndCalledWithAnIAsyncResult")));
			}
			RequestContext innerContext;
			if (base.InnerChannel.EndTryReceiveRequest(receiveTimeoutAsyncResult.InnerResult, out innerContext))
			{
				requestContext = this.FinishReceiveRequest(innerContext, receiveTimeoutAsyncResult.TimeoutHelper.RemainingTime());
				return true;
			}
			requestContext = null;
			return false;
		}

		// Token: 0x06006947 RID: 26951 RVA: 0x00189080 File Offset: 0x00187280
		public bool TryReceiveRequest(TimeSpan timeout, out RequestContext requestContext)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			RequestContext innerContext;
			if (base.InnerChannel.TryReceiveRequest(timeoutHelper.RemainingTime(), out innerContext))
			{
				requestContext = this.FinishReceiveRequest(innerContext, timeoutHelper.RemainingTime());
				return true;
			}
			requestContext = null;
			return false;
		}

		// Token: 0x06006948 RID: 26952 RVA: 0x001890C6 File Offset: 0x001872C6
		public bool WaitForRequest(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForRequest(timeout);
		}

		// Token: 0x06006949 RID: 26953 RVA: 0x001890D9 File Offset: 0x001872D9
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForRequest(timeout, callback, state);
		}

		// Token: 0x0600694A RID: 26954 RVA: 0x001890EE File Offset: 0x001872EE
		public bool EndWaitForRequest(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForRequest(result);
		}
	}
}

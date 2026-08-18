using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007BB RID: 1979
	internal class ContextReplyChannel : LayeredChannel<IReplyChannel>, IReplyChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004ABA RID: 19130 RVA: 0x0011242A File Offset: 0x0011062A
		public ContextReplyChannel(ChannelManagerBase channelManager, IReplyChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism) : base(channelManager, innerChannel)
		{
			this.contextExchangeMechanism = contextExchangeMechanism;
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06004ABB RID: 19131 RVA: 0x0011243B File Offset: 0x0011063B
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x00112448 File Offset: 0x00110648
		public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceiveRequest(timeout, callback, state);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x00112458 File Offset: 0x00110658
		public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceiveRequest(callback, state);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x00112467 File Offset: 0x00110667
		public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceiveRequest(timeout, callback, state);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x00112477 File Offset: 0x00110677
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForRequest(timeout, callback, state);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x00112488 File Offset: 0x00110688
		public RequestContext EndReceiveRequest(IAsyncResult result)
		{
			RequestContext requestContext = base.InnerChannel.EndReceiveRequest(result);
			if (requestContext == null)
			{
				return null;
			}
			return this.CreateContextChannelRequestContext(requestContext);
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x001124B0 File Offset: 0x001106B0
		public bool EndTryReceiveRequest(IAsyncResult result, out RequestContext context)
		{
			context = null;
			RequestContext requestContext;
			if (base.InnerChannel.EndTryReceiveRequest(result, out requestContext))
			{
				if (requestContext != null)
				{
					context = this.CreateContextChannelRequestContext(requestContext);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x001124DF File Offset: 0x001106DF
		public bool EndWaitForRequest(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForRequest(result);
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x001124F0 File Offset: 0x001106F0
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			RequestContext requestContext = base.InnerChannel.ReceiveRequest(timeout);
			if (requestContext == null)
			{
				return null;
			}
			return this.CreateContextChannelRequestContext(requestContext);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x00112518 File Offset: 0x00110718
		public RequestContext ReceiveRequest()
		{
			RequestContext requestContext = base.InnerChannel.ReceiveRequest();
			if (requestContext == null)
			{
				return null;
			}
			return this.CreateContextChannelRequestContext(requestContext);
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x00112540 File Offset: 0x00110740
		public bool TryReceiveRequest(TimeSpan timeout, out RequestContext context)
		{
			RequestContext innerContext;
			if (base.InnerChannel.TryReceiveRequest(timeout, out innerContext))
			{
				context = this.CreateContextChannelRequestContext(innerContext);
				return true;
			}
			context = null;
			return false;
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x0011256C File Offset: 0x0011076C
		public bool WaitForRequest(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForRequest(timeout);
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x0011257C File Offset: 0x0011077C
		private ContextChannelRequestContext CreateContextChannelRequestContext(RequestContext innerContext)
		{
			ServiceContextProtocol serviceContextProtocol = new ServiceContextProtocol(this.contextExchangeMechanism);
			serviceContextProtocol.OnIncomingMessage(innerContext.RequestMessage);
			return new ContextChannelRequestContext(innerContext, serviceContextProtocol, base.DefaultSendTimeout);
		}

		// Token: 0x04002F25 RID: 12069
		private ContextExchangeMechanism contextExchangeMechanism;
	}
}

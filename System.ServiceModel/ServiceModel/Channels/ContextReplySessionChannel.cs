using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007BC RID: 1980
	internal class ContextReplySessionChannel : LayeredChannel<IReplySessionChannel>, IReplySessionChannel, IReplyChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x06004AC8 RID: 19144 RVA: 0x001125AE File Offset: 0x001107AE
		public ContextReplySessionChannel(ChannelManagerBase channelManager, IReplySessionChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism) : base(channelManager, innerChannel)
		{
			this.contextProtocol = new ServiceContextProtocol(contextExchangeMechanism);
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06004AC9 RID: 19145 RVA: 0x001125C4 File Offset: 0x001107C4
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x06004ACA RID: 19146 RVA: 0x001125D1 File Offset: 0x001107D1
		public IInputSession Session
		{
			get
			{
				return base.InnerChannel.Session;
			}
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x001125DE File Offset: 0x001107DE
		public IAsyncResult BeginReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceiveRequest(timeout, callback, state);
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x001125EE File Offset: 0x001107EE
		public IAsyncResult BeginReceiveRequest(AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceiveRequest(callback, state);
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x001125FD File Offset: 0x001107FD
		public IAsyncResult BeginTryReceiveRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceiveRequest(timeout, callback, state);
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x0011260D File Offset: 0x0011080D
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForRequest(timeout, callback, state);
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x00112620 File Offset: 0x00110820
		public RequestContext EndReceiveRequest(IAsyncResult result)
		{
			RequestContext requestContext = base.InnerChannel.EndReceiveRequest(result);
			if (requestContext == null)
			{
				return null;
			}
			return this.CreateContextChannelRequestContext(requestContext);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x00112648 File Offset: 0x00110848
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

		// Token: 0x06004AD1 RID: 19153 RVA: 0x00112677 File Offset: 0x00110877
		public bool EndWaitForRequest(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForRequest(result);
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x00112688 File Offset: 0x00110888
		public RequestContext ReceiveRequest(TimeSpan timeout)
		{
			RequestContext requestContext = base.InnerChannel.ReceiveRequest(timeout);
			if (requestContext == null)
			{
				return null;
			}
			return this.CreateContextChannelRequestContext(requestContext);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x001126B0 File Offset: 0x001108B0
		public RequestContext ReceiveRequest()
		{
			RequestContext requestContext = base.InnerChannel.ReceiveRequest();
			if (requestContext == null)
			{
				return null;
			}
			return this.CreateContextChannelRequestContext(requestContext);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x001126D8 File Offset: 0x001108D8
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

		// Token: 0x06004AD5 RID: 19157 RVA: 0x00112704 File Offset: 0x00110904
		public bool WaitForRequest(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForRequest(timeout);
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x00112712 File Offset: 0x00110912
		private ContextChannelRequestContext CreateContextChannelRequestContext(RequestContext innerContext)
		{
			this.contextProtocol.OnIncomingMessage(innerContext.RequestMessage);
			return new ContextChannelRequestContext(innerContext, this.contextProtocol, base.DefaultSendTimeout);
		}

		// Token: 0x04002F26 RID: 12070
		private ContextProtocol contextProtocol;
	}
}

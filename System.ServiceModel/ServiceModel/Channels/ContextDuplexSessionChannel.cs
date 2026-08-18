using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007AD RID: 1965
	internal class ContextDuplexSessionChannel : ContextOutputChannelBase<IDuplexSessionChannel>, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
	{
		// Token: 0x06004A63 RID: 19043 RVA: 0x0011189A File Offset: 0x0010FA9A
		public ContextDuplexSessionChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism, Uri address, Uri callbackAddress, bool contextManagementEnabled) : base(channelManager, innerChannel)
		{
			this.contextProtocol = new ClientContextProtocol(contextExchangeMechanism, address, this, callbackAddress, contextManagementEnabled);
		}

		// Token: 0x06004A64 RID: 19044 RVA: 0x001118B7 File Offset: 0x0010FAB7
		public ContextDuplexSessionChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism) : base(channelManager, innerChannel)
		{
			this.contextProtocol = new ServiceContextProtocol(contextExchangeMechanism);
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x001118CD File Offset: 0x0010FACD
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06004A66 RID: 19046 RVA: 0x001118DA File Offset: 0x0010FADA
		public IDuplexSession Session
		{
			get
			{
				return base.InnerChannel.Session;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06004A67 RID: 19047 RVA: 0x001118E7 File Offset: 0x0010FAE7
		protected override ContextProtocol ContextProtocol
		{
			get
			{
				return this.contextProtocol;
			}
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x001118EF File Offset: 0x0010FAEF
		protected override bool IsClient
		{
			get
			{
				return this.ContextProtocol is ClientContextProtocol;
			}
		}

		// Token: 0x06004A69 RID: 19049 RVA: 0x001118FF File Offset: 0x0010FAFF
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x06004A6A RID: 19050 RVA: 0x0011190F File Offset: 0x0010FB0F
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(callback, state);
		}

		// Token: 0x06004A6B RID: 19051 RVA: 0x0011191E File Offset: 0x0010FB1E
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x06004A6C RID: 19052 RVA: 0x0011192E File Offset: 0x0010FB2E
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x06004A6D RID: 19053 RVA: 0x00111940 File Offset: 0x0010FB40
		public Message EndReceive(IAsyncResult result)
		{
			Message message = base.InnerChannel.EndReceive(result);
			if (message != null)
			{
				this.ContextProtocol.OnIncomingMessage(message);
			}
			return message;
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x0011196C File Offset: 0x0010FB6C
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			bool flag = base.InnerChannel.EndTryReceive(result, out message);
			if (flag && message != null)
			{
				this.ContextProtocol.OnIncomingMessage(message);
			}
			return flag;
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x0011199C File Offset: 0x0010FB9C
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForMessage(result);
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x001119AC File Offset: 0x0010FBAC
		public Message Receive(TimeSpan timeout)
		{
			Message message = base.InnerChannel.Receive(timeout);
			if (message != null)
			{
				this.ContextProtocol.OnIncomingMessage(message);
			}
			return message;
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x001119D8 File Offset: 0x0010FBD8
		public Message Receive()
		{
			Message message = base.InnerChannel.Receive();
			if (message != null)
			{
				this.ContextProtocol.OnIncomingMessage(message);
			}
			return message;
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x00111A04 File Offset: 0x0010FC04
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			bool flag = base.InnerChannel.TryReceive(timeout, out message);
			if (flag && message != null)
			{
				this.ContextProtocol.OnIncomingMessage(message);
			}
			return flag;
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x00111A34 File Offset: 0x0010FC34
		public bool WaitForMessage(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForMessage(timeout);
		}

		// Token: 0x04002F0F RID: 12047
		private ContextProtocol contextProtocol;
	}
}

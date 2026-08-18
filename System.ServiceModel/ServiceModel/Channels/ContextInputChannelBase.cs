using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B8 RID: 1976
	internal abstract class ContextInputChannelBase<TChannel> : LayeredChannel<TChannel> where TChannel : class, IInputChannel
	{
		// Token: 0x06004AA9 RID: 19113 RVA: 0x00112296 File Offset: 0x00110496
		protected ContextInputChannelBase(ChannelManagerBase channelManager, TChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism) : base(channelManager, innerChannel)
		{
			this.contextExchangeMechanism = contextExchangeMechanism;
			this.contextProtocol = new ServiceContextProtocol(contextExchangeMechanism);
		}

		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x06004AAA RID: 19114 RVA: 0x001122B3 File Offset: 0x001104B3
		public EndpointAddress LocalAddress
		{
			get
			{
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x001122C5 File Offset: 0x001104C5
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x001122DA File Offset: 0x001104DA
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(callback, state);
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x001122EE File Offset: 0x001104EE
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x00112303 File Offset: 0x00110503
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x00112318 File Offset: 0x00110518
		public Message EndReceive(IAsyncResult result)
		{
			Message message = base.InnerChannel.EndReceive(result);
			this.ProcessContextHeader(message);
			return message;
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x0011233F File Offset: 0x0011053F
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			if (base.InnerChannel.EndTryReceive(result, out message))
			{
				this.ProcessContextHeader(message);
				return true;
			}
			return false;
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00112360 File Offset: 0x00110560
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForMessage(result);
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x00112374 File Offset: 0x00110574
		public Message Receive(TimeSpan timeout)
		{
			Message message = base.InnerChannel.Receive(timeout);
			this.ProcessContextHeader(message);
			return message;
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0011239C File Offset: 0x0011059C
		public Message Receive()
		{
			Message message = base.InnerChannel.Receive();
			this.ProcessContextHeader(message);
			return message;
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x001123C2 File Offset: 0x001105C2
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			if (base.InnerChannel.TryReceive(timeout, out message))
			{
				this.ProcessContextHeader(message);
				return true;
			}
			return false;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x001123E3 File Offset: 0x001105E3
		public bool WaitForMessage(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForMessage(timeout);
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x001123F6 File Offset: 0x001105F6
		private void ProcessContextHeader(Message message)
		{
			if (message != null)
			{
				this.contextProtocol.OnIncomingMessage(message);
			}
		}

		// Token: 0x04002F23 RID: 12067
		private ContextExchangeMechanism contextExchangeMechanism;

		// Token: 0x04002F24 RID: 12068
		private ServiceContextProtocol contextProtocol;
	}
}

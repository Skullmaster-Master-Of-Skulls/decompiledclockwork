using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000016 RID: 22
	internal class DiscoveryClientDuplexChannel<TChannel> : DiscoveryClientOutputChannel<TChannel>, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel where TChannel : class, IDuplexChannel
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00005EF7 File Offset: 0x000040F7
		public DiscoveryClientDuplexChannel(ChannelManagerBase channelManagerBase, IChannelFactory<TChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider) : base(channelManagerBase, innerChannelFactory, findCriteria, discoveryEndpointProvider)
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005F04 File Offset: 0x00004104
		public EndpointAddress LocalAddress
		{
			get
			{
				if (base.InnerChannel == null)
				{
					return DiscoveryClientBindingElement.DiscoveryEndpointAddress;
				}
				return base.InnerChannel.LocalAddress;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005F29 File Offset: 0x00004129
		public override IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.EnsureReplyTo(message);
			return base.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005F3D File Offset: 0x0000413D
		public override void Send(Message message, TimeSpan timeout)
		{
			this.EnsureReplyTo(message);
			base.Send(message, timeout);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00005F4E File Offset: 0x0000414E
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(timeout, callback, state);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00005F63 File Offset: 0x00004163
		public IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginReceive(callback, state);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005F77 File Offset: 0x00004177
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005F8C File Offset: 0x0000418C
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005FA1 File Offset: 0x000041A1
		public Message EndReceive(IAsyncResult result)
		{
			return base.InnerChannel.EndReceive(result);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005FB4 File Offset: 0x000041B4
		public bool EndTryReceive(IAsyncResult result, out Message message)
		{
			return base.InnerChannel.EndTryReceive(result, out message);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005FC8 File Offset: 0x000041C8
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return base.InnerChannel.EndWaitForMessage(result);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005FDB File Offset: 0x000041DB
		public Message Receive(TimeSpan timeout)
		{
			return base.InnerChannel.Receive(timeout);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005FEE File Offset: 0x000041EE
		public Message Receive()
		{
			return base.InnerChannel.Receive();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00006000 File Offset: 0x00004200
		public bool TryReceive(TimeSpan timeout, out Message message)
		{
			return base.InnerChannel.TryReceive(timeout, out message);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00006014 File Offset: 0x00004214
		public bool WaitForMessage(TimeSpan timeout)
		{
			return base.InnerChannel.WaitForMessage(timeout);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00006027 File Offset: 0x00004227
		private void EnsureReplyTo(Message message)
		{
			if (message != null && message.Headers != null && message.Headers.ReplyTo == DiscoveryClientBindingElement.DiscoveryEndpointAddress)
			{
				message.Headers.ReplyTo = this.LocalAddress;
			}
		}
	}
}

using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000018 RID: 24
	internal class DiscoveryClientOutputChannel<TChannel> : DiscoveryClientChannelBase<TChannel>, IOutputChannel, IChannel, ICommunicationObject where TChannel : class, IOutputChannel
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00006080 File Offset: 0x00004280
		public DiscoveryClientOutputChannel(ChannelManagerBase channelManagerBase, IChannelFactory<TChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider) : base(channelManagerBase, innerChannelFactory, findCriteria, discoveryEndpointProvider)
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000608D File Offset: 0x0000428D
		public EndpointAddress RemoteAddress
		{
			get
			{
				if (base.InnerChannel == null)
				{
					return DiscoveryClientBindingElement.DiscoveryEndpointAddress;
				}
				return base.InnerChannel.RemoteAddress;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000060B2 File Offset: 0x000042B2
		public Uri Via
		{
			get
			{
				if (base.InnerChannel == null)
				{
					return DiscoveryClientBindingElement.DiscoveryEndpointAddress.Uri;
				}
				return base.InnerChannel.Via;
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000060DC File Offset: 0x000042DC
		public virtual IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000060F3 File Offset: 0x000042F3
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.BeginSend(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006104 File Offset: 0x00004304
		public void EndSend(IAsyncResult result)
		{
			base.InnerChannel.EndSend(result);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006117 File Offset: 0x00004317
		public virtual void Send(Message message, TimeSpan timeout)
		{
			base.InnerChannel.Send(message, timeout);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000612B File Offset: 0x0000432B
		public void Send(Message message)
		{
			this.Send(message, base.DefaultSendTimeout);
		}
	}
}

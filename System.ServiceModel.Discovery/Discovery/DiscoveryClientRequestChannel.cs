using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200001A RID: 26
	internal class DiscoveryClientRequestChannel<TChannel> : DiscoveryClientChannelBase<TChannel>, IRequestChannel, IChannel, ICommunicationObject where TChannel : class, IRequestChannel
	{
		// Token: 0x06000165 RID: 357 RVA: 0x0000615E File Offset: 0x0000435E
		public DiscoveryClientRequestChannel(ChannelManagerBase channelManagerBase, IChannelFactory<TChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider) : base(channelManagerBase, innerChannelFactory, findCriteria, discoveryEndpointProvider)
		{
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000616B File Offset: 0x0000436B
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00006190 File Offset: 0x00004390
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

		// Token: 0x06000168 RID: 360 RVA: 0x000061BA File Offset: 0x000043BA
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.InnerChannel.BeginRequest(message, timeout, callback, state);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000061D1 File Offset: 0x000043D1
		public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
		{
			return this.BeginRequest(message, base.DefaultSendTimeout, callback, state);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000061E2 File Offset: 0x000043E2
		public Message EndRequest(IAsyncResult result)
		{
			return base.InnerChannel.EndRequest(result);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000061F5 File Offset: 0x000043F5
		public Message Request(Message message, TimeSpan timeout)
		{
			return base.InnerChannel.Request(message, timeout);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006209 File Offset: 0x00004409
		public Message Request(Message message)
		{
			return this.Request(message, base.DefaultSendTimeout);
		}
	}
}

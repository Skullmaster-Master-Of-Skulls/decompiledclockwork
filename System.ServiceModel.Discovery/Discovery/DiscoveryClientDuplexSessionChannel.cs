using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000017 RID: 23
	internal class DiscoveryClientDuplexSessionChannel : DiscoveryClientDuplexChannel<IDuplexSessionChannel>, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
	{
		// Token: 0x06000159 RID: 345 RVA: 0x0000605C File Offset: 0x0000425C
		public DiscoveryClientDuplexSessionChannel(ChannelManagerBase channelManagerBase, IChannelFactory<IDuplexSessionChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider) : base(channelManagerBase, innerChannelFactory, findCriteria, discoveryEndpointProvider)
		{
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00006069 File Offset: 0x00004269
		public IDuplexSession Session
		{
			get
			{
				if (base.InnerChannel == null)
				{
					return null;
				}
				return base.InnerChannel.Session;
			}
		}
	}
}

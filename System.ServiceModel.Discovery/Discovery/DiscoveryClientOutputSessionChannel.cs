using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000019 RID: 25
	internal class DiscoveryClientOutputSessionChannel : DiscoveryClientOutputChannel<IOutputSessionChannel>, IOutputSessionChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000613A File Offset: 0x0000433A
		public DiscoveryClientOutputSessionChannel(ChannelManagerBase channelManagerBase, IChannelFactory<IOutputSessionChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider) : base(channelManagerBase, innerChannelFactory, findCriteria, discoveryEndpointProvider)
		{
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00006147 File Offset: 0x00004347
		public IOutputSession Session
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

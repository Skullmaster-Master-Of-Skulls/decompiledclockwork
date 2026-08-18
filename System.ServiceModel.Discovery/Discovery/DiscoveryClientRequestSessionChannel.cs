using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200001B RID: 27
	internal class DiscoveryClientRequestSessionChannel : DiscoveryClientRequestChannel<IRequestSessionChannel>, IRequestSessionChannel, IRequestChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00006218 File Offset: 0x00004418
		public DiscoveryClientRequestSessionChannel(ChannelManagerBase channelManagerBase, IChannelFactory<IRequestSessionChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider) : base(channelManagerBase, innerChannelFactory, findCriteria, discoveryEndpointProvider)
		{
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00006225 File Offset: 0x00004425
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

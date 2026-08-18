using System;
using System.Collections.Generic;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A1B RID: 2587
	internal interface IConnectAlgorithms : IDisposable
	{
		// Token: 0x06006652 RID: 26194
		void Connect(TimeSpan timeout);

		// Token: 0x06006653 RID: 26195
		void Initialize(IPeerMaintainer maintainer, PeerNodeConfig config, int wantedConnectedNeighbors, Dictionary<EndpointAddress, Referral> referralCache);

		// Token: 0x06006654 RID: 26196
		void PruneConnections();

		// Token: 0x06006655 RID: 26197
		void UpdateEndpointsCollection(ICollection<PeerNodeAddress> src);
	}
}

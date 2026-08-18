using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200077D RID: 1917
	internal interface IConnectionOrientedTransportChannelFactorySettings : IConnectionOrientedTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings
	{
		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06004923 RID: 18723
		string ConnectionPoolGroupName { get; }

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06004924 RID: 18724
		int MaxOutboundConnectionsPerEndpoint { get; }
	}
}

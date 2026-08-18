using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200077E RID: 1918
	internal interface ITcpChannelFactorySettings : IConnectionOrientedTransportChannelFactorySettings, IConnectionOrientedTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings
	{
		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06004925 RID: 18725
		TimeSpan LeaseTimeout { get; }
	}
}

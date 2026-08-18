using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000780 RID: 1920
	internal interface IPipeTransportFactorySettings : IConnectionOrientedTransportChannelFactorySettings, IConnectionOrientedTransportFactorySettings, ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings
	{
		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06004928 RID: 18728
		NamedPipeSettings PipeSettings { get; }
	}
}

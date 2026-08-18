using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200077F RID: 1919
	internal interface IHttpTransportFactorySettings : ITransportFactorySettings, IDefaultCommunicationTimeouts
	{
		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06004926 RID: 18726
		int MaxBufferSize { get; }

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06004927 RID: 18727
		TransferMode TransferMode { get; }
	}
}

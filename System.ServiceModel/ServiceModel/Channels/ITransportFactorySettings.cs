using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200077B RID: 1915
	internal interface ITransportFactorySettings : IDefaultCommunicationTimeouts
	{
		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x0600491A RID: 18714
		bool ManualAddressing { get; }

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x0600491B RID: 18715
		BufferManager BufferManager { get; }

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x0600491C RID: 18716
		long MaxReceivedMessageSize { get; }

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x0600491D RID: 18717
		MessageEncoderFactory MessageEncoderFactory { get; }

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x0600491E RID: 18718
		MessageVersion MessageVersion { get; }
	}
}

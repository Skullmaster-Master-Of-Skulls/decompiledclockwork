using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200077C RID: 1916
	internal interface IConnectionOrientedTransportFactorySettings : ITransportFactorySettings, IDefaultCommunicationTimeouts, IConnectionOrientedConnectionSettings
	{
		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x0600491F RID: 18719
		int MaxBufferSize { get; }

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06004920 RID: 18720
		StreamUpgradeProvider Upgrade { get; }

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06004921 RID: 18721
		TransferMode TransferMode { get; }

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06004922 RID: 18722
		ServiceSecurityAuditBehavior AuditBehavior { get; }
	}
}

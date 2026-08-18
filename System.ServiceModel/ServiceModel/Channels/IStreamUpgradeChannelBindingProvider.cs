using System;
using System.Security.Authentication.ExtendedProtection;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007C8 RID: 1992
	internal interface IStreamUpgradeChannelBindingProvider : IChannelBindingProvider
	{
		// Token: 0x06004B11 RID: 19217
		ChannelBinding GetChannelBinding(StreamUpgradeInitiator upgradeInitiator, ChannelBindingKind kind);

		// Token: 0x06004B12 RID: 19218
		ChannelBinding GetChannelBinding(StreamUpgradeAcceptor upgradeAcceptor, ChannelBindingKind kind);
	}
}

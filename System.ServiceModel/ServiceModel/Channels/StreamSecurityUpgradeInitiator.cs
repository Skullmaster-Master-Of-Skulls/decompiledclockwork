using System;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000830 RID: 2096
	public abstract class StreamSecurityUpgradeInitiator : StreamUpgradeInitiator
	{
		// Token: 0x06004E58 RID: 20056
		public abstract SecurityMessageProperty GetRemoteSecurity();

		// Token: 0x06004E59 RID: 20057 RVA: 0x0011E118 File Offset: 0x0011C318
		internal static SecurityMessageProperty GetRemoteSecurity(StreamUpgradeInitiator upgradeInitiator)
		{
			StreamSecurityUpgradeInitiator streamSecurityUpgradeInitiator = upgradeInitiator as StreamSecurityUpgradeInitiator;
			if (streamSecurityUpgradeInitiator != null)
			{
				return streamSecurityUpgradeInitiator.GetRemoteSecurity();
			}
			return null;
		}
	}
}

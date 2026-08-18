using System;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200082D RID: 2093
	public abstract class StreamSecurityUpgradeAcceptor : StreamUpgradeAcceptor
	{
		// Token: 0x06004E43 RID: 20035
		public abstract SecurityMessageProperty GetRemoteSecurity();
	}
}

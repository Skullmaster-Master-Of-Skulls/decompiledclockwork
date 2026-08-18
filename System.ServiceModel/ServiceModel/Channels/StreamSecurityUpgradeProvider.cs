using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000833 RID: 2099
	public abstract class StreamSecurityUpgradeProvider : StreamUpgradeProvider
	{
		// Token: 0x06004E72 RID: 20082 RVA: 0x0011E465 File Offset: 0x0011C665
		protected StreamSecurityUpgradeProvider()
		{
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0011E46D File Offset: 0x0011C66D
		protected StreamSecurityUpgradeProvider(IDefaultCommunicationTimeouts timeouts) : base(timeouts)
		{
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06004E74 RID: 20084
		public abstract EndpointIdentity Identity { get; }
	}
}

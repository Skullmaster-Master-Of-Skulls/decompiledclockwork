using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000010 RID: 16
	internal class DefaultDiscoveryServiceExtension : DiscoveryServiceExtension
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003A00 File Offset: 0x00001C00
		public DefaultDiscoveryServiceExtension(int duplicateMessageHistoryLength)
		{
			this.discoveryService = new DefaultDiscoveryService(this, new DiscoveryMessageSequenceGenerator(), duplicateMessageHistoryLength);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003A1A File Offset: 0x00001C1A
		protected override DiscoveryService GetDiscoveryService()
		{
			return this.discoveryService;
		}

		// Token: 0x04000035 RID: 53
		private readonly DiscoveryService discoveryService;
	}
}

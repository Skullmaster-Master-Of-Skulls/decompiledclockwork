using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B8 RID: 696
	[Flags]
	internal enum GetAdaptersAddressesFlags
	{
		// Token: 0x04001935 RID: 6453
		SkipUnicast = 1,
		// Token: 0x04001936 RID: 6454
		SkipAnycast = 2,
		// Token: 0x04001937 RID: 6455
		SkipMulticast = 4,
		// Token: 0x04001938 RID: 6456
		SkipDnsServer = 8,
		// Token: 0x04001939 RID: 6457
		IncludePrefix = 16,
		// Token: 0x0400193A RID: 6458
		SkipFriendlyName = 32,
		// Token: 0x0400193B RID: 6459
		IncludeWins = 64,
		// Token: 0x0400193C RID: 6460
		IncludeGateways = 128,
		// Token: 0x0400193D RID: 6461
		IncludeAllInterfaces = 256,
		// Token: 0x0400193E RID: 6462
		IncludeAllCompartments = 512,
		// Token: 0x0400193F RID: 6463
		IncludeTunnelBindingOrder = 1024
	}
}

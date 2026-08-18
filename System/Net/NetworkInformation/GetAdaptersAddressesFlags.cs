using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005F2 RID: 1522
	[Flags]
	internal enum GetAdaptersAddressesFlags
	{
		// Token: 0x04002CEC RID: 11500
		SkipUnicast = 1,
		// Token: 0x04002CED RID: 11501
		SkipAnycast = 2,
		// Token: 0x04002CEE RID: 11502
		SkipMulticast = 4,
		// Token: 0x04002CEF RID: 11503
		SkipDnsServer = 8,
		// Token: 0x04002CF0 RID: 11504
		IncludePrefix = 16,
		// Token: 0x04002CF1 RID: 11505
		SkipFriendlyName = 32
	}
}

using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B5 RID: 693
	[Flags]
	internal enum AdapterFlags
	{
		// Token: 0x04001920 RID: 6432
		DnsEnabled = 1,
		// Token: 0x04001921 RID: 6433
		RegisterAdapterSuffix = 2,
		// Token: 0x04001922 RID: 6434
		DhcpEnabled = 4,
		// Token: 0x04001923 RID: 6435
		ReceiveOnly = 8,
		// Token: 0x04001924 RID: 6436
		NoMulticast = 16,
		// Token: 0x04001925 RID: 6437
		Ipv6OtherStatefulConfig = 32,
		// Token: 0x04001926 RID: 6438
		NetBiosOverTcp = 64,
		// Token: 0x04001927 RID: 6439
		IPv4Enabled = 128,
		// Token: 0x04001928 RID: 6440
		IPv6Enabled = 256,
		// Token: 0x04001929 RID: 6441
		IPv6ManagedAddressConfigurationSupported = 512
	}
}

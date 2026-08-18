using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005EF RID: 1519
	[Flags]
	internal enum AdapterFlags
	{
		// Token: 0x04002CDB RID: 11483
		DnsEnabled = 1,
		// Token: 0x04002CDC RID: 11484
		RegisterAdapterSuffix = 2,
		// Token: 0x04002CDD RID: 11485
		DhcpEnabled = 4,
		// Token: 0x04002CDE RID: 11486
		ReceiveOnly = 8,
		// Token: 0x04002CDF RID: 11487
		NoMulticast = 16,
		// Token: 0x04002CE0 RID: 11488
		Ipv6OtherStatefulConfig = 32
	}
}

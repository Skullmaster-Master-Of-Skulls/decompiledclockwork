using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C1 RID: 705
	internal struct IpPerAdapterInfo
	{
		// Token: 0x04001993 RID: 6547
		internal bool autoconfigEnabled;

		// Token: 0x04001994 RID: 6548
		internal bool autoconfigActive;

		// Token: 0x04001995 RID: 6549
		internal IntPtr currentDnsServer;

		// Token: 0x04001996 RID: 6550
		internal IpAddrString dnsServerList;
	}
}

using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005FD RID: 1533
	internal struct IpPerAdapterInfo
	{
		// Token: 0x04002D46 RID: 11590
		internal bool autoconfigEnabled;

		// Token: 0x04002D47 RID: 11591
		internal bool autoconfigActive;

		// Token: 0x04002D48 RID: 11592
		internal IntPtr currentDnsServer;

		// Token: 0x04002D49 RID: 11593
		internal IpAddrString dnsServerList;
	}
}

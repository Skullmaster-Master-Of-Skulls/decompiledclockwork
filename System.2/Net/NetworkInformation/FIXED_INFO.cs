using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BA RID: 698
	internal struct FIXED_INFO
	{
		// Token: 0x04001944 RID: 6468
		internal const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x04001945 RID: 6469
		internal const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x04001946 RID: 6470
		internal const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x04001947 RID: 6471
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		internal string hostName;

		// Token: 0x04001948 RID: 6472
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		internal string domainName;

		// Token: 0x04001949 RID: 6473
		internal uint currentDnsServer;

		// Token: 0x0400194A RID: 6474
		internal IpAddrString DnsServerList;

		// Token: 0x0400194B RID: 6475
		internal NetBiosNodeType nodeType;

		// Token: 0x0400194C RID: 6476
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string scopeId;

		// Token: 0x0400194D RID: 6477
		internal bool enableRouting;

		// Token: 0x0400194E RID: 6478
		internal bool enableProxy;

		// Token: 0x0400194F RID: 6479
		internal bool enableDns;
	}
}

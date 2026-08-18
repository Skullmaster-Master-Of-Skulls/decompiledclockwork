using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005F6 RID: 1526
	internal struct FIXED_INFO
	{
		// Token: 0x04002CFB RID: 11515
		internal const int MAX_HOSTNAME_LEN = 128;

		// Token: 0x04002CFC RID: 11516
		internal const int MAX_DOMAIN_NAME_LEN = 128;

		// Token: 0x04002CFD RID: 11517
		internal const int MAX_SCOPE_ID_LEN = 256;

		// Token: 0x04002CFE RID: 11518
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		internal string hostName;

		// Token: 0x04002CFF RID: 11519
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		internal string domainName;

		// Token: 0x04002D00 RID: 11520
		internal uint currentDnsServer;

		// Token: 0x04002D01 RID: 11521
		internal IpAddrString DnsServerList;

		// Token: 0x04002D02 RID: 11522
		internal NetBiosNodeType nodeType;

		// Token: 0x04002D03 RID: 11523
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string scopeId;

		// Token: 0x04002D04 RID: 11524
		internal bool enableRouting;

		// Token: 0x04002D05 RID: 11525
		internal bool enableProxy;

		// Token: 0x04002D06 RID: 11526
		internal bool enableDns;
	}
}

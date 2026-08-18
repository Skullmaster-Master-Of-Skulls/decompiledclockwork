using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200060D RID: 1549
	internal struct Icmp6EchoReply
	{
		// Token: 0x04002DC5 RID: 11717
		internal Ipv6Address Address;

		// Token: 0x04002DC6 RID: 11718
		internal uint Status;

		// Token: 0x04002DC7 RID: 11719
		internal uint RoundTripTime;

		// Token: 0x04002DC8 RID: 11720
		internal IntPtr data;
	}
}

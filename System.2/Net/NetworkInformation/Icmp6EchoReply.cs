using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D7 RID: 727
	internal struct Icmp6EchoReply
	{
		// Token: 0x04001A48 RID: 6728
		internal Ipv6Address Address;

		// Token: 0x04001A49 RID: 6729
		internal uint Status;

		// Token: 0x04001A4A RID: 6730
		internal uint RoundTripTime;

		// Token: 0x04001A4B RID: 6731
		internal IntPtr data;
	}
}

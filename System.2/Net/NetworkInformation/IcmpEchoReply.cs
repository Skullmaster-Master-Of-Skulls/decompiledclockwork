using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D5 RID: 725
	internal struct IcmpEchoReply
	{
		// Token: 0x04001A3E RID: 6718
		internal uint address;

		// Token: 0x04001A3F RID: 6719
		internal uint status;

		// Token: 0x04001A40 RID: 6720
		internal uint roundTripTime;

		// Token: 0x04001A41 RID: 6721
		internal ushort dataSize;

		// Token: 0x04001A42 RID: 6722
		internal ushort reserved;

		// Token: 0x04001A43 RID: 6723
		internal IntPtr data;

		// Token: 0x04001A44 RID: 6724
		internal IPOptions options;
	}
}

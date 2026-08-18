using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005FB RID: 1531
	internal struct IpAdapterPrefix
	{
		// Token: 0x04002D2C RID: 11564
		internal uint length;

		// Token: 0x04002D2D RID: 11565
		internal uint ifIndex;

		// Token: 0x04002D2E RID: 11566
		internal IntPtr next;

		// Token: 0x04002D2F RID: 11567
		internal IpSocketAddress address;

		// Token: 0x04002D30 RID: 11568
		internal uint prefixLength;
	}
}

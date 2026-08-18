using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005F9 RID: 1529
	internal struct IpAdapterAddress
	{
		// Token: 0x04002D1E RID: 11550
		internal uint length;

		// Token: 0x04002D1F RID: 11551
		internal AdapterAddressFlags flags;

		// Token: 0x04002D20 RID: 11552
		internal IntPtr next;

		// Token: 0x04002D21 RID: 11553
		internal IpSocketAddress address;
	}
}

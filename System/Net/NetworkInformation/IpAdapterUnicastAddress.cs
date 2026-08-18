using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005FA RID: 1530
	internal struct IpAdapterUnicastAddress
	{
		// Token: 0x04002D22 RID: 11554
		internal uint length;

		// Token: 0x04002D23 RID: 11555
		internal AdapterAddressFlags flags;

		// Token: 0x04002D24 RID: 11556
		internal IntPtr next;

		// Token: 0x04002D25 RID: 11557
		internal IpSocketAddress address;

		// Token: 0x04002D26 RID: 11558
		internal PrefixOrigin prefixOrigin;

		// Token: 0x04002D27 RID: 11559
		internal SuffixOrigin suffixOrigin;

		// Token: 0x04002D28 RID: 11560
		internal DuplicateAddressDetectionState dadState;

		// Token: 0x04002D29 RID: 11561
		internal uint validLifetime;

		// Token: 0x04002D2A RID: 11562
		internal uint preferredLifetime;

		// Token: 0x04002D2B RID: 11563
		internal uint leaseLifetime;
	}
}

using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BD RID: 701
	internal struct IpAdapterUnicastAddress
	{
		// Token: 0x04001956 RID: 6486
		internal uint length;

		// Token: 0x04001957 RID: 6487
		internal AdapterAddressFlags flags;

		// Token: 0x04001958 RID: 6488
		internal IntPtr next;

		// Token: 0x04001959 RID: 6489
		internal IpSocketAddress address;

		// Token: 0x0400195A RID: 6490
		internal PrefixOrigin prefixOrigin;

		// Token: 0x0400195B RID: 6491
		internal SuffixOrigin suffixOrigin;

		// Token: 0x0400195C RID: 6492
		internal DuplicateAddressDetectionState dadState;

		// Token: 0x0400195D RID: 6493
		internal uint validLifetime;

		// Token: 0x0400195E RID: 6494
		internal uint preferredLifetime;

		// Token: 0x0400195F RID: 6495
		internal uint leaseLifetime;

		// Token: 0x04001960 RID: 6496
		internal byte prefixLength;
	}
}

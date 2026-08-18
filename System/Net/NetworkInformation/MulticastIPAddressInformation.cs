using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005E5 RID: 1509
	public abstract class MulticastIPAddressInformation : IPAddressInformation
	{
		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002F9A RID: 12186
		public abstract long AddressPreferredLifetime { get; }

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002F9B RID: 12187
		public abstract long AddressValidLifetime { get; }

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002F9C RID: 12188
		public abstract long DhcpLeaseLifetime { get; }

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002F9D RID: 12189
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002F9E RID: 12190
		public abstract PrefixOrigin PrefixOrigin { get; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002F9F RID: 12191
		public abstract SuffixOrigin SuffixOrigin { get; }
	}
}

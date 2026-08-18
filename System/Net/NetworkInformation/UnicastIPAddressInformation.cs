using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005E3 RID: 1507
	public abstract class UnicastIPAddressInformation : IPAddressInformation
	{
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002F86 RID: 12166
		public abstract long AddressPreferredLifetime { get; }

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002F87 RID: 12167
		public abstract long AddressValidLifetime { get; }

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002F88 RID: 12168
		public abstract long DhcpLeaseLifetime { get; }

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002F89 RID: 12169
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { get; }

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002F8A RID: 12170
		public abstract PrefixOrigin PrefixOrigin { get; }

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002F8B RID: 12171
		public abstract SuffixOrigin SuffixOrigin { get; }

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002F8C RID: 12172
		public abstract IPAddress IPv4Mask { get; }
	}
}

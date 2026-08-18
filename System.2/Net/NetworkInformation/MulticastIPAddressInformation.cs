using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002AC RID: 684
	[__DynamicallyInvokable]
	public abstract class MulticastIPAddressInformation : IPAddressInformation
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06001983 RID: 6531
		[__DynamicallyInvokable]
		public abstract long AddressPreferredLifetime { [__DynamicallyInvokable] get; }

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06001984 RID: 6532
		[__DynamicallyInvokable]
		public abstract long AddressValidLifetime { [__DynamicallyInvokable] get; }

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06001985 RID: 6533
		[__DynamicallyInvokable]
		public abstract long DhcpLeaseLifetime { [__DynamicallyInvokable] get; }

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06001986 RID: 6534
		[__DynamicallyInvokable]
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { [__DynamicallyInvokable] get; }

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001987 RID: 6535
		[__DynamicallyInvokable]
		public abstract PrefixOrigin PrefixOrigin { [__DynamicallyInvokable] get; }

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001988 RID: 6536
		[__DynamicallyInvokable]
		public abstract SuffixOrigin SuffixOrigin { [__DynamicallyInvokable] get; }

		// Token: 0x06001989 RID: 6537 RVA: 0x0007E0FD File Offset: 0x0007C2FD
		[__DynamicallyInvokable]
		protected MulticastIPAddressInformation()
		{
		}
	}
}

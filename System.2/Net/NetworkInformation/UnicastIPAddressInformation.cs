using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002AA RID: 682
	[__DynamicallyInvokable]
	public abstract class UnicastIPAddressInformation : IPAddressInformation
	{
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600196E RID: 6510
		[__DynamicallyInvokable]
		public abstract long AddressPreferredLifetime { [__DynamicallyInvokable] get; }

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600196F RID: 6511
		[__DynamicallyInvokable]
		public abstract long AddressValidLifetime { [__DynamicallyInvokable] get; }

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06001970 RID: 6512
		[__DynamicallyInvokable]
		public abstract long DhcpLeaseLifetime { [__DynamicallyInvokable] get; }

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06001971 RID: 6513
		[__DynamicallyInvokable]
		public abstract DuplicateAddressDetectionState DuplicateAddressDetectionState { [__DynamicallyInvokable] get; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06001972 RID: 6514
		[__DynamicallyInvokable]
		public abstract PrefixOrigin PrefixOrigin { [__DynamicallyInvokable] get; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06001973 RID: 6515
		[__DynamicallyInvokable]
		public abstract SuffixOrigin SuffixOrigin { [__DynamicallyInvokable] get; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001974 RID: 6516
		[__DynamicallyInvokable]
		public abstract IPAddress IPv4Mask { [__DynamicallyInvokable] get; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x0007E04A File Offset: 0x0007C24A
		[__DynamicallyInvokable]
		public virtual int PrefixLength
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x0007E051 File Offset: 0x0007C251
		[__DynamicallyInvokable]
		protected UnicastIPAddressInformation()
		{
		}
	}
}

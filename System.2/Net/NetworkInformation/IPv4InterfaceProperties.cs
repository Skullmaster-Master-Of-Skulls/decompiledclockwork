using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B2 RID: 690
	[__DynamicallyInvokable]
	public abstract class IPv4InterfaceProperties
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060019B3 RID: 6579
		[__DynamicallyInvokable]
		public abstract bool UsesWins { [__DynamicallyInvokable] get; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060019B4 RID: 6580
		[__DynamicallyInvokable]
		public abstract bool IsDhcpEnabled { [__DynamicallyInvokable] get; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060019B5 RID: 6581
		[__DynamicallyInvokable]
		public abstract bool IsAutomaticPrivateAddressingActive { [__DynamicallyInvokable] get; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060019B6 RID: 6582
		[__DynamicallyInvokable]
		public abstract bool IsAutomaticPrivateAddressingEnabled { [__DynamicallyInvokable] get; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060019B7 RID: 6583
		[__DynamicallyInvokable]
		public abstract int Index { [__DynamicallyInvokable] get; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060019B8 RID: 6584
		[__DynamicallyInvokable]
		public abstract bool IsForwardingEnabled { [__DynamicallyInvokable] get; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060019B9 RID: 6585
		[__DynamicallyInvokable]
		public abstract int Mtu { [__DynamicallyInvokable] get; }

		// Token: 0x060019BA RID: 6586 RVA: 0x0007E364 File Offset: 0x0007C564
		[__DynamicallyInvokable]
		protected IPv4InterfaceProperties()
		{
		}
	}
}

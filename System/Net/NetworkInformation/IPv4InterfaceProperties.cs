using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005EB RID: 1515
	public abstract class IPv4InterfaceProperties
	{
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002FC9 RID: 12233
		public abstract bool UsesWins { get; }

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002FCA RID: 12234
		public abstract bool IsDhcpEnabled { get; }

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002FCB RID: 12235
		public abstract bool IsAutomaticPrivateAddressingActive { get; }

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002FCC RID: 12236
		public abstract bool IsAutomaticPrivateAddressingEnabled { get; }

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002FCD RID: 12237
		public abstract int Index { get; }

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002FCE RID: 12238
		public abstract bool IsForwardingEnabled { get; }

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002FCF RID: 12239
		public abstract int Mtu { get; }
	}
}

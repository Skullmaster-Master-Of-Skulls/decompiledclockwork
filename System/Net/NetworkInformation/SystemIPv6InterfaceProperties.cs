using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000637 RID: 1591
	internal class SystemIPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x0600314C RID: 12620 RVA: 0x000D3BFC File Offset: 0x000D2BFC
		internal SystemIPv6InterfaceProperties(uint index, uint mtu)
		{
			this.index = index;
			this.mtu = mtu;
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000D3C12 File Offset: 0x000D2C12
		public override int Index
		{
			get
			{
				return (int)this.index;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000D3C1A File Offset: 0x000D2C1A
		public override int Mtu
		{
			get
			{
				return (int)this.mtu;
			}
		}

		// Token: 0x04002E7A RID: 11898
		private uint index;

		// Token: 0x04002E7B RID: 11899
		private uint mtu;
	}
}

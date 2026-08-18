using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FF RID: 767
	internal class SystemIPv6InterfaceProperties : IPv6InterfaceProperties
	{
		// Token: 0x06001B33 RID: 6963 RVA: 0x00081A68 File Offset: 0x0007FC68
		internal SystemIPv6InterfaceProperties(uint index, uint mtu, uint[] zoneIndices)
		{
			this.index = index;
			this.mtu = mtu;
			this.zoneIndices = zoneIndices;
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06001B34 RID: 6964 RVA: 0x00081A85 File Offset: 0x0007FC85
		public override int Index
		{
			get
			{
				return (int)this.index;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06001B35 RID: 6965 RVA: 0x00081A8D File Offset: 0x0007FC8D
		public override int Mtu
		{
			get
			{
				return (int)this.mtu;
			}
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x00081A95 File Offset: 0x0007FC95
		public override long GetScopeId(ScopeLevel scopeLevel)
		{
			if (scopeLevel < ScopeLevel.None || scopeLevel >= (ScopeLevel)this.zoneIndices.Length)
			{
				throw new ArgumentOutOfRangeException("scopeLevel");
			}
			return (long)((ulong)this.zoneIndices[(int)scopeLevel]);
		}

		// Token: 0x04001ADB RID: 6875
		private uint index;

		// Token: 0x04001ADC RID: 6876
		private uint mtu;

		// Token: 0x04001ADD RID: 6877
		private uint[] zoneIndices;
	}
}

using System;

namespace System.Net.Sockets
{
	// Token: 0x0200036E RID: 878
	public class MulticastOption
	{
		// Token: 0x06001FDE RID: 8158 RVA: 0x0009526C File Offset: 0x0009346C
		public MulticastOption(IPAddress group, IPAddress mcint)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (mcint == null)
			{
				throw new ArgumentNullException("mcint");
			}
			this.Group = group;
			this.LocalAddress = mcint;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0009529E File Offset: 0x0009349E
		public MulticastOption(IPAddress group, int interfaceIndex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (interfaceIndex < 0 || interfaceIndex > 16777215)
			{
				throw new ArgumentOutOfRangeException("interfaceIndex");
			}
			this.Group = group;
			this.ifIndex = interfaceIndex;
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000952D9 File Offset: 0x000934D9
		public MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.LocalAddress = IPAddress.Any;
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00095301 File Offset: 0x00093501
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00095309 File Offset: 0x00093509
		public IPAddress Group
		{
			get
			{
				return this.group;
			}
			set
			{
				this.group = value;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00095312 File Offset: 0x00093512
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x0009531A File Offset: 0x0009351A
		public IPAddress LocalAddress
		{
			get
			{
				return this.localAddress;
			}
			set
			{
				this.ifIndex = 0;
				this.localAddress = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0009532A File Offset: 0x0009352A
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x00095332 File Offset: 0x00093532
		public int InterfaceIndex
		{
			get
			{
				return this.ifIndex;
			}
			set
			{
				if (value < 0 || value > 16777215)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.localAddress = null;
				this.ifIndex = value;
			}
		}

		// Token: 0x04001DE8 RID: 7656
		private IPAddress group;

		// Token: 0x04001DE9 RID: 7657
		private IPAddress localAddress;

		// Token: 0x04001DEA RID: 7658
		private int ifIndex;
	}
}

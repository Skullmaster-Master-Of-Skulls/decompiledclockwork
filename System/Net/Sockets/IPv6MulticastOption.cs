using System;

namespace System.Net.Sockets
{
	// Token: 0x020005AD RID: 1453
	public class IPv6MulticastOption
	{
		// Token: 0x06002CDE RID: 11486 RVA: 0x000C1EC4 File Offset: 0x000C0EC4
		public IPv6MulticastOption(IPAddress group, long ifindex)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (ifindex < 0L || ifindex > (long)((ulong)-1))
			{
				throw new ArgumentOutOfRangeException("ifindex");
			}
			this.Group = group;
			this.InterfaceIndex = ifindex;
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x000C1EFD File Offset: 0x000C0EFD
		public IPv6MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.InterfaceIndex = 0L;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000C1F22 File Offset: 0x000C0F22
		// (set) Token: 0x06002CE1 RID: 11489 RVA: 0x000C1F2A File Offset: 0x000C0F2A
		public IPAddress Group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_Group = value;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06002CE2 RID: 11490 RVA: 0x000C1F41 File Offset: 0x000C0F41
		// (set) Token: 0x06002CE3 RID: 11491 RVA: 0x000C1F49 File Offset: 0x000C0F49
		public long InterfaceIndex
		{
			get
			{
				return this.m_Interface;
			}
			set
			{
				if (value < 0L || value > (long)((ulong)-1))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_Interface = value;
			}
		}

		// Token: 0x04002ADA RID: 10970
		private IPAddress m_Group;

		// Token: 0x04002ADB RID: 10971
		private long m_Interface;
	}
}

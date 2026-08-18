using System;

namespace System.Net.Sockets
{
	// Token: 0x0200036F RID: 879
	public class IPv6MulticastOption
	{
		// Token: 0x06001FE7 RID: 8167 RVA: 0x00095359 File Offset: 0x00093559
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

		// Token: 0x06001FE8 RID: 8168 RVA: 0x00095392 File Offset: 0x00093592
		public IPv6MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.InterfaceIndex = 0L;
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000953B7 File Offset: 0x000935B7
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x000953BF File Offset: 0x000935BF
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

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000953D6 File Offset: 0x000935D6
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x000953DE File Offset: 0x000935DE
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

		// Token: 0x04001DEB RID: 7659
		private IPAddress m_Group;

		// Token: 0x04001DEC RID: 7660
		private long m_Interface;
	}
}

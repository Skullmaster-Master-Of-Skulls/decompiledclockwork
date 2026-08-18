using System;

namespace System.Net.Sockets
{
	// Token: 0x020005AC RID: 1452
	public class MulticastOption
	{
		// Token: 0x06002CD5 RID: 11477 RVA: 0x000C1D9B File Offset: 0x000C0D9B
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

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000C1DD0 File Offset: 0x000C0DD0
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
			if (!ComNetOS.IsPostWin2K)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
			}
			this.Group = group;
			this.ifIndex = interfaceIndex;
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000C1E2D File Offset: 0x000C0E2D
		public MulticastOption(IPAddress group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			this.Group = group;
			this.LocalAddress = IPAddress.Any;
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000C1E55 File Offset: 0x000C0E55
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x000C1E5D File Offset: 0x000C0E5D
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

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000C1E66 File Offset: 0x000C0E66
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x000C1E6E File Offset: 0x000C0E6E
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

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000C1E7E File Offset: 0x000C0E7E
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000C1E86 File Offset: 0x000C0E86
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
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				this.localAddress = null;
				this.ifIndex = value;
			}
		}

		// Token: 0x04002AD7 RID: 10967
		private IPAddress group;

		// Token: 0x04002AD8 RID: 10968
		private IPAddress localAddress;

		// Token: 0x04002AD9 RID: 10969
		private int ifIndex;
	}
}

using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002E3 RID: 739
	[__DynamicallyInvokable]
	public abstract class NetworkInterface
	{
		// Token: 0x06001A07 RID: 6663 RVA: 0x0007EA0B File Offset: 0x0007CC0B
		[__DynamicallyInvokable]
		public static NetworkInterface[] GetAllNetworkInterfaces()
		{
			new NetworkInformationPermission(NetworkInformationAccess.Read).Demand();
			return SystemNetworkInterface.GetNetworkInterfaces();
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0007EA1D File Offset: 0x0007CC1D
		[__DynamicallyInvokable]
		public static bool GetIsNetworkAvailable()
		{
			return SystemNetworkInterface.InternalGetIsNetworkAvailable();
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001A09 RID: 6665 RVA: 0x0007EA24 File Offset: 0x0007CC24
		[__DynamicallyInvokable]
		public static int LoopbackInterfaceIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return SystemNetworkInterface.InternalLoopbackInterfaceIndex;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0007EA2B File Offset: 0x0007CC2B
		[__DynamicallyInvokable]
		public static int IPv6LoopbackInterfaceIndex
		{
			[__DynamicallyInvokable]
			get
			{
				return SystemNetworkInterface.InternalIPv6LoopbackInterfaceIndex;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001A0B RID: 6667 RVA: 0x0007EA32 File Offset: 0x0007CC32
		[__DynamicallyInvokable]
		public virtual string Id
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0007EA39 File Offset: 0x0007CC39
		[__DynamicallyInvokable]
		public virtual string Name
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x0007EA40 File Offset: 0x0007CC40
		[__DynamicallyInvokable]
		public virtual string Description
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x0007EA47 File Offset: 0x0007CC47
		[__DynamicallyInvokable]
		public virtual IPInterfaceProperties GetIPProperties()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x0007EA4E File Offset: 0x0007CC4E
		[__DynamicallyInvokable]
		public virtual IPv4InterfaceStatistics GetIPv4Statistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0007EA55 File Offset: 0x0007CC55
		[__DynamicallyInvokable]
		public virtual IPInterfaceStatistics GetIPStatistics()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001A11 RID: 6673 RVA: 0x0007EA5C File Offset: 0x0007CC5C
		[__DynamicallyInvokable]
		public virtual OperationalStatus OperationalStatus
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0007EA63 File Offset: 0x0007CC63
		[__DynamicallyInvokable]
		public virtual long Speed
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0007EA6A File Offset: 0x0007CC6A
		[__DynamicallyInvokable]
		public virtual bool IsReceiveOnly
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0007EA71 File Offset: 0x0007CC71
		[__DynamicallyInvokable]
		public virtual bool SupportsMulticast
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0007EA78 File Offset: 0x0007CC78
		[__DynamicallyInvokable]
		public virtual PhysicalAddress GetPhysicalAddress()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0007EA7F File Offset: 0x0007CC7F
		[__DynamicallyInvokable]
		public virtual NetworkInterfaceType NetworkInterfaceType
		{
			[__DynamicallyInvokable]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0007EA86 File Offset: 0x0007CC86
		[__DynamicallyInvokable]
		public virtual bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0007EA8D File Offset: 0x0007CC8D
		[__DynamicallyInvokable]
		protected NetworkInterface()
		{
		}
	}
}

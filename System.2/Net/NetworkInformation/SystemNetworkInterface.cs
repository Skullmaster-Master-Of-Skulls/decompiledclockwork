using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000300 RID: 768
	internal class SystemNetworkInterface : NetworkInterface
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001B37 RID: 6967 RVA: 0x00081ABA File Offset: 0x0007FCBA
		internal static int InternalLoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.GetBestInterfaceForAddress(IPAddress.Loopback);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x00081AC6 File Offset: 0x0007FCC6
		internal static int InternalIPv6LoopbackInterfaceIndex
		{
			get
			{
				return SystemNetworkInterface.GetBestInterfaceForAddress(IPAddress.IPv6Loopback);
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00081AD4 File Offset: 0x0007FCD4
		private static int GetBestInterfaceForAddress(IPAddress addr)
		{
			SocketAddress socketAddress = new SocketAddress(addr);
			int result;
			int bestInterfaceEx = (int)UnsafeNetInfoNativeMethods.GetBestInterfaceEx(socketAddress.m_Buffer, out result);
			if (bestInterfaceEx != 0)
			{
				throw new NetworkInformationException(bestInterfaceEx);
			}
			return result;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00081B04 File Offset: 0x0007FD04
		internal static bool InternalGetIsNetworkAvailable()
		{
			try
			{
				NetworkInterface[] networkInterfaces = SystemNetworkInterface.GetNetworkInterfaces();
				foreach (NetworkInterface networkInterface in networkInterfaces)
				{
					if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
					{
						return true;
					}
				}
			}
			catch (NetworkInformationException e)
			{
				if (Logging.On)
				{
					Logging.Exception(Logging.Web, "SystemNetworkInterface", "InternalGetIsNetworkAvailable", e);
				}
			}
			return false;
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00081B88 File Offset: 0x0007FD88
		internal static NetworkInterface[] GetNetworkInterfaces()
		{
			AddressFamily family = AddressFamily.Unspecified;
			uint cb = 0U;
			SafeLocalFree safeLocalFree = null;
			FixedInfo fixedInfo = SystemIPGlobalProperties.GetFixedInfo();
			List<SystemNetworkInterface> list = new List<SystemNetworkInterface>();
			GetAdaptersAddressesFlags flags = GetAdaptersAddressesFlags.IncludeWins | GetAdaptersAddressesFlags.IncludeGateways;
			uint adaptersAddresses = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(family, (uint)flags, IntPtr.Zero, SafeLocalFree.Zero, ref cb);
			while (adaptersAddresses == 111U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
					adaptersAddresses = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(family, (uint)flags, IntPtr.Zero, safeLocalFree, ref cb);
					if (adaptersAddresses == 0U)
					{
						IntPtr intPtr = safeLocalFree.DangerousGetHandle();
						while (intPtr != IntPtr.Zero)
						{
							IpAdapterAddresses ipAdapterAddresses = (IpAdapterAddresses)Marshal.PtrToStructure(intPtr, typeof(IpAdapterAddresses));
							list.Add(new SystemNetworkInterface(fixedInfo, ipAdapterAddresses));
							intPtr = ipAdapterAddresses.next;
						}
					}
				}
				finally
				{
					if (safeLocalFree != null)
					{
						safeLocalFree.Close();
					}
					safeLocalFree = null;
				}
			}
			if (adaptersAddresses == 232U || adaptersAddresses == 87U)
			{
				return new SystemNetworkInterface[0];
			}
			if (adaptersAddresses != 0U)
			{
				throw new NetworkInformationException((int)adaptersAddresses);
			}
			return list.ToArray();
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00081C84 File Offset: 0x0007FE84
		internal SystemNetworkInterface(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.id = ipAdapterAddresses.AdapterName;
			this.name = ipAdapterAddresses.friendlyName;
			this.description = ipAdapterAddresses.description;
			this.index = ipAdapterAddresses.index;
			this.physicalAddress = ipAdapterAddresses.address;
			this.addressLength = ipAdapterAddresses.addressLength;
			this.type = ipAdapterAddresses.type;
			this.operStatus = ipAdapterAddresses.operStatus;
			this.speed = (long)ipAdapterAddresses.receiveLinkSpeed;
			this.ipv6Index = ipAdapterAddresses.ipv6Index;
			this.adapterFlags = ipAdapterAddresses.flags;
			this.interfaceProperties = new SystemIPInterfaceProperties(fixedInfo, ipAdapterAddresses);
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00081D28 File Offset: 0x0007FF28
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x00081D30 File Offset: 0x0007FF30
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x00081D38 File Offset: 0x0007FF38
		public override string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x00081D40 File Offset: 0x0007FF40
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addressLength];
			Array.Copy(this.physicalAddress, array, (long)((ulong)this.addressLength));
			return new PhysicalAddress(array);
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x00081D72 File Offset: 0x0007FF72
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x00081D7A File Offset: 0x0007FF7A
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.interfaceProperties;
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x00081D82 File Offset: 0x0007FF82
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return new SystemIPv4InterfaceStatistics((long)((ulong)this.index));
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x00081D90 File Offset: 0x0007FF90
		public override IPInterfaceStatistics GetIPStatistics()
		{
			return new SystemIPInterfaceStatistics((long)((ulong)this.index));
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x00081D9E File Offset: 0x0007FF9E
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			return (networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && (this.adapterFlags & AdapterFlags.IPv6Enabled) != (AdapterFlags)0) || (networkInterfaceComponent == NetworkInterfaceComponent.IPv4 && (this.adapterFlags & AdapterFlags.IPv4Enabled) != (AdapterFlags)0);
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001B46 RID: 6982 RVA: 0x00081DC8 File Offset: 0x0007FFC8
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.operStatus;
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x00081DD0 File Offset: 0x0007FFD0
		public override long Speed
		{
			get
			{
				return this.speed;
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00081DD8 File Offset: 0x0007FFD8
		public override bool IsReceiveOnly
		{
			get
			{
				return (this.adapterFlags & AdapterFlags.ReceiveOnly) > (AdapterFlags)0;
			}
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001B49 RID: 6985 RVA: 0x00081DE5 File Offset: 0x0007FFE5
		public override bool SupportsMulticast
		{
			get
			{
				return (this.adapterFlags & AdapterFlags.NoMulticast) == (AdapterFlags)0;
			}
		}

		// Token: 0x04001ADE RID: 6878
		private string name;

		// Token: 0x04001ADF RID: 6879
		private string id;

		// Token: 0x04001AE0 RID: 6880
		private string description;

		// Token: 0x04001AE1 RID: 6881
		private byte[] physicalAddress;

		// Token: 0x04001AE2 RID: 6882
		private uint addressLength;

		// Token: 0x04001AE3 RID: 6883
		private NetworkInterfaceType type;

		// Token: 0x04001AE4 RID: 6884
		private OperationalStatus operStatus;

		// Token: 0x04001AE5 RID: 6885
		private long speed;

		// Token: 0x04001AE6 RID: 6886
		private uint index;

		// Token: 0x04001AE7 RID: 6887
		private uint ipv6Index;

		// Token: 0x04001AE8 RID: 6888
		private AdapterFlags adapterFlags;

		// Token: 0x04001AE9 RID: 6889
		private SystemIPInterfaceProperties interfaceProperties;
	}
}

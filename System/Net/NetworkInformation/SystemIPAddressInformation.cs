using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200062E RID: 1582
	internal class SystemIPAddressInformation : IPAddressInformation
	{
		// Token: 0x060030C4 RID: 12484 RVA: 0x000D2156 File Offset: 0x000D1156
		internal SystemIPAddressInformation(IPAddress address)
		{
			this.address = address;
			if (address.AddressFamily == AddressFamily.InterNetwork)
			{
				this.dnsEligible = ((address.m_Address & 65193L) <= 0L);
			}
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x000D218F File Offset: 0x000D118F
		internal SystemIPAddressInformation(IpAdapterUnicastAddress adapterAddress, IPAddress address)
		{
			this.address = address;
			this.transient = ((adapterAddress.flags & AdapterAddressFlags.Transient) > (AdapterAddressFlags)0);
			this.dnsEligible = ((adapterAddress.flags & AdapterAddressFlags.DnsEligible) > (AdapterAddressFlags)0);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x000D21C9 File Offset: 0x000D11C9
		internal SystemIPAddressInformation(IpAdapterAddress adapterAddress, IPAddress address)
		{
			this.address = address;
			this.transient = ((adapterAddress.flags & AdapterAddressFlags.Transient) > (AdapterAddressFlags)0);
			this.dnsEligible = ((adapterAddress.flags & AdapterAddressFlags.DnsEligible) > (AdapterAddressFlags)0);
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000D2203 File Offset: 0x000D1203
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060030C8 RID: 12488 RVA: 0x000D220B File Offset: 0x000D120B
		public override bool IsTransient
		{
			get
			{
				return this.transient;
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000D2213 File Offset: 0x000D1213
		public override bool IsDnsEligible
		{
			get
			{
				return this.dnsEligible;
			}
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000D221C File Offset: 0x000D121C
		internal static IPAddressCollection ToAddressCollection(IntPtr ptr, IPVersion versionSupported)
		{
			IPAddressCollection ipaddressCollection = new IPAddressCollection();
			if (ptr == IntPtr.Zero)
			{
				return ipaddressCollection;
			}
			IpAdapterAddress ipAdapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterAddress));
			AddressFamily addressFamily = (ipAdapterAddress.address.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;
			SocketAddress socketAddress = new SocketAddress(addressFamily, ipAdapterAddress.address.addressLength);
			Marshal.Copy(ipAdapterAddress.address.address, socketAddress.m_Buffer, 0, ipAdapterAddress.address.addressLength);
			IPEndPoint ipendPoint;
			if (addressFamily == AddressFamily.InterNetwork)
			{
				ipendPoint = (IPEndPoint)IPEndPoint.Any.Create(socketAddress);
			}
			else
			{
				ipendPoint = (IPEndPoint)IPEndPoint.IPv6Any.Create(socketAddress);
			}
			ipaddressCollection.InternalAdd(ipendPoint.Address);
			while (ipAdapterAddress.next != IntPtr.Zero)
			{
				ipAdapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ipAdapterAddress.next, typeof(IpAdapterAddress));
				addressFamily = ((ipAdapterAddress.address.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork);
				if ((addressFamily == AddressFamily.InterNetwork && (versionSupported & IPVersion.IPv4) > IPVersion.None) || (addressFamily == AddressFamily.InterNetworkV6 && (versionSupported & IPVersion.IPv6) > IPVersion.None))
				{
					socketAddress = new SocketAddress(addressFamily, ipAdapterAddress.address.addressLength);
					Marshal.Copy(ipAdapterAddress.address.address, socketAddress.m_Buffer, 0, ipAdapterAddress.address.addressLength);
					if (addressFamily == AddressFamily.InterNetwork)
					{
						ipendPoint = (IPEndPoint)IPEndPoint.Any.Create(socketAddress);
					}
					else
					{
						ipendPoint = (IPEndPoint)IPEndPoint.IPv6Any.Create(socketAddress);
					}
					ipaddressCollection.InternalAdd(ipendPoint.Address);
				}
			}
			return ipaddressCollection;
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000D23AC File Offset: 0x000D13AC
		internal static IPAddressInformationCollection ToAddressInformationCollection(IntPtr ptr, IPVersion versionSupported)
		{
			IPAddressInformationCollection ipaddressInformationCollection = new IPAddressInformationCollection();
			if (ptr == IntPtr.Zero)
			{
				return ipaddressInformationCollection;
			}
			IpAdapterAddress adapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterAddress));
			AddressFamily addressFamily = (adapterAddress.address.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;
			SocketAddress socketAddress = new SocketAddress(addressFamily, adapterAddress.address.addressLength);
			Marshal.Copy(adapterAddress.address.address, socketAddress.m_Buffer, 0, adapterAddress.address.addressLength);
			IPEndPoint ipendPoint;
			if (addressFamily == AddressFamily.InterNetwork)
			{
				ipendPoint = (IPEndPoint)IPEndPoint.Any.Create(socketAddress);
			}
			else
			{
				ipendPoint = (IPEndPoint)IPEndPoint.IPv6Any.Create(socketAddress);
			}
			ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(adapterAddress, ipendPoint.Address));
			while (adapterAddress.next != IntPtr.Zero)
			{
				adapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(adapterAddress.next, typeof(IpAdapterAddress));
				addressFamily = ((adapterAddress.address.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork);
				if ((addressFamily == AddressFamily.InterNetwork && (versionSupported & IPVersion.IPv4) > IPVersion.None) || (addressFamily == AddressFamily.InterNetworkV6 && (versionSupported & IPVersion.IPv6) > IPVersion.None))
				{
					socketAddress = new SocketAddress(addressFamily, adapterAddress.address.addressLength);
					Marshal.Copy(adapterAddress.address.address, socketAddress.m_Buffer, 0, adapterAddress.address.addressLength);
					if (addressFamily == AddressFamily.InterNetwork)
					{
						ipendPoint = (IPEndPoint)IPEndPoint.Any.Create(socketAddress);
					}
					else
					{
						ipendPoint = (IPEndPoint)IPEndPoint.IPv6Any.Create(socketAddress);
					}
					ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(adapterAddress, ipendPoint.Address));
				}
			}
			return ipaddressInformationCollection;
		}

		// Token: 0x04002E4E RID: 11854
		private IPAddress address;

		// Token: 0x04002E4F RID: 11855
		internal bool transient;

		// Token: 0x04002E50 RID: 11856
		internal bool dnsEligible = true;
	}
}

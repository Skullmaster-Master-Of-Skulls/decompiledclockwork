using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000635 RID: 1589
	internal class SystemUnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06003131 RID: 12593 RVA: 0x000D3658 File Offset: 0x000D2658
		private SystemUnicastIPAddressInformation()
		{
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000D3660 File Offset: 0x000D2660
		internal SystemUnicastIPAddressInformation(IpAdapterInfo ipAdapterInfo, IPExtendedAddress address)
		{
			this.innerInfo = new SystemIPAddressInformation(address.address);
			DateTime d = new DateTime(1970, 1, 1);
			d = d.AddSeconds(ipAdapterInfo.leaseExpires);
			this.dhcpLeaseLifetime = (long)(d - DateTime.UtcNow).TotalSeconds;
			this.ipv4Mask = address.mask;
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000D36CB File Offset: 0x000D26CB
		internal SystemUnicastIPAddressInformation(IpAdapterUnicastAddress adapterAddress, IPAddress ipAddress)
		{
			this.innerInfo = new SystemIPAddressInformation(adapterAddress, ipAddress);
			this.adapterAddress = adapterAddress;
			this.dhcpLeaseLifetime = (long)((ulong)adapterAddress.leaseLifetime);
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x000D36F5 File Offset: 0x000D26F5
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000D3702 File Offset: 0x000D2702
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return new IPAddress(0);
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000D371F File Offset: 0x000D271F
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x000D372C File Offset: 0x000D272C
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x000D3739 File Offset: 0x000D2739
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return this.adapterAddress.prefixOrigin;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000D375D File Offset: 0x000D275D
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return this.adapterAddress.suffixOrigin;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x000D3781 File Offset: 0x000D2781
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return this.adapterAddress.dadState;
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000D37A5 File Offset: 0x000D27A5
		public override long AddressValidLifetime
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return (long)((ulong)this.adapterAddress.validLifetime);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600313C RID: 12604 RVA: 0x000D37CA File Offset: 0x000D27CA
		public override long AddressPreferredLifetime
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return (long)((ulong)this.adapterAddress.preferredLifetime);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x000D37EF File Offset: 0x000D27EF
		public override long DhcpLeaseLifetime
		{
			get
			{
				return this.dhcpLeaseLifetime;
			}
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000D37F8 File Offset: 0x000D27F8
		internal static UnicastIPAddressInformationCollection ToAddressInformationCollection(IntPtr ptr)
		{
			UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
			if (ptr == IntPtr.Zero)
			{
				return unicastIPAddressInformationCollection;
			}
			IpAdapterUnicastAddress ipAdapterUnicastAddress = (IpAdapterUnicastAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterUnicastAddress));
			AddressFamily addressFamily = (ipAdapterUnicastAddress.address.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork;
			SocketAddress socketAddress = new SocketAddress(addressFamily, ipAdapterUnicastAddress.address.addressLength);
			Marshal.Copy(ipAdapterUnicastAddress.address.address, socketAddress.m_Buffer, 0, ipAdapterUnicastAddress.address.addressLength);
			IPEndPoint ipendPoint;
			if (addressFamily == AddressFamily.InterNetwork)
			{
				ipendPoint = (IPEndPoint)IPEndPoint.Any.Create(socketAddress);
			}
			else
			{
				ipendPoint = (IPEndPoint)IPEndPoint.IPv6Any.Create(socketAddress);
			}
			unicastIPAddressInformationCollection.InternalAdd(new SystemUnicastIPAddressInformation(ipAdapterUnicastAddress, ipendPoint.Address));
			while (ipAdapterUnicastAddress.next != IntPtr.Zero)
			{
				ipAdapterUnicastAddress = (IpAdapterUnicastAddress)Marshal.PtrToStructure(ipAdapterUnicastAddress.next, typeof(IpAdapterUnicastAddress));
				addressFamily = ((ipAdapterUnicastAddress.address.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork);
				socketAddress = new SocketAddress(addressFamily, ipAdapterUnicastAddress.address.addressLength);
				Marshal.Copy(ipAdapterUnicastAddress.address.address, socketAddress.m_Buffer, 0, ipAdapterUnicastAddress.address.addressLength);
				if (addressFamily == AddressFamily.InterNetwork)
				{
					ipendPoint = (IPEndPoint)IPEndPoint.Any.Create(socketAddress);
				}
				else
				{
					ipendPoint = (IPEndPoint)IPEndPoint.IPv6Any.Create(socketAddress);
				}
				unicastIPAddressInformationCollection.InternalAdd(new SystemUnicastIPAddressInformation(ipAdapterUnicastAddress, ipendPoint.Address));
			}
			return unicastIPAddressInformationCollection;
		}

		// Token: 0x04002E6B RID: 11883
		private IpAdapterUnicastAddress adapterAddress;

		// Token: 0x04002E6C RID: 11884
		private long dhcpLeaseLifetime;

		// Token: 0x04002E6D RID: 11885
		private SystemIPAddressInformation innerInfo;

		// Token: 0x04002E6E RID: 11886
		internal IPAddress ipv4Mask;
	}
}

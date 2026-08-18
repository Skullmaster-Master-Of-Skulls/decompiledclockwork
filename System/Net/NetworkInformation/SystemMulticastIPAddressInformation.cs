using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000634 RID: 1588
	internal class SystemMulticastIPAddressInformation : MulticastIPAddressInformation
	{
		// Token: 0x06003125 RID: 12581 RVA: 0x000D33FE File Offset: 0x000D23FE
		private SystemMulticastIPAddressInformation()
		{
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000D3406 File Offset: 0x000D2406
		internal SystemMulticastIPAddressInformation(IpAdapterAddress adapterAddress, IPAddress ipAddress)
		{
			this.innerInfo = new SystemIPAddressInformation(adapterAddress, ipAddress);
			this.adapterAddress = adapterAddress;
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000D3422 File Offset: 0x000D2422
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000D342F File Offset: 0x000D242F
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000D343C File Offset: 0x000D243C
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000D3449 File Offset: 0x000D2449
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000D3463 File Offset: 0x000D2463
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000D347D File Offset: 0x000D247D
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000D3497 File Offset: 0x000D2497
		public override long AddressValidLifetime
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return 0L;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000D34B2 File Offset: 0x000D24B2
		public override long AddressPreferredLifetime
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return 0L;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000D34CD File Offset: 0x000D24CD
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000D34D4 File Offset: 0x000D24D4
		internal static MulticastIPAddressInformationCollection ToAddressInformationCollection(IntPtr ptr)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			if (ptr == IntPtr.Zero)
			{
				return multicastIPAddressInformationCollection;
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
			multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation(ipAdapterAddress, ipendPoint.Address));
			while (ipAdapterAddress.next != IntPtr.Zero)
			{
				ipAdapterAddress = (IpAdapterAddress)Marshal.PtrToStructure(ipAdapterAddress.next, typeof(IpAdapterAddress));
				addressFamily = ((ipAdapterAddress.address.addressLength > 16) ? AddressFamily.InterNetworkV6 : AddressFamily.InterNetwork);
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
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation(ipAdapterAddress, ipendPoint.Address));
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x04002E69 RID: 11881
		private IpAdapterAddress adapterAddress;

		// Token: 0x04002E6A RID: 11882
		private SystemIPAddressInformation innerInfo;
	}
}

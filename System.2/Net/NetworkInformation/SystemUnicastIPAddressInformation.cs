using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FD RID: 765
	internal class SystemUnicastIPAddressInformation : UnicastIPAddressInformation
	{
		// Token: 0x06001B1C RID: 6940 RVA: 0x00081768 File Offset: 0x0007F968
		internal SystemUnicastIPAddressInformation(IpAdapterUnicastAddress adapterAddress)
		{
			IPAddress ipaddress = adapterAddress.address.MarshalIPAddress();
			this.innerInfo = new SystemIPAddressInformation(ipaddress, adapterAddress.flags);
			this.prefixOrigin = adapterAddress.prefixOrigin;
			this.suffixOrigin = adapterAddress.suffixOrigin;
			this.dadState = adapterAddress.dadState;
			this.validLifetime = adapterAddress.validLifetime;
			this.preferredLifetime = adapterAddress.preferredLifetime;
			this.dhcpLeaseLifetime = (long)((ulong)adapterAddress.leaseLifetime);
			this.prefixLength = adapterAddress.prefixLength;
			if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
			{
				this.ipv4Mask = SystemUnicastIPAddressInformation.PrefixLengthToSubnetMask(this.prefixLength, ipaddress.AddressFamily);
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x0008180F File Offset: 0x0007FA0F
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x0008181C File Offset: 0x0007FA1C
		public override IPAddress IPv4Mask
		{
			get
			{
				if (this.Address.AddressFamily != AddressFamily.InterNetwork)
				{
					return IPAddress.Any;
				}
				return this.ipv4Mask;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00081838 File Offset: 0x0007FA38
		public override int PrefixLength
		{
			get
			{
				return (int)this.prefixLength;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x00081840 File Offset: 0x0007FA40
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0008184D File Offset: 0x0007FA4D
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06001B22 RID: 6946 RVA: 0x0008185A File Offset: 0x0007FA5A
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return this.prefixOrigin;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x00081862 File Offset: 0x0007FA62
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return this.suffixOrigin;
			}
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06001B24 RID: 6948 RVA: 0x0008186A File Offset: 0x0007FA6A
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return this.dadState;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x00081872 File Offset: 0x0007FA72
		public override long AddressValidLifetime
		{
			get
			{
				return (long)((ulong)this.validLifetime);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0008187B File Offset: 0x0007FA7B
		public override long AddressPreferredLifetime
		{
			get
			{
				return (long)((ulong)this.preferredLifetime);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x00081884 File Offset: 0x0007FA84
		public override long DhcpLeaseLifetime
		{
			get
			{
				return this.dhcpLeaseLifetime;
			}
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0008188C File Offset: 0x0007FA8C
		internal static UnicastIPAddressInformationCollection MarshalUnicastIpAddressInformationCollection(IntPtr ptr)
		{
			UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
			while (ptr != IntPtr.Zero)
			{
				IpAdapterUnicastAddress ipAdapterUnicastAddress = (IpAdapterUnicastAddress)Marshal.PtrToStructure(ptr, typeof(IpAdapterUnicastAddress));
				unicastIPAddressInformationCollection.InternalAdd(new SystemUnicastIPAddressInformation(ipAdapterUnicastAddress));
				ptr = ipAdapterUnicastAddress.next;
			}
			return unicastIPAddressInformationCollection;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000818DC File Offset: 0x0007FADC
		private static IPAddress PrefixLengthToSubnetMask(byte prefixLength, AddressFamily family)
		{
			byte[] array;
			if (family == AddressFamily.InterNetwork)
			{
				array = new byte[4];
			}
			else
			{
				array = new byte[16];
			}
			for (int i = 0; i < (int)prefixLength; i++)
			{
				byte[] array2 = array;
				int num = i / 8;
				array2[num] |= (byte)(128 >> i % 8);
			}
			return new IPAddress(array);
		}

		// Token: 0x04001ACB RID: 6859
		private long dhcpLeaseLifetime;

		// Token: 0x04001ACC RID: 6860
		private SystemIPAddressInformation innerInfo;

		// Token: 0x04001ACD RID: 6861
		private IPAddress ipv4Mask;

		// Token: 0x04001ACE RID: 6862
		private PrefixOrigin prefixOrigin;

		// Token: 0x04001ACF RID: 6863
		private SuffixOrigin suffixOrigin;

		// Token: 0x04001AD0 RID: 6864
		private DuplicateAddressDetectionState dadState;

		// Token: 0x04001AD1 RID: 6865
		private uint validLifetime;

		// Token: 0x04001AD2 RID: 6866
		private uint preferredLifetime;

		// Token: 0x04001AD3 RID: 6867
		private byte prefixLength;
	}
}

using System;
using System.Net.Sockets;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F8 RID: 760
	internal class SystemIPInterfaceProperties : IPInterfaceProperties
	{
		// Token: 0x06001AD0 RID: 6864 RVA: 0x000811C8 File Offset: 0x0007F3C8
		internal SystemIPInterfaceProperties(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.adapterFlags = ipAdapterAddresses.flags;
			this.dnsSuffix = ipAdapterAddresses.dnsSuffix;
			this.dnsEnabled = fixedInfo.EnableDns;
			this.dynamicDnsEnabled = ((ipAdapterAddresses.flags & AdapterFlags.DnsEnabled) > (AdapterFlags)0);
			this.multicastAddresses = SystemMulticastIPAddressInformation.ToMulticastIpAddressInformationCollection(IpAdapterAddress.MarshalIpAddressInformationCollection(ipAdapterAddresses.firstMulticastAddress));
			this.dnsAddresses = IpAdapterAddress.MarshalIpAddressCollection(ipAdapterAddresses.firstDnsServerAddress);
			this.anycastAddresses = IpAdapterAddress.MarshalIpAddressInformationCollection(ipAdapterAddresses.firstAnycastAddress);
			this.unicastAddresses = SystemUnicastIPAddressInformation.MarshalUnicastIpAddressInformationCollection(ipAdapterAddresses.firstUnicastAddress);
			this.winsServersAddresses = IpAdapterAddress.MarshalIpAddressCollection(ipAdapterAddresses.firstWinsServerAddress);
			this.gatewayAddresses = SystemGatewayIPAddressInformation.ToGatewayIpAddressInformationCollection(IpAdapterAddress.MarshalIpAddressCollection(ipAdapterAddresses.firstGatewayAddress));
			this.dhcpServers = new IPAddressCollection();
			if (ipAdapterAddresses.dhcpv4Server.address != IntPtr.Zero)
			{
				this.dhcpServers.InternalAdd(ipAdapterAddresses.dhcpv4Server.MarshalIPAddress());
			}
			if (ipAdapterAddresses.dhcpv6Server.address != IntPtr.Zero)
			{
				this.dhcpServers.InternalAdd(ipAdapterAddresses.dhcpv6Server.MarshalIPAddress());
			}
			if ((this.adapterFlags & AdapterFlags.IPv4Enabled) != (AdapterFlags)0)
			{
				this.ipv4Properties = new SystemIPv4InterfaceProperties(fixedInfo, ipAdapterAddresses);
			}
			if ((this.adapterFlags & AdapterFlags.IPv6Enabled) != (AdapterFlags)0)
			{
				this.ipv6Properties = new SystemIPv6InterfaceProperties(ipAdapterAddresses.ipv6Index, ipAdapterAddresses.mtu, ipAdapterAddresses.zoneIndices);
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001AD1 RID: 6865 RVA: 0x0008132E File Offset: 0x0007F52E
		public override bool IsDnsEnabled
		{
			get
			{
				return this.dnsEnabled;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001AD2 RID: 6866 RVA: 0x00081336 File Offset: 0x0007F536
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return this.dynamicDnsEnabled;
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x0008133E File Offset: 0x0007F53E
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			if ((this.adapterFlags & AdapterFlags.IPv4Enabled) == (AdapterFlags)0)
			{
				throw new NetworkInformationException(SocketError.ProtocolNotSupported);
			}
			return this.ipv4Properties;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x0008135F File Offset: 0x0007F55F
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			if ((this.adapterFlags & AdapterFlags.IPv6Enabled) == (AdapterFlags)0)
			{
				throw new NetworkInformationException(SocketError.ProtocolNotSupported);
			}
			return this.ipv6Properties;
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x00081380 File Offset: 0x0007F580
		public override string DnsSuffix
		{
			get
			{
				return this.dnsSuffix;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001AD6 RID: 6870 RVA: 0x00081388 File Offset: 0x0007F588
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				return this.anycastAddresses;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001AD7 RID: 6871 RVA: 0x00081390 File Offset: 0x0007F590
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				return this.unicastAddresses;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001AD8 RID: 6872 RVA: 0x00081398 File Offset: 0x0007F598
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				return this.multicastAddresses;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001AD9 RID: 6873 RVA: 0x000813A0 File Offset: 0x0007F5A0
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return this.dnsAddresses;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001ADA RID: 6874 RVA: 0x000813A8 File Offset: 0x0007F5A8
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				return this.gatewayAddresses;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001ADB RID: 6875 RVA: 0x000813B0 File Offset: 0x0007F5B0
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				return this.dhcpServers;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001ADC RID: 6876 RVA: 0x000813B8 File Offset: 0x0007F5B8
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				return this.winsServersAddresses;
			}
		}

		// Token: 0x04001ABA RID: 6842
		private bool dnsEnabled;

		// Token: 0x04001ABB RID: 6843
		private bool dynamicDnsEnabled;

		// Token: 0x04001ABC RID: 6844
		private IPAddressCollection dnsAddresses;

		// Token: 0x04001ABD RID: 6845
		private UnicastIPAddressInformationCollection unicastAddresses;

		// Token: 0x04001ABE RID: 6846
		private MulticastIPAddressInformationCollection multicastAddresses;

		// Token: 0x04001ABF RID: 6847
		private IPAddressInformationCollection anycastAddresses;

		// Token: 0x04001AC0 RID: 6848
		private AdapterFlags adapterFlags;

		// Token: 0x04001AC1 RID: 6849
		private string dnsSuffix;

		// Token: 0x04001AC2 RID: 6850
		private SystemIPv4InterfaceProperties ipv4Properties;

		// Token: 0x04001AC3 RID: 6851
		private SystemIPv6InterfaceProperties ipv6Properties;

		// Token: 0x04001AC4 RID: 6852
		private IPAddressCollection winsServersAddresses;

		// Token: 0x04001AC5 RID: 6853
		private GatewayIPAddressInformationCollection gatewayAddresses;

		// Token: 0x04001AC6 RID: 6854
		private IPAddressCollection dhcpServers;
	}
}

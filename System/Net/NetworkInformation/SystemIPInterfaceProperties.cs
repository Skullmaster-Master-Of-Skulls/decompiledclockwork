using System;
using System.Collections;
using System.Net.Sockets;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000631 RID: 1585
	internal class SystemIPInterfaceProperties : IPInterfaceProperties
	{
		// Token: 0x060030EA RID: 12522 RVA: 0x000D2BCF File Offset: 0x000D1BCF
		private SystemIPInterfaceProperties()
		{
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000D2BD8 File Offset: 0x000D1BD8
		internal SystemIPInterfaceProperties(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.dnsEnabled = fixedInfo.EnableDns;
			this.index = ipAdapterAddresses.index;
			this.name = ipAdapterAddresses.AdapterName;
			this.ipv6Index = ipAdapterAddresses.ipv6Index;
			if (this.index > 0U)
			{
				this.versionSupported |= IPVersion.IPv4;
			}
			if (this.ipv6Index > 0U)
			{
				this.versionSupported |= IPVersion.IPv6;
			}
			this.mtu = ipAdapterAddresses.mtu;
			this.adapterFlags = ipAdapterAddresses.flags;
			this.dnsSuffix = ipAdapterAddresses.dnsSuffix;
			this.dynamicDnsEnabled = ((ipAdapterAddresses.flags & AdapterFlags.DnsEnabled) > (AdapterFlags)0);
			this.multicastAddresses = SystemMulticastIPAddressInformation.ToAddressInformationCollection(ipAdapterAddresses.FirstMulticastAddress);
			this.dnsAddresses = SystemIPAddressInformation.ToAddressCollection(ipAdapterAddresses.FirstDnsServerAddress, this.versionSupported);
			this.anycastAddresses = SystemIPAddressInformation.ToAddressInformationCollection(ipAdapterAddresses.FirstAnycastAddress, this.versionSupported);
			this.unicastAddresses = SystemUnicastIPAddressInformation.ToAddressInformationCollection(ipAdapterAddresses.FirstUnicastAddress);
			if (this.ipv6Index > 0U)
			{
				this.ipv6Properties = new SystemIPv6InterfaceProperties(this.ipv6Index, this.mtu);
			}
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000D2CFC File Offset: 0x000D1CFC
		internal SystemIPInterfaceProperties(FixedInfo fixedInfo, IpAdapterInfo ipAdapterInfo)
		{
			this.dnsEnabled = fixedInfo.EnableDns;
			this.name = ipAdapterInfo.adapterName;
			this.index = ipAdapterInfo.index;
			this.multicastAddresses = new MulticastIPAddressInformationCollection();
			this.anycastAddresses = new IPAddressInformationCollection();
			if (this.index > 0U)
			{
				this.versionSupported |= IPVersion.IPv4;
			}
			if (ComNetOS.IsWin2K)
			{
				this.ReadRegDnsSuffix();
			}
			this.unicastAddresses = new UnicastIPAddressInformationCollection();
			ArrayList arrayList = ipAdapterInfo.ipAddressList.ToIPExtendedAddressArrayList();
			foreach (object obj in arrayList)
			{
				IPExtendedAddress address = (IPExtendedAddress)obj;
				this.unicastAddresses.InternalAdd(new SystemUnicastIPAddressInformation(ipAdapterInfo, address));
			}
			try
			{
				this.ipv4Properties = new SystemIPv4InterfaceProperties(fixedInfo, ipAdapterInfo);
				if (this.dnsAddresses == null || this.dnsAddresses.Count == 0)
				{
					this.dnsAddresses = this.ipv4Properties.DnsAddresses;
				}
			}
			catch (NetworkInformationException ex)
			{
				if ((long)ex.ErrorCode != 87L)
				{
					throw;
				}
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x000D2E30 File Offset: 0x000D1E30
		public override bool IsDnsEnabled
		{
			get
			{
				return this.dnsEnabled;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000D2E38 File Offset: 0x000D1E38
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return this.dynamicDnsEnabled;
			}
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000D2E40 File Offset: 0x000D1E40
		public override IPv4InterfaceProperties GetIPv4Properties()
		{
			if (this.index == 0U)
			{
				throw new NetworkInformationException(SocketError.ProtocolNotSupported);
			}
			return this.ipv4Properties;
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000D2E5B File Offset: 0x000D1E5B
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			if (this.ipv6Index == 0U)
			{
				throw new NetworkInformationException(SocketError.ProtocolNotSupported);
			}
			return this.ipv6Properties;
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000D2E76 File Offset: 0x000D1E76
		public override string DnsSuffix
		{
			get
			{
				if (!ComNetOS.IsWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("Win2000Required"));
				}
				return this.dnsSuffix;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000D2E95 File Offset: 0x000D1E95
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				return this.anycastAddresses;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x000D2E9D File Offset: 0x000D1E9D
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				return this.unicastAddresses;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x000D2EA5 File Offset: 0x000D1EA5
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				return this.multicastAddresses;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000D2EAD File Offset: 0x000D1EAD
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				return this.dnsAddresses;
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000D2EB5 File Offset: 0x000D1EB5
		public override GatewayIPAddressInformationCollection GatewayAddresses
		{
			get
			{
				if (this.ipv4Properties != null)
				{
					return this.ipv4Properties.GetGatewayAddresses();
				}
				return new GatewayIPAddressInformationCollection();
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000D2ED0 File Offset: 0x000D1ED0
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				if (this.ipv4Properties != null)
				{
					return this.ipv4Properties.GetDhcpServerAddresses();
				}
				return new IPAddressCollection();
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000D2EEB File Offset: 0x000D1EEB
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				if (this.ipv4Properties != null)
				{
					return this.ipv4Properties.GetWinsServersAddresses();
				}
				return new IPAddressCollection();
			}
		}

		// Token: 0x060030F9 RID: 12537 RVA: 0x000D2F08 File Offset: 0x000D1F08
		internal bool Update(FixedInfo fixedInfo, IpAdapterInfo ipAdapterInfo)
		{
			try
			{
				ArrayList arrayList = ipAdapterInfo.ipAddressList.ToIPExtendedAddressArrayList();
				foreach (object obj in arrayList)
				{
					IPExtendedAddress ipextendedAddress = (IPExtendedAddress)obj;
					foreach (UnicastIPAddressInformation unicastIPAddressInformation in this.unicastAddresses)
					{
						SystemUnicastIPAddressInformation systemUnicastIPAddressInformation = (SystemUnicastIPAddressInformation)unicastIPAddressInformation;
						if (ipextendedAddress.address.Equals(systemUnicastIPAddressInformation.Address))
						{
							systemUnicastIPAddressInformation.ipv4Mask = ipextendedAddress.mask;
						}
					}
				}
				this.ipv4Properties = new SystemIPv4InterfaceProperties(fixedInfo, ipAdapterInfo);
				if (this.dnsAddresses == null || this.dnsAddresses.Count == 0)
				{
					this.dnsAddresses = this.ipv4Properties.DnsAddresses;
				}
			}
			catch (NetworkInformationException ex)
			{
				if ((long)ex.ErrorCode == 87L || (long)ex.ErrorCode == 13L || (long)ex.ErrorCode == 232L || (long)ex.ErrorCode == 1L || (long)ex.ErrorCode == 2L)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000D3054 File Offset: 0x000D2054
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces")]
		private void ReadRegDnsSuffix()
		{
			RegistryKey registryKey = null;
			try
			{
				string text = "SYSTEM\\CurrentControlSet\\Services\\Tcpip\\Parameters\\Interfaces\\" + this.name;
				registryKey = Registry.LocalMachine.OpenSubKey(text);
				if (registryKey != null)
				{
					this.dnsSuffix = (string)registryKey.GetValue("DhcpDomain");
					if (this.dnsSuffix == null)
					{
						this.dnsSuffix = (string)registryKey.GetValue("Domain");
						if (this.dnsSuffix == null)
						{
							this.dnsSuffix = string.Empty;
						}
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
		}

		// Token: 0x04002E58 RID: 11864
		private uint mtu;

		// Token: 0x04002E59 RID: 11865
		internal uint index;

		// Token: 0x04002E5A RID: 11866
		internal uint ipv6Index;

		// Token: 0x04002E5B RID: 11867
		internal IPVersion versionSupported;

		// Token: 0x04002E5C RID: 11868
		private bool dnsEnabled;

		// Token: 0x04002E5D RID: 11869
		private bool dynamicDnsEnabled;

		// Token: 0x04002E5E RID: 11870
		private IPAddressCollection dnsAddresses;

		// Token: 0x04002E5F RID: 11871
		private UnicastIPAddressInformationCollection unicastAddresses;

		// Token: 0x04002E60 RID: 11872
		private MulticastIPAddressInformationCollection multicastAddresses;

		// Token: 0x04002E61 RID: 11873
		private IPAddressInformationCollection anycastAddresses;

		// Token: 0x04002E62 RID: 11874
		private AdapterFlags adapterFlags;

		// Token: 0x04002E63 RID: 11875
		private string dnsSuffix;

		// Token: 0x04002E64 RID: 11876
		private string name;

		// Token: 0x04002E65 RID: 11877
		private SystemIPv4InterfaceProperties ipv4Properties;

		// Token: 0x04002E66 RID: 11878
		private SystemIPv6InterfaceProperties ipv6Properties;
	}
}

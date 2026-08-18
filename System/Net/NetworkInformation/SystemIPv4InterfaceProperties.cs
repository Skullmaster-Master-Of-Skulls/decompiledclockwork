using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000636 RID: 1590
	internal class SystemIPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x0600313F RID: 12607 RVA: 0x000D397C File Offset: 0x000D297C
		internal SystemIPv4InterfaceProperties(FixedInfo fixedInfo, IpAdapterInfo ipAdapterInfo)
		{
			this.index = ipAdapterInfo.index;
			this.routingEnabled = fixedInfo.EnableRouting;
			this.dhcpEnabled = ipAdapterInfo.dhcpEnabled;
			this.haveWins = ipAdapterInfo.haveWins;
			this.gatewayAddresses = ipAdapterInfo.gatewayList.ToIPGatewayAddressCollection();
			this.dhcpAddresses = ipAdapterInfo.dhcpServer.ToIPAddressCollection();
			IPAddressCollection ipaddressCollection = ipAdapterInfo.primaryWinsServer.ToIPAddressCollection();
			IPAddressCollection ipaddressCollection2 = ipAdapterInfo.secondaryWinsServer.ToIPAddressCollection();
			this.winsServerAddresses = new IPAddressCollection();
			foreach (IPAddress address in ipaddressCollection)
			{
				this.winsServerAddresses.InternalAdd(address);
			}
			foreach (IPAddress address2 in ipaddressCollection2)
			{
				this.winsServerAddresses.InternalAdd(address2);
			}
			SystemIPv4InterfaceStatistics systemIPv4InterfaceStatistics = new SystemIPv4InterfaceStatistics((long)((ulong)this.index));
			this.mtu = (uint)systemIPv4InterfaceStatistics.Mtu;
			if (ComNetOS.IsWin2K)
			{
				this.GetPerAdapterInfo(ipAdapterInfo.index);
				return;
			}
			this.dnsAddresses = fixedInfo.DnsAddresses;
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06003140 RID: 12608 RVA: 0x000D3AD0 File Offset: 0x000D2AD0
		internal IPAddressCollection DnsAddresses
		{
			get
			{
				return this.dnsAddresses;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000D3AD8 File Offset: 0x000D2AD8
		public override bool UsesWins
		{
			get
			{
				return this.haveWins;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06003142 RID: 12610 RVA: 0x000D3AE0 File Offset: 0x000D2AE0
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.dhcpEnabled;
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000D3AE8 File Offset: 0x000D2AE8
		public override bool IsForwardingEnabled
		{
			get
			{
				return this.routingEnabled;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06003144 RID: 12612 RVA: 0x000D3AF0 File Offset: 0x000D2AF0
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.autoConfigEnabled;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000D3AF8 File Offset: 0x000D2AF8
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.autoConfigActive;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000D3B00 File Offset: 0x000D2B00
		public override int Mtu
		{
			get
			{
				return (int)this.mtu;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000D3B08 File Offset: 0x000D2B08
		public override int Index
		{
			get
			{
				return (int)this.index;
			}
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x000D3B10 File Offset: 0x000D2B10
		internal GatewayIPAddressInformationCollection GetGatewayAddresses()
		{
			return this.gatewayAddresses;
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000D3B18 File Offset: 0x000D2B18
		internal IPAddressCollection GetDhcpServerAddresses()
		{
			return this.dhcpAddresses;
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000D3B20 File Offset: 0x000D2B20
		internal IPAddressCollection GetWinsServersAddresses()
		{
			return this.winsServerAddresses;
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000D3B28 File Offset: 0x000D2B28
		private void GetPerAdapterInfo(uint index)
		{
			if (index != 0U)
			{
				uint cb = 0U;
				SafeLocalFree safeLocalFree = null;
				uint perAdapterInfo = UnsafeNetInfoNativeMethods.GetPerAdapterInfo(index, SafeLocalFree.Zero, ref cb);
				while (perAdapterInfo == 111U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
						perAdapterInfo = UnsafeNetInfoNativeMethods.GetPerAdapterInfo(index, safeLocalFree, ref cb);
						if (perAdapterInfo == 0U)
						{
							IpPerAdapterInfo ipPerAdapterInfo = (IpPerAdapterInfo)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(IpPerAdapterInfo));
							this.autoConfigEnabled = ipPerAdapterInfo.autoconfigEnabled;
							this.autoConfigActive = ipPerAdapterInfo.autoconfigActive;
							this.dnsAddresses = ipPerAdapterInfo.dnsServerList.ToIPAddressCollection();
						}
					}
					finally
					{
						if (this.dnsAddresses == null)
						{
							this.dnsAddresses = new IPAddressCollection();
						}
						if (safeLocalFree != null)
						{
							safeLocalFree.Close();
						}
					}
				}
				if (this.dnsAddresses == null)
				{
					this.dnsAddresses = new IPAddressCollection();
				}
				if (perAdapterInfo != 0U)
				{
					throw new NetworkInformationException((int)perAdapterInfo);
				}
			}
		}

		// Token: 0x04002E6F RID: 11887
		private bool haveWins;

		// Token: 0x04002E70 RID: 11888
		private bool dhcpEnabled;

		// Token: 0x04002E71 RID: 11889
		private bool routingEnabled;

		// Token: 0x04002E72 RID: 11890
		private bool autoConfigEnabled;

		// Token: 0x04002E73 RID: 11891
		private bool autoConfigActive;

		// Token: 0x04002E74 RID: 11892
		private uint index;

		// Token: 0x04002E75 RID: 11893
		private uint mtu;

		// Token: 0x04002E76 RID: 11894
		private GatewayIPAddressInformationCollection gatewayAddresses;

		// Token: 0x04002E77 RID: 11895
		private IPAddressCollection dhcpAddresses;

		// Token: 0x04002E78 RID: 11896
		private IPAddressCollection winsServerAddresses;

		// Token: 0x04002E79 RID: 11897
		internal IPAddressCollection dnsAddresses;
	}
}

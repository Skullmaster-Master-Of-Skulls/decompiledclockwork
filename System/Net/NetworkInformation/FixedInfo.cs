using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000630 RID: 1584
	internal struct FixedInfo
	{
		// Token: 0x060030E1 RID: 12513 RVA: 0x000D2B51 File Offset: 0x000D1B51
		internal FixedInfo(FIXED_INFO info)
		{
			this.info = info;
			this.dnsAddresses = info.DnsServerList.ToIPAddressCollection();
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x000D2B6C File Offset: 0x000D1B6C
		internal IPAddressCollection DnsAddresses
		{
			get
			{
				return this.dnsAddresses;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x000D2B74 File Offset: 0x000D1B74
		internal string HostName
		{
			get
			{
				return this.info.hostName;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000D2B81 File Offset: 0x000D1B81
		internal string DomainName
		{
			get
			{
				return this.info.domainName;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x000D2B8E File Offset: 0x000D1B8E
		internal NetBiosNodeType NodeType
		{
			get
			{
				return this.info.nodeType;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000D2B9B File Offset: 0x000D1B9B
		internal string ScopeId
		{
			get
			{
				return this.info.scopeId;
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x000D2BA8 File Offset: 0x000D1BA8
		internal bool EnableRouting
		{
			get
			{
				return this.info.enableRouting;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060030E8 RID: 12520 RVA: 0x000D2BB5 File Offset: 0x000D1BB5
		internal bool EnableProxy
		{
			get
			{
				return this.info.enableProxy;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060030E9 RID: 12521 RVA: 0x000D2BC2 File Offset: 0x000D1BC2
		internal bool EnableDns
		{
			get
			{
				return this.info.enableDns;
			}
		}

		// Token: 0x04002E56 RID: 11862
		internal FIXED_INFO info;

		// Token: 0x04002E57 RID: 11863
		internal IPAddressCollection dnsAddresses;
	}
}

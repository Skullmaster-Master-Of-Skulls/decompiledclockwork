using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F7 RID: 759
	internal struct FixedInfo
	{
		// Token: 0x06001AC8 RID: 6856 RVA: 0x00081164 File Offset: 0x0007F364
		internal FixedInfo(FIXED_INFO info)
		{
			this.info = info;
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x0008116D File Offset: 0x0007F36D
		internal string HostName
		{
			get
			{
				return this.info.hostName;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001ACA RID: 6858 RVA: 0x0008117A File Offset: 0x0007F37A
		internal string DomainName
		{
			get
			{
				return this.info.domainName;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x00081187 File Offset: 0x0007F387
		internal NetBiosNodeType NodeType
		{
			get
			{
				return this.info.nodeType;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001ACC RID: 6860 RVA: 0x00081194 File Offset: 0x0007F394
		internal string ScopeId
		{
			get
			{
				return this.info.scopeId;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x000811A1 File Offset: 0x0007F3A1
		internal bool EnableRouting
		{
			get
			{
				return this.info.enableRouting;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001ACE RID: 6862 RVA: 0x000811AE File Offset: 0x0007F3AE
		internal bool EnableProxy
		{
			get
			{
				return this.info.enableProxy;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001ACF RID: 6863 RVA: 0x000811BB File Offset: 0x0007F3BB
		internal bool EnableDns
		{
			get
			{
				return this.info.enableDns;
			}
		}

		// Token: 0x04001AB9 RID: 6841
		internal FIXED_INFO info;
	}
}

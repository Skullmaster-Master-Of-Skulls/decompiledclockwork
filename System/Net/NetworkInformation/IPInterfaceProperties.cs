using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005DE RID: 1502
	public abstract class IPInterfaceProperties
	{
		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002F6C RID: 12140
		public abstract bool IsDnsEnabled { get; }

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x06002F6D RID: 12141
		public abstract string DnsSuffix { get; }

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x06002F6E RID: 12142
		public abstract bool IsDynamicDnsEnabled { get; }

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06002F6F RID: 12143
		public abstract UnicastIPAddressInformationCollection UnicastAddresses { get; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06002F70 RID: 12144
		public abstract MulticastIPAddressInformationCollection MulticastAddresses { get; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06002F71 RID: 12145
		public abstract IPAddressInformationCollection AnycastAddresses { get; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002F72 RID: 12146
		public abstract IPAddressCollection DnsAddresses { get; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002F73 RID: 12147
		public abstract GatewayIPAddressInformationCollection GatewayAddresses { get; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002F74 RID: 12148
		public abstract IPAddressCollection DhcpServerAddresses { get; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002F75 RID: 12149
		public abstract IPAddressCollection WinsServersAddresses { get; }

		// Token: 0x06002F76 RID: 12150
		public abstract IPv4InterfaceProperties GetIPv4Properties();

		// Token: 0x06002F77 RID: 12151
		public abstract IPv6InterfaceProperties GetIPv6Properties();
	}
}

using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005DC RID: 1500
	public abstract class IPGlobalProperties
	{
		// Token: 0x06002F42 RID: 12098 RVA: 0x000CF03D File Offset: 0x000CE03D
		public static IPGlobalProperties GetIPGlobalProperties()
		{
			new NetworkInformationPermission(NetworkInformationAccess.Read).Demand();
			return new SystemIPGlobalProperties();
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x000CF04F File Offset: 0x000CE04F
		internal static IPGlobalProperties InternalGetIPGlobalProperties()
		{
			return new SystemIPGlobalProperties();
		}

		// Token: 0x06002F44 RID: 12100
		public abstract IPEndPoint[] GetActiveUdpListeners();

		// Token: 0x06002F45 RID: 12101
		public abstract IPEndPoint[] GetActiveTcpListeners();

		// Token: 0x06002F46 RID: 12102
		public abstract TcpConnectionInformation[] GetActiveTcpConnections();

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002F47 RID: 12103
		public abstract string DhcpScopeName { get; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002F48 RID: 12104
		public abstract string DomainName { get; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002F49 RID: 12105
		public abstract string HostName { get; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002F4A RID: 12106
		public abstract bool IsWinsProxy { get; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002F4B RID: 12107
		public abstract NetBiosNodeType NodeType { get; }

		// Token: 0x06002F4C RID: 12108
		public abstract TcpStatistics GetTcpIPv4Statistics();

		// Token: 0x06002F4D RID: 12109
		public abstract TcpStatistics GetTcpIPv6Statistics();

		// Token: 0x06002F4E RID: 12110
		public abstract UdpStatistics GetUdpIPv4Statistics();

		// Token: 0x06002F4F RID: 12111
		public abstract UdpStatistics GetUdpIPv6Statistics();

		// Token: 0x06002F50 RID: 12112
		public abstract IcmpV4Statistics GetIcmpV4Statistics();

		// Token: 0x06002F51 RID: 12113
		public abstract IcmpV6Statistics GetIcmpV6Statistics();

		// Token: 0x06002F52 RID: 12114
		public abstract IPGlobalStatistics GetIPv4GlobalStatistics();

		// Token: 0x06002F53 RID: 12115
		public abstract IPGlobalStatistics GetIPv6GlobalStatistics();
	}
}

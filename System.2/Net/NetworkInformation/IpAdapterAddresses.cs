using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002BE RID: 702
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct IpAdapterAddresses
	{
		// Token: 0x04001961 RID: 6497
		internal const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x04001962 RID: 6498
		internal uint length;

		// Token: 0x04001963 RID: 6499
		internal uint index;

		// Token: 0x04001964 RID: 6500
		internal IntPtr next;

		// Token: 0x04001965 RID: 6501
		[MarshalAs(UnmanagedType.LPStr)]
		internal string AdapterName;

		// Token: 0x04001966 RID: 6502
		internal IntPtr firstUnicastAddress;

		// Token: 0x04001967 RID: 6503
		internal IntPtr firstAnycastAddress;

		// Token: 0x04001968 RID: 6504
		internal IntPtr firstMulticastAddress;

		// Token: 0x04001969 RID: 6505
		internal IntPtr firstDnsServerAddress;

		// Token: 0x0400196A RID: 6506
		internal string dnsSuffix;

		// Token: 0x0400196B RID: 6507
		internal string description;

		// Token: 0x0400196C RID: 6508
		internal string friendlyName;

		// Token: 0x0400196D RID: 6509
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		internal byte[] address;

		// Token: 0x0400196E RID: 6510
		internal uint addressLength;

		// Token: 0x0400196F RID: 6511
		internal AdapterFlags flags;

		// Token: 0x04001970 RID: 6512
		internal uint mtu;

		// Token: 0x04001971 RID: 6513
		internal NetworkInterfaceType type;

		// Token: 0x04001972 RID: 6514
		internal OperationalStatus operStatus;

		// Token: 0x04001973 RID: 6515
		internal uint ipv6Index;

		// Token: 0x04001974 RID: 6516
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal uint[] zoneIndices;

		// Token: 0x04001975 RID: 6517
		internal IntPtr firstPrefix;

		// Token: 0x04001976 RID: 6518
		internal ulong transmitLinkSpeed;

		// Token: 0x04001977 RID: 6519
		internal ulong receiveLinkSpeed;

		// Token: 0x04001978 RID: 6520
		internal IntPtr firstWinsServerAddress;

		// Token: 0x04001979 RID: 6521
		internal IntPtr firstGatewayAddress;

		// Token: 0x0400197A RID: 6522
		internal uint ipv4Metric;

		// Token: 0x0400197B RID: 6523
		internal uint ipv6Metric;

		// Token: 0x0400197C RID: 6524
		internal ulong luid;

		// Token: 0x0400197D RID: 6525
		internal IpSocketAddress dhcpv4Server;

		// Token: 0x0400197E RID: 6526
		internal uint compartmentId;

		// Token: 0x0400197F RID: 6527
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] networkGuid;

		// Token: 0x04001980 RID: 6528
		internal InterfaceConnectionType connectionType;

		// Token: 0x04001981 RID: 6529
		internal InterfaceTunnelType tunnelType;

		// Token: 0x04001982 RID: 6530
		internal IpSocketAddress dhcpv6Server;

		// Token: 0x04001983 RID: 6531
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 130)]
		internal byte[] dhcpv6ClientDuid;

		// Token: 0x04001984 RID: 6532
		internal uint dhcpv6ClientDuidLength;

		// Token: 0x04001985 RID: 6533
		internal uint dhcpV6Iaid;
	}
}

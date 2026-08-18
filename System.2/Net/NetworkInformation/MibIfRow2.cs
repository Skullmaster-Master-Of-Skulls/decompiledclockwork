using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C2 RID: 706
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct MibIfRow2
	{
		// Token: 0x04001997 RID: 6551
		private const int GuidLength = 16;

		// Token: 0x04001998 RID: 6552
		private const int IfMaxStringSize = 256;

		// Token: 0x04001999 RID: 6553
		private const int IfMaxPhysAddressLength = 32;

		// Token: 0x0400199A RID: 6554
		internal ulong interfaceLuid;

		// Token: 0x0400199B RID: 6555
		internal uint interfaceIndex;

		// Token: 0x0400199C RID: 6556
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] interfaceGuid;

		// Token: 0x0400199D RID: 6557
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 257)]
		internal char[] alias;

		// Token: 0x0400199E RID: 6558
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 257)]
		internal char[] description;

		// Token: 0x0400199F RID: 6559
		internal uint physicalAddressLength;

		// Token: 0x040019A0 RID: 6560
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] physicalAddress;

		// Token: 0x040019A1 RID: 6561
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		internal byte[] permanentPhysicalAddress;

		// Token: 0x040019A2 RID: 6562
		internal uint mtu;

		// Token: 0x040019A3 RID: 6563
		internal NetworkInterfaceType type;

		// Token: 0x040019A4 RID: 6564
		internal InterfaceTunnelType tunnelType;

		// Token: 0x040019A5 RID: 6565
		internal uint mediaType;

		// Token: 0x040019A6 RID: 6566
		internal uint physicalMediumType;

		// Token: 0x040019A7 RID: 6567
		internal uint accessType;

		// Token: 0x040019A8 RID: 6568
		internal uint directionType;

		// Token: 0x040019A9 RID: 6569
		internal byte interfaceAndOperStatusFlags;

		// Token: 0x040019AA RID: 6570
		internal OperationalStatus operStatus;

		// Token: 0x040019AB RID: 6571
		internal uint adminStatus;

		// Token: 0x040019AC RID: 6572
		internal uint mediaConnectState;

		// Token: 0x040019AD RID: 6573
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] networkGuid;

		// Token: 0x040019AE RID: 6574
		internal InterfaceConnectionType connectionType;

		// Token: 0x040019AF RID: 6575
		internal ulong transmitLinkSpeed;

		// Token: 0x040019B0 RID: 6576
		internal ulong receiveLinkSpeed;

		// Token: 0x040019B1 RID: 6577
		internal ulong inOctets;

		// Token: 0x040019B2 RID: 6578
		internal ulong inUcastPkts;

		// Token: 0x040019B3 RID: 6579
		internal ulong inNUcastPkts;

		// Token: 0x040019B4 RID: 6580
		internal ulong inDiscards;

		// Token: 0x040019B5 RID: 6581
		internal ulong inErrors;

		// Token: 0x040019B6 RID: 6582
		internal ulong inUnknownProtos;

		// Token: 0x040019B7 RID: 6583
		internal ulong inUcastOctets;

		// Token: 0x040019B8 RID: 6584
		internal ulong inMulticastOctets;

		// Token: 0x040019B9 RID: 6585
		internal ulong inBroadcastOctets;

		// Token: 0x040019BA RID: 6586
		internal ulong outOctets;

		// Token: 0x040019BB RID: 6587
		internal ulong outUcastPkts;

		// Token: 0x040019BC RID: 6588
		internal ulong outNUcastPkts;

		// Token: 0x040019BD RID: 6589
		internal ulong outDiscards;

		// Token: 0x040019BE RID: 6590
		internal ulong outErrors;

		// Token: 0x040019BF RID: 6591
		internal ulong outUcastOctets;

		// Token: 0x040019C0 RID: 6592
		internal ulong outMulticastOctets;

		// Token: 0x040019C1 RID: 6593
		internal ulong outBroadcastOctets;

		// Token: 0x040019C2 RID: 6594
		internal ulong outQLen;
	}
}

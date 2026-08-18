using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005FE RID: 1534
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct MibIfRow
	{
		// Token: 0x04002D4A RID: 11594
		internal const int MAX_INTERFACE_NAME_LEN = 256;

		// Token: 0x04002D4B RID: 11595
		internal const int MAXLEN_IFDESCR = 256;

		// Token: 0x04002D4C RID: 11596
		internal const int MAXLEN_PHYSADDR = 8;

		// Token: 0x04002D4D RID: 11597
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		internal string wszName;

		// Token: 0x04002D4E RID: 11598
		internal uint dwIndex;

		// Token: 0x04002D4F RID: 11599
		internal uint dwType;

		// Token: 0x04002D50 RID: 11600
		internal uint dwMtu;

		// Token: 0x04002D51 RID: 11601
		internal uint dwSpeed;

		// Token: 0x04002D52 RID: 11602
		internal uint dwPhysAddrLen;

		// Token: 0x04002D53 RID: 11603
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		internal byte[] bPhysAddr;

		// Token: 0x04002D54 RID: 11604
		internal uint dwAdminStatus;

		// Token: 0x04002D55 RID: 11605
		internal OldOperationalStatus operStatus;

		// Token: 0x04002D56 RID: 11606
		internal uint dwLastChange;

		// Token: 0x04002D57 RID: 11607
		internal uint dwInOctets;

		// Token: 0x04002D58 RID: 11608
		internal uint dwInUcastPkts;

		// Token: 0x04002D59 RID: 11609
		internal uint dwInNUcastPkts;

		// Token: 0x04002D5A RID: 11610
		internal uint dwInDiscards;

		// Token: 0x04002D5B RID: 11611
		internal uint dwInErrors;

		// Token: 0x04002D5C RID: 11612
		internal uint dwInUnknownProtos;

		// Token: 0x04002D5D RID: 11613
		internal uint dwOutOctets;

		// Token: 0x04002D5E RID: 11614
		internal uint dwOutUcastPkts;

		// Token: 0x04002D5F RID: 11615
		internal uint dwOutNUcastPkts;

		// Token: 0x04002D60 RID: 11616
		internal uint dwOutDiscards;

		// Token: 0x04002D61 RID: 11617
		internal uint dwOutErrors;

		// Token: 0x04002D62 RID: 11618
		internal uint dwOutQLen;

		// Token: 0x04002D63 RID: 11619
		internal uint dwDescrLen;

		// Token: 0x04002D64 RID: 11620
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal byte[] bDescr;
	}
}

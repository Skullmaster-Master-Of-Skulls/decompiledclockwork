using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005F7 RID: 1527
	internal struct IpAdapterInfo
	{
		// Token: 0x04002D07 RID: 11527
		internal const int MAX_ADAPTER_DESCRIPTION_LENGTH = 128;

		// Token: 0x04002D08 RID: 11528
		internal const int MAX_ADAPTER_NAME_LENGTH = 256;

		// Token: 0x04002D09 RID: 11529
		internal const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x04002D0A RID: 11530
		internal IntPtr Next;

		// Token: 0x04002D0B RID: 11531
		internal uint comboIndex;

		// Token: 0x04002D0C RID: 11532
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		internal string adapterName;

		// Token: 0x04002D0D RID: 11533
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 132)]
		internal string description;

		// Token: 0x04002D0E RID: 11534
		internal uint addressLength;

		// Token: 0x04002D0F RID: 11535
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		internal byte[] address;

		// Token: 0x04002D10 RID: 11536
		internal uint index;

		// Token: 0x04002D11 RID: 11537
		internal OldInterfaceType type;

		// Token: 0x04002D12 RID: 11538
		internal bool dhcpEnabled;

		// Token: 0x04002D13 RID: 11539
		internal IntPtr currentIpAddress;

		// Token: 0x04002D14 RID: 11540
		internal IpAddrString ipAddressList;

		// Token: 0x04002D15 RID: 11541
		internal IpAddrString gatewayList;

		// Token: 0x04002D16 RID: 11542
		internal IpAddrString dhcpServer;

		// Token: 0x04002D17 RID: 11543
		[MarshalAs(UnmanagedType.Bool)]
		internal bool haveWins;

		// Token: 0x04002D18 RID: 11544
		internal IpAddrString primaryWinsServer;

		// Token: 0x04002D19 RID: 11545
		internal IpAddrString secondaryWinsServer;

		// Token: 0x04002D1A RID: 11546
		internal uint leaseObtained;

		// Token: 0x04002D1B RID: 11547
		internal uint leaseExpires;
	}
}

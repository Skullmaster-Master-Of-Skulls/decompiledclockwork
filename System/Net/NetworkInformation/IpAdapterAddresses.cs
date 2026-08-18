using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020005FC RID: 1532
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal struct IpAdapterAddresses
	{
		// Token: 0x04002D31 RID: 11569
		internal const int MAX_ADAPTER_ADDRESS_LENGTH = 8;

		// Token: 0x04002D32 RID: 11570
		internal uint length;

		// Token: 0x04002D33 RID: 11571
		internal uint index;

		// Token: 0x04002D34 RID: 11572
		internal IntPtr next;

		// Token: 0x04002D35 RID: 11573
		[MarshalAs(UnmanagedType.LPStr)]
		internal string AdapterName;

		// Token: 0x04002D36 RID: 11574
		internal IntPtr FirstUnicastAddress;

		// Token: 0x04002D37 RID: 11575
		internal IntPtr FirstAnycastAddress;

		// Token: 0x04002D38 RID: 11576
		internal IntPtr FirstMulticastAddress;

		// Token: 0x04002D39 RID: 11577
		internal IntPtr FirstDnsServerAddress;

		// Token: 0x04002D3A RID: 11578
		internal string dnsSuffix;

		// Token: 0x04002D3B RID: 11579
		internal string description;

		// Token: 0x04002D3C RID: 11580
		internal string friendlyName;

		// Token: 0x04002D3D RID: 11581
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		internal byte[] address;

		// Token: 0x04002D3E RID: 11582
		internal uint addressLength;

		// Token: 0x04002D3F RID: 11583
		internal AdapterFlags flags;

		// Token: 0x04002D40 RID: 11584
		internal uint mtu;

		// Token: 0x04002D41 RID: 11585
		internal NetworkInterfaceType type;

		// Token: 0x04002D42 RID: 11586
		internal OperationalStatus operStatus;

		// Token: 0x04002D43 RID: 11587
		internal uint ipv6Index;

		// Token: 0x04002D44 RID: 11588
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal uint[] zoneIndices;

		// Token: 0x04002D45 RID: 11589
		internal IntPtr firstPrefix;
	}
}

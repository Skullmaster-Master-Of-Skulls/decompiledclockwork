using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200060C RID: 1548
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct Ipv6Address
	{
		// Token: 0x04002DC2 RID: 11714
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		internal byte[] Goo;

		// Token: 0x04002DC3 RID: 11715
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] Address;

		// Token: 0x04002DC4 RID: 11716
		internal uint ScopeID;
	}
}

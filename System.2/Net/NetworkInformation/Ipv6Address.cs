using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002D6 RID: 726
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct Ipv6Address
	{
		// Token: 0x04001A45 RID: 6725
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		internal byte[] Goo;

		// Token: 0x04001A46 RID: 6726
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] Address;

		// Token: 0x04001A47 RID: 6727
		internal uint ScopeID;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002B9 RID: 697
	internal struct IpAddrString
	{
		// Token: 0x04001940 RID: 6464
		internal IntPtr Next;

		// Token: 0x04001941 RID: 6465
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string IpAddress;

		// Token: 0x04001942 RID: 6466
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		internal string IpMask;

		// Token: 0x04001943 RID: 6467
		internal uint Context;
	}
}

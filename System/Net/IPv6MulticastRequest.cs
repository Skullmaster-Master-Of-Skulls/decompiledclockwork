using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000503 RID: 1283
	internal struct IPv6MulticastRequest
	{
		// Token: 0x0400273F RID: 10047
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] MulticastAddress;

		// Token: 0x04002740 RID: 10048
		internal int InterfaceIndex;

		// Token: 0x04002741 RID: 10049
		internal static readonly int Size = Marshal.SizeOf(typeof(IPv6MulticastRequest));
	}
}

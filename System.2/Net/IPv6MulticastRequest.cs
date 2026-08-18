using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001DA RID: 474
	internal struct IPv6MulticastRequest
	{
		// Token: 0x040014FE RID: 5374
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		internal byte[] MulticastAddress;

		// Token: 0x040014FF RID: 5375
		internal int InterfaceIndex;

		// Token: 0x04001500 RID: 5376
		internal static readonly int Size = Marshal.SizeOf(typeof(IPv6MulticastRequest));
	}
}

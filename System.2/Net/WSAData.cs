using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020001D6 RID: 470
	internal struct WSAData
	{
		// Token: 0x040014E4 RID: 5348
		internal short wVersion;

		// Token: 0x040014E5 RID: 5349
		internal short wHighVersion;

		// Token: 0x040014E6 RID: 5350
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
		internal string szDescription;

		// Token: 0x040014E7 RID: 5351
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		internal string szSystemStatus;

		// Token: 0x040014E8 RID: 5352
		internal short iMaxSockets;

		// Token: 0x040014E9 RID: 5353
		internal short iMaxUdpDg;

		// Token: 0x040014EA RID: 5354
		internal IntPtr lpVendorInfo;
	}
}

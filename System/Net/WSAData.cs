using System;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x020004FF RID: 1279
	internal struct WSAData
	{
		// Token: 0x04002726 RID: 10022
		internal short wVersion;

		// Token: 0x04002727 RID: 10023
		internal short wHighVersion;

		// Token: 0x04002728 RID: 10024
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 257)]
		internal string szDescription;

		// Token: 0x04002729 RID: 10025
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		internal string szSystemStatus;

		// Token: 0x0400272A RID: 10026
		internal short iMaxSockets;

		// Token: 0x0400272B RID: 10027
		internal short iMaxUdpDg;

		// Token: 0x0400272C RID: 10028
		internal IntPtr lpVendorInfo;
	}
}

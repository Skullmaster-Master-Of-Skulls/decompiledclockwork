using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002C9 RID: 713
	internal struct MibIcmpStatsEx
	{
		// Token: 0x040019FF RID: 6655
		internal uint dwMsgs;

		// Token: 0x04001A00 RID: 6656
		internal uint dwErrors;

		// Token: 0x04001A01 RID: 6657
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal uint[] rgdwTypeCount;
	}
}

using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000605 RID: 1541
	internal struct MibIcmpStatsEx
	{
		// Token: 0x04002DA1 RID: 11681
		internal uint dwMsgs;

		// Token: 0x04002DA2 RID: 11682
		internal uint dwErrors;

		// Token: 0x04002DA3 RID: 11683
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
		internal uint[] rgdwTypeCount;
	}
}

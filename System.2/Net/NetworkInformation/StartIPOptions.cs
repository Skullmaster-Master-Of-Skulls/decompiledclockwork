using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002DA RID: 730
	[Flags]
	internal enum StartIPOptions
	{
		// Token: 0x04001A4E RID: 6734
		Both = 3,
		// Token: 0x04001A4F RID: 6735
		None = 0,
		// Token: 0x04001A50 RID: 6736
		StartIPv4 = 1,
		// Token: 0x04001A51 RID: 6737
		StartIPv6 = 2
	}
}

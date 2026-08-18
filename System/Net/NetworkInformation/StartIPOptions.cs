using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000611 RID: 1553
	[Flags]
	internal enum StartIPOptions
	{
		// Token: 0x04002DCD RID: 11725
		Both = 3,
		// Token: 0x04002DCE RID: 11726
		None = 0,
		// Token: 0x04002DCF RID: 11727
		StartIPv4 = 1,
		// Token: 0x04002DD0 RID: 11728
		StartIPv6 = 2
	}
}

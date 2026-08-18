using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000619 RID: 1561
	[Flags]
	public enum NetworkInformationAccess
	{
		// Token: 0x04002DE1 RID: 11745
		None = 0,
		// Token: 0x04002DE2 RID: 11746
		Read = 1,
		// Token: 0x04002DE3 RID: 11747
		Ping = 4
	}
}

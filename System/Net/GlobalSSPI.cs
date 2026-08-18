using System;

namespace System.Net
{
	// Token: 0x020004ED RID: 1261
	internal static class GlobalSSPI
	{
		// Token: 0x040026C1 RID: 9921
		internal static SSPIInterface SSPIAuth = new SSPIAuthType();

		// Token: 0x040026C2 RID: 9922
		internal static SSPIInterface SSPISecureChannel = new SSPISecureChannelType();
	}
}

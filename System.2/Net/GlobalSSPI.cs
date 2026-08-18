using System;

namespace System.Net
{
	// Token: 0x020001C4 RID: 452
	internal static class GlobalSSPI
	{
		// Token: 0x0400147E RID: 5246
		internal static SSPIInterface SSPIAuth = new SSPIAuthType();

		// Token: 0x0400147F RID: 5247
		internal static SSPIInterface SSPISecureChannel = new SSPISecureChannelType();
	}
}

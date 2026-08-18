using System;

namespace System.ServiceModel
{
	// Token: 0x0200014C RID: 332
	internal static class NetNamedPipeSecurityModeHelper
	{
		// Token: 0x0600096C RID: 2412 RVA: 0x00025266 File Offset: 0x00023466
		internal static bool IsDefined(NetNamedPipeSecurityMode value)
		{
			return value == NetNamedPipeSecurityMode.Transport || value == NetNamedPipeSecurityMode.None;
		}
	}
}

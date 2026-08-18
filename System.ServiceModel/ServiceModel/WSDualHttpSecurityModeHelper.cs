using System;

namespace System.ServiceModel
{
	// Token: 0x02000165 RID: 357
	internal static class WSDualHttpSecurityModeHelper
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x000280C3 File Offset: 0x000262C3
		internal static bool IsDefined(WSDualHttpSecurityMode value)
		{
			return value == WSDualHttpSecurityMode.None || value == WSDualHttpSecurityMode.Message;
		}
	}
}

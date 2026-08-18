using System;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B9 RID: 953
	internal static class MsmqIntegrationSecurityModeHelper
	{
		// Token: 0x060023AC RID: 9132 RVA: 0x000822F7 File Offset: 0x000804F7
		internal static bool IsDefined(MsmqIntegrationSecurityMode value)
		{
			return value == MsmqIntegrationSecurityMode.Transport || value == MsmqIntegrationSecurityMode.None;
		}
	}
}

using System;

namespace System.ServiceModel
{
	// Token: 0x02000152 RID: 338
	internal static class SecurityModeHelper
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x00026194 File Offset: 0x00024394
		internal static bool IsDefined(SecurityMode value)
		{
			return value == SecurityMode.None || value == SecurityMode.Transport || value == SecurityMode.Message || value == SecurityMode.TransportWithMessageCredential;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000261A7 File Offset: 0x000243A7
		internal static SecurityMode ToSecurityMode(UnifiedSecurityMode value)
		{
			if (value <= UnifiedSecurityMode.Transport)
			{
				if (value == UnifiedSecurityMode.None)
				{
					return SecurityMode.None;
				}
				if (value == UnifiedSecurityMode.Transport)
				{
					return SecurityMode.Transport;
				}
			}
			else
			{
				if (value == UnifiedSecurityMode.Message)
				{
					return SecurityMode.Message;
				}
				if (value == UnifiedSecurityMode.TransportWithMessageCredential)
				{
					return SecurityMode.TransportWithMessageCredential;
				}
			}
			return (SecurityMode)value;
		}
	}
}

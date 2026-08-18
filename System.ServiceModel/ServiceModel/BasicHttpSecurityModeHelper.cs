using System;

namespace System.ServiceModel
{
	// Token: 0x02000133 RID: 307
	internal static class BasicHttpSecurityModeHelper
	{
		// Token: 0x0600086E RID: 2158 RVA: 0x000222A9 File Offset: 0x000204A9
		internal static bool IsDefined(BasicHttpSecurityMode value)
		{
			return value == BasicHttpSecurityMode.None || value == BasicHttpSecurityMode.Transport || value == BasicHttpSecurityMode.Message || value == BasicHttpSecurityMode.TransportWithMessageCredential || value == BasicHttpSecurityMode.TransportCredentialOnly;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000222C0 File Offset: 0x000204C0
		internal static BasicHttpSecurityMode ToSecurityMode(UnifiedSecurityMode value)
		{
			if (value <= UnifiedSecurityMode.Transport)
			{
				if (value == UnifiedSecurityMode.None)
				{
					return BasicHttpSecurityMode.None;
				}
				if (value == UnifiedSecurityMode.Transport)
				{
					return BasicHttpSecurityMode.Transport;
				}
			}
			else
			{
				if (value == UnifiedSecurityMode.Message)
				{
					return BasicHttpSecurityMode.Message;
				}
				if (value == UnifiedSecurityMode.TransportWithMessageCredential)
				{
					return BasicHttpSecurityMode.TransportWithMessageCredential;
				}
				if (value == UnifiedSecurityMode.TransportCredentialOnly)
				{
					return BasicHttpSecurityMode.TransportCredentialOnly;
				}
			}
			return (BasicHttpSecurityMode)value;
		}
	}
}

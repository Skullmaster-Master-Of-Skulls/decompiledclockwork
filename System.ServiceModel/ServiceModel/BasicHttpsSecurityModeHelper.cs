using System;

namespace System.ServiceModel
{
	// Token: 0x0200012D RID: 301
	internal static class BasicHttpsSecurityModeHelper
	{
		// Token: 0x0600084D RID: 2125 RVA: 0x00021EC0 File Offset: 0x000200C0
		internal static bool IsDefined(BasicHttpsSecurityMode value)
		{
			return value == BasicHttpsSecurityMode.Transport || value == BasicHttpsSecurityMode.TransportWithMessageCredential;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00021ECB File Offset: 0x000200CB
		internal static BasicHttpsSecurityMode ToSecurityMode(UnifiedSecurityMode value)
		{
			if (value == UnifiedSecurityMode.Transport)
			{
				return BasicHttpsSecurityMode.Transport;
			}
			if (value != UnifiedSecurityMode.TransportWithMessageCredential)
			{
				return (BasicHttpsSecurityMode)value;
			}
			return BasicHttpsSecurityMode.TransportWithMessageCredential;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00021EE0 File Offset: 0x000200E0
		internal static BasicHttpsSecurityMode ToBasicHttpsSecurityMode(BasicHttpSecurityMode mode)
		{
			return (mode == BasicHttpSecurityMode.Transport) ? BasicHttpsSecurityMode.Transport : BasicHttpsSecurityMode.TransportWithMessageCredential;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00021EF8 File Offset: 0x000200F8
		internal static BasicHttpSecurityMode ToBasicHttpSecurityMode(BasicHttpsSecurityMode mode)
		{
			if (!BasicHttpsSecurityModeHelper.IsDefined(mode))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("mode"));
			}
			return (mode == BasicHttpsSecurityMode.Transport) ? BasicHttpSecurityMode.Transport : BasicHttpSecurityMode.TransportWithMessageCredential;
		}
	}
}

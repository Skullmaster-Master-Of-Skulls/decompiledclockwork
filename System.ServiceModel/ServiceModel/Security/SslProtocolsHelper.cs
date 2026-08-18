using System;
using System.ComponentModel;
using System.Security.Authentication;

namespace System.ServiceModel.Security
{
	// Token: 0x0200034E RID: 846
	internal static class SslProtocolsHelper
	{
		// Token: 0x06001EB2 RID: 7858 RVA: 0x00071A44 File Offset: 0x0006FC44
		internal static bool IsDefined(SslProtocols value)
		{
			SslProtocols sslProtocols = SslProtocols.None;
			foreach (object obj in Enum.GetValues(typeof(SslProtocols)))
			{
				sslProtocols |= (SslProtocols)obj;
			}
			return (value & sslProtocols) == value;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x00071AAC File Offset: 0x0006FCAC
		internal static void Validate(SslProtocols value)
		{
			if (!SslProtocolsHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SslProtocols)));
			}
		}
	}
}

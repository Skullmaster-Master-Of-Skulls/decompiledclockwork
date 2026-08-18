using System;
using System.ComponentModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200016F RID: 367
	internal static class SecurityKeyUsageHelper
	{
		// Token: 0x06000B7E RID: 2942 RVA: 0x00036C16 File Offset: 0x00034E16
		internal static bool IsDefined(SecurityKeyUsage value)
		{
			return value == SecurityKeyUsage.Exchange || value == SecurityKeyUsage.Signature;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00036C21 File Offset: 0x00034E21
		internal static void Validate(SecurityKeyUsage value)
		{
			if (!SecurityKeyUsageHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SecurityKeyUsage)));
			}
		}
	}
}

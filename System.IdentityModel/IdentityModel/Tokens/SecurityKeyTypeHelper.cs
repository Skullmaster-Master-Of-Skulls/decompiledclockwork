using System;
using System.ComponentModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200016D RID: 365
	internal static class SecurityKeyTypeHelper
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x00036BDD File Offset: 0x00034DDD
		internal static bool IsDefined(SecurityKeyType value)
		{
			return value == SecurityKeyType.SymmetricKey || value == SecurityKeyType.AsymmetricKey || value == SecurityKeyType.BearerKey;
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x00036BEC File Offset: 0x00034DEC
		internal static void Validate(SecurityKeyType value)
		{
			if (!SecurityKeyTypeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SecurityKeyType)));
			}
		}
	}
}

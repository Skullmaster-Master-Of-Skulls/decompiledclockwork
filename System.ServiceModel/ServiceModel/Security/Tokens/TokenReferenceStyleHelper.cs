using System;
using System.ComponentModel;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000382 RID: 898
	internal static class TokenReferenceStyleHelper
	{
		// Token: 0x06002132 RID: 8498 RVA: 0x0007B178 File Offset: 0x00079378
		public static bool IsDefined(SecurityTokenReferenceStyle value)
		{
			return value == SecurityTokenReferenceStyle.External || value == SecurityTokenReferenceStyle.Internal;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0007B184 File Offset: 0x00079384
		public static void Validate(SecurityTokenReferenceStyle value)
		{
			if (!TokenReferenceStyleHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SecurityTokenReferenceStyle)));
			}
		}
	}
}

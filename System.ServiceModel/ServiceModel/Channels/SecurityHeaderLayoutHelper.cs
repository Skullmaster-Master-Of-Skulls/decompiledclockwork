using System;
using System.ComponentModel;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000992 RID: 2450
	internal static class SecurityHeaderLayoutHelper
	{
		// Token: 0x06005F79 RID: 24441 RVA: 0x00163968 File Offset: 0x00161B68
		public static bool IsDefined(SecurityHeaderLayout value)
		{
			return value == SecurityHeaderLayout.Lax || value == SecurityHeaderLayout.LaxTimestampFirst || value == SecurityHeaderLayout.LaxTimestampLast || value == SecurityHeaderLayout.Strict;
		}

		// Token: 0x06005F7A RID: 24442 RVA: 0x0016397C File Offset: 0x00161B7C
		public static void Validate(SecurityHeaderLayout value)
		{
			if (!SecurityHeaderLayoutHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SecurityHeaderLayout)));
			}
		}
	}
}

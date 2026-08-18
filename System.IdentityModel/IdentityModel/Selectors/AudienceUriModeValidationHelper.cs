using System;
using System.ComponentModel;

namespace System.IdentityModel.Selectors
{
	// Token: 0x0200019E RID: 414
	public static class AudienceUriModeValidationHelper
	{
		// Token: 0x06000D89 RID: 3465 RVA: 0x00036BDD File Offset: 0x00034DDD
		public static bool IsDefined(AudienceUriMode validationMode)
		{
			return validationMode == AudienceUriMode.Never || validationMode == AudienceUriMode.Always || validationMode == AudienceUriMode.BearerKeyOnly;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x0003EC67 File Offset: 0x0003CE67
		internal static void Validate(AudienceUriMode value)
		{
			if (!AudienceUriModeValidationHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(AudienceUriMode)));
			}
		}
	}
}

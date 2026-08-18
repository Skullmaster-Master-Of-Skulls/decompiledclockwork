using System;
using System.ComponentModel;

namespace System.ServiceModel.Security
{
	// Token: 0x0200033F RID: 831
	internal static class UserNamePasswordValidationModeHelper
	{
		// Token: 0x06001E2C RID: 7724 RVA: 0x000700A7 File Offset: 0x0006E2A7
		public static bool IsDefined(UserNamePasswordValidationMode validationMode)
		{
			return validationMode == UserNamePasswordValidationMode.Windows || validationMode == UserNamePasswordValidationMode.MembershipProvider || validationMode == UserNamePasswordValidationMode.Custom;
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000700B6 File Offset: 0x0006E2B6
		public static void Validate(UserNamePasswordValidationMode value)
		{
			if (!UserNamePasswordValidationModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(UserNamePasswordValidationMode)));
			}
		}
	}
}

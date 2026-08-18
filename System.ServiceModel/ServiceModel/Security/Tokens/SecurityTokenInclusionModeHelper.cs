using System;
using System.ComponentModel;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A0 RID: 928
	internal static class SecurityTokenInclusionModeHelper
	{
		// Token: 0x060022AA RID: 8874 RVA: 0x0007F2D6 File Offset: 0x0007D4D6
		public static bool IsDefined(SecurityTokenInclusionMode value)
		{
			return value == SecurityTokenInclusionMode.AlwaysToInitiator || value == SecurityTokenInclusionMode.AlwaysToRecipient || value == SecurityTokenInclusionMode.Never || value == SecurityTokenInclusionMode.Once;
		}

		// Token: 0x060022AB RID: 8875 RVA: 0x0007F2E9 File Offset: 0x0007D4E9
		public static void Validate(SecurityTokenInclusionMode value)
		{
			if (!SecurityTokenInclusionModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SecurityTokenInclusionMode)));
			}
		}
	}
}

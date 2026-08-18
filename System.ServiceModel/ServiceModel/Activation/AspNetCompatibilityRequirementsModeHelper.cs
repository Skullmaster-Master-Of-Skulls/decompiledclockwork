using System;
using System.ComponentModel;

namespace System.ServiceModel.Activation
{
	// Token: 0x020005BB RID: 1467
	internal static class AspNetCompatibilityRequirementsModeHelper
	{
		// Token: 0x06003941 RID: 14657 RVA: 0x000DE375 File Offset: 0x000DC575
		public static bool IsDefined(AspNetCompatibilityRequirementsMode x)
		{
			return x == AspNetCompatibilityRequirementsMode.NotAllowed || x == AspNetCompatibilityRequirementsMode.Allowed || x == AspNetCompatibilityRequirementsMode.Required;
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000DE384 File Offset: 0x000DC584
		public static void Validate(AspNetCompatibilityRequirementsMode value)
		{
			if (!AspNetCompatibilityRequirementsModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(AspNetCompatibilityRequirementsMode)));
			}
		}
	}
}

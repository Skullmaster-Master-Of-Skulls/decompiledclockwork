using System;
using System.ComponentModel;

namespace System.ServiceModel
{
	// Token: 0x02000037 RID: 55
	internal static class HostNameComparisonModeHelper
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x00009091 File Offset: 0x00007291
		internal static bool IsDefined(HostNameComparisonMode value)
		{
			return value == HostNameComparisonMode.StrongWildcard || value == HostNameComparisonMode.Exact || value == HostNameComparisonMode.WeakWildcard;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000090A0 File Offset: 0x000072A0
		public static void Validate(HostNameComparisonMode value)
		{
			if (!HostNameComparisonModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(HostNameComparisonMode)));
			}
		}
	}
}

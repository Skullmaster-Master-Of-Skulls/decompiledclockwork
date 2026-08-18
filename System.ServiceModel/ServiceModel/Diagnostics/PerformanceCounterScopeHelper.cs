using System;
using System.ComponentModel;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A8D RID: 2701
	internal static class PerformanceCounterScopeHelper
	{
		// Token: 0x06006AB8 RID: 27320 RVA: 0x0018DF71 File Offset: 0x0018C171
		internal static bool IsDefined(PerformanceCounterScope value)
		{
			return value == PerformanceCounterScope.Off || value == PerformanceCounterScope.Default || value == PerformanceCounterScope.ServiceOnly || value == PerformanceCounterScope.All;
		}

		// Token: 0x06006AB9 RID: 27321 RVA: 0x0018DF84 File Offset: 0x0018C184
		public static void Validate(PerformanceCounterScope value)
		{
			if (!PerformanceCounterScopeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(PerformanceCounterScope)));
			}
		}
	}
}

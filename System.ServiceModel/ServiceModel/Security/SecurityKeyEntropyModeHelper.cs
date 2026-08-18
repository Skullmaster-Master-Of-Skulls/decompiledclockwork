using System;
using System.ComponentModel;

namespace System.ServiceModel.Security
{
	// Token: 0x020002FD RID: 765
	internal sealed class SecurityKeyEntropyModeHelper
	{
		// Token: 0x060019E7 RID: 6631 RVA: 0x00061230 File Offset: 0x0005F430
		internal static bool IsDefined(SecurityKeyEntropyMode value)
		{
			return value == SecurityKeyEntropyMode.ClientEntropy || value == SecurityKeyEntropyMode.ServerEntropy || value == SecurityKeyEntropyMode.CombinedEntropy;
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0006123F File Offset: 0x0005F43F
		internal static void Validate(SecurityKeyEntropyMode value)
		{
			if (!SecurityKeyEntropyModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(SecurityKeyEntropyMode)));
			}
		}
	}
}

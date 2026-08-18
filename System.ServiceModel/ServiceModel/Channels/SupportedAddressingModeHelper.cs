using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000770 RID: 1904
	internal static class SupportedAddressingModeHelper
	{
		// Token: 0x060048B8 RID: 18616 RVA: 0x0010C9C2 File Offset: 0x0010ABC2
		internal static bool IsDefined(SupportedAddressingMode value)
		{
			return value == SupportedAddressingMode.Anonymous || value == SupportedAddressingMode.NonAnonymous || value == SupportedAddressingMode.Mixed;
		}
	}
}

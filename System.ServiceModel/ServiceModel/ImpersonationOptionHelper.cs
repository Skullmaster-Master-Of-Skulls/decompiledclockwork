using System;

namespace System.ServiceModel
{
	// Token: 0x020000D3 RID: 211
	internal static class ImpersonationOptionHelper
	{
		// Token: 0x060003D9 RID: 985 RVA: 0x000156B4 File Offset: 0x000138B4
		public static bool IsDefined(ImpersonationOption option)
		{
			return option == ImpersonationOption.NotAllowed || option == ImpersonationOption.Allowed || option == ImpersonationOption.Required;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000156C3 File Offset: 0x000138C3
		internal static bool AllowedOrRequired(ImpersonationOption option)
		{
			return option == ImpersonationOption.Allowed || option == ImpersonationOption.Required;
		}
	}
}

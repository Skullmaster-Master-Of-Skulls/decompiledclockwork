using System;

namespace System.ServiceModel.Description
{
	// Token: 0x020003C1 RID: 961
	internal static class PrincipalPermissionModeHelper
	{
		// Token: 0x060023FB RID: 9211 RVA: 0x00082EBF File Offset: 0x000810BF
		public static bool IsDefined(PrincipalPermissionMode principalPermissionMode)
		{
			return Enum.IsDefined(typeof(PrincipalPermissionMode), principalPermissionMode);
		}
	}
}

using System;
using System.Security;
using System.Web;

namespace Telerik.Web.UI
{
	// Token: 0x02000F56 RID: 3926
	internal static class SecurityHelper
	{
		// Token: 0x060095B6 RID: 38326 RVA: 0x00216DB5 File Offset: 0x00214FB5
		public static bool IsPermissionGranted(IPermission permission)
		{
			return SecurityManager.IsGranted(permission);
		}

		// Token: 0x060095B7 RID: 38327 RVA: 0x00216DC0 File Offset: 0x00214FC0
		public static AspNetHostingPermissionLevel GetCurrentTrustLevel()
		{
			AspNetHostingPermissionLevel[] array = new AspNetHostingPermissionLevel[]
			{
				AspNetHostingPermissionLevel.Unrestricted,
				AspNetHostingPermissionLevel.High,
				AspNetHostingPermissionLevel.Medium,
				AspNetHostingPermissionLevel.Low,
				AspNetHostingPermissionLevel.Minimal
			};
			int i = 0;
			while (i < array.Length)
			{
				AspNetHostingPermissionLevel aspNetHostingPermissionLevel = array[i];
				try
				{
					new AspNetHostingPermission(aspNetHostingPermissionLevel).Demand();
				}
				catch (SecurityException)
				{
					goto IL_4F;
				}
				goto IL_4B;
				IL_4F:
				i++;
				continue;
				IL_4B:
				return aspNetHostingPermissionLevel;
			}
			return AspNetHostingPermissionLevel.None;
		}
	}
}

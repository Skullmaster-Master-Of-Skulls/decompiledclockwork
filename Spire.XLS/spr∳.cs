using System;
using System.Security;
using System.Security.Permissions;

// Token: 0x02000456 RID: 1110
internal class spr\u2233
{
	// Token: 0x060042CC RID: 17100 RVA: 0x00256C2C File Offset: 0x00255C2C
	public static bool ᜀ()
	{
		bool result;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			result = true;
			SecurityPermission securityPermission = new SecurityPermission(PermissionState.Unrestricted);
			try
			{
				securityPermission.Demand();
			}
			catch (SecurityException)
			{
				result = false;
			}
			break;
		}
		}
		return result;
	}
}

using System;
using System.Data.SqlClient;
using System.Security;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x02000214 RID: 532
	internal static class Permission
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x00050748 File Offset: 0x0004E948
		internal static bool HasSqlClientPermission()
		{
			NamedPermissionSet namedPermissionSet = HttpRuntime.NamedPermissionSet;
			if (namedPermissionSet == null)
			{
				return true;
			}
			IPermission permission = namedPermissionSet.GetPermission(typeof(SqlClientPermission));
			if (permission == null)
			{
				return false;
			}
			IPermission permission2 = null;
			try
			{
				permission2 = new SqlClientPermission(PermissionState.Unrestricted);
			}
			catch
			{
				return false;
			}
			return permission2.IsSubsetOf(permission);
		}
	}
}

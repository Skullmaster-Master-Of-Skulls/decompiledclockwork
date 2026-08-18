using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.DAO.UserSettingsPermissions
{
	// Token: 0x02000019 RID: 25
	public interface IPermissionDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000045 RID: 69
		IList<UserPermission> LoadUserPermissions(int pid);

		// Token: 0x06000046 RID: 70
		void LegacyLoadUserAndGroupPermissionTables(int pid, out DataTable personPermissionsTable, out DataTable groupPermissionsTable);

		// Token: 0x06000047 RID: 71
		UserOrGroupJustPermissionSet LoadJustUserPermissions(int pid);

		// Token: 0x06000048 RID: 72
		UserOrGroupJustPermissionSet LoadJustGroupPermissions(int gid);

		// Token: 0x06000049 RID: 73
		void UpdateJustUserOrGroupPermissions(UserOrGroupJustPermissionSet permissionSet);
	}
}

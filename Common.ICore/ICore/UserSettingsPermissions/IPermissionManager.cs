using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.ICore.UserSettingsPermissions
{
	// Token: 0x02000016 RID: 22
	public interface IPermissionManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600008E RID: 142
		bool IsUserAllowed(int pid, UserPermissionEnum permission);

		// Token: 0x0600008F RID: 143
		bool IsUserAllowedToViewScreen(int pid, int screenNum);

		// Token: 0x06000090 RID: 144
		bool IsUserAllowedToModifyScreen(int pid, int screenNum);

		// Token: 0x06000091 RID: 145
		bool IsUserAllowedToCreateScreen(int pid, int screenNum);

		// Token: 0x06000092 RID: 146
		IList<UserPermission> LoadUserPermissions(int pid);

		// Token: 0x06000093 RID: 147
		IList<UserPermission> LoadUserPermissions(int pid, bool ignoreCache);

		// Token: 0x06000094 RID: 148
		UserPermissionIsAllowedSet LoadUserPermissionSet(int pid, bool ignoreCache);

		// Token: 0x06000095 RID: 149
		UserOrGroupJustPermissionSet LoadJustUserPermissions(int pid);

		// Token: 0x06000096 RID: 150
		UserOrGroupJustPermissionSet LoadJustGroupPermissions(int gid);

		// Token: 0x06000097 RID: 151
		void UpdateJustUserOrGroupPermissions(UserOrGroupJustPermissionSet permissionSet);
	}
}

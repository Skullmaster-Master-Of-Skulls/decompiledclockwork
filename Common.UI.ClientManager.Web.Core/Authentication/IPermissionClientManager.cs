using System;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Authentication
{
	// Token: 0x02000016 RID: 22
	public interface IPermissionClientManager
	{
		// Token: 0x06000050 RID: 80
		bool IsPersonAllowed(UserPermissionEnum PermissionCode);

		// Token: 0x06000051 RID: 81
		bool IsPersonAllowed(UserPermissionEnum PermissionCode, int val);
	}
}

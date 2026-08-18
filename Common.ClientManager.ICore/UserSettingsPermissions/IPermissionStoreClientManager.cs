using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions
{
	// Token: 0x02000007 RID: 7
	public interface IPermissionStoreClientManager : IWebService
	{
		// Token: 0x06000018 RID: 24
		UserPermissionIsAllowedSetDTO LoadUserPermissionIsAllowedSet(int pid);
	}
}

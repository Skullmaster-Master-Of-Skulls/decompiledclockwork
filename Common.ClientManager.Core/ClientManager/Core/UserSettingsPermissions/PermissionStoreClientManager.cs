using System;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.UserSettingsPermissions
{
	// Token: 0x0200000A RID: 10
	public class PermissionStoreClientManager : IPermissionStoreClientManager, IWebService
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003240 File Offset: 0x00001440
		public UserPermissionIsAllowedSetDTO LoadUserPermissionIsAllowedSet(int pid)
		{
			LoadUserPermissionIsAllowedSetReq loadUserPermissionIsAllowedSetReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUserPermissionIsAllowedSetReq>();
			loadUserPermissionIsAllowedSetReq.ForPersonId = pid;
			loadUserPermissionIsAllowedSetReq.IgnoreCache = true;
			return ClientServiceFactory.GetClientInstance<IPermissions>().LoadUserPermissionIsAllowedSet(loadUserPermissionIsAllowedSetReq).IsAllowedSet;
		}
	}
}

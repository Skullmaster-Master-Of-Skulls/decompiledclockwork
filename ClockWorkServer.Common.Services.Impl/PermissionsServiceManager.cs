using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Core.Mappers.UserSettingsPermissions;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200009F RID: 159
	public class PermissionsServiceManager : IPermissions, IService
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x0001B0B4 File Offset: 0x000192B4
		public LoadUserPermissionIsAllowedSetResp LoadUserPermissionIsAllowedSet(LoadUserPermissionIsAllowedSetReq Request)
		{
			IPermissionManager permissionManager = new PermissionManager(Request.GetOperationContext());
			UserPermissionIsAllowedSet userPermissionIsAllowedSet = permissionManager.LoadUserPermissionSet((Request.ForPersonId < 1) ? Request.WhoAmI : Request.ForPersonId, Request.IgnoreCache);
			return new LoadUserPermissionIsAllowedSetResp
			{
				IsAllowedSet = ((userPermissionIsAllowedSet == null) ? null : userPermissionIsAllowedSet.ToDTO())
			};
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0001B110 File Offset: 0x00019310
		public LoadJustUserPermissionsResp LoadJustUserPermissions(LoadJustUserPermissionsReq Request)
		{
			IPermissionManager permissionManager = new PermissionManager(Request.GetOperationContext());
			UserOrGroupJustPermissionSet userOrGroupJustPermissionSet = permissionManager.LoadJustUserPermissions((Request.ForPersonId < 1) ? Request.WhoAmI : Request.ForPersonId);
			return new LoadJustUserPermissionsResp
			{
				PermissionSet = ((userOrGroupJustPermissionSet == null) ? null : userOrGroupJustPermissionSet.ToDTO())
			};
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0001B164 File Offset: 0x00019364
		public LoadJustGroupPermissionsResp LoadJustGroupPermissions(LoadJustGroupPermissionsReq Request)
		{
			IPermissionManager permissionManager = new PermissionManager(Request.GetOperationContext());
			UserOrGroupJustPermissionSet userOrGroupJustPermissionSet = permissionManager.LoadJustGroupPermissions(Request.ForGroupId);
			return new LoadJustGroupPermissionsResp
			{
				PermissionSet = ((userOrGroupJustPermissionSet == null) ? null : userOrGroupJustPermissionSet.ToDTO())
			};
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0001B1A8 File Offset: 0x000193A8
		public void UpdateJustUserOrGroupPermissions(UpdateJustUserOrGroupPermissionsReq Request)
		{
			IPermissionManager permissionManager = new PermissionManager(Request.GetOperationContext());
			permissionManager.UpdateJustUserOrGroupPermissions(Request.PermissionSet.ToDomainObject());
		}
	}
}

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Extensions;

namespace TechnoPro.Common.Web.Security.Authorization.Requirement.Inventory
{
	// Token: 0x02000020 RID: 32
	public class InventoryAdminHandler : AuthorizationHandler<InventoryAdminRequirement>
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00003A59 File Offset: 0x00001C59
		public InventoryAdminHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003A68 File Offset: 0x00001C68
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InventoryAdminRequirement requirement)
		{
			IPeopleGroupManager peopleGroupManager = ObjectFactory.Resolve<IPeopleGroupManager>();
			peopleGroupManager.OpContext = (this._opContext ?? context.User.GetOperationContext());
			if (peopleGroupManager.IsAdmin(this._opContext.WhoAmI))
			{
				context.Succeed(requirement);
			}
			else
			{
				IOldUserSettingManager oldUserSettingManager = ObjectFactory.Resolve<IOldUserSettingManager>();
				oldUserSettingManager.OpContext = this._opContext;
				if (oldUserSettingManager.GetSettingValue_Bool(this._opContext.WhoAmI, eSettingCode.SETTING_Inventory_IsInventoryAdmin))
				{
					context.Succeed(requirement);
				}
				else
				{
					context.Fail();
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x04000023 RID: 35
		private readonly OperationContext _opContext;
	}
}

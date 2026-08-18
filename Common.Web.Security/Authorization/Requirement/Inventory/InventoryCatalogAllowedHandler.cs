using System;
using System.Collections.Generic;
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
	// Token: 0x02000022 RID: 34
	public class InventoryCatalogAllowedHandler : AuthorizationHandler<InventoryCatalogAllowedRequirement, int>
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x00003AEC File Offset: 0x00001CEC
		public InventoryCatalogAllowedHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003AFC File Offset: 0x00001CFC
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InventoryCatalogAllowedRequirement requirement, int catalogId)
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
					List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(this._opContext.WhoAmI, eSettingCode.SETTING_Inventory_AllowedCatalogIds);
					if (settingValue_ConcatenatedIntList.Count == 0 && !oldUserSettingManager.UserHasAnySettings(this._opContext.WhoAmI, eSettingCode.SETTING_Inventory_AllowedCatalogIds))
					{
						settingValue_ConcatenatedIntList.Add(1);
					}
					if (settingValue_ConcatenatedIntList.Contains(catalogId))
					{
						context.Succeed(requirement);
					}
					else
					{
						context.Fail();
					}
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x04000024 RID: 36
		private readonly OperationContext _opContext;
	}
}

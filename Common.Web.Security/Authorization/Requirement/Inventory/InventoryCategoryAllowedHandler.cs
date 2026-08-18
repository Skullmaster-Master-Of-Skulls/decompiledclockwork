using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Extensions;

namespace TechnoPro.Common.Web.Security.Authorization.Requirement.Inventory
{
	// Token: 0x02000024 RID: 36
	public class InventoryCategoryAllowedHandler : AuthorizationHandler<InventoryCategoryAllowedRequirement, string>
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003BD5 File Offset: 0x00001DD5
		public InventoryCategoryAllowedHandler(OperationContext opContext)
		{
			this._opContext = opContext;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003BE4 File Offset: 0x00001DE4
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, InventoryCategoryAllowedRequirement requirement, string categoryName)
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
					IInventoryCategoryManager inventoryCategoryManager = ObjectFactory.Resolve<IInventoryCategoryManager>();
					inventoryCategoryManager.OpContext = (this._opContext ?? context.User.GetOperationContext());
					Func<InventoryCategory, bool> <>9__0;
					foreach (int catalogId in settingValue_ConcatenatedIntList)
					{
						IEnumerable<InventoryCategory> categoriesByCatalog = inventoryCategoryManager.GetCategoriesByCatalog(catalogId);
						Func<InventoryCategory, bool> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = ((InventoryCategory c) => c.CategoryName == categoryName));
						}
						if (categoriesByCatalog.Any(predicate))
						{
							context.Succeed(requirement);
							return Task.CompletedTask;
						}
					}
					context.Fail();
				}
			}
			return Task.CompletedTask;
		}

		// Token: 0x04000025 RID: 37
		private readonly OperationContext _opContext;
	}
}

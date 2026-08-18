using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.Inventory.Adapters
{
	// Token: 0x020000EB RID: 235
	public static class OperationContextAdapter
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x0003AD60 File Offset: 0x00038F60
		internal static IList<int> GetAllowedCatalogIds(this OperationContext opContext)
		{
			bool flag = opContext.IsInventoryAdmin(true);
			IList<int> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(opContext);
				List<int> settingValue_ConcatenatedIntList = oldUserSettingManager.GetSettingValue_ConcatenatedIntList(opContext.WhoAmI, eSettingCode.SETTING_Inventory_AllowedCatalogIds);
				bool flag2 = settingValue_ConcatenatedIntList.Count == 0 && !oldUserSettingManager.UserHasAnySettings(opContext.WhoAmI, eSettingCode.SETTING_Inventory_AllowedCatalogIds);
				if (flag2)
				{
					settingValue_ConcatenatedIntList.Add(1);
				}
				result = settingValue_ConcatenatedIntList;
			}
			return result;
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0003ADC8 File Offset: 0x00038FC8
		internal static bool IsCatalogAllowedForUser(this OperationContext opContext, int catalogId)
		{
			bool flag = opContext.IsInventoryAdmin(true);
			bool flag2 = flag;
			return flag2 || opContext.GetAllowedCatalogIds().Contains(catalogId);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		internal static bool IsInventoryAdmin(this OperationContext opContext, bool includeClockWorkAdmin = true)
		{
			if (includeClockWorkAdmin)
			{
				IPeopleGroupManager peopleGroupManager = new PeopleGroupManager(opContext);
				bool flag = peopleGroupManager.IsAdmin(opContext.WhoAmI);
				bool flag2 = flag;
				if (flag2)
				{
					return true;
				}
			}
			OldUserSettingManager oldUserSettingManager = new OldUserSettingManager(opContext);
			return oldUserSettingManager.GetSettingValue_Bool(opContext.WhoAmI, eSettingCode.SETTING_Inventory_IsInventoryAdmin);
		}
	}
}

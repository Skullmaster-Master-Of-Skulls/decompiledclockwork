using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000174 RID: 372
	public static class ServiceProvidersOriginal
	{
		// Token: 0x06001043 RID: 4163 RVA: 0x00077D48 File Offset: 0x00075F48
		public static ServiceProvidersOperationContext GetProviderTypes(this OperationContext opContext)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = "u_serviceProviderApplicationContext";
			ServiceProvidersOperationContext serviceProvidersOperationContext = (ServiceProvidersOperationContext)cacheStorageManager[key];
			bool flag = serviceProvidersOperationContext == null;
			if (flag)
			{
				IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(opContext);
				string serviceTypeDescriptions = oldUserSettingManager.GetSettingValue_String(opContext.WhoAmI, eSettingCode.SETTING_ServiceProviders_ServiceTypeDescriptions, false) ?? "";
				serviceProvidersOperationContext = new ServiceProvidersOperationContext
				{
					WhoAmI = opContext.WhoAmI,
					ServiceProviderTypes = serviceTypeDescriptions.ServiceProviderTypesFromString()
				};
				cacheStorageManager.Insert(key, serviceProvidersOperationContext, TimeSpan.FromMinutes(30.0));
			}
			return serviceProvidersOperationContext;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00077DE0 File Offset: 0x00075FE0
		public static IList<ServiceProviderType> ServiceProviderTypesFromString(this string serviceTypeDescriptions)
		{
			string[] array = serviceTypeDescriptions.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries);
			int num = 0;
			List<ServiceProviderType> list = new List<ServiceProviderType>();
			foreach (string text in array)
			{
				num++;
				int num2 = text.IndexOf('=');
				bool flag = num2 > 0;
				string title;
				if (flag)
				{
					string s = text.Substring(0, num2);
					title = text.Substring(num2 + 1);
					int.TryParse(s, out num2);
					bool flag2 = num2 < 1;
					if (flag2)
					{
						num2 = num;
					}
				}
				else
				{
					title = text;
				}
				int num3 = Convert.ToInt32(Math.Pow(2.0, (double)(num2 - 1)));
				list.Add(new ServiceProviderType
				{
					ServiceProviderTypeId = num3,
					MatchingMethod = ((num3 == 128) ? eServiceProviderMatchingMethod.ByAvailabilityAndCourseTimetable : eServiceProviderMatchingMethod.Unknown),
					SpecializedServiceProviderType = ((num3 == 128) ? eSpecializedServiceProviderType.Notetaking : eSpecializedServiceProviderType.General),
					Title = title
				});
			}
			return list;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Caching;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public.Entities.Settings;

namespace ClockWorkWebAPI.Settings
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class AppSettingsV2
	{
		// Token: 0x06000397 RID: 919 RVA: 0x00019AD4 File Offset: 0x00017CD4
		public AppSettingsV2()
		{
			this.mSettings = new WebSettingsClientManager();
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00019AEC File Offset: 0x00017CEC
		public static string IntListToString(List<int> list)
		{
			bool flag = list == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < list.Count; i++)
				{
					bool flag2 = i > 0;
					if (flag2)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(list[i].ToString());
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00019B60 File Offset: 0x00017D60
		public static List<int> CommaSeparatedStringListToList(string s)
		{
			List<int> list = new List<int>();
			bool flag = s == null;
			List<int> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				string[] array = s.Split(new char[]
				{
					','
				});
				foreach (string text in array)
				{
					string text2 = text.Trim();
					bool flag2 = text2.Length > 0;
					if (flag2)
					{
						list.Add(int.Parse(text2));
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public static List<int> GetSettingValueIntArray(Setting setting, db conn, Cache cache)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			string settingValue = webSettingsClientManager.GetSettingValue<string>(setting);
			return AppSettingsV2.CommaSeparatedStringListToList(settingValue);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00019C0C File Offset: 0x00017E0C
		public static int GetSettingValueIntArraySingle(Setting setting, db conn, Cache cache)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int[] settingValue = webSettingsClientManager.GetSettingValue<int[]>(setting);
			return (settingValue == null || settingValue.Length < 1) ? 0 : settingValue[0];
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00019C3C File Offset: 0x00017E3C
		public static int[] GetSettingValueIntArray2(Setting setting, db conn, Cache cache)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return webSettingsClientManager.GetSettingValue<int[]>(setting);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00019C5C File Offset: 0x00017E5C
		public static string GetSettingValueString(Setting setting, db conn, Cache cache)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return webSettingsClientManager.GetSettingValue<string>(setting);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00019C7C File Offset: 0x00017E7C
		public static int GetSettingValueInt(Setting setting, db conn, Cache cache)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return webSettingsClientManager.GetSettingValue<int>(setting);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00019C9C File Offset: 0x00017E9C
		public static bool GetSettingValueBool(Setting setting, db conn, Cache cache)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return webSettingsClientManager.GetSettingValue<bool>(setting);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00019CBC File Offset: 0x00017EBC
		public T GetSettingValue<T>(Setting setting)
		{
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			return webSettingsClientManager.GetSettingValue<T>(setting);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00019CDC File Offset: 0x00017EDC
		public static AppSettingsV2 GetAppSettingsFromCache(Cache cache, db conn)
		{
			object obj = cache[AppSettingsV2.cacheName];
			bool flag = obj == null;
			AppSettingsV2 appSettingsV;
			if (flag)
			{
				appSettingsV = new AppSettingsV2();
				string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("instancename");
				ISettingManager currentInstance = SettingManager.CurrentInstance;
				currentInstance.InstanceName = appSettingsByNameUsingProtection;
				cache.Insert(AppSettingsV2.cacheName, appSettingsV);
			}
			else
			{
				appSettingsV = (AppSettingsV2)obj;
			}
			return appSettingsV;
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00019D3E File Offset: 0x00017F3E
		public static void ClearSettingsCache(Cache cache)
		{
			cache.Remove(AppSettingsV2.cacheName);
		}

		// Token: 0x040001D3 RID: 467
		private static string cacheName = "CLOCKWORKAPPSETTINGS";

		// Token: 0x040001D4 RID: 468
		private IWebSettingsClientManager mSettings;
	}
}

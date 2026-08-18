using System;
using System.Collections.Generic;
using System.Web.Caching;
using System.Web.UI;
using Databases;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.UI.ClientManager.Web.Auth
{
	// Token: 0x0200000A RID: 10
	public static class LegacyCaching
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000479C File Offset: 0x0000299C
		public static List<AuthenticationMethod> GetLookupAuthenticationMethods(Page Page, object LOGIN_AuthenticationMethods)
		{
			return LegacyCaching.GetLookupAuthenticationMethods(Page, Setting.LOGIN_AuthenticationMethods);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000047BC File Offset: 0x000029BC
		public static List<AuthenticationMethod> GetLookupAuthenticationMethods(Page Page, Setting LOGIN_AuthenticationMethods)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Cache cache = Page.Cache;
			string key = "LoginAuthenticationMethods";
			object obj = cache[key];
			bool dontCache = LegacyCaching.DontCache;
			if (dontCache)
			{
				obj = null;
			}
			bool flag = obj == null;
			List<AuthenticationMethod> list;
			if (flag)
			{
				IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
				string settingValue = webSettingsClientManager.GetSettingValue<string>(LOGIN_AuthenticationMethods);
				list = Utility.ParseXmlAuthenticationMethods(settingValue);
				cache.Insert(key, list);
			}
			else
			{
				list = (List<AuthenticationMethod>)obj;
			}
			return list;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00004838 File Offset: 0x00002A38
		private static bool DontCache
		{
			get
			{
				string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("dontcache");
				return Core.ParseBooleanAttribute(appSettingsByNameUsingProtection, false);
			}
		}
	}
}

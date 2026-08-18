using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Settings.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Settings
{
	// Token: 0x02000016 RID: 22
	public class WebSettingsRestClientManager : BearerTokenRestProxy<IWebSettingsClientManager>, IWebSettingsClientManager, IWebService
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00003AB2 File Offset: 0x00001CB2
		public WebSettingsRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003ABC File Offset: 0x00001CBC
		public WebSettingsRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003AC7 File Offset: 0x00001CC7
		public IList<string> GetInstanceNames()
		{
			return base.GetMany<string>("websettings/instancenames", true);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public IList<AppSetting> GetSettings(Group group)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[group.GetCacheKey(WebSettingsRestClientManager.GetInstanceName())];
			if (obj != null)
			{
				return obj as IList<AppSetting>;
			}
			string instanceName = WebSettingsRestClientManager.GetInstanceName();
			IList<AppSetting> list = base.GetMany<AppSettingDTO>(string.Format("websettings/group/{0}/instancename/{1}", group, instanceName), true).ToDomainObject();
			cacheStorageManager.Insert(group.GetCacheKey(WebSettingsRestClientManager.GetInstanceName()), list, WebSettingsRestClientManager.SettingsSlidingExpirationTime, true);
			return list;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003B44 File Offset: 0x00001D44
		public AppSetting GetSetting(Setting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<AppSetting> list = (IList<AppSetting>)cacheStorageManager[setting.GetGroup().GetCacheKey(WebSettingsRestClientManager.GetInstanceName())];
			if (list != null)
			{
				return list.FirstOrDefault((AppSetting s) => s.LookupSetting.Setting == setting);
			}
			AppSetting appSetting = (AppSetting)cacheStorageManager[setting.GetCacheKey(WebSettingsRestClientManager.GetInstanceName())];
			if (appSetting != null)
			{
				return appSetting;
			}
			string instanceName = WebSettingsRestClientManager.GetInstanceName();
			appSetting = base.Get<AppSettingDTO>(string.Format("websettings/setting/{0}/instancename/{1}", setting, instanceName), true).ToDomainObject();
			cacheStorageManager.Insert(setting.GetCacheKey(WebSettingsRestClientManager.GetInstanceName()), appSetting, WebSettingsRestClientManager.SettingsSlidingExpirationTime, true);
			return appSetting;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003C08 File Offset: 0x00001E08
		public AppSetting GetSetting(Setting setting, string sValue)
		{
			string instanceName = WebSettingsRestClientManager.GetInstanceName();
			return base.Get<AppSetting>(string.Format("websettings/setting/{0}?svalue={1}&instancename={2}", setting, sValue, instanceName), true);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C34 File Offset: 0x00001E34
		public void SaveSetting(AppSetting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(setting.LookupSetting.Group.GetCacheKey(WebSettingsRestClientManager.GetInstanceName()));
			cacheStorageManager.Remove(setting.LookupSetting.Setting.GetCacheKey(WebSettingsRestClientManager.GetInstanceName()));
			SaveSettingReq saveSettingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveSettingReq>();
			saveSettingReq.InstanceName = WebSettingsRestClientManager.GetInstanceName();
			saveSettingReq.Setting = setting.ToDTO();
			base.Post<SaveSettingReq>(saveSettingReq, "websettings");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003CAC File Offset: 0x00001EAC
		public void ClearSettingsCache(Group group)
		{
			this.RemoveSettingsFromCache(group);
			ClearSettingsCacheByGroupReq clearSettingsCacheByGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearSettingsCacheByGroupReq>();
			clearSettingsCacheByGroupReq.InstanceName = WebSettingsRestClientManager.GetInstanceName();
			clearSettingsCacheByGroupReq.Group = group;
			base.Post<ClearSettingsCacheByGroupReq>(clearSettingsCacheByGroupReq, "websettings/clearsettingscachebygroup");
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003CEC File Offset: 0x00001EEC
		public void ClearSettingsCache()
		{
			this.ClearCache();
			ClearSettingsCacheReq clearSettingsCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearSettingsCacheReq>();
			clearSettingsCacheReq.InstanceName = WebSettingsRestClientManager.GetInstanceName();
			base.Post<ClearSettingsCacheReq>(clearSettingsCacheReq, "websettings/clearsettingscache");
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D24 File Offset: 0x00001F24
		public T GetSettingValue<T>(Setting setting)
		{
			AppSetting setting2 = this.GetSetting(setting);
			object obj = setting2.Value;
			if (obj == null)
			{
				return setting2.LookupSetting.GetDefaultValue<T>();
			}
			if (obj is T)
			{
				return (T)((object)obj);
			}
			if (obj is string)
			{
				if (typeof(T) == typeof(bool))
				{
					obj = ("1yestrue".IndexOf(((string)obj).ToLower()) >= 0);
					return (T)((object)obj);
				}
				if (typeof(T) != typeof(int))
				{
					return setting2.LookupSetting.GetDefaultValue<T>();
				}
				string text = ((string)obj).Trim();
				int num;
				if (text.Length > 0)
				{
					try
					{
						num = int.Parse(text);
						goto IL_CD;
					}
					catch
					{
						num = 0;
						goto IL_CD;
					}
				}
				num = 0;
				IL_CD:
				obj = num;
				return (T)((object)obj);
			}
			else
			{
				if (!(obj is int[]))
				{
					return setting2.LookupSetting.GetDefaultValue<T>();
				}
				int[] array = (int[])obj;
				if (typeof(T) == typeof(int))
				{
					obj = array[0];
					return (T)((object)obj);
				}
				if (typeof(T) != typeof(string))
				{
					return setting2.LookupSetting.GetDefaultValue<T>();
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int num2 in array)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(num2.ToString());
				}
				obj = stringBuilder.ToString();
				return (T)((object)obj);
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003EE0 File Offset: 0x000020E0
		private static string GetInstanceName()
		{
			return ((string)ObjectFactory.Resolve<ICacheStorageManager>()["instancename"]) ?? "ClockWork";
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003F00 File Offset: 0x00002100
		private void ClearCache()
		{
			foreach (Group group in (Group[])Enum.GetValues(typeof(Group)))
			{
				this.RemoveSettingsFromCache(group);
			}
			foreach (Setting s in (Setting[])Enum.GetValues(typeof(Setting)))
			{
				this.RemoveSettingFromCache(s);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003F6B File Offset: 0x0000216B
		private void RemoveSettingsFromCache(Group group)
		{
			ObjectFactory.Resolve<ICacheStorageManager>().Remove(group.GetCacheKey(WebSettingsRestClientManager.GetInstanceName()));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003F82 File Offset: 0x00002182
		private void RemoveSettingFromCache(Setting s)
		{
			ObjectFactory.Resolve<ICacheStorageManager>().Remove(s.GetCacheKey(WebSettingsRestClientManager.GetInstanceName()));
		}

		// Token: 0x04000001 RID: 1
		private static readonly TimeSpan SettingsSlidingExpirationTime = new TimeSpan(0, 30, 0);
	}
}

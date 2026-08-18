using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Settings.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Settings
{
	// Token: 0x0200001C RID: 28
	public class WebSettingsClientManager : IWebSettingsClientManager, IWebService
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x000052F8 File Offset: 0x000034F8
		private static string GetInstanceName()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			return ((string)cacheStorageManager["instancename"]) ?? "ClockWork";
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000532C File Offset: 0x0000352C
		private void ClearCache()
		{
			Group[] array = (Group[])Enum.GetValues(typeof(Group));
			foreach (Group group in array)
			{
				this.RemoveSettingsFromCache(group);
			}
			Setting[] array3 = (Setting[])Enum.GetValues(typeof(Setting));
			foreach (Setting s in array3)
			{
				this.RemoveSettingFromCache(s);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000053B0 File Offset: 0x000035B0
		private void RemoveSettingsFromCache(Group group)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(group.GetCacheKey(WebSettingsClientManager.GetInstanceName()));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000053D8 File Offset: 0x000035D8
		private void RemoveSettingFromCache(Setting s)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(s.GetCacheKey(WebSettingsClientManager.GetInstanceName()));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005400 File Offset: 0x00003600
		public IList<string> GetInstanceNames()
		{
			GetInstanceNameReq getInstanceNameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetInstanceNameReq>();
			getInstanceNameReq.InstanceName = WebSettingsClientManager.GetInstanceName();
			return ClientServiceFactory.GetClientInstance<IWebSettings>().GetInstanceNames(getInstanceNameReq).InstanceNames;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000543C File Offset: 0x0000363C
		public IList<AppSetting> GetSettings(Group group)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[group.GetCacheKey(WebSettingsClientManager.GetInstanceName())];
			bool flag = obj != null;
			IList<AppSetting> result;
			if (flag)
			{
				result = (obj as IList<AppSetting>);
			}
			else
			{
				GetSettingsByGroupReq getSettingsByGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetSettingsByGroupReq>();
				getSettingsByGroupReq.InstanceName = WebSettingsClientManager.GetInstanceName();
				getSettingsByGroupReq.SettingGroup = group;
				IList<AppSetting> list = ClientServiceFactory.GetClientInstance<IWebSettings>().GetSettings(getSettingsByGroupReq).Settings.ToDomainObject();
				cacheStorageManager.Insert(group.GetCacheKey(WebSettingsClientManager.GetInstanceName()), list, WebSettingsClientManager.SettingsSlidingExpirationTime, true);
				result = list;
			}
			return result;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000054CC File Offset: 0x000036CC
		public AppSetting GetSetting(Setting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<AppSetting> list = (IList<AppSetting>)cacheStorageManager[setting.GetGroup().GetCacheKey(WebSettingsClientManager.GetInstanceName())];
			bool flag = list != null;
			AppSetting result;
			if (flag)
			{
				result = list.FirstOrDefault((AppSetting s) => s.LookupSetting.Setting == setting);
			}
			else
			{
				AppSetting appSetting = (AppSetting)cacheStorageManager[setting.GetCacheKey(WebSettingsClientManager.GetInstanceName())];
				bool flag2 = appSetting != null;
				if (flag2)
				{
					result = appSetting;
				}
				else
				{
					GetSettingReq getSettingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetSettingReq>();
					getSettingReq.InstanceName = WebSettingsClientManager.GetInstanceName();
					getSettingReq.Setting = setting;
					appSetting = ClientServiceFactory.GetClientInstance<IWebSettings>().GetSetting(getSettingReq).Setting.ToDomainObject();
					cacheStorageManager.Insert(setting.GetCacheKey(WebSettingsClientManager.GetInstanceName()), appSetting, WebSettingsClientManager.SettingsSlidingExpirationTime, true);
					result = appSetting;
				}
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000055C0 File Offset: 0x000037C0
		public AppSetting GetSetting(Setting setting, string sValue)
		{
			GetSettingFromStringReq getSettingFromStringReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetSettingFromStringReq>();
			getSettingFromStringReq.InstanceName = WebSettingsClientManager.GetInstanceName();
			getSettingFromStringReq.Setting = setting;
			getSettingFromStringReq.StringValue = sValue;
			return ClientServiceFactory.GetClientInstance<IWebSettings>().GetSettingFromString(getSettingFromStringReq).Setting.ToDomainObject();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005610 File Offset: 0x00003810
		public void SaveSetting(AppSetting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(setting.LookupSetting.Group.GetCacheKey(WebSettingsClientManager.GetInstanceName()));
			cacheStorageManager.Remove(setting.LookupSetting.Setting.GetCacheKey(WebSettingsClientManager.GetInstanceName()));
			SaveSettingReq saveSettingReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SaveSettingReq>();
			saveSettingReq.InstanceName = WebSettingsClientManager.GetInstanceName();
			saveSettingReq.Setting = setting.ToDTO();
			ClientServiceFactory.GetClientInstance<IWebSettings>().SaveSetting(saveSettingReq);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000568C File Offset: 0x0000388C
		public void ClearSettingsCache(Group group)
		{
			this.RemoveSettingsFromCache(group);
			ClearSettingsCacheByGroupReq clearSettingsCacheByGroupReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearSettingsCacheByGroupReq>();
			clearSettingsCacheByGroupReq.InstanceName = WebSettingsClientManager.GetInstanceName();
			clearSettingsCacheByGroupReq.Group = group;
			ClientServiceFactory.GetClientInstance<IWebSettings>().ClearSettingsCache(clearSettingsCacheByGroupReq);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000056D0 File Offset: 0x000038D0
		public void ClearSettingsCache()
		{
			this.ClearCache();
			ClearSettingsCacheReq clearSettingsCacheReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ClearSettingsCacheReq>();
			clearSettingsCacheReq.InstanceName = WebSettingsClientManager.GetInstanceName();
			ClientServiceFactory.GetClientInstance<IWebSettings>().ClearSettingsCache(clearSettingsCacheReq);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005708 File Offset: 0x00003908
		public T GetSettingValue<T>(Setting setting)
		{
			AppSetting setting2 = this.GetSetting(setting);
			object obj = setting2.Value;
			bool flag = obj == null;
			T result;
			if (flag)
			{
				result = setting2.LookupSetting.GetDefaultValue<T>();
			}
			else
			{
				bool flag2 = obj is T;
				if (flag2)
				{
					result = (T)((object)obj);
				}
				else
				{
					bool flag3 = obj is string;
					if (flag3)
					{
						bool flag4 = typeof(T) == typeof(bool);
						if (flag4)
						{
							bool flag5 = "1yestrue".IndexOf(((string)obj).ToLower()) >= 0;
							obj = flag5;
							result = (T)((object)obj);
						}
						else
						{
							bool flag6 = typeof(T) != typeof(int);
							if (flag6)
							{
								result = setting2.LookupSetting.GetDefaultValue<T>();
							}
							else
							{
								string text = ((string)obj).Trim();
								bool flag7 = text.Length > 0;
								int num;
								if (flag7)
								{
									try
									{
										num = int.Parse(text);
									}
									catch
									{
										num = 0;
									}
								}
								else
								{
									num = 0;
								}
								obj = num;
								result = (T)((object)obj);
							}
						}
					}
					else
					{
						bool flag8 = !(obj is int[]);
						if (flag8)
						{
							result = setting2.LookupSetting.GetDefaultValue<T>();
						}
						else
						{
							int[] array = (int[])obj;
							bool flag9 = typeof(T) == typeof(int);
							if (flag9)
							{
								obj = array[0];
								result = (T)((object)obj);
							}
							else
							{
								bool flag10 = typeof(T) != typeof(string);
								if (flag10)
								{
									result = setting2.LookupSetting.GetDefaultValue<T>();
								}
								else
								{
									StringBuilder stringBuilder = new StringBuilder();
									foreach (int num2 in array)
									{
										bool flag11 = stringBuilder.Length > 0;
										if (flag11)
										{
											stringBuilder.Append(",");
										}
										stringBuilder.Append(num2.ToString());
									}
									obj = stringBuilder.ToString();
									result = (T)((object)obj);
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000004 RID: 4
		private static readonly TimeSpan SettingsSlidingExpirationTime = new TimeSpan(0, 30, 0);
	}
}

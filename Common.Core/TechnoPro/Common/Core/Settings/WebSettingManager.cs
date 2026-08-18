using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClockWorkLogger;
using TechnoPro.Common.DAO.Impl.Settings;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Settings.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Settings
{
	// Token: 0x02000048 RID: 72
	public class WebSettingManager : IWebSettingManager, IBaseOperationContext<SettingsOperationContext>
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000112FF File Offset: 0x0000F4FF
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00011307 File Offset: 0x0000F507
		private ILookupSettingManager LookupSettingManager { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00011310 File Offset: 0x0000F510
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00011318 File Offset: 0x0000F518
		private ISettingDAO SettingsDAO { get; set; }

		// Token: 0x060002F5 RID: 757 RVA: 0x00011321 File Offset: 0x0000F521
		public WebSettingManager(SettingsOperationContext settingsOperationContext)
		{
			this.SettingsDAO = new SettingDAO(settingsOperationContext);
			this.LookupSettingManager = new LookupSettingManager(settingsOperationContext);
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00011348 File Offset: 0x0000F548
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x00011365 File Offset: 0x0000F565
		public SettingsOperationContext OpContext
		{
			get
			{
				return this.SettingsDAO.OpContext;
			}
			set
			{
				this.SettingsDAO.OpContext = value;
			}
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00011378 File Offset: 0x0000F578
		public IList<AppSetting> GetSettings(Group group)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[group.GetCacheKey(this.OpContext.InstanceName)];
			bool flag = obj != null;
			IList<AppSetting> result;
			if (flag)
			{
				result = (obj as IList<AppSetting>);
			}
			else
			{
				IList<LookupSetting> allLookupSettings = this.LookupSettingManager.GetAllLookupSettings(group);
				IList<AppSetting> settings = this.SettingsDAO.GetSettings(group);
				Dictionary<Setting, AppSetting> dictionary = new Dictionary<Setting, AppSetting>();
				foreach (AppSetting appSetting in settings)
				{
					bool flag2 = Enum.IsDefined(typeof(Setting), appSetting.LookupSetting.Setting);
					if (flag2)
					{
						dictionary.Add(appSetting.LookupSetting.Setting, appSetting);
					}
				}
				foreach (LookupSetting lookupSetting in allLookupSettings)
				{
					bool flag3 = !dictionary.ContainsKey(lookupSetting.Setting);
					if (flag3)
					{
						dictionary.Add(lookupSetting.Setting, new AppSetting
						{
							LookupSetting = lookupSetting,
							Value = lookupSetting.Setting.GetDefaultValue(this.OpContext)
						});
					}
				}
				List<AppSetting> list = dictionary.Values.ToList<AppSetting>();
				list.Sort(delegate(AppSetting s1, AppSetting s2)
				{
					bool flag4 = s1 == null && s2 == null;
					int result2;
					if (flag4)
					{
						result2 = 0;
					}
					else
					{
						bool flag5 = s1 == null;
						if (flag5)
						{
							result2 = -1;
						}
						else
						{
							bool flag6 = s2 == null;
							if (flag6)
							{
								result2 = 1;
							}
							else
							{
								LookupSetting lookupSetting2 = s1.LookupSetting;
								LookupSetting lookupSetting3 = s2.LookupSetting;
								bool flag7 = lookupSetting2 == null && lookupSetting3 == null;
								if (flag7)
								{
									result2 = 0;
								}
								else
								{
									bool flag8 = lookupSetting2 == null;
									if (flag8)
									{
										result2 = -1;
									}
									else
									{
										bool flag9 = lookupSetting3 == null;
										if (flag9)
										{
											result2 = 1;
										}
										else
										{
											result2 = string.Concat(new string[]
											{
												lookupSetting2.GroupName,
												": ",
												lookupSetting2.SubGroup,
												": ",
												lookupSetting2.Name
											}).CompareTo(string.Concat(new string[]
											{
												lookupSetting3.GroupName,
												": ",
												lookupSetting3.SubGroup,
												": ",
												lookupSetting3.Name
											}));
										}
									}
								}
							}
						}
					}
					return result2;
				});
				cacheStorageManager.Insert(group.GetCacheKey(this.OpContext.InstanceName), list, WebSettingManager.SettingsSlidingExpirationTime, true);
				result = list;
			}
			return result;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00011540 File Offset: 0x0000F740
		public AppSetting GetSetting(Setting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<AppSetting> list = (IList<AppSetting>)cacheStorageManager[setting.GetGroup().GetCacheKey(this.OpContext.InstanceName)];
			bool flag = list != null;
			AppSetting result;
			if (flag)
			{
				result = list.FirstOrDefault((AppSetting appSetting) => appSetting.LookupSetting.Setting == setting);
			}
			else
			{
				AppSetting appSetting3 = (AppSetting)cacheStorageManager[setting.GetCacheKey(this.OpContext.InstanceName)];
				bool flag2 = appSetting3 != null;
				if (flag2)
				{
					CWLogger.Logger.Trace("WebSettingsManager::GetSetting:: Getting setting '{0}' from setting cache", setting);
					result = appSetting3;
				}
				else
				{
					AppSetting appSetting2 = this.SettingsDAO.GetSetting(setting);
					bool flag3 = appSetting2 == null;
					if (flag3)
					{
						CWLogger.Logger.Trace("WebSettingsManager::GetSetting:: Getting setting '{0}' from default value", setting);
						LookupSettingManager lookupSettingManager = new LookupSettingManager(this.OpContext);
						appSetting2 = new AppSetting
						{
							LookupSetting = lookupSettingManager.GetLookupSetting(setting),
							Value = setting.GetDefaultValue(this.OpContext)
						};
					}
					else
					{
						CWLogger.Logger.Trace("WebSettingsManager::GetSetting:: Getting setting '{0}' from Database", setting);
					}
					CWLogger.Logger.Trace("WebSettingsManager::GetSetting:: Inserting setting '{0}' to cache", setting);
					cacheStorageManager.Insert(setting.GetCacheKey(this.OpContext.InstanceName), appSetting2, WebSettingManager.SettingsSlidingExpirationTime, true);
					result = appSetting2;
				}
			}
			return result;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000116E0 File Offset: 0x0000F8E0
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
				bool flag2 = !(obj is T);
				if (flag2)
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
							bool flag6 = typeof(T) == typeof(int);
							if (flag6)
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
							else
							{
								result = setting2.LookupSetting.GetDefaultValue<T>();
							}
						}
					}
					else
					{
						bool flag8 = obj is int[];
						if (flag8)
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
								bool flag10 = typeof(T) == typeof(string);
								if (flag10)
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
								else
								{
									result = setting2.LookupSetting.GetDefaultValue<T>();
								}
							}
						}
						else
						{
							result = setting2.LookupSetting.GetDefaultValue<T>();
						}
					}
				}
				else
				{
					result = (T)((object)obj);
				}
			}
			return result;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001191C File Offset: 0x0000FB1C
		public T GetSettingValue<T>(int settingCode)
		{
			return this.GetSettingValue<T>((Setting)settingCode);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00011938 File Offset: 0x0000FB38
		public void Save(AppSetting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			CWLogger.Logger.Trace("WebSettingsManager::Save:: Cleaning web settings cache, instancename='{2}', group='{0}', setting='{1}'", setting.LookupSetting.Group, setting.LookupSetting.Setting, this.OpContext.InstanceName);
			cacheStorageManager.Remove(setting.LookupSetting.Group.GetCacheKey(this.OpContext.InstanceName));
			cacheStorageManager.Remove(setting.LookupSetting.Setting.GetCacheKey(this.OpContext.InstanceName));
			this.SettingsDAO.Save(setting);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public void RemoveSettings(Group group)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(group.GetCacheKey(this.OpContext.InstanceName));
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00011A04 File Offset: 0x0000FC04
		public void ClearCache()
		{
			Group[] array = (Group[])Enum.GetValues(typeof(Group));
			foreach (Group group in array)
			{
				this.RemoveSettings(group);
			}
			Setting[] array3 = (Setting[])Enum.GetValues(typeof(Setting));
			foreach (Setting s in array3)
			{
				this.RemoveSettingFromCache(s);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00011A88 File Offset: 0x0000FC88
		public IList<string> GetInstanceNames()
		{
			return this.SettingsDAO.GetInstanceNames();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public AppSetting GetSetting(Setting setting, string sValue)
		{
			return this.SettingsDAO.GetSetting(setting, sValue);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00011AC8 File Offset: 0x0000FCC8
		private void RemoveSettingFromCache(Setting s)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(s.GetCacheKey(this.OpContext.InstanceName));
		}

		// Token: 0x04000090 RID: 144
		private static readonly TimeSpan SettingsSlidingExpirationTime = new TimeSpan(0, 30, 0);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.Common.DAO.Impl.Settings;
using TechnoPro.Common.DAO.Settings;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Public.Entities.Settings.Adapters;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.Settings
{
	// Token: 0x02000046 RID: 70
	[Obsolete("Use WebSettingManager instead")]
	public class SettingManager : ISettingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00010654 File Offset: 0x0000E854
		private ILookupSettingManager lookupSettingManager
		{
			get
			{
				ILookupSettingManager result;
				if ((result = this.lm) == null)
				{
					result = (this.lm = new LookupSettingManager(new SettingsOperationContext(this.OpContext, this.InstanceName)));
				}
				return result;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0001068C File Offset: 0x0000E88C
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x000106B2 File Offset: 0x0000E8B2
		public static ISettingManager CurrentInstance
		{
			get
			{
				ISettingManager result;
				if ((result = SettingManager._currentInstance) == null)
				{
					result = (SettingManager._currentInstance = new SettingManager());
				}
				return result;
			}
			set
			{
				SettingManager._currentInstance = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x000106BB File Offset: 0x0000E8BB
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x000106C3 File Offset: 0x0000E8C3
		public ISettingDAO dao { get; set; }

		// Token: 0x060002D3 RID: 723 RVA: 0x000106CC File Offset: 0x0000E8CC
		public SettingManager() : this("ClockWork")
		{
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000106DC File Offset: 0x0000E8DC
		public SettingManager(string instanceName)
		{
			this._instanceName = "ClockWork";
			base..ctor();
			this.InstanceName = instanceName;
			this.dao = new SettingDAO(new SettingsOperationContext
			{
				InstanceName = this.InstanceName,
				WhoAmI = 0
			});
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001072C File Offset: 0x0000E92C
		public SettingManager(string instanceName, OperationContext opContext)
		{
			this._instanceName = "ClockWork";
			base..ctor();
			this.InstanceName = instanceName;
			this.OpContext = opContext;
			bool flag = this.OpContext == null;
			if (flag)
			{
				this.OpContext = new OperationContext();
			}
			this.dao = new SettingDAO(new SettingsOperationContext(this.OpContext, this.InstanceName));
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00010792 File Offset: 0x0000E992
		public SettingManager(OperationContext opContext) : this("ClockWork", opContext)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x000107A4 File Offset: 0x0000E9A4
		public static ISettingManager GetInstance()
		{
			return SettingManager.GetInstance("ClockWork");
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000107C0 File Offset: 0x0000E9C0
		public static ISettingManager GetInstance(OperationContext opContext)
		{
			return SettingManager.GetInstance("ClockWork", opContext);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public static ISettingManager GetInstance(string instanceName)
		{
			bool flag = SettingManager.CurrentInstance == null || !SettingManager.CurrentInstance.InstanceName.ToLower().Equals(instanceName.ToLower());
			if (flag)
			{
				SettingManager.CurrentInstance = new SettingManager
				{
					InstanceName = instanceName
				};
			}
			return SettingManager.CurrentInstance;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010838 File Offset: 0x0000EA38
		public static ISettingManager GetInstance(string instanceName, OperationContext opContext)
		{
			bool flag = SettingManager.CurrentInstance == null || !SettingManager.CurrentInstance.InstanceName.ToLower().Equals(instanceName.ToLower());
			if (flag)
			{
				SettingManager.CurrentInstance = new SettingManager
				{
					InstanceName = instanceName,
					OpContext = opContext
				};
			}
			return SettingManager.CurrentInstance;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00010898 File Offset: 0x0000EA98
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000108C0 File Offset: 0x0000EAC0
		public OperationContext OpContext
		{
			get
			{
				return (this.dao == null) ? null : this.dao.OpContext;
			}
			set
			{
				bool flag = this.dao != null;
				if (flag)
				{
					this.dao.OpContext = ((value is SettingsOperationContext) ? ((SettingsOperationContext)value) : new SettingsOperationContext(value, this.InstanceName));
				}
				else
				{
					this.dao = new SettingDAO((value is SettingsOperationContext) ? ((SettingsOperationContext)value) : new SettingsOperationContext(value, this.InstanceName));
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00010930 File Offset: 0x0000EB30
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

		// Token: 0x060002DE RID: 734 RVA: 0x000109B4 File Offset: 0x0000EBB4
		public void RemoveSettings(Group group)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[group];
			bool flag = obj != null;
			if (flag)
			{
				cacheStorageManager.Remove(group);
			}
			Setting[] source = (Setting[])Enum.GetValues(typeof(Setting));
			List<Setting> list = (from g in source
			where g.GetGroup() == @group
			select g).ToList<Setting>();
			foreach (Setting setting in list)
			{
				string key = this.InstanceName + "." + setting.ToString();
				cacheStorageManager.Remove(key);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public IList<AppSetting> GetSettings(Group group)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			object obj = cacheStorageManager[group];
			bool flag = obj != null;
			IList<AppSetting> result;
			if (flag)
			{
				result = (obj as IList<AppSetting>);
			}
			else
			{
				IList<LookupSetting> allLookupSettings = this.lookupSettingManager.GetAllLookupSettings(group);
				IList<AppSetting> settings = this.dao.GetSettings(group);
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
				cacheStorageManager.Insert(group, list, SettingManager.SettingsSlidingExpirationTime, true);
				result = list;
			}
			return result;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00010C4C File Offset: 0x0000EE4C
		public AppSetting GetSetting(Setting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			IList<AppSetting> list = (IList<AppSetting>)cacheStorageManager[setting.GetGroup()];
			bool flag = list != null;
			AppSetting result;
			if (flag)
			{
				result = list.FirstOrDefault((AppSetting appSetting) => appSetting.LookupSetting.Setting == setting);
			}
			else
			{
				AppSetting appSetting3 = (AppSetting)cacheStorageManager[string.Format("{0}.{1}", this.InstanceName, setting.ToString())];
				bool flag2 = appSetting3 != null;
				if (flag2)
				{
					result = appSetting3;
				}
				else
				{
					AppSetting appSetting2 = this.dao.GetSetting(setting);
					bool flag3 = appSetting2 == null;
					if (flag3)
					{
						LookupSettingManager lookupSettingManager = new LookupSettingManager(new SettingsOperationContext(this.OpContext, this.InstanceName));
						appSetting2 = new AppSetting
						{
							LookupSetting = lookupSettingManager.GetLookupSetting(setting),
							Value = setting.GetDefaultValue(this.OpContext)
						};
					}
					cacheStorageManager.Insert(string.Format("{0}.{1}", this.InstanceName, setting.ToString()), appSetting2, SettingManager.SettingsSlidingExpirationTime, true);
					result = appSetting2;
				}
			}
			return result;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010D90 File Offset: 0x0000EF90
		public AppSetting GetSetting(LookupSetting lookupSetting)
		{
			return this.GetSetting(lookupSetting.Setting);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public AppSetting GetSetting(int settingCode)
		{
			return this.GetSetting((Setting)settingCode);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00010DCC File Offset: 0x0000EFCC
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

		// Token: 0x060002E4 RID: 740 RVA: 0x00011008 File Offset: 0x0000F208
		public T GetSettingValue<T>(int settingCode)
		{
			return this.GetSettingValue<T>((Setting)settingCode);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00011024 File Offset: 0x0000F224
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0001103C File Offset: 0x0000F23C
		public string InstanceName
		{
			get
			{
				return this._instanceName;
			}
			set
			{
				this._instanceName = value;
				bool flag = this.dao != null && this.dao.OpContext != null;
				if (flag)
				{
					this.dao.OpContext.InstanceName = value;
				}
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00011080 File Offset: 0x0000F280
		public void Save(AppSetting setting)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(setting.LookupSetting.Group);
			cacheStorageManager.Remove(setting.LookupSetting.Setting);
			this.dao.Save(setting);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000110D0 File Offset: 0x0000F2D0
		public void SetStringValue(AppSetting setting, string sValue)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Remove(setting.LookupSetting.Group);
			cacheStorageManager.Remove(setting.LookupSetting.Setting);
			bool flag = sValue == null;
			if (flag)
			{
				setting.Value = null;
			}
			else
			{
				this.dao.SetStringValue(setting, sValue);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00011134 File Offset: 0x0000F334
		public IList<string> GetInstanceNames()
		{
			return this.dao.GetInstanceNames();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00011154 File Offset: 0x0000F354
		private void RemoveSettingFromCache(Setting s)
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = string.Format("{0}.{1}", this.InstanceName, s.ToString());
			object obj = cacheStorageManager[key];
			bool flag = obj != null;
			if (flag)
			{
				cacheStorageManager.Remove(key);
			}
		}

		// Token: 0x04000088 RID: 136
		private ILookupSettingManager lm;

		// Token: 0x04000089 RID: 137
		private static ISettingManager _currentInstance;

		// Token: 0x0400008B RID: 139
		public static readonly TimeSpan SettingsSlidingExpirationTime = new TimeSpan(0, 30, 0);

		// Token: 0x0400008C RID: 140
		private string _instanceName;
	}
}

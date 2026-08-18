using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.ICore.AppointmentSync;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentSync;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.Core.AppointmentSync
{
	// Token: 0x02000134 RID: 308
	public class AppointmentSyncSettingsManager : IAppointmentSyncSettingsManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000D4E RID: 3406 RVA: 0x00060EA1 File Offset: 0x0005F0A1
		public AppointmentSyncSettingsManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.LoadSyncSettings("ClockWork");
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x00060EBF File Offset: 0x0005F0BF
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x00060EC7 File Offset: 0x0005F0C7
		public OperationContext OpContext { get; set; }

		// Token: 0x06000D51 RID: 3409 RVA: 0x00060ED0 File Offset: 0x0005F0D0
		public SyncApplicationSettings LoadSyncSettings(string settingsInstanceName = "ClockWork")
		{
			ISettingManager instance = SettingManager.GetInstance(settingsInstanceName);
			instance.ClearCache();
			string xml = "<?xml version='1.0'?>" + instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_ClockWorkUsersToSync);
			List<ClockWorkExternalApplicationSyncUser> disabledSyncUsers;
			List<ClockWorkExternalApplicationSyncUser> syncUsers = this.ParseSyncUsers(xml, out disabledSyncUsers);
			string settingValue = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_SlowSyncRunningSchedule);
			List<TimeSpan> list = new List<TimeSpan>();
			bool flag = string.IsNullOrEmpty(settingValue);
			if (flag)
			{
				list.Add(new TimeSpan(12, 0, 0));
				list.Add(new TimeSpan(20, 0, 0));
			}
			else
			{
				using (StringReader stringReader = new StringReader(settingValue))
				{
					string text;
					do
					{
						text = stringReader.ReadLine();
						TimeSpan item;
						bool flag2 = TimeSpan.TryParse(text, out item);
						if (flag2)
						{
							list.Add(item);
						}
					}
					while (text != null);
				}
			}
			string settingValue2 = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_GoogleServiceAccountEmail);
			string settingValue3 = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_GoogleServiceAccountClientId);
			string settingValue4 = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_GoogleServiceAccountPKCS12Filename);
			SyncApplicationSettings syncApplicationSettings = new SyncApplicationSettings();
			syncApplicationSettings.SyncIsActive = instance.GetSettingValue<bool>(Setting.CLOCKWORKAPPOINTMENTSYNC_AppointmentSyncIsActive);
			syncApplicationSettings.SyncConnection = new SyncApplicationConnection
			{
				UserCredentials = new SyncApplicationConnection.Credentials
				{
					Username = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_DelegateUserName),
					Password = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_DelegatePassword)
				},
				ServiceCredentials = new SyncApplicationConnection.ServiceAccountCredentials
				{
					ServiceAccountEmail = settingValue2,
					ServiceAccountPKCS12Filename = settingValue4,
					ServiceClientId = settingValue3
				},
				ApplicationUrl = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_ServerUrl),
				ApplicationVersion = instance.GetSettingValue<string>(Setting.CLOCKWORKAPPOINTMENTSYNC_ServerVersion),
				UseAutoDiscoverUrl = instance.GetSettingValue<bool>(Setting.CLOCKWORKAPPOINTMENTSYNC_UseAutodiscoverUrl)
			};
			syncApplicationSettings.SyncFrequencyInMinutes = instance.GetSettingValue<int>(Setting.CLOCKWORKAPPOINTMENTSYNC_SyncFequency);
			syncApplicationSettings.ShowNonOutlookUsersInMemoWhenCreatingUpdatingOutlookAppointment = instance.GetSettingValue<bool>(Setting.CLOCKWORKAPPOINTMENTSYNC_ShowNonExternalApplicationUsersInExternalAppointmentMemo);
			syncApplicationSettings.SyncUsers = syncUsers;
			syncApplicationSettings.DisabledSyncUsers = disabledSyncUsers;
			syncApplicationSettings.SyncIntervalInDays = instance.GetSettingValue<int>(Setting.CLOCKWORKAPPOINTMENTSYNC_SyncChunkDayCount);
			syncApplicationSettings.SyncIntervalCount = instance.GetSettingValue<int>(Setting.CLOCKWORKAPPOINTMENTSYNC_SyncChunkIterationCount);
			syncApplicationSettings.SkipAllDayAppointments = instance.GetSettingValue<bool>(Setting.CLOCKWORKAPPOINTMENTSYNC_SkipAllDayAppointments);
			syncApplicationSettings.FastSyncIsActive = instance.GetSettingValue<bool>(Setting.CLOCKWORKAPPOINTMENTSYNC_AppointmentFastSyncIsActive);
			syncApplicationSettings.SkipPrivateAppointments = instance.GetSettingValue<bool>(Setting.CLOCKWORKAPPOINTMENTSYNC_SkipPrivateAppointments);
			syncApplicationSettings.TimeToWaitBeforeStartNewFastSyncInMinutes = instance.GetSettingValue<int>(Setting.CLOCKWORKAPPOINTMENTSYNC_FastSyncFrequency);
			syncApplicationSettings.SlowSyncDayRunningTimeSchedule = list;
			syncApplicationSettings.SkipRecurringAppointmentsInFastSync = instance.GetSettingValue<bool>(Setting.CLOCKWORKAPPOINTMENTSYNC_SkipRecurringAppointmentsInFastSync);
			SyncApplicationSettings result = syncApplicationSettings;
			this.SyncSettings = syncApplicationSettings;
			return result;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x00061158 File Offset: 0x0005F358
		// (set) Token: 0x06000D53 RID: 3411 RVA: 0x00061180 File Offset: 0x0005F380
		public SyncApplicationSettings SyncSettings
		{
			get
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				return (SyncApplicationSettings)cacheStorageManager["SyncApplicationSettings"];
			}
			private set
			{
				bool flag = value == null;
				if (!flag)
				{
					ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
					cacheStorageManager.Insert("SyncApplicationSettings", value);
				}
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000611AC File Offset: 0x0005F3AC
		private List<ClockWorkExternalApplicationSyncUser> ParseSyncUsers(string xml, out List<ClockWorkExternalApplicationSyncUser> disabledSyncUsers)
		{
			disabledSyncUsers = new List<ClockWorkExternalApplicationSyncUser>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(xml);
			XPathNavigator xpathNavigator = xmlDocument.CreateNavigator();
			xpathNavigator.MoveToRoot();
			xpathNavigator.MoveToFirstChild();
			xpathNavigator.MoveToFirstChild();
			List<ClockWorkExternalApplicationSyncUser> list = new List<ClockWorkExternalApplicationSyncUser>();
			do
			{
				XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
				bool flag = xpathNavigator2.MoveToFirstChild();
				if (flag)
				{
					ClockWorkExternalApplicationSyncUser clockWorkExternalApplicationSyncUser = new ClockWorkExternalApplicationSyncUser
					{
						SyncIsEnabled = true
					};
					do
					{
						string name = xpathNavigator2.Name;
						string value = xpathNavigator2.Value;
						string text = name;
						string a = text;
						if (!(a == "personid"))
						{
							if (!(a == "exchangeusername"))
							{
								if (a == "syncisenabled")
								{
									clockWorkExternalApplicationSyncUser.SyncIsEnabled = (!string.IsNullOrEmpty(value) && Convert.ToBoolean(value));
								}
							}
							else
							{
								clockWorkExternalApplicationSyncUser.ExternalApplicationUsername = value.ToLower();
							}
						}
						else
						{
							int personId;
							bool flag2 = !int.TryParse(value, out personId);
							if (flag2)
							{
								personId = 0;
							}
							clockWorkExternalApplicationSyncUser.ClockWorkUser = new PersonBase
							{
								PersonId = personId
							};
						}
					}
					while (xpathNavigator2.MoveToNext());
					bool flag3 = clockWorkExternalApplicationSyncUser.ClockWorkUser != null && clockWorkExternalApplicationSyncUser.ClockWorkUser.PersonId > 0 && !string.IsNullOrEmpty(clockWorkExternalApplicationSyncUser.ExternalApplicationUsername);
					if (flag3)
					{
						bool syncIsEnabled = clockWorkExternalApplicationSyncUser.SyncIsEnabled;
						if (syncIsEnabled)
						{
							list.Add(clockWorkExternalApplicationSyncUser);
						}
						else
						{
							disabledSyncUsers.Add(clockWorkExternalApplicationSyncUser);
						}
					}
				}
			}
			while (xpathNavigator.MoveToNext());
			return list;
		}
	}
}

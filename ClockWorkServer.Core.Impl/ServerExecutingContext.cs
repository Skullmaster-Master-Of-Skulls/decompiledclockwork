using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Timers;
using System.Web.Hosting;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Core;
using TechnoPro.Common.Core.AppointmentsReminder;
using TechnoPro.Common.Core.ClockWorkServerConnection;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.Core.Timers;
using TechnoPro.Common.Core.UserAccount;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore;
using TechnoPro.Common.ICore.AppointmentsReminder;
using TechnoPro.Common.ICore.ClockWorkServerConnection;
using TechnoPro.Common.ICore.Membership;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.ICore.Timers;
using TechnoPro.Common.ICore.UserAccount;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsReminder;
using TechnoPro.Common.Public.Entities.Caching;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.Membership;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Timers;
using TechnoPro.Common.Public.Entities.UserAccount.LoginTracking;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Win32;

namespace TechnoPro.ClockWorkServer.Core.Impl
{
	// Token: 0x02000003 RID: 3
	public class ServerExecutingContext : ApplicationContext, IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002058 File Offset: 0x00000258
		public eClockWorkServerInstanceName ClockWorkServerInstanceName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002069 File Offset: 0x00000269
		public ITimerManager Timers { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002072 File Offset: 0x00000272
		[Obsolete("Use ObjectFactory.Resolve<ServerExecutingContext> instead")]
		public static ServerExecutingContext CurrentContext
		{
			get
			{
				return ObjectFactory.Resolve<ServerExecutingContext>();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002079 File Offset: 0x00000279
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002081 File Offset: 0x00000281
		public string ServerVirtualDirectory { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000208A File Offset: 0x0000028A
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002092 File Offset: 0x00000292
		public string ServerVirtualApplicationName { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000209B File Offset: 0x0000029B
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020A3 File Offset: 0x000002A3
		public string ServerResourcesPath { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020AC File Offset: 0x000002AC
		public ProductLicenseState ServerLicenseStatus
		{
			get
			{
				bool flag = this._serverLicenseStatus == ProductLicenseState.NoneLicense;
				if (flag)
				{
					LicensingManager licensingManager = new LicensingManager();
					DateTime? dateTime;
					this._serverLicenseStatus = licensingManager.GetProductState("ClockWork Server", out dateTime);
				}
				return this._serverLicenseStatus;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000011 RID: 17 RVA: 0x000020EC File Offset: 0x000002EC
		// (remove) Token: 0x06000012 RID: 18 RVA: 0x00002124 File Offset: 0x00000324
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<AppointmentsReminderEventArgs> OnAppointmentsReminder;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000013 RID: 19 RVA: 0x0000215C File Offset: 0x0000035C
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x00002194 File Offset: 0x00000394
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event OnExpiredSessionsRemovedEventHandler OnExpiredSessionsRemoved;

		// Token: 0x06000015 RID: 21 RVA: 0x000021CC File Offset: 0x000003CC
		public ServerExecutingContext()
		{
			string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("serverinstancename");
			CWLogger.Logger.Trace("Creating Server Application Context, serverinstancename={0} ...", appSettingsByNameUsingProtection ?? "NULL");
			this.ClockWorkServerInstanceName = ((!string.IsNullOrEmpty(appSettingsByNameUsingProtection) && Enum.IsDefined(typeof(eClockWorkServerInstanceName), appSettingsByNameUsingProtection)) ? ((eClockWorkServerInstanceName)Enum.Parse(typeof(eClockWorkServerInstanceName), appSettingsByNameUsingProtection)) : eClockWorkServerInstanceName.ClockWorkServer);
			this.ServerVirtualDirectory = HostingEnvironment.ApplicationPhysicalPath;
			this.ServerVirtualApplicationName = ((HostingEnvironment.ApplicationVirtualPath != null) ? HostingEnvironment.ApplicationVirtualPath.Substring(1) : eClockWorkServerInstanceName.ClockWorkServer.ToString());
			this.ServerResourcesPath = Path.Combine(this.ServerVirtualDirectory, "Resources");
			CWLogger.Logger.Trace("ServerExecutingContext: Starting up {1} from '{0}'", this.ServerVirtualDirectory, this.ServerVirtualApplicationName);
			this.Timers = TimerManager.Current;
			base.ExecutingPath = this.GetServerBinPath();
			CWLogger.Logger.Trace("ServerExecutingContext:CheckDatabaseConnectivity: Checking connectivity to Database ...");
			bool flag = this.TestDatabaseConnectivity();
			if (flag)
			{
				CWLogger.Logger.Trace("ServerExecutingContext:CheckDatabaseConnectivity: Connection to Database is succesful");
				this.ConnectedToDatabase = true;
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				clockWork.ExecuteNonQuery("SET ARITHABORT ON");
				this.Init();
			}
			else
			{
				CWLogger.Logger.Trace("ServerExecutingContext:CheckDatabaseConnectivity: Connection to Database failed. Starting up timer to check database connectivity ...");
				this.Timers.AddTimer(new ClockWorkServerTimer
				{
					Name = "CheckDatabaseConnectivityTimer",
					TimeInterval = 30000.0,
					TimeElapsedFunc = delegate(object o, ElapsedEventArgs args)
					{
						this.CheckDatabaseConnectivity();
					},
					Enabled = true
				});
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002398 File Offset: 0x00000598
		public string GetServerBinPath()
		{
			return Path.Combine(this.ServerVirtualDirectory, "bin");
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023BC File Offset: 0x000005BC
		public string GetServerFileSystemStorageFolder()
		{
			return Path.Combine(this.ServerVirtualDirectory, "FileSystem Storage");
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E0 File Offset: 0x000005E0
		private bool TestDatabaseConnectivity()
		{
			bool result;
			try
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				result = clockWork.TestDatabaseConnectivity("select * from people where 1 = 2");
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000241C File Offset: 0x0000061C
		private void membershipManager_OnUserLogon(object sender, OnLogonEventArgs e)
		{
			CWLogger.Logger.Trace("ServerExecutingContext::OnUserLogon:: Before clearing DB cache for user='{0}'", e.PersonId);
			IUserDatabaseCacheStorageManager userDatabaseCacheStorageManager = new UserDatabaseCacheStorageManager();
			userDatabaseCacheStorageManager.Clear(e.PersonId);
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			string key = "u" + eServerCacheItemType.uAllowedAppTypeIds.ToString() + "_" + e.PersonId.ToString();
			cacheStorageManager.Remove(key);
			CWLogger.Logger.Trace("ServerExecutingContext::OnUserLogon:: After clearing DB cache for user='{0}'", e.PersonId);
			LoginInfo loginInfo = new LoginInfo
			{
				PersonId = e.PersonId,
				LoginDate = DateTime.Now,
				Ip = ((e.ClientParameters == null || !e.ClientParameters.ContainsKey("IP")) ? "" : (e.ClientParameters["IP"] ?? "")),
				ClockWorkVersion = ((e.ClientParameters == null || !e.ClientParameters.ContainsKey("CLOCKWORK_VERSION")) ? "" : (e.ClientParameters["CLOCKWORK_VERSION"] ?? "")).DeserializeVersionFromString(),
				NetVersions = ((e.ClientParameters == null || !e.ClientParameters.ContainsKey("NET_VERSIONS")) ? "" : (e.ClientParameters["NET_VERSIONS"] ?? "")).SplitEnumValues<DotNetVersion>()
			};
			IUserLoginTrackingManager userLoginTrackingManager = new UserLoginTrackingManager(new OperationContext
			{
				WhoAmI = e.PersonId
			});
			userLoginTrackingManager.RecordNewLogin(loginInfo);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025B8 File Offset: 0x000007B8
		private void AppointmentsReminderTimerElapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				IAppointmentsReminderManager appointmentsReminderManager = new AppointmentsReminderManager(new OperationContext
				{
					WhoAmI = 0,
					AppContext = this
				});
				IList<AppointmentReminder> list = appointmentsReminderManager.LoadAppointmentsReminder();
				bool flag = list != null && list.Count > 0;
				if (flag)
				{
					CWLogger.Logger.Trace("ServerExecutingContext:AppointmentsReminder: Trying to send {0} appointments reminder to the user ...", list.Count);
					this.FireOnAppointmentsReminder(list);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ServerExecutingContext:: AppointmentsReminder: {0}", ex.ToString()), ex);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002650 File Offset: 0x00000850
		private void FireOnAppointmentsReminder(IList<AppointmentReminder> appReminderList)
		{
			EventHandler<AppointmentsReminderEventArgs> onAppointmentsReminder = this.OnAppointmentsReminder;
			bool flag = onAppointmentsReminder != null;
			if (flag)
			{
				onAppointmentsReminder(this, new AppointmentsReminderEventArgs(appReminderList));
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000267C File Offset: 0x0000087C
		private void ExpiredSessionsTimerElapsed(object sender, ElapsedEventArgs e)
		{
			try
			{
				CWLogger.Logger.Trace("Start to execute expired sessions cleaner on {0}", e.SignalTime);
				ObjectFactory.Resolve<IMembershipManager>().RemoveExpiredSessions();
				bool flag = this.OnExpiredSessionsRemoved != null;
				if (flag)
				{
					this.OnExpiredSessionsRemoved();
				}
				CWLogger.Logger.Trace("Expired Sessions cleaner was executed succesfully on {0}", DateTime.Now.Date.ToString("MMM dd, yyyy hh:mm:ss tt"));
			}
			catch (Exception exception)
			{
				CWLogger.Logger.ErrorException(string.Format("Expired Session cleaner failed on {0}", e.SignalTime), exception);
				throw;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000272C File Offset: 0x0000092C
		protected void Init()
		{
			bool flag = !this.ConnectedToDatabase;
			if (!flag)
			{
				CWLogger.Logger.Trace("ServerExecutingContext:Init: *********** Initializing ClockWorkServer ***************");
				this.LoadLicences();
				this.InitServerConnectionInfo();
				this.InitTimers();
				this.InitUserLogon();
				this.InitPeopleTableCache();
				IMembershipManager membershipManager = ObjectFactory.Resolve<IMembershipManager>();
				membershipManager.LoadAuthenticationSessions();
				CWLogger.Logger.Trace("ServerExecutingContext:Init: *********** End ClockWorkServer Initialization *************");
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000279C File Offset: 0x0000099C
		private void InitServerConnectionInfo()
		{
			IClockWorkServerConnectionInfoManager clockWorkServerConnectionInfoManager = new ClockWorkServerConnectionInfoManager(new ClockWorkServerOperationContext
			{
				WhoAmI = 0,
				ClockWorkServerInstanceName = this.ClockWorkServerInstanceName,
				ClockWorkServerVirtualDirectory = this.ServerVirtualApplicationName
			});
			ClockWorkServerConnectionInfo clockWorkServerConnectionInfo = clockWorkServerConnectionInfoManager.GetClockWorkServerConnectionInfo();
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager();
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(0, eSettingCode.SETTING_ClockWorkServer_PreferredBindingType, false);
			eBindingType bindingType;
			bool flag = !Enum.TryParse<eBindingType>(settingValue_String, out bindingType);
			if (flag)
			{
				bindingType = eBindingType.NetTcpBinding;
			}
			ClockWorkServerPreferredConnectionInfo value = new ClockWorkServerPreferredConnectionInfo
			{
				VirtualDirectory = clockWorkServerConnectionInfo.VirtualDirectory,
				Certificate = clockWorkServerConnectionInfo.Certificate,
				ExternalHostname = clockWorkServerConnectionInfo.HttpHostname,
				ExternalPort = clockWorkServerConnectionInfo.HttpPort,
				Hostname = clockWorkServerConnectionInfo.TcpHostname,
				Port = clockWorkServerConnectionInfo.TcpPort,
				IISVersion = clockWorkServerConnectionInfo.IISVersion,
				IdentityDNS = clockWorkServerConnectionInfo.IdentityDNS,
				BindingType = bindingType
			};
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			cacheStorageManager.Insert("cClockWorkServerPreferredConnectionInfo", value);
			cacheStorageManager.Insert("ServerCertificateString", clockWorkServerConnectionInfo.Certificate.CertificatePublicKey);
			CWLogger.Logger.Trace("ServerExecutingContext: Server Connection info was added to cache ...");
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028BC File Offset: 0x00000ABC
		private void InitTimers()
		{
			CWLogger.Logger.Trace("ServerExecutingContext:InitTimers: Initializing server timers ...");
			this.Timers.AddTimer(new ClockWorkServerTimer
			{
				Name = "ExpiredSessionsTimer",
				TimeElapsedFunc = new Action<object, ElapsedEventArgs>(this.ExpiredSessionsTimerElapsed),
				TimeInterval = (double)this._defaultExpiredSessionTimerInterval,
				Enabled = true
			});
			CWLogger.Logger.Trace("ServerExecutingContext:InitTimers: Expired Sessions Timer was added");
			IMiscSafeManager miscSafeManager = new MiscSafeManager();
			string value = miscSafeManager.GetValue("AppointmentsReminder.Enable");
			string value2 = miscSafeManager.GetValue("AppointmentsReminder.TotalSecondsBetweenCheckingForAppsReminder");
			long num;
			bool flag = string.IsNullOrEmpty(value2) || !long.TryParse(value2, out num);
			if (flag)
			{
				num = 60L;
			}
			bool flag3;
			bool flag2 = string.IsNullOrEmpty(value) || (bool.TryParse(value, out flag3) && flag3);
			this.Timers.AddTimer(new ClockWorkServerTimer
			{
				Name = "AppointmentsReminderTimer",
				TimeElapsedFunc = new Action<object, ElapsedEventArgs>(this.AppointmentsReminderTimerElapsed),
				TimeInterval = (double)(num * 1000L),
				Enabled = flag2
			});
			bool flag4 = flag2;
			if (flag4)
			{
				CWLogger.Logger.Trace("ServerExecutingContext:InitTimers: Appointments Reminder Timer is running every {0} seconds now", num);
			}
			else
			{
				CWLogger.Logger.Trace("ServerExecutingContext:InitTimers: Appointments Reminder Timer is disable");
			}
			CWLogger.Logger.Trace("ServerExecutingContext:InitTimers: End of initializing server timers ...");
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002A0C File Offset: 0x00000C0C
		private void InitUserLogon()
		{
			CWLogger.Logger.Trace("ServerExecutingContext::Init:: Before on user logon event subscriber ...");
			IMembershipManager membershipManager = ObjectFactory.Resolve<IMembershipManager>();
			membershipManager.OnUserLogon += this.membershipManager_OnUserLogon;
			CWLogger.Logger.Trace("ServerExecutingContext::Init:: After on user logon event subscriber ...");
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002A53 File Offset: 0x00000C53
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002A5B File Offset: 0x00000C5B
		public bool ConnectedToDatabase { get; private set; }

		// Token: 0x06000023 RID: 35 RVA: 0x00002A64 File Offset: 0x00000C64
		private void CheckDatabaseConnectivity()
		{
			bool connectedToDatabase = this.ConnectedToDatabase;
			if (!connectedToDatabase)
			{
				CWLogger.Logger.Trace("ServerExecutingContext:CheckDatabaseConnectivity: Checking connectivity to Database ...");
				bool flag = this.TestDatabaseConnectivity();
				if (flag)
				{
					this.ConnectedToDatabase = true;
					CWLogger.Logger.Trace("ServerExecutingContext:CheckDatabaseConnectivity: Connection to Database is succesful");
					this.Timers.RemoveTimer("CheckDatabaseConnectivityTimer");
					CWLogger.Logger.Trace("ServerExecutingContext:CheckDatabaseConnectivity: Removing check database connectivity timer");
					this.Init();
				}
				else
				{
					CWLogger.Logger.Trace("ServerExecutingContext:CheckDatabaseConnectivity: Connection to Database failed");
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AF0 File Offset: 0x00000CF0
		private void InitPeopleTableCache()
		{
			CWLogger.Logger.Trace("ServerExecutingContext:Startup:Starting to load all user objects cache.");
			IPeopleManager peopleManager = new PeopleManager(new OperationContext
			{
				WhoAmI = 0
			});
			List<PersonBase> list = peopleManager.LoadAllUserObjects(true);
			CWLogger.Logger.Trace("ServerExecutingContext:Startup:Completed loading all user objects cache.  Total count is {0}.", (list == null) ? "0" : list.Count.ToString());
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B54 File Offset: 0x00000D54
		private void LoadLicences()
		{
			ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
			LicensingManager licensingManager = new LicensingManager();
			CWLogger.Logger.Trace("ServerExecutingContext:LoadLicenses:Loading CAL license information ...");
			string text = "CAL";
			LicenseKeyInfo productKey = licensingManager.GetProductKey(text);
			bool flag = productKey != null;
			if (flag)
			{
				LicenseState licenseState = licensingManager.GetLicenseState(productKey);
				bool flag2 = licenseState == LicenseState.Updated;
				if (flag2)
				{
					cacheStorageManager.Insert(string.Format("Licenses.{0}", text), productKey);
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002BBF File Offset: 0x00000DBF
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002BD4 File Offset: 0x00000DD4
		private void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
					this.Timers.Dispose();
					IMembershipManager membershipManager = ObjectFactory.Resolve<IMembershipManager>();
					membershipManager.OnUserLogon -= this.membershipManager_OnUserLogon;
				}
				this.disposed = true;
				CWLogger.Logger.Debug("ServerExecutingContext::Dispose::ClockWorkServer has been disposed.");
			}
		}

		// Token: 0x04000003 RID: 3
		private readonly long _defaultExpiredSessionTimerInterval = (long)TimeSpan.FromDays(1.0).TotalMilliseconds;

		// Token: 0x04000007 RID: 7
		private ProductLicenseState _serverLicenseStatus = ProductLicenseState.NoneLicense;

		// Token: 0x0400000B RID: 11
		protected bool disposed = false;
	}
}

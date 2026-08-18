using System;
using System.IO;
using System.Web.Hosting;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerConnection;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.Core.ClockWorkServerConnection;
using TechnoPro.Common.ClientManager.Core.Startup;
using TechnoPro.Common.ClientManager.ICore.ClockWorkServerConnection;
using TechnoPro.Common.ClientManager.ICore.Startup;
using TechnoPro.Common.Configuration;
using TechnoPro.Common.Core.Mappers.ClockWorkServerConnection;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Authentication;
using TechnoPro.Common.Public.Entities.ClockWorkServerConnection;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;
using TechnoPro.Common.UI.ClientManager.Web.Core.Startup;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.UI.ClientManager.Web.Core.Impl.Local.Startup
{
	// Token: 0x02000016 RID: 22
	public class StartupWebClientManager : IStartupWebClientManager
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000047C9 File Offset: 0x000029C9
		public void Startup()
		{
			this.InitializeClockWorkSettings();
			this.InitializeClockWorkServerConnection();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000047DC File Offset: 0x000029DC
		private void InitializeClockWorkServerConnection()
		{
			string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("useclockworkserver");
			bool flag = !string.IsNullOrEmpty(appSettingsByNameUsingProtection) && "1yestrue".IndexOf(appSettingsByNameUsingProtection, StringComparison.OrdinalIgnoreCase) >= 0;
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(new OperationContext
			{
				WhoAmI = 0
			});
			flag = (flag && oldUserSettingManager.GetSettingValue_Bool(0, eSettingCode.SETTING_UseClockWorkServer));
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			bool flag2 = flag;
			if (flag2)
			{
				ClockWorkServerPreferredConnectionInfo clockWorkServerPreferredConnectionInfo = this.GetClockWorkServerPreferredConnectionInfo();
				bool flag3 = clockWorkServerPreferredConnectionInfo != null;
				if (flag3)
				{
					clientCache.ClientClockWorkServerConnectionInfo = clockWorkServerPreferredConnectionInfo;
					IClientStartupClientManager clientStartupClientManager = new ClientStartupClientManager();
					bool flag4 = clientStartupClientManager.CheckConnectivityToServer();
					clientCache.Insert("cClockWorkServerEnabled", flag4);
					clientCache.ClientClockWorkServerConnectionInfo.Certificate = clientStartupClientManager.GetClockWorkServerCertificate();
				}
				else
				{
					clientCache.Insert("cClockWorkServerEnabled", false);
				}
			}
			else
			{
				clientCache.Insert("cClockWorkServerEnabled", false);
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000048C8 File Offset: 0x00002AC8
		private void InitializeClockWorkSettings()
		{
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			clientCache.AuthenticationMode = eAuthenticationMode.PerSession;
			ApplicationContext applicationContext = ObjectFactory.Resolve<ApplicationContext>();
			applicationContext.ExecutingPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "bin");
			string appSettingsByNameUsingProtection = ClockWorkConfigurationManager.GetAppSettingsByNameUsingProtection("instancename");
			clientCache.InstanceName = (string.IsNullOrEmpty(appSettingsByNameUsingProtection) ? "ClockWork" : appSettingsByNameUsingProtection);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004924 File Offset: 0x00002B24
		private ClockWorkServerPreferredConnectionInfo GetClockWorkServerPreferredConnectionInfo()
		{
			string text = (HostingEnvironment.ApplicationVirtualPath != null && !string.IsNullOrEmpty(HostingEnvironment.ApplicationVirtualPath.Substring(1))) ? HostingEnvironment.ApplicationVirtualPath.Substring(1) : "ClockWork";
			RegistryHelper registryHelper = new RegistryHelper();
			string text2 = registryHelper.ReadLocalMachineRegistry<string>(eRegWow64Options.KEY_WOW64_32KEY, new string[]
			{
				"ClockWorkWeb",
				text,
				"ClockWorkServer",
				"DiscoveryServiceEndpoints",
				"PreferedEndpointConnection"
			});
			bool flag = string.IsNullOrEmpty(text2);
			ClockWorkServerPreferredConnectionInfo result;
			if (flag)
			{
				result = null;
			}
			else
			{
				Uri uri = new Uri(text2);
				IClockWorkServerConnectionsClientManager clockWorkServerConnectionsClientManager = new ClockWorkServerConnectionsClientManager();
				ClockWorkServerPreferredConnectionInfoDTO clockWorkServerConnectionInfo = clockWorkServerConnectionsClientManager.GetClockWorkServerConnectionInfo(uri);
				result = ((clockWorkServerConnectionInfo != null) ? clockWorkServerConnectionInfo.ToDomainObject() : null);
			}
			return result;
		}
	}
}

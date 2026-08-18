using System;
using System.Collections.Specialized;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200002E RID: 46
	public static class ConfigurationManager
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000103A4 File Offset: 0x0000E5A4
		internal static bool SetConfigurationSystemInProgress
		{
			get
			{
				return ConfigurationManager.InitState.NotStarted < ConfigurationManager.s_initState && ConfigurationManager.s_initState < ConfigurationManager.InitState.Completed;
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000103BC File Offset: 0x0000E5BC
		internal static void SetConfigurationSystem(IInternalConfigSystem configSystem, bool initComplete)
		{
			object obj = ConfigurationManager.s_initLock;
			lock (obj)
			{
				if (ConfigurationManager.s_initState != ConfigurationManager.InitState.NotStarted)
				{
					throw new InvalidOperationException(SR.GetString("Config_system_already_set"));
				}
				ConfigurationManager.s_configSystem = configSystem;
				if (initComplete)
				{
					ConfigurationManager.s_initState = ConfigurationManager.InitState.Completed;
				}
				else
				{
					ConfigurationManager.s_initState = ConfigurationManager.InitState.Usable;
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0001042C File Offset: 0x0000E62C
		private static void EnsureConfigurationSystem()
		{
			object obj = ConfigurationManager.s_initLock;
			lock (obj)
			{
				if (ConfigurationManager.s_initState < ConfigurationManager.InitState.Usable)
				{
					ConfigurationManager.s_initState = ConfigurationManager.InitState.Started;
					try
					{
						try
						{
							ConfigurationManager.s_configSystem = new ClientConfigurationSystem();
							ConfigurationManager.s_initState = ConfigurationManager.InitState.Usable;
						}
						catch (Exception inner)
						{
							ConfigurationManager.s_initError = new ConfigurationErrorsException(SR.GetString("Config_client_config_init_error"), inner);
							throw ConfigurationManager.s_initError;
						}
					}
					catch
					{
						ConfigurationManager.s_initState = ConfigurationManager.InitState.Completed;
						throw;
					}
				}
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000104D4 File Offset: 0x0000E6D4
		internal static void SetInitError(Exception initError)
		{
			ConfigurationManager.s_initError = initError;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000104E0 File Offset: 0x0000E6E0
		internal static void CompleteConfigInit()
		{
			object obj = ConfigurationManager.s_initLock;
			lock (obj)
			{
				ConfigurationManager.s_initState = ConfigurationManager.InitState.Completed;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00010524 File Offset: 0x0000E724
		private static void PrepareConfigSystem()
		{
			if (ConfigurationManager.s_initState < ConfigurationManager.InitState.Usable)
			{
				ConfigurationManager.EnsureConfigurationSystem();
			}
			if (ConfigurationManager.s_initError != null)
			{
				throw ConfigurationManager.s_initError;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00010546 File Offset: 0x0000E746
		internal static bool SupportsUserConfig
		{
			get
			{
				ConfigurationManager.PrepareConfigSystem();
				return ConfigurationManager.s_configSystem.SupportsUserConfig;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0001055C File Offset: 0x0000E75C
		public static NameValueCollection AppSettings
		{
			get
			{
				object section = ConfigurationManager.GetSection("appSettings");
				if (section == null || !(section is NameValueCollection))
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_appsettings_declaration_invalid"));
				}
				return (NameValueCollection)section;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000237 RID: 567 RVA: 0x00010598 File Offset: 0x0000E798
		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get
			{
				object section = ConfigurationManager.GetSection("connectionStrings");
				if (section == null || section.GetType() != typeof(ConnectionStringsSection))
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_connectionstrings_declaration_invalid"));
				}
				ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)section;
				return connectionStringsSection.ConnectionStrings;
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000105E8 File Offset: 0x0000E7E8
		public static object GetSection(string sectionName)
		{
			if (string.IsNullOrEmpty(sectionName))
			{
				return null;
			}
			ConfigurationManager.PrepareConfigSystem();
			return ConfigurationManager.s_configSystem.GetSection(sectionName);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00010613 File Offset: 0x0000E813
		public static void RefreshSection(string sectionName)
		{
			if (string.IsNullOrEmpty(sectionName))
			{
				return;
			}
			ConfigurationManager.PrepareConfigSystem();
			ConfigurationManager.s_configSystem.RefreshConfig(sectionName);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00010630 File Offset: 0x0000E830
		public static Configuration OpenMachineConfiguration()
		{
			return ConfigurationManager.OpenExeConfigurationImpl(null, true, ConfigurationUserLevel.None, null, false);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0001063C File Offset: 0x0000E83C
		public static Configuration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
		{
			return ConfigurationManager.OpenExeConfigurationImpl(fileMap, true, ConfigurationUserLevel.None, null, false);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00010648 File Offset: 0x0000E848
		public static Configuration OpenExeConfiguration(ConfigurationUserLevel userLevel)
		{
			return ConfigurationManager.OpenExeConfigurationImpl(null, false, userLevel, null, false);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00010654 File Offset: 0x0000E854
		public static Configuration OpenExeConfiguration(string exePath)
		{
			return ConfigurationManager.OpenExeConfigurationImpl(null, false, ConfigurationUserLevel.None, exePath, false);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00010660 File Offset: 0x0000E860
		public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
		{
			return ConfigurationManager.OpenExeConfigurationImpl(fileMap, false, userLevel, null, false);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001066C File Offset: 0x0000E86C
		public static Configuration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad)
		{
			return ConfigurationManager.OpenExeConfigurationImpl(fileMap, false, userLevel, null, preLoad);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00010678 File Offset: 0x0000E878
		private static Configuration OpenExeConfigurationImpl(ConfigurationFileMap fileMap, bool isMachine, ConfigurationUserLevel userLevel, string exePath, bool preLoad = false)
		{
			if (!isMachine && ((fileMap == null && exePath == null) || (fileMap != null && ((ExeConfigurationFileMap)fileMap).ExeConfigFilename == null)) && ConfigurationManager.s_configSystem != null && ConfigurationManager.s_configSystem.GetType() != typeof(ClientConfigurationSystem))
			{
				throw new ArgumentException(SR.GetString("Config_configmanager_open_noexe"));
			}
			Configuration configuration = ClientConfigurationHost.OpenExeConfiguration(fileMap, isMachine, userLevel, exePath);
			if (preLoad)
			{
				ConfigurationManager.PreloadConfiguration(configuration);
			}
			return configuration;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000106EC File Offset: 0x0000E8EC
		private static void PreloadConfiguration(Configuration configuration)
		{
			if (configuration == null)
			{
				return;
			}
			foreach (object obj in configuration.Sections)
			{
				ConfigurationSection configurationSection = (ConfigurationSection)obj;
			}
			foreach (object obj2 in configuration.SectionGroups)
			{
				ConfigurationSectionGroup sectionGroup = (ConfigurationSectionGroup)obj2;
				ConfigurationManager.PreloadConfigurationSectionGroup(sectionGroup);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0001078C File Offset: 0x0000E98C
		private static void PreloadConfigurationSectionGroup(ConfigurationSectionGroup sectionGroup)
		{
			if (sectionGroup == null)
			{
				return;
			}
			foreach (object obj in sectionGroup.Sections)
			{
				ConfigurationSection configurationSection = (ConfigurationSection)obj;
			}
			foreach (object obj2 in sectionGroup.SectionGroups)
			{
				ConfigurationSectionGroup sectionGroup2 = (ConfigurationSectionGroup)obj2;
				ConfigurationManager.PreloadConfigurationSectionGroup(sectionGroup2);
			}
		}

		// Token: 0x040001DD RID: 477
		private static volatile IInternalConfigSystem s_configSystem;

		// Token: 0x040001DE RID: 478
		private static volatile ConfigurationManager.InitState s_initState = ConfigurationManager.InitState.NotStarted;

		// Token: 0x040001DF RID: 479
		private static object s_initLock = new object();

		// Token: 0x040001E0 RID: 480
		private static volatile Exception s_initError;

		// Token: 0x020000CE RID: 206
		private enum InitState
		{
			// Token: 0x04000492 RID: 1170
			NotStarted,
			// Token: 0x04000493 RID: 1171
			Started,
			// Token: 0x04000494 RID: 1172
			Usable,
			// Token: 0x04000495 RID: 1173
			Completed
		}
	}
}

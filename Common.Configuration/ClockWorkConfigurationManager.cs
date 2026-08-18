using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using ClockWorkLogger;
using TechnoPro.Common.Configuration.Sections;

namespace TechnoPro.Common.Configuration
{
	// Token: 0x02000002 RID: 2
	public static class ClockWorkConfigurationManager
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool IsWebApplication
		{
			get
			{
				return !string.IsNullOrEmpty(HttpRuntime.AppDomainAppVirtualPath);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		public static ConnectionStringsSection ConnectionStringsSectionUsingProtection
		{
			get
			{
				Configuration configuration = ClockWorkConfigurationManager.GetConfiguration();
				ConnectionStringsSection connectionStringsSection = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
				bool flag = connectionStringsSection != null;
				if (flag)
				{
					bool isProtected = connectionStringsSection.SectionInformation.IsProtected;
					if (isProtected)
					{
						connectionStringsSection.SectionInformation.UnprotectSection();
					}
				}
				return connectionStringsSection;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020B4 File Offset: 0x000002B4
		public static AppSettingsSection AppSettingsSectionUsingProtection
		{
			get
			{
				Configuration configuration = ClockWorkConfigurationManager.GetConfiguration();
				AppSettingsSection appSettingsSection = configuration.GetSection("appSettings") as AppSettingsSection;
				bool flag = appSettingsSection != null;
				if (flag)
				{
					bool isProtected = appSettingsSection.SectionInformation.IsProtected;
					if (isProtected)
					{
						appSettingsSection.SectionInformation.UnprotectSection();
					}
				}
				return appSettingsSection;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002108 File Offset: 0x00000308
		public static string GetConnectionStringByNameUsingProtection(string name)
		{
			string result;
			try
			{
				Configuration configuration = ClockWorkConfigurationManager.GetConfiguration();
				result = configuration.GetConnectionStringByNameUsingProtection(name);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ConfigurationManager::GetConnectionStringByNameUsingProtection: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000215C File Offset: 0x0000035C
		public static string GetConnectionStringByNameUsingProtection(this Configuration config, string name)
		{
			string result;
			try
			{
				ConnectionStringsSection connectionStringsSection = config.GetSection("connectionStrings") as ConnectionStringsSection;
				bool flag = connectionStringsSection != null;
				if (flag)
				{
					bool isProtected = connectionStringsSection.SectionInformation.IsProtected;
					if (isProtected)
					{
						connectionStringsSection.SectionInformation.UnprotectSection();
					}
					ConnectionStringSettings connectionStringSettings = connectionStringsSection.ConnectionStrings[name];
					bool flag2 = connectionStringSettings != null;
					if (flag2)
					{
						return connectionStringSettings.ConnectionString;
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000021E0 File Offset: 0x000003E0
		public static string GetAppSettingsByNameUsingProtection(string name)
		{
			string result;
			try
			{
				Configuration configuration = ClockWorkConfigurationManager.GetConfiguration();
				result = configuration.GetAppSettingsByNameUsingProtection(name);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002218 File Offset: 0x00000418
		public static void SetCustomSettingsByName(this Configuration config, string name, string value)
		{
			try
			{
				CustomSettingsSection customSettingsSection = config.GetSection("customSettings") as CustomSettingsSection;
				bool flag = customSettingsSection != null;
				if (flag)
				{
					bool isProtected = customSettingsSection.SectionInformation.IsProtected;
					if (isProtected)
					{
						customSettingsSection.SectionInformation.UnprotectSection();
					}
					bool flag2 = customSettingsSection.Settings.AllKeys.Contains(name);
					if (flag2)
					{
						customSettingsSection.Settings[name].Value = value;
					}
					else
					{
						customSettingsSection.Settings.Add(name, value);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022B0 File Offset: 0x000004B0
		public static void SetAppSettingsByName(this Configuration config, string name, string value)
		{
			try
			{
				AppSettingsSection appSettingsSection = config.GetSection("appSettings") as AppSettingsSection;
				bool flag = appSettingsSection != null;
				if (flag)
				{
					bool isProtected = appSettingsSection.SectionInformation.IsProtected;
					if (isProtected)
					{
						appSettingsSection.SectionInformation.UnprotectSection();
					}
					bool flag2 = appSettingsSection.Settings.AllKeys.Contains(name);
					if (flag2)
					{
						appSettingsSection.Settings[name].Value = value;
					}
					else
					{
						appSettingsSection.Settings.Add(name, value);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002348 File Offset: 0x00000548
		public static string GetAppSettingsByNameUsingProtection(this Configuration config, string name)
		{
			string result;
			try
			{
				string text = ClockWorkConfigurationManager.TryGettingCustomSettings(config, name);
				bool flag = text != null;
				if (flag)
				{
					result = text;
				}
				else
				{
					AppSettingsSection appSettingsSection = config.GetSection("appSettings") as AppSettingsSection;
					bool flag2 = appSettingsSection != null;
					if (flag2)
					{
						bool isProtected = appSettingsSection.SectionInformation.IsProtected;
						if (isProtected)
						{
							appSettingsSection.SectionInformation.UnprotectSection();
						}
						KeyValueConfigurationElement keyValueConfigurationElement = appSettingsSection.Settings[name];
						bool flag3 = keyValueConfigurationElement != null;
						if (flag3)
						{
							return keyValueConfigurationElement.Value;
						}
					}
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023E4 File Offset: 0x000005E4
		public static string GetConnectionStringByName(string name)
		{
			string result;
			try
			{
				string text = null;
				ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[name];
				bool flag = connectionStringSettings != null;
				if (flag)
				{
					text = connectionStringSettings.ConnectionString;
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000242C File Offset: 0x0000062C
		public static void ProtectSection(this Configuration config, string sectionName)
		{
			ConfigurationSection section = config.GetSection(sectionName);
			bool flag = !section.SectionInformation.IsProtected;
			if (flag)
			{
				section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
			}
			section.SectionInformation.ForceSave = true;
			config.Save();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000247C File Offset: 0x0000067C
		public static void ProtectSection(string sectionName)
		{
			Configuration configuration = ClockWorkConfigurationManager.GetConfiguration();
			configuration.ProtectSection(sectionName);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002498 File Offset: 0x00000698
		public static void UnProtectSection(this Configuration config, string sectionName)
		{
			ConfigurationSection section = config.GetSection(sectionName);
			bool isProtected = section.SectionInformation.IsProtected;
			if (isProtected)
			{
				section.SectionInformation.UnprotectSection();
			}
			section.SectionInformation.ForceSave = true;
			config.Save();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000024E0 File Offset: 0x000006E0
		public static void UnProtectSection(string sectionName)
		{
			Configuration configuration = ClockWorkConfigurationManager.GetConfiguration();
			configuration.UnProtectSection(sectionName);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000024FC File Offset: 0x000006FC
		private static string TryGettingCustomSettings(Configuration config, string name)
		{
			string result;
			try
			{
				CustomSettingsSection customSettingsSection = config.GetSection("customSettings") as CustomSettingsSection;
				bool flag = customSettingsSection != null;
				if (flag)
				{
					bool isProtected = customSettingsSection.SectionInformation.IsProtected;
					if (isProtected)
					{
						customSettingsSection.SectionInformation.UnprotectSection();
					}
					KeyValueConfigurationElement keyValueConfigurationElement = customSettingsSection.Settings[name];
					bool flag2 = keyValueConfigurationElement != null;
					if (flag2)
					{
						return keyValueConfigurationElement.Value;
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002580 File Offset: 0x00000780
		private static Configuration GetConfiguration()
		{
			bool isWebApplication = ClockWorkConfigurationManager.IsWebApplication;
			Configuration result;
			if (isWebApplication)
			{
				result = WebConfigurationManager.OpenWebConfiguration("~");
			}
			else
			{
				result = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		public const string ConnectionStringsSectionName = "connectionStrings";

		// Token: 0x04000002 RID: 2
		public const string AppSettingsSectionName = "appSettings";

		// Token: 0x04000003 RID: 3
		public const string CustomSettingsSectionName = "customSettings";
	}
}

using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.Versioning;

namespace System.Windows.Forms
{
	// Token: 0x02000110 RID: 272
	internal static class ConfigurationOptions
	{
		// Token: 0x0600073C RID: 1852 RVA: 0x00014CDC File Offset: 0x00012EDC
		static ConfigurationOptions()
		{
			ConfigurationOptions.PopulateWinformsSection();
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x00014D28 File Offset: 0x00012F28
		private static void PopulateWinformsSection()
		{
			if (ConfigurationOptions.NetFrameworkVersion.CompareTo(ConfigurationOptions.featureSupportedMinimumFrameworkVersion) >= 0)
			{
				try
				{
					ConfigurationOptions.applicationConfigOptions = (ConfigurationManager.GetSection("System.Windows.Forms.ApplicationConfigurationSection") as NameValueCollection);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00014D70 File Offset: 0x00012F70
		public static Version NetFrameworkVersion
		{
			get
			{
				if (ConfigurationOptions.netFrameworkVersion == null)
				{
					ConfigurationOptions.netFrameworkVersion = new Version(0, 0, 0, 0);
					try
					{
						string targetFrameworkName = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName;
						if (!string.IsNullOrEmpty(targetFrameworkName))
						{
							FrameworkName frameworkName = new FrameworkName(targetFrameworkName);
							if (string.Equals(frameworkName.Identifier, ".NETFramework"))
							{
								ConfigurationOptions.netFrameworkVersion = frameworkName.Version;
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
				return ConfigurationOptions.netFrameworkVersion;
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00014DF0 File Offset: 0x00012FF0
		public static string GetConfigSettingValue(string settingName)
		{
			if (ConfigurationOptions.applicationConfigOptions != null && !string.IsNullOrEmpty(settingName))
			{
				return ConfigurationOptions.applicationConfigOptions.Get(settingName);
			}
			return null;
		}

		// Token: 0x040004EA RID: 1258
		private static NameValueCollection applicationConfigOptions = null;

		// Token: 0x040004EB RID: 1259
		private static Version netFrameworkVersion = null;

		// Token: 0x040004EC RID: 1260
		private static readonly Version featureSupportedMinimumFrameworkVersion = new Version(4, 7);

		// Token: 0x040004ED RID: 1261
		internal static Version OSVersion = Environment.OSVersion.Version;

		// Token: 0x040004EE RID: 1262
		internal static readonly Version RS2Version = new Version(10, 0, 14933, 0);
	}
}

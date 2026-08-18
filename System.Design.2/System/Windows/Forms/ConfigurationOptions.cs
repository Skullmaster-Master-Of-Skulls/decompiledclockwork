using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Runtime.Versioning;

namespace System.Windows.Forms
{
	// Token: 0x02000289 RID: 649
	internal static class ConfigurationOptions
	{
		// Token: 0x060018BE RID: 6334 RVA: 0x0008B36C File Offset: 0x0008956C
		static ConfigurationOptions()
		{
			ConfigurationOptions.PopulateWinformsSection();
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0008B3B8 File Offset: 0x000895B8
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

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060018C0 RID: 6336 RVA: 0x0008B400 File Offset: 0x00089600
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

		// Token: 0x060018C1 RID: 6337 RVA: 0x0008B480 File Offset: 0x00089680
		public static string GetConfigSettingValue(string settingName)
		{
			if (ConfigurationOptions.applicationConfigOptions != null && !string.IsNullOrEmpty(settingName))
			{
				return ConfigurationOptions.applicationConfigOptions.Get(settingName);
			}
			return null;
		}

		// Token: 0x04001531 RID: 5425
		private static NameValueCollection applicationConfigOptions = null;

		// Token: 0x04001532 RID: 5426
		private static Version netFrameworkVersion = null;

		// Token: 0x04001533 RID: 5427
		private static readonly Version featureSupportedMinimumFrameworkVersion = new Version(4, 7);

		// Token: 0x04001534 RID: 5428
		internal static Version OSVersion = Environment.OSVersion.Version;

		// Token: 0x04001535 RID: 5429
		internal static readonly Version RS2Version = new Version(10, 0, 14933, 0);
	}
}

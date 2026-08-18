using System;

namespace System.Configuration.Internal
{
	// Token: 0x020000AA RID: 170
	internal sealed class ConfigurationManagerInternal : IConfigurationManagerInternal
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x000115BE File Offset: 0x0000F7BE
		private ConfigurationManagerInternal()
		{
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0001F609 File Offset: 0x0001D809
		bool IConfigurationManagerInternal.SupportsUserConfig
		{
			get
			{
				return ConfigurationManager.SupportsUserConfig;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0001F610 File Offset: 0x0001D810
		bool IConfigurationManagerInternal.SetConfigurationSystemInProgress
		{
			get
			{
				return ConfigurationManager.SetConfigurationSystemInProgress;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0001F617 File Offset: 0x0001D817
		string IConfigurationManagerInternal.MachineConfigPath
		{
			get
			{
				return ClientConfigurationHost.MachineConfigFilePath;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0001F61E File Offset: 0x0001D81E
		string IConfigurationManagerInternal.ApplicationConfigUri
		{
			get
			{
				return ClientConfigPaths.Current.ApplicationConfigUri;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0001F62A File Offset: 0x0001D82A
		string IConfigurationManagerInternal.ExeProductName
		{
			get
			{
				return ClientConfigPaths.Current.ProductName;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001F636 File Offset: 0x0001D836
		string IConfigurationManagerInternal.ExeProductVersion
		{
			get
			{
				return ClientConfigPaths.Current.ProductVersion;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0001F642 File Offset: 0x0001D842
		string IConfigurationManagerInternal.ExeRoamingConfigDirectory
		{
			get
			{
				return ClientConfigPaths.Current.RoamingConfigDirectory;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001F64E File Offset: 0x0001D84E
		string IConfigurationManagerInternal.ExeRoamingConfigPath
		{
			get
			{
				return ClientConfigPaths.Current.RoamingConfigFilename;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0001F65A File Offset: 0x0001D85A
		string IConfigurationManagerInternal.ExeLocalConfigDirectory
		{
			get
			{
				return ClientConfigPaths.Current.LocalConfigDirectory;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001F666 File Offset: 0x0001D866
		string IConfigurationManagerInternal.ExeLocalConfigPath
		{
			get
			{
				return ClientConfigPaths.Current.LocalConfigFilename;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001F672 File Offset: 0x0001D872
		string IConfigurationManagerInternal.UserConfigFilename
		{
			get
			{
				return "user.config";
			}
		}
	}
}

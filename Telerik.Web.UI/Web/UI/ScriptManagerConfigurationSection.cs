using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x02000E7E RID: 3710
	public class ScriptManagerConfigurationSection : ConfigurationSection
	{
		// Token: 0x17002C6B RID: 11371
		// (get) Token: 0x06008CA1 RID: 36001 RVA: 0x001FEA1E File Offset: 0x001FCC1E
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}

		// Token: 0x17002C6C RID: 11372
		// (get) Token: 0x06008CA2 RID: 36002 RVA: 0x001FEA30 File Offset: 0x001FCC30
		[ConfigurationProperty("whiteList")]
		public AssemblyListConfigurationElement WhiteList
		{
			get
			{
				return (AssemblyListConfigurationElement)base["whiteList"];
			}
		}

		// Token: 0x17002C6D RID: 11373
		// (get) Token: 0x06008CA3 RID: 36003 RVA: 0x001FEA42 File Offset: 0x001FCC42
		[ConfigurationProperty("enableAssemblyWhiteList", DefaultValue = false)]
		public bool EnableAssemblyWhiteList
		{
			get
			{
				return (bool)base["enableAssemblyWhiteList"];
			}
		}

		// Token: 0x17002C6E RID: 11374
		// (get) Token: 0x06008CA4 RID: 36004 RVA: 0x001FEA54 File Offset: 0x001FCC54
		// (set) Token: 0x06008CA5 RID: 36005 RVA: 0x001FEA66 File Offset: 0x001FCC66
		[ConfigurationProperty("defaultCacheProvider", DefaultValue = "AppDataCacheProvider")]
		public string DefaultCacheProvider
		{
			get
			{
				return (string)base["defaultCacheProvider"];
			}
			set
			{
				base["defaultCacheProvider"] = value;
			}
		}
	}
}

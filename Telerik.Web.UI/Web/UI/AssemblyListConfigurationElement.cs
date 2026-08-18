using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x0200085A RID: 2138
	public class AssemblyListConfigurationElement : ConfigurationElement
	{
		// Token: 0x170019C1 RID: 6593
		// (get) Token: 0x06004ECA RID: 20170 RVA: 0x000F7175 File Offset: 0x000F5375
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection AssemblyProviders
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}

		// Token: 0x170019C2 RID: 6594
		// (get) Token: 0x06004ECB RID: 20171 RVA: 0x000F7187 File Offset: 0x000F5387
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultAssemblyProvider", DefaultValue = "AppDataAssemblyProvider")]
		public string DefaultAssemblyProvider
		{
			get
			{
				return (string)base["defaultAssemblyProvider"];
			}
		}
	}
}

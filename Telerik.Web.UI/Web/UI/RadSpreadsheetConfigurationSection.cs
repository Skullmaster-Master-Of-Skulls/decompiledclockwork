using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x0200089A RID: 2202
	public class RadSpreadsheetConfigurationSection : ConfigurationSection
	{
		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x060051DA RID: 20954 RVA: 0x000FF374 File Offset: 0x000FD574
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x060051DB RID: 20955 RVA: 0x000FF386 File Offset: 0x000FD586
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("defaultProvider", DefaultValue = "Integrated")]
		public string DefaulTaskProvider
		{
			get
			{
				return (string)base["defaultProvider"];
			}
		}
	}
}

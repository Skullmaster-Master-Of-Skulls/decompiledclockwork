using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x020002FC RID: 764
	public class RadGanttConfigurationSection : ConfigurationSection
	{
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x000551C3 File Offset: 0x000533C3
		[ConfigurationProperty("taskProviders")]
		public ProviderSettingsCollection TaskProviders
		{
			get
			{
				return (ProviderSettingsCollection)base["taskProviders"];
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x000551D5 File Offset: 0x000533D5
		[ConfigurationProperty("defaultTaskProvider", DefaultValue = "Integrated")]
		[StringValidator(MinLength = 1)]
		public string DefaulTaskProvider
		{
			get
			{
				return (string)base["defaultTaskProvider"];
			}
		}
	}
}

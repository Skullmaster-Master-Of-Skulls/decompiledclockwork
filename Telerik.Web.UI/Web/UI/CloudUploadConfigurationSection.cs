using System;
using System.Configuration;

namespace Telerik.Web.UI
{
	// Token: 0x020001B7 RID: 439
	internal class CloudUploadConfigurationSection : ConfigurationSection
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x0003B388 File Offset: 0x00039588
		[ConfigurationProperty("storageProviders")]
		public ProviderSettingsCollection StorageProviders
		{
			get
			{
				return (ProviderSettingsCollection)base["storageProviders"];
			}
		}
	}
}

using System;
using System.Configuration;

namespace System.Data.SqlClient
{
	// Token: 0x020001DD RID: 477
	internal class SqlAuthenticationProviderConfigurationSection : ConfigurationSection
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x000D38FC File Offset: 0x000D2CFC
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x000D391C File Offset: 0x000D2D1C
		[ConfigurationProperty("initializerType")]
		public string InitializerType
		{
			get
			{
				return base["initializerType"] as string;
			}
		}

		// Token: 0x0400112A RID: 4394
		public const string Name = "SqlAuthenticationProviders";
	}
}

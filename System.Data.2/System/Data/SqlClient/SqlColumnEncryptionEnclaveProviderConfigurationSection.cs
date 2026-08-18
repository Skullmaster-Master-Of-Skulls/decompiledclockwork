using System;
using System.Configuration;

namespace System.Data.SqlClient
{
	// Token: 0x0200023C RID: 572
	internal class SqlColumnEncryptionEnclaveProviderConfigurationSection : ConfigurationSection
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x000F3A14 File Offset: 0x000F2E14
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base["providers"];
			}
		}
	}
}

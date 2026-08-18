using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.Data.SqlClient
{
	// Token: 0x0200023D RID: 573
	internal class SqlColumnEncryptionEnclaveProviderConfigurationManager
	{
		// Token: 0x06002342 RID: 9026 RVA: 0x000F3A48 File Offset: 0x000F2E48
		public SqlColumnEncryptionEnclaveProviderConfigurationManager(SqlColumnEncryptionEnclaveProviderConfigurationSection configSection)
		{
			if (configSection != null && configSection.Providers != null && configSection.Providers.Count > 0)
			{
				foreach (object obj in configSection.Providers)
				{
					ProviderSettings providerSettings = (ProviderSettings)obj;
					string text = providerSettings.Name.ToLowerInvariant();
					SqlColumnEncryptionEnclaveProvider value;
					try
					{
						Type type = Type.GetType(providerSettings.Type, true);
						value = (SqlColumnEncryptionEnclaveProvider)Activator.CreateInstance(type);
					}
					catch (Exception innerException)
					{
						throw SQL.CannotCreateSqlColumnEncryptionEnclaveProvider(text, providerSettings.Type, innerException);
					}
					this._enclaveProviders[text] = value;
				}
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x000F3B3C File Offset: 0x000F2F3C
		public SqlColumnEncryptionEnclaveProvider GetSqlColumnEncryptionEnclaveProvider(string SqlColumnEncryptionEnclaveProviderName)
		{
			if (string.IsNullOrEmpty(SqlColumnEncryptionEnclaveProviderName))
			{
				throw SQL.SqlColumnEncryptionEnclaveProviderNameCannotBeEmpty();
			}
			SqlColumnEncryptionEnclaveProviderName = SqlColumnEncryptionEnclaveProviderName.ToLowerInvariant();
			SqlColumnEncryptionEnclaveProvider result = null;
			this._enclaveProviders.TryGetValue(SqlColumnEncryptionEnclaveProviderName, out result);
			return result;
		}

		// Token: 0x0400155F RID: 5471
		private readonly Dictionary<string, SqlColumnEncryptionEnclaveProvider> _enclaveProviders = new Dictionary<string, SqlColumnEncryptionEnclaveProvider>();
	}
}

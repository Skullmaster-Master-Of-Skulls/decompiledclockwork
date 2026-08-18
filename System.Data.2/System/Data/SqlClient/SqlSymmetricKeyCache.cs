using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x0200018C RID: 396
	internal sealed class SqlSymmetricKeyCache
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x000A94A4 File Offset: 0x000A88A4
		private SqlSymmetricKeyCache()
		{
			this._cache = new MemoryCache("ColumnEncryptionKeyCache", null);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x000A94C8 File Offset: 0x000A88C8
		internal static SqlSymmetricKeyCache GetInstance()
		{
			return SqlSymmetricKeyCache._singletonInstance;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000A94DC File Offset: 0x000A88DC
		internal bool GetKey(SqlEncryptionKeyInfo keyInfo, string serverName, out SqlClientSymmetricKey encryptionKey)
		{
			StringBuilder stringBuilder = new StringBuilder(serverName, serverName.Length + SqlSecurityUtility.GetBase64LengthFromByteLength(keyInfo.encryptedKey.Length) + keyInfo.keyStoreName.Length + 2);
			stringBuilder.Append(":");
			stringBuilder.Append(Convert.ToBase64String(keyInfo.encryptedKey));
			stringBuilder.Append(":");
			stringBuilder.Append(keyInfo.keyStoreName);
			string key = stringBuilder.ToString();
			encryptionKey = (this._cache.Get(key, null) as SqlClientSymmetricKey);
			if (encryptionKey == null)
			{
				IList<string> list;
				if (SqlConnection.ColumnEncryptionTrustedMasterKeyPaths.TryGetValue(serverName, out list) && (list == null || list.Count<string>() == 0 || !list.Any((string s) => s.Equals(keyInfo.keyPath, StringComparison.InvariantCultureIgnoreCase))))
				{
					throw SQL.UntrustedKeyPath(keyInfo.keyPath, serverName);
				}
				SqlColumnEncryptionKeyStoreProvider sqlColumnEncryptionKeyStoreProvider;
				if (!SqlConnection.TryGetColumnEncryptionKeyStoreProvider(keyInfo.keyStoreName, out sqlColumnEncryptionKeyStoreProvider))
				{
					throw SQL.UnrecognizedKeyStoreProviderName(keyInfo.keyStoreName, SqlConnection.GetColumnEncryptionSystemKeyStoreProviders(), SqlConnection.GetColumnEncryptionCustomKeyStoreProviders());
				}
				byte[] rootKey;
				try
				{
					rootKey = sqlColumnEncryptionKeyStoreProvider.DecryptColumnEncryptionKey(keyInfo.keyPath, keyInfo.algorithmName, keyInfo.encryptedKey);
				}
				catch (Exception e)
				{
					string bytesAsString = SqlSecurityUtility.GetBytesAsString(keyInfo.encryptedKey, true, 10);
					throw SQL.KeyDecryptionFailed(keyInfo.keyStoreName, bytesAsString, e);
				}
				encryptionKey = new SqlClientSymmetricKey(rootKey);
				if (SqlConnection.ColumnEncryptionKeyCacheTtl != TimeSpan.Zero)
				{
					DateTimeOffset absoluteExpiration = DateTimeOffset.UtcNow.Add(SqlConnection.ColumnEncryptionKeyCacheTtl);
					this._cache.Add(key, encryptionKey, absoluteExpiration, null);
				}
			}
			return true;
		}

		// Token: 0x04000E5D RID: 3677
		private readonly MemoryCache _cache;

		// Token: 0x04000E5E RID: 3678
		private static readonly SqlSymmetricKeyCache _singletonInstance = new SqlSymmetricKeyCache();
	}
}

using System;
using System.Collections.Concurrent;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000193 RID: 403
	internal class SqlAeadAes256CbcHmac256Factory : SqlClientEncryptionAlgorithmFactory
	{
		// Token: 0x0600180B RID: 6155 RVA: 0x000AB0E8 File Offset: 0x000AA4E8
		internal override SqlClientEncryptionAlgorithm Create(SqlClientSymmetricKey encryptionKey, SqlClientEncryptionType encryptionType, string encryptionAlgorithm)
		{
			if (encryptionType != SqlClientEncryptionType.Deterministic && encryptionType != SqlClientEncryptionType.Randomized)
			{
				throw SQL.InvalidEncryptionType("AEAD_AES_256_CBC_HMAC_SHA256", encryptionType, new SqlClientEncryptionType[]
				{
					SqlClientEncryptionType.Deterministic,
					SqlClientEncryptionType.Randomized
				});
			}
			StringBuilder stringBuilder = new StringBuilder(Convert.ToBase64String(encryptionKey.RootKey), SqlSecurityUtility.GetBase64LengthFromByteLength(encryptionKey.RootKey.Length) + 4);
			stringBuilder.Append(":");
			stringBuilder.Append((int)encryptionType);
			stringBuilder.Append(":");
			stringBuilder.Append(1);
			string key = stringBuilder.ToString();
			SqlAeadAes256CbcHmac256Algorithm sqlAeadAes256CbcHmac256Algorithm;
			if (!this._encryptionAlgorithms.TryGetValue(key, out sqlAeadAes256CbcHmac256Algorithm))
			{
				SqlAeadAes256CbcHmac256EncryptionKey encryptionKey2 = new SqlAeadAes256CbcHmac256EncryptionKey(encryptionKey.RootKey, "AEAD_AES_256_CBC_HMAC_SHA256");
				sqlAeadAes256CbcHmac256Algorithm = new SqlAeadAes256CbcHmac256Algorithm(encryptionKey2, encryptionType, 1);
				this._encryptionAlgorithms.TryAdd(key, sqlAeadAes256CbcHmac256Algorithm);
			}
			return sqlAeadAes256CbcHmac256Algorithm;
		}

		// Token: 0x04000E87 RID: 3719
		private readonly ConcurrentDictionary<string, SqlAeadAes256CbcHmac256Algorithm> _encryptionAlgorithms = new ConcurrentDictionary<string, SqlAeadAes256CbcHmac256Algorithm>(4 * Environment.ProcessorCount, 2);
	}
}

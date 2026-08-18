using System;
using System.Collections.Concurrent;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x02000196 RID: 406
	internal class SqlAes256CbcFactory : SqlAeadAes256CbcHmac256Factory
	{
		// Token: 0x06001814 RID: 6164 RVA: 0x000AB364 File Offset: 0x000AA764
		internal override SqlClientEncryptionAlgorithm Create(SqlClientSymmetricKey encryptionKey, SqlClientEncryptionType encryptionType, string encryptionAlgorithm)
		{
			if (encryptionType != SqlClientEncryptionType.Deterministic && encryptionType != SqlClientEncryptionType.Randomized)
			{
				throw SQL.InvalidEncryptionType("AES_256_CBC", encryptionType, new SqlClientEncryptionType[]
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
			SqlAes256CbcAlgorithm sqlAes256CbcAlgorithm;
			if (!this._encryptionAlgorithms.TryGetValue(key, out sqlAes256CbcAlgorithm))
			{
				SqlAeadAes256CbcHmac256EncryptionKey encryptionKey2 = new SqlAeadAes256CbcHmac256EncryptionKey(encryptionKey.RootKey, "AES_256_CBC");
				sqlAes256CbcAlgorithm = new SqlAes256CbcAlgorithm(encryptionKey2, encryptionType, 1);
				this._encryptionAlgorithms.TryAdd(key, sqlAes256CbcAlgorithm);
			}
			return sqlAes256CbcAlgorithm;
		}

		// Token: 0x04000E91 RID: 3729
		private readonly ConcurrentDictionary<string, SqlAes256CbcAlgorithm> _encryptionAlgorithms = new ConcurrentDictionary<string, SqlAes256CbcAlgorithm>(4 * Environment.ProcessorCount, 2);
	}
}

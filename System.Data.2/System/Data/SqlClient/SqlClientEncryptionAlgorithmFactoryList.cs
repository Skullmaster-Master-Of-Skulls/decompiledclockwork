using System;
using System.Collections.Concurrent;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x0200018B RID: 395
	internal sealed class SqlClientEncryptionAlgorithmFactoryList
	{
		// Token: 0x060017BE RID: 6078 RVA: 0x000A934C File Offset: 0x000A874C
		private SqlClientEncryptionAlgorithmFactoryList()
		{
			this._encryptionAlgoFactoryList = new ConcurrentDictionary<string, SqlClientEncryptionAlgorithmFactory>(4 * Environment.ProcessorCount, 2);
			this._encryptionAlgoFactoryList.TryAdd("AEAD_AES_256_CBC_HMAC_SHA256", new SqlAeadAes256CbcHmac256Factory());
			this._encryptionAlgoFactoryList.TryAdd("AES_256_CBC", new SqlAes256CbcFactory());
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000A93A0 File Offset: 0x000A87A0
		internal static SqlClientEncryptionAlgorithmFactoryList GetInstance()
		{
			return SqlClientEncryptionAlgorithmFactoryList._singletonInstance;
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000A93B4 File Offset: 0x000A87B4
		internal string GetRegisteredCipherAlgorithmNames()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string value in this._encryptionAlgoFactoryList.Keys)
			{
				if (flag)
				{
					stringBuilder.Append("'");
					flag = false;
				}
				else
				{
					stringBuilder.Append(", '");
				}
				stringBuilder.Append(value);
				stringBuilder.Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000A944C File Offset: 0x000A884C
		internal void GetAlgorithm(SqlClientSymmetricKey key, byte type, string algorithmName, out SqlClientEncryptionAlgorithm encryptionAlgorithm)
		{
			encryptionAlgorithm = null;
			SqlClientEncryptionAlgorithmFactory sqlClientEncryptionAlgorithmFactory = null;
			if (!this._encryptionAlgoFactoryList.TryGetValue(algorithmName, out sqlClientEncryptionAlgorithmFactory))
			{
				throw SQL.UnknownColumnEncryptionAlgorithm(algorithmName, SqlClientEncryptionAlgorithmFactoryList.GetInstance().GetRegisteredCipherAlgorithmNames());
			}
			encryptionAlgorithm = sqlClientEncryptionAlgorithmFactory.Create(key, (SqlClientEncryptionType)type, algorithmName);
		}

		// Token: 0x04000E5B RID: 3675
		private readonly ConcurrentDictionary<string, SqlClientEncryptionAlgorithmFactory> _encryptionAlgoFactoryList;

		// Token: 0x04000E5C RID: 3676
		private static readonly SqlClientEncryptionAlgorithmFactoryList _singletonInstance = new SqlClientEncryptionAlgorithmFactoryList();
	}
}

using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000195 RID: 405
	internal class SqlAes256CbcAlgorithm : SqlAeadAes256CbcHmac256Algorithm
	{
		// Token: 0x06001811 RID: 6161 RVA: 0x000AB31C File Offset: 0x000AA71C
		internal SqlAes256CbcAlgorithm(SqlAeadAes256CbcHmac256EncryptionKey encryptionKey, SqlClientEncryptionType encryptionType, byte algorithmVersion) : base(encryptionKey, encryptionType, algorithmVersion)
		{
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x000AB334 File Offset: 0x000AA734
		internal override byte[] EncryptData(byte[] plainText)
		{
			return base.EncryptData(plainText, false);
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x000AB34C File Offset: 0x000AA74C
		internal override byte[] DecryptData(byte[] cipherText)
		{
			return base.DecryptData(cipherText, false);
		}

		// Token: 0x04000E90 RID: 3728
		internal new const string AlgorithmName = "AES_256_CBC";
	}
}

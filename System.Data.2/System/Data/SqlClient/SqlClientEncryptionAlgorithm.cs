using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000197 RID: 407
	internal abstract class SqlClientEncryptionAlgorithm
	{
		// Token: 0x06001816 RID: 6166
		internal abstract byte[] EncryptData(byte[] plainText);

		// Token: 0x06001817 RID: 6167
		internal abstract byte[] DecryptData(byte[] cipherText);
	}
}

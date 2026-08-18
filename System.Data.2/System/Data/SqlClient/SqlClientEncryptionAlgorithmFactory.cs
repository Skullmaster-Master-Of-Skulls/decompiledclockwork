using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000198 RID: 408
	internal abstract class SqlClientEncryptionAlgorithmFactory
	{
		// Token: 0x06001819 RID: 6169
		internal abstract SqlClientEncryptionAlgorithm Create(SqlClientSymmetricKey encryptionKey, SqlClientEncryptionType encryptionType, string encryptionAlgorithm);
	}
}

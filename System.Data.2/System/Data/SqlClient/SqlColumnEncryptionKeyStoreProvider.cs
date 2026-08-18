using System;

namespace System.Data.SqlClient
{
	// Token: 0x0200018D RID: 397
	public abstract class SqlColumnEncryptionKeyStoreProvider
	{
		// Token: 0x060017C7 RID: 6087
		public abstract byte[] DecryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] encryptedColumnEncryptionKey);

		// Token: 0x060017C8 RID: 6088
		public abstract byte[] EncryptColumnEncryptionKey(string masterKeyPath, string encryptionAlgorithm, byte[] columnEncryptionKey);

		// Token: 0x060017C9 RID: 6089 RVA: 0x000A96CC File Offset: 0x000A8ACC
		public virtual byte[] SignColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x000A96E0 File Offset: 0x000A8AE0
		public virtual bool VerifyColumnMasterKeyMetadata(string masterKeyPath, bool allowEnclaveComputations, byte[] signature)
		{
			throw new NotImplementedException();
		}
	}
}

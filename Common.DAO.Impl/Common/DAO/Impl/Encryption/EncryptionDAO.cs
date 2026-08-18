using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Encryption;

namespace TechnoPro.Common.DAO.Impl.Encryption
{
	// Token: 0x020000D0 RID: 208
	public class EncryptionDAO : IEncryptionDAO
	{
		// Token: 0x060005AE RID: 1454 RVA: 0x000362E2 File Offset: 0x000344E2
		public EncryptionDAO(IEncryption encryption)
		{
			this._encryption = encryption;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x000362F3 File Offset: 0x000344F3
		public void DecryptColumns(DataTable t, params string[] colNames)
		{
			this._encryption.DecryptColumns(t, colNames);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00036304 File Offset: 0x00034504
		public void EncryptColumns(DataTable t, params string[] colNames)
		{
			this._encryption.EncryptColumns(t, colNames);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00036318 File Offset: 0x00034518
		public IList<byte[]> EncryptData(IList<string> items)
		{
			IBatchEncryptor batchEncryptor = this._encryption.GetBatchEncryptor();
			return (from g in items
			where g != null && g.Trim().Length > 0
			select g).Select(new Func<string, byte[]>(batchEncryptor.Encrypt)).ToList<byte[]>();
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00036374 File Offset: 0x00034574
		public IList<string> DecryptData(IList<byte[]> items)
		{
			IBatchDecryptor batchDecryptor = this._encryption.GetBatchDecryptor();
			return (from g in items
			select (g == null) ? null : batchDecryptor.Decrypt(g)).ToList<string>();
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000363B4 File Offset: 0x000345B4
		public byte[] EncryptData(string item)
		{
			return this._encryption.Encrypt(item);
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000363D4 File Offset: 0x000345D4
		public string DecryptData(byte[] item)
		{
			return this._encryption.Decrypt(item);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000363F4 File Offset: 0x000345F4
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt)
		{
			return this._encryption.EncryptOrDecryptNameDataTableBatch(encrypt, t, colsToEncrypt);
		}

		// Token: 0x040002F0 RID: 752
		private readonly IEncryption _encryption;
	}
}

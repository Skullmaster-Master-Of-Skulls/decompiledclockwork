using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.DAO.Encryption
{
	// Token: 0x02000075 RID: 117
	public interface IEncryptionDAO
	{
		// Token: 0x060002E5 RID: 741
		void DecryptColumns(DataTable t, params string[] colNames);

		// Token: 0x060002E6 RID: 742
		void EncryptColumns(DataTable t, params string[] colNames);

		// Token: 0x060002E7 RID: 743
		IList<byte[]> EncryptData(IList<string> items);

		// Token: 0x060002E8 RID: 744
		IList<string> DecryptData(IList<byte[]> items);

		// Token: 0x060002E9 RID: 745
		byte[] EncryptData(string item);

		// Token: 0x060002EA RID: 746
		string DecryptData(byte[] item);

		// Token: 0x060002EB RID: 747
		DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt);
	}
}

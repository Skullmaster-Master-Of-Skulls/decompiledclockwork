using System;
using System.Data;
using TechnoPro.Common.Reports.Public;
using TechnoPro.Common.Reports.Public.Entities.OperationContexts;

namespace TechnoPro.Common.Reports.ICore.Common
{
	// Token: 0x02000004 RID: 4
	public interface IEncryptionReportManager : IOperationContextRO, IBaseOperationContextRO<OperationContextRO>
	{
		// Token: 0x0600000A RID: 10
		string DecryptData(byte[] data);

		// Token: 0x0600000B RID: 11
		byte[] EncryptData(string data);

		// Token: 0x0600000C RID: 12
		DataTable DecryptTable(DataTable t, params string[] ColumnsToDecrypt);

		// Token: 0x0600000D RID: 13
		DataTable EncryptTable(DataTable t, params string[] ColumnsToEncrypt);

		// Token: 0x0600000E RID: 14
		object GetBatchDecryptor();

		// Token: 0x0600000F RID: 15
		string BatchDecryptData(object batchDecryptor, byte[] data);

		// Token: 0x06000010 RID: 16
		object GetBatchEncryptor();

		// Token: 0x06000011 RID: 17
		byte[] BatchEncryptData(object batchEncryptor, string data);
	}
}

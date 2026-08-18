using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.ICore.Encryption
{
	// Token: 0x0200008F RID: 143
	public interface ILegacyEncryptionManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600040D RID: 1037
		byte[] Encrypt(string text);

		// Token: 0x0600040E RID: 1038
		string Decrypt(byte[] bytes);

		// Token: 0x0600040F RID: 1039
		IList<byte[]> EncryptData(IList<string> items);

		// Token: 0x06000410 RID: 1040
		IList<string> DecryptData(IList<byte[]> items);

		// Token: 0x06000411 RID: 1041
		DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt);

		// Token: 0x06000412 RID: 1042
		string EncodeUrlVariable(string varValue, bool encrypted);

		// Token: 0x06000413 RID: 1043
		IList<LegacyDynamicDataItemItemsThatHaveBeenDecrypted> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecrypted> itemsToBeDecrypted);
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Encryption
{
	// Token: 0x02000057 RID: 87
	public interface ILegacyEncryptionClientManager : IWebService
	{
		// Token: 0x06000296 RID: 662
		byte[] Encrypt(string text);

		// Token: 0x06000297 RID: 663
		string Decrypt(byte[] bytes);

		// Token: 0x06000298 RID: 664
		DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt);

		// Token: 0x06000299 RID: 665
		IList<byte[]> EncryptData(params string[] plainTextValues);

		// Token: 0x0600029A RID: 666
		IList<string> DecryptData(params byte[][] encryptedValues);

		// Token: 0x0600029B RID: 667
		string EncodeUrlVariable(string varValue, bool isEncrypted);

		// Token: 0x0600029C RID: 668
		IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecryptedDTO> itemsToBeDecrypted);
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.ClockWorkServer.Contracts.DTO.Encryption;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Encryption;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Encryption
{
	// Token: 0x0200004D RID: 77
	public class LegacyEncryptionRestClientManager : BearerTokenRestProxy<ILegacyEncryptionClientManager>, ILegacyEncryptionClientManager, IWebService
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00008D39 File Offset: 0x00006F39
		public LegacyEncryptionRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008D43 File Offset: 0x00006F43
		public LegacyEncryptionRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008D4E File Offset: 0x00006F4E
		public byte[] Encrypt(string text)
		{
			return base.Post<string, byte[]>(text, "legacyencryption/encrypt");
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00008D5C File Offset: 0x00006F5C
		public string Decrypt(byte[] bytes)
		{
			return base.Post<byte[], string>(bytes, "legacyencryption/decrypt");
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008D6C File Offset: 0x00006F6C
		public IList<byte[]> EncryptData(params string[] plainTextValues)
		{
			EncryptDataReq encryptDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EncryptDataReq>();
			encryptDataReq.PlainTextValues = (plainTextValues ?? new string[0]).ToList<string>();
			return base.Post<EncryptDataReq, IList<byte[]>>(encryptDataReq, "legacyencryption/encryptdata");
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008DA8 File Offset: 0x00006FA8
		public IList<string> DecryptData(params byte[][] encryptedValues)
		{
			DecryptDataReq decryptDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DecryptDataReq>();
			decryptDataReq.EncryptedValues = (encryptedValues ?? new byte[0][]).ToList<byte[]>();
			return base.Post<DecryptDataReq, IList<string>>(decryptDataReq, "legacyencryption/decryptdata");
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00008DE2 File Offset: 0x00006FE2
		public string EncodeUrlVariable(string varValue, bool isEncrypted)
		{
			return base.Get<string>(string.Format("legacyencryption/encodeurlvariable/variablevalue/{0}/isencrypted/{1}", varValue, isEncrypted), true);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00008DFC File Offset: 0x00006FFC
		public IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecryptedDTO> itemsToBeDecrypted)
		{
			DecryptLegacyDataItemsNeedingDecryptionReq decryptLegacyDataItemsNeedingDecryptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DecryptLegacyDataItemsNeedingDecryptionReq>();
			decryptLegacyDataItemsNeedingDecryptionReq.ItemsToDecrypt = itemsToBeDecrypted;
			return base.Post<DecryptLegacyDataItemsNeedingDecryptionReq, IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO>>(decryptLegacyDataItemsNeedingDecryptionReq, "legacyencryption/decryptlegacydataitemsneedingdecryption");
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00008E28 File Offset: 0x00007028
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncrypt)
		{
			EncryptOrDecryptNameDataTableBatchReq encryptOrDecryptNameDataTableBatchReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EncryptOrDecryptNameDataTableBatchReq>();
			encryptOrDecryptNameDataTableBatchReq.Encrypt = encrypt;
			encryptOrDecryptNameDataTableBatchReq.Table = t;
			encryptOrDecryptNameDataTableBatchReq.ColsToEncryptOrDecrypt = colsToEncrypt;
			return base.Post<EncryptOrDecryptNameDataTableBatchReq, DataTable>(encryptOrDecryptNameDataTableBatchReq, "legacyencryption/encryptordecryptnamedatatablebatch");
		}
	}
}

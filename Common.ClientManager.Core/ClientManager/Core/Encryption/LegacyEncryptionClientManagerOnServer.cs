using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Encryption;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Encryption;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Encryption
{
	// Token: 0x0200005D RID: 93
	public class LegacyEncryptionClientManagerOnServer : ILegacyEncryptionClientManager, IWebService
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000EB58 File Offset: 0x0000CD58
		public byte[] Encrypt(string text)
		{
			EncryptReq encryptReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EncryptReq>();
			encryptReq.Text = text;
			return ClientServiceFactory.GetClientInstance<ILegacyEncryption>().Encrypt(encryptReq).EncryptedBytes;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public string Decrypt(byte[] bytes)
		{
			bool flag = bytes == null || bytes.Length < 1;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				DecryptReq decryptReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DecryptReq>();
				decryptReq.EncryptedBytes = bytes;
				result = ClientServiceFactory.GetClientInstance<ILegacyEncryption>().Decrypt(decryptReq).Text;
			}
			return result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000EBE0 File Offset: 0x0000CDE0
		public DataTable EncryptOrDecryptNameDataTableBatch(bool encrypt, DataTable t, params string[] colsToEncryptOrDecrypt)
		{
			EncryptOrDecryptNameDataTableBatchReq encryptOrDecryptNameDataTableBatchReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EncryptOrDecryptNameDataTableBatchReq>();
			encryptOrDecryptNameDataTableBatchReq.Encrypt = encrypt;
			encryptOrDecryptNameDataTableBatchReq.Table = t;
			encryptOrDecryptNameDataTableBatchReq.ColsToEncryptOrDecrypt = colsToEncryptOrDecrypt;
			return ClientServiceFactory.GetClientInstance<ILegacyEncryption>().EncryptOrDecryptNameDataTableBatch(encryptOrDecryptNameDataTableBatchReq).Table;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000EC28 File Offset: 0x0000CE28
		public IList<byte[]> EncryptData(params string[] plainTextValues)
		{
			EncryptDataReq encryptDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EncryptDataReq>();
			encryptDataReq.PlainTextValues = (plainTextValues ?? new string[0]).ToList<string>();
			return ClientServiceFactory.GetClientInstance<ILegacyEncryption>().EncryptData(encryptDataReq).EncryptedValues;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public IList<string> DecryptData(params byte[][] encryptedValues)
		{
			DecryptDataReq decryptDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DecryptDataReq>();
			decryptDataReq.EncryptedValues = (encryptedValues ?? new byte[0][]).ToList<byte[]>();
			return ClientServiceFactory.GetClientInstance<ILegacyEncryption>().DecryptData(decryptDataReq).PlainTextValues;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		public string EncodeUrlVariable(string varValue, bool isEncrypted)
		{
			EncodeUrlVariableReq encodeUrlVariableReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<EncodeUrlVariableReq>();
			encodeUrlVariableReq.IsEncrypted = isEncrypted;
			encodeUrlVariableReq.VariableValue = varValue;
			return ClientServiceFactory.GetClientInstance<ILegacyEncryption>().EncodeUrlVariable(encodeUrlVariableReq).EncodedUrlVariable;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO> DecryptLegacyDataItemsNeedingDecryption(IList<LegacyDynamicDataItemItemsToBeDecryptedDTO> itemsToBeDecrypted)
		{
			DecryptLegacyDataItemsNeedingDecryptionReq decryptLegacyDataItemsNeedingDecryptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DecryptLegacyDataItemsNeedingDecryptionReq>();
			decryptLegacyDataItemsNeedingDecryptionReq.ItemsToDecrypt = itemsToBeDecrypted;
			return ClientServiceFactory.GetClientInstance<ILegacyEncryption>().DecryptLegacyDataItemsNeedingDecryption(decryptLegacyDataItemsNeedingDecryptionReq).DecryptedItems;
		}
	}
}

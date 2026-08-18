using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Encryption;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Core.Encryption;
using TechnoPro.Common.Core.Mappers.Legacy.DynamicData;
using TechnoPro.Common.ICore.Encryption;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000045 RID: 69
	public class LegacyEncryptionServiceManager : ILegacyEncryption, IService
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		public EncryptResp Encrypt(EncryptReq Request)
		{
			ILegacyEncryptionManager legacyEncryptionManager = new LegacyEncryptionManager(Request.GetOperationContext());
			byte[] encryptedBytes = legacyEncryptionManager.Encrypt(Request.Text);
			return new EncryptResp
			{
				EncryptedBytes = encryptedBytes
			};
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		public DecryptResp Decrypt(DecryptReq Request)
		{
			ILegacyEncryptionManager legacyEncryptionManager = new LegacyEncryptionManager(Request.GetOperationContext());
			string text = legacyEncryptionManager.Decrypt(Request.EncryptedBytes);
			return new DecryptResp
			{
				Text = text
			};
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000D524 File Offset: 0x0000B724
		public EncryptOrDecryptNameDataTableBatchResp EncryptOrDecryptNameDataTableBatch(EncryptOrDecryptNameDataTableBatchReq Request)
		{
			ILegacyEncryptionManager legacyEncryptionManager = new LegacyEncryptionManager(Request.GetOperationContext());
			DataTable table = legacyEncryptionManager.EncryptOrDecryptNameDataTableBatch(Request.Encrypt, Request.Table, Request.ColsToEncryptOrDecrypt);
			return new EncryptOrDecryptNameDataTableBatchResp
			{
				Table = table
			};
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000D568 File Offset: 0x0000B768
		public EncryptDataResp EncryptData(EncryptDataReq Request)
		{
			ILegacyEncryptionManager legacyEncryptionManager = new LegacyEncryptionManager(Request.GetOperationContext());
			IList<byte[]> encryptedValues = legacyEncryptionManager.EncryptData(Request.PlainTextValues);
			return new EncryptDataResp
			{
				EncryptedValues = encryptedValues
			};
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000D5A0 File Offset: 0x0000B7A0
		public DecryptDataResp DecryptData(DecryptDataReq Request)
		{
			ILegacyEncryptionManager legacyEncryptionManager = new LegacyEncryptionManager(Request.GetOperationContext());
			IList<string> plainTextValues = legacyEncryptionManager.DecryptData(Request.EncryptedValues);
			return new DecryptDataResp
			{
				PlainTextValues = plainTextValues
			};
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		public EncodeUrlVariableResp EncodeUrlVariable(EncodeUrlVariableReq Request)
		{
			ILegacyEncryptionManager legacyEncryptionManager = new LegacyEncryptionManager(Request.GetOperationContext());
			return new EncodeUrlVariableResp
			{
				EncodedUrlVariable = legacyEncryptionManager.EncodeUrlVariable(Request.VariableValue, Request.IsEncrypted)
			};
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000D614 File Offset: 0x0000B814
		public DecryptLegacyDataItemsNeedingDecryptionResp DecryptLegacyDataItemsNeedingDecryption(DecryptLegacyDataItemsNeedingDecryptionReq Request)
		{
			ILegacyEncryptionManager legacyEncryptionManager = new LegacyEncryptionManager(Request.GetOperationContext());
			IList<LegacyDynamicDataItemItemsThatHaveBeenDecrypted> list = legacyEncryptionManager.DecryptLegacyDataItemsNeedingDecryption((from g in Request.ItemsToDecrypt
			select g.ToDomainObject()).ToList<LegacyDynamicDataItemItemsToBeDecrypted>());
			DecryptLegacyDataItemsNeedingDecryptionResp decryptLegacyDataItemsNeedingDecryptionResp = new DecryptLegacyDataItemsNeedingDecryptionResp();
			IList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO> decryptedItems;
			if (list != null)
			{
				decryptedItems = (from g in list
				select g.ToDTO()).ToList<LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO>();
			}
			else
			{
				decryptedItems = null;
			}
			decryptLegacyDataItemsNeedingDecryptionResp.DecryptedItems = decryptedItems;
			return decryptLegacyDataItemsNeedingDecryptionResp;
		}
	}
}

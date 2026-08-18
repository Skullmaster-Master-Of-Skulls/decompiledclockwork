using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Core.Legacy;
using TechnoPro.Common.Core.Mappers.Legacy.DynamicData;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200005B RID: 91
	public class LegacyDynamicDataServiceManager : ILegacyDynamicData, IService
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0000FCC8 File Offset: 0x0000DEC8
		public GetDynamicDataDecryptedPreviewItemsResp GetDynamicDataDecryptedPreviewItems(GetDynamicDataDecryptedPreviewItemsReq Request)
		{
			ILegacyDynamicDataManager legacyDynamicDataManager = new LegacyDynamicDataManager(Request.GetOperationContext());
			IList<DynamicDataDecryptedPreviewItem> dynamicDataDecryptedPreviewItems = legacyDynamicDataManager.GetDynamicDataDecryptedPreviewItems(Request.ScreenNum, Request.ControlId);
			GetDynamicDataDecryptedPreviewItemsResp getDynamicDataDecryptedPreviewItemsResp = new GetDynamicDataDecryptedPreviewItemsResp();
			IList<DynamicDataDecryptedPreviewItemDTO> decryptedItems;
			if (dynamicDataDecryptedPreviewItems == null)
			{
				decryptedItems = null;
			}
			else
			{
				decryptedItems = (from g in dynamicDataDecryptedPreviewItems
				select g.ToDTO()).ToList<DynamicDataDecryptedPreviewItemDTO>();
			}
			getDynamicDataDecryptedPreviewItemsResp.DecryptedItems = decryptedItems;
			return getDynamicDataDecryptedPreviewItemsResp;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000FD38 File Offset: 0x0000DF38
		public ReverseEncryptionOnDataResp ReverseEncryptionOnData(ReverseEncryptionOnDataReq Request)
		{
			ILegacyDynamicDataManager legacyDynamicDataManager = new LegacyDynamicDataManager(Request.GetOperationContext());
			int numItemsAffected = legacyDynamicDataManager.ReverseEncryptionOnData(Request.ScreenNum, Request.ControlId, Request.NewEncrypted);
			return new ReverseEncryptionOnDataResp
			{
				NumItemsAffected = numItemsAffected
			};
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public LookupStaffSignatureBase64Resp LookupStaffSignatureBase64(LookupStaffSignatureBase64Req Request)
		{
			ILegacyDynamicDataManager legacyDynamicDataManager = new LegacyDynamicDataManager(Request.GetOperationContext());
			string staffSigBase = legacyDynamicDataManager.LookupStaffSignatureBase64(Request.PersonId);
			return new LookupStaffSignatureBase64Resp
			{
				StaffSigBase64 = staffSigBase
			};
		}
	}
}

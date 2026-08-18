using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Legacy
{
	// Token: 0x0200004A RID: 74
	public class LegacyDynamicDataClientManager : ILegacyDynamicDataClientManager, IWebService
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		public IList<DynamicDataDecryptedPreviewItemDTO> GetDynamicDataDecryptedPreviewItems(int ScreenNum, int ControlId)
		{
			GetDynamicDataDecryptedPreviewItemsReq getDynamicDataDecryptedPreviewItemsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetDynamicDataDecryptedPreviewItemsReq>();
			getDynamicDataDecryptedPreviewItemsReq.ScreenNum = ScreenNum;
			getDynamicDataDecryptedPreviewItemsReq.ControlId = ControlId;
			return ClientServiceFactory.GetClientInstance<ILegacyDynamicData>().GetDynamicDataDecryptedPreviewItems(getDynamicDataDecryptedPreviewItemsReq).DecryptedItems;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C01C File Offset: 0x0000A21C
		public int ReverseEncryptionOnData(int ScreenNum, int ControlId, bool newEncrypted)
		{
			ReverseEncryptionOnDataReq reverseEncryptionOnDataReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<ReverseEncryptionOnDataReq>();
			reverseEncryptionOnDataReq.ScreenNum = ScreenNum;
			reverseEncryptionOnDataReq.ControlId = ControlId;
			reverseEncryptionOnDataReq.NewEncrypted = newEncrypted;
			return ClientServiceFactory.GetClientInstance<ILegacyDynamicData>().ReverseEncryptionOnData(reverseEncryptionOnDataReq).NumItemsAffected;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C064 File Offset: 0x0000A264
		public string LookupStaffSignatureBase64(int pid)
		{
			LookupStaffSignatureBase64Req lookupStaffSignatureBase64Req = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LookupStaffSignatureBase64Req>();
			lookupStaffSignatureBase64Req.PersonId = pid;
			return ClientServiceFactory.GetClientInstance<ILegacyDynamicData>().LookupStaffSignatureBase64(lookupStaffSignatureBase64Req).StaffSigBase64;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000387F File Offset: 0x00001A7F
		public void SaveLegacyStudentNote(LegacyStudentNoteDTO note)
		{
			throw new NotImplementedException();
		}
	}
}

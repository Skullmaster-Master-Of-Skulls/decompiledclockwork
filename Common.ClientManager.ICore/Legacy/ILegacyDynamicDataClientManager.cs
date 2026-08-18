using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Legacy
{
	// Token: 0x02000045 RID: 69
	public interface ILegacyDynamicDataClientManager : IWebService
	{
		// Token: 0x060001EB RID: 491
		IList<DynamicDataDecryptedPreviewItemDTO> GetDynamicDataDecryptedPreviewItems(int ScreenNum, int ControlId);

		// Token: 0x060001EC RID: 492
		int ReverseEncryptionOnData(int ScreenNum, int ControlId, bool newEncrypted);

		// Token: 0x060001ED RID: 493
		string LookupStaffSignatureBase64(int pid);

		// Token: 0x060001EE RID: 494
		void SaveLegacyStudentNote(LegacyStudentNoteDTO note);
	}
}

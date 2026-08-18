using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.DAO.Legacy
{
	// Token: 0x02000061 RID: 97
	public interface ILegacyDynamicDataDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600022E RID: 558
		IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItemsForPerStudentData(int ControlId, bool IsDataEncrypted);

		// Token: 0x0600022F RID: 559
		IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItemsForPerAppointmentData(int ControlId, bool IsDataEncrypted);

		// Token: 0x06000230 RID: 560
		IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItemsForAccommodationData(int ControlId, bool IsDataEncrypted);

		// Token: 0x06000231 RID: 561
		int ReEncryptAndSaveDataPerStudent(IList<DynamicDataDecryptedPreviewItem> previewItems);

		// Token: 0x06000232 RID: 562
		int ReDecryptAndSaveDataPerStudent(IList<DynamicDataDecryptedPreviewItem> previewItems);

		// Token: 0x06000233 RID: 563
		int ReEncryptAndSaveDataPerAppointment(IList<DynamicDataDecryptedPreviewItem> previewItems);

		// Token: 0x06000234 RID: 564
		int ReDecryptAndSaveDataPerAppointment(IList<DynamicDataDecryptedPreviewItem> previewItems);

		// Token: 0x06000235 RID: 565
		int ReEncryptAndSaveDataAccommodationData(IList<DynamicDataDecryptedPreviewItem> previewItems);

		// Token: 0x06000236 RID: 566
		int ReDecryptAndSaveDataAccommodationData(IList<DynamicDataDecryptedPreviewItem> previewItems);

		// Token: 0x06000237 RID: 567
		byte[] LookupStaffSignature(int pid);

		// Token: 0x06000238 RID: 568
		void SaveLegacyStudentNote(LegacyStudentNote note);
	}
}

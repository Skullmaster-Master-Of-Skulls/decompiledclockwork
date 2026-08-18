using System;
using System.Collections.Generic;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.DynamicData;

namespace TechnoPro.Common.ICore.Legacy
{
	// Token: 0x02000076 RID: 118
	public interface ILegacyDynamicDataManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600034D RID: 845
		IList<DynamicDataDecryptedPreviewItem> GetDynamicDataDecryptedPreviewItems(int ScreenNum, int ControlId);

		// Token: 0x0600034E RID: 846
		int ReverseEncryptionOnData(int ScreenNum, int ControlId, bool newEncrypted);

		// Token: 0x0600034F RID: 847
		string LookupStaffSignatureBase64(int pid);

		// Token: 0x06000350 RID: 848
		void SaveLegacyStudentNote(LegacyStudentNote note);

		// Token: 0x06000351 RID: 849
		IList<Pair<int, string>> GetPersonEmailPhone(int pid);
	}
}

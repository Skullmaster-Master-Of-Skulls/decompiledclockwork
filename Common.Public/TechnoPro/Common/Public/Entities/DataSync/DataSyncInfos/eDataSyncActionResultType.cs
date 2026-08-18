using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos
{
	// Token: 0x020003E1 RID: 993
	public enum eDataSyncActionResultType
	{
		// Token: 0x040017BD RID: 6077
		Unknown,
		// Token: 0x040017BE RID: 6078
		ClockWorkDataClearedSuccess,
		// Token: 0x040017BF RID: 6079
		ClockWorkDataUpdatedSuccess,
		// Token: 0x040017C0 RID: 6080
		ClockWorkDataClearedFail,
		// Token: 0x040017C1 RID: 6081
		ClockWorkDataUpdatedFail,
		// Token: 0x040017C2 RID: 6082
		FailBecauseStudentNoNotFound,
		// Token: 0x040017C3 RID: 6083
		FailedToParseExternalData
	}
}

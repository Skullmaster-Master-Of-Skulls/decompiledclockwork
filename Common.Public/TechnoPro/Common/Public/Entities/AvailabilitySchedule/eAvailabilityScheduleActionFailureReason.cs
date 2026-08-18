using System;

namespace TechnoPro.Common.Public.Entities.AvailabilitySchedule
{
	// Token: 0x02000482 RID: 1154
	[Serializable]
	public enum eAvailabilityScheduleActionFailureReason
	{
		// Token: 0x04001A24 RID: 6692
		Unknown,
		// Token: 0x04001A25 RID: 6693
		ConflictWithExistingSchedule,
		// Token: 0x04001A26 RID: 6694
		InvalidParameters,
		// Token: 0x04001A27 RID: 6695
		AuthenticationProblem,
		// Token: 0x04001A28 RID: 6696
		InvalidParametersItemNotFound
	}
}

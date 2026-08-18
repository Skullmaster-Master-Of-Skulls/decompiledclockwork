using System;

namespace TechnoPro.Common.Public.Entities.MergeDuplicates.Courses
{
	// Token: 0x020002A2 RID: 674
	public enum eDuplicateCourseMergeStatus
	{
		// Token: 0x04001131 RID: 4401
		Unknown,
		// Token: 0x04001132 RID: 4402
		Success,
		// Token: 0x04001133 RID: 4403
		Failed,
		// Token: 0x04001134 RID: 4404
		TestModeNotRun,
		// Token: 0x04001135 RID: 4405
		BatchProcessStarted,
		// Token: 0x04001136 RID: 4406
		BatchProcessCompletedSuccessfully,
		// Token: 0x04001137 RID: 4407
		BatchProcessFailedInterrupted
	}
}

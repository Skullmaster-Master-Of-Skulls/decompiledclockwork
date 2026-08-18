using System;

namespace TechnoPro.Common.Public.Entities.Surveys
{
	// Token: 0x0200017C RID: 380
	[Serializable]
	public enum eSurveyStatusType
	{
		// Token: 0x04000727 RID: 1831
		[SurveyStatusType("New submission")]
		New,
		// Token: 0x04000728 RID: 1832
		[SurveyStatusType("Pending (working on it)")]
		PendingWorkingOnIt,
		// Token: 0x04000729 RID: 1833
		[SurveyStatusType("Pending (but waiting for something)")]
		PendingButWaiting,
		// Token: 0x0400072A RID: 1834
		[SurveyStatusType("Pending (with a problem)")]
		PendingWithProblem,
		// Token: 0x0400072B RID: 1835
		[SurveyStatusType("Hold")]
		Hold,
		// Token: 0x0400072C RID: 1836
		[SurveyStatusType("Closed and complete")]
		ClosedComplete,
		// Token: 0x0400072D RID: 1837
		[SurveyStatusType("Closed but in-complete")]
		ClosedIncomplete
	}
}

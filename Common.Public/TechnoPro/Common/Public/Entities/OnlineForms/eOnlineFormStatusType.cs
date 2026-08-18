using System;

namespace TechnoPro.Common.Public.Entities.OnlineForms
{
	// Token: 0x02000273 RID: 627
	[Serializable]
	public enum eOnlineFormStatusType
	{
		// Token: 0x0400103B RID: 4155
		[OnlineFormStatusType("New submission")]
		New,
		// Token: 0x0400103C RID: 4156
		[OnlineFormStatusType("Pending (working on it)")]
		PendingWorkingOnIt,
		// Token: 0x0400103D RID: 4157
		[OnlineFormStatusType("Pending (but waiting for something)")]
		PendingButWaiting,
		// Token: 0x0400103E RID: 4158
		[OnlineFormStatusType("Pending (with a problem)")]
		PendingWithProblem,
		// Token: 0x0400103F RID: 4159
		[OnlineFormStatusType("Hold")]
		Hold,
		// Token: 0x04001040 RID: 4160
		[OnlineFormStatusType("Closed and complete")]
		ClosedComplete,
		// Token: 0x04001041 RID: 4161
		[OnlineFormStatusType("Closed but in-complete")]
		ClosedIncomplete
	}
}

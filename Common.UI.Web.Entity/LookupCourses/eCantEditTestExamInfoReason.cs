using System;

namespace TechnoPro.Common.UI.Web.Entity.LookupCourses
{
	// Token: 0x0200002F RID: 47
	[Serializable]
	public enum eCantEditTestExamInfoReason
	{
		// Token: 0x04000112 RID: 274
		Unknown,
		// Token: 0x04000113 RID: 275
		UserCancelled,
		// Token: 0x04000114 RID: 276
		CutoffTimeForEditingTestHasPassed,
		// Token: 0x04000115 RID: 277
		InstructorOrAltContactNotAllowed,
		// Token: 0x04000116 RID: 278
		CantAddTestCourseHasEnded
	}
}

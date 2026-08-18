using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing
{
	// Token: 0x020001A8 RID: 424
	public enum eTryToSendInstructorEmailStatus
	{
		// Token: 0x04000804 RID: 2052
		Unknown,
		// Token: 0x04000805 RID: 2053
		Success,
		// Token: 0x04000806 RID: 2054
		FailedMissingProfEmail,
		// Token: 0x04000807 RID: 2055
		FailedStatusNotApproved,
		// Token: 0x04000808 RID: 2056
		FailedProfNotAllowedToSeeStudentAccommodationLetter,
		// Token: 0x04000809 RID: 2057
		FailedUnspecified
	}
}

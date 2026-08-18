using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x020001A1 RID: 417
	[Flags]
	[Serializable]
	public enum eStudentCourseAccommodationRequestStatus
	{
		// Token: 0x040007E2 RID: 2018
		[StudentCourseAccommodationRequestStatus("Unknown")]
		Unknown = 0,
		// Token: 0x040007E3 RID: 2019
		[StudentCourseAccommodationRequestStatus("Pending waiting for staff")]
		PendingWaitingForStaff = 1,
		// Token: 0x040007E4 RID: 2020
		[StudentCourseAccommodationRequestStatus("Pending waiting for student")]
		PendingWaitingForStudent = 2,
		// Token: 0x040007E5 RID: 2021
		[StudentCourseAccommodationRequestStatus("Denied")]
		Denied = 4,
		// Token: 0x040007E6 RID: 2022
		[StudentCourseAccommodationRequestStatus("Approved")]
		Approved = 8,
		// Token: 0x040007E7 RID: 2023
		[StudentCourseAccommodationRequestStatus("Instructor info missing")]
		InstructorInfoMissing = 16
	}
}

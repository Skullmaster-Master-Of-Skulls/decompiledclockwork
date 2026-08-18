using System;

namespace TechnoPro.Common.Public.Entities.StudentAccommodationRequests
{
	// Token: 0x0200019B RID: 411
	[Serializable]
	public enum eStudentCourseAccommodationRequestHistoryItemHowModified
	{
		// Token: 0x040007BF RID: 1983
		[StudentCourseAccommodationRequestHistoryItemHowModified("Unknown")]
		Unknown = 63,
		// Token: 0x040007C0 RID: 1984
		[StudentCourseAccommodationRequestHistoryItemHowModified("Deleted")]
		Deleted = 100,
		// Token: 0x040007C1 RID: 1985
		[StudentCourseAccommodationRequestHistoryItemHowModified("Updated")]
		Updated = 117,
		// Token: 0x040007C2 RID: 1986
		[StudentCourseAccommodationRequestHistoryItemHowModified("Added")]
		Added = 110
	}
}

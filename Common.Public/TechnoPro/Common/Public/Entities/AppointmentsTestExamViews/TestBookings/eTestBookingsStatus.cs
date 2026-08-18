using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings
{
	// Token: 0x020004F1 RID: 1265
	[Serializable]
	public enum eTestBookingsStatus
	{
		// Token: 0x04001C2B RID: 7211
		[TestBookingsStatus("Unknown")]
		Unknown,
		// Token: 0x04001C2C RID: 7212
		[TestBookingsStatus("Booked")]
		Booked,
		// Token: 0x04001C2D RID: 7213
		[TestBookingsStatus("No students")]
		NoStudents,
		// Token: 0x04001C2E RID: 7214
		[TestBookingsStatus("Dropped")]
		DroppedCourse,
		// Token: 0x04001C2F RID: 7215
		[TestBookingsStatus("Cancelled")]
		Cancelled,
		// Token: 0x04001C30 RID: 7216
		[TestBookingsStatus("No-show")]
		NoShow,
		// Token: 0x04001C31 RID: 7217
		[TestBookingsStatus("Tentative")]
		Tentative,
		// Token: 0x04001C32 RID: 7218
		[TestBookingsStatus("Accommodations modified")]
		AccommodationsModified = 6
	}
}

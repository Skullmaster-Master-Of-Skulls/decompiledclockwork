using System;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x0200009F RID: 159
	[Serializable]
	public enum eTryToBookFailureType
	{
		// Token: 0x04000170 RID: 368
		Unknown,
		// Token: 0x04000171 RID: 369
		ClassTestIsAHoliday,
		// Token: 0x04000172 RID: 370
		StudentAlreadyBookedATestForThisClassDateTime,
		// Token: 0x04000173 RID: 371
		NoAccommodationsToUse,
		// Token: 0x04000174 RID: 372
		StudentIsDoubleBooked
	}
}

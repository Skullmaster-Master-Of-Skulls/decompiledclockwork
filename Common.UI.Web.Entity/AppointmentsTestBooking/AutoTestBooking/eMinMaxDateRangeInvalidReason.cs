using System;

namespace TechnoPro.Common.UI.Web.Entity.AppointmentsTestBooking.AutoTestBooking
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public enum eMinMaxDateRangeInvalidReason
	{
		// Token: 0x04000166 RID: 358
		Unknown,
		// Token: 0x04000167 RID: 359
		IsValid,
		// Token: 0x04000168 RID: 360
		InvalidFinalExamPeriodRangeAndOrCutoffDate,
		// Token: 0x04000169 RID: 361
		AccommodationsExpiredBecauseDateIsBlank,
		// Token: 0x0400016A RID: 362
		AccommodationsExpiredBeforeMinBookingDate,
		// Token: 0x0400016B RID: 363
		FinalExamPeriodRangeIsInThePast
	}
}

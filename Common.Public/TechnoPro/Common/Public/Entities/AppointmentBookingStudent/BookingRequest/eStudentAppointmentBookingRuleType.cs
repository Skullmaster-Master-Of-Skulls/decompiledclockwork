using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200056E RID: 1390
	public enum eStudentAppointmentBookingRuleType
	{
		// Token: 0x04001F77 RID: 8055
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.Unknown, null)]
		Unknown,
		// Token: 0x04001F78 RID: 8056
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinDateOfAppointment, "StudentAppointmentBookingRuleMaxNumberAppsInFutureManager")]
		MaxNumberInFuture,
		// Token: 0x04001F79 RID: 8057
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinDateOfAppointment, "StudentAppointmentBookingRuleMaxNumberAppsInWeekManager")]
		MaxNumberInAWeek,
		// Token: 0x04001F7A RID: 8058
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinDateOfAppointment, "StudentAppointmentBookingRuleMaxNumberAppsInDayManager")]
		MaxNumberPerday,
		// Token: 0x04001F7B RID: 8059
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinStudent, "StudentAppointmentBookingRuleStudentMaxNumNoshowsManager")]
		MaxNumberOfNoShows,
		// Token: 0x04001F7C RID: 8060
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinStartEndOfAppointment, "StudentAppointmentBookingRuleCheckCutoffTimeManager")]
		CheckCutoffTime,
		// Token: 0x04001F7D RID: 8061
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinStartEndOfAppointment, "StudentAppointmentBookingRuleCheckStudentDoubleBookedManager")]
		CheckStudentDoubleBooked,
		// Token: 0x04001F7E RID: 8062
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinStaffToBookWith, "StudentAppointmentBookingRuleCheckStaffDoubleBookedManager")]
		CheckStaffDoubleBooked,
		// Token: 0x04001F7F RID: 8063
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinStudent, "StudentAppointmentBookingRuleStudentBannedManager")]
		CheckStudentBanned,
		// Token: 0x04001F80 RID: 8064
		[StudentAppointmentBookingRuleType(eStudentAppointmentBookingRuleAppliesTo.MinStartEndOfAppointment, "StudentAppointmentBookingRuleStudentCheckValidDateTimeManager")]
		CheckValidDateTime
	}
}

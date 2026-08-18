using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x02000089 RID: 137
	public interface IStudentClassTestInfoClientManager : IWebService
	{
		// Token: 0x0600040E RID: 1038
		void UpdateExamStatus(int AppointmentId, int NewExamStatusLookupId);

		// Token: 0x0600040F RID: 1039
		void UpdateBookingNote(int AppointmentId, string BookingNote);

		// Token: 0x06000410 RID: 1040
		void UpdatePrivateNote(int AppointmentId, string PrivateNote);

		// Token: 0x06000411 RID: 1041
		void UpdateBookingAndPrivateNote(int AppointmentId, string BookingNote, string PrivateNote);

		// Token: 0x06000412 RID: 1042
		void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(int AppointmentId);

		// Token: 0x06000413 RID: 1043
		StudentClassTestDTO LoadStudentTestInfoByAppointmentId(int AppointmentId);
	}
}

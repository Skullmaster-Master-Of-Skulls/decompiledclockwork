using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000C9 RID: 201
	public interface IStudentClassTestInfoManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000616 RID: 1558
		void DeleteStudentClassTestInfo(int AppointmentCourseId);

		// Token: 0x06000617 RID: 1559
		void UpdateExamStatus(int AppointmentId, int NewExamStatusLookupId);

		// Token: 0x06000618 RID: 1560
		StudentClassTest LoadClassTestByAppointmentId(int AppointmentId);

		// Token: 0x06000619 RID: 1561
		ExamStatus LoadExamStatusByAppointmentId(int AppointmentId);

		// Token: 0x0600061A RID: 1562
		void UpdateBookingAndPrivateNote(int AppointmentId, string BookingNote, string PrivateNote);

		// Token: 0x0600061B RID: 1563
		void UpdateBookingNote(int AppointmentId, string BookingNote);

		// Token: 0x0600061C RID: 1564
		void UpdatePrivateNote(int AppointmentId, string PrivateNote);

		// Token: 0x0600061D RID: 1565
		void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(int AppointmentId);

		// Token: 0x0600061E RID: 1566
		int CreateStudentClassTest(int AppointmentId, StudentClassTest StudentClassTest);

		// Token: 0x0600061F RID: 1567
		IDictionary<int, StudentClassTest> LoadClassTestsByAppointmentIds(params int[] appointmentIds);
	}
}

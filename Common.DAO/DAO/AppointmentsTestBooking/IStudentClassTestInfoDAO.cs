using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000BB RID: 187
	public interface IStudentClassTestInfoDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000501 RID: 1281
		void DeleteStudentClassTestInfo(int AppointmentCourseId);

		// Token: 0x06000502 RID: 1282
		void UpdateExamStatus(int AppointmentId, int NewExamStatusLookupId);

		// Token: 0x06000503 RID: 1283
		StudentClassTest LoadClassTestByAppointmentId(int AppointmentId);

		// Token: 0x06000504 RID: 1284
		ExamStatus LoadExamStatusByAppointmentId(int AppointmentId);

		// Token: 0x06000505 RID: 1285
		void UpdateBookingNote(int AppointmentId, string BookingNote);

		// Token: 0x06000506 RID: 1286
		void UpdatePrivateNote(int AppointmentId, string PrivateNote);

		// Token: 0x06000507 RID: 1287
		void UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTime(int AppointmentId);

		// Token: 0x06000508 RID: 1288
		int CreateStudentClassTest(int AppointmentId, StudentClassTest StudentClassTest);

		// Token: 0x06000509 RID: 1289
		IDictionary<int, StudentClassTest> LoadClassTestsByAppointmentIds(params int[] appointmentIds);
	}
}

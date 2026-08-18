using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeData;

namespace TechnoPro.Common.DAO.AppointmentsTestBooking
{
	// Token: 0x020000C1 RID: 193
	public interface ITestBookingDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000526 RID: 1318
		List<AccommodationForTest> LoadTestAccommodations(int AppointmentId, int PersonId, int LuCourseId);

		// Token: 0x06000527 RID: 1319
		List<Test> LoadTests(DateTime StartDate, DateTime EndDate, bool HideCancelled);

		// Token: 0x06000528 RID: 1320
		List<Test> LoadClassTestDefinitionBookings(int ExamId);

		// Token: 0x06000529 RID: 1321
		Test LoadTestById(int AppointmentId);

		// Token: 0x0600052A RID: 1322
		IList<MailMergeTestBooking> LoadTestBookingMailMergeInfoByDate(DateTime Date, bool ExcludeCancelled, IList<int> AppTypeIdsToExclude);

		// Token: 0x0600052B RID: 1323
		IList<Test> LoadTestsByAppointmentIds(IList<int> AppointmentIds);

		// Token: 0x0600052C RID: 1324
		IList<BasicTest> LoadBasicTestsByAppointmentIds(IList<int> AppointmentIds);

		// Token: 0x0600052D RID: 1325
		IList<ExamStatus> LoadAllExamStatuses();

		// Token: 0x0600052E RID: 1326
		IList<AccommodationData> LoadAccommodationsByTest(int AppointmentId, out int PersonId, out int LuCourseId);

		// Token: 0x0600052F RID: 1327
		void AddTestAccommodations(int AppointmentId, int PersonId, IList<int> ControlIds);

		// Token: 0x06000530 RID: 1328
		void RemoveTestAccommodations(int AppointmentId, int PersonId, IList<int> ControlIds);

		// Token: 0x06000531 RID: 1329
		TestForEdit LoadTestForEditById(int AppointmentId);

		// Token: 0x06000532 RID: 1330
		void UpdateTestBookingSpecific(int AppointmentId, TestForEditBookingSpecific info);

		// Token: 0x06000533 RID: 1331
		void CreateTestBookingSpecific(int AppointmentId, int LuCourseId, TestForEditBookingSpecific info);

		// Token: 0x06000534 RID: 1332
		void UpdateClassTestDefinitionSpecific(int ExamId, TestForEditClassDefinitionSpecific info);

		// Token: 0x06000535 RID: 1333
		void UpdateBreakTime(int AppointmentId, int BreakTimeMinutes);

		// Token: 0x06000536 RID: 1334
		void SetAppointmentExamId(int AppointmentId, int ExamId);

		// Token: 0x06000537 RID: 1335
		IList<Test> LoadTestsByStudent(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled);

		// Token: 0x06000538 RID: 1336
		IList<int> LoadAppointmentIdsByExamId(int ExamId);

		// Token: 0x06000539 RID: 1337
		IList<StudentWritingTest> LoadStudentsWritingExam(int examId);

		// Token: 0x0600053A RID: 1338
		InstructorAcknowledgedStudent LoadInstructorAcknowledgedStudent(int appId, IDictionary<int, string> acknowledgeValueTitles);
	}
}

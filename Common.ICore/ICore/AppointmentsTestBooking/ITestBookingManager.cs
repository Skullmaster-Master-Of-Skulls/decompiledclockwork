using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeData;

namespace TechnoPro.Common.ICore.AppointmentsTestBooking
{
	// Token: 0x020000CF RID: 207
	public interface ITestBookingManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600064D RID: 1613
		IList<Test> LoadTestsBySittingId(int SittingId);

		// Token: 0x0600064E RID: 1614
		IList<Test> LoadTests(DateTime StartDate, DateTime EndDate, bool HideCancelled);

		// Token: 0x0600064F RID: 1615
		Test LoadTestByAppointmentId(int AppointmentId);

		// Token: 0x06000650 RID: 1616
		IList<Test> LoadTestsByExamId(int ExamId);

		// Token: 0x06000651 RID: 1617
		IList<StudentWritingTest> LoadStudentsWritingExam(int examId);

		// Token: 0x06000652 RID: 1618
		IList<TestBase> LoadTestBasesByExamId(int ExamId);

		// Token: 0x06000653 RID: 1619
		List<AccommodationForTest> LoadTestAccommodations(int AppointmentId, int PersonId, int LuCourseId);

		// Token: 0x06000654 RID: 1620
		IList<DynamicData> LoadInstructorFormData(int ExamId);

		// Token: 0x06000655 RID: 1621
		IList<MailMergeTestBooking> LoadTestBookingMailMergeInfoByDate(DateTime Date, bool ExcludeCancelled, IList<int> AppTypeIdsToExclude);

		// Token: 0x06000656 RID: 1622
		void DeleteTest(bool runInTransaction, int AppointmentId);

		// Token: 0x06000657 RID: 1623
		IList<Test> LoadTestsByAppointmentIds(IList<int> AppointmentIds);

		// Token: 0x06000658 RID: 1624
		IList<BasicTest> LoadBasicTestsByAppointmentIds(IList<int> AppointmentIds);

		// Token: 0x06000659 RID: 1625
		IList<ExamStatus> LoadAllExamStatuses();

		// Token: 0x0600065A RID: 1626
		IList<AccommodationData> LoadAccommodationsByTest(int AppointmentId, out int PersonId, out int LuCourseId);

		// Token: 0x0600065B RID: 1627
		void LoadTestAndAllowedAccommodations(int AppointmentId, out IList<AccommodationData> AllowedAccommodations, out IList<AccommodationData> AccommodationsForTest, out int PersonId, out int LuCourseId);

		// Token: 0x0600065C RID: 1628
		TestForEdit LoadTestForEditByAppointmentId(int AppointmentId);

		// Token: 0x0600065D RID: 1629
		void UpdateTestAccommodations(int AppointmentId, int PersonId, IList<int> cidsToAdd, IList<int> cidsToRemove);

		// Token: 0x0600065E RID: 1630
		void UpdateTest(TestForEdit2 Test, IList<DynamicData> StudentAdditionalInfoData, IList<AccommodationForTest> InstructorFormData, IList<ExamFile> ExamFiles, Sitting Sitting);

		// Token: 0x0600065F RID: 1631
		int CreateTest(TestForEdit2 Test, IList<DynamicData> StudentAdditionalInfoData, IList<AccommodationForTest> InstructorFormData, IList<ExamFile> ExamFiles, Sitting Sitting);

		// Token: 0x06000660 RID: 1632
		void UpdateInstructorFormData(int ExamId, IList<AccommodationForTest> NewData);

		// Token: 0x06000661 RID: 1633
		void UpdateBreakTime(int AppointmentId, int BreakTimeMinutes);

		// Token: 0x06000662 RID: 1634
		IList<Test> LoadTestsByStudent(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled);

		// Token: 0x06000663 RID: 1635
		IList<int> LoadAppointmentIdsByExamId(int ExamId);

		// Token: 0x06000664 RID: 1636
		InstructorAcknowledgedStudent LoadInstructorAcknowledgedStudent(int appId);
	}
}

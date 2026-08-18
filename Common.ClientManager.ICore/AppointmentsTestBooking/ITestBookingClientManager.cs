using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeData;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking
{
	// Token: 0x0200008A RID: 138
	public interface ITestBookingClientManager : IWebService
	{
		// Token: 0x06000414 RID: 1044
		IList<TestDTO> LoadTests(DateTime StartDate, DateTime EndDate, bool HideCancelled);

		// Token: 0x06000415 RID: 1045
		IList<AccommodationForTestDTO> LoadTestAccommodations(int AppointmentId, int PersonId, int LuCourseId);

		// Token: 0x06000416 RID: 1046
		TestDTO LoadTestByAppointmentId(int AppointmentId);

		// Token: 0x06000417 RID: 1047
		IList<MailMergeTestBookingDTO> LoadTestBookingMailMergeInfoByDate(DateTime Date, bool ExcludeCancelled, IList<int> AppTypeIdsToExclude);

		// Token: 0x06000418 RID: 1048
		void DeleteTest(int AppointmentId);

		// Token: 0x06000419 RID: 1049
		IList<TestDTO> LoadTestsByExamId(int ExamId);

		// Token: 0x0600041A RID: 1050
		IList<TestDTO> LoadTestsByAppointmentIds(IList<int> AppointmentIds);

		// Token: 0x0600041B RID: 1051
		IList<BasicTestDTO> LoadBasicTestsByAppointmentIds(IList<int> AppointmentIds);

		// Token: 0x0600041C RID: 1052
		IList<ExamStatusDTO> LoadAllExamStatuses();

		// Token: 0x0600041D RID: 1053
		IList<AccommodationDataDTO> LoadAccommodationsByTest(int AppointmentId, out int PersonId, out int LuCourseId);

		// Token: 0x0600041E RID: 1054
		void LoadTestAndAllowedAccommodations(int AppointmentId, out IList<AccommodationDataDTO> AllowedAccommodations, out IList<AccommodationDataDTO> AccommodationsForTest, out int PersonId, out int LuCourseId);

		// Token: 0x0600041F RID: 1055
		TestForEditDTO LoadTestForEditByAppointmentId(int AppointmentId);

		// Token: 0x06000420 RID: 1056
		void UpdateTestAccommodations(int AppointmentId, int PersonId, IList<int> cidsToAdd, IList<int> cidsToRemove);

		// Token: 0x06000421 RID: 1057
		void UpdateTest(TestForEdit2DTO Test, IList<DynamicDataDTO> StudentAdditionalInfoData, IList<AccommodationForTestDTO> InstructorFormData, IList<ExamFileDTO> ExamFiles, SittingDTO Sitting);

		// Token: 0x06000422 RID: 1058
		int CreateTest(TestForEdit2DTO Test, IList<DynamicDataDTO> StudentAdditionalInfoData, IList<AccommodationForTestDTO> InstructorFormData, IList<ExamFileDTO> ExamFiles, SittingDTO Sitting);

		// Token: 0x06000423 RID: 1059
		void CancelOrUncancelTestBooking(int AppointmentId, bool NewIsCancelled);

		// Token: 0x06000424 RID: 1060
		void ChangeTentativeStatus(int AppointmentId, bool NewIsTentative);

		// Token: 0x06000425 RID: 1061
		void AddProctorToTest(int AppointmentId, int PersonId);

		// Token: 0x06000426 RID: 1062
		IList<StudentWritingTestDTO> LoadStudentsWritingExam(int examId);

		// Token: 0x06000427 RID: 1063
		InstructorAcknowledgedStudentDTO LoadInstructorAcknowledgedStudent(int appId);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeData;
using TechnoPro.Common.Core.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.Accommodations;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.MailMergeEntities.MailMergeData;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeData;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000097 RID: 151
	public class TestBookingServiceManager : ITestBooking, IService
	{
		// Token: 0x06000569 RID: 1385 RVA: 0x00019188 File Offset: 0x00017388
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001919C File Offset: 0x0001739C
		public LoadTestsResp LoadTests(LoadTestsReq request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(request.GetOperationContext());
			IList<Test> list = testBookingManager.LoadTests(request.StartDate, request.EndDate, request.HideCancelled);
			LoadTestsResp loadTestsResp = new LoadTestsResp();
			List<TestDTO> tests;
			if (list == null)
			{
				tests = null;
			}
			else
			{
				tests = list.ToList<Test>().ConvertAll<TestDTO>((Test f) => f.ToDTO());
			}
			loadTestsResp.Tests = tests;
			return loadTestsResp;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00019210 File Offset: 0x00017410
		public void UpdateTest(UpdateTestReq request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(request.GetOperationContext());
			ITestBookingManager testBookingManager2 = testBookingManager;
			TestForEdit2 test = request.Test.ToDomainObject();
			IList<DynamicDataDTO> studentAdditionalInfoData = request.StudentAdditionalInfoData;
			IList<DynamicData> studentAdditionalInfoData2;
			if (studentAdditionalInfoData == null)
			{
				studentAdditionalInfoData2 = null;
			}
			else
			{
				studentAdditionalInfoData2 = studentAdditionalInfoData.ToList<DynamicDataDTO>().ConvertAll<DynamicData>((DynamicDataDTO g) => g.ToDomainObject());
			}
			IList<AccommodationForTestDTO> instructorFormData = request.InstructorFormData;
			IList<AccommodationForTest> instructorFormData2;
			if (instructorFormData == null)
			{
				instructorFormData2 = null;
			}
			else
			{
				instructorFormData2 = instructorFormData.ToList<AccommodationForTestDTO>().ConvertAll<AccommodationForTest>((AccommodationForTestDTO g) => g.ToDomainObject());
			}
			IList<ExamFileDTO> examFiles = request.ExamFiles;
			IList<ExamFile> examFiles2;
			if (examFiles == null)
			{
				examFiles2 = null;
			}
			else
			{
				examFiles2 = examFiles.ToList<ExamFileDTO>().ConvertAll<ExamFile>((ExamFileDTO g) => g.ToDomainObject());
			}
			SittingDTO sitting = request.Sitting;
			testBookingManager2.UpdateTest(test, studentAdditionalInfoData2, instructorFormData2, examFiles2, (sitting != null) ? sitting.ToDomainObject() : null);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000192F0 File Offset: 0x000174F0
		public LoadTestAccommodationsResp LoadTestAccommodations(LoadTestAccommodationsReq request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(request.GetOperationContext());
			List<AccommodationForTest> list = testBookingManager.LoadTestAccommodations(request.AppointmentId, request.PersonId, request.LuCourseId);
			LoadTestAccommodationsResp loadTestAccommodationsResp = new LoadTestAccommodationsResp();
			List<AccommodationForTestDTO> accommodationsForTest;
			if (list == null)
			{
				accommodationsForTest = null;
			}
			else
			{
				accommodationsForTest = list.ConvertAll<AccommodationForTestDTO>((AccommodationForTest g) => g.ToDTO());
			}
			loadTestAccommodationsResp.AccommodationsForTest = accommodationsForTest;
			return loadTestAccommodationsResp;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00019360 File Offset: 0x00017560
		public LoadTestByAppointmentIdResp LoadTestByAppointmentId(LoadTestByAppointmentIdReq request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(request.GetOperationContext());
			Test test = testBookingManager.LoadTestByAppointmentId(request.AppointmentId);
			return new LoadTestByAppointmentIdResp
			{
				Test = ((test != null) ? test.ToDTO() : null)
			};
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x000193A4 File Offset: 0x000175A4
		public LoadTestBookingMailMergeInfoByDateResp LoadTestBookingMailMergeInfoByDate(LoadTestBookingMailMergeInfoByDateReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<MailMergeTestBooking> list = testBookingManager.LoadTestBookingMailMergeInfoByDate(Request.Date, Request.ExcludeCancelled, Request.AppTypeIdsToExclude);
			LoadTestBookingMailMergeInfoByDateResp loadTestBookingMailMergeInfoByDateResp = new LoadTestBookingMailMergeInfoByDateResp();
			IList<MailMergeTestBookingDTO> items;
			if (list == null)
			{
				items = null;
			}
			else
			{
				items = list.ToList<MailMergeTestBooking>().ConvertAll<MailMergeTestBookingDTO>((MailMergeTestBooking g) => g.ToDTO());
			}
			loadTestBookingMailMergeInfoByDateResp.Items = items;
			return loadTestBookingMailMergeInfoByDateResp;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00019418 File Offset: 0x00017618
		public void DeleteTest(DeleteTestReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			testBookingManager.DeleteTest(false, Request.AppointmentId);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00019440 File Offset: 0x00017640
		public LoadTestsByExamIdResp LoadTestsByExamId(LoadTestsByExamIdReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<Test> list = testBookingManager.LoadTestsByExamId(Request.ExamId);
			LoadTestsByExamIdResp loadTestsByExamIdResp = new LoadTestsByExamIdResp();
			IList<TestDTO> tests;
			if (list == null)
			{
				tests = null;
			}
			else
			{
				tests = list.ToList<Test>().ConvertAll<TestDTO>((Test f) => f.ToDTO());
			}
			loadTestsByExamIdResp.Tests = tests;
			return loadTestsByExamIdResp;
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000194A8 File Offset: 0x000176A8
		public LoadTestsByAppointmentIdsResp LoadTestsByAppointmentIds(LoadTestsByAppointmentIdsReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<Test> list = testBookingManager.LoadTestsByAppointmentIds(Request.AppointmentIds);
			LoadTestsByAppointmentIdsResp loadTestsByAppointmentIdsResp = new LoadTestsByAppointmentIdsResp();
			IList<TestDTO> tests;
			if (list == null)
			{
				tests = null;
			}
			else
			{
				tests = list.ToList<Test>().ConvertAll<TestDTO>((Test f) => f.ToDTO());
			}
			loadTestsByAppointmentIdsResp.Tests = tests;
			return loadTestsByAppointmentIdsResp;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00019510 File Offset: 0x00017710
		public LoadBasicTestsByAppointmentIdsResp LoadBasicTestsByAppointmentIds(LoadBasicTestsByAppointmentIdsReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<BasicTest> list = testBookingManager.LoadBasicTestsByAppointmentIds(Request.AppointmentIds);
			LoadBasicTestsByAppointmentIdsResp loadBasicTestsByAppointmentIdsResp = new LoadBasicTestsByAppointmentIdsResp();
			IList<BasicTestDTO> tests;
			if (list == null)
			{
				tests = null;
			}
			else
			{
				tests = list.ToList<BasicTest>().ConvertAll<BasicTestDTO>((BasicTest f) => f.ToDTO());
			}
			loadBasicTestsByAppointmentIdsResp.Tests = tests;
			return loadBasicTestsByAppointmentIdsResp;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00019578 File Offset: 0x00017778
		public LoadAllExamStatusesResp LoadAllExamStatuses(LoadAllExamStatusesReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<ExamStatus> list = testBookingManager.LoadAllExamStatuses();
			LoadAllExamStatusesResp loadAllExamStatusesResp = new LoadAllExamStatusesResp();
			IList<ExamStatusDTO> examStatuses;
			if (list == null)
			{
				examStatuses = null;
			}
			else
			{
				examStatuses = list.ToList<ExamStatus>().ConvertAll<ExamStatusDTO>((ExamStatus f) => f.ToDTO());
			}
			loadAllExamStatusesResp.ExamStatuses = examStatuses;
			return loadAllExamStatusesResp;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x000195DC File Offset: 0x000177DC
		public LoadAccommodationsByTestResp LoadAccommodationsByTest(LoadAccommodationsByTestReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			int personId;
			int luCourseId;
			IList<AccommodationData> list = testBookingManager.LoadAccommodationsByTest(Request.AppointmentId, out personId, out luCourseId);
			LoadAccommodationsByTestResp loadAccommodationsByTestResp = new LoadAccommodationsByTestResp();
			IList<AccommodationDataDTO> testAccommodations;
			if (list == null)
			{
				testAccommodations = null;
			}
			else
			{
				testAccommodations = list.ToList<AccommodationData>().ConvertAll<AccommodationDataDTO>((AccommodationData g) => g.ToDTO());
			}
			loadAccommodationsByTestResp.TestAccommodations = testAccommodations;
			loadAccommodationsByTestResp.LuCourseId = luCourseId;
			loadAccommodationsByTestResp.PersonId = personId;
			return loadAccommodationsByTestResp;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0001965C File Offset: 0x0001785C
		public LoadTestAndAllowedAccommodationsResp LoadTestAndAllowedAccommodations(LoadTestAndAllowedAccommodationsReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<AccommodationData> list;
			IList<AccommodationData> list2;
			int personId;
			int luCourseId;
			testBookingManager.LoadTestAndAllowedAccommodations(Request.AppointmentId, out list, out list2, out personId, out luCourseId);
			LoadTestAndAllowedAccommodationsResp loadTestAndAllowedAccommodationsResp = new LoadTestAndAllowedAccommodationsResp();
			IList<AccommodationDataDTO> testAccommodations;
			if (list2 == null)
			{
				testAccommodations = null;
			}
			else
			{
				testAccommodations = list2.ToList<AccommodationData>().ConvertAll<AccommodationDataDTO>((AccommodationData g) => g.ToDTO());
			}
			loadTestAndAllowedAccommodationsResp.TestAccommodations = testAccommodations;
			IList<AccommodationDataDTO> allowedAccommodations;
			if (list == null)
			{
				allowedAccommodations = null;
			}
			else
			{
				allowedAccommodations = list.ToList<AccommodationData>().ConvertAll<AccommodationDataDTO>((AccommodationData g) => g.ToDTO());
			}
			loadTestAndAllowedAccommodationsResp.AllowedAccommodations = allowedAccommodations;
			loadTestAndAllowedAccommodationsResp.LuCourseId = luCourseId;
			loadTestAndAllowedAccommodationsResp.PersonId = personId;
			return loadTestAndAllowedAccommodationsResp;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00019718 File Offset: 0x00017918
		public LoadTestForEditByAppointmentIdResp LoadTestForEditByAppointmentId(LoadTestForEditByAppointmentIdReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			TestForEdit testForEdit = testBookingManager.LoadTestForEditByAppointmentId(Request.AppointmentId);
			return new LoadTestForEditByAppointmentIdResp
			{
				TestForEdit = ((testForEdit != null) ? testForEdit.ToDTO() : null)
			};
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0001975C File Offset: 0x0001795C
		public UpdateTestAccommodationsResp UpdateTestAccommodations(UpdateTestAccommodationsReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			testBookingManager.UpdateTestAccommodations(Request.AppointmentId, Request.PersonId, Request.ControlIdsToAdd, Request.ControlIdsToRemove);
			return new UpdateTestAccommodationsResp();
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x000197A0 File Offset: 0x000179A0
		public CreateTestResp CreateTest(CreateTestReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			CreateTestResp createTestResp = new CreateTestResp();
			ITestBookingManager testBookingManager2 = testBookingManager;
			TestForEdit2 test = Request.Test.ToDomainObject();
			IList<DynamicDataDTO> studentAdditionalInfoData = Request.StudentAdditionalInfoData;
			IList<DynamicData> studentAdditionalInfoData2;
			if (studentAdditionalInfoData == null)
			{
				studentAdditionalInfoData2 = null;
			}
			else
			{
				studentAdditionalInfoData2 = studentAdditionalInfoData.ToList<DynamicDataDTO>().ConvertAll<DynamicData>((DynamicDataDTO g) => g.ToDomainObject());
			}
			IList<AccommodationForTestDTO> instructorFormData = Request.InstructorFormData;
			IList<AccommodationForTest> instructorFormData2;
			if (instructorFormData == null)
			{
				instructorFormData2 = null;
			}
			else
			{
				instructorFormData2 = instructorFormData.ToList<AccommodationForTestDTO>().ConvertAll<AccommodationForTest>((AccommodationForTestDTO g) => g.ToDomainObject());
			}
			IList<ExamFileDTO> examFiles = Request.ExamFiles;
			IList<ExamFile> examFiles2;
			if (examFiles == null)
			{
				examFiles2 = null;
			}
			else
			{
				examFiles2 = examFiles.ToList<ExamFileDTO>().ConvertAll<ExamFile>((ExamFileDTO g) => g.ToDomainObject());
			}
			SittingDTO sitting = Request.Sitting;
			createTestResp.AppointmentId = testBookingManager2.CreateTest(test, studentAdditionalInfoData2, instructorFormData2, examFiles2, (sitting != null) ? sitting.ToDomainObject() : null);
			return createTestResp;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00019890 File Offset: 0x00017A90
		public LoadTestsByStudentResp LoadTestsByStudent(LoadTestsByStudentReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<Test> list = testBookingManager.LoadTestsByStudent(Request.PersonId, Request.StartDate, Request.EndDate, Request.HideCancelled);
			LoadTestsByStudentResp loadTestsByStudentResp = new LoadTestsByStudentResp();
			List<TestDTO> tests;
			if (list == null)
			{
				tests = null;
			}
			else
			{
				tests = list.ToList<Test>().ConvertAll<TestDTO>((Test f) => f.ToDTO());
			}
			loadTestsByStudentResp.Tests = tests;
			return loadTestsByStudentResp;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001990C File Offset: 0x00017B0C
		public LoadStudentsWritingExamResp LoadStudentsWritingExam(LoadStudentsWritingExamReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			IList<StudentWritingTest> list = testBookingManager.LoadStudentsWritingExam(Request.ExamId);
			LoadStudentsWritingExamResp loadStudentsWritingExamResp = new LoadStudentsWritingExamResp();
			List<StudentWritingTestDTO> studentsWritingTests;
			if (list == null)
			{
				studentsWritingTests = null;
			}
			else
			{
				studentsWritingTests = (from f in list
				select f.ToDTO()).ToList<StudentWritingTestDTO>();
			}
			loadStudentsWritingExamResp.StudentsWritingTests = studentsWritingTests;
			return loadStudentsWritingExamResp;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00019974 File Offset: 0x00017B74
		public LoadInstructorAcknowledgedStudentResp LoadInstructorAcknowledgedStudent(LoadInstructorAcknowledgedStudentReq Request)
		{
			ITestBookingManager testBookingManager = new TestBookingManager(Request.GetOperationContext());
			InstructorAcknowledgedStudent instructorAcknowledgedStudent = testBookingManager.LoadInstructorAcknowledgedStudent(Request.AppId);
			return new LoadInstructorAcknowledgedStudentResp
			{
				AcknowledgedInfo = ((instructorAcknowledgedStudent != null) ? instructorAcknowledgedStudent.ToDTO() : null)
			};
		}
	}
}

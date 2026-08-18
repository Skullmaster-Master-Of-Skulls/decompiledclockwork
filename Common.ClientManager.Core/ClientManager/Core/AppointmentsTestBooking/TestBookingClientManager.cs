using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeData;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.Appointments;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x02000090 RID: 144
	public class TestBookingClientManager : ITestBookingClientManager, IWebService
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0001704C File Offset: 0x0001524C
		public IList<TestDTO> LoadTests(DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			LoadTestsReq loadTestsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsReq>();
			loadTestsReq.StartDate = StartDate;
			loadTestsReq.EndDate = EndDate;
			loadTestsReq.HideCancelled = HideCancelled;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTests(loadTestsReq).Tests;
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00017094 File Offset: 0x00015294
		public void UpdateTest(TestForEdit2DTO Test, IList<DynamicDataDTO> StudentAdditionalInfoData, IList<AccommodationForTestDTO> InstructorFormData, IList<ExamFileDTO> ExamFiles, SittingDTO Sitting)
		{
			UpdateTestReq updateTestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestReq>();
			updateTestReq.Test = Test;
			updateTestReq.StudentAdditionalInfoData = StudentAdditionalInfoData;
			updateTestReq.InstructorFormData = InstructorFormData;
			updateTestReq.ExamFiles = ExamFiles;
			updateTestReq.Sitting = Sitting;
			ClientServiceFactory.GetClientInstance<ITestBooking>().UpdateTest(updateTestReq);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x000170E4 File Offset: 0x000152E4
		public IList<AccommodationForTestDTO> LoadTestAccommodations(int AppointmentId, int PersonId, int LuCourseId)
		{
			LoadTestAccommodationsReq loadTestAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestAccommodationsReq>();
			loadTestAccommodationsReq.AppointmentId = AppointmentId;
			loadTestAccommodationsReq.PersonId = PersonId;
			loadTestAccommodationsReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestAccommodations(loadTestAccommodationsReq).AccommodationsForTest;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001712C File Offset: 0x0001532C
		public TestDTO LoadTestByAppointmentId(int AppointmentId)
		{
			LoadTestByAppointmentIdReq loadTestByAppointmentIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestByAppointmentIdReq>();
			loadTestByAppointmentIdReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestByAppointmentId(loadTestByAppointmentIdReq).Test;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00017164 File Offset: 0x00015364
		public IList<MailMergeTestBookingDTO> LoadTestBookingMailMergeInfoByDate(DateTime Date, bool ExcludeCancelled, IList<int> AppTypeIdsToExclude)
		{
			LoadTestBookingMailMergeInfoByDateReq loadTestBookingMailMergeInfoByDateReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestBookingMailMergeInfoByDateReq>();
			loadTestBookingMailMergeInfoByDateReq.Date = Date;
			loadTestBookingMailMergeInfoByDateReq.ExcludeCancelled = ExcludeCancelled;
			loadTestBookingMailMergeInfoByDateReq.AppTypeIdsToExclude = AppTypeIdsToExclude;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestBookingMailMergeInfoByDate(loadTestBookingMailMergeInfoByDateReq).Items;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000171AC File Offset: 0x000153AC
		public void DeleteTest(int AppointmentId)
		{
			DeleteTestReq deleteTestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteTestReq>();
			deleteTestReq.AppointmentId = AppointmentId;
			ClientServiceFactory.GetClientInstance<ITestBooking>().DeleteTest(deleteTestReq);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000171DC File Offset: 0x000153DC
		public IList<TestDTO> LoadTestsByExamId(int ExamId)
		{
			LoadTestsByExamIdReq loadTestsByExamIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsByExamIdReq>();
			loadTestsByExamIdReq.ExamId = ExamId;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestsByExamId(loadTestsByExamIdReq).Tests;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00017214 File Offset: 0x00015414
		public IList<TestDTO> LoadTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			LoadTestsByAppointmentIdsReq loadTestsByAppointmentIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsByAppointmentIdsReq>();
			loadTestsByAppointmentIdsReq.AppointmentIds = AppointmentIds;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestsByAppointmentIds(loadTestsByAppointmentIdsReq).Tests;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001724C File Offset: 0x0001544C
		public IList<BasicTestDTO> LoadBasicTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			LoadBasicTestsByAppointmentIdsReq loadBasicTestsByAppointmentIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadBasicTestsByAppointmentIdsReq>();
			loadBasicTestsByAppointmentIdsReq.AppointmentIds = AppointmentIds;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadBasicTestsByAppointmentIds(loadBasicTestsByAppointmentIdsReq).Tests;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00017284 File Offset: 0x00015484
		public IList<ExamStatusDTO> LoadAllExamStatuses()
		{
			LoadAllExamStatusesReq request = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAllExamStatusesReq>();
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadAllExamStatuses(request).ExamStatuses;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x000172B4 File Offset: 0x000154B4
		public IList<AccommodationDataDTO> LoadAccommodationsByTest(int AppointmentId, out int PersonId, out int LuCourseId)
		{
			LoadAccommodationsByTestReq loadAccommodationsByTestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAccommodationsByTestReq>();
			loadAccommodationsByTestReq.AppointmentId = AppointmentId;
			LoadAccommodationsByTestResp loadAccommodationsByTestResp = ClientServiceFactory.GetClientInstance<ITestBooking>().LoadAccommodationsByTest(loadAccommodationsByTestReq);
			bool flag = loadAccommodationsByTestResp == null;
			IList<AccommodationDataDTO> result;
			if (flag)
			{
				PersonId = 0;
				LuCourseId = 0;
				result = null;
			}
			else
			{
				PersonId = loadAccommodationsByTestResp.PersonId;
				LuCourseId = loadAccommodationsByTestResp.LuCourseId;
				result = loadAccommodationsByTestResp.TestAccommodations;
			}
			return result;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x00017310 File Offset: 0x00015510
		public void LoadTestAndAllowedAccommodations(int AppointmentId, out IList<AccommodationDataDTO> AllowedAccommodations, out IList<AccommodationDataDTO> AccommodationsForTest, out int PersonId, out int LuCourseId)
		{
			LoadTestAndAllowedAccommodationsReq loadTestAndAllowedAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestAndAllowedAccommodationsReq>();
			loadTestAndAllowedAccommodationsReq.AppointmentId = AppointmentId;
			LoadTestAndAllowedAccommodationsResp loadTestAndAllowedAccommodationsResp = ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestAndAllowedAccommodations(loadTestAndAllowedAccommodationsReq);
			bool flag = loadTestAndAllowedAccommodationsResp == null;
			if (flag)
			{
				AllowedAccommodations = null;
				AccommodationsForTest = null;
				PersonId = 0;
				LuCourseId = 0;
			}
			else
			{
				AllowedAccommodations = loadTestAndAllowedAccommodationsResp.AllowedAccommodations;
				AccommodationsForTest = loadTestAndAllowedAccommodationsResp.TestAccommodations;
				PersonId = loadTestAndAllowedAccommodationsResp.PersonId;
				LuCourseId = loadTestAndAllowedAccommodationsResp.LuCourseId;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00017378 File Offset: 0x00015578
		public TestForEditDTO LoadTestForEditByAppointmentId(int AppointmentId)
		{
			LoadTestForEditByAppointmentIdReq loadTestForEditByAppointmentIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestForEditByAppointmentIdReq>();
			loadTestForEditByAppointmentIdReq.AppointmentId = AppointmentId;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestForEditByAppointmentId(loadTestForEditByAppointmentIdReq).TestForEdit;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000173B0 File Offset: 0x000155B0
		public void UpdateTestAccommodations(int AppointmentId, int PersonId, IList<int> cidsToAdd, IList<int> cidsToRemove)
		{
			UpdateTestAccommodationsReq updateTestAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestAccommodationsReq>();
			updateTestAccommodationsReq.AppointmentId = AppointmentId;
			updateTestAccommodationsReq.PersonId = PersonId;
			updateTestAccommodationsReq.ControlIdsToAdd = cidsToAdd;
			updateTestAccommodationsReq.ControlIdsToRemove = cidsToRemove;
			ClientServiceFactory.GetClientInstance<ITestBooking>().UpdateTestAccommodations(updateTestAccommodationsReq);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000173F8 File Offset: 0x000155F8
		public int CreateTest(TestForEdit2DTO Test, IList<DynamicDataDTO> StudentAdditionalInfoData, IList<AccommodationForTestDTO> InstructorFormData, IList<ExamFileDTO> ExamFiles, SittingDTO Sitting)
		{
			CreateTestReq createTestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateTestReq>();
			createTestReq.Test = Test;
			createTestReq.InstructorFormData = InstructorFormData;
			createTestReq.StudentAdditionalInfoData = StudentAdditionalInfoData;
			createTestReq.Sitting = Sitting;
			createTestReq.ExamFiles = ExamFiles;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().CreateTest(createTestReq).AppointmentId;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00017450 File Offset: 0x00015650
		public void CancelOrUncancelTestBooking(int AppointmentId, bool NewIsCancelled)
		{
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			if (NewIsCancelled)
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				appointmentClientManager.CancelAppointment(AppointmentId, new AppCancelInfoDTO
				{
					CancelledBy = (PersonBaseDTO)cacheStorageManager["cWhoAmI"],
					CancelledDate = DateTime.Now
				});
			}
			else
			{
				appointmentClientManager.UnCancelAppointment(AppointmentId);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000174AC File Offset: 0x000156AC
		public void ChangeTentativeStatus(int AppointmentId, bool NewIsTentative)
		{
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			if (NewIsTentative)
			{
				appointmentClientManager.MarkAppointmentTentative(AppointmentId);
			}
			else
			{
				appointmentClientManager.UnMarkAppointmentTentative(AppointmentId);
			}
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x000174D8 File Offset: 0x000156D8
		public void AddProctorToTest(int AppointmentId, int PersonId)
		{
			IAppointmentAttendeeClientManager appointmentAttendeeClientManager = new AppointmentAttendeeClientManager();
			appointmentAttendeeClientManager.InsertOrUpdateAppointmentAttendee(AppointmentId, new AttendeeDTO
			{
				IsNoShow = false,
				MiscCode = -1,
				Person = new PersonBaseDTO
				{
					PersonId = PersonId
				}
			});
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00017520 File Offset: 0x00015720
		public IList<StudentWritingTestDTO> LoadStudentsWritingExam(int examId)
		{
			LoadStudentsWritingExamReq loadStudentsWritingExamReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsWritingExamReq>();
			loadStudentsWritingExamReq.ExamId = examId;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadStudentsWritingExam(loadStudentsWritingExamReq).StudentsWritingTests;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00017558 File Offset: 0x00015758
		public InstructorAcknowledgedStudentDTO LoadInstructorAcknowledgedStudent(int appId)
		{
			LoadInstructorAcknowledgedStudentReq loadInstructorAcknowledgedStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadInstructorAcknowledgedStudentReq>();
			loadInstructorAcknowledgedStudentReq.AppId = appId;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadInstructorAcknowledgedStudent(loadInstructorAcknowledgedStudentReq).AcknowledgedInfo;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00017590 File Offset: 0x00015790
		public IList<TestDTO> LoadTestsByStudent(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			LoadTestsByStudentReq loadTestsByStudentReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadTestsByStudentReq>();
			loadTestsByStudentReq.PersonId = PersonId;
			loadTestsByStudentReq.StartDate = StartDate;
			loadTestsByStudentReq.EndDate = EndDate;
			loadTestsByStudentReq.HideCancelled = HideCancelled;
			return ClientServiceFactory.GetClientInstance<ITestBooking>().LoadTestsByStudent(loadTestsByStudentReq).Tests;
		}
	}
}

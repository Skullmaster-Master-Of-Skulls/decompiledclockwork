using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeData;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsTestBooking
{
	// Token: 0x0200007A RID: 122
	public class TestBookingRestClientManager : BearerTokenRestProxy<ITestBookingClientManager>, ITestBookingClientManager, IWebService
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
		public TestBookingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000D4BA File Offset: 0x0000B6BA
		public TestBookingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000D4C5 File Offset: 0x0000B6C5
		public IList<TestDTO> LoadTests(DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			return base.GetMany<TestDTO>(string.Format("testbooking/tests/range/{0}/{1}?hidecancelled={2}", StartDate, EndDate, HideCancelled), true);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000D4EA File Offset: 0x0000B6EA
		public IList<AccommodationForTestDTO> LoadTestAccommodations(int AppointmentId, int PersonId, int LuCourseId)
		{
			return base.GetMany<AccommodationForTestDTO>(string.Format("testbooking/testaccommodations/appid/{0}/pid/{1}/lucourseid/{2}", AppointmentId, PersonId, LuCourseId), true);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000D50F File Offset: 0x0000B70F
		public TestDTO LoadTestByAppointmentId(int AppointmentId)
		{
			return base.Get<TestDTO>(string.Format("testbooking/appid/{0}", AppointmentId), true);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000D528 File Offset: 0x0000B728
		public IList<MailMergeTestBookingDTO> LoadTestBookingMailMergeInfoByDate(DateTime Date, bool ExcludeCancelled, IList<int> AppTypeIdsToExclude)
		{
			return base.GetMany<MailMergeTestBookingDTO>(string.Format("testbooking/mailmergeinfo/date/{0}/apptypeidstoexclude/{1}?excludecancelled={2}", Date, AppTypeIdsToExclude.CommaSeparatedValuesWithoutSpace<int>(), ExcludeCancelled), true);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000D54D File Offset: 0x0000B74D
		public void DeleteTest(int AppointmentId)
		{
			base.Delete(string.Format("testbooking/appid/{0}", AppointmentId));
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000D565 File Offset: 0x0000B765
		public IList<TestDTO> LoadTestsByExamId(int ExamId)
		{
			return base.GetMany<TestDTO>(string.Format("testbooking/examid/{0}", ExamId), true);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000D57E File Offset: 0x0000B77E
		public IList<TestDTO> LoadTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			return base.GetMany<TestDTO>(string.Format("testbooking/appids/{0}", AppointmentIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000D597 File Offset: 0x0000B797
		public IList<BasicTestDTO> LoadBasicTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			return base.GetMany<BasicTestDTO>(string.Format("testbooking/basictests/appids/{0}", AppointmentIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
		public IList<ExamStatusDTO> LoadAllExamStatuses()
		{
			return base.GetMany<ExamStatusDTO>("testbooking/allexamstatus", true);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
		public IList<AccommodationDataDTO> LoadAccommodationsByTest(int AppointmentId, out int PersonId, out int LuCourseId)
		{
			LoadAccommodationsByTestResp loadAccommodationsByTestResp = base.Get<LoadAccommodationsByTestResp>(string.Format("testbooking/accommodations/appid/{0}", AppointmentId), true);
			if (loadAccommodationsByTestResp == null)
			{
				PersonId = 0;
				LuCourseId = 0;
				return null;
			}
			PersonId = loadAccommodationsByTestResp.PersonId;
			LuCourseId = loadAccommodationsByTestResp.LuCourseId;
			return loadAccommodationsByTestResp.TestAccommodations;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000D608 File Offset: 0x0000B808
		public void LoadTestAndAllowedAccommodations(int AppointmentId, out IList<AccommodationDataDTO> AllowedAccommodations, out IList<AccommodationDataDTO> AccommodationsForTest, out int PersonId, out int LuCourseId)
		{
			LoadTestAndAllowedAccommodationsResp loadTestAndAllowedAccommodationsResp = base.Get<LoadTestAndAllowedAccommodationsResp>(string.Format("testbooking/testandallowedaccommodations/appid/{0}", AppointmentId), true);
			if (loadTestAndAllowedAccommodationsResp == null)
			{
				AllowedAccommodations = null;
				AccommodationsForTest = null;
				PersonId = 0;
				LuCourseId = 0;
				return;
			}
			AllowedAccommodations = loadTestAndAllowedAccommodationsResp.AllowedAccommodations;
			AccommodationsForTest = loadTestAndAllowedAccommodationsResp.TestAccommodations;
			PersonId = loadTestAndAllowedAccommodationsResp.PersonId;
			LuCourseId = loadTestAndAllowedAccommodationsResp.LuCourseId;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000D661 File Offset: 0x0000B861
		public TestForEditDTO LoadTestForEditByAppointmentId(int AppointmentId)
		{
			return base.Get<TestForEditDTO>(string.Format("testbooking/testforedit/appid/{0}", AppointmentId), true);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000D67C File Offset: 0x0000B87C
		public void UpdateTestAccommodations(int AppointmentId, int PersonId, IList<int> cidsToAdd, IList<int> cidsToRemove)
		{
			UpdateTestAccommodationsReq updateTestAccommodationsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestAccommodationsReq>();
			updateTestAccommodationsReq.AppointmentId = AppointmentId;
			updateTestAccommodationsReq.PersonId = PersonId;
			updateTestAccommodationsReq.ControlIdsToAdd = cidsToAdd;
			updateTestAccommodationsReq.ControlIdsToRemove = cidsToRemove;
			base.Put<UpdateTestAccommodationsReq>(updateTestAccommodationsReq, "testbooking/testaccommodations");
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		public void UpdateTest(TestForEdit2DTO Test, IList<DynamicDataDTO> StudentAdditionalInfoData, IList<AccommodationForTestDTO> InstructorFormData, IList<ExamFileDTO> ExamFiles, SittingDTO Sitting)
		{
			UpdateTestReq updateTestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateTestReq>();
			updateTestReq.Test = Test;
			updateTestReq.StudentAdditionalInfoData = StudentAdditionalInfoData;
			updateTestReq.InstructorFormData = InstructorFormData;
			updateTestReq.ExamFiles = ExamFiles;
			updateTestReq.Sitting = Sitting;
			base.Put<UpdateTestReq>(updateTestReq, "testbooking");
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000D70C File Offset: 0x0000B90C
		public int CreateTest(TestForEdit2DTO Test, IList<DynamicDataDTO> StudentAdditionalInfoData, IList<AccommodationForTestDTO> InstructorFormData, IList<ExamFileDTO> ExamFiles, SittingDTO Sitting)
		{
			CreateTestReq createTestReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateTestReq>();
			createTestReq.Test = Test;
			createTestReq.InstructorFormData = InstructorFormData;
			createTestReq.StudentAdditionalInfoData = StudentAdditionalInfoData;
			createTestReq.Sitting = Sitting;
			createTestReq.ExamFiles = ExamFiles;
			return base.Post<CreateTestReq, int>(createTestReq, "testbooking");
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D758 File Offset: 0x0000B958
		public void CancelOrUncancelTestBooking(int AppointmentId, bool NewIsCancelled)
		{
			IAppointmentClientManager appointmentClientManager = ObjectFactory.Resolve<IAppointmentClientManager>();
			if (NewIsCancelled)
			{
				ICacheStorageManager cacheStorageManager = ObjectFactory.Resolve<ICacheStorageManager>();
				appointmentClientManager.CancelAppointment(AppointmentId, new AppCancelInfoDTO
				{
					CancelledBy = (PersonBaseDTO)cacheStorageManager["cWhoAmI"],
					CancelledDate = DateTime.Now
				});
				return;
			}
			appointmentClientManager.UnCancelAppointment(AppointmentId);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		public void ChangeTentativeStatus(int AppointmentId, bool NewIsTentative)
		{
			IAppointmentClientManager appointmentClientManager = ObjectFactory.Resolve<IAppointmentClientManager>();
			if (NewIsTentative)
			{
				appointmentClientManager.MarkAppointmentTentative(AppointmentId);
				return;
			}
			appointmentClientManager.UnMarkAppointmentTentative(AppointmentId);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D7D1 File Offset: 0x0000B9D1
		public void AddProctorToTest(int AppointmentId, int PersonId)
		{
			ObjectFactory.Resolve<IAppointmentAttendeeClientManager>().InsertOrUpdateAppointmentAttendee(AppointmentId, new AttendeeDTO
			{
				IsNoShow = false,
				MiscCode = -1,
				Person = new PersonBaseDTO
				{
					PersonId = PersonId
				}
			});
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000D804 File Offset: 0x0000BA04
		public IList<StudentWritingTestDTO> LoadStudentsWritingExam(int examId)
		{
			return base.GetMany<StudentWritingTestDTO>(string.Format("testbooking/studentwritingexam/examid/{0}", examId), true);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClockWorkLogger;
using TechnoPro.Common.Core.AppointmentLog;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Appointments;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentLog;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeData;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.TextFormat.Adapters;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x02000145 RID: 325
	public class TestBookingManager : ITestBookingManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0006BCDA File Offset: 0x00069EDA
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x0006BCE2 File Offset: 0x00069EE2
		public ITestBookingDAO dao { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0006BCEC File Offset: 0x00069EEC
		private DynamicDataManager dynamicDataManager
		{
			get
			{
				DynamicDataManager result;
				if ((result = this._dynamicDataManager) == null)
				{
					result = (this._dynamicDataManager = new DynamicDataManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0006BD17 File Offset: 0x00069F17
		public TestBookingManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new TestBookingDAO(opContext);
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0006BD36 File Offset: 0x00069F36
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x0006BD3E File Offset: 0x00069F3E
		public OperationContext OpContext { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0006BD48 File Offset: 0x00069F48
		private IAppointmentLogDAO appLogDao
		{
			get
			{
				bool flag = this._appLogDao == null;
				if (flag)
				{
					this._appLogDao = new AppointmentLogDAO(this.OpContext);
				}
				return this._appLogDao;
			}
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x0006BD80 File Offset: 0x00069F80
		public IList<Test> LoadTests(DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			return this.dao.LoadTests(StartDate, EndDate, HideCancelled);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0006BDA0 File Offset: 0x00069FA0
		public List<AccommodationForTest> LoadTestAccommodations(int AppointmentId, int PersonId, int LuCourseId)
		{
			return this.dao.LoadTestAccommodations(AppointmentId, PersonId, LuCourseId);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0006BDC0 File Offset: 0x00069FC0
		public Test LoadTestByAppointmentId(int AppointmentId)
		{
			return this.dao.LoadTestById(AppointmentId);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x0006BDE0 File Offset: 0x00069FE0
		public IList<AccommodationData> LoadAccommodationsByTest(int AppointmentId, out int PersonId, out int LuCourseId)
		{
			return this.dao.LoadAccommodationsByTest(AppointmentId, out PersonId, out LuCourseId);
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x0006BE00 File Offset: 0x0006A000
		public void LoadTestAndAllowedAccommodations(int AppointmentId, out IList<AccommodationData> AllowedAccommodations, out IList<AccommodationData> AccommodationsForTest, out int PersonId, out int LuCourseId)
		{
			AccommodationsForTest = this.LoadAccommodationsByTest(AppointmentId, out PersonId, out LuCourseId);
			IAccommodationsManager accommodationsManager = new AccommodationsManager(this.OpContext);
			IList<AccommodationData> list2;
			if (PersonId <= 0 || LuCourseId <= 0)
			{
				IList<AccommodationData> list = new List<AccommodationData>();
				list2 = list;
			}
			else
			{
				list2 = accommodationsManager.LoadAccommodationsByStudentAndCourseOrTemplate(PersonId, LuCourseId);
			}
			IList<AccommodationData> source = list2;
			AllowedAccommodations = (from g in source
			where g.Detail != null && (g.Detail.Group & eAccommodationGroup.TestExam) > eAccommodationGroup.None
			select g).ToList<AccommodationData>();
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x0006BE78 File Offset: 0x0006A078
		public void UpdateInstructorFormData(int ExamId, IList<AccommodationForTest> NewData)
		{
			string instanceName = "ClockWork";
			ISettingManager settingManager = new SettingManager(instanceName, this.OpContext);
			int settingValue = settingManager.GetSettingValue<int>(Setting.INSTRUCTOR_uploadscreennum);
			List<DynamicData> list;
			if (NewData != null)
			{
				list = NewData.ToList<AccommodationForTest>().ConvertAll<DynamicData>((AccommodationForTest g) => g.DynamicFieldData);
			}
			else
			{
				list = new List<DynamicData>();
			}
			List<DynamicData> data = list;
			DynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			this.dynamicDataManager.SaveData(new DynamicDataContext
			{
				PrimaryId = ExamId
			}, data, eDynamicFormType.PerInstructor);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0006BF04 File Offset: 0x0006A104
		public IList<DynamicData> LoadInstructorFormData(int ExamId)
		{
			string instanceName = "ClockWork";
			ISettingManager settingManager = new SettingManager(instanceName, this.OpContext);
			int settingValue = settingManager.GetSettingValue<int>(Setting.INSTRUCTOR_uploadscreennum);
			DynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			return this.dynamicDataManager.LoadData(new DynamicDataContext
			{
				PrimaryId = ExamId
			}, new DynamicForm
			{
				FormType = eDynamicFormType.PerInstructor,
				ScreenNum = settingValue
			});
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x0006BF78 File Offset: 0x0006A178
		public IList<MailMergeTestBooking> LoadTestBookingMailMergeInfoByDate(DateTime Date, bool ExcludeCancelled, IList<int> AppTypeIdsToExclude)
		{
			return this.dao.LoadTestBookingMailMergeInfoByDate(Date, ExcludeCancelled, AppTypeIdsToExclude);
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0006BF98 File Offset: 0x0006A198
		public void DeleteTest(bool runInTransaction, int AppointmentId)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(this.OpContext);
			baseAppointmentManager.DeleteAppointment(runInTransaction, AppointmentId);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<Test> LoadTestsBySittingId(int SittingId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0006BFBC File Offset: 0x0006A1BC
		public IList<Test> LoadTestsByExamId(int ExamId)
		{
			return this.dao.LoadClassTestDefinitionBookings(ExamId);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0006BFDC File Offset: 0x0006A1DC
		public IList<StudentWritingTest> LoadStudentsWritingExam(int examId)
		{
			return this.dao.LoadStudentsWritingExam(examId);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000072EA File Offset: 0x000054EA
		public IList<TestBase> LoadTestBasesByExamId(int ExamId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0006BFFC File Offset: 0x0006A1FC
		public IList<Test> LoadTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			return this.dao.LoadTestsByAppointmentIds(AppointmentIds);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0006C01C File Offset: 0x0006A21C
		public IList<BasicTest> LoadBasicTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			return this.dao.LoadBasicTestsByAppointmentIds(AppointmentIds);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0006C03C File Offset: 0x0006A23C
		public IList<ExamStatus> LoadAllExamStatuses()
		{
			return this.dao.LoadAllExamStatuses();
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0006C05C File Offset: 0x0006A25C
		public TestForEdit LoadTestForEditByAppointmentId(int AppointmentId)
		{
			TestForEdit testForEdit = this.dao.LoadTestForEditById(AppointmentId);
			string text;
			if (testForEdit == null)
			{
				text = null;
			}
			else
			{
				Test test = testForEdit.Test;
				text = ((test != null) ? test.Memo : null);
			}
			string text2 = text;
			bool flag = string.IsNullOrWhiteSpace(text2) || TestBookingManager.IsRtf(text2);
			TestForEdit result;
			if (flag)
			{
				result = testForEdit;
			}
			else
			{
				testForEdit.Test.Memo = text2.ConvertPlainTextToRtf();
				result = testForEdit;
			}
			return result;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0006C0C0 File Offset: 0x0006A2C0
		private static bool IsRtf(string text)
		{
			bool flag = string.IsNullOrWhiteSpace(text);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = text.StartsWith("{\\rtf");
				result = flag2;
			}
			return result;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0006C0F4 File Offset: 0x0006A2F4
		public void UpdateTestAccommodations(int AppointmentId, int PersonId, IList<int> cidsToAdd, IList<int> cidsToRemove)
		{
			bool flag = cidsToAdd != null && cidsToAdd.Count > 0;
			if (flag)
			{
				this.dao.AddTestAccommodations(AppointmentId, PersonId, cidsToAdd);
			}
			bool flag2 = cidsToRemove != null && cidsToRemove.Count > 0;
			if (flag2)
			{
				this.dao.RemoveTestAccommodations(AppointmentId, PersonId, cidsToRemove);
			}
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0006C14C File Offset: 0x0006A34C
		public InstructorAcknowledgedStudent LoadInstructorAcknowledgedStudent(int appId)
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			string settingValue = webSettingManager.GetSettingValue<string>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_AcknowledgeMessage);
			string settingValue2 = webSettingManager.GetSettingValue<string>(Setting.INSTRUCTOR_InstructorMustAcknowledgeReceiptOfExamRequests_QuestionsMessage);
			Dictionary<int, string> acknowledgeValueTitles = new Dictionary<int, string>
			{
				{
					0,
					settingValue2
				},
				{
					1,
					settingValue
				}
			};
			return this.dao.LoadInstructorAcknowledgedStudent(appId, acknowledgeValueTitles);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0006C1B8 File Offset: 0x0006A3B8
		public void UpdateTest(TestForEdit2 Test, IList<DynamicData> StudentAdditionalInfoData, IList<AccommodationForTest> InstructorFormData, IList<ExamFile> ExamFiles, Sitting Sitting)
		{
			PersonBase firstStudent = Test.GetFirstStudent();
			bool flag = firstStudent == null || firstStudent.PersonId < 1;
			if (flag)
			{
				CWLogger.Logger.Error("TestBookingManager:UpdateTest:Missing student; can't update test");
			}
			bool flag2 = Test.AppointmentId < 1;
			if (flag2)
			{
				CWLogger.Logger.Error("TestBookingManager:UpdateTest:Missing AppointmentId; can't update test");
			}
			this.appLogDao.LogAppModificationsPreChangeCommitted(Test.AppointmentId);
			this.UpdateBreakTime(Test.AppointmentId, Test.BreakTimeMinutes);
			IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
			baseAppointmentDAO.UpdateBaseExtendedAppointment(Test, null);
			Task.Run(delegate()
			{
				IAppointmentLogManager appointmentLogManager = new AppointmentLogManager(this.OpContext);
				appointmentLogManager.LogAppModifications(Test.AppointmentId, eAppointmentModifiedItemType.TestInfo);
			});
			this.dao.UpdateClassTestDefinitionSpecific(Test.ExamId, Test.ClassTestDefinitionSpecificInfo);
			this.dao.UpdateTestBookingSpecific(Test.AppointmentId, Test.BookingSpecificInfo);
			this.UpdateTestAccommodations(Test.AppointmentId, firstStudent.PersonId, Test.BookingSpecificInfo.AccommodationCids, new List<int>());
			bool flag3 = InstructorFormData != null;
			if (flag3)
			{
				this.UpdateInstructorFormData(Test.ExamId, InstructorFormData);
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0006C320 File Offset: 0x0006A520
		public int CreateTest(TestForEdit2 Test, IList<DynamicData> StudentAdditionalInfoData, IList<AccommodationForTest> InstructorFormData, IList<ExamFile> ExamFiles, Sitting Sitting)
		{
			bool flag = Test.ExamId < 1;
			int result;
			if (flag)
			{
				CWLogger.Logger.Error("TestBookingManager:CreateTest:Missing exam id; unable to create test");
				result = 0;
			}
			else
			{
				ClassTestBase classTestBase = null;
				bool flag2 = Test.LuCourseId < 1 || Test.ClassTestDefinitionSpecificInfo == null;
				if (flag2)
				{
					IClassTestDefinitionManager classTestDefinitionManager = new ClassTestDefinitionManager(this.OpContext);
					classTestBase = classTestDefinitionManager.LoadClassTestBaseById(Test.ExamId);
				}
				bool flag3 = Test.LuCourseId < 1;
				if (flag3)
				{
					bool flag4 = classTestBase != null && classTestBase.Course != null;
					if (flag4)
					{
						Test.LuCourseId = classTestBase.Course.LuCourseId;
					}
					bool flag5 = Test.LuCourseId < 1;
					if (flag5)
					{
						CWLogger.Logger.Error("TestBookingManager:CreateTest:CourseIsMissing; unable to create test");
						return 0;
					}
				}
				bool flag6 = classTestBase != null;
				if (flag6)
				{
					Test.BookingSpecificInfo.StudentReportedClassStartTime = new DateTime?(classTestBase.StartDateTime);
					Test.BookingSpecificInfo.StudentReportedClassEndTime = new DateTime?(classTestBase.EndDateTime);
				}
				PersonBase firstStudent = Test.GetFirstStudent();
				bool flag7 = firstStudent == null || firstStudent.PersonId < 1;
				if (flag7)
				{
					CWLogger.Logger.Error("TestBookingManager:CreateTest:Missing student; can't create test");
					result = 0;
				}
				else
				{
					this.dao.UpdateClassTestDefinitionSpecific(Test.ExamId, Test.ClassTestDefinitionSpecificInfo);
					IBaseAppointmentDAO baseAppointmentDAO = new BaseAppointmentDAO(this.OpContext);
					int num = baseAppointmentDAO.CreateBaseExtendedAppointment(Test, null);
					bool flag8 = num < 1;
					if (flag8)
					{
						CWLogger.Logger.Error("TestBookingManager:CreateTest:Unable to create test appointment");
						result = 0;
					}
					else
					{
						Test.AppointmentId = num;
						this.dao.SetAppointmentExamId(num, Test.ExamId);
						this.dao.CreateTestBookingSpecific(num, Test.LuCourseId, Test.BookingSpecificInfo);
						this.UpdateBreakTime(num, Test.BreakTimeMinutes);
						this.UpdateTestAccommodations(num, firstStudent.PersonId, Test.BookingSpecificInfo.AccommodationCids, new List<int>());
						bool flag9 = InstructorFormData != null;
						if (flag9)
						{
							this.UpdateInstructorFormData(Test.ExamId, InstructorFormData);
						}
						bool flag10 = ExamFiles != null;
						if (flag10)
						{
						}
						bool flag11 = StudentAdditionalInfoData != null;
						if (flag11)
						{
						}
						result = num;
					}
				}
			}
			return result;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0006C547 File Offset: 0x0006A747
		public void UpdateBreakTime(int AppointmentId, int BreakTimeMinutes)
		{
			this.dao.UpdateBreakTime(AppointmentId, BreakTimeMinutes);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0006C558 File Offset: 0x0006A758
		public IList<Test> LoadTestsByStudent(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			return this.dao.LoadTestsByStudent(PersonId, StartDate, EndDate, HideCancelled);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x0006C57C File Offset: 0x0006A77C
		public IList<int> LoadAppointmentIdsByExamId(int ExamId)
		{
			return this.dao.LoadAppointmentIdsByExamId(ExamId);
		}

		// Token: 0x040002A9 RID: 681
		private DynamicDataManager _dynamicDataManager;

		// Token: 0x040002AB RID: 683
		private IAppointmentLogDAO _appLogDao;
	}
}

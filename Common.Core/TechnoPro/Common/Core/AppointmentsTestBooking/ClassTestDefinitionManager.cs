using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.UserSettingsPermissions;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.AppointmentsTestBooking;
using TechnoPro.Common.ICore.AppointmentsTestBooking;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.UserSettingsPermissions;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions.OldUserSettings;

namespace TechnoPro.Common.Core.AppointmentsTestBooking
{
	// Token: 0x0200013C RID: 316
	public class ClassTestDefinitionManager : IClassTestDefinitionManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000DEE RID: 3566 RVA: 0x00069621 File Offset: 0x00067821
		public ClassTestDefinitionManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ClassTestDefinitionDAO(opContext);
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0006963F File Offset: 0x0006783F
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x00069647 File Offset: 0x00067847
		public OperationContext OpContext { get; set; }

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00069650 File Offset: 0x00067850
		public int CreateClassTestDefinition(ClassTest ClassTestDefinition)
		{
			return this.dao.CreateClassTestDefinition(ClassTestDefinition);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0006966E File Offset: 0x0006786E
		public void DeleteClassTestDefinition(int ExamId)
		{
			this.dao.DeleteClassTestDefinition(ExamId);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0006967E File Offset: 0x0006787E
		public void UpdateClassTestDefinition(ClassTest ClassTestDefinition)
		{
			this.dao.UpdateClassTestDefinition(ClassTestDefinition);
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0006968E File Offset: 0x0006788E
		public void RemoveInstructorHasSubmittedInformationAboutThisTestMarker(int examId)
		{
			this.dao.RemoveInstructorHasSubmittedInformationAboutThisTestMarker(examId);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0006969E File Offset: 0x0006789E
		public void MarkTestDelivered(int ExamId, string TestDeliveredMessage)
		{
			this.dao.MarkTestDelivered(ExamId, TestDeliveredMessage);
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x000696B0 File Offset: 0x000678B0
		public IList<ClassTest> LoadClassTestDefinitionsByCourse(int LuCourseId)
		{
			return this.dao.LoadClassTestDefinitionsByCourse(LuCourseId, eClassTestType.Unknown);
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000696D0 File Offset: 0x000678D0
		public ClassTest LoadClassTestDefinitionByAppointmentId(int AppointmentId)
		{
			return this.dao.LoadClassTestDefinitionByAppointmentId(AppointmentId);
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x000696F0 File Offset: 0x000678F0
		public ClassTest LoadClassTestDefinitionById(int ExamId)
		{
			return this.dao.LoadClassTestDefinitionById(ExamId);
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00069710 File Offset: 0x00067910
		public ClassTest LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(int ExamId, int InstructorId, int AlternateContactId)
		{
			return this.dao.LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(ExamId, InstructorId, AlternateContactId);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00069730 File Offset: 0x00067930
		public int CreateClassTestDefinitionBase(ClassTestBase ClassTestBase)
		{
			return this.dao.CreateClassTestDefinitionBase(ClassTestBase);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0006974E File Offset: 0x0006794E
		public void UpdateClassTestDefinitionBase(ClassTestBase ClassTestBase)
		{
			this.dao.UpdateClassTestDefinitionBase(ClassTestBase);
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00069760 File Offset: 0x00067960
		public ClassTestBase LoadClassTestBaseById(int ExamId)
		{
			return this.dao.LoadClassTestBaseById(ExamId);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x00069780 File Offset: 0x00067980
		public ClassTestForEdit LoadClassTestForEditById(int ExamId)
		{
			ClassTest classTest = this.LoadClassTestDefinitionById(ExamId);
			return new ClassTestForEdit
			{
				ClassTest = classTest
			};
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x000697A8 File Offset: 0x000679A8
		public void UpdateInstructorSubmittedTestInfo(int ExamId, int InstructorId)
		{
			bool flag = InstructorId > 0;
			if (flag)
			{
				this.dao.SetInstructorLastModified(ExamId, InstructorId);
			}
			else
			{
				this.dao.ClearInstructorLastModified(ExamId);
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000697DB File Offset: 0x000679DB
		public void UpdateInstructorContactedInfo(int ExamId, DateTime? InstructorContactedDate, string Note)
		{
			this.dao.UpdateInstructorContactedInfo(ExamId, InstructorContactedDate, Note);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000697ED File Offset: 0x000679ED
		public void UpdateTestPickedUp(int ExamId, DateTime? DatePickedUp, string Note)
		{
			this.dao.UpdateTestPickedUp(ExamId, DatePickedUp, Note);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x00069800 File Offset: 0x00067A00
		public ClassTestForExamRequest LoadClassTestForExamRequestById(int ExamId)
		{
			return this.dao.LoadClassTestForExamRequestById(ExamId);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00069820 File Offset: 0x00067A20
		public IList<ClassTestForExamRequest> LoadClassTestsForExamRequestByDateRange(int LuCourseId, DateTime StartDate, DateTime EndDate, eClassTestType testType = eClassTestType.Unknown)
		{
			return this.dao.LoadClassTestsForExamRequestByDateRange(LuCourseId, StartDate, EndDate, testType);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00069844 File Offset: 0x00067A44
		public IList<ClassTestForDisplay> LoadClassTestsForDisplay(DateTime StartDate, DateTime EndDate)
		{
			IOldUserSettingManager oldUserSettingManager = new OldUserSettingManager(this.OpContext);
			string settingValue_String = oldUserSettingManager.GetSettingValue_String(this.OpContext.WhoAmI, eSettingCode.SETTING_Tests_InstructorFormCidsToShowInMasterList, false);
			List<int> list;
			if (!string.IsNullOrEmpty(settingValue_String))
			{
				list = (from n in (from g in settingValue_String.Split(new char[]
				{
					','
				})
				select g.Trim() into h
				where h.Length > 0
				select h).Select(delegate(string m)
				{
					int result2;
					int.TryParse(m, out result2);
					return result2;
				}).Distinct<int>()
				where n > 0
				select n).ToList<int>();
			}
			else
			{
				list = new List<int>();
			}
			List<int> list2 = list;
			IList<ClassTestForDisplay> list3 = this.dao.LoadClassTestsForDisplayWithoutInstructorFormData(StartDate, EndDate);
			bool flag = list2.Count < 1;
			IList<ClassTestForDisplay> result;
			if (flag)
			{
				result = list3;
			}
			else
			{
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				IList<DynamicDataSet> list4 = dynamicDataManager.LoadInstructorFormDataForMultipleExams((from g in list3
				select g.ExamId).ToList<int>(), list2);
				using (IEnumerator<DynamicDataSet> enumerator = list4.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						DynamicDataSet dynamicDataSet = enumerator.Current;
						ClassTestForDisplay classTestForDisplay = list3.FirstOrDefault((ClassTestForDisplay g) => g.ExamId == dynamicDataSet.Context.SecondaryId);
						bool flag2 = classTestForDisplay == null;
						if (!flag2)
						{
							classTestForDisplay.InstructorFormData = dynamicDataSet.Data;
						}
					}
				}
				result = list3;
			}
			return result;
		}

		// Token: 0x04000296 RID: 662
		private IClassTestDefinitionDAO dao;
	}
}

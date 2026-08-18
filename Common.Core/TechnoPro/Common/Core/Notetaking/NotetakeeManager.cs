using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Settings;
using TechnoPro.Common.DAO.Impl.Notetaking;
using TechnoPro.Common.DAO.Notetaking;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Notetaking;
using TechnoPro.Common.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Info;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Rules;
using TechnoPro.Common.Public.Entities.Notetaking.Notetakee.Status;
using TechnoPro.Common.Public.Entities.OperationContexts;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.Core.Notetaking
{
	// Token: 0x020000AD RID: 173
	public class NotetakeeManager : INotetakeeManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000669 RID: 1641 RVA: 0x0002581B File Offset: 0x00023A1B
		public NotetakeeManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0002582D File Offset: 0x00023A2D
		// (set) Token: 0x0600066B RID: 1643 RVA: 0x00025835 File Offset: 0x00023A35
		public OperationContext OpContext { get; set; }

		// Token: 0x0600066C RID: 1644 RVA: 0x00025840 File Offset: 0x00023A40
		private NotetakeeCourseRegistrationStudentRules GetRules()
		{
			IWebSettingManager webSettingManager = new WebSettingManager(new SettingsOperationContext(this.OpContext));
			return new NotetakeeCourseRegistrationStudentRules
			{
				AllowStudentsToChooseTheirOwnNotetakers = webSettingManager.GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToChooseTheirOwnNotetakers),
				AllowedStudentToCancelAssignedNotetaker = webSettingManager.GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToCancelNotetaker),
				AllowedToViewNotesEvenIfNoNotetakerIsAssigned = webSettingManager.GetSettingValue<bool>(Setting.NOTETAKINGB_AllowStudentsToAccessNotesEvenIfTheyDontHaveAnAssignedNotetaker),
				EquivalentCoursesNum = webSettingManager.GetSettingValue<int>(Setting.NOTETAKINGB_EquivalentCourseStoredProcedureNumber),
				NotetakerApprovedForAllCoursesCid = webSettingManager.GetSettingValue<int>(Setting.NOTETAKINGB_NotetakerApprovedForAllCoursesCid),
				TreatEmptyExpiryDateAsExpired = webSettingManager.GetSettingValue<bool>(Setting.TESTBOOKING_AccommodationsTreatEmptyExpiryDateAsExpired),
				AccommodationsExpiryDateCid = webSettingManager.GetSettingValue<int>(Setting.TESTBOOKING_AccommodationsExpiryDateCid),
				RestrictAccessBaseOnLoa = true,
				RestrictAccessBasedOnSelfReg = true
			};
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x000258F8 File Offset: 0x00023AF8
		private NotetakeeCourseRegistrationStudentInfo LoadStudentInfo(int NotetakeePersonId, NotetakeeCourseRegistrationStudentRules rules)
		{
			bool flag = rules.NotetakerApprovedForAllCoursesCid > 0;
			bool notetakerIsAlwaysApprovedForAllNewCourses;
			if (flag)
			{
				IDynamicDataManager dynamicDataManager = (rules.NotetakerApprovedForAllCoursesCid > 0) ? new DynamicDataManager(this.OpContext) : null;
				IList<IDynamicDataSerializableItem> list = (dynamicDataManager != null) ? dynamicDataManager.LoadDynamicDataItemsByControlIds(new DynamicDataContext
				{
					PrimaryId = NotetakeePersonId,
					SecondaryId = 0
				}, new List<int>
				{
					rules.AccommodationsExpiryDateCid
				}, eDynamicFormType.AccommodationTemplateOnly) : null;
				notetakerIsAlwaysApprovedForAllNewCourses = (list != null && list.Count > 0);
			}
			else
			{
				notetakerIsAlwaysApprovedForAllNewCourses = false;
			}
			return new NotetakeeCourseRegistrationStudentInfo
			{
				AccommodationsExpiryDate = ((rules.AccommodationsExpiryDateCid > 0) ? new AccommodationsManager(this.OpContext).GetStudentAccommodationsExpiryDate(NotetakeePersonId) : null),
				NotetakerIsAlwaysApprovedForAllNewCourses = notetakerIsAlwaysApprovedForAllNewCourses
			};
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x000259B8 File Offset: 0x00023BB8
		private void CalculateCourseStatuses(ref IList<NotetakeeCourseRegistration> courses, NotetakeeCourseRegistrationStudentRules rules, NotetakeeCourseRegistrationStudentInfo studentInfo)
		{
			foreach (NotetakeeCourseRegistration notetakeeCourseRegistration in courses)
			{
				bool flag = rules.RestrictAccessBasedOnSelfReg && notetakeeCourseRegistration.CourseInfo.SelfRegistrationRequestId < 1;
				bool flag2 = rules.RestrictAccessBaseOnLoa && notetakeeCourseRegistration.CourseInfo.DateLetterIssued == null;
				NotetakeeCourseRegistration notetakeeCourseRegistration2 = notetakeeCourseRegistration;
				NotetakeeCourseRegistrationStudentCourseStatus courseStatus;
				if (!flag && !flag2)
				{
					NotetakeeCourseRegistrationStudentCourseStatus notetakeeCourseRegistrationStudentCourseStatus = new NotetakeeCourseRegistrationStudentCourseStatus();
					notetakeeCourseRegistrationStudentCourseStatus.RequiresApprovedSelfRegistrationRequest = false;
					notetakeeCourseRegistrationStudentCourseStatus.RequiresLoaGeneration = false;
					notetakeeCourseRegistrationStudentCourseStatus.AllowedToCancelNotetaker = rules.AllowedStudentToCancelAssignedNotetaker;
					notetakeeCourseRegistrationStudentCourseStatus.AllowedToAutoCreateServiceProviderRequest = studentInfo.NotetakerIsAlwaysApprovedForAllNewCourses;
					notetakeeCourseRegistrationStudentCourseStatus.AllowedToViewExistingNotes = (rules.AllowedToViewNotesEvenIfNoNotetakerIsAssigned || notetakeeCourseRegistration.CourseInfo.AssignedProviderId > 0);
					courseStatus = notetakeeCourseRegistrationStudentCourseStatus;
					notetakeeCourseRegistrationStudentCourseStatus.AllowedToSelectNotetaker = rules.AllowStudentsToChooseTheirOwnNotetakers;
				}
				else
				{
					NotetakeeCourseRegistrationStudentCourseStatus notetakeeCourseRegistrationStudentCourseStatus2 = new NotetakeeCourseRegistrationStudentCourseStatus();
					notetakeeCourseRegistrationStudentCourseStatus2.RequiresApprovedSelfRegistrationRequest = flag;
					notetakeeCourseRegistrationStudentCourseStatus2.RequiresLoaGeneration = flag2;
					notetakeeCourseRegistrationStudentCourseStatus2.AllowedToCancelNotetaker = false;
					notetakeeCourseRegistrationStudentCourseStatus2.AllowedToAutoCreateServiceProviderRequest = false;
					notetakeeCourseRegistrationStudentCourseStatus2.AllowedToViewExistingNotes = false;
					courseStatus = notetakeeCourseRegistrationStudentCourseStatus2;
					notetakeeCourseRegistrationStudentCourseStatus2.AllowedToSelectNotetaker = false;
				}
				notetakeeCourseRegistration2.CourseStatus = courseStatus;
			}
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00025AE4 File Offset: 0x00023CE4
		private NotetakeeCourseRegistrationStudentStatus CalculateStudentStatus(int NotetakeePersonId, NotetakeeCourseRegistrationStudentRules rules, NotetakeeCourseRegistrationStudentInfo studentInfo)
		{
			return new NotetakeeCourseRegistrationStudentStatus
			{
				RequiresAccommodationExpiryDateExtension = (rules.AccommodationsExpiryDateCid > 0 && ((studentInfo.AccommodationsExpiryDate != null) ? (studentInfo.AccommodationsExpiryDate.Value < DateTime.Now) : rules.TreatEmptyExpiryDateAsExpired))
			};
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00025B40 File Offset: 0x00023D40
		public NotetakeeStudentCourseRegistrations LoadNotetakeeCourseRegistrations(int NotetakeePersonId, DateTime StartDate, DateTime EndDate)
		{
			NotetakeeCourseRegistrationStudentRules rules = this.GetRules();
			NotetakeeCourseRegistrationStudentInfo studentInfo = this.LoadStudentInfo(NotetakeePersonId, rules);
			INotetakeeDAO notetakeeDAO = new NotetakeeDAO(this.OpContext);
			IList<NotetakeeCourseRegistration> list = notetakeeDAO.LoadNotetakeeCourseRegistrations(NotetakeePersonId, StartDate, EndDate, rules.RestrictAccessBasedOnSelfReg, false);
			this.CalculateCourseStatuses(ref list, rules, studentInfo);
			List<int> lucids = (from g in list
			where g.CourseStatus.AllowedToSelectNotetaker
			select g into h
			select h.CourseBase.LuCourseId).Distinct<int>().ToList<int>();
			IList<int> list2 = notetakeeDAO.FindLuCourseidsWhereAtLeastOneNotetakerIsAvailable(rules.EquivalentCoursesNum, lucids);
			foreach (NotetakeeCourseRegistration notetakeeCourseRegistration in from g in list
			where g.CourseStatus.AllowedToSelectNotetaker
			select g)
			{
				bool flag = list2.Contains(notetakeeCourseRegistration.CourseBase.LuCourseId);
				if (flag)
				{
					notetakeeCourseRegistration.CourseStatus.HasAtLeastOnePotentialNotetakerAvailable = true;
				}
				else
				{
					notetakeeCourseRegistration.CourseStatus.AllowedToSelectNotetaker = false;
				}
			}
			return new NotetakeeStudentCourseRegistrations
			{
				StudentPersonId = NotetakeePersonId,
				CourseRegistrations = list,
				StudentStatus = this.CalculateStudentStatus(NotetakeePersonId, rules, studentInfo)
			};
		}
	}
}

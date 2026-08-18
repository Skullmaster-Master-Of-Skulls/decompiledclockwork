using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.Core.Settings;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x0200003F RID: 63
	public class LookupCourseClientManager : ILookupCourseClientManager, IWebService
	{
		// Token: 0x06000249 RID: 585 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public int CreateLookupCourse(LookupCourseDTO course)
		{
			CreateLookupCourseReq createLookupCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateLookupCourseReq>();
			createLookupCourseReq.Course = course;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().CreateLookupCourse(createLookupCourseReq).LuCourseId;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AE24 File Offset: 0x00009024
		public IList<LookupCourseBaseDTO> LoadCourseBasesBySearchString(DateTime StartDate, DateTime EndDate, string SearchString)
		{
			LoadCourseBasesBySearchStringReq loadCourseBasesBySearchStringReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCourseBasesBySearchStringReq>();
			loadCourseBasesBySearchStringReq.StartDate = StartDate;
			loadCourseBasesBySearchStringReq.EndDate = EndDate;
			loadCourseBasesBySearchStringReq.SearchString = SearchString;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadCourseBasesBySearchString(loadCourseBasesBySearchStringReq).CourseBases;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000AE6C File Offset: 0x0000906C
		public IList<LookupCourseDTO> LoadCoursesBySubjectAndSession(SessionDTO Session, int SubjectId)
		{
			LoadCoursesBySubjectAndSessionReq loadCoursesBySubjectAndSessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCoursesBySubjectAndSessionReq>();
			loadCoursesBySubjectAndSessionReq.Session = Session;
			loadCoursesBySubjectAndSessionReq.SubjectId = SubjectId;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadCoursesBySubjectAndSession(loadCoursesBySubjectAndSessionReq).Courses;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000AEAC File Offset: 0x000090AC
		public LookupCourseDTO CreateLookupCourseBase(LookupCourseBaseDTO CourseBase)
		{
			CreateLookupCourseBaseReq createLookupCourseBaseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateLookupCourseBaseReq>();
			createLookupCourseBaseReq.CourseBase = CourseBase;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().CreateLookupCourseBase(createLookupCourseBaseReq).NewCourse;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public void UpdateCourseInstructorExemption(int LuCourseId, int InstructorId, bool NewIsInstructorExemptFromCourseList)
		{
			UpdateCourseInstructorExemptionReq updateCourseInstructorExemptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCourseInstructorExemptionReq>();
			updateCourseInstructorExemptionReq.LuCourseId = LuCourseId;
			updateCourseInstructorExemptionReq.InstructorId = InstructorId;
			updateCourseInstructorExemptionReq.NewIsInstructorExemptFromCourseList = NewIsInstructorExemptFromCourseList;
			ClientServiceFactory.GetClientInstance<ILookupCourse>().UpdateCourseInstructorExemption(updateCourseInstructorExemptionReq);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000AF24 File Offset: 0x00009124
		public IDictionary<int, bool> LoadIsLookupCourseExemptFromDataSync(IList<int> LuCourseIds)
		{
			LoadIsLookupCourseExemptFromDataSyncReq loadIsLookupCourseExemptFromDataSyncReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadIsLookupCourseExemptFromDataSyncReq>();
			loadIsLookupCourseExemptFromDataSyncReq.LuCourseIds = LuCourseIds;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadIsLookupCourseExemptFromDataSync(loadIsLookupCourseExemptFromDataSyncReq).IsExemptFromDataSyncList;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000AF5C File Offset: 0x0000915C
		public void UpdateLookupCourseExemptionFromDataSync(int LuCourseId, bool NewIsExempt)
		{
			UpdateLookupCourseExemptionFromDataSyncReq updateLookupCourseExemptionFromDataSyncReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateLookupCourseExemptionFromDataSyncReq>();
			updateLookupCourseExemptionFromDataSyncReq.LuCourseId = LuCourseId;
			updateLookupCourseExemptionFromDataSyncReq.NewIsExempt = NewIsExempt;
			ClientServiceFactory.GetClientInstance<ILookupCourse>().UpdateLookupCourseExemptionFromDataSync(updateLookupCourseExemptionFromDataSyncReq);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000AF94 File Offset: 0x00009194
		public LookupCourseDTO LoadCourseByLuCourseId(int LuCourseId)
		{
			LoadCourseByLuCourseIdReq loadCourseByLuCourseIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCourseByLuCourseIdReq>();
			loadCourseByLuCourseIdReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadCourseByLuCourseId(loadCourseByLuCourseIdReq).Course;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000AFCC File Offset: 0x000091CC
		public IList<LookupDurationTermSubjectDTO> LoadDurationTermSubjectsBySession(SessionDTO Session)
		{
			LoadDurationTermSubjectsBySessionReq loadDurationTermSubjectsBySessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadDurationTermSubjectsBySessionReq>();
			loadDurationTermSubjectsBySessionReq.Session = Session;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadDurationTermSubjectsBySession(loadDurationTermSubjectsBySessionReq).DurationTermSubjects;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B004 File Offset: 0x00009204
		public IList<int> LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(List<int> LuCourseIdsToCheck, DateTime StartDate, DateTime EndDate)
		{
			LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq loadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq>();
			loadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq.LuCourseIds = LuCourseIdsToCheck;
			loadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq.StartDate = StartDate;
			loadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq.EndDate = EndDate;
			IList<int> lucids = ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(loadLookupCourseIdsWithAtLeastOneClassTestDefinitionReq).Lucids;
			return (lucids != null) ? lucids.ToList<int>() : null;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000B058 File Offset: 0x00009258
		public bool IsCourseCurrentlyInScopeForActionByStudentOrProf(eCourseUsageType usageType, DateTime courseStartDate, DateTime courseEndDate)
		{
			switch (usageType)
			{
			case eCourseUsageType.StudentAccommodationLetterRequests:
				return this.IsCourseCurrentlyInScopeForActionByStudentOrProf(courseEndDate, Setting.SELFREGC_CourseEndDateAuthorizationExtensionInDays);
			case eCourseUsageType.StudentTestBooking:
				return this.IsCourseCurrentlyInScopeForActionByStudentOrProf(courseEndDate, Setting.TESTBOOKING_CourseEndDateAuthorizationExtensionInDays);
			case eCourseUsageType.StudentExamBooking:
				return this.IsCourseCurrentlyInScopeForActionByStudentOrProf(courseEndDate, Setting.EXAMBOOKING_CourseEndDateAuthorizationExtensionInDays);
			case eCourseUsageType.InstructorAccommodationLetters:
				return this.IsCourseCurrentlyInScopeForActionByStudentOrProf(courseEndDate, Setting.INSTRUCTOR_AccommodationLetterCourseEndDateAuthorizationExtensionInDays);
			case eCourseUsageType.InstructorTestBooking:
			case eCourseUsageType.InstructorExamBooking:
				return this.IsCourseCurrentlyInScopeForActionByStudentOrProf(courseEndDate, Setting.INSTRUCTOR_TestExamCourseEndDateAuthorizationExtensionInDays);
			}
			return DateTime.Now.Date <= courseEndDate;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000B0F4 File Offset: 0x000092F4
		public IList<CourseRegistrationDTO> LoadStudentsCourses(SessionDTO Session, int PersonId)
		{
			LoadStudentsCoursesBySessionReq loadStudentsCoursesBySessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsCoursesBySessionReq>();
			loadStudentsCoursesBySessionReq.PersonId = PersonId;
			loadStudentsCoursesBySessionReq.Session = Session;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadStudentsCoursesBySession(loadStudentsCoursesBySessionReq).Courses;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B134 File Offset: 0x00009334
		public IList<LookupCourseDateRangeDTO> LoadUniqueCourseDateRangesBySession(SessionDTO session)
		{
			LoadUniqueCourseDateRangesBySessionReq loadUniqueCourseDateRangesBySessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadUniqueCourseDateRangesBySessionReq>();
			loadUniqueCourseDateRangesBySessionReq.Session = session;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadUniqueCourseDateRangesBySession(loadUniqueCourseDateRangesBySessionReq).UniqueDateRanges;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B16C File Offset: 0x0000936C
		public void UpdateCourseDateRange(LookupCourseDateRangeDTO oldDateRange, LookupCourseDateRangeDTO newDateRange)
		{
			UpdateCourseDateRangeReq updateCourseDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCourseDateRangeReq>();
			updateCourseDateRangeReq.OldDateRange = oldDateRange;
			updateCourseDateRangeReq.NewDateRange = newDateRange;
			ClientServiceFactory.GetClientInstance<ILookupCourse>().UpdateCourseDateRange(updateCourseDateRangeReq);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B1A4 File Offset: 0x000093A4
		public IList<LookupCourseBaseDTO> LoadCoursesInDateRange(LookupCourseDateRangeDTO dateRange)
		{
			LoadCoursesInDateRangeReq loadCoursesInDateRangeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCoursesInDateRangeReq>();
			loadCoursesInDateRangeReq.DateRange = dateRange;
			return ClientServiceFactory.GetClientInstance<ILookupCourse>().LoadCoursesInDateRange(loadCoursesInDateRangeReq).CourseBases;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B1DC File Offset: 0x000093DC
		private bool IsCourseCurrentlyInScopeForActionByStudentOrProf(DateTime courseEndDate, Setting settingWithExtendedCourseEndDate)
		{
			DateTime date = DateTime.Now.Date;
			IWebSettingsClientManager webSettingsClientManager = new WebSettingsClientManager();
			int settingValue = webSettingsClientManager.GetSettingValue<int>(settingWithExtendedCourseEndDate);
			DateTime t = courseEndDate.AddDays((double)settingValue);
			return date <= t;
		}
	}
}

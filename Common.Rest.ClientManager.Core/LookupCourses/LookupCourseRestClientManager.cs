using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.ClientManager.ICore.Settings;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.Settings;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.LookupCourses
{
	// Token: 0x02000034 RID: 52
	public class LookupCourseRestClientManager : BearerTokenRestProxy<ILookupCourseClientManager>, ILookupCourseClientManager, IWebService
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00006A69 File Offset: 0x00004C69
		public LookupCourseRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00006A73 File Offset: 0x00004C73
		public LookupCourseRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00006A7E File Offset: 0x00004C7E
		public int CreateLookupCourse(LookupCourseDTO course)
		{
			return base.Post<LookupCourseDTO, int>(course, "lookupcourse");
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00006A8C File Offset: 0x00004C8C
		public IList<LookupCourseBaseDTO> LoadCourseBasesBySearchString(DateTime StartDate, DateTime EndDate, string SearchString)
		{
			return base.GetMany<LookupCourseBaseDTO>(string.Format("lookupcourse/coursebases/range/{0}/{1}?searchstring={2}", StartDate, EndDate, SearchString), true);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00006AAC File Offset: 0x00004CAC
		public IList<LookupCourseDTO> LoadCoursesBySubjectAndSession(SessionDTO Session, int SubjectId)
		{
			LoadCoursesBySubjectAndSessionReq loadCoursesBySubjectAndSessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCoursesBySubjectAndSessionReq>();
			loadCoursesBySubjectAndSessionReq.Session = Session;
			loadCoursesBySubjectAndSessionReq.SubjectId = SubjectId;
			return base.Post<LoadCoursesBySubjectAndSessionReq, IList<LookupCourseDTO>>(loadCoursesBySubjectAndSessionReq, "lookupcourse/loadcoursesbysubjectandsession");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00006ADE File Offset: 0x00004CDE
		public LookupCourseDTO CreateLookupCourseBase(LookupCourseBaseDTO CourseBase)
		{
			return base.Post<LookupCourseBaseDTO, LookupCourseDTO>(CourseBase, "lookupcourse/coursebase");
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00006AEC File Offset: 0x00004CEC
		public void UpdateCourseInstructorExemption(int LuCourseId, int InstructorId, bool NewIsInstructorExemptFromCourseList)
		{
			UpdateCourseInstructorExemptionReq updateCourseInstructorExemptionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateCourseInstructorExemptionReq>();
			updateCourseInstructorExemptionReq.LuCourseId = LuCourseId;
			updateCourseInstructorExemptionReq.InstructorId = InstructorId;
			updateCourseInstructorExemptionReq.NewIsInstructorExemptFromCourseList = NewIsInstructorExemptFromCourseList;
			base.Put<UpdateCourseInstructorExemptionReq>(updateCourseInstructorExemptionReq, "lookupcourse/instructorexemption");
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006B25 File Offset: 0x00004D25
		public IDictionary<int, bool> LoadIsLookupCourseExemptFromDataSync(IList<int> LuCourseIds)
		{
			return base.Get<IDictionary<int, bool>>(string.Format("lookupcourse/isexemptfromdatasync/lucouseids/{0}", LuCourseIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006B40 File Offset: 0x00004D40
		public void UpdateLookupCourseExemptionFromDataSync(int LuCourseId, bool NewIsExempt)
		{
			UpdateLookupCourseExemptionFromDataSyncReq updateLookupCourseExemptionFromDataSyncReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateLookupCourseExemptionFromDataSyncReq>();
			updateLookupCourseExemptionFromDataSyncReq.LuCourseId = LuCourseId;
			updateLookupCourseExemptionFromDataSyncReq.NewIsExempt = NewIsExempt;
			base.Put<UpdateLookupCourseExemptionFromDataSyncReq>(updateLookupCourseExemptionFromDataSyncReq, "lookupcourse/exemptfromdatasync");
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006B72 File Offset: 0x00004D72
		public LookupCourseDTO LoadCourseByLuCourseId(int LuCourseId)
		{
			return base.Get<LookupCourseDTO>(string.Format("lookupcourse/lucouseid/{0}", LuCourseId), true);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006B8B File Offset: 0x00004D8B
		public IList<LookupDurationTermSubjectDTO> LoadDurationTermSubjectsBySession(SessionDTO Session)
		{
			return base.Post<SessionDTO, IList<LookupDurationTermSubjectDTO>>(Session, "lookupcourse/loaddurationtermsubjectsbysession");
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00006B99 File Offset: 0x00004D99
		public IList<int> LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(List<int> LuCourseIdsToCheck, DateTime StartDate, DateTime EndDate)
		{
			return base.GetMany<int>(string.Format("lookupcourse/courseidswithatleastoneclasstestdefinition/lucourseids/{0}/range/{1}/{2}", LuCourseIdsToCheck.CommaSeparatedValuesWithoutSpace<int>(), StartDate, EndDate), true);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00006BC0 File Offset: 0x00004DC0
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

		// Token: 0x060001ED RID: 493 RVA: 0x00006C48 File Offset: 0x00004E48
		public IList<CourseRegistrationDTO> LoadStudentsCourses(SessionDTO Session, int PersonId)
		{
			LoadStudentsCoursesBySessionReq loadStudentsCoursesBySessionReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadStudentsCoursesBySessionReq>();
			loadStudentsCoursesBySessionReq.PersonId = PersonId;
			loadStudentsCoursesBySessionReq.Session = Session;
			return base.Post<LoadStudentsCoursesBySessionReq, IList<CourseRegistrationDTO>>(loadStudentsCoursesBySessionReq, "lookupcourse/loadstudentscoursesbysession");
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006C7C File Offset: 0x00004E7C
		private bool IsCourseCurrentlyInScopeForActionByStudentOrProf(DateTime courseEndDate, Setting settingWithExtendedCourseEndDate)
		{
			DateTime date = DateTime.Now.Date;
			int settingValue = ObjectFactory.Resolve<IWebSettingsClientManager>().GetSettingValue<int>(settingWithExtendedCourseEndDate);
			DateTime t = courseEndDate.AddDays((double)settingValue);
			return date <= t;
		}
	}
}

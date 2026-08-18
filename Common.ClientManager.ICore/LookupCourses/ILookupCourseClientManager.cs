using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ClientManager.ICore.LookupCourses
{
	// Token: 0x0200003A RID: 58
	public interface ILookupCourseClientManager : IWebService
	{
		// Token: 0x060001A0 RID: 416
		int CreateLookupCourse(LookupCourseDTO course);

		// Token: 0x060001A1 RID: 417
		IList<LookupCourseBaseDTO> LoadCourseBasesBySearchString(DateTime StartDate, DateTime EndDate, string SearchString);

		// Token: 0x060001A2 RID: 418
		IList<LookupCourseDTO> LoadCoursesBySubjectAndSession(SessionDTO Session, int SubjectId);

		// Token: 0x060001A3 RID: 419
		LookupCourseDTO CreateLookupCourseBase(LookupCourseBaseDTO CourseBase);

		// Token: 0x060001A4 RID: 420
		void UpdateCourseInstructorExemption(int LuCourseId, int InstructorId, bool NewIsInstructorExemptFromCourseList);

		// Token: 0x060001A5 RID: 421
		IDictionary<int, bool> LoadIsLookupCourseExemptFromDataSync(IList<int> LuCourseIds);

		// Token: 0x060001A6 RID: 422
		void UpdateLookupCourseExemptionFromDataSync(int LuCourseId, bool NewIsExempt);

		// Token: 0x060001A7 RID: 423
		LookupCourseDTO LoadCourseByLuCourseId(int LuCourseId);

		// Token: 0x060001A8 RID: 424
		IList<LookupDurationTermSubjectDTO> LoadDurationTermSubjectsBySession(SessionDTO Session);

		// Token: 0x060001A9 RID: 425
		IList<int> LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(List<int> LuCourseIdsToCheck, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001AA RID: 426
		bool IsCourseCurrentlyInScopeForActionByStudentOrProf(eCourseUsageType usageType, DateTime courseStartDate, DateTime courseEndDate);

		// Token: 0x060001AB RID: 427
		IList<CourseRegistrationDTO> LoadStudentsCourses(SessionDTO Session, int PersonId);

		// Token: 0x060001AC RID: 428
		IList<LookupCourseDateRangeDTO> LoadUniqueCourseDateRangesBySession(SessionDTO session);

		// Token: 0x060001AD RID: 429
		void UpdateCourseDateRange(LookupCourseDateRangeDTO oldDateRange, LookupCourseDateRangeDTO newDateRange);

		// Token: 0x060001AE RID: 430
		IList<LookupCourseBaseDTO> LoadCoursesInDateRange(LookupCourseDateRangeDTO dateRange);
	}
}

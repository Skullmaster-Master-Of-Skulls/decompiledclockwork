using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.ICore.LookupCourses
{
	// Token: 0x0200006C RID: 108
	public interface ILookupCourseManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060002F3 RID: 755
		LookupCourse LoadCourse(int LuCourseId);

		// Token: 0x060002F4 RID: 756
		void SaveCourse(LookupCourse course);

		// Token: 0x060002F5 RID: 757
		int CreateLookupCourse(LookupCourse course);

		// Token: 0x060002F6 RID: 758
		List<LookupCourse> LoadLookupCoursesByInstructor(int InstructorId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060002F7 RID: 759
		List<LookupCourse> LoadCoursesByDate(DateTime StartDate, DateTime EndDate);

		// Token: 0x060002F8 RID: 760
		IList<LookupCourse> LoadCoursesByIds(IList<int> LuCourseIds);

		// Token: 0x060002F9 RID: 761
		List<LookupCourseBase> LoadCourseBaseInfoByDate(DateTime StartDate, DateTime EndDate);

		// Token: 0x060002FA RID: 762
		IList<LookupCourseBase> LoadCourseBasesBySearchString(DateTime StartDate, DateTime EndDate, string SearchString);

		// Token: 0x060002FB RID: 763
		string GetCourseDescription(LookupCourse Course);

		// Token: 0x060002FC RID: 764
		string GetSubjectDescription(LookupSubject Subject);

		// Token: 0x060002FD RID: 765
		List<CourseRegistration> LoadStudentsCourses(Session Session, int PersonId);

		// Token: 0x060002FE RID: 766
		List<CourseRegistration> LoadStudentsCourses(int PersonId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060002FF RID: 767
		List<LookupCourse> LoadCoursesBySubjectAndSession(Session Session, int SubjectId);

		// Token: 0x06000300 RID: 768
		LookupCourse CreateLookupCourseFromExternalCourse(DataSyncExternalCourse ExternalCourse, int SubjectId, List<LookupInstructor> Instructors);

		// Token: 0x06000301 RID: 769
		LookupCourse CreateLookupCourseBase(LookupCourseBase CourseBase);

		// Token: 0x06000302 RID: 770
		List<int> LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(List<int> LuCourseIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x06000303 RID: 771
		void UpdateCourseInstructorExemption(int LuCourseId, int InstructorId, bool NewIsInstructorExemptFromCourseList);

		// Token: 0x06000304 RID: 772
		IDictionary<int, bool> LoadIsLookupCourseExemptFromDataSync(IList<int> LuCourseIds);

		// Token: 0x06000305 RID: 773
		void UpdateLookupCourseExemptionFromDataSync(int LuCourseId, bool NewIsExempt);

		// Token: 0x06000306 RID: 774
		void ClearPrimaryInstructor(int lucid);

		// Token: 0x06000307 RID: 775
		void ReplacePrimaryInstructor(int lucid, int iid);

		// Token: 0x06000308 RID: 776
		IList<LookupInstructor> LoadCourseInstructors(int lucid);

		// Token: 0x06000309 RID: 777
		IList<LookupDurationTermSubject> LoadDurationTermSubjectsBySession(Session Session);

		// Token: 0x0600030A RID: 778
		void UpdateCourseNote(int lucid, string newCourseNote);

		// Token: 0x0600030B RID: 779
		IList<LookupCourseBase> LoadCourseBasesByIds(int[] LuCourseIds);

		// Token: 0x0600030C RID: 780
		LookupCourse LoadLookupCourseByExamId(int ExamId);

		// Token: 0x0600030D RID: 781
		IList<LookupCourseDateRange> LoadUniqueCourseDateRangesBySession(Session session);

		// Token: 0x0600030E RID: 782
		void UpdateCourseDateRange(LookupCourseDateRange oldDateRange, LookupCourseDateRange newDateRange);

		// Token: 0x0600030F RID: 783
		IList<LookupCourseBase> LoadCoursesInDateRange(LookupCourseDateRange dateRange);
	}
}

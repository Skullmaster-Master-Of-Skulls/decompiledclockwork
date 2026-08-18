using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.LookupCourses
{
	// Token: 0x02000057 RID: 87
	public interface ILookupCourseDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001E2 RID: 482
		LookupCourse LoadCourse(int LuCourseId);

		// Token: 0x060001E3 RID: 483
		void SaveCourse(LookupCourse course);

		// Token: 0x060001E4 RID: 484
		List<LookupCourse> LoadLookupCoursesByInstructor(int InstructorId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001E5 RID: 485
		List<LookupCourse> LoadCoursesByDate(DateTime StartDate, DateTime EndDate);

		// Token: 0x060001E6 RID: 486
		List<LookupCourseBase> LoadCourseBaseInfoByDate(DateTime StartDate, DateTime EndDate);

		// Token: 0x060001E7 RID: 487
		List<CourseRegistration> LoadStudentsCourses(int PersonId, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001E8 RID: 488
		List<LookupCourse> LoadCoursesBySubjectAndSession(Session Session, int SubjectId);

		// Token: 0x060001E9 RID: 489
		LookupCourse CreateLookupCourseFromExternalCourse(DataSyncExternalCourse ExternalCourse, int SubjectId);

		// Token: 0x060001EA RID: 490
		LookupCourse CreateLookupCourseBase(LookupCourseBase CourseBase);

		// Token: 0x060001EB RID: 491
		void RemoveSecondaryInstructorFromCourse(int lucid, int iid);

		// Token: 0x060001EC RID: 492
		void AddSecondaryInstructorToCourse(int lucid, int iid);

		// Token: 0x060001ED RID: 493
		void SetPrimaryInstructor(int lucid, int iid);

		// Token: 0x060001EE RID: 494
		List<int> LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(List<int> LuCourseIds, DateTime StartDate, DateTime EndDate);

		// Token: 0x060001EF RID: 495
		IList<LookupCourse> LoadCoursesByIds(IList<int> LuCourseIds);

		// Token: 0x060001F0 RID: 496
		IList<LookupCourseBase> LoadCourseBasesBySearchString(DateTime StartDate, DateTime EndDate, string SearchString);

		// Token: 0x060001F1 RID: 497
		void UpdateCourseInstructorExemption(int LuCourseId, int InstructorId, bool NewIsInstructorExemptFromCourseList);

		// Token: 0x060001F2 RID: 498
		IDictionary<int, bool> LoadIsLookupCourseExemptFromDataSync(IList<int> LuCourseIds);

		// Token: 0x060001F3 RID: 499
		void UpdateLookupCourseExemptionFromDataSync(int LuCourseId, bool NewIsExempt);

		// Token: 0x060001F4 RID: 500
		void ClearPrimaryInstructor(int lucid);

		// Token: 0x060001F5 RID: 501
		void ReplacePrimaryInstructor(int lucid, int iid);

		// Token: 0x060001F6 RID: 502
		IList<LookupInstructor> LoadCourseInstructors(int lucid);

		// Token: 0x060001F7 RID: 503
		IList<LookupDurationTermSubject> LoadDurationTermSubjectsBySession(Session Session);

		// Token: 0x060001F8 RID: 504
		void UpdateCourseNote(int lucid, string newCourseNote);

		// Token: 0x060001F9 RID: 505
		IList<LookupCourseBase> LoadCourseBasesByIds(int[] LuCourseIds);

		// Token: 0x060001FA RID: 506
		void UpdateClockWorkCourseCredits(int lucid, decimal newCredits);

		// Token: 0x060001FB RID: 507
		LookupCourse LoadLookupCourseByExamId(int ExamId);

		// Token: 0x060001FC RID: 508
		IList<LookupCourseDateRange> LoadUniqueCourseDateRanges(DateTime startDate, DateTime endDate);

		// Token: 0x060001FD RID: 509
		void UpdateCourseDateRange(DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate);

		// Token: 0x060001FE RID: 510
		IList<LookupCourseBase> LoadCoursesInDateRange(DateTime startDate, DateTime endDate);
	}
}

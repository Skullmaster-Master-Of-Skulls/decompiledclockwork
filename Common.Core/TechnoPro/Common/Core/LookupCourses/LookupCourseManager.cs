using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.CourseRegistrations;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.LookupCourses
{
	// Token: 0x020000D2 RID: 210
	public class LookupCourseManager : ILookupCourseManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0003720A File Offset: 0x0003540A
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00037212 File Offset: 0x00035412
		public ILookupCourseDAO dao { get; set; }

		// Token: 0x060007E4 RID: 2020 RVA: 0x0003721B File Offset: 0x0003541B
		public LookupCourseManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new LookupCourseDAO(opContext);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0003723A File Offset: 0x0003543A
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00037242 File Offset: 0x00035442
		public OperationContext OpContext { get; set; }

		// Token: 0x060007E7 RID: 2023 RVA: 0x0003724C File Offset: 0x0003544C
		public int CreateLookupCourse(LookupCourse course)
		{
			course.LuCourseId = 0;
			this.SaveCourse(course);
			return course.LuCourseId;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00037274 File Offset: 0x00035474
		public LookupCourse LoadCourse(int LuCourseId)
		{
			return this.dao.LoadCourse(LuCourseId);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00037292 File Offset: 0x00035492
		public void SaveCourse(LookupCourse course)
		{
			this.dao.SaveCourse(course);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000372A4 File Offset: 0x000354A4
		public List<LookupCourse> LoadLookupCoursesByInstructor(int InstructorId, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadLookupCoursesByInstructor(InstructorId, StartDate, EndDate);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000372C4 File Offset: 0x000354C4
		public List<LookupCourseBase> LoadCourseBaseInfoByDate(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadCourseBaseInfoByDate(StartDate, EndDate);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000372E4 File Offset: 0x000354E4
		public List<LookupCourse> LoadCoursesByDate(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadCoursesByDate(StartDate, EndDate);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00037304 File Offset: 0x00035504
		public string GetCourseDescription(LookupCourse Course)
		{
			bool flag = Course == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Format("{0} {1} {2} {3}", new object[]
				{
					this.GetSubjectDescription(Course.Subject),
					Course.Course,
					Course.Section,
					Course.TimeOfDay
				});
			}
			return result;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00037360 File Offset: 0x00035560
		public string GetSubjectDescription(LookupSubject Subject)
		{
			bool flag = Subject == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = ((Subject.SubjectDescription == null) ? "" : Subject.SubjectDescription);
			}
			return result;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00037398 File Offset: 0x00035598
		public List<CourseRegistration> LoadStudentsCourses(Session Session, int PersonId)
		{
			return this.dao.LoadStudentsCourses(PersonId, Session.StartDate, Session.EndDate);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000373C4 File Offset: 0x000355C4
		public List<CourseRegistration> LoadStudentsCourses(int PersonId, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadStudentsCourses(PersonId, StartDate, EndDate);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000373E4 File Offset: 0x000355E4
		public List<LookupCourse> LoadCoursesBySubjectAndSession(Session Session, int SubjectId)
		{
			return this.dao.LoadCoursesBySubjectAndSession(Session, SubjectId);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00037404 File Offset: 0x00035604
		public LookupCourse CreateLookupCourseFromExternalCourse(DataSyncExternalCourse ExternalCourse, int SubjectId, List<LookupInstructor> Instructors)
		{
			return this.dao.CreateLookupCourseFromExternalCourse(ExternalCourse, SubjectId);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00037424 File Offset: 0x00035624
		public LookupCourse CreateLookupCourseBase(LookupCourseBase CourseBase)
		{
			return this.dao.CreateLookupCourseBase(CourseBase);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00037444 File Offset: 0x00035644
		public List<int> LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(List<int> LuCourseIds, DateTime StartDate, DateTime EndDate)
		{
			return this.dao.LoadLookupCourseIdsWithAtLeastOneClassTestDefinition(LuCourseIds, StartDate, EndDate);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00037464 File Offset: 0x00035664
		public IList<LookupCourse> LoadCoursesByIds(IList<int> LuCourseIds)
		{
			return this.dao.LoadCoursesByIds(LuCourseIds);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00037482 File Offset: 0x00035682
		public void UpdateCourseInstructorExemption(int LuCourseId, int InstructorId, bool NewIsInstructorExemptFromCourseList)
		{
			this.dao.UpdateCourseInstructorExemption(LuCourseId, InstructorId, NewIsInstructorExemptFromCourseList);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00037494 File Offset: 0x00035694
		public IList<LookupCourseBase> LoadCourseBasesBySearchString(DateTime StartDate, DateTime EndDate, string SearchString)
		{
			return this.dao.LoadCourseBasesBySearchString(StartDate, EndDate, SearchString);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000374B4 File Offset: 0x000356B4
		public IDictionary<int, bool> LoadIsLookupCourseExemptFromDataSync(IList<int> LuCourseIds)
		{
			return this.dao.LoadIsLookupCourseExemptFromDataSync(LuCourseIds);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x000374D2 File Offset: 0x000356D2
		public void UpdateLookupCourseExemptionFromDataSync(int LuCourseId, bool NewIsExempt)
		{
			this.dao.UpdateLookupCourseExemptionFromDataSync(LuCourseId, NewIsExempt);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000374E3 File Offset: 0x000356E3
		public void ClearPrimaryInstructor(int lucid)
		{
			this.dao.ClearPrimaryInstructor(lucid);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x000374F3 File Offset: 0x000356F3
		public void ReplacePrimaryInstructor(int lucid, int iid)
		{
			this.dao.ReplacePrimaryInstructor(lucid, iid);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00037504 File Offset: 0x00035704
		public IList<LookupInstructor> LoadCourseInstructors(int lucid)
		{
			return this.dao.LoadCourseInstructors(lucid);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00037524 File Offset: 0x00035724
		public IList<LookupDurationTermSubject> LoadDurationTermSubjectsBySession(Session Session)
		{
			return this.dao.LoadDurationTermSubjectsBySession(Session);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00037542 File Offset: 0x00035742
		public void UpdateCourseNote(int lucid, string newCourseNote)
		{
			this.dao.UpdateCourseNote(lucid, newCourseNote);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00037554 File Offset: 0x00035754
		public IList<LookupCourseBase> LoadCourseBasesByIds(int[] LuCourseIds)
		{
			return this.dao.LoadCourseBasesByIds(LuCourseIds);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00037574 File Offset: 0x00035774
		public LookupCourse LoadLookupCourseByExamId(int ExamId)
		{
			return this.dao.LoadLookupCourseByExamId(ExamId);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00037594 File Offset: 0x00035794
		public IList<LookupCourseDateRange> LoadUniqueCourseDateRangesBySession(Session session)
		{
			return this.dao.LoadUniqueCourseDateRanges(session.StartDate, session.EndDate);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x000375BD File Offset: 0x000357BD
		public void UpdateCourseDateRange(LookupCourseDateRange oldDateRange, LookupCourseDateRange newDateRange)
		{
			this.dao.UpdateCourseDateRange(oldDateRange.StartDate, oldDateRange.EndDate, newDateRange.StartDate, newDateRange.EndDate);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000375E4 File Offset: 0x000357E4
		public IList<LookupCourseBase> LoadCoursesInDateRange(LookupCourseDateRange dateRange)
		{
			return this.dao.LoadCoursesInDateRange(dateRange.StartDate, dateRange.EndDate);
		}
	}
}

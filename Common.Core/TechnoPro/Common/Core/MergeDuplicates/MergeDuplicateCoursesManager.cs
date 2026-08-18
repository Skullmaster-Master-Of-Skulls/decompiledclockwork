using System;
using System.Collections.Generic;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.DAO.Impl.MergeDuplicates;
using TechnoPro.Common.DAO.MergeDuplicates;
using TechnoPro.Common.ICore.MergeDuplicates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.MergeDuplicates;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Courses;

namespace TechnoPro.Common.Core.MergeDuplicates
{
	// Token: 0x020000B4 RID: 180
	public class MergeDuplicateCoursesManager : IMergeDuplicateCoursesManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00026A84 File Offset: 0x00024C84
		private LookupCourseManager lookupCourseManager
		{
			get
			{
				LookupCourseManager result;
				if ((result = this.lm) == null)
				{
					result = (this.lm = new LookupCourseManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00026AB0 File Offset: 0x00024CB0
		private IMergeDuplicateCoursesDAO dao
		{
			get
			{
				IMergeDuplicateCoursesDAO result;
				if ((result = this.d) == null)
				{
					result = (this.d = new MergeDuplicateCoursesDAO(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00026ADB File Offset: 0x00024CDB
		public MergeDuplicateCoursesManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x00026AED File Offset: 0x00024CED
		// (set) Token: 0x060006BF RID: 1727 RVA: 0x00026AF5 File Offset: 0x00024CF5
		public OperationContext OpContext { get; set; }

		// Token: 0x060006C0 RID: 1728 RVA: 0x00026B00 File Offset: 0x00024D00
		public IList<DuplicateCourseMergeResult> MergeDuplicateCourses(DateTime StartDate, DateTime EndDate)
		{
			IList<DuplicateCourseSet> duplicateCourses = this.LoadPossibleDuplicateCourses(StartDate, EndDate);
			return this.MergeCourses(duplicateCourses);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00026B24 File Offset: 0x00024D24
		public List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForStudents(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.MergeDuplicateCourseRegistrationsWithSameLuCourseIdForStudents(StartDate, EndDate);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00026B44 File Offset: 0x00024D44
		public List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForServiceProviders(DateTime StartDate, DateTime EndDate)
		{
			return this.dao.MergeDuplicateCourseRegistrationsWithSameLuCourseIdForServiceProviders(StartDate, EndDate);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00026B64 File Offset: 0x00024D64
		public IList<DuplicateCourseSet> LoadPossibleDuplicateCourses(DateTime StartDate, DateTime EndDate)
		{
			LookupCourseManager lookupCourseManager = this.lookupCourseManager;
			List<LookupCourseBase> list = lookupCourseManager.LoadCourseBaseInfoByDate(StartDate, EndDate);
			list.Sort((LookupCourseBase c1, LookupCourseBase c2) => this.GetCourseCompareString(c1).CompareTo(this.GetCourseCompareString(c2)));
			List<DuplicateCourseSet> list2 = new List<DuplicateCourseSet>();
			int j;
			for (int i = 0; i < list.Count; i = j)
			{
				LookupCourseBase c = list[i];
				for (j = i + 1; j < list.Count; j++)
				{
					LookupCourseBase c3 = list[j];
					bool flag = !this.CoursesAreEqual(c3, c);
					if (flag)
					{
						break;
					}
				}
				int num = j - i;
				bool flag2 = num > 1;
				if (flag2)
				{
					DuplicateCourseSet duplicateCourseSet = new DuplicateCourseSet
					{
						DuplicateCoursesWithInfo = new List<DuplicateCourse>()
					};
					for (int k = i; k < j; k++)
					{
						duplicateCourseSet.DuplicateCoursesWithInfo.Add(new DuplicateCourse
						{
							CourseRelatedInfo = null,
							LookupCourse = list[k]
						});
					}
					list2.Add(duplicateCourseSet);
				}
			}
			return list2;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00026C78 File Offset: 0x00024E78
		private bool CoursesAreEqual(LookupCourseBase c1, LookupCourseBase c2)
		{
			return c1.Term.Equals(c2.Term, StringComparison.OrdinalIgnoreCase) && c1.Duration.Equals(c2.Duration, StringComparison.OrdinalIgnoreCase) && c1.Subject.SubjectDescription.Equals(c2.Subject.SubjectDescription, StringComparison.OrdinalIgnoreCase) && c1.Course.Equals(c2.Course, StringComparison.OrdinalIgnoreCase) && c1.Section.Equals(c2.Section, StringComparison.OrdinalIgnoreCase) && c1.TimeOfDay.Equals(c2.TimeOfDay, StringComparison.OrdinalIgnoreCase) && !(c1.EndDate <= c2.StartDate) && !(c1.StartDate > c2.EndDate);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00026D40 File Offset: 0x00024F40
		private string GetCourseCompareString(LookupCourseBase c)
		{
			return string.Format("{0}.{1}.{2}.{3}.{4}.{5}.{6}", new object[]
			{
				c.Term,
				c.Duration ?? "",
				c.Subject.SubjectDescription,
				c.Course,
				c.Section,
				c.TimeOfDay ?? "",
				c.StartDate.ToString("yyyy-MM-dd")
			});
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00026DC8 File Offset: 0x00024FC8
		private IList<DuplicateCourseMergeResult> MergeCourses(IList<DuplicateCourseSet> DuplicateCourses)
		{
			List<DuplicateCourseMergeAction> list = new List<DuplicateCourseMergeAction>();
			for (int i = 0; i < DuplicateCourses.Count; i++)
			{
				DuplicateCourseSet duplicateCourseSet = DuplicateCourses[i];
				bool flag = duplicateCourseSet.DuplicateCoursesWithInfo.Count > 1;
				if (flag)
				{
					LookupCourseBase lookupCourse = duplicateCourseSet.DuplicateCoursesWithInfo[0].LookupCourse;
					int luCourseId = lookupCourse.LuCourseId;
					List<int> list2 = new List<int>();
					for (int j = 1; j < duplicateCourseSet.DuplicateCoursesWithInfo.Count; j++)
					{
						int luCourseId2 = duplicateCourseSet.DuplicateCoursesWithInfo[j].LookupCourse.LuCourseId;
						list2.Add(luCourseId2);
					}
					list.AddRange(this.GetActionsForTablesAndColumns(luCourseId, list2, new List<TableAndColumn>
					{
						new TableAndColumn
						{
							Table = eClockWorkTable.AccommodationEmailsSent,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.AccommodationLetterOutPending,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.AccommodationLoaIssued,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.AccommodationsApproval,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.AppointmentCourses,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Appointments,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Archive_AppointmentCourses,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Archive_exams,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Cache_lucourses_simplifiedCrosslisted,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Caching_AccommodationsModified,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Courses,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.EmailHistory,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.EmailOut,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ExamImportLog,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ExamRequest,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Exams,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.LuCourseInstructor,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.NotetakerDocument,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ServiceProviderApplicationCourses,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ServiceProviderNotes,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ServiceProviderRequests,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ServiceProviderRequests,
							Column = eClockWorkColumn.ServiceProviderLuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ServiceProviderRequestsHistory,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.ServiceProviderRequestsHistory,
							Column = eClockWorkColumn.ServiceProviderLuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.TestsSubmitted,
							Column = eClockWorkColumn.LuCourseId
						},
						new TableAndColumn
						{
							Table = eClockWorkTable.Timetable,
							Column = eClockWorkColumn.LuCourseId
						}
					}));
					foreach (int num in list2)
					{
						list.Add(new DuplicateCourseMergeAction
						{
							ActionType = eDuplicateCourseMergeActionType.RemoveLookupCourse,
							OldLucid = num,
							NewLucid = num,
							TableAndColumnToApplyTo = new TableAndColumn
							{
								Table = eClockWorkTable.LuCourses,
								Column = eClockWorkColumn.LuCourseId
							}
						});
					}
				}
			}
			return this.dao.ExecuteCourseMergeActions(list);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0002723C File Offset: 0x0002543C
		private List<DuplicateCourseMergeAction> GetActionsForTablesAndColumns(int primaryLucid, List<int> mergedLucids, List<TableAndColumn> tableAndColumns)
		{
			List<DuplicateCourseMergeAction> list = new List<DuplicateCourseMergeAction>();
			foreach (TableAndColumn tableAndColumnToApplyTo in tableAndColumns)
			{
				foreach (int oldLucid in mergedLucids)
				{
					list.Add(new DuplicateCourseMergeAction
					{
						ActionType = eDuplicateCourseMergeActionType.ChangeLucid,
						OldLucid = oldLucid,
						NewLucid = primaryLucid,
						TableAndColumnToApplyTo = tableAndColumnToApplyTo
					});
				}
			}
			return list;
		}

		// Token: 0x04000146 RID: 326
		private LookupCourseManager lm;

		// Token: 0x04000147 RID: 327
		private IMergeDuplicateCoursesDAO d;
	}
}

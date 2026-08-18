using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Courses;

namespace TechnoPro.Common.ICore.MergeDuplicates
{
	// Token: 0x02000060 RID: 96
	public interface IMergeDuplicateCoursesManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000299 RID: 665
		IList<DuplicateCourseSet> LoadPossibleDuplicateCourses(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600029A RID: 666
		IList<DuplicateCourseMergeResult> MergeDuplicateCourses(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600029B RID: 667
		List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForStudents(DateTime StartDate, DateTime EndDate);

		// Token: 0x0600029C RID: 668
		List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForServiceProviders(DateTime StartDate, DateTime EndDate);
	}
}

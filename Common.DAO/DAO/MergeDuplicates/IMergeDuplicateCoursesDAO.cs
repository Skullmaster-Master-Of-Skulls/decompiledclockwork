using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Courses;

namespace TechnoPro.Common.DAO.MergeDuplicates
{
	// Token: 0x0200004D RID: 77
	public interface IMergeDuplicateCoursesDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060001B1 RID: 433
		List<DuplicateCourseMergeResult> ExecuteCourseMergeActions(List<DuplicateCourseMergeAction> Actions);

		// Token: 0x060001B2 RID: 434
		List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForStudents(DateTime StartDate, DateTime EndDate);

		// Token: 0x060001B3 RID: 435
		List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForServiceProviders(DateTime StartDate, DateTime EndDate);
	}
}

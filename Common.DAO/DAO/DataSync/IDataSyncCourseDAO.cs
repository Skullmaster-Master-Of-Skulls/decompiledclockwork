using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.DataSync
{
	// Token: 0x0200008F RID: 143
	public interface IDataSyncCourseDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060003B3 RID: 947
		LookupCourse FindLookupCourse(DataSyncExternalCourse ExternalCourse, int SubjectId);

		// Token: 0x060003B4 RID: 948
		LookupInstructor FindLookupInstructor(DataSyncExternalCourseInstructor ExternalInstructor);

		// Token: 0x060003B5 RID: 949
		LookupSubject FindLookupSubject(string SubjectName);

		// Token: 0x060003B6 RID: 950
		LookupCourse CreateLookupCourse(DataSyncExternalCourse extCourse, int subjectId, IList<DataSyncExternalCourseSyncResult> results = null);

		// Token: 0x060003B7 RID: 951
		DataTable LoadCustomCoursesTable(int RowsPerPage, int PageNumber);

		// Token: 0x060003B8 RID: 952
		void FixNoPrimaryWhenSecondariesExistProblemWithProfs(List<int> lucids);

		// Token: 0x060003B9 RID: 953
		void UpdateClockWorkCourse(DataSyncExternalCourse extCourse, bool campusChanged, bool deptChanged);
	}
}

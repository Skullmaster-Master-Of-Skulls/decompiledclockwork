using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.DataSync;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.OperationContexts;

namespace TechnoPro.Common.ICore.DataSync
{
	// Token: 0x020000A6 RID: 166
	public interface IDataSyncCourseManager : IBaseOperationContext<DataSyncOperationContext>
	{
		// Token: 0x060004D7 RID: 1239
		List<DataSyncExternalCourse> ParseExternalCourseRowParts(List<DataSyncExternalCourseRowPart> rowParts);

		// Token: 0x060004D8 RID: 1240
		LookupCourse FindLookupCourse(DataSyncExternalCourse externalCourse);

		// Token: 0x060004D9 RID: 1241
		LookupInstructor FindLookupInstructor(DataSyncExternalCourseInstructor externalInstructor);

		// Token: 0x060004DA RID: 1242
		LookupSubject FindLookupSubject(string subjectName);

		// Token: 0x060004DB RID: 1243
		List<DataSyncExternalCourseSyncResult> DataSyncCourses(string studentNumber, List<DataSyncExternalCourse> allExternalCourses);

		// Token: 0x060004DC RID: 1244
		LookupSubject FindSubjectCreateIfNecessary(string subjectDescription, string subjectLong, out bool created);

		// Token: 0x060004DD RID: 1245
		LookupInstructor FindInstructorCreateIfNecessary(DataSyncExternalCourseInstructor externalProf, out bool createdProf);

		// Token: 0x060004DE RID: 1246
		IList<DataSyncExternalCourseSyncResult> DataSyncCoursesForNotetakers(string studentNumber, List<DataSyncExternalCourse> externalCourses);

		// Token: 0x060004DF RID: 1247
		void CreateLookupCoursesFromCustomCoursesTable();

		// Token: 0x060004E0 RID: 1248
		List<DataSyncExternalCourse> FindMatchingLookupCourses(ref List<DataSyncExternalCourseSyncResult> results, ref List<DataSyncExternalCourse> ExternalCourses);

		// Token: 0x060004E1 RID: 1249
		IList<DataSyncExternalCourseSyncResult> DataSyncLookupCourses(DataTable table);

		// Token: 0x060004E2 RID: 1250
		IList<DataSyncExternalCourseSyncResult> DataSyncLookupCourses(IList<DataSyncExternalCourse> allExternalCourses);

		// Token: 0x060004E3 RID: 1251
		List<DataSyncExternalCourseRowPart> GetRowPartsFromDataTable(DataTable table);

		// Token: 0x060004E4 RID: 1252
		IList<DataSyncExternalCourseSyncResult> ImportOldCourses(string StudentNumber, IList<DataSyncExternalCourse> ExternalCourses);

		// Token: 0x060004E5 RID: 1253
		IList<DataSyncExternalCourseSyncResult> DataSyncCourses(string studentNumber, int batchDataSyncLogId = 0);
	}
}

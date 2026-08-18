using System;

namespace TechnoPro.Common.DAO.Impl.People.QueryStorage
{
	// Token: 0x0200007E RID: 126
	internal static class QueryStorageStudentManagement
	{
		// Token: 0x04000165 RID: 357
		internal const string QS_ACTIVE_STUDENTS = "CREATE TABLE #tpids (personid INT)\r\n\r\nINSERT INTO #tpids\r\n\tEXEC ActiveStudentPids @startdate,@enddate\r\n\r\nSELECT \tt.personid,p.lastname,p.firstname,p.middlename,p.student_no\r\nFROM \t#tpids t LEFT JOIN people p ON p.personid=t.personid\r\nORDER BY t.personid;\r\n\r\nDROP TABLE #tpids";

		// Token: 0x04000166 RID: 358
		internal const string QS_STUDENT_NUMBER = "SELECT student_no FROM people WHERE personid=@pid";
	}
}

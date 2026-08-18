using System;

namespace TechnoPro.Common.DAO.Impl.QueryStorage
{
	// Token: 0x0200011D RID: 285
	public class QueryStorageCache
	{
		// Token: 0x040004CF RID: 1231
		internal const string QS_ACADEMICTERMS = "SELECT    lucoursesessiondateid,description,startmonth,startday,endmonth,endday \r\nFROM        lucoursesessiondate \r\nORDER BY startmonth,startday";

		// Token: 0x040004D0 RID: 1232
		internal const string QS_STUDENTS = "SELECT p.personid,p.firstname,p.lastname,p.student_no,p.middlename,1 AS groupid,'Students' AS description\r\nFROM people p WHERE p.isactive=1 AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)";

		// Token: 0x040004D1 RID: 1233
		internal const string QS_ROOMS = "SELECT p.personid,p.firstname,p.lastname,p.student_no,p.middlename,3 AS groupid,'Rooms' AS description\r\nFROM people p WHERE p.isactive=1 AND p.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)";

		// Token: 0x040004D2 RID: 1234
		internal const string QS_APPTYPES_TEST = "SELECT apptypeid,description AS apptypedescription,defaultcolour,appointmenttypegroupid,'' AS apptypegrouptitle\r\nFROM appointmenttypes \r\nWHERE iscourse=1\r\nORDER BY description";
	}
}

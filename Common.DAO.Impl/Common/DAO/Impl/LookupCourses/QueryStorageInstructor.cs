using System;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x0200009A RID: 154
	public class QueryStorageInstructor
	{
		// Token: 0x040001D6 RID: 470
		internal const string QS_ALLOWED_LUCIDS_BY_INSTRUCTOR_OR_ALTCONTACT = "SELECT DISTINCT lucourseid FROM \r\n(\r\nSELECT\tlucourseid FROM lucourseinstructor WHERE instructorid=@iid\r\nUNION ALL\r\nSELECT\tv.lucourseid\r\nFROM\tvAlternateContactList v LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=v.alternatecontactid\r\nWHERE\tv.alternatecontactid=@altid \r\n\t\tAND (@permissionlevel=-1 OR (alt.altpermissionlevel & @permissionlevel)>0)\r\n) x";

		// Token: 0x040001D7 RID: 471
		internal const string QS_INSTRUCTOR_COURSE_REGISTRATION_UNIQUE_DATES = "SELECT    DISTINCT luc.startdate \r\nFROM        vInstructorList c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\nWHERE       c.instructorid=@instructorid\r\nORDER BY luc.startdate";

		// Token: 0x040001D8 RID: 472
		internal const string QS_INSTRUCTORS_BY_SEARCH_STRING = "SELECT    lucd2.lucoursedataid AS instructorid,lucd2.altlookupstring AS instructorname,\r\n            lucd2.email AS instructoremail,lucd2.phone AS instructorphone,\r\n            lucd2.username AS instructorusername,lucd2.externalid AS instructorexternalid,\r\n            lucd2.id AS instructoremployeeid,lucd2.exemptfromdatasync\r\nFROM        lucoursedata lucd2\r\nWHERE       lucd2.lookuplisttype=1\r\n            AND\r\n            (lucd2.altlookupstring LIKE @searchstring OR lucd2.email LIKE @searchstring \r\n            OR lucd2.phone LIKE @searchstring OR lucd2.username LIKE @searchstring \r\n            OR lucd2.id LIKE @searchstring)\r\nORDER BY    lucd2.altlookupstring";

		// Token: 0x040001D9 RID: 473
		internal const string QU_INSTRUCTOR_ASSIGNMENT = "IF EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND instructorid=@instructorid)\r\n    UPDATE lucourses SET ExemptAssignmentFromDataSync=COALESCE(@isexempt,ExemptAssignmentFromDataSync) WHERE lucourseid=@lucid AND instructorid=@instructorid\r\nELSE IF EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@instructorid)\r\n    UPDATE lucourseinstructor SET ExemptAssignmentFromDataSync=COALESCE(@isexempt,ExemptAssignmentFromDataSync) WHERE lucourseid=@lucid AND instructorid=@instructorid\r\nELSE\r\nBEGIN\r\n    IF EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND instructorid>0)\r\n        INSERT INTO lucourseinstructor (lucourseid,instructorid,ExemptAssignmentFromDataSync) VALUES (@lucid,@instructorid,COALESCE(@isexempt,0))\r\n    ELSE\r\n        UPDATE lucourses SET instructorid=@instructorid,ExemptAssignmentFromDataSync=COALESCE(@isexempt,ExemptAssignmentFromDataSync) WHERE lucourseid=@lucid\r\nEND";

		// Token: 0x040001DA RID: 474
		internal const string QD_INSTRUCTOR_ASSIGNMENT = "DELETE FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@instructorid\r\nUPDATE LUCourses SET instructorid=-1 WHERE LUCourseID=@lucid AND instructorid=@instructorid\r\n\r\nIF EXISTS(SELECT lucourseid FROM LUCourses WHERE LUCourseID=@lucid AND instructorid=-1)\r\n\tAND EXISTS(SELECT lucourseid FROM lucourseinstructor WHERE lucourseid=@lucid)\r\nBEGIN\r\n    DECLARE @acid int\r\n    SET @acid=(SELECT TOP 1 instructorid FROM lucourseinstructor WHERE lucourseid=@lucid)\r\n    UPDATE lucourses SET instructorid=@acid WHERE lucourseid=@lucid\r\n    DELETE FROM lucourseinstructor WHERE lucourseid=@lucid AND instructorid=@acid\r\nEND";

		// Token: 0x040001DB RID: 475
		internal const string QS_INSTRUCTORS_BY_COURSE = "SELECT    vil.instructorid,lucd.altlookupstring AS instructorname,lucd.email AS instructoremail,\r\n            lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\n            lucd.externalid AS instructorexternalid,lucd.id AS instructoremployeeid,\r\n            lucd.exemptfromdatasync,vil.instructorexemptassignmentfromdatasync,vil.PrimaryInstructorId\r\nFROM        vInstructorList vil LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=vil.instructorid\r\nWHERE       vil.lucourseid=@lucid\r\nORDER BY    lucd.altlookupstring,lucd.email";

		// Token: 0x040001DC RID: 476
		internal const string QU_INSTRUCTOR_EXEMPT_STATUS = "UPDATE lucoursedata SET exemptfromdatasync=@exempt WHERE lucoursedataid=@instructorid";
	}
}

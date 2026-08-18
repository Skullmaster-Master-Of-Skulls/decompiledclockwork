using System;

namespace TechnoPro.Common.DAO.Impl.DataSync
{
	// Token: 0x020000FB RID: 251
	public class QueryStorageDataSyncCourse
	{
		// Token: 0x04000420 RID: 1056
		internal const string QS_ALL_CUSTOM_MAPPINGS_BY_TABLE = "SELECT c.ClockWorkTableName,c.ExternalColumnName,c.ClockWorkColumnName,c.IsEncrypted\r\nFROM    CUSTOM_ExternalInternalMappings c\r\nWHERE   c.ClockWorkTableName=@tablename";

		// Token: 0x04000421 RID: 1057
		internal const string QS_ALL_CUSTOM_MAPPINGS_FOR_MULTIPLE_TABLES = "SELECT c.ClockWorkTableName,c.ExternalColumnName,c.ClockWorkColumnName,c.IsEncrypted\r\nFROM    CUSTOM_ExternalInternalMappings c\r\nWHERE   c.ClockWorkTableName IN (SELECT orderid AS ClockWorkTableName FROM splitstrings2(@tablenames,','))\r\nORDER BY c.ClockWorkTableName";

		// Token: 0x04000422 RID: 1058
		internal const string QS_CoursesWithMissingPrimaryProfThatHaveOneOrMoreSecondaryProfs_bylucids = "SELECT DISTINCT luc.lucourseid,MIN(i.instructorid) AS instructorid\r\nFROM LUCourses luc LEFT JOIN lucourseinstructor i ON i.lucourseid=luc.lucourseid\r\nWHERE   luc.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))\r\n        AND luc.InstructorID<0 AND NOT i.lucourseid IS NULL\r\nGROUP BY luc.lucourseid\r\nORDER BY luc.lucourseid";

		// Token: 0x04000423 RID: 1059
		internal const string QS_FIND_INSTRUCTOR = "SELECT\tlucd.luCourseDataID AS instructorid,lucd.altLookupString AS instructorname,\r\n\t\tlucd.email AS instructoremail,lucd.username AS instructorusername,lucd.id AS instructoremployeeid,\r\n\t\tlucd.phone AS instructorphone,lucd.ExternalId AS instructorexternalid,lucd.PermissionLevel AS instructorpermissionlevel,\r\n        lucd.ExemptFromDataSync\r\nFROM\tLUCourseData lucd \r\nWHERE\t(LEN(COALESCE(@instructorexternalid,'')) > 0 AND lucd.ExternalId=@instructorexternalid)\r\n\t\tOR\r\n\t\t(LEN(COALESCE(@instructorusername,'')) > 0 AND lucd.username=@instructorusername)\r\n\t\tOR\r\n\t\t(LEN(COALESCE(@instructoremployeeid,'')) > 0 AND lucd.id=@instructoremployeeid)";

		// Token: 0x04000424 RID: 1060
		internal const string QS_FIND_LOOKUP_COURSE = "SELECT    luc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,luc.subjectid,\r\nlucd.altlookupstring AS subjectdescription,lucd.lookupstring AS subjectcode,lucd.email AS subjectemail,\r\nluc.course,luc.timeofday,luc.section,luc.instructorid AS pinstructorid,lucd2.phone AS pinstructorphone,\r\nlucd2.altlookupstring AS pinstructorname,lucd2.email AS pinstructoremail,\r\nlucd2.username AS pinstructorusername,lucd2.id AS pinstructoremployeeid,\r\nlucd2.externalid AS pinstructorexternalid,lucd2.permissionlevel AS pinstructorpermissionlevel,\r\nlucd2.exemptfromdatasync AS pexemptfromdatasync,\r\nluc.ExemptAssignmentFromDataSync AS pExemptAssignmentFromDataSync,\r\nluc.crosslistcode,luc.equivalentcode,\r\nluc.whoadded,luc.dateadded,luc.location,luc.alternatecontactid AS primaryalternatecontactid,\r\nac.altname AS primaryaltname,ac.altemail AS primaryaltemail,ac.altphone AS primaryaltphone,\r\nac.altusername AS primaryaltusername,ac.altpermissionlevel AS primaryaltpermissionlevel,ac.externalid AS primaryaltexternalid,\r\nluc.campus,luc.department,luc.externalid,\r\nli.instructorid AS p3instructorid,lucd3.phone AS p3instructorphone,\r\nlucd3.altlookupstring AS p3instructorname,lucd3.email AS p3instructoremail,\r\nlucd3.username AS p3instructorusername,lucd3.id AS p3instructoremployeeid,\r\nlucd3.externalid AS p3instructorexternalid,lucd3.permissionlevel AS p3instructorpermissionlevel,\r\nlucd3.exemptfromdatasync AS p3exemptfromdatasync,tt.timetableid,\r\nli.ExemptAssignmentFromDataSync AS p3ExemptAssignmentFromDataSync,\r\ntt.timetabletype,tt.sunstartminutes,tt.sunendminutes,tt.sunroom,\r\ntt.monstartminutes,tt.monendminutes,tt.monroom,tt.tuestartminutes,tt.tueendminutes,tt.tueroom,\r\ntt.wedstartminutes,tt.wedendminutes,tt.wedroom,tt.thustartminutes,tt.thuendminutes,tt.thuroom,\r\ntt.fristartminutes,tt.friendminutes,tt.friroom,tt.satstartminutes,tt.satendminutes,tt.satroom,\r\nluc.exemptfromdatasync AS lucexemptfromdatasync,luc.coursenote\r\nFROM    lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n        LEFT JOIN lucourseinstructor li ON li.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursedata lucd3 ON lucd3.lucoursedataid=li.instructorid\r\n        LEFT JOIN timetable tt ON tt.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\n        LEFT JOIN LuCourseAltContact lucac ON lucac.lucourseid=luc.lucourseid\r\n        LEFT JOIN lucoursealternatecontact ac2 ON ac2.alternatecontactid=lucac.alternatecontactid\r\nWHERE   NOT ( luc.enddate <= @startdate OR luc.startdate > @enddate)\r\n        AND luc.duration=@duration AND luc.term=@term AND luc.subjectid=@subjectid AND luc.course=@course\r\n        AND luc.section=@section AND luc.timeofday=@timeofday AND luc.campus=@campus";

		// Token: 0x04000425 RID: 1061
		internal const string QU_UPDATE_LOOKUP_COURSE_NON_DEFINING_PROPERTIES = "UPDATE lucourses SET department=COALESCE(@department,department),campus=COALESCE(@campus,campus) WHERE lucourseid=@lucid";

		// Token: 0x04000426 RID: 1062
		internal const string QI_EXTERNAL_COL_MAPPING = "INSERT INTO CUSTOM_ExternalInternalMappings (ClockWorkTableName,ExternalColumnName,ClockWorkColumnName,IsEncrypted)\r\nVALUES (@tablename,@externalcolumnname,@clockworkcolumnname,@isencrypted)";

		// Token: 0x04000427 RID: 1063
		internal const string QU_UPDATE_LOOKUP_COURSE_PRIMARY_INSTRUCTOR = "UPDATE lucourses SET instructorid=@iid WHERE lucourseid=@lucid";

		// Token: 0x04000428 RID: 1064
		internal const string QD_ALL_EXTERNAL_COL_MAPPINGS = "DELETE FROM CUSTOM_ExternalInternalMappings WHERE ClockWorkTableName=@tablename";
	}
}

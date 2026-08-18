using System;

namespace TechnoPro.Common.DAO.Impl.LookupCourses.Management
{
	// Token: 0x020000A3 RID: 163
	internal static class QueryStorageLookupInstructorManagement
	{
		// Token: 0x04000222 RID: 546
		internal const string QS_ALL_LOOKUP_INSTRUCTORS_FOR_MANAGEMENT = "SELECT\tlucd.luCourseDataID AS instructorid,\r\n        lucd.altLookupString AS instructorname,lucd.email AS instructoremail,\r\n        lucd.ExternalId AS instructorexternalid,lucd.id AS instructoremployeeid,\r\n\t\tlucd.lookupString,lucd.passwordhash,lucd.PermissionLevel,lucd.phone AS instructorphone,lucd.username AS instructorusername,\r\n        lucd2.altlookupstring AS subject,luc.course,luc.[section],luc.timeofday,luc.campus,luc.duration,luc.term,\r\n        luc.startdate,luc.enddate,luc.lucourseid,\r\n        c.registrationstatus,c.personid,\r\n        p.firstname,p.lastname,p.middlename,p.student_no,\r\n        vil.InstructorExemptAssignmentFromDataSync,lucd.exemptfromdatasync\r\nFROM\tLuCourseData lucd LEFT JOIN vInstructorList vil ON vil.instructorid=lucd.luCourseDataID\r\n\t\tLEFT JOIN LUCourses luc ON luc.LUCourseID=vil.lucourseid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.subjectid\r\n\t\tLEFT JOIN Courses c ON c.luCourseID=luc.LUCourseID \r\n\t\tLEFT JOIN People p ON p.PersonID=c.personID\r\nWHERE\tlucd.lookupListType=1\r\nORDER BY lucd.altLookupString,lucd.username,lucd.externalid,lucd.luCourseDataID";

		// Token: 0x04000223 RID: 547
		internal const string QD_INSTRUCTOR = "DELETE FROM LuCourseInstructor WHERE instructorid=@instructorid\r\nUPDATE LuCourses SET InstructorID=-1 WHERE InstructorID=@instructorid\r\nDELETE FROM LuCourseData WHERE LuCourseDataId=@instructorid";

		// Token: 0x04000224 RID: 548
		internal const string QU_SWAP_INSTRUCTORS = "-- insert new rows into lucourseinstructor with existing lucourseids and @instructorsourceid, unless that already exists\r\n-- then delete the original rows with @instructorsourceid\r\nINSERT INTO LuCourseInstructor (lucourseid,instructorid,ExemptAssignmentFromDataSync)\r\n     SELECT lucourseid,@instructordestid,ExemptAssignmentFromDataSync FROM LuCourseInstructor \r\n\t WHERE  instructorid=@instructorsourceid AND NOT lucourseid IN (SELECT lucourseid FROM LuCourseInstructor WHERE instructorid=@instructordestid)\r\n\r\nDELETE FROM LuCourseInstructor WHERE instructorid=@instructorsourceid\r\n\r\nUPDATE LuCourses SET InstructorID=@instructordestid WHERE InstructorID=@instructorsourceid";
	}
}

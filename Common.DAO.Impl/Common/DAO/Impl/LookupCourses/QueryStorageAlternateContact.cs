using System;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x02000099 RID: 153
	public class QueryStorageAlternateContact
	{
		// Token: 0x040001CA RID: 458
		internal const string QS_ALTERNATE_CONTACT_COURSE_REGISTRATION_UNIQUE_DATES = "SELECT    DISTINCT luc.startdate \r\nFROM        vAlternateContactList c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\nWHERE       c.alternatecontactid=@alternatecontactid\r\nORDER BY luc.startdate";

		// Token: 0x040001CB RID: 459
		internal const string QS_ALTERNATE_CONTACT_BY_ID = "SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.alternatecontactid=@id";

		// Token: 0x040001CC RID: 460
		internal const string QS_ALTERNATE_CONTACT_BY_USERNAME = "SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.altusername=@username";

		// Token: 0x040001CD RID: 461
		internal const string QS_ALTERNATE_CONTACT_BY_EMPLOYEEID = "SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.externalid=@employeeid";

		// Token: 0x040001CE RID: 462
		internal const string QS_ALTERNATE_CONTACT_BY_EMAIL = "SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.altemail=@email";

		// Token: 0x040001CF RID: 463
		internal const string QS_ALTERNATE_CONTACTS_BY_COURSE = "SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       ac.alternatecontactid IN (SELECT alternatecontactid FROM lucourses WHERE lucourseid=@lucid)\r\n            OR ac.alternatecontactid IN (SELECT alternatecontactid FROM lucoursealtcontact WHERE lucourseid=@lucid)\r\nORDER BY    ac.altname";

		// Token: 0x040001D0 RID: 464
		internal const string QS_ALTERNATE_CONTACTS_BY_SEARCH_STRING = "SELECT    ac.alternatecontactid,ac.altname,ac.altemail,ac.altphone,ac.altusername,ac.altpermissionlevel,\r\n            ac.whocreated AS whocreatedpersonid,pwho.firstname AS whocreatedfirstname,\r\n            pwho.lastname AS whocreatedlastname,pwho.student_no AS whocreatedstudent_no,\r\n            ac.isactive,ac.externalid\r\nFROM        lucoursealternatecontact ac LEFT JOIN people pwho ON pwho.personid=ac.whocreated\r\nWHERE       altname LIKE @searchstring OR altemail LIKE @searchstring OR altphone LIKE @searchstring\r\n            OR altusername LIKE @searchstring OR externalid LIKE @searchstring\r\nORDER BY    ac.altname";

		// Token: 0x040001D1 RID: 465
		internal const string QU_ALTERNATE_CONTACT_ASSIGNMENT = "IF NOT EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND alternatecontactid=@alternatecontactid)\r\n    AND NOT EXISTS(SELECT lucourseid FROM lucoursealtcontact WHERE lucourseid=@lucid AND alternatecontactid=@alternatecontactid)\r\nBEGIN\r\n    IF EXISTS(SELECT lucourseid FROM lucourses WHERE lucourseid=@lucid AND alternatecontactid>0)\r\n        INSERT INTO lucoursealtcontact (lucourseid,alternatecontactid) VALUES (@lucid,@alternatecontactid)\r\n    ELSE\r\n        UPDATE lucourses SET alternatecontactid=@alternatecontactid WHERE lucourseid=@lucid\r\nEND";

		// Token: 0x040001D2 RID: 466
		internal const string QU_ALTERNATE_CONTACT = "UPDATE lucoursealternatecontact SET \r\n        altname=COALESCE(@name,altname),altemail=COALESCE(@email,altemail),altphone=COALESCE(@phone,altphone),\r\n        altusername=COALESCE(@username,altusername),altpermissionlevel=COALESCE(@permissionlevel,altpermissionlevel),\r\n        isactive=COALESCE(@isactive,isactive),externalid=COALESCE(@externalid,externalid)\r\nWHERE   alternatecontactid=@id";

		// Token: 0x040001D3 RID: 467
		internal const string QI_ALTERNATE_CONTACT = "INSERT INTO lucoursealternatecontact(altname,altemail,altphone,altusername,altpermissionlevel,whocreated,externalid) \r\nVALUES \r\n(@name,@email,@phone,@username,@permissionlevel,@whocreated,@externalid);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS alternatecontactid";

		// Token: 0x040001D4 RID: 468
		internal const string QD_ALTERNATE_CONTACT = "DELETE FROM lucoursealternatecontact WHERE alternatecontactid=@id";

		// Token: 0x040001D5 RID: 469
		internal const string QD_ALTERNATE_CONTACT_ASSIGNMENT = "DELETE FROM lucoursealtcontact WHERE lucourseid=@lucid AND alternatecontactid=@alternatecontactid\r\nUPDATE LUCourses SET alternatecontactid=-1 WHERE LUCourseID=@lucid AND alternatecontactid=@alternatecontactid\r\n\r\nIF EXISTS(SELECT lucourseid FROM LUCourses WHERE LUCourseID=@lucid AND alternatecontactid=-1)\r\n\tAND EXISTS(SELECT lucourseid FROM lucoursealtcontact WHERE lucourseid=@lucid)\r\nBEGIN\r\n    DECLARE @acid int\r\n    SET @acid=(SELECT TOP 1 alternatecontactid FROM lucoursealtcontact WHERE lucourseid=@lucid)\r\n    UPDATE lucourses SET alternatecontactid=@acid WHERE lucourseid=@lucid\r\n    DELETE FROM lucoursealtcontact WHERE lucourseid=@lucid AND alternatecontactid=@acid\r\nEND";
	}
}

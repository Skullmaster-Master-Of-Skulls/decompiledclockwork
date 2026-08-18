using System;

namespace TechnoPro.Common.DAO.Impl.Membership
{
	// Token: 0x0200008F RID: 143
	internal static class QueryStorageUserMembership
	{
		// Token: 0x040001AC RID: 428
		internal const string QS_AUTHENTICATE_AND_GET_PERSONID = "SELECT p.personid,p.student_no,p.middlename,m.*\r\nFROM Messaging_Users m LEFT JOIN people p ON p.personid=m.ID\r\nWHERE m.username=@username AND NOT p.personid IS NULL AND p.isactive=1";

		// Token: 0x040001AD RID: 429
		internal const string QS_GET_USER_MUST_CHANGE_PASSWORD = "SELECT requirepasswordchange FROM userinfo \r\nWHERE username=@username AND NOT requirepasswordchange IS NULL AND requirepasswordchange=1";

		// Token: 0x040001AE RID: 430
		internal const string SQ_ROLES_BY_USER = "select u.PersonID as UserID, r.GroupID as RoleID, r.description as 'RoleName' from Groups as r\r\n            Inner Join PeopleGroups as ur ON r.GroupID = ur.GroupID\r\n            Inner Join People as u ON ur.PersonID = u.PersonID\r\n            Where u.PersonID = @id";

		// Token: 0x040001AF RID: 431
		internal const string SQ_USER_BY_USERNAME = "select * from Messaging_Users Where username = @username";

		// Token: 0x040001B0 RID: 432
		internal const string SQ_EXISTS_USER_BY_USERNAME = "select 1 from Messaging_Users Where username = @username";

		// Token: 0x040001B1 RID: 433
		internal const string SQ_VALIDATE_USER_PASSWORD = "select 1 from Messaging_Users where username=@username and pass=@password";

		// Token: 0x040001B2 RID: 434
		internal const string QU_CHANGE_PASSWORD = "UPDATE userinfo SET pass=@passwordnew,requirepasswordchange=0,lastpasswordchangedate=getdate(),passwordexpirydate=NULL,isencrypted=@isencrypted\r\nWHERE username=@username; \r\nSELECT personid FROM userinfo WHERE username=@username AND pass=@passwordnew;";

		// Token: 0x040001B3 RID: 435
		internal const string QU_CLEAR_PASSWORD = "DELETE FROM userinfo \r\nWHERE username=@username;\r\nSELECT personid FROM userinfo WHERE username=@username";

		// Token: 0x040001B4 RID: 436
		internal const string QU_SET_PASSWORD = "IF EXISTS(SELECT personid FROM userinfo WHERE username=@username)\r\n    UPDATE userinfo SET pass=@passwordnew,isencrypted=@isencrypted WHERE username=@username\r\nELSE\r\nBEGIN\r\n    DECLARE @pid int\r\n    SET @pid = (SELECT TOP 1 personid FROM people WHERE isactive=1 AND student_no=@username)\r\n    IF NOT @pid IS NULL\r\n        INSERT INTO userinfo (personid,username,pass,isencrypted) VALUES (@pid,@username,@passwordnew,@isencrypted)\r\nEND\r\nSELECT personid FROM userinfo WHERE username=@username AND pass=@passwordnew;";
	}
}

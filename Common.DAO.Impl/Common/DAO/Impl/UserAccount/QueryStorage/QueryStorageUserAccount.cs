using System;

namespace TechnoPro.Common.DAO.Impl.UserAccount.QueryStorage
{
	// Token: 0x0200002F RID: 47
	public static class QueryStorageUserAccount
	{
		// Token: 0x04000069 RID: 105
		internal const string QS_LOAD_PASSWORD_BY_PID_AND_USERNAME = "SELECT username,pass,personid,requirepasswordchange,lastpasswordchangedate,passwordexpirydate,isencrypted FROM userinfo WHERE username=@username AND (@pid=0 OR personid=@pid)";

		// Token: 0x0400006A RID: 106
		internal const string QS_PIDS_BY_USERNAME = "SELECT personid FROM userinfo WHERE username=@username AND (( NOT @includedeleted IS NULL AND @includedeleted=1) OR NOT personid IN (SELECT personid FROM people WHERE isactive=0))";

		// Token: 0x0400006B RID: 107
		internal const string QD_PASSWORD = "DELETE FROM userinfo WHERE personid=@pid AND username=@username";

		// Token: 0x0400006C RID: 108
		internal const string QD_ALL_PASSWORDS = "DELETE FROM userinfo WHERE personid=@pid";

		// Token: 0x0400006D RID: 109
		internal const string QD_ALL_PASSWORDS_EXCEPT_PRIMARY = "DELETE FROM userinfo WHERE personid=@pid AND NOT username=@username";

		// Token: 0x0400006E RID: 110
		internal const string QI_PASSWORD = "INSERT INTO userinfo (username,personid,pass,requirepasswordchange,isencrypted) VALUES (@username,@pid,@password,@requirepasswordchange,@isencrypted)";

		// Token: 0x0400006F RID: 111
		internal const string QU_REQUIRECHANGE = "UPDATE userinfo SET requirepasswordchange=@requirepasswordchange,lastpasswordchangedate=getdate() WHERE personid=@pid AND username=@username";

		// Token: 0x04000070 RID: 112
		internal const string QU_PASSWORD_EXPIRY = "UPDATE userinfo SET passwordexpirydate=@passwordexpiry WHERE personid=@pid AND username=@username";

		// Token: 0x04000071 RID: 113
		internal const string QU_PASSWORD = "IF NOT EXISTS(SELECT 1 FROM userinfo WHERE personid=@pid AND username=@username)\r\n    INSERT INTO userinfo(personid,username,pass,isencrypted) VALUES (@pid,@username,@password,@isencrypted)\r\nELSE \r\n    UPDATE userinfo SET pass=@password,lastpasswordchangedate=getdate(),isencrypted=@isencrypted WHERE personid=@pid AND username=@username";

		// Token: 0x04000072 RID: 114
		internal const string QU_PASSWORD2 = "IF NOT EXISTS(SELECT 1 FROM userinfo WHERE personid=@pid AND username=@username)\r\n    INSERT INTO userinfo(personid,username,pass,requirepasswordchange,passwordexpirydate,isencrypted) VALUES (@pid,@username,@password,@requirepasswordchange,@passwordexpirydate,@isencrypted)\r\nELSE \r\n    UPDATE userinfo SET pass=@password,lastpasswordchangedate=getdate(),requirepasswordchange=@requirepasswordchange,passwordexpirydate=@passwordexpirydate,isencrypted=@isencrypted WHERE personid=@pid AND username=@username";
	}
}

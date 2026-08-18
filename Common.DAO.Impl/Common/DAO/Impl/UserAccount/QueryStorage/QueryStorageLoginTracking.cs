using System;

namespace TechnoPro.Common.DAO.Impl.UserAccount.QueryStorage
{
	// Token: 0x0200002E RID: 46
	public static class QueryStorageLoginTracking
	{
		// Token: 0x04000065 RID: 101
		internal const string QS_LOGIN_INFO_BY_PERSONID = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nSELECT PersonId,LoginDate,ip,ClockWorkVersion,NetVersion FROM UserLogins WHERE PersonId=@pid\r\nEND\r\nELSE\r\nBEGIN\r\nselect 1 where 0=1\r\nEND\r\n";

		// Token: 0x04000066 RID: 102
		internal const string QS_ALL_LOGIN_INFOS = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nSELECT PersonId,LoginDate,ip,ClockWorkVersion,NetVersion FROM UserLogins ORDER BY LoginDate desc\r\nEND\r\nELSE\r\nBEGIN\r\nselect 1 where 0=1\r\nEND";

		// Token: 0x04000067 RID: 103
		internal const string QS_LOGIN_INFOS_BY_DATE_RANGE = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nSELECT PersonId,LoginDate,ip,ClockWorkVersion,NetVersion FROM UserLogins WHERE LoginDate>=@sdate AND LoginDate<@edate ORDER BY LoginDate desc\r\nEND\r\nELSE\r\nBEGIN\r\nselect 1 where 0=1\r\nEND";

		// Token: 0x04000068 RID: 104
		internal const string QI_NEW_LOGIN_INFO = "IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserLogins')\r\nBEGIN\r\nIF EXISTS(SELECT personid FROM UserLogins WHERE personid=@pid)\r\n    UPDATE UserLogins SET LoginDate=getdate(),ip=@ip,ClockWorkVersion=@clockworkversion,NetVersion=@netversion WHERE PersonId=@pid\r\nELSE\r\n    INSERT INTO UserLogins (PersonId,LoginDate,ip,ClockWorkVersion,NetVersion) VALUES (@pid,getdate(),@ip,@clockworkversion,@netversion)\r\nEND";
	}
}

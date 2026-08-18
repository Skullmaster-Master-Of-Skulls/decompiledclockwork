using System;

namespace TechnoPro.ClockWorkServer.DAO.Impl.QueryStorage
{
	// Token: 0x0200000D RID: 13
	public static class AuthenticationSessionQueryStorage
	{
		// Token: 0x04000018 RID: 24
		internal const string SQ_ALL_SESSIONS = "select * from [ClockWorkServer_AuthenticationSession]";

		// Token: 0x04000019 RID: 25
		internal const string UQ_CLIENT_PARAMETERS_BY_ID = "update [ClockWorkServer_AuthenticationSession] set ClientParameters = @clientparameters where ID = @sessionid";

		// Token: 0x0400001A RID: 26
		internal const string IQ_AUTH_SESSION = "insert into [ClockWorkServer_AuthenticationSession] (ID, IssuedOn, NeverExpires, Username, ClientParameters) values(@sessionid, @issuedon, @neverexpires, @username, @clientparameters)";

		// Token: 0x0400001B RID: 27
		internal const string DQ_SESSION_BY_ID = "delete from [ClockWorkServer_AuthenticationSession] where ID = @sessionid";
	}
}

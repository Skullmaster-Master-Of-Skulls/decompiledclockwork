using System;

namespace TechnoPro.Common.DAO.Impl.Communications.QueryStorage
{
	// Token: 0x0200010C RID: 268
	internal static class QueryStorageCommunicationsSent
	{
		// Token: 0x04000473 RID: 1139
		internal const string QI_COMMUNICATION = "INSERT INTO Communications (PersonId,DateSendAttempted,SentSuccessfully,ErrorMessage,WhoSentPersonId,SendAttemptedMethods,Subject,Body)\r\nVALUES (@pid,getdate(),@sentsuccessfully,@errormessage,@whosentpersonid,@methods,@subject,@body)\r\n\r\nSET @id=(SELECT CAST(SCOPE_IDENTITY() AS int) AS id)";

		// Token: 0x04000474 RID: 1140
		internal const string QS_COMMUNICATIONS_BY_USER = "SELECT\tc.CommunicationId,c.PersonId,c.DateSendAttempted,c.SentSuccessfully,c.ErrorMessage,c.WhoSentPersonId,c.SendAttemptedMethods,c.[Subject],c.Body,\r\n        p.lastName AS WhoSentLastName,p.firstName AS WhoSentFirstName,p.middleName AS WhoSentMiddleName,p.student_no AS WhoSentStudent_no\r\nFROM    Communications c LEFT JOIN People p ON p.PersonId=c.WhoSentPersonId\r\nWHERE   c.PersonId= @pid\r\nORDER BY c.DateSendAttempted";
	}
}

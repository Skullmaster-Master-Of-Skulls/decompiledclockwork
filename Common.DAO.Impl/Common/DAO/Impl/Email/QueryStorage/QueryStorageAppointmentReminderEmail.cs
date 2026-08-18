using System;

namespace TechnoPro.Common.DAO.Impl.Email.QueryStorage
{
	// Token: 0x020000D5 RID: 213
	public static class QueryStorageAppointmentReminderEmail
	{
		// Token: 0x040002F6 RID: 758
		internal const string QI_EMAIL_HISTORY_ITEM = "INSERT INTO emailhistory (personid,templateid,datesent,sentby,etoccbcc,ebody,attachments,enote,successful,infopcid,lucourseid,emailtypecode)\r\nVALUES (@pid,@templateid,getdate(),NULL,@etoccbcc,@subject,'',@note,1,NULL,NULL,@title)";
	}
}

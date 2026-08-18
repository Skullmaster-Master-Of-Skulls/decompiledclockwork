using System;

namespace TechnoPro.Common.DAO.Impl.DataSync
{
	// Token: 0x020000FA RID: 250
	public static class QueryStorageDataSync
	{
		// Token: 0x0400041E RID: 1054
		internal const string QI_CREATE_NEW_BATCH_DATASYNC_LOG_ENTRY = "DECLARE @cutoffdate datetime = DATEADD(year,-1,getdate())\r\nDELETE FROM BatchDataSyncLog WHERE StartDateTime < @cutoffdate\r\n\r\nINSERT INTO BatchDataSyncLog (AttemptedStudentCount) VALUES (@attemptedstudentcount)\r\nSELECT TOP 1 CAST(@@identity AS int) AS batchdatasynclogid FROM BatchDataSyncLog";

		// Token: 0x0400041F RID: 1055
		internal const string QU_BATCH_DATASYNC_LOG_ENTRY = "UPDATE BatchDataSyncLog SET EndDateTime=getdate(),SuccessfulStudentCount=@successfulstudentcount,ErrorMessage=@errormessage WHERE BatchDataSyncLogId=@batchdatasynclogid";
	}
}

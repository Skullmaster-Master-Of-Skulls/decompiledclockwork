using System;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat.QueryStorage
{
	// Token: 0x02000173 RID: 371
	internal static class QueryStorageMediaJobStatus
	{
		// Token: 0x040006DA RID: 1754
		internal const string SQ_GET_JOB_STATUS_BY_NAME = "select * from AlternativeFormat_MediaJobStatus where MediaJobStatusName=@mediajobstatusname";

		// Token: 0x040006DB RID: 1755
		internal const string SQ_GET_JOB_STATUS_LIST = "select * from AlternativeFormat_MediaJobStatus\r\n            ORDER BY MediaJobStatusOrderNum, MediaJobStatusName";

		// Token: 0x040006DC RID: 1756
		internal const string SQ_GET_JOB_STATUS_LIST_BY_GROUP = "SELECT * FROM AlternativeFormat_MediaJobStatus\r\n            where MediaJobStatusGroupName=@mediajobstatusgroupname\r\n            ORDER BY MediaJobStatusOrderNum, MediaJobStatusName";

		// Token: 0x040006DD RID: 1757
		internal const string IQ_CREATE_JOB_STATUS = "declare @ordernum as int\r\nset @ordernum = COALESCE ((select MAX(MediaJobStatusOrderNum)+1 from AlternativeFormat_MediaJobStatus where MediaJobStatusGroupName=@mediajobstatusgroupname), 0)\r\n\r\nINSERT INTO [AlternativeFormat_MediaJobStatus]\r\n                    ([MediaJobStatusName]\r\n                    ,[MediaJobStatusDescription]\r\n                    ,[MediaJobStatusGroupName]\r\n                    ,[MediaJobStatusOrderNum])\r\n                VALUES\r\n                    (@mediajobstatusname\r\n                    ,@mediajobstatusdescription\r\n                    ,@mediajobstatusgroupname\r\n                    ,@ordernum)\r\n            set @mediajobstatusid=scope_identity()";
	}
}

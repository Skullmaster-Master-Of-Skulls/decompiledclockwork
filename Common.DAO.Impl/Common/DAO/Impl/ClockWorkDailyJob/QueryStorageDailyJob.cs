using System;

namespace TechnoPro.Common.DAO.Impl.ClockWorkDailyJob
{
	// Token: 0x02000114 RID: 276
	public class QueryStorageDailyJob
	{
		// Token: 0x04000496 RID: 1174
		internal const string QS_ACTIVE_JOB_GROUPS = "SELECT DISTINCT taskgroupid FROM windowstaskjob ORDER BY taskgroupid";

		// Token: 0x04000497 RID: 1175
		internal const string QS_ALL_TASKS = "SELECT    w.windowstaskjobid,w.taskgroupid,w.searchinfoid AS reportid,\r\n            si.title AS reporttitle,si.description AS reportdescription,\r\n            w.arguments,w.isactive,w.lastrunstartdate,w.lastrunenddate,w.lastrunresult,\r\n            w.description,w.ordernum\r\nFROM        windowstaskjob w LEFT JOIN searchinfo si ON si.searchinfoid=w.searchinfoid\r\nWHERE       @taskgroupid=-1 OR w.taskgroupid=@taskgroupid\r\nORDER BY    w.taskgroupid,w.ordernum";

		// Token: 0x04000498 RID: 1176
		internal const string QS_TASK_BY_ID = "SELECT    w.windowstaskjobid,w.taskgroupid,w.searchinfoid AS reportid,\r\n            si.title AS reporttitle,si.description AS reportdescription,\r\n            w.arguments,w.isactive,w.lastrunstartdate,w.lastrunenddate,w.lastrunresult,\r\n            w.description,w.ordernum\r\nFROM        windowstaskjob w LEFT JOIN searchinfo si ON si.searchinfoid=w.searchinfoid\r\nWHERE       w.windowstaskjobid=@id\r\nORDER BY    w.taskgroupid,w.ordernum";

		// Token: 0x04000499 RID: 1177
		internal const string QI_TASK = "INSERT INTO windowstaskjob \r\n    (taskgroupid,searchinfoid,arguments,isactive,lastrunstartdate,lastrunenddate,lastrunresult,description,ordernum)\r\nVALUES\r\n    (@taskgroupid,@reportid,@arguments,@isactive,@lastrunstartdate,@lastrunenddate,@lastrunresult,@description,@ordernum);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS windowstaskjobid";

		// Token: 0x0400049A RID: 1178
		internal const string QI_TASK_RESULT_START = "INSERT INTO windowstaskjobsetresults \r\n    (taskgroupid,runstartdate,runenddate,runresult,runcomment)\r\nVALUES\r\n    (@taskgroupid,@runstartdate,@runenddate,@runresult,@runcomment);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS windowstaskjobsetresultsid";

		// Token: 0x0400049B RID: 1179
		internal const string QI_TASK_RUN_RESULT = "INSERT INTO WindowsTaskJobResult (windowstaskjobid,reportid,taskgroupid,enddate,successful,runresult) VALUES (@windowstaskjobid,@reportid,@taskgroupid,@enddate,@successful,@runresult);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS windowstaskjobresultid";

		// Token: 0x0400049C RID: 1180
		internal const string QU_TASK_RUN_RESULT_END = "UPDATE WindowsTaskJobResult SET successful=@successful,runresult=@runresult,enddate=getdate() WHERE WindowsTaskJobResultId=@windowstaskjobresultid";

		// Token: 0x0400049D RID: 1181
		internal const string QU_TASK = "UPDATE windowstaskjob SET searchinfoid=@reportid,arguments=@arguments,isactive=@isactive,description=@description,ordernum=@ordernum,taskgroupid=@groupid\r\nWHERE windowstaskjobid=@id";

		// Token: 0x0400049E RID: 1182
		internal const string QU_TASK_RUN = "UPDATE windowstaskjob SET lastrunstartdate=@sd,lastrunenddate=getdate(),lastrunresult=@result\r\nWHERE windowstaskjobid=@id";

		// Token: 0x0400049F RID: 1183
		internal const string QU_TASK_RESULT_END = "UPDATE windowstaskjobsetresults SET runenddate=@runenddate,runresult=@runresult,runcomment=@runcomment WHERE windowstaskjobsetresultsid=@id";

		// Token: 0x040004A0 RID: 1184
		internal const string QU_TASK_ISACTIVE = "UPDATE windowstaskjob SET isactive=@isactive WHERE windowstaskjobid=@id";

		// Token: 0x040004A1 RID: 1185
		internal const string QD_TASK = "DELETE FROM windowstaskjob WHERE windowstaskjobid=@id";
	}
}

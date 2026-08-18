using System;

namespace TechnoPro.Common.DAO.Impl.ClockWorkServerJob.QueryStorage
{
	// Token: 0x02000110 RID: 272
	public static class ClockWorkServerJobQueryStorage
	{
		// Token: 0x0400047E RID: 1150
		internal const string DQ_DELETE_CLOCKWORKSERVER_JOB = "delete from [ClockWorkServerJob_JobInfo] where JobId=@jobid";

		// Token: 0x0400047F RID: 1151
		internal const string IQ_CREATE_CLOCKWORKSERVER_JOB = "insert into ClockWorkServerJob_JobInfo\r\n\t            (JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword)\r\n            values\r\n\t            (@jobtitle, @jobnotes, @starttimeinminutes, @jobschedule, @timeoutinminutes, @isjobactive, @impersonationdomain, @impersonationusername, @impersonationpassword)\r\n            set @jobid = SCOPE_IDENTITY()";

		// Token: 0x04000480 RID: 1152
		internal const string UQ_ENABLE_DISABLE_CLOCKWORKSERVER_JOB = "Update [ClockWorkServerJob_JobInfo] set IsJobActive=@isactive where JobID=@jobid";

		// Token: 0x04000481 RID: 1153
		internal const string UQ_UPDATE_CLOCKWORKSERVER_JOB_LAST_RUN = "update ClockWorkServerJob_JobInfo\r\n                set LastRunStartDatetime = @lastrunstartdatetime\r\n                   ,LastRunEndDatetime = @lastrunenddatetime\r\n                   ,LastRunStatus = @lastrunstatus\r\n                   ,LastRunMessage = @lastrunmessage\r\n                where JobId = @jobid";

		// Token: 0x04000482 RID: 1154
		internal const string UQ_UPDATE_CLOCKWORKSERVER_JOB_LAST_RUN_FOR_BEGIN = "update ClockWorkServerJob_JobInfo\r\n                set LastRunStartDatetime = @lastrunstartdatetime\r\n                   ,LastRunStatus = @lastrunstatus\r\n                   ,LastRunMessage = @lastrunmessage\r\n                where JobId = @jobid";

		// Token: 0x04000483 RID: 1155
		internal const string UQ_UPDATE_CLOCKWORKSERVER_JOB_LAST_RUN_FOR_END = "update ClockWorkServerJob_JobInfo\r\n                set LastRunEndDatetime = @lastrunenddatetime\r\n                   ,LastRunStatus = @lastrunstatus\r\n                   ,LastRunMessage = @lastrunmessage\r\n                where JobId = @jobid";

		// Token: 0x04000484 RID: 1156
		internal const string DQ_REMOVE_CLOCKWORKSERVER_JOB_STEP = "delete from [ClockWorkServerJob_JobSteps] where FK_JobId=@jobid and JobStepId = @stepid";

		// Token: 0x04000485 RID: 1157
		internal const string IQ_CREATE_CLOCKWORKSERVER_EXECUTION_LOG = "insert into [ClockworkserverJob_ExecutionLogs]\r\n\t            (FK_JobId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress, TransactionId)\r\n            values\r\n\t            (@fk_jobid, @fk_stepid, @jobexecutionstatus, @starttime, @endtime, @message, @serveripaddress, @transactionid)\r\n            set @joblogid = SCOPE_IDENTITY()";

		// Token: 0x04000486 RID: 1158
		internal const string SQ_GET_CLOCKWORKSERVER_JOBS_BASE = "select JobId, JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, JobUniqueId, LastRunStartDatetime, LastRunEndDatetime, LastRunStatus, LastRunMessage, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword, IsSystemJob \r\nfrom [ClockWorkServerJob_JobInfo]";

		// Token: 0x04000487 RID: 1159
		internal const string SQ_GET_CLOCKWORKSERVER_JOB_STEPS_BASE = "select JobStepId, FK_JobId, JobType, StepTitle, StepNotes, StepParams, StepOrderNum, IsStepActive from [ClockWorkServerJob_JobSteps]";

		// Token: 0x04000488 RID: 1160
		internal const string SQ_GET_CLOCKWORKSERVER_JOB_STEPS_BY_JOB_ID = "select JobStepId, FK_JobId, JobType, StepTitle, StepNotes, StepParams, StepOrderNum, IsStepActive from [ClockWorkServerJob_JobSteps] where Fk_JobId = @jobid order by StepOrderNum ASC";

		// Token: 0x04000489 RID: 1161
		internal const string SQ_GET_CLOCKWORKSERVER_JOB_STEP_BY_ID = "select JobStepId, FK_JobId, JobType, StepTitle, StepNotes, StepParams, StepOrderNum, IsStepActive from [ClockWorkServerJob_JobSteps] where FK_JobId = @jobid and JobStepId = @stepid";

		// Token: 0x0400048A RID: 1162
		internal const string SQ_GET_CLOCKWORKSERVER_JOB_STEP_BY_ORDERNUM = "select JobStepId, FK_JobId, JobType, StepTitle, StepNotes, StepParams, StepOrderNum, IsStepActive from [ClockWorkServerJob_JobSteps] where FK_JobId = @jobid and StepOrderNum = @stepordernum";

		// Token: 0x0400048B RID: 1163
		internal const string SQ_GET_CLOCKWORKSERVER_JOBS = "select JobId, JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, JobUniqueId, LastRunStartDatetime, LastRunEndDatetime, LastRunStatus, LastRunMessage, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword, IsSystemJob \r\nfrom [ClockWorkServerJob_JobInfo] order by StartTimeInMinutes ASC";

		// Token: 0x0400048C RID: 1164
		internal const string SQ_GET_ACTIVE_CLOCKWORKSERVER_JOBS = "select JobId, JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, JobUniqueId, LastRunStartDatetime, LastRunEndDatetime, LastRunStatus, LastRunMessage, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword, IsSystemJob \r\nfrom [ClockWorkServerJob_JobInfo] where IsJobActive=1 order by StartTimeInMinutes ASC";

		// Token: 0x0400048D RID: 1165
		internal const string SQ_GET_CLOCKWORKSERVER_JOB_BY_ID = "select JobId, JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, JobUniqueId, LastRunStartDatetime, LastRunEndDatetime, LastRunStatus, LastRunMessage, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword, IsSystemJob \r\nfrom [ClockWorkServerJob_JobInfo] where JobID=@jobid";

		// Token: 0x0400048E RID: 1166
		internal const string SQ_GET_CLOCKWORKSERVER_EXECUTINGLOGS_BASE = "select JobLogId, FK_JobId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress, TransactionId from [ClockWorkServerJob_ExecutionLogs]";

		// Token: 0x0400048F RID: 1167
		internal const string SQ_GET_CLOCKWORKSERVER_EXECUTINGLOGS_BY_ID_AND_DATES = "select JobLogId, FK_JobId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress, TransactionId from [ClockWorkServerJob_ExecutionLogs] where FK_JobId = @jobid and StartTime between @starttime and @endtime group by transactionid, FK_JobId, JobLogId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress order by StartTime DESC";

		// Token: 0x04000490 RID: 1168
		internal const string SQ_GET_CLOCKWORKSERVER_EXECUTINGLOGS_BY_DATES = "select JobLogId, FK_JobId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress, TransactionId from [ClockWorkServerJob_ExecutionLogs] where StartTime between @starttime and @endtime group by transactionid, FK_JobId, JobLogId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress order by StartTime DESC";
	}
}
